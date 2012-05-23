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
        public RealTimeMemory.RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        private uint _searchRangeDumpLength;
        public List<string> _offsets;
        private BindingList<Types.SearchResults> _searchResult = new BindingList<Types.SearchResults>();
        private string _filepath2 = null; //Trainer Scanner
        #endregion

        public MainForm()
        {
            InitializeComponent();

            //Set comboboxes correctly - need to be here for some reason... or it won't work...
            searchRangeBaseValueTypeCB.SelectedIndex = 0;
            searchRangeEndTypeCB.SelectedIndex = 0;
            resultGrid.DataSource = _searchResult;
            combocodetype.SelectedIndex = 0;
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
        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Peek Poker - Open Source Memory Editor"));
            stringBuilder.AppendLine(string.Format("By"));
            stringBuilder.AppendLine(string.Format("Cybersam"));
            stringBuilder.AppendLine(string.Format("8Ball"));
            stringBuilder.AppendLine(string.Format("PureIso"));
            stringBuilder.AppendLine(string.Format("cornnatron"));
            stringBuilder.AppendLine(string.Format("Special Thanks"));
            stringBuilder.AppendLine(string.Format("Mojobojo"));
            stringBuilder.AppendLine(string.Format("fairchild"));
            stringBuilder.AppendLine(string.Format("360Haven"));
            ShowMessageBox(stringBuilder.ToString(), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void HowToUseToolStripMenuItemClick(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Select a game stores offsets / pointer information."));
            stringBuilder.AppendLine(string.Format("Select a game and naviagte to the options"));
            stringBuilder.AppendLine(string.Format("Options with [P] = search range pointer info"));
            stringBuilder.AppendLine(string.Format("Options with [A] = Address of the actual value"));
            stringBuilder.AppendLine(string.Format("NB: This is still work in progress."));
        }

        #region button clicks
        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                SetLogText("Connecting to: " + ipAddressTextBox.Text);
                _rtm = new RealTimeMemory.RealTimeMemory(ipAddressTextBox.Text, 0, 0);//initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {
                    SetLogText("Connecting to " + ipAddressTextBox.Text+" Failed.");
                    throw new Exception("Connection Failed!");
                }
                peeknpoke.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetLogText("Connected to " + ipAddressTextBox.Text);

                if (!File.Exists(_filepath)) File.Create(_filepath).Dispose(); //Create the file if it doesn't exist
                var objWriter = new StreamWriter(_filepath); //Writer Declaration
                objWriter.Write(ipAddressTextBox.Text); //Writes IP address to text file
                objWriter.Close(); //Close Writer
                connectButton.Text = String.Format("Re-Connect");
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
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
                SetLogText("Error: " + ex.Message);
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
                SetLogText("Error: " + ex.Message);
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //When you click on the www.360haven.com
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            SetLogText("URL connection to www.360haven.com");
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
        private void SetLogText(string value)
        {
            if(logTextBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetLogText(value)));
            else
            {
                var m = DateTime.Now.ToString("HH:mm:ss tt") + " " + value + Environment.NewLine;
                logTextBox.Text += m;
                logTextBox.Select(logTextBox.Text.Length, 0); // set the cursor to end of textbox
                logTextBox.ScrollToCaret();                     // scroll down to the cursor position
            }
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
                    var result = Int32.TryParse(decimalbox.Text, out number); //Stops things like a single "-" causing errors
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
            if (!hexcalcbox.Focused) return;
            var hexycalc = hexcalcbox.Text.StartsWith("0x") ? hexcalcbox.Text.Substring(2) : hexcalcbox.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(hexycalc, @"\A\b[0-9a-fA-F]+\b\Z")) //Prevents error via random nonsense @"^[A-Fa-f0-9]*$"
            {
                try
                {
                    decimalbox.Text = Convert.ToInt32(hexcalcbox.Text, 16).ToString(); //Basic Hex > Decimal Conversion
                }
                catch { }
            }
        }
        #endregion
       
        #region Games
        #region Select_Games
        //Resident Evil ORC - current xp
        private void CurrentXpToolStripMenuItemClick(object sender, EventArgs e)
        {
            SetLogText("#select game# Resident Evil ORC - current xp - selected");
            searchRangeBaseValueTypeCB.Text = string.Format("Hex");
            searchRangeEndTypeCB.Text = string.Format("Length");
            searchRangeValueTextBox.Text = string.Format("8211EF4C5C242888000000030000000000000000");
            startRangeAddressTextBox.Text = string.Format("0xDA1D0000");
            endRangeAddressTextBox.Text = string.Format("0xFFFF");
        }

        //Prototype 2 - current xp
        private void CurrentXpToolStripMenuItem1Click(object sender, EventArgs e)
        {
            SetLogText("#select game# Prototype 2 - current xp - selected");
            peekPokeAddressTextBox.Text = string.Format("0xc33BE970");
            peekLengthTextBox.Text = string.Format("4");
        }

        //BloodForge - current blood
        private void CurrentBloodToolStripMenuItemClick(object sender, EventArgs e)
        {
            SetLogText("#select game# BloodForge - current blood - selected");
            peekPokeAddressTextBox.Text = string.Format("0xcF4D3130");
            peekLengthTextBox.Text = string.Format("4");
        }

        //UFC 3 - attributes
        private void PAttributeToolStripMenuItemClick(object sender, EventArgs e)
        {
            SetLogText("#select game# UFC3 - attribute - selected");
            searchRangeBaseValueTypeCB.Text = string.Format("Hex");
            searchRangeEndTypeCB.Text = string.Format("Length");
            searchRangeValueTextBox.Text = string.Format("82010480");
            startRangeAddressTextBox.Text = string.Format("0xC7897000");
            endRangeAddressTextBox.Text = string.Format("0xFFF");
        }

        private void ResonanceOfFateMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            
            try
            {
                var oThread = new Thread(ExROF);
                oThread.Start(menu.Tag);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
#endregion
        #region Trainers
        #region Skyrim
//Skyrim TU#4/5
  // Inf Stamina
        private void Skyrim_infSprint(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Skyrim - TU#4/5 - Infinite Sprint - Sent");
            try
            {_rtm.WriteMemory(0x834F9890, "00000000"); 
             _rtm.WriteMemory(0x834F9650, "00000000");
             _rtm.WriteMemory(0x834FB234, "00000000");
             _rtm.WriteMemory(0x834FB24C, "00000000");
            }
            catch { SetLogText("Error! Could not poke code."); } 
        }
  // Inf Mana  
        private void Skyrim_infMagicka(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Skyrim - TU#4/5 - Infinite Stamina - Sent");
            try
            { _rtm.WriteMemory(0x834FB234, "00000000");}
            catch { SetLogText("Error! Could not poke code."); }             
        }
#endregion
        #region DarkSouls
//Dark Souls TU#0/1
  // Max Level
        private void DS0_MaxLevel(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Level - Sent");
            try
            {_rtm.WriteMemory(0xC95A2108, "000002C8");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Souls 
        private void DS0_MaxSouls(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Souls - Sent");
            try
            {_rtm.WriteMemory(0xC95A210C, "3B9AC9FF");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Humanity 
        private void DS0_Humanity(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Humanity - Sent");
            try
            {_rtm.WriteMemory(0xC95A20FC, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Vitality           
        private void DS0_Vitality(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Vitality - Sent");
            try
            {_rtm.WriteMemory(0xC95A20B8, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Attunement
        private void DS0_Attunement(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Attunement - Sent");
            try
            {_rtm.WriteMemory(0xC95A20C0, "00000063");} 
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Intelligence
        private void DS0_Intelligence(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Intelligence- Sent");
            try
            {_rtm.WriteMemory(0xC95A20E0, "00000063");} 
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Resistance 
        private void DS0_Resistance(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Resistance - Sent");
            try
            {_rtm.WriteMemory(0xC95A2100, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Dexterity
        private void DS0_Dexterity(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Dexterity - Sent");
            try
            {_rtm.WriteMemory(0xC95A20D8, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Faith  
        private void DS0_Faith(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Faith - Sent");
            try
            {_rtm.WriteMemory(0xC95A20E8, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Stamina 
        private void DS0_Stamina(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Stamina  - Sent");
            try
            {_rtm.WriteMemory(0xC95A20B0, "000000A0");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Stamina 1 Million 
        private void DS0_MillionStamina(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - 1 Million Stamina  - Sent");
            try
            {_rtm.WriteMemory(0xC95A20B0, "3B9ACA00");}
            catch { SetLogText("Error! Could not poke code.");}
        }
  // Max Strength  
        private void DS0_Strength(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Strength  - Sent");
            try
            {_rtm.WriteMemory(0xC95A20D0, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Max Endurance 
        private void DS0_Endurance(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - Max Endurance  - Sent");
            try
            {_rtm.WriteMemory(0xC95A20C8, "00000063");}
            catch { SetLogText("Error! Could not poke code."); }
        }
  // Endurance 1 Million
        private void DS0_MillionEndurance(object sender, EventArgs e)
        {
            SetLogText("#Trainers# Dark Souls - TU#0/1 - 1 Million Endurance  - Sent");
            try
            {
            _rtm.WriteMemory(0xC95A20C8, "3B9ACA00");
            }
            catch { SetLogText("Error! Could not poke code."); }
        }
       private void DS0_All(object sender, EventArgs e)
        {
                DS0_MaxLevel(DS0MaxLevel, System.EventArgs.Empty);
                DS0_MaxSouls(DS0MaxSouls, System.EventArgs.Empty);
                DS0_Vitality(DS0MaxVit, System.EventArgs.Empty);
                DS0_Endurance(DS0MaxEnd, System.EventArgs.Empty);
                DS0_Attunement(DS0MaxAtt, System.EventArgs.Empty);
                DS0_Strength(DS0MaxStr, System.EventArgs.Empty);
                DS0_Dexterity(DS0MaxDex, System.EventArgs.Empty);
                DS0_Resistance(DS0MaxRes, System.EventArgs.Empty);
                DS0_Intelligence(DS0MaxInt, System.EventArgs.Empty); 
                DS0_Faith(DS0MaxFaith, System.EventArgs.Empty);
                DS0_Humanity(DS0MaxHum, System.EventArgs.Empty);
                DS0_Stamina(DS0MaxStam, System.EventArgs.Empty);
         }
        #endregion
        #region Resonce Of Fate
       private void ExROF(object sets)
       {
           List<string> _poke;
           #region List Values
           List<string> WhiteHex = new List<string>();
           List<string> ColorHex = new List<string>();
           List<string> HexStations = new List<string>();
           List<string> WeaponSet1 = new List<string>();
           List<string> WeaponSet2 = new List<string>();
           List<string> WeaponDebugSet = new List<string>();
           List<string> ItemSpecialSet1 = new List<string>();
           List<string> ItemSpecialSet2 = new List<string>();

           #region White Hex
           WhiteHex.Add("0001010000000001000003E7000003E700000000");
           WhiteHex.Add("0002010000000001000003E7000003E700000000");
           WhiteHex.Add("0003010000000001000003E7000003E700000000");
           WhiteHex.Add("0004010000000001000003E7000003E700000000");
           WhiteHex.Add("0005010000000001000003E7000003E700000000");
           WhiteHex.Add("0006010000000001000003E7000003E700000000");
           WhiteHex.Add("0007010000000001000003E7000003E700000000");
           WhiteHex.Add("0008010000000001000003E7000003E700000000");
           WhiteHex.Add("0009010000000001000003E7000003E700000000");
           WhiteHex.Add("000A010000000001000003E7000003E700000000");
           #endregion
           #region Colored Hex
           ColorHex.Add("000B010000000001000003E7000003E700000000");
           ColorHex.Add("0010010000000001000003E7000003E700000000");
           ColorHex.Add("0015010000000001000003E7000003E700000000");
           ColorHex.Add("001A010000000001000003E7000003E700000000");
           ColorHex.Add("001F010000000001000003E7000003E700000000");
           ColorHex.Add("0024010000000001000003E7000003E700000000");
           ColorHex.Add("0029010000000001000003E7000003E700000000");
           ColorHex.Add("002E010000000001000003E7000003E700000000");
           ColorHex.Add("0033010000000001000003E7000003E700000000");
           ColorHex.Add("0038010000000001000003E7000003E700000000");
           ColorHex.Add("003D010000000001000003E7000003E700000000");
           ColorHex.Add("003E010000000001000003E7000003E700000000");
           #endregion
           #region Hex Stations
           HexStations.Add("0042010000000001000003E7000003E700000000");
           HexStations.Add("0043010000000001000003E7000003E700000000");
           HexStations.Add("0044010000000001000003E7000003E700000000");
           HexStations.Add("0045010000000001000003E7000003E700000000");
           HexStations.Add("0046010000000001000003E7000003E700000000");
           HexStations.Add("0047010000000001000003E7000003E700000000");
           HexStations.Add("0048010000000001000003E7000003E700000000");
           HexStations.Add("0049010000000001000003E7000003E700000000");
           HexStations.Add("004A010000000001000003E7000003E700000000");
           HexStations.Add("004B010000000001000003E7000003E700000000");
           HexStations.Add("004C010000000001000003E7000003E700000000");
           #endregion
           #region Weapon Set 1
           WeaponSet1.Add("03F0010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F1010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F2010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F3010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F4010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F5010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F6010000000001000003E7000003E700000000");
           WeaponSet1.Add("03F7010000000001000003E7000003E700000000");
           #endregion
           #region Weapon Set 2
           WeaponSet2.Add("03FE010000000001000003E7000003E700000000");
           WeaponSet2.Add("0400010000000001000003E7000003E700000000");
           WeaponSet2.Add("0401010000000001000003E7000003E700000000");
           WeaponSet2.Add("0402010000000001000003E7000003E700000000");
           WeaponSet2.Add("0403010000000001000003E7000003E700000000");
           WeaponSet2.Add("0404010000000001000003E7000003E700000000");
           WeaponSet2.Add("0405010000000001000003E7000003E700000000");
           WeaponSet2.Add("0406010000000001000003E7000003E700000000");
           WeaponSet2.Add("0407010000000001000003E7000003E700000000");
           WeaponSet2.Add("0408010000000001000003E7000003E700000000");
           WeaponSet2.Add("0409010000000001000003E7000003E700000000");
           #endregion
           #region Weapon Debug Set
           WeaponDebugSet.Add("03F8010000000001000003E7000003E700000000");
           WeaponDebugSet.Add("03F9010000000001000003E7000003E700000000");
           WeaponDebugSet.Add("03FA010000000001000003E7000003E700000000");
           #endregion
           #region Item Special Set 1
           ItemSpecialSet1.Add("0528010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("0471010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("046B010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("046A010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F0010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F1010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F2010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F8010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F9010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07FA010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("0464010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07FB010000000001000003E7000003E700000000");
           ItemSpecialSet1.Add("07F3010000000001000003E7000003E700000000");
           #endregion
           #region Item Special Set 2
           ItemSpecialSet2.Add("0566010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("0567010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("0562010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("07FC010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("07FD010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("03F4010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("0561010000000001000003E7000003E700000000");
           ItemSpecialSet2.Add("04E0010000000001000003E7000003E700000000");
           #endregion
           #endregion
           #region Switch
           switch ((string)sets)
           {
               case "WhiteHex":
                   _poke = WhiteHex;
                   break;
               case "ColorHex":
                   _poke = ColorHex;
                   break;
               case "HexStations":
                   _poke = HexStations;
                   break;
               case "WeaponSet1":
                   _poke = WeaponSet1;
                   break;
               case "WeaponSet2":
                   _poke = WeaponSet2;
                   break;
               case "WeaponDebugSet":
                   _poke = WeaponDebugSet;
                   break;
               case "ItemSpecialSet1":
                   _poke = ItemSpecialSet1;
                   break;
               case "ItemSpecialSet2":
                   _poke = ItemSpecialSet2;
                   break;
                   
               default:
                   _poke = WhiteHex;
                   break;
           }
           #endregion

           #region Dump/Search and Poke
           try
           {
               CheckForIllegalCrossThreadCalls = false; //line 476 grid cross thread error
               _rtm.DumpOffset = 0xCD500000;
               _rtm.DumpLength = 0x500000;

               SetLogText("#Trainers# Resonance Of Fate Dumping & Searching ...");
               //The ExFindHexOffset function is a Experimental search function
               var results = _rtm.ExFindHexOffset("04B10100000003E8");
               //Reset the progressbar...
               UpdateProgressbar(0, 100, 0);

               if (results.Count < 1)
               {
                   ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return; //We don't want it to continue
               }

               UpdateProgressbar(0, 100, 0, "Poking");
               SetLogText("#Trainers# Resonance Of Fate Poking ...");

               for (int i = 0; i < _poke.Count; i++)
               {
                   uint offsets = Functions.BytesToUInt32(Functions.HexToBytes(results[0].Offset)) + (uint)(i * 0x14);

                   SetLogText(_poke[i]);
                   _rtm.Poke(offsets, _poke[i]);
               }
               SetLogText("#Trainers# Resonance Of Fate Done... Buy the items");

               UpdateProgressbar(0, 100, 0);
           }
           catch (Exception e)
           {
               ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
           finally
           {
               Thread.CurrentThread.Abort();
           }
           #endregion
       }
        #endregion

        #region Trainer-Utility
       private void scanTrainerCodes(object sender, EventArgs e) //Opens a trainers txt file to read its codes
       {
           SetLogText("#Trainers# Activating Trainer Scanner");
           try
           {
               OpenFileDialog Open = new OpenFileDialog();
               Open.Filter = "GAME_ID.txt|*.txt";
               Open.Title = "Open Trainer Code File";
               Open.ShowDialog();
               _filepath2 = Open.FileName;
               if (!File.Exists(_filepath2)) return;
               Application.DoEvents();
               ReadFile(_filepath2);
            }
           catch (Exception ex){MessageBox.Show(ex.Message);}
       }
       public void ReadFile(string _filepath2)
       {   try
       {
           TrainerTextBox.Text = File.ReadAllText(_filepath2);
               SetLogText("#Trainers# Opening Trainer, Game ID:" + _filepath2.Substring(_filepath2.Length - 12, 8)); //GAMEID Extraction from filepath
          
       }
           catch (Exception ex){MessageBox.Show(ex.Message);}
        }
     
        //Save TrainerTextBox contents to file of users choice.
       private void createtrainerbutton_Click(object sender, EventArgs e) 

       {
           SetLogText("#Trainers# Saving Trainer");
           SaveFileDialog Save = new SaveFileDialog();
           Save.Filter = "*.txt|*.txt";
           Save.Title = "Save Trainer Code File";
           Save.ShowDialog();
           if (Save.FileName != "")
           {
               System.IO.StreamWriter file = new System.IO.StreamWriter(Save.FileName);
               file.Write(TrainerTextBox.Text);
               file.Close();
               SetLogText("#Trainers# Saved Trainer to " + Save.FileName);
       }}

     //Appends a blank line and regains focus to trainerbox
       private void newcodebutton_Click(object sender, EventArgs e)
       {   
           TrainerTextBox.AppendText(Environment.NewLine);
           TrainerTextBox.AppendText((Environment.NewLine) + "#" + codenamebox.Text);
           TrainerTextBox.AppendText((Environment.NewLine) + combocodetype.SelectedIndex.ToString("X") + " " + codeaddressbox.Text + " " + codevaluebox.Text);
           TrainerTextBox.Focus();
       }
           
     //Appends code to list, includes codename, type, address and value
       private void addcodebutton_Click(object sender, EventArgs e) //Appends code to TrainerTextBox.
       {
           TrainerTextBox.AppendText((Environment.NewLine) + combocodetype.SelectedIndex.ToString("X") + " " + codeaddressbox.Text + " " + codevaluebox.Text);
           TrainerTextBox.Focus();
       }
        #endregion
        #endregion
        #endregion

    }
}
