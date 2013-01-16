﻿using System;
using System.Windows.Forms;

namespace PeekPoker
{
    public partial class Converter : Form
    {
        public Converter()
        {
            InitializeComponent();
        }
        #region Autocalculation
        private void Int32ToHex(object sender, EventArgs e)
        {
            if (!integer32CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer32CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            Int32 number;
            var validResult = Int32.TryParse(integer32CalculatorTextBox.Text, out number); //Stops things like a single "-" causing errors
            if (!validResult) return;


            if (!BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                var num3 = (number >> 0x10) & 0xff;
                var num4 = (number >> 0x18) & 0xff;
                number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
            }

            var hex = number.ToString("X4"); //x is for hex and 4 is padding to a 4 digit value, uppercases.
            hexCalculatorTextBox.Text = (string.Format("0x" + hex)); //Formats string, adds 0x
        }

        private void Int8ToHex(object sender, EventArgs e)
        {
            if (!integer8CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer8CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            byte number;
            var validResult = byte.TryParse(integer8CalculatorTextBox.Text, out number);
            if (!validResult) return;

            var hex = number.ToString("X2"); //x is for hex and 2 is padding to a 2 digit value, uppercases.
            hexCalculatorTextBox.Text = (string.Format("0x" + hex)); //Formats string, adds 0x
        }

        private void Int16ToHex(object sender, EventArgs e)
        {
            if (!integer16CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer16CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            short number;
            var validResult = short.TryParse(integer16CalculatorTextBox.Text, out number);
            if (!validResult) return;

            if (!BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                number = (short)((num << 8) | num2);
            }
            hexCalculatorTextBox.Text = (string.Format("0x" + number.ToString("X3")));
        }

        private void FloatToHex(object sender, EventArgs e)
        {
            if (!floatCalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(floatCalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            float number;
            var validResult = float.TryParse(floatCalculatorTextBox.Text, out number);
            if (!validResult) return;

            var buffer = BitConverter.GetBytes(number);//comes out as little endian
            if (BigEndianRadioButton.Checked) Array.Reverse(buffer);

            var hex = BitConverter.ToString(buffer).Replace("-", "");
            hexCalculatorTextBox.Text = (string.Format("0x" + hex));
        }

        private void HexToInt(object sender, EventArgs e)
        {
            if (!hexCalculatorTextBox.Focused) return;
            var hexycalc = hexCalculatorTextBox.Text.StartsWith("0x") ? hexCalculatorTextBox.Text.Substring(2) : hexCalculatorTextBox.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(hexycalc, @"\A\b[0-9a-fA-F]+\b\Z")) return;
            try
            {
                if (hexycalc.Length >= 0 && hexycalc.Length <= 2)
                {
                    integer8CalculatorTextBox.Text = Convert.ToSByte(hexCalculatorTextBox.Text, 16).ToString();
                    integer16CalculatorTextBox.Clear();
                    integer32CalculatorTextBox.Clear();
                    floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 2 && hexycalc.Length <= 4)
                {
                    var number = Convert.ToInt16(hexCalculatorTextBox.Text, 16);
                    if (!BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        number = (short)((num << 8) | num2);
                    }

                    integer16CalculatorTextBox.Text = number.ToString();
                    integer32CalculatorTextBox.Clear();
                    floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 8)
                {
                    var number = Convert.ToInt32(hexCalculatorTextBox.Text, 16);
                    if (!BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        var num3 = (number >> 0x10) & 0xff;
                        var num4 = (number >> 0x18) & 0xff;
                        number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
                    }
                    integer32CalculatorTextBox.Text = number.ToString();

                    var input = hexCalculatorTextBox.Text;
                    var output = new byte[(input.Length / 2)];

                    if ((input.Length % 2) != 0) input = "0" + input;
                    int index;
                    for (index = 0; index < output.Length; index++)
                    {
                        output[index] = Convert.ToByte(input.Substring((index * 2), 2), 16);
                    }
                    Array.Reverse(output);
                    floatCalculatorTextBox.Text = BitConverter.ToSingle(output, 0).ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ConverterClearButtonClick(object sender, EventArgs e)
        {
            integer8CalculatorTextBox.Clear();
            integer16CalculatorTextBox.Clear();
            integer32CalculatorTextBox.Clear();
            floatCalculatorTextBox.Clear();
            hexCalculatorTextBox.Clear();
        }
        #endregion
    }
}
