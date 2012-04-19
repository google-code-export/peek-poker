using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PeekPoker
{
    #region Delegates
    public delegate void UpdateProgressBarHandler(int min, int max, int value, string text);
    #endregion

    public partial class MainForm : Form
    {
        #region global varibales
        private RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        private uint _searchRangeDumpLength;
        private List<string> _offsets;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            //Set comboboxes correctly - need to be here for some reason... or it won't work...
            SearchRangeBaseValueTypeCB.SelectedIndex = 0;
            SearchRangeEndTypeCB.SelectedIndex = 0;
        }
        private void Form1Load(Object sender, EventArgs e)
        {
            //This is for handling automatic loading of the IP address and txt file creation. -8Ball
            if (!File.Exists(_filepath)) File.Create(_filepath).Dispose();
            else ipAddressTextBox.Text = File.ReadAllText(_filepath);

            //Set correct max. min values for the numeric fields
            changeNumericMaxMin();
        }

        #region button clicks
        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            _rtm = new RealTimeMemory(ipAddressTextBox.Text, 0, 0);//initialize real time memory
            _rtm.ReportProgress += new UpdateProgressBarHandler(UpdateProgressbar);
            try
            {
                if (!_rtm.Connect()) throw new Exception("Connection Failed!");
                peeknpoke.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            System.Diagnostics.Process.Start("www.360haven.com");
        }

        //When you click on the new button
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
        }

        //When you click on the search range button - Search Range Tab
        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (SearchRangeEndTypeCB.SelectedIndex == 1)
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
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Experimental search Button
        private void EXSearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (SearchRangeEndTypeCB.SelectedIndex == 1)
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

            peekPokeAddressTextBox.Text = "0x" + searchRangeResultListView.FocusedItem.SubItems[1].Text;
        }
        // Refresh results
        private void ResultRefresh_Click(object sender, EventArgs e)
        {
            if (searchRangeResultListView.Items.Count > 0)
            {
                var oThread = new Thread(RefreshSearchRangeListViewList);
                oThread.Start();
            }
            else
            {
                ShowMessageBox("Can not refresh! \r\n Resultlist empty!!", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            changeNumericMaxMin();
            ChangeNumericValue();
        }
        private void NumericInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }
        #endregion
        #region Search Tab Events
        private void searchRangeValueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && searchRangeValueTextBox.Focused)
            {
                try
                {
                    if (SearchRangeEndTypeCB.SelectedIndex == 1)
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
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
                searchRangeButton.Focus();
            }
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
        //Search the memory for the specified value
        private void SearchRange()
        {
            try
            {
                //You can always use: 
                //CheckForIllegalCrossThreadCalls = false; when dealing with threads
                _rtm.DumpOffset = GetStartRangeAddressTextBoxText();
                _rtm.DumpLength = _searchRangeDumpLength;

                SearchRangeListViewListClean();//Clean list view

                //The FindHexOffset function is slow in searching - I might use Mojo's algorithm
                _offsets = _rtm.FindHexOffset(GetSearchRangeValueTextBoxText());//pointer
                if (_offsets.Count < 1)
                {
                    //Reset the progressbar...
                    UpdateProgressbar(0, 100, 0, "Idle");

                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0, "Idle");

                var i = 0; //The index number
                foreach (var offset in _offsets)
                {
                    //Collection initializer or use array either will do
                    //put the numnber @index 0
                    //put the hex offset @index 1
                    var newOffset = new[] { i.ToString(), offset };
                    //send the newOffset details to safe thread which adds to listview
                    SearchRangeListViewListUpdate(newOffset);
                    i++;
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Searches the memory for the specified value (Experimental)
        private void ExSearchRange()
        {
            try
            {
                //You can always use: 
                //CheckForIllegalCrossThreadCalls = false; when dealing with threads
                _rtm.DumpOffset = GetStartRangeAddressTextBoxText();
                _rtm.DumpLength = _searchRangeDumpLength;

                SearchRangeListViewListClean();//Clean list view

                //The FindHexOffset function is slow in searching - I might use Mojo's algorithm
                List<Types.SearchResults>  _results = _rtm.ExFindHexOffset(GetSearchRangeValueTextBoxText());//pointer

                if (_results.Count < 1)
                {
                    //Reset the progressbar...
                    UpdateProgressbar(0, 100, 0, "Idle");

                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0, "Idle");

                var i = 0; //The index number
                foreach (Types.SearchResults result in _results)
                {
                    //Collection initializer or use array either will do
                    //put the numnber @index 0
                    //put the hex offset @index 1
                    var newOffset = new[] { i.ToString(), result.Offset, result.Value};
                    //send the newOffset details to safe thread which adds to listview
                    SearchRangeListViewListUpdate(newOffset);
                    i++;
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //When you select an offset on the hexbox
        private void ChangeNumericValue()
        {
            if (hexBox.ByteProvider != null)
            {
                List<byte> buffer = hexBox.ByteProvider.Bytes;
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
                byte[] _prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
                int _address = Functions.BytesToInt32(_prev);
                SelAddress.Text = string.Format("0x" + (_address + (int)hexBox.SelectionStart).ToString("X8"));
            }
        }
        private void ChangedNumericValue(object sender)
        {
            if (hexBox.SelectionStart < hexBox.ByteProvider.Bytes.Count)
            {
                NumericUpDown numeric = (NumericUpDown)sender;
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
                                System.Convert.ToByte((byte)numeric.Value));
                        }
                        break;
                    case "NumericInt16":
                        for (int i = 0; i < 2; i++)
                        {
                            if (isSigned.Checked)
                            {
                                hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                    Functions.Int16ToBytes((short)numeric.Value)[i]);
                            }
                            else
                            {
                                hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                    Functions.UInt16ToBytes((ushort)numeric.Value)[i]);
                            }
                        }
                        break;
                    case "NumericInt32":
                        for (int i = 0; i < 4; i++)
                        {
                            if (isSigned.Checked)
                            {
                                hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                    Functions.Int32ToBytes((int)numeric.Value)[i]);
                            }
                            else
                            {
                                hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                    Functions.UInt32ToBytes((uint)numeric.Value)[i]);
                            }
                        }
                        break;
                    default:
                        break;
                }
                hexBox.Refresh();
            }
        }
        private void changeNumericMaxMin()
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
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }
        private void SearchRangeListViewListUpdate(string[] data)
        {
            //IList or represents a collection of objects(String)
            if (searchRangeResultListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                searchRangeResultListView.Invoke((MethodInvoker)(() => SearchRangeListViewListUpdate(data)));
            else
            {
                searchRangeResultListView.Items.Add(new ListViewItem(data));
            }
        }
        //Refresh the values of Search Results
        private void RefreshSearchRangeListViewList()
        {
            //IList or represents a collection of objects(String)
            if (searchRangeResultListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                searchRangeResultListView.Invoke((MethodInvoker)(() => RefreshSearchRangeListViewList()));
            else
            {
                int x = 0;
                foreach (ListViewItem _item in searchRangeResultListView.Items)
                {
                    peekPokeAddressTextBox.Text = "0x" + _item.SubItems[1].Text;
                    string _length = (searchRangeValueTextBox.Text.Length / 2).ToString("X");
                    string retvalue = _rtm.Peek("0x" + _item.SubItems[1].Text, _length, "0x" + _item.SubItems[1].Text, _length);
                    _item.SubItems[2].Text = retvalue;

                    UpdateProgressbar(0, searchRangeResultListView.Items.Count, x);
                    x++;
                }
                searchRangeResultListView.Refresh();
                UpdateProgressbar(0, 100, 0, "idle");
            }
        }
        private void SearchRangeListViewListClean()
        {
            if (searchRangeResultListView.InvokeRequired)
                searchRangeResultListView.Invoke((MethodInvoker)(SearchRangeListViewListClean));
            else
                searchRangeResultListView.Items.Clear();
        }

        //Progressbar Delegates
        internal void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker)(() => UpdateProgressbar(min, max, value, text)));
            }
            else
            {
                StatusProgressBar.ProgressBar.Maximum = max;
                StatusProgressBar.ProgressBar.Minimum = min;
                StatusProgressBar.ProgressBar.Value = value;
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
                if (System.Text.RegularExpressions.Regex.IsMatch(hexcalcbox.Text, @"^[A-Fa-f0-9]*$")) //Prevents error via random nonsense
                {
                    decimalbox.Text = Convert.ToInt32(hexcalcbox.Text, 16).ToString(); //Basic Hex > Decimal Conversion
                }
            }
        }
        #endregion
    }
}
