using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker
{
    public partial class Search : Form
    {
        public event ShowMessageBoxHandler ShowMessageBox;
        public event UpdateProgressBarHandler UpdateProgressbar;
        public event EnableControlHandler EnableControl;
        public event GetTextBoxTextHandler GetTextBoxText;

        private BindingList<SearchResults> _searchResult = new BindingList<SearchResults>();
        private RealTimeMemory _rtm;

        public Search(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
            resultGrid.DataSource = _searchResult;
        }

        //Control changes
        private void GridRowColours(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(() => GridRowColours(value)));
            else
                resultGrid.Rows[value - 1].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void GridRowRemove(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(() => GridRowRemove(value)));
            else
                //resultGrid.Rows[value - 1].
                resultGrid.Rows.RemoveAt(resultGrid.Rows[value - 1].Index);
        }

        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                Thread oThread = new Thread(SearchRange);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Refresh results Thread
        private void RefreshResultList()
        {
            try
            {
                EnableControl(resultRefreshButton, false);
                BindingList<SearchResults> newSearchResults = new BindingList<SearchResults>();
                var value = 0;
                foreach (var item in _searchResult)
                {
                    UpdateProgressbar(0, _searchResult.Count, value, "Refreshing...");
                    value++;

                    var length = (item.Value.Length / 2).ToString("X");
                    var retvalue = _rtm.Peek("0x" + item.Offset, length, "0x" + item.Offset, length);

                    //===================================================
                    //Default
                    if(defaultRadioButton.Checked)
                    {
                        if (item.Value == retvalue) continue; //if value hasn't change continue for each loop
                        this.GridRowColours(value);
                        item.Value = retvalue;
                    }
                    else if (ifEqualsRadioButton.Checked)
                    {
                        if (retvalue == newValueTextBox.Text)
                        {
                            SearchResults searchResultItem = new SearchResults
                                                                 {
                                                                     ID = item.ID,
                                                                     Offset = item.Offset,
                                                                     Value = retvalue
                                                                 };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifGreaterThanRadioButton.Checked)
                    {
                        uint currentResults;
                        uint newResult;

                        if(!uint.TryParse("0x" + searchRangeValueTextBox.Text, out currentResults))
                            throw new Exception("Invalid Search Value this function only works for Unsigned Integers.");
                        uint.TryParse("0x" + retvalue, out newResult);

                        if (newResult > currentResults)
                        {
                            SearchResults searchResultItem = new SearchResults
                            {
                                ID = item.ID,
                                Offset = item.Offset,
                                Value = retvalue
                            };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifLessThanRadioButton.Checked)
                    {
                        uint currentResults;
                        uint newResult;

                        if (!uint.TryParse("0x" + searchRangeValueTextBox.Text, out currentResults))
                            throw new Exception("Invalid Search Value this function only works for Unsigned Integers.");
                        uint.TryParse("0x" + retvalue, out newResult);

                        if (newResult < currentResults)
                        {
                            SearchResults searchResultItem = new SearchResults
                            {
                                ID = item.ID,
                                Offset = item.Offset,
                                Value = retvalue
                            };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifLessThanRadioButton.Checked)
                    {
                        if (retvalue != newValueTextBox.Text)
                        {
                            SearchResults searchResultItem = new SearchResults
                            {
                                ID = item.ID,
                                Offset = item.Offset,
                                Value = retvalue
                            };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                }
                if(defaultRadioButton.Checked)
                    ResultGridUpdate();
                else
                {
                    _searchResult = newSearchResults;
                    ResultGridUpdate();
                }
                UpdateProgressbar(0, 100, 0, "idle");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(resultRefreshButton, true);
                UpdateProgressbar(0, 100, 0, "idle");
                Thread.CurrentThread.Abort();
            }
        }

        // Refresh results
        private void ResultRefreshClick(object sender, EventArgs e)
        {
            if (_searchResult.Count > 0)
            {
                var thread = new Thread(RefreshResultList);
                thread.Start();
            }
            else
            {
                ShowMessageBox("Can not refresh! \r\n Result list empty!!", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResultGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = (DataGridCell)sender;
            if (resultGrid.Rows[cell.RowNumber].Cells[2].Value != null)
                resultGrid.Rows[cell.RowNumber].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }

        private void SearchRangeValueTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !searchRangeValueTextBox.Focused) return;
            var oThread = new Thread(SearchRange);
            oThread.Start();
            e.Handled = true;
            searchRangeButton.Focus();
        }

        //Searches the memory for the specified value (Experimental)
        private void SearchRange()
        {
            try
            {
                EnableControl(searchRangeButton, false);
                EnableControl(stopSearchButton, true);
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(startRangeAddressTextBox));
                _rtm.DumpLength = Functions.Convert(GetTextBoxText(lengthRangeAddressTextBox));

                ResultGridClean();//Clean list view

                //The ExFindHexOffset function is a Experimental search function
                var results = _rtm.FindHexOffset(GetTextBoxText(searchRangeValueTextBox));//pointer
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0);

                if (results.Count < 1)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }
                _searchResult = results;
                ResultGridUpdate();
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(searchRangeButton, true);
                EnableControl(stopSearchButton, false);
                Thread.CurrentThread.Abort();
            }
        }


        //Refresh the values of Search Results
        private void ResultGridClean()
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(ResultGridClean));
            else
                resultGrid.Rows.Clear();
        }
        private void ResultGridUpdate()
        {
            //IList or represents a collection of objects(String)
            if (resultGrid.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                resultGrid.Invoke((MethodInvoker)(ResultGridUpdate));
            else
            {
                resultGrid.DataSource = _searchResult;
                resultGrid.Refresh();
            }
        }

        private void StopSearchButtonClick(object sender, EventArgs e)
        {
            _rtm.StopSearch = true;
        }

        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!startRangeAddressTextBox.Text.StartsWith("0x"))
                {
                    if (!startRangeAddressTextBox.Text.Equals(""))
                        startRangeAddressTextBox.Text = "0x" + uint.Parse(startRangeAddressTextBox.Text).ToString("X");
                }

                if (!lengthRangeAddressTextBox.Text.StartsWith("0x"))
                {
                    if (!lengthRangeAddressTextBox.Text.Equals(""))
                        lengthRangeAddressTextBox.Text = "0x" + uint.Parse(lengthRangeAddressTextBox.Text).ToString("X");

                }

                uint value = Convert.ToUInt32(startRangeAddressTextBox.Text, 16);
                uint valueTwo = Convert.ToUInt32(lengthRangeAddressTextBox.Text, 16);
                totalTextBoxText.Text = "0x" +(value+valueTwo).ToString("X");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchRangeValueTextBoxLeave(object sender, EventArgs e)
        {
            searchRangeValueTextBox.Text = searchRangeValueTextBox.Text.Replace(" ", "");
        }

        private void resultGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(string.Format("0x" + resultGrid.Rows[resultGrid.SelectedRows[0].Index].Cells[1].Value));
                e.SuppressKeyPress = true;
            }
        }
    }
}
