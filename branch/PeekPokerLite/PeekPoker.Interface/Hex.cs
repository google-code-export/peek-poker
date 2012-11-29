using System;
using System.Text.RegularExpressions;

namespace PeekPoker.Interface
{
    /// <summary>Contains function/s to convert input values to hex</summary>
    public class Hex
    {
        /// <summary>Verifies if the given string is hex</summary>
        /// <param name="value">The string value to check</param>
        /// <returns>True if its hex and false if it isn't.</returns>
        public static bool IsHex(string value)
        {
            if (value.Length%2 != 0) return false;
            //^ - Begin the match at the beginning of the line.
            //$ - End the match at the end of the line.
            return Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>Convert Long to String Hex</summary>
        /// <param name="value">The byte array</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(long value)
        {
            try
            {
                byte[] buffer = ToByteArray.Int64(value);
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Convert float to String Hex</summary>
        /// <param name="value">The float value</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(float value)
        {
            try
            {
                return ToHexString(ToByteArray.Single(value));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
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

        /// <summary>Convert a decimal value to 4 bytes hex string</summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The hex string</returns>
        public static string ToHexString4Bytes(Decimal value)
        {
            try
            {
                byte[] output = ToByteArray.Decimal4Bytes(value);
                return (ToHexString(output));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Convert a decimal value to hex string</summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The hex string</returns>
        public static string ToHexString(Decimal value)
        {
            try
            {
                byte[] output = ToByteArray.Decimal(value);
                return (ToHexString(output));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Edit a String Hex</summary>
        /// <param name="hexString">The valid String you want to edit</param>
        /// <param name="firstCharPos">The first Character we want to keep Position -starting from 0</param>
        /// <param name="finalCharPos">The Final Character we want to keep</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(string hexString, int firstCharPos, int finalCharPos)
        {
            try
            {
                hexString = hexString.Replace(" ", "");
                hexString = hexString.Replace("-", "");
                hexString = hexString.Replace("0x", "");
                hexString = hexString.Replace("0X", "");
                string str = hexString.Remove(finalCharPos + 1, hexString.Length - (finalCharPos + 1));
                string str2 = str.Substring(firstCharPos, str.Length - firstCharPos);
                if ((str2.Length%2) != 0)
                {
                    return "0" + str2;
                }
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}