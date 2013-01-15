#region

using System;

#endregion

namespace PeekPoker.Interface
{
    /// <summary>Contains function/s to converts bytes or objects to values </summary>
    public class ToValue
    {
        /// <summary>Converts 2 Bytes Array to Unsigned Integer 16</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt16 ToUInt16(byte[] buffer)
        {
            if (buffer.Length > 2)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 2)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (ushort) (buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 2 Bytes Array to Integer 16</summary>
        /// <param name="buffer">byte Array</param>
        public static Int16 ToInt16(Byte[] buffer)
        {
            if (buffer.Length > 2)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 2)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (short) (buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 3 Bytes Array to Integer 24</summary>
        /// <param name="buffer">byte Array</param>
        public static Int32 ToInt24(Byte[] buffer)
        {
            if (buffer.Length > 3)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 3)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 3 Bytes Array to Unsigned Unsigned Integer 24</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt32 ToUInt24(Byte[] buffer)
        {
            if (buffer.Length > 3)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 3)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((uint) buffer[2] << 16 | (uint) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 4 Bytes Array to Integer 32</summary>
        /// <param name="buffer">byte Array</param>
        public static Int32 ToInt32(Byte[] buffer)
        {
            if (buffer.Length > 4)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 4)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (buffer[3] << 24 | buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 4 Bytes Array to Unsigned Integer 32</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt32 ToUInt32(Byte[] buffer)
        {
            if (buffer.Length > 4)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 4)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (uint) (buffer[3] << 24 | buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 5 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt40(Byte[] buffer)
        {
            if (buffer.Length > 5)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 5)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[4] << 32 | (long) buffer[3] << 24 | (long) buffer[2] << 16 | (long) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts 5 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt40(Byte[] buffer)
        {
            if (buffer.Length > 5)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 5)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts 6 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt48(Byte[] buffer)
        {
            if (buffer.Length > 6)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 6)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[5] << 40 | (long) buffer[4] << 32 | (long) buffer[3] << 24 | (long) buffer[2] << 16 |
                    (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 6 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt48(Byte[] buffer)
        {
            if (buffer.Length > 6)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 6)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[5] << 40 | (ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 |
                    (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 7 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt56(Byte[] buffer)
        {
            if (buffer.Length > 7)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 7)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[6] << 48 | (long) buffer[5] << 40 | (long) buffer[4] << 32 | (long) buffer[3] << 24 |
                    (long) buffer[2] << 16 | (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 7 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt56(Byte[] buffer)
        {
            if (buffer.Length > 7)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 7)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[6] << 48 | (ulong) buffer[5] << 40 | (ulong) buffer[4] << 32 |
                    (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 8 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt64(Byte[] buffer)
        {
            if (buffer.Length > 8)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 8)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[7] << 56 | (long) buffer[6] << 48 | (long) buffer[5] << 40 | (long) buffer[4] << 32 |
                    (long) buffer[3] << 24 | (long) buffer[2] << 16 | (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 8 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt64(Byte[] buffer)
        {
            if (buffer.Length > 8)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 8)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[7] << 56 | (ulong) buffer[6] << 48 | (ulong) buffer[5] << 40 |
                    (ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts Byte Array to Single/Float</summary>
        /// <param name="buffer">The Byte Array</param>
        public static Single ToSingle(Byte[] buffer)
        {
            try
            {
                Array.Reverse(buffer);
                return BitConverter.ToSingle(buffer, 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Converts Byte Array to Double</summary>
        /// <param name="buffer">The Byte Array</param>
        public static Double ToDouble(Byte[] buffer)
        {
            try
            {
                Array.Reverse(buffer);
                return BitConverter.ToDouble(buffer, 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Convert hex string to Int</summary>
        /// <param name="value">The string(hex) value</param>
        /// <returns>A long value of the given hex</returns>
        public static int ToInt(String value)
        {
            try
            {
                if (!Hex.IsHex(value))
                    throw new Exception(string.Format("{0} is not a valid hex.", value));
                return ToInt32(ToByteArray.HexToBytes(value));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Convert hex string to Long</summary>
        /// <param name="value">The string(hex) value</param>
        /// <returns>A long value of the given hex</returns>
        public static long ToLong(String value)
        {
            try
            {
                if (!Hex.IsHex(value))
                    throw new Exception(string.Format("{0} is not a valid hex.", value));
                return ToInt64(ToByteArray.HexToBytes(value));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}