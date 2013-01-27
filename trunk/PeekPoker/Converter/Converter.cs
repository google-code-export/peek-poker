using System;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.Converter
{
    public partial class Converter : Form
    {
		public event ShowMessageBoxHandler ShowMessageBox;
        public Converter()
        {
            this.InitializeComponent();
        }
        #region Autocalculation
        private void Int32ToHex(object sender, EventArgs e)
        {
            if (!this.integer32CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(this.integer32CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            Int32 number;
            var validResult = Int32.TryParse(this.integer32CalculatorTextBox.Text, out number); //Stops things like a single "-" causing errors
            if (!validResult) return;


            if (!this.BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                var num3 = (number >> 0x10) & 0xff;
                var num4 = (number >> 0x18) & 0xff;
                number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
            }

            var hex = number.ToString("X4"); //x is for hex and 4 is padding to a 4 digit value, uppercases.
            this.hexCalculatorTextBox.Text = (string.Format(hex)); //Formats string, adds 0x
        }

        private void Int8ToHex(object sender, EventArgs e)
        {
            if (!this.integer8CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(this.integer8CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            byte number;
            var validResult = byte.TryParse(this.integer8CalculatorTextBox.Text, out number);
            if (!validResult) return;

            var hex = number.ToString("X2"); //x is for hex and 2 is padding to a 2 digit value, uppercases.
            this.hexCalculatorTextBox.Text = (string.Format(hex)); //Formats string, adds 0x
        }

        private void Int16ToHex(object sender, EventArgs e)
        {
            if (!this.integer16CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(this.integer16CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            short number;
            var validResult = short.TryParse(this.integer16CalculatorTextBox.Text, out number);
            if (!validResult) return;

            if (!this.BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                number = (short)((num << 8) | num2);
            }
            this.hexCalculatorTextBox.Text = (string.Format(number.ToString("X3")));
        }

        private void FloatToHex(object sender, EventArgs e)
        {
            if (!this.floatCalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(this.floatCalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            float number;
            var validResult = float.TryParse(this.floatCalculatorTextBox.Text, out number);
            if (!validResult) return;

            var buffer = BitConverter.GetBytes(number);//comes out as little endian
            if (this.BigEndianRadioButton.Checked) Array.Reverse(buffer);

            var hex = BitConverter.ToString(buffer).Replace("-", "");
            this.hexCalculatorTextBox.Text = (string.Format(hex));
        }

        private void HexToInt(object sender, EventArgs e)
        {
            if (!this.hexCalculatorTextBox.Focused) return;
            var hexycalc = this.hexCalculatorTextBox.Text.StartsWith("0x") ? this.hexCalculatorTextBox.Text.Substring(2) : this.hexCalculatorTextBox.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(hexycalc, @"\A\b[0-9a-fA-F]+\b\Z")) return;
            try
            {
                if (hexycalc.Length >= 0 && hexycalc.Length <= 2)
                {
                    this.integer8CalculatorTextBox.Text = Convert.ToSByte(this.hexCalculatorTextBox.Text, 16).ToString();
                    this.integer16CalculatorTextBox.Clear();
                    this.integer32CalculatorTextBox.Clear();
                    this.floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 2 && hexycalc.Length <= 4)
                {
                    var number = Convert.ToInt16(this.hexCalculatorTextBox.Text, 16);
                    if (!this.BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        number = (short)((num << 8) | num2);
                    }

                    this.integer16CalculatorTextBox.Text = number.ToString();
                    this.integer32CalculatorTextBox.Clear();
                    this.floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 8)
                {
                    var number = Convert.ToInt32(this.hexCalculatorTextBox.Text, 16);
                    if (!this.BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        var num3 = (number >> 0x10) & 0xff;
                        var num4 = (number >> 0x18) & 0xff;
                        number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
                    }
                    this.integer32CalculatorTextBox.Text = number.ToString();

                    var input = this.hexCalculatorTextBox.Text;
                    var output = new byte[(input.Length / 2)];

                    if ((input.Length % 2) != 0) input = "0" + input;
                    int index;
                    for (index = 0; index < output.Length; index++)
                    {
                        output[index] = Convert.ToByte(input.Substring((index * 2), 2), 16);
                    }
                    Array.Reverse(output);
                    this.floatCalculatorTextBox.Text = BitConverter.ToSingle(output, 0).ToString();
                }
            }
            catch (Exception ex)
            {
				this.ShowMessageBox(ex.Message,"Peek Poker - Converter",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void ConverterClearButtonClick(object sender, EventArgs e)
        {
            this.integer8CalculatorTextBox.Clear();
            this.integer16CalculatorTextBox.Clear();
            this.integer32CalculatorTextBox.Clear();
            this.floatCalculatorTextBox.Clear();
            this.hexCalculatorTextBox.Clear();
        }
        #endregion
    }
}
