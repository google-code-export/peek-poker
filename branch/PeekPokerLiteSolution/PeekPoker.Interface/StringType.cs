namespace PeekPoker.Interface
{
    /// <summary>
    ///   The String type enumeration
    /// </summary>
    public enum StringType : byte
    {
        /// <summary>
        ///   This is Ascii character type
        /// </summary>
        Ascii = 1,

        /// <summary>
        ///   This is Unicode character type
        /// </summary>
        Unicode = 2,

        /// <summary>
        ///   Hex String Format
        /// </summary>
        HexString = 3
    }
}