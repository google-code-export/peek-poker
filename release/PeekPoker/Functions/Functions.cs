using System;

namespace PeekPoker
{
    static class Functions
    {
        /// <summary>Verifies if the given string is hex</summary>
        /// <param name="value">The string value to check</param>
        /// <returns>True if its hex and false if it isn't.</returns>
        public static bool IsHex(string value)
        {
            if (value.Length % 2 != 0) return false;
            //^ - Begin the match at the beginning of the line.
            //$ - End the match at the end of the line.
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>Convert Byte Array to String Hex</summary>
        /// <param name="value">The byte array</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(byte[] value)
        {
            try
            {
                return BitConverter.ToString(value).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static byte[] StringToByteArray(string text)
        {
            var bytes = new byte[text.Length / 2];

            for (int i = 0; i < text.Length; i += 2)
            {
                bytes[i / 2] = byte.Parse(text[i].ToString() + text[i + 1].ToString(),
                    System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }

        public static uint Convert(string value)
        {
            //using Ternary operator
            return value.Contains("0x") ?
                System.Convert.ToUInt32(value.Substring(2), 16) : System.Convert.ToUInt32(value);
        }
        
        public static string ByteArrayToString(byte[] bytes)
        {
            var text = "";

            foreach (byte t in bytes)
            {
                text += String.Format("{0,0:X2}", t);
            }

            return text;
        }

        /// <summary>Converts Unsigned Integer 32 to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt32ToBytes(UInt32 value)
        {
            var buffer = new Byte[4];
            buffer[3] = (Byte)(value & 0xFF);
            buffer[2] = (Byte)((value >> 8) & 0xFF);
            buffer[1] = (Byte)((value >> 16) & 0xFF);
            buffer[0] = (Byte)((value >> 24) & 0xFF);
            return buffer;
        }

        /// <summary>Converts a Hex string to bytes</summary>
        /// <param name="input">Is the String input</param>
        public static byte[] HexToBytes(String input)
        {
            input = input.Replace(" ", "");
            input = input.Replace("-", "");
            input = input.Replace("0x", "");
            input = input.Replace("0X", "");
            if ((input.Length % 2) != 0)
                input = "0" + input;
            var output = new byte[(input.Length / 2)];

            try
            {
                int index;
                for (index = 0; index < output.Length; index++)
                {
                    output[index] = System.Convert.ToByte(input.Substring((index * 2), 2), 16);
                }
                return output;
            }
            catch
            {
                throw new Exception("Invalid byte Input");
            }
        }
    }
}
