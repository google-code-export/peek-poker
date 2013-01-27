using System;
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.PeekNPoke
{
    
    public partial class PeekNPoke : Form
    {
        #region variables
        public event ShowMessageBoxHandler ShowMessageBox;
        public event EnableControlHandler EnableControl;
        public event GetTextBoxTextHandler GetTextBoxText;
        public event SetTextBoxTextDelegateHandler SetTextBoxText;
        public event UpdateProgressBarHandler UpdateProgressbar;
        private byte[] _old;

        private readonly RealTimeMemory _rtm;
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        #endregion

        public PeekNPoke(RealTimeMemory rtm)
        {
            this.InitializeComponent();
            this._rtm = rtm;
        }

        #region Handlers
        private void FreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                this._rtm.StopCommand();
                this.unfreezeButton.Enabled = true;
                this.freezeButton.Enabled = false;
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.unfreezeButton.Enabled = false;
                this.freezeButton.Enabled = true;
            }
        }
        private void UnfreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                this._rtm.StartCommand();
                this.unfreezeButton.Enabled = false;
                this.freezeButton.Enabled = true;
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.unfreezeButton.Enabled = true;
                this.freezeButton.Enabled = false;
            }
        }
        private void HexBoxSelectionStartChanged(object sender, EventArgs e)
        {
            this.ChangeNumericValue();//When you select an offset on the hexbox

            var prev = Functions.HexToBytes(this.peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            this.SelAddress.Text = string.Format((address + (int)this.hexBox.SelectionStart).ToString("X8"));
        }
        private void IsSignedCheckedChanged(object sender, EventArgs e)
        {
            this.ChangeNumericMaxMin();
            this.ChangeNumericValue();
        }
        private void NumericIntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.hexBox.ByteProvider != null)
            {
                this.ChangedNumericValue(sender);
            }
        }
        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!Functions.IsHex(peekPokeAddressTextBox.Text))
                {
                    if (!this.peekPokeAddressTextBox.Text.Equals(""))
                        this.peekPokeAddressTextBox.Text = uint.Parse(this.peekPokeAddressTextBox.Text).ToString("X");
                }
                if (Functions.IsHex(peekLengthTextBox.Text)) return;
                if (!this.peekLengthTextBox.Text.Equals(""))
                    this.peekLengthTextBox.Text = uint.Parse(this.peekLengthTextBox.Text).ToString("X");
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message,"PeekNPoke",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void PeekButtonClick(object sender, EventArgs e)
        {
            this.AutoComplete();//run function
            ThreadPool.QueueUserWorkItem(this.Peek);
        }
        private void PokeButtonClick(object sender, EventArgs e)
        {
            this.AutoComplete(); //run function
            ThreadPool.QueueUserWorkItem(this.Poke);
        }
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            this.NewPeek();
        }
        private void NewPeek()
        {
            //Clean up
            this.peekPokeAddressTextBox.Text = "C0000000";
            this.peekLengthTextBox.Text = "FF";
            this.SelAddress.Clear();
            this.peekPokeFeedBackTextBox.Clear();
            this.NumericInt8.Value = 0;
            this.NumericInt16.Value = 0;
            this.NumericInt32.Value = 0;
            this.NumericFloatTextBox.Text = "0";
            this.hexBox.ByteProvider = null;
            this.hexBox.Refresh();
        }
        private void PeekNPokeLoad(object sender, EventArgs e)
        {
            this.ChangeNumericMaxMin();
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
            
            this.ChangeNumericValue();//When you select an offset on the hexbox

            if(this.hexBox.ByteProvider == null)return;
            var prev = Functions.HexToBytes(this.peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            this.SelAddress.Text = string.Format((address + (int)this.hexBox.SelectionStart).ToString("X8"));
        }
        #endregion

        #region Functions
        private void ChangeNumericMaxMin()
        {
            if (this.isSigned.Checked)
            {
                this.NumericInt8.Maximum = SByte.MaxValue;
                this.NumericInt8.Minimum = SByte.MinValue;
                this.NumericInt16.Maximum = Int16.MaxValue;
                this.NumericInt16.Minimum = Int16.MinValue;
                this.NumericInt32.Maximum = Int32.MaxValue;
                this.NumericInt32.Minimum = Int32.MinValue;
            }
            else
            {
                this.NumericInt8.Maximum = Byte.MaxValue;
                this.NumericInt8.Minimum = Byte.MinValue;
                this.NumericInt16.Maximum = UInt16.MaxValue;
                this.NumericInt16.Minimum = UInt16.MinValue;
                this.NumericInt32.Maximum = UInt32.MaxValue;
                this.NumericInt32.Minimum = UInt32.MinValue;
            }
        }
        private void ChangeNumericValue()
        {
            if (this.hexBox.ByteProvider == null) return;
            var buffer = this.hexBox.ByteProvider.Bytes;
            if (this.isSigned.Checked)
            {
                this.NumericInt8.Value = (buffer.Count - this.hexBox.SelectionStart) > 0 ?
                    Functions.ByteToSByte(this.hexBox.ByteProvider.ReadByte(this.hexBox.SelectionStart)) : 0;
                this.NumericInt16.Value = (buffer.Count - this.hexBox.SelectionStart) > 1 ?
                    Functions.BytesToInt16(buffer.GetRange((int)this.hexBox.SelectionStart, 2).ToArray()) : 0;
                this.NumericInt32.Value = (buffer.Count - this.hexBox.SelectionStart) > 3 ?
                    Functions.BytesToInt32(buffer.GetRange((int)this.hexBox.SelectionStart, 4).ToArray()) : 0;
                this.NumericFloatTextBox.Clear();
                float f = (buffer.Count - this.hexBox.SelectionStart) > 3
                    ? Functions.BytesToSingle(buffer.GetRange((int)this.hexBox.SelectionStart, 4).ToArray()) : 0;
                this.NumericFloatTextBox.Text = f.ToString();
            }
            else
            {
                this.NumericInt8.Value = (buffer.Count - this.hexBox.SelectionStart) > 0 ?
                    buffer[(int)this.hexBox.SelectionStart] : 0;
                this.NumericInt16.Value = (buffer.Count - this.hexBox.SelectionStart) > 1 ?
                    Functions.BytesToUInt16(buffer.GetRange((int)this.hexBox.SelectionStart, 2).ToArray()) : 0;
                this.NumericInt32.Value = (buffer.Count - this.hexBox.SelectionStart) > 3 ?
                    Functions.BytesToUInt32(buffer.GetRange((int)this.hexBox.SelectionStart, 4).ToArray()) : 0;
                
                this.NumericFloatTextBox.Clear();
                float f = (buffer.Count - this.hexBox.SelectionStart) > 3
                    ? Functions.BytesToSingle(buffer.GetRange((int) this.hexBox.SelectionStart, 4).ToArray()) : 0;
                this.NumericFloatTextBox.Text = f.ToString();
            }
            var prev = Functions.HexToBytes(this.peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            this.SelAddress.Text = string.Format((address + (int)this.hexBox.SelectionStart).ToString("X8"));
        }
        private void ChangedNumericValue(object sender)
        {
            if (this.hexBox.SelectionStart >= this.hexBox.ByteProvider.Bytes.Count) return;
            var numeric = (NumericUpDown)sender;
            switch (numeric.Name)
            {
                case "NumericInt8":
                    if (this.isSigned.Checked)
                    {
                        Console.WriteLine(((sbyte)numeric.Value).ToString("X2"));
                        this.hexBox.ByteProvider.WriteByte(this.hexBox.SelectionStart,
                                                      Functions.HexToBytes(((sbyte)numeric.Value).ToString("X2"))[0]);
                    }
                    else
                    {
                        this.hexBox.ByteProvider.WriteByte(this.hexBox.SelectionStart,
                                                      Convert.ToByte((byte)numeric.Value));
                    }
                    break;
                case "NumericInt16":
                    for (var i = 0; i < 2; i++)
                    {
                        this.hexBox.ByteProvider.WriteByte(this.hexBox.SelectionStart + i, this.isSigned.Checked
                                                                                    ? Functions.Int16ToBytes((short)numeric.Value)[i]
                                                                                    : Functions.UInt16ToBytes((ushort)numeric.Value)[i]);
                    }
                    break;
                case "NumericInt32":
                    for (var i = 0; i < 4; i++)
                    {
                        this.hexBox.ByteProvider.WriteByte(this.hexBox.SelectionStart + i, this.isSigned.Checked
                                                                                    ? Functions.Int32ToBytes((int)numeric.Value)[i]
                                                                                    : Functions.UInt32ToBytes((uint)numeric.Value)[i]);
                    }
                    break;
                case "NumericFloat":
                    for (var i = 0; i < 4; i++)
                    {
                        this.hexBox.ByteProvider.WriteByte(this.hexBox.SelectionStart + i, Functions.FloatToByteArray((int)numeric.Value)[i]);
                    }
                    break;
            }
            this.hexBox.Refresh();
        }

        private void AutoComplete()
        {
            this.peekPokeAddressTextBox.AutoCompleteCustomSource = this._data;//put the auto complete data into the textbox
            var count = this._data.Count;
            for (var index = 0; index < count; index++)
            {
                var value = this._data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, this.peekPokeAddressTextBox.Text))
                    this._data.Add(this.peekPokeAddressTextBox.Text);
            }
        }

        #endregion

        #region Thread Safe
        private void SetHexBoxByteProvider(DynamicByteProvider value)
        {
            if (this.hexBox.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.SetHexBoxByteProvider(value)));
            else
            {
                this.hexBox.ByteProvider = value;
            }
        }
        private void SetHexBoxRefresh()
        {
            if (this.hexBox.InvokeRequired)
                this.Invoke((MethodInvoker)(this.SetHexBoxRefresh));
            else
            {
                this.hexBox.Refresh();
            }
        }
        private DynamicByteProvider GetHexBoxByteProvider()
        {
            //recursion
            DynamicByteProvider returnVal = new DynamicByteProvider(new byte[]{0,0,0,0});
            if (this.hexBox.InvokeRequired) this.hexBox.Invoke((MethodInvoker)
                  delegate { returnVal = this.GetHexBoxByteProvider(); });
            else
                return (DynamicByteProvider)this.hexBox.ByteProvider;
            return returnVal;
        }
        #endregion

        #region Thread Function
        private void Peek(object a)
        {
            try
            {
                this.EnableControl(this.peekButton, false);
                if (string.IsNullOrEmpty(this.GetTextBoxText(this.peekLengthTextBox)) || Convert.ToUInt32(this.GetTextBoxText(this.peekLengthTextBox), 16) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(this.GetTextBoxText(this.peekPokeAddressTextBox)) || Convert.ToUInt32(this.GetTextBoxText(this.peekPokeAddressTextBox), 16) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte
                
                byte[] retValue = Functions.StringToByteArray(this._rtm.Peek(this.GetTextBoxText(this.peekPokeAddressTextBox), this.GetTextBoxText(this.peekLengthTextBox), this.GetTextBoxText(this.peekPokeAddressTextBox), this.GetTextBoxText(this.peekLengthTextBox)));
                DynamicByteProvider buffer = new DynamicByteProvider(retValue) { IsWriteByte = true }; //object initilizer 
                
                this._old = new byte[buffer.Bytes.Count];
                buffer.Bytes.CopyTo(this._old);

                this.SetHexBoxByteProvider(buffer);
                this.SetHexBoxRefresh();
                this.EnableControl(this.peekButton, true);
                
                this.SetTextBoxText(this.peekPokeFeedBackTextBox,"Peek Success!");
                this.UpdateProgressbar(0, 100, 0);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.EnableControl(this.peekButton, true);
            }
        }
        private void Poke(object a)
        {
            try
            {
                this.EnableControl(this.pokeButton, false);
                uint dumplength = (uint)this.hexBox.ByteProvider.Length / 2;
                this._rtm.DumpOffset = Functions.Convert(this.GetTextBoxText(this.peekPokeAddressTextBox));//Set the dump offset
                this._rtm.DumpLength = dumplength;//The length of data to dump

                DynamicByteProvider buffer = this.GetHexBoxByteProvider();

                for (int i = 0; i < buffer.Bytes.Count; i++)
                {
                    if(buffer.Bytes[i] == this._old[i]) continue;

                    uint value = Convert.ToUInt32(this.peekPokeAddressTextBox.Text, 16);
                    string address = string.Format((value + i).ToString("X8"));
                    this._rtm.Poke(address, String.Format("{0,0:X2}",buffer.Bytes[i]));
                    this.SetTextBoxText(this.peekPokeFeedBackTextBox, "Poke Success!");
                }
                
                this.EnableControl(this.pokeButton, true);
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.EnableControl(this.pokeButton, true);
            }
        }
        #endregion
    }
}
