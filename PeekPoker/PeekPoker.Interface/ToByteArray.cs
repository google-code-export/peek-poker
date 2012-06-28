using System;
using System.Globalization;
using System.IO;

namespace PeekPoker.Interface
{
    #region ToByteArray

    /// <summary>Contains function/s to converts values or objects to byte array</summary>
    public class ToByteArray
    {
        /// <summary>Converts Integer 16 to 2 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int16(Int16 value)
        {
            var buffer = new Byte[2];
            buffer[1] = (Byte)(value & 0xFF);
            buffer[0] = (Byte)((value >> 8) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 16 to 2 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt16(UInt16 value)
        {
            var buffer = new Byte[2];
            buffer[1] = (Byte)(value & 0xFF);
            buffer[0] = (Byte)((value >> 8) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 24 to 3 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int24(Int32 value)
        {
            if (value < -8388608 || value > 8388607)
                throw new Exception("Invalid value");
            var buffer = new Byte[3];
            buffer[2] = (Byte)(value & 0xFF);
            buffer[1] = (Byte)((value >> 8) & 0xFF);
            buffer[0] = (Byte)((value >> 16) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 24 to 3 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt24(UInt32 value)
        {
            if (value < 0 || value > 16777215)
                throw new Exception("Invalid value");
            var buffer = new Byte[3];
            buffer[2] = (Byte)(value & 0xFF);
            buffer[1] = (Byte)((value >> 8) & 0xFF);
            buffer[0] = (Byte)((value >> 16) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 32 to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int32(Int32 value)
        {
            var buffer = new Byte[4];
            buffer[3] = (Byte)(value & 0xFF);
            buffer[2] = (Byte)((value >> 8) & 0xFF);
            buffer[1] = (Byte)((value >> 16) & 0xFF);
            buffer[0] = (Byte)((value >> 24) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 32 to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt32(UInt32 value)
        {
            var buffer = new Byte[4];
            buffer[3] = (Byte)(value & 0xFF);
            buffer[2] = (Byte)((value >> 8) & 0xFF);
            buffer[1] = (Byte)((value >> 16) & 0xFF);
            buffer[0] = (Byte)((value >> 24) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 40 to 5 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int40(Int64 value)
        {
            if (value < -549755813888 || value > 549755813887)
            {
                throw new Exception("Invalid value");
            }
            var buffer = new Byte[5];
            buffer[4] = (Byte)(value & 0xFF);
            buffer[3] = (Byte)((value >> 8) & 0xFF);
            buffer[2] = (Byte)((value >> 16) & 0xFF);
            buffer[1] = (Byte)((value >> 24) & 0xFF);
            buffer[0] = (Byte)((value >> 32) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 40 to 5 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt40(UInt64 value)
        {
            if (value < 0 || value > 1099511627775)
            {
                throw new Exception("Invalid value");
            }
            var buffer = new Byte[5];
            buffer[4] = (Byte)(value & 0xFF);
            buffer[3] = (Byte)((value >> 8) & 0xFF);
            buffer[2] = (Byte)((value >> 16) & 0xFF);
            buffer[1] = (Byte)((value >> 24) & 0xFF);
            buffer[0] = (Byte)((value >> 32) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 64 to 6 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int48(Int64 value)
        {
            var buffer = new Byte[6];
            buffer[5] = (Byte)(value & 0xFF);
            buffer[4] = (Byte)((value >> 8) & 0xFF);
            buffer[3] = (Byte)((value >> 16) & 0xFF);
            buffer[2] = (Byte)((value >> 24) & 0xFF);
            buffer[1] = (Byte)((value >> 32) & 0xFF);
            buffer[0] = (Byte)((value >> 40) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 64 to 6 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt48(UInt64 value)
        {
            var buffer = new Byte[6];
            buffer[5] = (Byte)(value & 0xFF);
            buffer[4] = (Byte)((value >> 8) & 0xFF);
            buffer[3] = (Byte)((value >> 16) & 0xFF);
            buffer[2] = (Byte)((value >> 24) & 0xFF);
            buffer[1] = (Byte)((value >> 32) & 0xFF);
            buffer[0] = (Byte)((value >> 40) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 64 to 7 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int56(Int64 value)
        {
            var buffer = new Byte[7];
            buffer[6] = (Byte)(value & 0xFF);
            buffer[5] = (Byte)((value >> 8) & 0xFF);
            buffer[4] = (Byte)((value >> 16) & 0xFF);
            buffer[3] = (Byte)((value >> 24) & 0xFF);
            buffer[2] = (Byte)((value >> 32) & 0xFF);
            buffer[1] = (Byte)((value >> 40) & 0xFF);
            buffer[0] = (Byte)((value >> 48) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 64 to 7 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt56(UInt64 value)
        {
            var buffer = new Byte[7];
            buffer[6] = (Byte)(value & 0xFF);
            buffer[5] = (Byte)((value >> 8) & 0xFF);
            buffer[4] = (Byte)((value >> 16) & 0xFF);
            buffer[3] = (Byte)((value >> 24) & 0xFF);
            buffer[2] = (Byte)((value >> 32) & 0xFF);
            buffer[1] = (Byte)((value >> 40) & 0xFF);
            buffer[0] = (Byte)((value >> 48) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Integer 64 to 8 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Int64(Int64 value)
        {
            var buffer = new Byte[8];
            buffer[7] = (Byte)(value & 0xFF);
            buffer[6] = (Byte)((value >> 8) & 0xFF);
            buffer[5] = (Byte)((value >> 16) & 0xFF);
            buffer[4] = (Byte)((value >> 24) & 0xFF);
            buffer[3] = (Byte)((value >> 32) & 0xFF);
            buffer[2] = (Byte)((value >> 40) & 0xFF);
            buffer[1] = (Byte)((value >> 48) & 0xFF);
            buffer[0] = (Byte)((value >> 56) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Unsigned Integer 64 to 8 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt64(UInt64 value)
        {
            var buffer = new Byte[8];
            buffer[7] = (Byte)(value & 0xFF);
            buffer[6] = (Byte)((value >> 8) & 0xFF);
            buffer[5] = (Byte)((value >> 16) & 0xFF);
            buffer[4] = (Byte)((value >> 24) & 0xFF);
            buffer[3] = (Byte)((value >> 32) & 0xFF);
            buffer[2] = (Byte)((value >> 40) & 0xFF);
            buffer[1] = (Byte)((value >> 48) & 0xFF);
            buffer[0] = (Byte)((value >> 56) & 0xFF);
            return buffer;
        }

        /// <summary>Converts Single to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Single(Single value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>Converts Double to 8 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Double(Double value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>Converts Decimal to 8 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Decimal(Decimal value)
        {
            var buffer = BitConverter.GetBytes(long.Parse(value.ToString(CultureInfo.InvariantCulture)));
            Array.Reverse(buffer);
            return buffer;
        }

        /// <summary>Converts Decimal to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] Decimal4Bytes(Decimal value)
        {
            var buffer = BitConverter.GetBytes(int.Parse(value.ToString(CultureInfo.InvariantCulture)));
            Array.Reverse(buffer);
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
                    output[index] = Convert.ToByte(input.Substring((index * 2), 2), 16);
                }
                return output;
            }
            catch
            {
                throw new Exception("Invalid byte Input");
            }
        }

        /// <summary>Get a piece of a full byte array</summary>
        /// <param name="piece">The Full byte you want to take a piece from</param>
        /// <param name="startOffset">The starting offset</param>
        /// <param name="size">The full size of your new byte piece</param>
        public static Byte[] BytePiece(Byte[] piece, UInt32 startOffset, UInt32 size)
        {
            var buffer = new Byte[size];
            
            for (var i = 0; i < size; i++)
            {
                buffer[i] = piece[startOffset+i];
            }
            return buffer;
        }
    }
    #endregion
}
