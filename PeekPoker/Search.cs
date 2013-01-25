using System;
using System.ComponentModel;
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
                resultGrid.Rows[value - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
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
                var value = 0;
                foreach (var item in _searchResult)
                {
                    UpdateProgressbar(0, _searchResult.Count, value, "Refreshing...");
                    value++;

                    //peekPokeAddressTextBox.Text = "0x" + _item.Offset;
                    var length = (item.Value.Length / 2).ToString("X");
                    var retvalue = _rtm.Peek("0x" + item.Offset, length, "0x" + item.Offset, length);

                    if (item.Value == retvalue) continue;//if value hasn't change continue for each loop

                    GridRowColours(value);
                    item.Value = retvalue;
                }
                ResultGridUpdate();
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
                //thread.Name = "RefreshResultsList";
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

                if (lengthRangeAddressTextBox.Text.StartsWith("0x")) return;
                if (!lengthRangeAddressTextBox.Text.Equals(""))
                    lengthRangeAddressTextBox.Text = "0x" + uint.Parse(lengthRangeAddressTextBox.Text).ToString("X");
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
    }
}
