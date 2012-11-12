using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Peek_Poker_Lite
{
    /// <summary>Contains function/s that deals with I/O reading and writing of Data</summary>
    public class RWStream
    {
        #region Eventhandlers/DelegateHandlers
        public event UpdateProgressBarHandler ReportProgress;
        #endregion

        private bool _accessed;
        private BinaryReader _bReader;
        private BinaryWriter _bWriter;
        private string _fileName;
        private Stream _fStream;
        private readonly bool _isBigEndian;
        private readonly byte[] _buffer;

        #region RwStream Constructors
        /// <summary>Makes a temporary file Stream</summary>
        public RWStream()
        {
            try
            {
                _fileName = Path.GetTempPath() + Guid.NewGuid() + ".ISOLib";
                _fStream = new FileStream(_fileName,FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                _bReader = new BinaryReader(_fStream);
                _bWriter = new BinaryWriter(_fStream);
                _isBigEndian = true;
                _accessed = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public RWStream(string filename)
        {
            try
            {
                _fileName = filename;
                _fStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                _bReader = new BinaryReader(_fStream);
                _bWriter = new BinaryWriter(_fStream);
                _isBigEndian = true;
                _accessed = true;
                _buffer = ReadBytes((int)Length);
                Position = 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Methods

        /// <summary>Clears buffer by Flushing and Closes theI/O Stream</summary>
        /// <param name="delete">Delete file </param>
        public void Close(bool delete)
        {
            try
            {
                if (_accessed)
                {
                    Flush();
                    _bWriter.Close();
                    _bWriter = null;
                    _bReader.Close();
                    _bReader = null;
                    _fStream.Close();
                    _fStream = null;
                    _accessed = false;
                    if (_fileName != null)
                    {
                        if(delete)File.Delete(_fileName);
                        _fileName = null;
                    }
                        Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        /// <summary>Release all resources used by reader and writer stream</summary>
        private void Dispose()
        {
            try
            {
                if (_accessed)
                {
                    if (_bReader != null)
                    {
                        _bReader.BaseStream.Dispose();
                    }
                    if (_bWriter != null)
                    {
                        _bWriter.BaseStream.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        /// <summary>Clears buffer for this stream and any unbuffered data will be written</summary>
        public void Flush()
        {
            try
            {
                _bReader.BaseStream.Flush();
                _bWriter.BaseStream.Flush();
                _fStream.Flush();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion
        
        #region Reader
        /// <summary>Reads a set size of bytes</summary>
        /// <param name="length">The byte array length</param>
        /// <returns>Byte Array</returns>
        public byte[] ReadBytes(int length)
        {
            return ReadBytes(length, _isBigEndian);
        }

        /// <summary>Reads a set size of bytes</summary>
        /// <param name="length">The byte array length</param>
        /// <param name="isBigEndien">Specific if read is in Big Endian Type</param>
        private byte[] ReadBytes(int length, bool isBigEndien)
        {
            try
            {
                if (Position == Length)
                    throw new Exception("Cannot move position past file size");
                if (length == 0)
                    return new byte[0];
                var buffer = new byte[length];
                _fStream.Read(buffer, 0, length);
                if(!isBigEndien)
                    Array.Reverse(buffer);
                return buffer;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
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
                int value = Array.IndexOf(_buffer, criteria[0], startPosition);
                ReportProgress(0, _buffer.Length, value, "Searching...");
                while (value > 0 && value < (endPosition - criteria.Length))
                {
                    ReportProgress(0, _buffer.Length, value, "Searching...");
                    if (results.Count > 0 && firstStop) break;
                    byte[] segment = new byte[criteria.Length];
                    Buffer.BlockCopy(_buffer, value, segment, 0, criteria.Length);
                    if (Functions.CompareByteArray(segment, criteria))
                    {
                        if (results.Count >= 65535) break;
                        results.Add(value);
                        value = Array.IndexOf(_buffer, criteria[0], value + criteria.Length);
                    }
                    else
                    {
                        value = Array.IndexOf(_buffer, criteria[0], value + 1);
                    }
                    if (results.Count > 0 && firstStop)break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }


        /// <summary>
        ///   Search for ByteArray inside the MainArray
        /// </summary>
        /// <param name="criteria"> Array to search for </param>
        /// <param name="startPosition"> start position to start Looking </param>
        /// <param name="returnOnFirst"> return only at first result </param>
        /// <param name="endposition"> The end Position For Searching </param>
        /// <returns> List With results </returns>
        public List<long> SearchBytes(byte[] criteria, int startPosition = 0, bool returnOnFirst = false,int endposition = -1)
        {
            try
            {
                if (criteria[0] == 0) throw new Exception("Cannot start criteria with value: 00");
                if (endposition == -1)
                {
                    endposition = _buffer.Length;
                }
                List<long> result = new List<long>();
                Thread othread =new Thread(
                        () => this.SearchBytes(criteria, startPosition, returnOnFirst, endposition, result));
                othread.Start();
                while (othread.ThreadState == ThreadState.Running){}
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Writer
        /// <summary>Writes a region of a byte array to the current stream</summary>
        /// <param name="buffer">A byte array containing the data to write</param>
        /// <param name="index">The starting point to start writing</param>
        /// <param name="count">The amount of bytes to write</param>
        public void WriteBytes(byte[] buffer, int index, int count)
        {
            try
            {
                WriteBytes(buffer, index, count, _isBigEndian);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Writes a region of a byte array to the current stream</summary>
        /// <param name="buffer">A byte array containing the data to write</param>
        /// <param name="index">The starting point to start writing</param>
        /// <param name="count">The amount of bytes to write</param>
        /// <param name="isBigEndian">If the Endian type is Big</param>
        private void WriteBytes(byte[] buffer, int index, int count, bool isBigEndian)
        {
            try
            {
                if (!isBigEndian)
                    Array.Reverse(buffer);
                _bWriter.Write(buffer, index, count);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion
        
        #region Properties
        /// <summary>Get/Set if current initialization length</summary>
        private long Length
        {
            get
            {
                return _fStream.Length;
            }
        }
        /// <summary>Get/Set if current initialization position</summary>
        public long Position
        {
            private get
            {
                return _fStream.Position;
            }
            set
            {
                try
                {
                    if ((value != _fStream.Position) || (value <= _fStream.Length))
                    {
                        _fStream.Position = value;
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
        }
        public bool Accessed
        {
            get { return _accessed; }
        }
        #endregion
    }
}

