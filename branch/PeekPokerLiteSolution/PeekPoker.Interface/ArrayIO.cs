using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace PeekPoker.Interface
{
    /// <summary>
    ///   Array Class for all your Array Needs
    /// </summary>
    public sealed class ArrayIo : IDisposable
    {
        #region IDisposable Members

        /// <summary>
        ///   Dispose Class, Erase Data
        /// </summary>
        public void Dispose()
        {
            _xarray = null;
            if (!_xdisposed)
            {
                GC.SuppressFinalize(this);
            }
            _xdisposed = true;
        }

        #endregion

        public event UpdateProgressBarHandler ReportProgress;

        #region Variables

        private EndianType _xCurrenEndian;
        private UInt64 _xLast;
        private byte[] _xarray;
        private bool _xdisposed;
        private UInt64 _xposition;
        private Thread _xthread;

        #endregion

        #region Various Stuff

        #region EndianType enum

        /// <summary>
        ///   Endian Type (Byte Order)
        /// </summary>
        public enum EndianType
        {
            /// <summary>
            ///   Big Endian Byte Order
            /// </summary>
            Big,

            /// <summary>
            ///   Little Endian Byte Order
            /// </summary>
            Little
        }

        #endregion

        #region SecurityType enum

        /// <summary>
        ///   Several Security Types
        /// </summary>
        public enum SecurityType
        {
            /// <summary>
            /// </summary>
            Adler32,

            /// <summary>
            /// </summary>
            Checksum32,

            /// <summary>
            /// </summary>
            Crc16Ccitt_FFFF,

            /// <summary>
            /// </summary>
            Crc16Ccitt_0000,

            /// <summary>
            /// </summary>
            Crc16Ccitt_1D0F,

            /// <summary>
            /// </summary>
            CRC32,

            /// <summary>
            /// </summary>
            CRC32Bzip2,

            /// <summary>
            /// </summary>
            CRC32Jam,

            /// <summary>
            /// </summary>
            HMACMD5,

            /// <summary>
            /// </summary>
            Sha1
        }

        #endregion

        /// <summary>
        ///   initialize New array and endiantype
        /// </summary>
        /// <param name="array"> Array to set </param>
        /// <param name="endian"> Endian Type </param>
        public ArrayIo(byte[] array, EndianType endian = EndianType.Little)
        {
            _xarray = array;
            _xCurrenEndian = endian;
            _xdisposed = false;
        }

        /// <summary>
        ///   Initialize File to array and EndianType
        /// </summary>
        /// <param name="file"> File for Posrting to Array </param>
        /// <param name="endian"> Endian Type </param>
        public ArrayIo(string file, EndianType endian = EndianType.Little)
        {
            _xCurrenEndian = endian;
            _xdisposed = false;
            _xthread = new Thread(() => ReadFileX(file));
            _xthread.Start();
        }

        private void ReadFileX(string file)
        {
            try
            {
                _xarray = File.ReadAllBytes(file);
                if (_xthread.ThreadState == ThreadState.Running)
                {
                    _xthread.Abort();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Reader

        /// <summary>
        ///   ReadBytes and Advances by the length
        /// </summary>
        /// <param name="length"> Length to Read </param>
        /// <param name="endian"> Endian Type </param>
        /// <returns> Readed Bytes </returns>
        public byte[] ReadBytes(int length, EndianType endian)
        {
            if (_xdisposed)
            {
                throw (new Exception("Class Has been disposed !"));
            }
            byte[] x = new byte[length];
            _xLast = _xposition;
            Array.Copy(_xarray, Convert.ToInt32(_xposition), x, 0, length);
            _xposition = (ulong) ((Convert.ToInt32(_xposition)) + length);
            if (endian == EndianType.Big)
            {
                Array.Reverse(x);
            }
            return x;
        }

        /// <summary>
        ///   ReadBytes and Advances by the length
        /// </summary>
        /// <param name="length"> Length to Read </param>
        /// <returns> Readed Bytes </returns>
        public byte[] ReadBytes(int length)
        {
            return ReadBytes(length, _xCurrenEndian);
        }

        /// <summary>
        ///   Read Single Byte and advances by 1
        /// </summary>
        /// <returns> Byte </returns>
        public byte ReadUInt8()
        {
            byte s = _xarray[Convert.ToInt32(_xposition)];
            _xposition = _xposition + 1;
            return s;
        }

        /// <summary>
        ///   Reads Single byte as Boolean and advances by 1 byte
        /// </summary>
        /// <returns> Boolean value </returns>
        public bool ReadBoolean()
        {
            return Convert.ToBoolean(ReadUInt8());
        }

        /// <summary>
        ///   Read SByte abd Advances by 1
        /// </summary>
        /// <returns> SByte </returns>
        public SByte ReadInt8()
        {
            return Convert.ToSByte(ReadUInt8());
        }

        /// <summary>
        ///   Read Uint16 and advances by 2 bytes
        /// </summary>
        /// <param name="endian"> Endian Type </param>
        /// <returns> Uint16 </returns>
        public UInt16 ReadUint16(EndianType endian)
        {
            return BitConverter.ToUInt16(ReadBytes(2, endian), 0);
        }

        /// <summary>
        ///   Read int16 and advances by 2 bytes
        /// </summary>
        /// <param name="endian"> Endian Type </param>
        /// <returns> int16 </returns>
        public short ReadInt16(EndianType endian)
        {
            return BitConverter.ToInt16(ReadBytes(2, endian), 0);
        }

        /// <summary>
        ///   Read Uint16 and advances by 2 bytes
        /// </summary>
        /// <returns> Uint16 </returns>
        public UInt16 ReadUint16()
        {
            return ReadUint16(_xCurrenEndian);
        }

        /// <summary>
        ///   Read int16 and advances by 2 bytes
        /// </summary>
        /// <returns> int16 </returns>
        public short ReadInt16()
        {
            return ReadInt16(_xCurrenEndian);
        }

        /// <summary>
        ///   Read Int24 and advances by 3 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> Signed integer </returns>
        public int ReadInt24(EndianType Endian)
        {
            byte[] buffer = ReadBytes(3, Endian);
            return buffer[2] << 16 | buffer[1] << 8 | buffer[0];
        }

        /// <summary>
        ///   Read UInt24 and advances by 3 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> Unsigned integer </returns>
        public UInt32 ReadUInt24(EndianType Endian)
        {
            byte[] buffer = ReadBytes(3, Endian);
            return Convert.ToUInt32(buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>
        ///   Read Int24 and advances by 3 bytes
        /// </summary>
        /// <returns> Signed integer </returns>
        public int ReadInt24()
        {
            return ReadInt24(_xCurrenEndian);
        }

        /// <summary>
        ///   Read UInt24 and advances by 3 bytes
        /// </summary>
        /// <returns> Unsigned integer </returns>
        public UInt32 ReadUInt24()
        {
            return ReadUInt24(_xCurrenEndian);
        }

        /// <summary>
        ///   Reads int32 and advances by 4 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> Int32 </returns>
        public int ReadInt32(EndianType Endian)
        {
            return BitConverter.ToInt32(ReadBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Reads Uint32 and advances by 4 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> UInt32 </returns>
        public UInt32 ReadUInt32(EndianType Endian)
        {
            return BitConverter.ToUInt32(ReadBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Reads int32 and advances by 4 bytes
        /// </summary>
        /// <returns> Int32 </returns>
        public int ReadInt32()
        {
            return ReadInt32(_xCurrenEndian);
        }

        /// <summary>
        ///   Reads Uint32 and advances by 4 bytes
        /// </summary>
        /// <returns> UInt32 </returns>
        public UInt32 ReadUInt32()
        {
            return ReadUInt32(_xCurrenEndian);
        }

        /// <summary>
        ///   Reads int64 and advances by 8 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> Int64 </returns>
        public long ReadInt64(EndianType Endian)
        {
            return BitConverter.ToInt64(ReadBytes(8, Endian), 0);
        }

        /// <summary>
        ///   Reads Uint64 and advances by 8 bytes
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> UInt64 </returns>
        public UInt64 ReadUInt64(EndianType Endian)
        {
            return BitConverter.ToUInt64(ReadBytes(8, Endian), 0);
        }

        /// <summary>
        ///   Reads int64 and advances by 8 bytes
        /// </summary>
        /// <returns> Int64 </returns>
        public long ReadInt64()
        {
            return BitConverter.ToInt64(ReadBytes(8, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Reads Uint64 and advances by 8 bytes
        /// </summary>
        /// <returns> UInt64 </returns>
        public UInt64 ReadUInt64()
        {
            return BitConverter.ToUInt64(ReadBytes(8, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Reads Filetime and advances by 8 bytes
        /// </summary>
        /// <param name="endian"> Endian type </param>
        /// <returns> Date and time </returns>
        public DateTime ReadFileTime(EndianType endian)
        {
            return DateTime.FromFileTimeUtc((int) (ReadUInt64(endian)));
        }

        /// <summary>
        ///   Reads Filetime and advances by 8 bytes
        /// </summary>
        /// <returns> Date and time </returns>
        public DateTime ReadFileTime()
        {
            return DateTime.FromFileTimeUtc((int) (ReadUInt64(_xCurrenEndian)));
        }

        /// <summary>
        ///   Reads Float Value and advances by 4 bytes
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> Single </returns>
        public float ReadSingle(EndianType Endian)
        {
            return BitConverter.ToSingle(ReadBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Reads Float Value and advances by 4 bytes
        /// </summary>
        /// <returns> Single </returns>
        public float ReadSingle()
        {
            return BitConverter.ToSingle(ReadBytes(4, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Reads String and advances by length
        /// </summary>
        /// <param name="stringtype"> String type to read </param>
        /// <param name="length"> Length for string to read </param>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> String </returns>
        public string ReadString(StringType stringtype, int length, EndianType Endian)
        {
            return BytesToString(ReadBytes(length, Endian), stringtype);
        }


        /// <summary>
        ///   Reads String and advances by length
        /// </summary>
        /// <param name="stringtype"> String type to read </param>
        /// <param name="Length"> Length for string to read </param>
        /// <returns> String </returns>
        public string ReadString(StringType stringtype, int Length)
        {
            return ReadString(stringtype, Length, _xCurrenEndian);
        }

        /// <summary>
        ///   Search for Byte array inside Array
        /// </summary>
        /// <param name="criteria"> Byte array to search </param>
        /// <param name="startPosition"> Position to start searching </param>
        /// <param name="firstStop"> return only first result </param>
        /// <param name="endPosition"> End Position for searching </param>
        /// <param name="results"> list of long with results </param>
        private void SearchBytes(byte[] criteria, int startPosition, bool firstStop, int endPosition, List<long> results)
        {
            try
            {
                int value = Array.IndexOf(_xarray, criteria[0], startPosition);
                ReportProgress(0, _xarray.Length, value, "Searching...");
                while (value > 0 && value < (endPosition - criteria.Length))
                {
                    ReportProgress(0, _xarray.Length, value, "Searching...");
                    if (results.Count > 0 && firstStop)
                    {
                        break;
                    }
                    byte[] segment = new byte[criteria.Length];
                    Buffer.BlockCopy(_xarray, value, segment, 0, criteria.Length);
                    if (CompareBytes(segment, criteria))
                    {
                        if (results.Count >= 65535) break;
                        results.Add(value);
                        value = Array.IndexOf(_xarray, criteria[0], value + criteria.Length);
                    }
                    else
                    {
                        value = Array.IndexOf(_xarray, criteria[0], value + 1);
                    }
                    if (results.Count > 0 && firstStop)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            _xthread.Abort();
        }

        /// <summary>
        ///   Search for ByteArray inside the MainArray
        /// </summary>
        /// <param name="criteria"> Array to search for </param>
        /// <param name="startPosition"> start position to start Looking </param>
        /// <param name="returnOnFirst"> return only at first result </param>
        /// <param name="endposition"> The end Position For Searching </param>
        /// <returns> List With results </returns>
        public List<long> SearchBytes(byte[] criteria, int startPosition = 0, bool returnOnFirst = false,
                                      int endposition = -1)
        {
            try
            {
                if (criteria[0] == 0) throw new Exception("Cannot start criteria with value: 00");
                if (endposition == -1)
                {
                    endposition = _xarray.Length;
                }
                List<long> r = new List<long>();
                _xthread =
                    new Thread(
                        () => this.SearchBytes(criteria, startPosition, returnOnFirst, endposition, r));
                _xthread.Start();
                while (_xthread.ThreadState == ThreadState.Running)
                {
                }
                return r;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///   Search for string Inside Array
        /// </summary>
        /// <param name="value"> StringValue to search </param>
        /// <param name="type"> String Type </param>
        /// <param name="startposition"> Position To start searching </param>
        /// <param name="returnOnFirst"> Return only First result </param>
        /// <param name="endposition"> End position for searching </param>
        /// <returns> List With results </returns>
        public List<long> SearchString(string value, StringType type, int startposition = 0, bool returnOnFirst = false,
                                       int endposition = -1)
        {
            if (endposition == -1)
            {
                endposition = _xarray.Length;
            }
            List<long> r = new List<long>();
            _xthread =
                new Thread(
                    () => this.SearchBytes(StringToBytes(value, type), startposition, returnOnFirst, endposition, r));
            _xthread.Start();
            while (_xthread.ThreadState == ThreadState.Running)
            {
            }
            return r;
        }

        #endregion

        #region peek

        /// <summary>
        ///   PeekBytes
        /// </summary>
        /// <param name="Length"> length to Peek </param>
        /// <param name="Endian"> endian type </param>
        /// <returns> Bytes </returns>
        public byte[] PeekBytes(int Length, EndianType Endian)
        {
            if (_xdisposed)
            {
                throw (new Exception("Class Has been disposed !"));
            }
            byte[] x = new byte[Length];
            Array.Copy(_xarray, Convert.ToInt32(_xposition), x, 0, Length);
            if (Endian == EndianType.Big)
            {
                Array.Reverse(x);
            }
            return x;
        }

        /// <summary>
        ///   Peek Bytes
        /// </summary>
        /// <param name="length"> length to peek </param>
        /// <returns> Bytes </returns>
        public byte[] PeekBytes(int length)
        {
            return PeekBytes(length, _xCurrenEndian);
        }

        /// <summary>
        ///   Peek single Byte
        /// </summary>
        /// <returns> Byte </returns>
        public byte PeekUInt8()
        {
            return _xarray[Convert.ToInt32(_xposition)];
        }

        /// <summary>
        ///   Peek SByte
        /// </summary>
        /// <returns> SByte </returns>
        public SByte PeekInt8()
        {
            return Convert.ToSByte(PeekUInt8());
        }

        /// <summary>
        ///   Peek Uint16
        /// </summary>
        /// <param name="endian"> Endian Type </param>
        /// <returns> Uint16 </returns>
        public UInt16 PeekUint16(EndianType endian)
        {
            return BitConverter.ToUInt16(PeekBytes(2, endian), 0);
        }

        /// <summary>
        ///   Peek int16
        /// </summary>
        /// <param name="endian"> Endian Type </param>
        /// <returns> int16 </returns>
        public short PeekInt16(EndianType endian)
        {
            return BitConverter.ToInt16(PeekBytes(2, endian), 0);
        }

        /// <summary>
        ///   Peek Uint16
        /// </summary>
        /// <returns> Uint16 </returns>
        public UInt16 PeekUint16()
        {
            return PeekUint16(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek int16
        /// </summary>
        /// <returns> int16 </returns>
        public short PeekInt16()
        {
            return PeekInt16(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek int24
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> int24 </returns>
        public int PeekInt24(EndianType Endian)
        {
            byte[] buffer = PeekBytes(3, Endian);
            return buffer[2] << 16 | buffer[1] << 8 | buffer[0];
        }

        /// <summary>
        ///   Peek Uint24
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> Uint24 </returns>
        public UInt32 PeekUInt24(EndianType Endian)
        {
            byte[] buffer = PeekBytes(3, Endian);
            return Convert.ToUInt32(buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>
        ///   Peek int24
        /// </summary>
        /// <returns> int24 </returns>
        public int PeekInt24()
        {
            return PeekInt24(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek Uint24
        /// </summary>
        /// <returns> Uint24 </returns>
        public UInt32 PeekUInt24()
        {
            return PeekUInt24(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek int32
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> int32 </returns>
        public int PeekInt32(EndianType Endian)
        {
            return BitConverter.ToInt32(PeekBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Peek Uint32
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> Uint32 </returns>
        public UInt32 PeekUInt32(EndianType Endian)
        {
            return BitConverter.ToUInt32(PeekBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Peek int32
        /// </summary>
        /// <returns> int32 </returns>
        public int PeekInt32()
        {
            return PeekInt32(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek Uint32
        /// </summary>
        /// <returns> Uint32 </returns>
        public UInt32 PeekUInt32()
        {
            return PeekUInt32(_xCurrenEndian);
        }

        /// <summary>
        ///   Peek int32
        /// </summary>
        /// <returns> int32 </returns>
        public long PeekInt64(EndianType Endian)
        {
            return BitConverter.ToInt64(PeekBytes(8, Endian), 0);
        }

        /// <summary>
        ///   Peek Uint64
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> Uint64 </returns>
        public UInt64 PeekUInt64(EndianType Endian)
        {
            return BitConverter.ToUInt64(PeekBytes(8, Endian), 0);
        }

        /// <summary>
        ///   Peek int64
        /// </summary>
        /// <returns> int64 </returns>
        public long PeekInt64()
        {
            return BitConverter.ToInt64(PeekBytes(8, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Peek Uint64
        /// </summary>
        /// <returns> Uint64 </returns>
        public UInt64 PeekUInt64()
        {
            return BitConverter.ToUInt64(PeekBytes(8, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Peek Filetime
        /// </summary>
        /// <param name="Endian"> Endian type </param>
        /// <returns> Date And Time </returns>
        public DateTime PeekFileTime(EndianType Endian)
        {
            return DateTime.FromFileTimeUtc((int) (PeekUInt64(Endian)));
        }

        /// <summary>
        ///   Peek Filetime
        /// </summary>
        /// <returns> Date And Time </returns>
        public DateTime PeekFileTime()
        {
            return DateTime.FromFileTimeUtc((int) (PeekUInt64(_xCurrenEndian)));
        }

        /// <summary>
        ///   Peek Float Value
        /// </summary>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> Float Value </returns>
        public float PeekSingle(EndianType Endian)
        {
            return BitConverter.ToSingle(PeekBytes(4, Endian), 0);
        }

        /// <summary>
        ///   Peek Float Value
        /// </summary>
        /// <returns> Float Value </returns>
        public float PeekSingle()
        {
            return BitConverter.ToSingle(PeekBytes(4, _xCurrenEndian), 0);
        }

        /// <summary>
        ///   Peek String
        /// </summary>
        /// <param name="Stringtype"> String Type </param>
        /// <param name="Length"> Length to peek </param>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> String </returns>
        public string PeekString(StringType Stringtype, int Length, EndianType Endian)
        {
            return BytesToString(PeekBytes(Length, Endian), Stringtype);
        }

        /// <summary>
        ///   Peek String
        /// </summary>
        /// <param name="Stringtype"> String Type </param>
        /// <param name="Length"> Length to peek </param>
        /// <returns> String </returns>
        public string PeekString(StringType Stringtype, int Length)
        {
            return PeekString(Stringtype, Length, _xCurrenEndian);
        }

        #endregion

        #region Writer

        /// <summary>
        ///   Writes Bytes to array
        /// </summary>
        /// <param name="buffer"> Byte array to write and advances by length </param>
        /// <param name="SourceIndex"> Index of SourceArray to insert </param>
        /// <param name="endian"> Endian Type </param>
        /// <param name="Destinationindex"> Destination Index </param>
        /// <param name="length"> Length to write </param>
        public void WriteBytes(byte[] buffer, int SourceIndex, EndianType endian, int Destinationindex, int length)
        {
            if (this._xdisposed)
            {
                throw (new Exception("Class has Been Disposed"));
            }
            if (_xarray == null || _xarray.Length == 0)
            {
                throw (new Exception("Array Is Empty !"));
            }
            _xLast = _xposition;
            if (endian == EndianType.Big)
            {
                Array.Reverse(buffer);
            }
            Array.Copy(buffer, SourceIndex, _xarray, Destinationindex, length);
            _xposition = (ulong) ((Convert.ToInt32(_xposition)) + buffer.Length);
        }

        /// <summary>
        ///   Writes Bytes to array and advances by length
        /// </summary>
        /// <param name="buffer"> Byte array to write </param>
        /// <param name="endian"> Endian Type </param>
        /// <param name="Destinationindex"> Destination Index </param>
        public void WriteBytes(byte[] buffer, EndianType endian, int Destinationindex)
        {
            WriteBytes(buffer, 0, endian, Destinationindex, buffer.Length);
        }

        /// <summary>
        ///   Writes Bytes to array and advances by length
        /// </summary>
        /// <param name="buffer"> Byte array to write </param>
        /// <param name="endian"> Endian Type </param>
        public void WriteBytes(byte[] buffer, EndianType endian)
        {
            WriteBytes(buffer, 0, endian, Convert.ToInt32(_xposition), buffer.Length);
        }

        /// <summary>
        ///   Writes Bytes to array and advances by length
        /// </summary>
        /// <param name="buffer"> Byte array to write </param>
        public void WriteBytes(byte[] buffer)
        {
            WriteBytes(buffer, 0, _xCurrenEndian, Convert.ToInt32(_xposition), buffer.Length);
        }

        /// <summary>
        ///   Write Boolean Value and advances by 1 byte
        /// </summary>
        /// <param name="Value"> Boolean Value to write </param>
        public void WriteBoolean(bool Value)
        {
            byte[] s = new[] {Convert.ToByte(Value)};
            WriteBytes(s);
        }

        /// <summary>
        ///   Write single Byte and advances by 1 byte
        /// </summary>
        /// <param name="value"> Value to write </param>
        public void WriteInt8(SByte value)
        {
            _xarray[Convert.ToInt32(_xposition)] = (byte) value;
            _xposition = _xposition + 1;
        }

        /// <summary>
        ///   Write Byte and advances by 1 byte
        /// </summary>
        /// ///
        /// <param name="value"> Value to write </param>
        public void WriteUInt8(byte value)
        {
            _xarray[Convert.ToInt32(_xposition)] = value;
            _xposition = _xposition + 1;
        }

        /// <summary>
        ///   Write Int16 and advances by 2 bytes
        /// </summary>
        /// ///
        /// <param name="Value"> Value to write </param>
        public void WriteInt16(short Value)
        {
            WriteBytes(BitConverter.GetBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Write UInt16 and advances by 2 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        public void WriteUInt16(UInt16 Value)
        {
            WriteBytes(BitConverter.GetBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Write Int16 and advances by 2 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteInt16(short Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(Value), Endian);
        }

        /// <summary>
        ///   Write UInt16 and advances by 2 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteUInt16(UInt16 Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(Value), Endian);
        }

        /// <summary>
        ///   Write Int24 and advances by 3 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        public void WriteInt24(int Value)
        {
            WriteBytes(Int24ToBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Write UInt24 and advances by 3 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        public void WriteUInt24(UInt32 Value)
        {
            WriteBytes(Int24ToBytes(Convert.ToInt32(Value)), _xCurrenEndian);
        }

        /// <summary>
        ///   Write Int24 and advances by 3 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteInt24(int Value, EndianType Endian)
        {
            WriteBytes(Int24ToBytes(Value), Endian);
        }

        /// <summary>
        ///   Write UInt24 and advances by 3 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteUInt24(UInt32 Value, EndianType Endian)
        {
            WriteBytes(Int24ToBytes(Convert.ToInt32(Value)), Endian);
        }

        /// <summary>
        ///   Write Int32 and advances by 4 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        public void WriteInt32(int Value)
        {
            WriteBytes(BitConverter.GetBytes((short) Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Write UInt32 and advances by 4 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        public void WriteUInt32(UInt32 Value)
        {
            WriteBytes(BitConverter.GetBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Writes Int32 and advances by 4 bytes
        /// </summary>
        /// <param name="Value"> Value to write </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteInt32(int Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes((short) Value), Endian);
        }

        /// <summary>
        ///   Writes Uint32 Value and advances 4 bytes
        /// </summary>
        /// <param name="Value"> Uint32 Value </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteUInt32(UInt32 Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(Value), Endian);
        }

        /// <summary>
        ///   Writes int64 Value and advances 8 bytes
        /// </summary>
        /// <param name="Value"> int64 Value </param>
        public void WriteInt64(long Value)
        {
            WriteBytes(BitConverter.GetBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Writes Uint64 Value and advances 8 bytes
        /// </summary>
        /// <param name="Value"> Uint64 Value </param>
        public void WriteUInt64(UInt64 Value)
        {
            WriteBytes(BitConverter.GetBytes(Value), _xCurrenEndian);
        }

        /// <summary>
        ///   Writes int64 Value and advances 8 bytes
        /// </summary>
        /// <param name="Value"> int64 Value </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteInt64(long Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(Value), Endian);
        }

        /// <summary>
        ///   Writes Uint64 Value and advances 8 bytes
        /// </summary>
        /// <param name="Value"> Uint64 Value </param>
        /// <param name="Endian"> Endian Type </param>
        public void WriteUInt64(UInt64 Value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(Value), Endian);
        }

        /// <summary>
        ///   Writes date/Time and Advances 8 bytes
        /// </summary>
        /// <param name="time"> the time as Date/Time for writing </param>
        public void WriteFileTime(DateTime time)
        {
            WriteInt64((int) (Convert.ToUInt64(time)));
        }

        /// <summary>
        ///   Writes date/Time and advances 8 bytes
        /// </summary>
        /// <param name="time"> the time as Date/Time for writing </param>
        /// <param name="Endian"> Endian type </param>
        public void WriteFileTime(DateTime time, EndianType Endian)
        {
            WriteInt64((int) (Convert.ToUInt64(time)), Endian);
        }

        /// <summary>
        ///   Wrute Float Value and advances 4 bytes
        /// </summary>
        /// <param name="value"> Float Value to write </param>
        public void WriteSingle(float value)
        {
            WriteBytes(BitConverter.GetBytes(value), _xCurrenEndian);
        }

        /// <summary>
        ///   Write Float Value
        /// </summary>
        /// <param name="value"> Float Value </param>
        /// <param name="Endian"> Endian type </param>
        public void WriteSingle(float value, EndianType Endian)
        {
            WriteBytes(BitConverter.GetBytes(value), Endian);
        }

        /// <summary>
        ///   Write String to Array
        /// </summary>
        /// <param name="value"> Value to write </param>
        /// <param name="Type"> String Type </param>
        public void WriteString(string value, StringType Type)
        {
            WriteBytes(StringToBytes(value, Type), _xCurrenEndian);
        }

        /// <summary>
        ///   Write String to Array
        /// </summary>
        /// <param name="value"> value to write </param>
        /// <param name="Type"> StringType </param>
        /// <param name="Endian"> Endiantype </param>
        public void WriteString(string value, StringType Type, EndianType Endian)
        {
            WriteBytes(StringToBytes(value, Type), Endian);
        }

        /// <summary>
        ///   Write Nulled Bytes
        /// </summary>
        /// <param name="count"> Count for the bytes to write </param>
        public void WriteNull(int count)
        {
            byte[] buffer = new byte[count];
            WriteBytes(buffer);
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Get/Set Position
        /// </summary>
        public UInt64 Position
        {
            get { return _xposition; }
            set
            {
                if (Convert.ToInt32(value) == -1 || Convert.ToInt32(value) > _xarray.Length)
                {
                    throw (new Exception("Position can not be negative or beyond array"));
                }
                _xposition = value;
            }
        }

        /// <summary>
        ///   Get Last Position
        /// </summary>
        public UInt64 LastPosition
        {
            get { return _xLast; }
        }

        /// <summary>
        ///   Get/Set Current Array
        /// </summary>
        public byte[] CurrentArray
        {
            get { return _xarray; }
            set
            {
                if (value.Equals(null) || value.Length == 0)
                {
                    throw (new Exception("Array cannot be empty"));
                }
                _xarray = value;
            }
        }

        /// <summary>
        ///   Get/Set CurrentEndian
        /// </summary>
        public EndianType CurrentEndian
        {
            get { return _xCurrenEndian; }
            set { _xCurrenEndian = value; }
        }

        /// <summary>
        ///   Returns Array length
        /// </summary>
        public long Length
        {
            get { return _xarray.Length; }
        }

        #endregion

        #region Some Functions

        /// <summary>
        ///   Compare two arrays
        /// </summary>
        /// <param name="First"> first Array to compare </param>
        /// <param name="Second"> second Array to Compare </param>
        /// <returns> True if Match </returns>
        public bool CompareBytes(byte[] First, byte[] Second)
        {
            if (First == null || Second == null)
            {
                return false;
            }
            if (First.Length != Second.Length)
            {
                return false;
            }
            for (int i = 0; i <= First.Length - 1; i++)
            {
                if (First[i] != Second[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///   Convert int24 Value to Bytes
        /// </summary>
        /// <param name="value"> Value to Convert </param>
        /// <returns> </returns>
        public byte[] Int24ToBytes(int value)
        {
            if (value < -8388608 || value > 8388607)
            {
                throw (new Exception("Invalid value"));
            }
            byte[] buffer = new byte[3];
            buffer[2] = (byte) (value & 0xFF);
            buffer[1] = (byte) ((byte) (value >> 8) & 0xFF);
            buffer[0] = (byte) ((byte) (value >> 16) & 0xFF);
            return buffer;
        }

        /// <summary>
        ///   returns to last Offset
        /// </summary>
        public void Jumpback()
        {
            _xposition = _xLast;
        }

        /// <summary>
        ///   Jumb Back using Count
        /// </summary>
        /// <param name="count"> The count to jumBack </param>
        public void Jumpback(int count)
        {
            _xposition = (ulong) ((Convert.ToInt32(_xposition)) - count);
        }

        /// <summary>
        ///   Insert Bytes into Array
        /// </summary>
        /// <param name="Source"> Bytes to Insert </param>
        /// <param name="SourceIndex"> Source startIndex </param>
        /// <param name="DestinationIndex"> Destination Start Index </param>
        /// <param name="Length"> Length to Insert </param>
        public void InsertBytes(byte[] Source, UInt32 SourceIndex, UInt32 DestinationIndex, UInt32 Length)
        {
            Array.Resize(ref _xarray, (int) (_xarray.Length + Length));
            Array.Copy(_xarray, DestinationIndex, _xarray, (int) (DestinationIndex + Length),
                       Convert.ToInt32((_xarray.Length - DestinationIndex) - Length));
            Array.Copy(Source, SourceIndex, _xarray, DestinationIndex, Length);
        }

        /// <summary>
        ///   Insert Bytes into Array
        /// </summary>
        /// <param name="Source"> Bytes to Insert </param>
        /// <param name="DestinationIndex"> The Position for adding the bytes </param>
        public void InsertBytes(byte[] Source, UInt32 DestinationIndex)
        {
            InsertBytes(Source, 0, DestinationIndex, (uint) Source.Length);
        }

        ///<summary>
        ///  Delete Bytes from Array
        ///</summary>
        ///<param name="Index"> The Index For starting Deletion </param>
        ///<param name="Length"> The length to delete </param>
        public void DeleteBytes(UInt32 Index, UInt32 Length)
        {
            Array.Copy(_xarray, (int) (Index + Length), _xarray, Index, _xarray.Length - (Index + Length));
            object temp_object = _xarray;
            byte[] temp_byte = (byte[]) (temp_object);
            Array.Resize(ref temp_byte, (int) (_xarray.Length - Length));
        }

        /// <summary>
        ///   Converts String to Byte Array
        /// </summary>
        /// <param name="Value"> Value to Convert </param>
        /// <param name="stringtype"> The string type </param>
        /// <returns> Bytes from Converted String </returns>
        public byte[] StringToBytes(string Value, StringType stringtype)
        {
            switch (stringtype)
            {
                case StringType.Ascii:
                    return Encoding.ASCII.GetBytes(Value);
                case StringType.HexString:
                    if (!IsValidHex(Value))
                    {
                        throw (new Exception("Invalid Hex Input !"));
                    }
                    byte[] Buffer = new byte[(Convert.ToInt32(Value.Length/2))];
                    for (int i = 0; i <= Convert.ToInt32((Value.Length/2) - 1); i++)
                    {
                        Buffer[i] = Convert.ToByte("&H" + Value.Substring(i*2, 2));
                    }
                    return Buffer;
                case StringType.Unicode:
                    return Encoding.BigEndianUnicode.GetBytes(Value);
                default:
                    return null;
            }
        }

        /// <summary>
        ///   Checks if Hexvalue is Valid
        /// </summary>
        /// <param name="value"> Value to Validate </param>
        /// <returns> True if valid </returns>
        public bool IsValidHex(string value)
        {
            if (value.Length%2 != 0)
            {
                return false;
            }
            return
                new Regex("^[A-Fa-f0-9]*$",
                          RegexOptions.IgnoreCase).IsMatch
                    (value);
        }

        /// <summary>
        ///   Converts Bytes to String
        /// </summary>
        /// <param name="buffer"> Bytes to Convert </param>
        /// <param name="stringtype"> The Type to Convert the string to </param>
        /// <returns> Converted String </returns>
        public string BytesToString(byte[] buffer, StringType stringtype)
        {
            switch (stringtype)
            {
                case StringType.Ascii:
                    return Convert.ToString(Encoding.ASCII.GetChars(buffer));
                case StringType.Unicode:
                    return Encoding.BigEndianUnicode.GetChars(buffer).ToString();
                case StringType.HexString:
                    return BitConverter.ToString(buffer).Replace("-", "");
                default:
                    return null;
            }
        }

        /// <summary>
        ///   Replace ByteArray
        /// </summary>
        /// <param name="Source"> The bytes for replace </param>
        /// <param name="ArrayToReplace"> The bytes to Replace </param>
        /// <param name="ReplaceFirstOnly"> Replace all Instances if True </param>
        /// <param name="Endian"> Endian Type </param>
        /// <returns> The Total replaced Instances </returns>
        public int ReplaceBytes(byte[] Source, byte[] ArrayToReplace, EndianType Endian, bool ReplaceFirstOnly = false)
        {
            List<long> r = SearchBytes(Source, 0, ReplaceFirstOnly);
            if (r.Count > 0)
            {
                if (ReplaceFirstOnly)
                {
                    WriteBytes(Source, 0, Endian, Convert.ToInt32(r[0]), Source.Length);

                    return 1;
                }
                else
                {
                    foreach (long value in r)
                    {
                        WriteBytes(Source, 0, Endian, (int) value, Source.Length);
                    }
                    return r.Count;
                }
            }
            return 0;
        }

        /// <summary>
        ///   Replace ByteArray
        /// </summary>
        /// <param name="Source"> The bytes for replace </param>
        /// <param name="ArrayToReplace"> The bytes to Replace </param>
        /// <param name="ReplaceFirstOnly"> Replace all Instances if True </param>
        /// <returns> The Total replaced Instances </returns>
        public int ReplaceBytes(byte[] Source, byte[] ArrayToReplace, bool ReplaceFirstOnly = false)
        {
            return ReplaceBytes(Source, ArrayToReplace, _xCurrenEndian, ReplaceFirstOnly);
        }

        #endregion
    }
}