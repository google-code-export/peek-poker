using System;
using System.Drawing;
using System.Windows.Forms;
using ISOLib.XBDMPackage;

namespace PeekPoker
{
    public partial class MainForm : Form
    {
        // Testing Submission with new login
        //Global Variables
        private RealTimeMemory _rtm;
        private bool _trigger;
        private String _lastAddress;
        private AutoCompleteStringCollection _data = new AutoCompleteStringCollection();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            _rtm = new RealTimeMemory(ipAddressTextBox.Text,0,0);
            try
            {
                if (!_rtm.Connect())throw new Exception("Connection Failed!");
                _lastAddress = null;
                _trigger = false;
                panel1.Enabled = true;
                panel2.Enabled = true;
                statusStripLabel.Text = String.Format("Connected");
                MessageBox.Show(this, String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete();
            try
            {
                if (string.IsNullOrEmpty(peekLengthTextBox.Text))
                    throw new Exception("Invalide peek length!");
                //==============================================
                if (_trigger && _lastAddress == peekAddressTextBox.Text)
                {
                    string retValue = _rtm.Peek(peekAddressTextBox.Text, peekLengthTextBox.Text, peekAddressTextBox.Text, peekLengthTextBox.Text);
                    char[] previousValue = peekResultTextBox.Text.ToCharArray();
                    peekResultTextBox.Clear();
                    int i = 0;
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
                    string retValue = _rtm.Peek(peekAddressTextBox.Text, peekLengthTextBox.Text, peekAddressTextBox.Text, peekLengthTextBox.Text);
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
            AutoComplete();
            try
            {
                _rtm.DumpOffset = pokeAddressTextBox.Text;
                _rtm.DumpLength = (uint)pokeValueTextBox.Text.Length/2;
                _rtm.Poke(pokeAddressTextBox.Text, pokeValueTextBox.Text);
                MessageBox.Show(this, String.Format("Done!"), String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

        private void NewPeek()
        {
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
            var trigger = false;
            var trigger2 = false;
            peekAddressTextBox.AutoCompleteCustomSource = _data;
            for (var index = 0; index < _data.Count; index++)
            {
                var value = _data[index];
                if (ReferenceEquals(value, peekAddressTextBox.Text)) trigger = true;
                if (ReferenceEquals(value, pokeAddressTextBox.Text)) trigger2 = true;
            }
            if (!trigger) _data.Add(peekAddressTextBox.Text);
            if (!trigger2) _data.Add(pokeAddressTextBox.Text);
        }
    }
}
