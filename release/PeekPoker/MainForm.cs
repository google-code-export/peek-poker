using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ISOLib.XBDMPackage;

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
                panel2.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Information);
                var objWriter = new StreamWriter(_filepath); //Writer Declaration
                objWriter.Write(ipAddressTextBox.Text); //Writes IP address to text file
                objWriter.Close(); //Close Writer
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
                //Check if you are peeking the same offset so we can identify changes
                if (_trigger && _lastAddress == peekAddressTextBox.Text)
                {
                    var retValue = _rtm.Peek(peekAddressTextBox.Text, peekLengthTextBox.Text, peekAddressTextBox.Text, peekLengthTextBox.Text);
                    var previousValue = peekResultTextBox.Text.ToCharArray();
                    peekResultTextBox.Clear();
                    var i = 0;
                    foreach (var character in retValue)
                    {
                        if (character == previousValue[i])
                        {
                            peekResultTextBox.SelectionColor = Color.Black;
                            peekResultTextBox.SelectedText = character.ToString();
                        }
                        else
                        {
                            peekResultTextBox.SelectionColor = Color.Red;
                            peekResultTextBox.SelectedText = character.ToString();
                        }
                        i++;
                    }
                }
                else
                {
                    peekResultTextBox.Clear();
                    var retValue = _rtm.Peek(peekAddressTextBox.Text, peekLengthTextBox.Text, peekAddressTextBox.Text, peekLengthTextBox.Text);
                    peekResultTextBox.Text = retValue;

                    _lastAddress = peekAddressTextBox.Text;
                    _trigger = true;
                }
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
                _rtm.DumpOffset = pokeAddressTextBox.Text;//Set the dump offset
                _rtm.DumpLength = (uint)pokeValueTextBox.Text.Length/2;//The length of data to dump
                _rtm.Poke(pokeAddressTextBox.Text, pokeValueTextBox.Text);
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
            try
            {
                _searchRangeDumpLength = (Convert(startRangeAddressTextBox.Text) - Convert(endRangeAddressTextBox.Text));
                var oThread = new Thread(SearchRange);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region functions
        private void NewPeek()
        {
            //Clean up
            _lastAddress = null;
            _trigger = false;
            peekAddressTextBox.Clear();
            peekLengthTextBox.Clear();
            peekResultTextBox.Clear();
            pokeAddressTextBox.Clear();
            pokeValueTextBox.Clear();
        }
        private void AutoComplete()
        {
            peekAddressTextBox.AutoCompleteCustomSource = _data;//put the auto complete data into the textbox
            var count = _data.Count;
            for (var index = 0; index < count; index++)
            {
                var value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, peekAddressTextBox.Text))
                    _data.Add(peekAddressTextBox.Text);
                if (!ReferenceEquals(value, pokeAddressTextBox.Text))
                    _data.Add(pokeAddressTextBox.Text);
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
                var offsets = _rtm.FindHexOffset(GetSearchRangeValueTextBoxText());
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
        private String GetStartRangeAddressTextBoxText()
        {
            //recursion
            var returnVal = "";
            if (startRangeAddressTextBox.InvokeRequired) startRangeAddressTextBox.Invoke((MethodInvoker)
                  delegate { returnVal = GetStartRangeAddressTextBoxText(); });
            else
                return startRangeAddressTextBox.Text;
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
                searchRangeResultListView.Clear();
        }
        #endregion
    }
}
