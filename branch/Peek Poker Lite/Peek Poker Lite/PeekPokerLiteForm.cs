using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;
using Microsoft.Win32;

namespace Peek_Poker_Lite
{
    public delegate void UpdateProgressBarHandler(int min, int max, int value, string text);

    public partial class PeekPokerLiteForm : Form
    {
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt");
        private RealTimeMemory _rtm;
        private BindingList<SearchResults> _bindingList;


        public PeekPokerLiteForm()
        {
            InitializeComponent();
        }

        private void PeekPokerLiteFormLoad(object sender, EventArgs e)
        {
            try
            {
                //feature suggested by Fairchild
                Regex ipRegex = new Regex(@"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$");
                //IP Check for XDK Name
                string xboxname = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "XboxName", "NotFound");
                //feature suggested by Fairchild -Introduced regex to accommodate the possibility of a name instead of ip, which is very possible.
                Match validIp = ipRegex.Match(xboxname); // For regex to check if there's a match
                if (validIp.Success)
                    SetText(ipAddressTextBox,xboxname);
                if (File.Exists(_filepath))
                    SetText(ipAddressTextBox,File.ReadAllText(_filepath));
                else 
                    SetLogText("XboxIP.txt was not detected, will be created upon connection.");
            }
            catch (Exception)
            {
                SetLogText("Error: XenonSDK not installed XboxName is null! You can still use PeekPoker");
            }
        }
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            Thread oThead = new Thread(Connect);
            oThead.Start();
        }
        private void PeekButtonClick(object sender, EventArgs e)
        {
            Thread oThread = new Thread(Peek);
            oThread.Start();
        }
        private void PokeButtonClick(object sender, EventArgs e)
        {
            Thread oThread = new Thread(Poke);
            oThread.Start();
        }
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            Thread oThread = new Thread(NewPoke);
            oThread.Start();
        }
        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            var oThread = new Thread(SearchRange);
            oThread.Start();
        }
        private void StopSearchButtonClick(object sender, EventArgs e)
        {
            SetLogText("Searching was stopped!");
            _rtm.StopSearch = true;
        }
        private void ResultRefreshButtonClick(object sender, EventArgs e)
        {
            if (_bindingList.Count > 0)
            {
                Thread thread = new Thread(RefreshResultList);
                thread.Start();
            }
            else
            {
                ShowMessageBox("Can not refresh! \r\n Result list empty!!", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Dump Buttons
        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0xC0000000");
            dumpLengthTextBox.Text = string.Format("0x1FFF0FFF");
        }
        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0x82000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0x40000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }
        #endregion

        #region Safe Thread
        #region Get
        private string GetText(Control control)
        {
            //recursion
            var returnVal = "";
            if (control.InvokeRequired) control.Invoke((MethodInvoker)
                  delegate { returnVal = GetText(control); });
            else
                return control.Text;
            return returnVal;
        }
        #endregion

        #region Set
        private void SetLogText(string value)
        {
            if (logTextBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetLogText(value)));
            else
            {
                string m = DateTime.Now.ToString("HH:mm:ss tt") + " " + value + Environment.NewLine;
                logTextBox.Text += m;
                logTextBox.Select(logTextBox.Text.Length, 0); // set the cursor to end of textbox
                logTextBox.ScrollToCaret(); // scroll down to the cursor position
            }
        }
        private void SetMainStatusStripLabel(ToolStripStatusLabel label, string value)
        {
            if (mainStatusStrip.InvokeRequired)
                Invoke((MethodInvoker)(() => SetMainStatusStripLabel(label, value)));
            else
                label.Text = value;
        }
        private void SetText(Control control, string value)
        {
            if (control.InvokeRequired)
                Invoke((MethodInvoker)(() => SetText(control, value)));
            else
                control.Text = value;
        }
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }
        private void ResultGridUpdate(BindingList<SearchResults> list)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(() => ResultGridUpdate(list)));
            else
            {
                resultGrid.DataSource = list;
                resultGrid.Refresh();
            }
        }
        #endregion

        #region Hex Box
        private void HexBoxRefresh(DynamicByteProvider buffer)
        {
            if (hexBox.InvokeRequired) hexBox.Invoke((MethodInvoker)
                  delegate { HexBoxRefresh(buffer); });
            else
            {
                hexBox.ByteProvider = buffer;
                hexBox.Refresh();
            }
        }
        private uint HexBoxLength()
        {
            //recursion
            uint returnVal = 0;
            if (hexBox.InvokeRequired) hexBox.Invoke((MethodInvoker)
                  delegate { returnVal = HexBoxLength(); });
            else
                return (uint)hexBox.ByteProvider.Length / 2;
            return returnVal;
        }
        private DynamicByteProvider HexBoxBuffer()
        {
            //recursion
            DynamicByteProvider returnVal = null;
            if (hexBox.InvokeRequired) hexBox.Invoke((MethodInvoker)
                  delegate { returnVal = HexBoxBuffer(); });
            else
                return (DynamicByteProvider) hexBox.ByteProvider;
            return returnVal;
        }
        #endregion

        #region Progress bar
        private void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (mainStatusStrip.InvokeRequired)
            {
                mainStatusStrip.Invoke((MethodInvoker)(() => UpdateProgressbar(min, max, value, text)));
            }
            else
            {
                if (StatusProgressBar.ProgressBar != null)
                {
                    StatusProgressBar.ProgressBar.Maximum = max;
                    StatusProgressBar.ProgressBar.Minimum = min;
                    StatusProgressBar.ProgressBar.Value = value;
                }
                statusStripLabel.Text = text;
            }


        }
        #endregion

        #region SearchList
        private void GridRowColours(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)delegate { GridRowColours(value); });
            else
                resultGrid.Rows[value - 1].DefaultCellStyle.ForeColor = Color.Red;
        }
        private void ResultGridClean()
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(ResultGridClean));
            else
                resultGrid.Rows.Clear();
        }
        private void RefreshResultList()
        {
            try
            {
                int value = 0;
                foreach (SearchResults item in _bindingList)
                {
                    UpdateProgressbar(0, _bindingList.Count, value, "Refreshing...");
                    value++;
                    string length = (item.Value.Length / 2).ToString("X");
                    string retvalue = _rtm.Peek("0x" + item.Offset, length, "0x" + item.Offset, length);

                    if (item.Value == retvalue) continue;//if value hasn't change continue for each loop

                    GridRowColours(value);
                    item.Value = retvalue;
                }
                SetLogText("Search List was refreshed!");
                ResultGridUpdate(_bindingList);
            }
            catch (Exception ex)
            {
                SetLogText("Search List Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateProgressbar(0, 100, 0, "idle");
                Thread.CurrentThread.Abort();
            }
        }
        #endregion
        #endregion

        #region Thread Functions
        private void Connect()
        {
            try
            {
                SetLogText("Connecting to: " + GetText(ipAddressTextBox));
                _rtm = new RealTimeMemory(GetText(ipAddressTextBox), 0, 0); //initialize real time memory
                if (!_rtm.Connect())
                {
                    SetLogText("Connecting to " + GetText(ipAddressTextBox) + " Failed.");
                    throw new Exception("Connection Failed!");
                }

                SetMainStatusStripLabel(statusStripLabel,String.Format("Connected"));
                ShowMessageBox(String.Format("Connected"), String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetLogText("Connected to " + GetText(ipAddressTextBox));
                SetText(connectButton,String.Format("Re-Connect"));
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void Peek()
        {
            try
            {
                if (string.IsNullOrEmpty(GetText(peekLengthTextBox)) || Functions.StringToUInt(GetText(peekLengthTextBox)) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(GetText(peekPokeAddressTextBox)) || Functions.StringToUInt(GetText(peekPokeAddressTextBox)) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte
                byte[] retValue = Functions.HexToBytes(_rtm.Peek(GetText(peekPokeAddressTextBox), GetText(peekLengthTextBox),GetText(peekPokeAddressTextBox), GetText(peekLengthTextBox)));
                DynamicByteProvider buffer = new DynamicByteProvider(retValue)
                {
                    IsWriteByte = true
                };
                HexBoxRefresh(buffer);
                SetLogText("Peeked Address: " + GetText(peekPokeAddressTextBox) + " Length: " + GetText(peekLengthTextBox));
                SetText(peekPokeFeedBackTextBox, string.Format("Peek Success!"));
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void Poke()
        {
            try
            {
                uint dumplength = HexBoxLength();
                if (dumplength > 240)
                    throw new Exception("Poke Length has to be under 240 bytes.");
                _rtm.DumpOffset = Functions.StringToUInt(GetText(peekPokeAddressTextBox));//Set the dump offset
                _rtm.DumpLength = HexBoxLength();

                DynamicByteProvider buffer = HexBoxBuffer();
                SetLogText("Poked Address: " + GetText(peekPokeAddressTextBox) + " Length: " + _rtm.DumpLength);
                _rtm.Poke(GetText(peekPokeAddressTextBox), Functions.ByteArrayToHexString(buffer.Bytes.ToArray()));
                SetText(peekPokeFeedBackTextBox, string.Format("Poke Success!"));
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void NewPoke()
        {
            try
            {
                SetText(peekPokeAddressTextBox, null);
                SetText(peekLengthTextBox, null);
                HexBoxRefresh(null);
            }
            catch(Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void SearchRange()
        {
            try
            {
                SetLogText("Search Offset: " + GetText(startRangeAddressTextBox) + " Search Length: " + GetText(endRangeAddressTextBox));
                _rtm.DumpOffset = Functions.StringToUInt(GetText(startRangeAddressTextBox));
                _rtm.DumpLength = Functions.StringToUInt(GetText(endRangeAddressTextBox));

                ResultGridClean();

                List<long> results = _rtm.FindHexOffset(GetText(searchRangeValueTextBox));//pointer
                UpdateProgressbar(0, 100, 0);

                if (results.Count < 1)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; 
                }
                _bindingList = new BindingList<SearchResults>();
                int x = 0;
                foreach (long searchResults in results)
                {
                    SearchResults item = new SearchResults
                    {
                        Offset = searchResults.ToString(),
                        Value = GetText(searchRangeValueTextBox),
                        Id = x.ToString()
                    };
                    _bindingList.Add(item);
                    x++;
                }
                ResultGridUpdate(_bindingList);
            }
            catch (Exception e)
            {
                SetLogText("Search Range Error: " + e.Message);
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        #endregion

        #region Private Functions
        // These will automatically add "0x" to an offset if it hasn't been added already - 8Ball
        private void FixTheAddresses(object sender, EventArgs e)
        {
            if (!peekPokeAddressTextBox.Text.StartsWith("0x")) //Peek Address Box, Formatting Check.
            {
                //PeekPokeAddress
                if (!peekPokeAddressTextBox.Text.Equals("")) //Empty Check
                    peekPokeAddressTextBox.Text = (string.Format("0x" + peekPokeAddressTextBox.Text)); //Formatting
            }
            if (peekLengthTextBox.Text.StartsWith("0x")) // Checks if peek length is hex value or not based on 0x
            { 
                //Peek length
                string result = (peekLengthTextBox.Text.ToUpper().Substring(2));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(peekLengthTextBox.Text.ToUpper(), "^[A-Z]$")) //Checks if hex, based on uppercase alphabet presence.
            {
                //Peek length pt2
                string result = (peekLengthTextBox.Text.ToUpper());
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (peekLengthTextBox.Text.StartsWith("h")) //Checks if hex, based on starting with h.
            { 
                //Peek length pt3
                string result = (peekLengthTextBox.Text.ToUpper().Substring(1));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            if (!startRangeAddressTextBox.Text.StartsWith("0x"))
            {
                //Range Start
                if (!startRangeAddressTextBox.Text.Equals(""))
                    startRangeAddressTextBox.Text = (string.Format("0x" + startRangeAddressTextBox.Text));
            }
            //Range End
            if (endRangeAddressTextBox.Text.StartsWith("0x")) return; 
            if (!endRangeAddressTextBox.Text.Equals(""))
                endRangeAddressTextBox.Text = (string.Format("0x" + endRangeAddressTextBox.Text));

            //Dump Start
            if (dumpStartOffsetTextBox.Text.StartsWith("0x")) return;
            if (!dumpStartOffsetTextBox.Text.Equals(""))
                dumpStartOffsetTextBox.Text = (string.Format("0x" + dumpStartOffsetTextBox.Text));

            //Dump Length
            if (dumpLengthTextBox.Text.StartsWith("0x")) return;
            if (!dumpLengthTextBox.Text.Equals(""))
                dumpLengthTextBox.Text = (string.Format("0x" + dumpLengthTextBox.Text));
        }
        #endregion
    }
}
