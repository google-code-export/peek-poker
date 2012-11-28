using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace UFC3___Memory_Editor
{
    public partial class Ufc3Form : Form //Peek Poker required you to implement IPlugin
    {
        private RealTimeMemory _rtm;//private global variable
        private uint[] _pointerAddress;//some games require pointers
        const string Pointer = "82010480"; //pointer for this specific game

        public Ufc3Form()
        {
            InitializeComponent();
        }

        public Ufc3Form(string ipaddress)
        {
            InitializeComponent();
            ipAddressTextBox.Text = ipaddress;
        }

        private void PokeMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked)
                {
                    //Poke the specified offset with hex value - E1E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E2
                    //Remember offset means the start of a position
                    _rtm.Poke(_pointerAddress[0] + (uint)Pointer.Length, "E1E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E0E2");
                }
                else
                {
                    //ToByteArray & Hex are part of the peekpoker interface library
                    var stat = ToByteArray.HexToBytes(customAttrTextBox.Text);//convert string to hex in byte array
                    _rtm.Poke(_pointerAddress[0], Hex.ToHexString(stat));
                }

                //Write Cred
                //Convert credNumericUpDown values to int then to byte array
                var value = BitConverter.GetBytes(int.Parse(credNumericUpDown.Value.ToString(CultureInfo.InvariantCulture)));
                Array.Reverse(value);//reverse so it's big endian
                //Move the address forward by A8(base 16 aka hex) bytes
                //The Cred value is A8 bytes away from the pointer address #11#
                _rtm.Poke(_pointerAddress[0] + 0xA8, Hex.ToHexString(value));
                MessageBox.Show(this,string.Format("Successful Memory Edit."), string.Format("UFC 3"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //Notice the layout
                //this will fix the dialog box infront of the editor preventing you to click anything else
                MessageBox.Show(this,ex.Message, string.Format("UFC 3"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm = new RealTimeMemory(ipAddressTextBox.Text);//initialise with ipaddress
                if (_rtm.Connect())
                {
                    MessageBox.Show(this,string.Format("Connected"), string.Format("UFC 3"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pokeMemoryButton.Enabled = true;
                    panel1.Enabled = true;

                    //initialise the dumpOffset & dump length
                    //It will dump that area of memory to a temp file which will be used to find offset
                    _rtm.DumpOffset = 0xC7897000;
                    _rtm.DumpLength = 0xFFF;

                    _pointerAddress = _rtm.FindUIntOffset(Pointer);//find the pointer offset (The start of the pointer)
                    //Peek the pointer address + A8 (A8 refers to #11#)
                    credNumericUpDown.Value = uint.Parse(_rtm.Peek(_pointerAddress[0] + 0xA8, 4));
                }
                else
                {
                    MessageBox.Show(this,string.Format("Already Connected or No Connection"), string.Format("UFC 3"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,ex.Message);
            }
        }
    }
}
