using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PeekPoker
{
    public partial class MainForm : Form
    {
        #region global varibales
        private RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private bool _trigger;//Traggers(True) if using the same memory address
        private String _lastAddress;//The last address the user ented
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        private uint _searchRangeDumpLength;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1Load(Object sender, EventArgs e)
        {
            //This is for handling automatic loading of the IP address and txt file creation. -8Ball
            if (!File.Exists(_filepath)) File.Create(_filepath).Dispose();
            else ipAddressTextBox.Text = File.ReadAllText(_filepath);
        }

        #region button clicks
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            _rtm = new RealTimeMemory(ipAddressTextBox.Text,0,0);//initialize real time memory
            try
            {
                if (!_rtm.Connect())throw new Exception("Connection Failed!");
                _lastAddress = null;
                _trigger = false;
                panel1.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete();//run function
            try
            {
                if (string.IsNullOrEmpty(peekLengthTextBox.Text))
                    throw new Exception("Invalide peek length!");
                byte[] retValue = StringToByteArray(_rtm.Peek(PeekPokeAddressTextBox.Text, peekLengthTextBox.Text, PeekPokeAddressTextBox.Text, peekLengthTextBox.Text));
                Be.Windows.Forms.DynamicByteProvider _buffer = new Be.Windows.Forms.DynamicByteProvider(retValue);
                _buffer.IsWriteByte = true;
                hexBox.ByteProvider = _buffer;
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

        private void PokeButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            try
            {
                _rtm.DumpOffset = Convert(PeekPokeAddressTextBox.Text);//Set the dump offset
                _rtm.DumpLength = (uint)hexBox.ByteProvider.Length / 2;//The length of data to dump

                Be.Windows.Forms.DynamicByteProvider _buffer = (Be.Windows.Forms.DynamicByteProvider)hexBox.ByteProvider;
                
                Console.WriteLine(ByteArrayToString(_buffer.Bytes.ToArray()));
                _rtm.Poke(PeekPokeAddressTextBox.Text, ByteArrayToString(_buffer.Bytes.ToArray()));
                MessageBox.Show(this, String.Format("Done!"), String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.360haven.com");
        }

        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
        }

        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            //if (peekResultTextBox.Text.Equals("")) // Check if you have peeked code yet.
            //{ ShowMessageBox("Please peek the memory first.", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error); }
            //else //If you have peeked it continues
            //{
                try
                {
                    _searchRangeDumpLength = (Convert(endRangeAddressTextBox.Text) - Convert(startRangeAddressTextBox.Text));
                    dumpLengthTextBoxReadOnly.Text = _searchRangeDumpLength.ToString();
                    var oThread = new Thread(SearchRange);
                    oThread.Start();
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
        }
        
        #endregion

        #region functions
        private void NewPeek()
        {
            //Clean up
            _lastAddress = null;
            _trigger = false;
            PeekPokeAddressTextBox.Clear();
            peekLengthTextBox.Clear();
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }
        private void AutoComplete()
        {
            PeekPokeAddressTextBox.AutoCompleteCustomSource = _data;//put the auto complete data into the textbox
            var count = _data.Count;
            for (var index = 0; index < count; index++)
            {
                var value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, PeekPokeAddressTextBox.Text))
                    _data.Add(PeekPokeAddressTextBox.Text);
            }
        }
        private void SearchRange()
        {
            try
            {
                //You can always use: 
                //CheckForIllegalCrossThreadCalls = false; when dealing with threads
                _rtm.DumpOffset = GetStartRangeAddressTextBoxText();
                _rtm.DumpLength = _searchRangeDumpLength;

                //The FindHexOffset function is slow in searching - I might use Mojo's algorithm
                var offsets = _rtm.FindHexOffset(GetSearchRangeValueTextBoxText());//pointer
                if(offsets == null)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }
                SearchRangeListViewListClean();//Clean list view
                var i = 0; //The index number
                foreach (var offset in offsets)
                {
                    //Collection initializer or use array either will do
                    //put the numnber @index 0
                    //put the hex offset @index 1
                    var newOffset = new[]{i.ToString(), offset};
                    //send the newOffset details to safe thread which adds to listview
                    SearchRangeListViewListUpdate(newOffset);
                    i++;
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"),MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private uint Convert(string value)
        {
            //using Ternary operator
            return value.Contains("0x") ? 
                System.Convert.ToUInt32(value.Substring(2), 16) : System.Convert.ToUInt32(value);
        }
        private string ByteArrayToString(byte[] bytes)
        {
            string text = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                text += String.Format("{0,0:X2}", bytes[i]);
            }

            return text;
        }
        public byte[] StringToByteArray(string text)
        {
            byte[] bytes = new byte[text.Length / 2];

            for (int i = 0; i < text.Length; i += 2)
            {
                bytes[i / 2] = byte.Parse(text[i].ToString() + text[i + 1].ToString(),
                    System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
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
            if(searchRangeValueTextBox.InvokeRequired)searchRangeValueTextBox.Invoke((MethodInvoker)
                delegate{returnVal = GetSearchRangeValueTextBoxText();});
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
                return Convert(startRangeAddressTextBox.Text);
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
        private void SearchRangeListViewListClean()
        {
            if (searchRangeResultListView.InvokeRequired)
                searchRangeResultListView.Invoke((MethodInvoker)(SearchRangeListViewListClean));
            else
                searchRangeResultListView.Items.Clear();
        }
        #endregion

        #region Addressbox Autocorrection
        // These will automatically add "0x" to an offset if it hasn't been added already - 8Ball
        private void FixTheAddresses(object sender, EventArgs e)
        {
            if (!PeekPokeAddressTextBox.Text.StartsWith("0x")) //Peek Address Box, Formatting Check.
            {
                if (!PeekPokeAddressTextBox.Text.Equals("")) //Empty Check
                    PeekPokeAddressTextBox.Text = ("0x" + PeekPokeAddressTextBox.Text); //Formatting
            }
            if (peekLengthTextBox.Text.StartsWith("0x")) // Checks if peek length is hex value or not based on 0x
            { //This could probably do with some cleanup -8Ball
                string Result;
                UInt32 Result2;
                Result = (peekLengthTextBox.Text.ToUpper().Substring(2));
                Result2 = UInt32.Parse(Result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = Result2.ToString();
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(peekLengthTextBox.Text.ToUpper(), "^[A-Z]$")) //Checks if hex, based on uppercase alphabet presence.
            {
                string Result;
                UInt32 Result2;
                Result = (peekLengthTextBox.Text.ToUpper());
                Result2 = UInt32.Parse(Result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = Result2.ToString();
            }
            if (!startRangeAddressTextBox.Text.StartsWith("0x"))
            {
                if (!startRangeAddressTextBox.Text.Equals(""))
                    startRangeAddressTextBox.Text = ("0x" + startRangeAddressTextBox.Text);
            }
            if (!endRangeAddressTextBox.Text.StartsWith("0x"))
            {
                if (!endRangeAddressTextBox.Text.Equals(""))
                    endRangeAddressTextBox.Text = ("0x" + endRangeAddressTextBox.Text);
            }
        }
        #endregion 
    }
}
