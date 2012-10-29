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
    ///   Xbox 360 Real Time Memory Access Class using xbdm
    ///   NB: Large dump speed depends on the version of xbdm you have
    /// </summary>
    public class RealTimeMemory
    {
        private bool _connected;
        private string _ipAddress;
        private bool _memexValidConnection;
        private uint _startDumpLength;
        private uint _startDumpOffset;
        private bool _stopSearch;
        private TcpClient _tcp;

        #region Constructor

        /// <summary>
        ///   RealTimeMemory constructor Example: Default start dump = "0xC0000000" and length = "0x1FFFFFFF"
        /// </summary>
        /// <param name="ipAddress"> The IP address </param>
        public RealTimeMemory(string ipAddress)
        {
            _ipAddress = ipAddress;
            _connected = false;
        }

        /// <summary>
        ///   RealTimeMemory constructor Example: Default start dump = "0xC0000000" and length = "0x1FFFFFFF"
        /// </summary>
        /// <param name="ipAddress"> The IP address </param>
        /// <param name="startDumpOffset"> The start dump address </param>
        /// <param name="startDumpLength"> The dump length </param>
        public RealTimeMemory(string ipAddress, string startDumpOffset, string startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            //full memory dump by default
            _startDumpOffset = Functions.StringToUInt(startDumpOffset);
            _startDumpLength = Functions.StringToUInt(startDumpLength);
        }

        /// <summary>
        ///   RealTimeMemory constructor Example: Default start dump = 0xC0000000 and length = 0x1FFFFFFF
        /// </summary>
        /// <param name="ipAddress"> The IP address </param>
        /// <param name="startDumpOffset"> The start dump address </param>
        /// <param name="startDumpLength"> The dump length </param>
        public RealTimeMemory(string ipAddress, uint startDumpOffset, uint startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            //full memory dump by default
            _startDumpOffset = startDumpOffset;
            _startDumpLength = startDumpLength;
        }

        #endregion

        #region Methods

        #region Connect

        /// <summary>
        ///   Connect to the  using port 730 using the given ip address
        /// </summary>
        /// <returns> True if connection was successful and False if not </returns>
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

        /// <summary>
        ///   Connect to the
        /// </summary>
        /// <param name="port"> The port to use Example: 730 </param>
        /// <returns> True if connection was successful and False if not </returns>
        public bool Connect(int port)
        {
            try
            {
                if (_ipAddress.Length < 5)
                    throw new Exception("Invalid IP");
                if (_connected) return true; //If you are already connected then return
                _tcp = new TcpClient(); //New Instance of TCP
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

        #endregion

        #region Poke

        /// <summary>
        ///   Poke the Memory
        /// </summary>
        /// <param name="memoryAddress"> The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset </param>
        /// <param name="value"> The value to poke Example:000032FF (hex string) </param>
        public void Poke(string memoryAddress, string value)
        {
            Poke(Functions.StringToUInt(memoryAddress), value);
        }

        /// <summary>
        ///   Poke the Memory
        /// </summary>
        /// <param name="memoryAddress"> The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset </param>
        /// <param name="value"> The value to poke Example:000032FF (hex string) </param>
        public void Poke(uint memoryAddress, string value)
        {
            if (!Functions.IsValidHex(value))
                throw new Exception("Not a valid Hex String!");
            if (!Connect()) return; //Call function - If not connected return
            try
            {
                WriteMemory(memoryAddress, value); //Items 1 flame grenade
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

        #endregion

        #region Peek

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="memoryAddress"> The memory address to Peek Example:0xCEADEADE - Uses *.FindOffset </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(string memoryAddress, string peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, Functions.StringToUInt(memoryAddress),
                        Functions.StringToInt(peekSize));
        }

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="memoryAddress"> The memory address to Peek Example:0xCEADEADE - Uses *.FindOffset </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(string memoryAddress, int peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, Functions.StringToUInt(memoryAddress), peekSize);
        }

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="memoryAddress"> The memory address to Peek Example:0xCEADEADE - Uses *.FindOffset </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(uint memoryAddress, int peekSize)
        {
            return Peek(_startDumpOffset, _startDumpLength, memoryAddress, peekSize);
        }

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="startDumpAddress"> The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength"> The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress"> The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(string startDumpAddress, string dumpLength, string memoryAddress, string peekSize)
        {
            return Peek(Functions.StringToUInt(startDumpAddress), Functions.StringToUInt(dumpLength),
                        Functions.StringToUInt(memoryAddress), Functions.StringToInt(peekSize));
        }

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="startDumpAddress"> The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength"> The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress"> The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            uint total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return

            try
            {
                RWStream readWriter = new RWStream();
                byte[] data = new byte[1026]; //byte

                //Writing each byte========
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
                return Functions.ByteArrayToHexString(value);
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

        #endregion

        #region Peek byte

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="startDumpAddress"> The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength"> The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress"> The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the byte[] of the value </returns>
        public byte[] PeekBytes(string startDumpAddress, string dumpLength, string memoryAddress, string peekSize)
        {
            return PeekBytes(Functions.StringToUInt(startDumpAddress), Functions.StringToUInt(dumpLength),
                             Functions.StringToUInt(memoryAddress), Functions.StringToInt(peekSize));
        }

        /// <summary>
        ///   Peek into the Memory
        /// </summary>
        /// <param name="startDumpAddress"> The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength"> The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress"> The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the byte[] of the value </returns>
        public byte[] PeekBytes(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            uint total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return

            if (!GetMeMex(startDumpAddress, dumpLength))
                return null; //call function - If not connected or if something wrong return

            try
            {
                RWStream readWriter = new RWStream();
                byte[] data = new byte[1026]; //byte 

                //Writing each byte ========
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
                readWriter.Position = total;
                byte[] value = readWriter.ReadBytes(peekSize);
                readWriter.Close();
                return value;
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

        #endregion

        #region search

        /// <summary>
        ///   Find the address of a pointer from the start dump offset
        /// </summary>
        /// <param name="pointer"> The hex string of the pointer Example: 821122114455EEFF000000 </param>
        /// <returns> Returns and array of the address or all address where the pointer was found </returns>
        public string[] FindHexOffset(string pointer)
        {
            if (!Functions.IsValidHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            try
            {
                //LENGTH or size = Length of the dump
                uint size = _startDumpLength;
                RWStream readWriter = new RWStream();
                byte[] data = new byte[1026]; //byte 

                //Writing each byte ========
                for (int i = 0; i < size/1024; i++)
                {
                    if (_stopSearch) return null;
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (size/1024), (i + 1), "Dumping Memory...");
                }
                //Write whatever is left
                int extra = (int) (size%1024);
                if (extra > 0)
                {
                    if (_stopSearch) return null;
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, extra);
                }
                readWriter.Flush();
                //===================================
                if (_stopSearch) return null;
                readWriter.Position = 0;
                byte[] buffer = readWriter.ReadBytes((int) readWriter.Length);
                readWriter.Close();
                ArrayIo ai = new ArrayIo(buffer);
                byte[] searchBuffer = Functions.HexToBytes(pointer);
                List<long> results = ai.SearchBytes(searchBuffer);
                List<string> addresses = new List<string>(results.Count);
                foreach (long value in results)
                {
                    addresses.Add(
                        Functions.ByteArrayToHexString(
                            Functions.UInt32ToBytesArray(_startDumpOffset + (uint) value)));
                }
                return addresses.ToArray();
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
                ReportProgress(0, 100, 0, "Idle");
            }
        }

        /// <summary>
        ///   Find the address of a pointer from the start dump offset
        /// </summary>
        /// <param name="pointer"> The hex string of the pointer Example: 821122114455EEFF000000 </param>
        /// <returns> Returns and array of the address or all address where the pointer was found </returns>
        public uint[] FindUIntOffset(string pointer)
        {
            if (!Functions.IsValidHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            try
            {
                //LENGTH or size = Length of the dump
                uint size = _startDumpLength;
                RWStream readWriter = new RWStream();
                byte[] data = new byte[1026]; //byte 

                //Writing each byte ========
                for (int i = 0; i < size/1024; i++)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (size/1024), (i + 1), "Dumping Memory...");
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
                byte[] buffer = readWriter.ReadBytes((int) readWriter.Length);
                readWriter.Close();
                ArrayIo ai = new ArrayIo(buffer);
                byte[] searchBuffer = Functions.HexToBytes(pointer);
                List<long> results = ai.SearchBytes(searchBuffer);
                List<uint> addresses = new List<uint>(results.Count);
                foreach (long value in results)
                {
                    addresses.Add(_startDumpOffset + (uint) value);
                }

                return addresses.ToArray();
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
                ReportProgress(0, 100, 0, "Idle");
            }
        }

        #endregion

        /// <summary>
        ///   Send a freeze command to the xbox
        /// </summary>
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

        /// <summary>
        ///   Send a start command to the xbox
        /// </summary>
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

        /// <summary>
        ///   Dump memory to a specified file path
        /// </summary>
        /// <param name="filename"> The file path </param>
        /// <param name="startDumpAddress"> The starting offset/address </param>
        /// <param name="dumpLength"> The dump length </param>
        public void Dump(string filename, string startDumpAddress, string dumpLength)
        {
            Dump(filename, Functions.StringToUInt(startDumpAddress), Functions.StringToUInt(dumpLength));
        }

        /// <summary>
        ///   Dump memory to a specified file path
        /// </summary>
        /// <param name="filename"> The file path </param>
        /// <param name="startDumpAddress"> The starting offset/address </param>
        /// <param name="dumpLength"> The dump length </param>
        public void Dump(string filename, uint startDumpAddress, uint dumpLength)
        {
            if (!Connect()) return; //Call function - If not connected return
            if (!GetMeMex(startDumpAddress, dumpLength))
                return; //call function - If not connected or if something wrong return

            RWStream readWriter = new RWStream(filename);
            try
            {
                byte[] data = new byte[1026]; //byte 
                //Writing each byte ========
                for (int i = 0; i < dumpLength/1024; i++)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int) (dumpLength/1024), (i + 1), "Dumping Memory...");
                }
                //Write whatever is left
                int extra = (int) (dumpLength%1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, extra);
                }
                readWriter.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readWriter.Close();
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
                ReportProgress(0, 100, 0, "Idle");
            }
        }

        #region Private

        private void WriteMemory(uint address, string data)
        {
            // Send the set memory command
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("SETMEM ADDR=0x{0} DATA={1}\r\n", address.ToString("X2"), data)));

            // Check to see our response
            byte[] packet = new byte[1026];
            _tcp.Client.Receive(packet);
        }

        private bool GetMeMex()
        {
            return GetMeMex(_startDumpOffset, _startDumpLength);
        }

        private bool GetMeMex(uint startDump, uint length)
        {
            if (_memexValidConnection) return true;
            _tcp.Client.Send(
                Encoding.ASCII.GetBytes(string.Format("GETMEMEX ADDR={0} LENGTH={1}\r\n", startDump, length)));
            byte[] response = new byte[1024];
            _tcp.Client.Receive(response);
            string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
            //validate connection
            _memexValidConnection = reponseString.Substring(0, 3) == "203";
            return _memexValidConnection;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        ///   Set or Get the IPAddress
        /// </summary>
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        ///   Set or Get the start dump offset
        /// </summary>
        public uint DumpOffset
        {
            get { return _startDumpOffset; }
            set { _startDumpOffset = value; }
        }

        /// <summary>
        ///   Set or Get the dump length
        /// </summary>
        public uint DumpLength
        {
            get { return _startDumpLength; }
            set { _startDumpLength = value; }
        }

        public bool StopSearch
        {
            set { _stopSearch = value; }
        }

        #endregion

        public event UpdateProgressBarHandler ReportProgress;
    }
}