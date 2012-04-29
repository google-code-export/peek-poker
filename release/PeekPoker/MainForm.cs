using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;

//=====================================================
// Namespaces        -> PascalCased                  //
// Class names       -> PascalCased                  //
// Private Variables -> _carmelCased + Underscore    //
// Public Variables  -> PascalCased                  //
// Local Variables   -> carmelCased                  //
//=====================================================

namespace PeekPoker
{
    #region Delegates
    public delegate void UpdateProgressBarHandler(int min, int max, int value, string text);
    #endregion

    public partial class MainForm : Form
    {
        #region global varibales
        private RealTimeMemory.RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        private uint _searchRangeDumpLength;
        private List<string> _offsets;
        private BindingList<Types.SearchResults> _searchResult = new BindingList<Types.SearchResults>();
        #endregion

        public MainForm()
        {
            InitializeComponent();

            //Set comboboxes correctly - need to be here for some reason... or it won't work...
            searchRangeBaseValueTypeCB.SelectedIndex = 0;
            searchRangeEndTypeCB.SelectedIndex = 0;
            resultGrid.DataSource = _searchResult;
        }
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();//Immidiable stop the process
        }
        private void Form1Load(Object sender, EventArgs e)
        {
            //feature suggested by fairchild
            var xboxname = (string)Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "XboxName", "NotFound");
            if (xboxname != "NotFound")
                ipAddressTextBox.Text = xboxname;
            //This is for handling automatic loading of the IP address and txt file creation. -8Ball
            //Changed a bit to only check if it does exist creation and fill code is in the same place now - sam
            if (File.Exists(_filepath)) ipAddressTextBox.Text = File.ReadAllText(_filepath);

            //Set correct max. min values for the numeric fields
            ChangeNumericMaxMin();
        }

        #region button clicks
        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm = new RealTimeMemory.RealTimeMemory(ipAddressTextBox.Text, 0, 0);//initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect()) throw new Exception("Connection Failed!");
                peeknpoke.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!File.Exists(_filepath)) File.Create(_filepath).Dispose(); //Create the file if it doesn't exist
                var objWriter = new StreamWriter(_filepath); //Writer Declaration
                objWriter.Write(ipAddressTextBox.Text); //Writes IP address to text file
                objWriter.Close(); //Close Writer
                connectButton.Text = String.Format("Re-Connect");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        //When you click on the peek button
        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete();//run function
            try
            {
                if (string.IsNullOrEmpty(peekLengthTextBox.Text))
                    throw new Exception("Invalide peek length!");
                var retValue = Functions.StringToByteArray(_rtm.Peek(peekPokeAddressTextBox.Text, peekLengthTextBox.Text, peekPokeAddressTextBox.Text, peekLengthTextBox.Text));
                var buffer = new Be.Windows.Forms.DynamicByteProvider(retValue) {IsWriteByte = true}; //object initilizer 
                hexBox.ByteProvider = buffer;
                hexBox.Refresh();
                // the changed are handled automatically with my modifications of Be.HexBox

                MessageBox.Show(this, String.Format("Done!"), String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        //When you click on the poke button
        private void PokeButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            try
            {
                _rtm.DumpOffset = Functions.Convert(peekPokeAddressTextBox.Text);//Set the dump offset
                _rtm.DumpLength = (uint)hexBox.ByteProvider.Length / 2;//The length of data to dump

                var buffer = (Be.Windows.Forms.DynamicByteProvider)hexBox.ByteProvider;

                Console.WriteLine(Functions.ByteArrayToString(buffer.Bytes.ToArray()));//?????
                _rtm.Poke(peekPokeAddressTextBox.Text, Functions.ByteArrayToString(buffer.Bytes.ToArray()));
                MessageBox.Show(this, String.Format("Done!"), String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //When you click on the www.360haven.com
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            Process.Start("www.360haven.com");
        }

        //When you click on the new button
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
        }

        //When you click on the search range button - Search Range Tab
        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            if (searchRangeEndTypeCB.SelectedIndex == 1)
            {
                _searchRangeDumpLength = (Functions.Convert(endRangeAddressTextBox.Text) - Functions.Convert(startRangeAddressTextBox.Text));
            }
            else
            {
                _searchRangeDumpLength = Functions.Convert(endRangeAddressTextBox.Text);
            }
            var oThread = new Thread(SearchRange);
            oThread.Start();
        }

        //Experimental search Button
        private void ExSearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (searchRangeEndTypeCB.SelectedIndex == 1)
                {
                    _searchRangeDumpLength = (Functions.Convert(endRangeAddressTextBox.Text) - Functions.Convert(startRangeAddressTextBox.Text));
                }
                else
                {
                    _searchRangeDumpLength = Functions.Convert(endRangeAddressTextBox.Text);
                }
                var oThread = new Thread(ExSearchRange);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //When you click on an item on the search range result list view - Search Range tab
        private void SearchRangeResultListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return; //if its not a left click return
            peekPokeAddressTextBox.Text = string.Format("0x" + resultGrid.Rows[resultGrid.SelectedRows[0].Index].Cells[1].Value);
        }
        private void ResultGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = (DataGridCell)sender;
            if (resultGrid.Rows[cell.RowNumber].Cells[2].Value != null)
                resultGrid.Rows[cell.RowNumber].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
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
                ShowMessageBox("Can not refresh! \r\n Resultlist empty!!", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //stop the search
        private void StopSearchButtonClick(object sender, EventArgs e)
        {
            _rtm.StopSearch = true; 
        }
        #endregion
        
        #region HexBox Events
        private void HexBoxSelectionStartChanged(object sender, EventArgs e)
        {
            ChangeNumericValue();//When you select an offset on the hexbox

            var prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format("0x" + (address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void IsSignedCheckedChanged(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
            ChangeNumericValue();
        }
        private void NumericIntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }
        #endregion
        
        #region Search Tab Events
        private void SearchRangeValueTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !searchRangeValueTextBox.Focused) return;

            if (searchRangeEndTypeCB.SelectedIndex == 1)
            {
                _searchRangeDumpLength = (Functions.Convert(endRangeAddressTextBox.Text) - Functions.Convert(startRangeAddressTextBox.Text));
            }
            else
            {
                _searchRangeDumpLength = Functions.Convert(endRangeAddressTextBox.Text);
            }
            
            var oThread = new Thread(SearchRange);
            oThread.Start();
            e.Handled = true;
            searchRangeButton.Focus();
        }
        #endregion

        #region functions
        private void NewPeek()
        {
            //Clean up
            peekPokeAddressTextBox.Clear();
            peekLengthTextBox.Clear();
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }
        private void AutoComplete()
        {
            peekPokeAddressTextBox.AutoCompleteCustomSource = _data;//put the auto complete data into the textbox
            var count = _data.Count;
            for (var index = 0; index < count; index++)
            {
                var value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, peekPokeAddressTextBox.Text))
                    _data.Add(peekPokeAddressTextBox.Text);
            }
        }
        
        //When you select an offset on the hexbox
        private void ChangeNumericValue()
        {
            if (hexBox.ByteProvider == null) return;
            var buffer = hexBox.ByteProvider.Bytes;
            if (isSigned.Checked)
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0 ? 
                    Functions.ByteToSByte(hexBox.ByteProvider.ReadByte(hexBox.SelectionStart)) : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1 ?
                    Functions.BytesToInt16(buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray()) : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3 ?
                    Functions.BytesToInt32(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
            }
            else
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0 ?
                    buffer[(int)hexBox.SelectionStart] : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1 ? 
                    Functions.BytesToUInt16(buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray()) : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3 ?
                    Functions.BytesToUInt32(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
            }
            var prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format("0x" + (address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void ChangedNumericValue(object sender)
        {
            if (hexBox.SelectionStart >= hexBox.ByteProvider.Bytes.Count) return;
            var numeric = (NumericUpDown)sender;
            switch (numeric.Name)
            {
                case "NumericInt8":
                    if (isSigned.Checked)
                    {
                        Console.WriteLine(((sbyte)numeric.Value).ToString("X2"));
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                      Functions.HexToBytes(((sbyte)numeric.Value).ToString("X2"))[0]);
                    }
                    else
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                      Convert.ToByte((byte)numeric.Value));
                    }
                    break;
                case "NumericInt16":
                    for (var i = 0; i < 2; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,isSigned.Checked
                                                                                    ? Functions.Int16ToBytes((short) numeric.Value)[i]
                                                                                    : Functions.UInt16ToBytes((ushort) numeric.Value)[i]);
                    }
                    break;
                case "NumericInt32":
                    for (var i = 0; i < 4; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,isSigned.Checked
                                                                                    ? Functions.Int32ToBytes((int) numeric.Value)[i]
                                                                                    : Functions.UInt32ToBytes((uint) numeric.Value)[i]);
                    }
                    break;
            }
            hexBox.Refresh();
        }
        private void ChangeNumericMaxMin()
        {
            if (isSigned.Checked)
            {
                NumericInt8.Maximum = SByte.MaxValue;
                NumericInt8.Minimum = SByte.MinValue;
                NumericInt16.Maximum = Int16.MaxValue;
                NumericInt16.Minimum = Int16.MinValue;
                NumericInt32.Maximum = Int32.MaxValue;
                NumericInt32.Minimum = Int32.MinValue;
            }
            else
            {
                NumericInt8.Maximum = Byte.MaxValue;
                NumericInt8.Minimum = Byte.MinValue;
                NumericInt16.Maximum = UInt16.MaxValue;
                NumericInt16.Minimum = UInt16.MinValue;
                NumericInt32.Maximum = UInt32.MaxValue;
                NumericInt32.Minimum = UInt32.MinValue;
            }
        }
        #endregion

        #region Thread Functions
        //Refresh results Thread
        private void RefreshResultList()
        {
            try
            {
                var value = 0;
                foreach (var item in _searchResult)
                {
                    UpdateProgressbar(0, _searchResult.Count, value, "Refreshing...");
                    value++;

                    //peekPokeAddressTextBox.Text = "0x" + _item.Offset;
                    var length = (item.Value.Length / 2).ToString("X");
                    var retvalue = _rtm.Peek("0x" + item.Offset, length, "0x" + item.Offset, length);

                    if (item.Value == retvalue) continue;//if value hasn't change continue foreach loop

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
                Thread.CurrentThread.Abort();
            }
        }
        //Search the memory for the specified value
        private void SearchRange()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false; //line 463 grid cross thread error
                EnableExSearchRangeButton(false);
                EnableStopSearchButton(true);
                EnableSearchRangeButton(false);
                _rtm.DumpOffset = GetStartRangeAddressTextBoxText();
                _rtm.DumpLength = _searchRangeDumpLength;
                ResultGridClean();//Clean list view

                //The FindHexOffset function is slow in searching - I might use Mojo's algorithm
                _offsets = _rtm.FindHexOffset(GetSearchRangeValueTextBoxText());//pointer
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0);

                if (_offsets.Count < 1)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }

                var i = 0; //The index number
                foreach (var offset in _offsets)
                {
                    //create a new variable of the SearchResult type and set the variables
                    var results = new Types.SearchResults { Offset = offset, ID = i.ToString() };
                    //add the new variable to the result list
                    _searchResult.Add(results);
                    i++;
                }
                ResultGridUpdate();
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableExSearchRangeButton(true);
                EnableSearchRangeButton(true);
                EnableStopSearchButton(false);
                Thread.CurrentThread.Abort();
            }
        }
        //Searches the memory for the specified value (Experimental)
        private void ExSearchRange()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false; //line 476 grid cross thread error
                EnableSearchRangeButton(false);
                EnableExSearchRangeButton(false);
                EnableStopSearchButton(true);
                _rtm.DumpOffset = GetStartRangeAddressTextBoxText();
                _rtm.DumpLength = _searchRangeDumpLength;

                ResultGridClean();//Clean list view

                //The ExFindHexOffset function is a Experimental search function
                var results = _rtm.ExFindHexOffset(GetSearchRangeValueTextBoxText());//pointer
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
                EnableExSearchRangeButton(true);
                EnableSearchRangeButton(true);
                EnableStopSearchButton(false);
                Thread.CurrentThread.Abort();
            }
        }
        #endregion

        #region safeThreadingProperties
        //==================================================================
        // This method demonstrates a pattern for making thread-safe
        // calls on a Windows Forms control. 
        //
        // If the calling thread is different from the thread that
        // created the TextBox control, this method creates a
        // Set/Get Method and calls itself asynchronously using the
        // Invoke method.
        //
        // If the calling thread is the same as the thread that created
        // the TextBox control, the Text property is set directly. 
        //Reference: http://msdn.microsoft.com/en-us/library/ms171728.aspx
        //===================================================================

        //Get and Set values
        private String GetSearchRangeValueTextBoxText()//Get the value from the textbox - safe
        {
            //recursion
            var returnVal = "";
            if (searchRangeValueTextBox.InvokeRequired) searchRangeValueTextBox.Invoke((MethodInvoker)
                  delegate { returnVal = GetSearchRangeValueTextBoxText(); });
            else
                return searchRangeValueTextBox.Text;
            return returnVal;
        }
        private uint GetStartRangeAddressTextBoxText()
        {
            //recursion
            uint returnVal = 0;
            if (startRangeAddressTextBox.InvokeRequired) startRangeAddressTextBox.Invoke((MethodInvoker)
                  delegate { returnVal = GetStartRangeAddressTextBoxText(); });
            else
                return Functions.Convert(startRangeAddressTextBox.Text);
            return returnVal;
        }
        private void EnableStopSearchButton(bool value)
        {
            if (stopSearchButton.InvokeRequired)
                stopSearchButton.Invoke((MethodInvoker)delegate { EnableStopSearchButton(value); });
            else
                stopSearchButton.Enabled = value;
        }
        private void EnableSearchRangeButton(bool value)
        {
            if (searchRangeButton.InvokeRequired)
                searchRangeButton.Invoke((MethodInvoker)delegate { EnableSearchRangeButton(value); });
            else
                searchRangeButton.Enabled = value;
        }
        private void EnableExSearchRangeButton(bool value)
        {
            if (EXsearchRangeButton.InvokeRequired)
                EXsearchRangeButton.Invoke((MethodInvoker)delegate { EnableExSearchRangeButton(value); });
            else
                EXsearchRangeButton.Enabled = value;
        }
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }

        //Control changes
        private void GridRowColours(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)delegate { GridRowColours(value); });
            else
                resultGrid.Rows[value - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
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

        //Progressbar Delegates
        private void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker)(() => UpdateProgressbar(min, max, value, text)));
            }
            else
            {
                if (StatusProgressBar.ProgressBar != null)
                {
                    StatusProgressBar.ProgressBar.Maximum = max;
                    StatusProgressBar.ProgressBar.Minimum = min;
                    StatusProgressBar.ProgressBar.Value = value;
                }
                statusStripLabel.Text = text;
            }
            
                
        }
        #endregion

        #region Addressbox Autocorrection
        // These will automatically add "0x" to an offset if it hasn't been added already - 8Ball
        private void FixTheAddresses(object sender, EventArgs e)
        {
            if (!peekPokeAddressTextBox.Text.StartsWith("0x")) //Peek Address Box, Formatting Check.
            {//PeekPokeAddress
                if (!peekPokeAddressTextBox.Text.Equals("")) //Empty Check
                    peekPokeAddressTextBox.Text = (string.Format("0x" + peekPokeAddressTextBox.Text)); //Formatting
            }
            if (peekLengthTextBox.Text.StartsWith("0x")) // Checks if peek length is hex value or not based on 0x
            { //Peeklength pt1
                string result = (peekLengthTextBox.Text.ToUpper().Substring(2));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(peekLengthTextBox.Text.ToUpper(), "^[A-Z]$")) //Checks if hex, based on uppercase alphabet presence.
            {//Peeklength pt2
                string result = (peekLengthTextBox.Text.ToUpper());
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (peekLengthTextBox.Text.StartsWith("h")) //Checks if hex, based on starting with h.
            {//Peeklength pt3
                string result = (peekLengthTextBox.Text.ToUpper().Substring(1));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            if (!startRangeAddressTextBox.Text.StartsWith("0x"))
            {//RangeStart
                if (!startRangeAddressTextBox.Text.Equals(""))
                    startRangeAddressTextBox.Text = (string.Format("0x" + startRangeAddressTextBox.Text));
            }
            if (!endRangeAddressTextBox.Text.StartsWith("0x"))
            {//RangeEnd
                if (!endRangeAddressTextBox.Text.Equals(""))
                    endRangeAddressTextBox.Text = (string.Format("0x" + endRangeAddressTextBox.Text));
            }
        }
        #endregion

        #region Autocalculation
        private void Dec2Hex(object sender, EventArgs e)
        {
            if (decimalbox.Focused) //Prevents chaos via triggering both textchange events
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(decimalbox.Text.ToUpper(), "[A-Z]")) //Checks for alpha characters, we don't like those
                {
                    Int32 number;
                    bool result = Int32.TryParse(decimalbox.Text, out number); //Stops things like a single "-" causing errors
                    if (result)
                    {
                        string hex = number.ToString("X4"); //x is for hex and 4 is padding to a 4 digit value, uppercases.
                        hexcalcbox.Text = (string.Format("0x" + hex)); //Formats string, adds 0x
                    }
                }
            }
        }
        private void Hex2Dec(object sender, EventArgs e)
        {
            if (hexcalcbox.Focused) //Prevents chaos via triggering both textchange events
            {
                string Hexycalc;
                if (hexcalcbox.Text.StartsWith("0x"))
                {
                    Hexycalc = hexcalcbox.Text.Substring(2);
                }
                else { Hexycalc = hexcalcbox.Text; }
                if (System.Text.RegularExpressions.Regex.IsMatch(Hexycalc, @"\A\b[0-9a-fA-F]+\b\Z")) //Prevents error via random nonsense @"^[A-Fa-f0-9]*$"
                {
                    try
                    {
                        decimalbox.Text = Convert.ToInt32(hexcalcbox.Text, 16).ToString(); //Basic Hex > Decimal Conversion
                    }
                    catch { }
                }
            }
        }
        #endregion

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Peek Poker - Open Source Memory Editor"));
            stringBuilder.AppendLine(string.Format("By"));
            stringBuilder.AppendLine(string.Format("Cybersam"));
            stringBuilder.AppendLine(string.Format("8Ball"));
            stringBuilder.AppendLine(string.Format("PureIso"));
            stringBuilder.AppendLine(string.Format("Special Thanks"));
            stringBuilder.AppendLine(string.Format("Mojobojo"));
            stringBuilder.AppendLine(string.Format("fairchild"));
            stringBuilder.AppendLine(string.Format("360Haven"));
            ShowMessageBox(stringBuilder.ToString(),string.Format("Peek Poker"),MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CurrentXpToolStripMenuItemClick(object sender, EventArgs e)
        {
            searchRangeBaseValueTypeCB.Text = string.Format("Hex");
            searchRangeEndTypeCB.Text = string.Format("Length");
            searchRangeValueTextBox.Text = string.Format("8211EF4C5C242888000000030000000000000000");
            startRangeAddressTextBox.Text = string.Format("0xDA1D0000");
            endRangeAddressTextBox.Text = string.Format("0xFFFF");
        }

        private void currentXpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            peekPokeAddressTextBox.Text = string.Format("0xc33BE970");
            peekLengthTextBox.Text = string.Format("4");
        }

        private void currentBloodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            peekPokeAddressTextBox.Text = string.Format("0xcF4D3130");
            peekLengthTextBox.Text = string.Format("4");
        }
    }
}
