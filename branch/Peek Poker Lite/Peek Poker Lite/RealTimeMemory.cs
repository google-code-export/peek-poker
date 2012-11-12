using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Peek_Poker_Lite
{
    public class RealTimeMemory
    {
        #region Eventhandlers/DelegateHandlers
        public event UpdateProgressBarHandler ReportProgress;
        #endregion

        private readonly string _ipAddress;
        private bool _connected;
        private bool _memexValidConnection;
        private TcpClient _tcp;
        private RWStream _readWriter;

        public uint DumpOffset { get; set; }
        public uint DumpLength { get; set; }
        public bool StopSearch { get; set; }

        #region Constructor
        /// <summary>RealTimeMemory constructor Example: Default start dump = 0xC0000000 and length = 0x1FFFFFFF</summary>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="startDumpOffset">The start dump address</param>
        /// <param name="startDumpLength">The dump length</param>
        public RealTimeMemory(string ipAddress, uint startDumpOffset, uint startDumpLength)
        {
            _ipAddress = ipAddress;
            _connected = false;
            DumpOffset = startDumpOffset;
            DumpLength = startDumpLength;
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
                if (_connected) 
                    return true;                //If you are already connected then return
                _tcp = new TcpClient();         //New Instance of TCP
                _tcp.Connect(_ipAddress, 730);  //Connect to the specified host using port 730
                byte[] response = new byte[1024];
                _tcp.Client.Receive(response);
                string reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
                _connected = reponseString.Substring(0, 3) == "201";    //validate connection
                return _connected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Peek
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
        /// <summary>Peek into the Memory</summary>
        /// <param name="startDumpAddress"> The Hex offset to start dump Example:0xC0000000 </param>
        /// <param name="dumpLength"> The Length or size of dump Example:0xFFFFFF </param>
        /// <param name="memoryAddress"> The memory address to peek Example:0xC5352525 </param>
        /// <param name="peekSize"> The byte size to peek Example: "0x4" or "4" </param>
        /// <returns> Return the hex string of the value </returns>
        public string Peek(uint startDumpAddress, uint dumpLength, uint memoryAddress, int peekSize)
        {
            var total = (memoryAddress - startDumpAddress);
            if (memoryAddress > (startDumpAddress + dumpLength) || memoryAddress < startDumpAddress)
                throw new Exception("Memory Address Out of Bounds");

            if (!Connect()) return null; //Call function - If not connected return
            if (!GetMeMex(startDumpAddress, dumpLength)) return null; //call function - If not connected or if somethign wrong return

            var readWriter = new RWStream();
            try
            {
                var data = new byte[1026];
                for (var i = 0; i < dumpLength / 1024; i++)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, 1024);
                }
                var extra = (int)(dumpLength % 1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    readWriter.WriteBytes(data, 2, extra);
                }
                readWriter.Flush();
                readWriter.Position = total;
                var value = readWriter.ReadBytes(peekSize);
                return Functions.ByteArrayToHexString(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readWriter.Close(true);
                _tcp.Close();
                _connected = false;
                _memexValidConnection = false;
            }
        }
        #endregion

        #region Poke
        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(string memoryAddress, string value)
        {
            Poke(Functions.StringToUInt(memoryAddress), value);
        }
        /// <summary>Poke the Memory</summary>
        /// <param name="memoryAddress">The memory address to Poke Example:0xCEADEADE - Uses *.FindOffset</param>
        /// <param name="value">The value to poke Example:000032FF (hex string)</param>
        public void Poke(uint memoryAddress, string value)
        {
            if (!Functions.IsValidHex(value))
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
        #endregion

        #region Search Hex
        public List<long> FindHexOffset(string pointer)
        {
            if (pointer == null)
                throw new Exception("Empty Search string!");
            if (!Functions.IsValidHex(pointer))
                throw new Exception(string.Format("{0} is not a valid Hex string.", pointer));
            if (!Connect()) return null; //Call function - If not connected return
            if (!GetMeMex()) return null; //call function - If not connected or if something wrong return

            try
            {
                //LENGTH or Size = Length of the dump
                uint size = DumpLength;
                _readWriter = new RWStream();
                _readWriter.ReportProgress += new UpdateProgressBarHandler(ReportProgress);
                byte[] data = new byte[1026];

                for (int i = 0; i < size / 1024; i++)
                {
                    if (StopSearch) return null;
                    _tcp.Client.Receive(data);
                    _readWriter.WriteBytes(data, 2, 1024);
                    ReportProgress(0, (int)(size / 1024), (i + 1), "Dumping Memory...");
                }
                int extra = (int)(size % 1024);
                if (extra > 0)
                {
                    _tcp.Client.Receive(data);
                    _readWriter.WriteBytes(data, 2, extra);
                }
                _readWriter.Flush();
                _readWriter.Position = 0;
                List<long> values = _readWriter.SearchBytes(Functions.HexToBytes(pointer));
                return values;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _readWriter.Close(true);
                _tcp.Close(); //close connection
                _connected = false;
                _memexValidConnection = false;
            }
        }
        #endregion

        #region Get MeMex
        private bool GetMeMex()
        {
            return GetMeMex(DumpOffset, DumpLength);
        }
        private bool GetMeMex(uint startDump, uint length)
        {
            if (_memexValidConnection) return true;
            _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("GETMEMEX ADDR={0} LENGTH={1}\r\n", startDump, length)));
            var response = new byte[1024];
            _tcp.Client.Receive(response);
            var reponseString = Encoding.ASCII.GetString(response).Replace("\0", "");
            //validate connection
            _memexValidConnection = reponseString.Substring(0, 3) == "203";
            return _memexValidConnection;
        }
        #endregion

        #region Private Functions
        public void WriteMemory(uint address, string data)
        {
            _tcp.Client.Send(Encoding.ASCII.GetBytes(string.Format("SETMEM ADDR=0x{0} DATA={1}\r\n", address.ToString("X2"), data)));
            // Check to see our response
            var packet = new byte[1026];
            _tcp.Client.Receive(packet);
        }
        #endregion
    }
}
