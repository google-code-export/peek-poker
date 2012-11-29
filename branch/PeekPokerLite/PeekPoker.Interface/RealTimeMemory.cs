using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PeekPoker.Interface
{
    //===============================================================
    // Thank to :Mojobojo for most of the codes                    //
    //           Nathan for xbdm                                   //
    //           All 360Haven Crew & Members - Sharing Is Caring   //
    //==============================================================
    /// <summary>
    ///     Xbox 360 Real Time Memory Access Class using xbdm
    ///     NB: Large dump speed depends on the version of xbdm you have
    /// </summary>
    public class RealTimeMemory
    {
        private bool _connected;
        private string _ipAddress;
        private bool _memexValidConnection;
        private RWStream _readWriterDump;
        private uint _startDumpLength;
        private uint _startDumpOffset;
        private bool _streamDumpFlag;
        private TcpClient _tcp;

        #region Constructor

        /// <summary>RealTimeMemory constructor Example: Default start dump = "0xC0000000" and length = "0x1FFFFFFF"</summary>
        /// <param name="ipAddress">The IP address</param>
        public RealTimeMemory(string ipAddress)
        {
            _ipAddress = ipAddress;
            _connected = false;
        }

        /// <summary>RealTimeMemory constructor Example: Default start dump = "0xC0000000" and length = "0x1FFFFFFF"</summary>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="startDumpOffset">The start dump address</param>
        /// <param name="startDumpLength">The dump length</param>
        public RealTimeMemory(string ipAddress, string startDumpOffset, string startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            //fullmemory dump by default
            _startDumpOffset = Convert(startDumpOffset);
            _startDumpLength = Convert(startDumpLength);
        }

        /// <summary>RealTimeMemory constructor Example: Default start dump = 0xC0000000 and length = 0x1FFFFFFF</summary>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="startDumpOffset">The start dump address</param>
        /// <param name="startDumpLength">The dump length</param>
        public RealTimeMemory(string ipAddress, uint startDumpOffset, uint startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            //fullmemory dump by default
            _startDumpOffset = startDumpOffset;
            _startDumpLength = startDumpLength;
        }

        #endregion

        #region Methods

        /// <summary>Connect to the  using port 730 using the given ip address</summary>
        /// <returns>True if connection was successful and False if not</returns>
        public bool Connect()
        {
            try
            {
                if (_ipAddress.Length < 5)
                    throw new Exception("Invalid IP");
                if (_connected) return true; //If you are already connected then return
                _tcp = new TcpClient(); //New Instance of TCP
                //Connect to the specified host using port 730
                _tcp.Connect(_ipAddress, 730);
                byte[] response = new byte[1024];
                _tcp.Client.Receive(response);
                string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
                //validate connection
                _connected = reponseString.Substring(0, 3) == "201";
                return _connected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Connect to the </summary>
        /// <param name="port">The port to use Example: 730</param>
        /// <returns>True if connection was successful and False if not</returns>
        public bool Connect(int port)
        {
            try
            {
                if (_ipAddress.Length < 5)
                    throw new Exception("Invalid IP");
                if (_connected) return true; //If you are already connected then return
                _tcp = new TcpClient(); //New Istance of TCP
                //Connect to the specified host using port 730
                _tcp.Connect(_ipAddress, port);
                byte[] response = new byte[1024];
                _tcp.Client.Receive(response);
                string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
                //validate connection
                _connected = reponseString.Substring(0, 3) == "201";
                return _connected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory addess to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(string memoryAddress, string value)
        {
            Poke(Convert(memoryAddress), value);
        }

        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory addess to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(uint memoryAddress, string value)
        {
            if (!Hex.IsHex(value))
                throw new Exception("Not a valid Hex String!");
            if (!Connect()) return; //Call function - If not connected return
            try
            {
                WriteMemory(memoryAddress, value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
            }
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="memoryAddress">The memory addess to Peek Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(string memoryAddress, string peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, Convert(memoryAddress), ConvertSigned(peekSize));
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="memoryAddress">The memory addess to Peek Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(string memoryAddress, int peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, Convert(memoryAddress), peekSize);
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="memoryAddress">The memory addess to Peek Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(uint memoryAddress, int peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, memoryAddress, peekSize);
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(string startDumpAddress, string dumpLength, string memoryAddress, string peekSize)
        {
            return Peek(Convert(startDumpAddress), Convert(dumpLength), Convert(memoryAddress), ConvertSigned(peekSize));
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the hex string of the value</returns>
        public string Peek(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            uint total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return

            if (!_streamDumpFlag)
                if (!GetMeMex(startDumpAddress, dumpLength))
                    return null; //call function - If not connected or if somethign wrong return

            try
            {
                if (!_streamDumpFlag)
                {
                    RWStream readWriter = new RWStream();
                    byte[] data = new byte[1026]; //byte chuncks

                    //Writing each byte chuncks========
                    //No need to mess with it :D
                    for (int i = 0; i < dumpLength/1024; i++)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, 1024);
                    }
                    //Write whatever is left
                    int extra = (int) (dumpLength%1024);
                    if (extra > 0)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, extra);
                    }
                    readWriter.Flush();
                    //===================================
                    //===================================
                    readWriter.Position = total;
                    byte[] value = readWriter.ReadBytes(peekSize);
                    readWriter.Close();
                    return Hex.ToHexString(value);
                }

                _readWriterDump.Position = total;
                byte[] value2 = _readWriterDump.ReadBytes(peekSize);
                return Hex.ToHexString(value2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the byte[] of the value</returns>
        public byte[] PeekBytes(string startDumpAddress, string dumpLength, string memoryAddress, string peekSize)
        {
            return PeekBytes(Convert(startDumpAddress), Convert(dumpLength), Convert(memoryAddress),
                             ConvertSigned(peekSize));
        }

        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress">The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength">The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress">The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize">The byte size to peek Example: "0x4" or "4"</param>
        /// <returns>Return the byte[] of the value</returns>
        public byte[] PeekBytes(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            uint total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return

            if (!_streamDumpFlag)
                if (!GetMeMex(startDumpAddress, dumpLength))
                    return null; //call function - If not connected or if somethign wrong return

            try
            {
                if (!_streamDumpFlag)
                {
                    RWStream readWriter = new RWStream();
                    byte[] data = new byte[1026]; //byte chuncks

                    //Writing each byte chuncks========
                    //No need to mess with it :D
                    for (int i = 0; i < dumpLength/1024; i++)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, 1024);
                    }
                    //Write whatever is left
                    int extra = (int) (dumpLength%1024);
                    if (extra > 0)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, extra);
                    }
                    readWriter.Flush();
                    //===================================
                    //===================================
                    readWriter.Position = total;
                    byte[] value = readWriter.ReadBytes(peekSize);
                    readWriter.Close();
                    return value;
                }

                _readWriterDump.Position = total;
                byte[] value2 = _readWriterDump.ReadBytes(peekSize);
                return value2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Find the address of a pointer from the start dump offset</summary>
        /// <param name="pointer">The hex string of the pointer Example: 821122114455EEFF000000</param>
        /// <returns>Returns and array of the address or all address where the pointer was found</returns>
        public string[] FindHexOffset(string pointer)
        {
            if (!Hex.IsHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            if (!_streamDumpFlag)
                if (!GetMeMex()) return null; //call function - If not connected or if somethign wrong return

            try
            {
                if (!_streamDumpFlag)
                {
                    //LENGTH or Size = Length of the dump
                    uint size = _startDumpLength;
                    RWStream readWriter = new RWStream();
                    byte[] data = new byte[1026]; //byte chuncks

                    //Writing each byte chuncks========
                    //No need to mess with it :D
                    for (int i = 0; i < size/1024; i++)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, 1024);
                    }
                    //Write whatever is left
                    int extra = (int) (size%1024);
                    if (extra > 0)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, extra);
                    }
                    readWriter.Flush();
                    //===================================
                    //===================================
                    readWriter.Position = 0;
                    long[] values = readWriter.SearchHexString(pointer, false);
                    List<string> addresses = new List<string>(values.Length);
                    foreach (long value in values)
                    {
                        addresses.Add(Hex.ToHexString(ToByteArray.UInt32(_startDumpOffset + (uint) value)));
                    }
                    readWriter.Close();
                    return addresses.ToArray();
                }

                _readWriterDump.Position = 0;
                long[] values2 = _readWriterDump.SearchHexString(pointer, false);
                List<string> addresses2 = new List<string>(values2.Length);
                foreach (long value in values2)
                {
                    addresses2.Add(Hex.ToHexString(ToByteArray.UInt32(_startDumpOffset + (uint) value)));
                }
                return addresses2.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Find the address of a pointer from the start dump offset</summary>
        /// <param name="pointer">The hex string of the pointer Example: 821122114455EEFF000000</param>
        /// <returns>Returns and array of the address or all address where the pointer was found</returns>
        public uint[] FindUIntOffset(string pointer)
        {
            if (!Hex.IsHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            if (!_streamDumpFlag)
                if (!GetMeMex()) return null; //call function - If not connected or if somethign wrong return

            try
            {
                if (!_streamDumpFlag)
                {
                    //LENGTH or Szie = Length of the dump
                    uint size = _startDumpLength;
                    RWStream readWriter = new RWStream();
                    byte[] data = new byte[1026]; //byte chuncks

                    //Writing each byte chuncks========
                    //No need to mess with it :D
                    for (int i = 0; i < size/1024; i++)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, 1024);
                    }
                    //Write whatever is left
                    int extra = (int) (size%1024);
                    if (extra > 0)
                    {
                        _tcp.Client.Receive(data);
                        readWriter.WriteBytes(data, 2, extra);
                    }
                    readWriter.Flush();
                    //===================================
                    //===================================
                    readWriter.Position = 0;
                    long[] values = readWriter.SearchHexString(pointer, false);
                    List<uint> addresses = new List<uint>(values.Length);
                    foreach (long value in values)
                    {
                        addresses.Add(_startDumpOffset + (uint) value);
                    }
                    readWriter.Close();
                    return addresses.ToArray();
                }

                _readWriterDump.Position = 0;
                long[] values2 = _readWriterDump.SearchHexString(pointer, false);
                List<uint> addresses2 = new List<uint>(values2.Length);
                foreach (long value in values2)
                {
                    addresses2.Add(_startDumpOffset + (uint) value);
                }
                return addresses2.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Send a freeze command to the xbox</summary>
        public void StopCommand()
        {
            try
            {
                if (!Connect()) return; //Call function - If not connected return
                byte[] response = new byte[1024];
                //Send a stop command to the xbox - freeze
                _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("STOP\r\n")));
                _tcp.Client.Receive(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Send a start command to the xbox</summary>
        public void StartCommand()
        {
            try
            {
                if (!Connect()) return; //Call function - If not connected return
                byte[] response = new byte[1024];
                //Send a start command to the xbox - resume
                _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("GO\r\n")));
                _tcp.Client.Receive(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        /// <summary>Dump  memory into a temporary file which will be used to peek and poke</summary>
        public void StreamDump()
        {
            if (!Connect()) return; //Call function - If not connected return
            if (!GetMeMex()) return; //call function - If not connected or if somethign wrong return

            try
            {
                _streamDumpFlag = true;
                //Read & Writer Stream you can always use your own filestream
                _readWriterDump = new RWStream();
                byte[] data = new byte[1026]; //byte chuncks

                //Writing each byte chuncks========
                //No need to mess with it :D
                for (int i = 0; i < _startDumpLength/1024; i++)
                {
                    _tcp.Client.Receive(data);
                    _readWriterDump.WriteBytes(data, 2, 1024);
                }
                //Write whatever is left
                int extra = (int) (_startDumpLength%1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    _readWriterDump.WriteBytes(data, 2, extra);
                }
                _readWriterDump.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }

        #region Private

        private void WriteMemory(uint address, string data)
        {
            // Send the setmem command
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("SETMEM ADDR=0x{0} DATA={1}\r\n", address.ToString("X2"), data)));

            // Check to see our response
            byte[] packet = new byte[1026];
            _tcp.Client.Receive(packet);

            //========================Broken=============================================================
            //if (Encoding.ASCII.GetString(packet).Replace("\0", "").Substring(0, 11) == "0 bytes set")
            //    throw new Exception("A problem occurred while writing bytes. 0 bytes set");
            //============================================================================================
        }

        private bool GetMeMex()
        {
            if (_memexValidConnection) return true;
            //ADDR=0xDA1D0000 - The start offset in the physical memory I want the dump to start
            //LENGTH = Length of the dump
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("GETMEMEX ADDR={0} LENGTH={1}\r\n", _startDumpOffset,
                                                      _startDumpLength)));
            byte[] response = new byte[1024];
            _tcp.Client.Receive(response);
            string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
            //validate connection
            _memexValidConnection = reponseString.Substring(0, 3) == "203";
            return _memexValidConnection;
        }

        private bool GetMeMex(uint startDump, uint length)
        {
            if (_memexValidConnection) return true;
            //ADDR=0xDA1D0000 - The start offset in the physical memory I want the dump to start
            //LENGTH = Length of the dump
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("GETMEMEX ADDR={0} LENGTH={1}\r\n", startDump, length)));
            byte[] response = new byte[1024];
            _tcp.Client.Receive(response);
            string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
            //validate connection
            _memexValidConnection = reponseString.Substring(0, 3) == "203";
            return _memexValidConnection;
        }

        private uint Convert(string value)
        {
            if (value.Contains("0x"))
                return System.Convert.ToUInt32(value.Substring(2), 16);
            return System.Convert.ToUInt32(value);
        }

        private int ConvertSigned(string value)
        {
            if (value.Contains("0x"))
                return System.Convert.ToInt32(value.Substring(2), 16);
            return System.Convert.ToInt32(value);
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>Set or Get the IPAddress</summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>Set or Get the start dump offset</summary>
        public uint DumpOffset
        {
            get { return _startDumpOffset; }
            set { _startDumpOffset = value; }
        }

        /// <summary>Set or Get the dump length</summary>
        public uint DumpLength
        {
            get { return _startDumpLength; }
            set { _startDumpLength = value; }
        }

        /// <summary>Checks if you are using the stream dump</summary>
        public bool StreamDumpFlag
        {
            get { return _streamDumpFlag; }
            set { _streamDumpFlag = value; }
        }

        /// <summary>Close the Stream Dump if used</summary>
        public void CloseStreamDump()
        {
            _readWriterDump.Close();
        }

        #endregion
    }
}