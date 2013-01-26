using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker
{
    
    public partial class PeekNPoke : Form
    {
        #region variables
        public event ShowMessageBoxHandler ShowMessageBox;
        public event EnableControlHandler EnableControl;
        public event GetTextBoxTextHandler GetTextBoxText;
        public event SetTextBoxTextDelegateHandler SetTextBoxText;
        public event UpdateProgressBarHandler UpdateProgressbar;

        private readonly RealTimeMemory _rtm;
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        #endregion

        public PeekNPoke(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
        }

        #region Handlers
        private void FreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm.StopCommand();
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
        }
        private void UnfreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _rtm.StartCommand();
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
        }
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
        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!peekPokeAddressTextBox.Text.StartsWith("0x"))
                {
                    if (!peekPokeAddressTextBox.Text.Equals(""))
                        peekPokeAddressTextBox.Text = "0x"+uint.Parse(peekPokeAddressTextBox.Text).ToString("X");
                }

                if (peekLengthTextBox.Text.StartsWith("0x")) return;
                if (!peekLengthTextBox.Text.Equals(""))
                    peekLengthTextBox.Text = "0x" + uint.Parse(peekLengthTextBox.Text).ToString("X");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message,"PeekNPoke",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete();//run function
            ThreadPool.QueueUserWorkItem(Peek);
        }
        private void PokeButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            ThreadPool.QueueUserWorkItem(Poke);
        }
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
        }
        private void NewPeek()
        {
            //Clean up
            peekPokeAddressTextBox.Text = "0xC0000000";
            peekLengthTextBox.Text = "0xFF";
            SelAddress.Clear();
            peekPokeFeedBackTextBox.Clear();
            NumericInt8.Value = 0;
            NumericInt16.Value = 0;
            NumericInt32.Value = 0;
            NumericFloatTextBox.Text = "0";
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }
        private void PeekNPokeLoad(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
        }
        private void HexBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.hexBox.CopyHex();
                e.SuppressKeyPress = true;
            }
        }
        private void HexBoxMouseUp(object sender, MouseEventArgs e)
        {
            ChangeNumericValue();//When you select an offset on the hexbox

            if(hexBox.ByteProvider == null)return;
            var prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format("0x" + (address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        #endregion

        #region Functions
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
                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                    ? Functions.BytesToSingle(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
                NumericFloatTextBox.Text = f.ToString();
            }
            else
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0 ?
                    buffer[(int)hexBox.SelectionStart] : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1 ?
                    Functions.BytesToUInt16(buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray()) : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3 ?
                    Functions.BytesToUInt32(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
                
                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                    ? Functions.BytesToSingle(buffer.GetRange((int) hexBox.SelectionStart, 4).ToArray()) : 0;
                NumericFloatTextBox.Text = f.ToString();
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
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                    ? Functions.Int16ToBytes((short)numeric.Value)[i]
                                                                                    : Functions.UInt16ToBytes((ushort)numeric.Value)[i]);
                    }
                    break;
                case "NumericInt32":
                    for (var i = 0; i < 4; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                    ? Functions.Int32ToBytes((int)numeric.Value)[i]
                                                                                    : Functions.UInt32ToBytes((uint)numeric.Value)[i]);
                    }
                    break;
                case "NumericFloat":
                    for (var i = 0; i < 4; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, Functions.FloatToByteArray((int)numeric.Value)[i]);
                    }
                    break;
            }
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

        #endregion

        #region Thread Safe
        private void SetHexBoxByteProvider(DynamicByteProvider value)
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetHexBoxByteProvider(value)));
            else
            {
                hexBox.ByteProvider = value;
            }
        }
        private void SetHexBoxRefresh()
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker)(SetHexBoxRefresh));
            else
            {
                hexBox.Refresh();
            }
        }
        private DynamicByteProvider GetHexBoxByteProvider()
        {
            //recursion
            DynamicByteProvider returnVal = new DynamicByteProvider(new byte[]{0,0,0,0});
            if (hexBox.InvokeRequired) hexBox.Invoke((MethodInvoker)
                  delegate { returnVal = GetHexBoxByteProvider(); });
            else
                return (DynamicByteProvider)hexBox.ByteProvider;
            return returnVal;
        }
        #endregion

        #region Thread Function
        private void Peek(object a)
        {
            try
            {
                EnableControl(peekButton, false);
                if (string.IsNullOrEmpty(GetTextBoxText(peekLengthTextBox)) || Convert.ToUInt32(GetTextBoxText(peekLengthTextBox), 16) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(GetTextBoxText(peekPokeAddressTextBox)) || Convert.ToUInt32(GetTextBoxText(peekPokeAddressTextBox), 16) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte
                byte[] retValue = Functions.StringToByteArray(_rtm.Peek(GetTextBoxText(peekPokeAddressTextBox), GetTextBoxText(peekLengthTextBox), GetTextBoxText(peekPokeAddressTextBox), GetTextBoxText(peekLengthTextBox)));
                DynamicByteProvider buffer = new DynamicByteProvider(retValue) { IsWriteByte = true }; //object initilizer 
                SetHexBoxByteProvider(buffer);
                SetHexBoxRefresh();
                EnableControl(peekButton, true);
                SetTextBoxText(peekPokeFeedBackTextBox,"Peek Success!");
                UpdateProgressbar(0, 100, 0);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Error);
                EnableControl(peekButton, true);
            }
        }
        private void Poke(object a)
        {
            try
            {
                EnableControl(pokeButton, false);
                uint dumplength = (uint)hexBox.ByteProvider.Length / 2;
                //if (dumplength > 240)
                //    throw new Exception("Poke Length has to be under 240 bytes.");
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(peekPokeAddressTextBox));//Set the dump offset
                _rtm.DumpLength = dumplength;//The length of data to dump

                DynamicByteProvider buffer = GetHexBoxByteProvider();

                StringBuilder sb = new StringBuilder(buffer.Bytes.Count * 2);
                foreach (byte b in buffer.Bytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
                _rtm.Poke(GetTextBoxText(peekPokeAddressTextBox), sb.ToString());
                SetTextBoxText(peekPokeFeedBackTextBox, "Poke Success!");
                EnableControl(pokeButton, true);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControl(pokeButton, true);
            }
        }
        #endregion
    }
}
