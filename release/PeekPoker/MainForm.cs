using System;
using System.IO;
using System.Drawing;
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
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

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

        private void Form1Load(Object sender, EventArgs e)
        {
            //This is for handling automatic loading of the IP address and txt file creation. -8Ball
            if (!File.Exists(_filepath)) File.Create(_filepath).Dispose();
            else ipAddressTextBox.Text = File.ReadAllText(_filepath);
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
        #endregion
    }
}
