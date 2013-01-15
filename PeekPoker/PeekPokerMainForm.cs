using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Win32;
using PeekPoker.Interface;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Be.Windows.Forms;

namespace PeekPoker
{
    public partial class PeekPokerMainForm : Form
    {
        #region global varibales

        private RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        private uint _searchRangeDumpLength;
        private string _dumpFilePath;
        private BindingList<SearchResults> _searchResult = new BindingList<SearchResults>();

        #endregion

        public PeekPokerMainForm()
        {
            InitializeComponent();
            SetLogText("Welcome to Peek Poker.");
            SetLogText("Please make sure you have the xbdm xbox 360 plug-in loaded.");
            SetLogText("All the information provided in this application is for educational purposes only. Neither the application nor host are in any way responsible for any misuse of the information.");
            LoadPlugins();
            resultGrid.DataSource = _searchResult;
        }
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();//Immediately stop the process
        }
        private void Form1Load(Object sender, EventArgs e)
        {
            try
            {
                //Set correct max. min values for the numeric fields
                ChangeNumericMaxMin();

                //feature suggested by Fairchild
                var ipRegex = new Regex(@"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"); //IP Check for XDK Name
                var xboxname = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "XboxName", "NotFound"); //feature suggested by fairchild -Introduced regex to accomodate the possiblity of a name instead of ip, which is very possible.
                var validIp = ipRegex.Match(xboxname); // For regex to check if there's a match
                if (validIp.Success) ipAddressTextBox.Text = xboxname;// If the registry contains a valid IP, send to textbox.
                if (File.Exists(_filepath)) ipAddressTextBox.Text = File.ReadAllText(_filepath);
                else SetLogText("XboxIP.txt was not detected, will be created upon connection.");
            }
            catch (Exception)
            {
                SetLogText("Error: XenonSDK not installed XboxName is null! You can still use PeekPoker");
            } 
        }
        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Peek Poker - Open Source Memory Editor :Revision 8"));
            stringBuilder.AppendLine(string.Format("================By===================="));
            stringBuilder.AppendLine(string.Format("Cybersam, 8Ball, PureIso & Jappi88"));
            stringBuilder.AppendLine(string.Format("=============Special Thanks To========="));
            stringBuilder.AppendLine(string.Format("optantic (tester), Mojobojo (codes), Natelx (xbdm), Be.Windows.Forms.HexBox.dll"));
            stringBuilder.AppendLine(string.Format("Fairchild (codes), actualmanx (tester), ioritree (tester), 360Haven"));
            ShowMessageBox(stringBuilder.ToString(), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ConverterClearButtonClick(object sender, EventArgs e)
        {
            integer8CalculatorTextBox.Clear();
            integer16CalculatorTextBox.Clear();
            integer32CalculatorTextBox.Clear();
            floatCalculatorTextBox.Clear();
            hexCalculatorTextBox.Clear();
            SetLogText("Conversion Texts Cleared");
        }

        

        #region button clicks
        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Connect);
        }

        //When you click on the peek button
        private void PeekButtonClick(object sender, EventArgs e)
        {
            AutoComplete();//run function
            ThreadPool.QueueUserWorkItem(Peek);
        }

        //When you click on the poke button
        private void PokeButtonClick(object sender, EventArgs e)
        {
            AutoComplete(); //run function
            ThreadPool.QueueUserWorkItem(Poke);
        }

        //When you click on the www.360haven.com
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            SetLogText("URL connection to www.360haven.com");
            Process.Start("www.360haven.com");
        }

        //When you click on the new button
        private void NewPeekButtonClick(object sender, EventArgs e)
        {
            NewPeek();
            SetLogText("New Peek Initiated!");
        }

        //search Button
        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _searchRangeDumpLength = (Functions.Convert(endRangeAddressTextBox.Text) - Functions.Convert(startRangeAddressTextBox.Text));
                var oThread = new Thread(SearchRange);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //When you click on an item on the search range result list view - Search Range tab
        private void SearchRangeResultListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return; //if its not a left click return
            peekPokeAddressTextBox.Text = string.Format("0x" + resultGrid.Rows[resultGrid.SelectedRows[0].Index].Cells[1].Value);
        }
        
        private void ResultGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = (DataGridCell)sender;
            if (resultGrid.Rows[cell.RowNumber].Cells[2].Value != null)
                resultGrid.Rows[cell.RowNumber].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }

        // Refresh results
        private void ResultRefreshClick(object sender, EventArgs e)
        {
            if (_searchResult.Count > 0)
            {
                var thread = new Thread(RefreshResultList);
                //thread.Name = "RefreshResultsList";
                thread.Start();
            }
            else
            {
                ShowMessageBox("Can not refresh! \r\n Result list empty!!", string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //stop the search
        private void StopSearchButtonClick(object sender, EventArgs e)
        {
            SetLogText("Searching was stopped!");
            _rtm.StopSearch = true;
        }

        private void DumpMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                _dumpFilePath = saveFileDialog.FileName;
                FileStream file = File.Create(_dumpFilePath);
                file.Close();
                SetLogText("Dump Memory to: " + _dumpFilePath);

                var oThread = new Thread(Dump);
                oThread.Start();
            }
            catch (Exception ex)
            {
                SetLogText("Dump Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0x82000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0x40000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }

        private void FreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                SetLogText("Freeze xbox console - command.");
                _rtm.StopCommand();
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
            catch (Exception ex)
            {
                SetLogText("Freeze Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }           
        }

        private void UnfreezeButtonClick(object sender, EventArgs e)
        {
            try
            {
                SetLogText("Un-Freeze xbox console - command.");
                _rtm.StartCommand();
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
            catch (Exception ex)
            {
                SetLogText("Un-Freeze Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }  
        }

        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0xC0000000");
            dumpLengthTextBox.Text = string.Format("0x1FFF0FFF");
        }

        private void QuickCalculatorPlusButtonClick(object sender, EventArgs e)
        {
            try
            {
                //convert text to uint the format results to hex string
                SetTextBoxText(quickCalculatorAnswerTextBox,
                    String.Format("0x{0:X}",
                    Functions.Convert(GetTextBoxText(quickCalculatorValueOneTextBox)) +
                    Functions.Convert(GetTextBoxText(quickCalculatorValueTwoTextBox))));
            }
            catch (Exception ex)
            {
                quickCalculatorAnswerTextBox.Clear();
                SetLogText("Quick Calculator Error [Plus]: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuickCalculatorMinusButtonClick(object sender, EventArgs e)
        {
            try
            {
                //convert text to uint the format results to hex string
                quickCalculatorAnswerTextBox.Text = String.Format("0x{0:X}", Functions.Convert(quickCalculatorValueOneTextBox.Text) -
                                                     Functions.Convert(quickCalculatorValueTwoTextBox.Text));
            }
            catch (Exception ex)
            {
                quickCalculatorAnswerTextBox.Clear();
                SetLogText("Quick Calculator Error [Minus]: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstantPokingButtonClick(object sender, EventArgs e)
        {
            Thread othread = new Thread(ConstantPoking);
            othread.Start();
        }
        #endregion

        #region HexBox Events
        private void HexBoxSelectionStartChanged(object sender, EventArgs e)
        {
            ChangeNumericValue();//When you select an offset on the hexbox

            var prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format("0x" + (address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void IsSignedCheckedChanged(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
            ChangeNumericValue();
        }
        private void NumericIntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }
        #endregion

        #region Search Tab Events
        private void SearchRangeValueTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !searchRangeValueTextBox.Focused) return;
            _searchRangeDumpLength = (Functions.Convert(endRangeAddressTextBox.Text) - Functions.Convert(startRangeAddressTextBox.Text));
            var oThread = new Thread(SearchRange);
            oThread.Start();
            e.Handled = true;
            searchRangeButton.Focus();
        }
        #endregion

        #region Functions
        private void NewPeek()
        {
            //Clean up
            peekPokeAddressTextBox.Clear();
            peekLengthTextBox.Clear();
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }
        private void AutoComplete()
        {
            peekPokeAddressTextBox.AutoCompleteCustomSource = _data;//put the auto complete data into the textbox
            var count = _data.Count;
            for (var index = 0; index < count; index++)
            {
                var value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, peekPokeAddressTextBox.Text))
                    _data.Add(peekPokeAddressTextBox.Text);
            }
        }

        private void LoadPlugins()
        {
            try
            {
                var pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plugins";
                if (!(Directory.Exists(pathToPlugins))) Directory.CreateDirectory(pathToPlugins);
                Plugin.Plugin.Init(pathToPlugins);
                pluginListView.Items.Clear();

                foreach (var plugin in Plugin.Plugin.PluginService.PluginList)
                {
                    //
                    var item = new ToolStripMenuItem
                    {
                        Name = plugin.Instance.ApplicationName,
                        Tag = plugin.Instance.ApplicationName,
                        Text = plugin.Instance.ApplicationName,
                        Image = plugin.Instance.Icon.ToBitmap(),
                        Size = new System.Drawing.Size(170, 22)
                    };

                    item.Click += PluginClickEventHandler; //Event handler if you click on the cheat code
                    pluginsToolStripMenuItem.DropDownItems.Add(item);
                    //Plugin Details
                    var listviewItem = new ListViewItem(plugin.Name);
                    listviewItem.SubItems.Add(plugin.Description);
                    listviewItem.SubItems.Add(plugin.Author);
                    listviewItem.SubItems.Add(plugin.Version);
                    pluginListView.Items.Add(listviewItem);
                    SetLogText("Plugin Added: " + plugin.Name + " Version: " + plugin.Version);
                }
            }
            catch (Exception e)
            {
                SetLogText("Plugin Error: " + e.Message);
                SetLogText("Please Check your plugin (*.dll)");
            }
        }

        //When you select an offset on the hexbox
        private void ChangeNumericValue()
        {
            if (hexBox.ByteProvider == null) return;
            var buffer = hexBox.ByteProvider.Bytes;
            if (isSigned.Checked)
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0 ?
                    Functions.ByteToSByte(hexBox.ByteProvider.ReadByte(hexBox.SelectionStart)) : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1 ?
                    Functions.BytesToInt16(buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray()) : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3 ?
                    Functions.BytesToInt32(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
            }
            else
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0 ?
                    buffer[(int)hexBox.SelectionStart] : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1 ?
                    Functions.BytesToUInt16(buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray()) : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3 ?
                    Functions.BytesToUInt32(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray()) : 0;
            }
            var prev = Functions.HexToBytes(peekPokeAddressTextBox.Text);
            var address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format("0x" + (address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void ChangedNumericValue(object sender)
        {
            if (hexBox.SelectionStart >= hexBox.ByteProvider.Bytes.Count) return;
            var numeric = (NumericUpDown)sender;
            switch (numeric.Name)
            {
                case "NumericInt8":
                    if (isSigned.Checked)
                    {
                        Console.WriteLine(((sbyte)numeric.Value).ToString("X2"));
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                      Functions.HexToBytes(((sbyte)numeric.Value).ToString("X2"))[0]);
                    }
                    else
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                      Convert.ToByte((byte)numeric.Value));
                    }
                    break;
                case "NumericInt16":
                    for (var i = 0; i < 2; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                    ? Functions.Int16ToBytes((short)numeric.Value)[i]
                                                                                    : Functions.UInt16ToBytes((ushort)numeric.Value)[i]);
                    }
                    break;
                case "NumericInt32":
                    for (var i = 0; i < 4; i++)
                    {
                        hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                    ? Functions.Int32ToBytes((int)numeric.Value)[i]
                                                                                    : Functions.UInt32ToBytes((uint)numeric.Value)[i]);
                    }
                    break;
            }
            hexBox.Refresh();
        }
        private void ChangeNumericMaxMin()
        {
            if (isSigned.Checked)
            {
                NumericInt8.Maximum = SByte.MaxValue;
                NumericInt8.Minimum = SByte.MinValue;
                NumericInt16.Maximum = Int16.MaxValue;
                NumericInt16.Minimum = Int16.MinValue;
                NumericInt32.Maximum = Int32.MaxValue;
                NumericInt32.Minimum = Int32.MinValue;
            }
            else
            {
                NumericInt8.Maximum = Byte.MaxValue;
                NumericInt8.Minimum = Byte.MinValue;
                NumericInt16.Maximum = UInt16.MaxValue;
                NumericInt16.Minimum = UInt16.MinValue;
                NumericInt32.Maximum = UInt32.MaxValue;
                NumericInt32.Minimum = UInt32.MinValue;
            }
        }

        //The cxlick handler for the plugins
        private void PluginClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                var item = (ToolStripMenuItem)sender;       // get the menu item
                foreach (var plugin in Plugin.Plugin.PluginService.PluginList)
                {
                    if (plugin.Name == item.Name)
                    {
                        plugin.Instance.Display();
                    }
                }
            }
            catch (Exception ex)
            {
                SetLogText("Plugin Instance: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion

        #region Thread Functions
        private void Peek(object a)
        {
            try
            {
                EnableControl(peekButton, false);
                if (string.IsNullOrEmpty(GetTextBoxText(peekLengthTextBox)) || Convert.ToUInt32(GetTextBoxText(peekLengthTextBox), 16) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(GetTextBoxText(peekPokeAddressTextBox)) || Convert.ToUInt32(GetTextBoxText(peekPokeAddressTextBox), 16) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte
                byte[] retValue = Functions.StringToByteArray(_rtm.Peek(GetTextBoxText(peekPokeAddressTextBox), GetTextBoxText(peekLengthTextBox), GetTextBoxText(peekPokeAddressTextBox), GetTextBoxText(peekLengthTextBox)));
                DynamicByteProvider buffer = new DynamicByteProvider(retValue) { IsWriteByte = true }; //object initilizer 
                hexBox.ByteProvider = buffer;
                hexBox.Refresh();
                SetLogText("Peeked Address: " + GetTextBoxText(peekPokeAddressTextBox) + " Length: " + GetTextBoxText(peekLengthTextBox));
                SetTextBoxText(peekPokeFeedBackTextBox,"Peek Success!");
                EnableControl(peekButton, true);
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                EnableControl(peekButton, true);
            }
        }

        private void Poke(object a)
        {
            try
            {
                EnableControl(pokeButton,false);
                uint dumplength = (uint)hexBox.ByteProvider.Length / 2;
                if (dumplength > 240)
                    throw new Exception("Poke Length has to be under 240 bytes.");
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(peekPokeAddressTextBox));//Set the dump offset
                _rtm.DumpLength = (uint)hexBox.ByteProvider.Length / 2;//The length of data to dump

                DynamicByteProvider buffer = (DynamicByteProvider)hexBox.ByteProvider;
                SetLogText("Poked Address: " + GetTextBoxText(peekPokeAddressTextBox) + " Length: " + _rtm.DumpLength);

                _rtm.Poke(GetTextBoxText(peekPokeAddressTextBox), Functions.ByteArrayToString(buffer.Bytes.ToArray()));
                SetTextBoxText(peekPokeFeedBackTextBox,"Poke Success!");
                EnableControl(pokeButton, true);
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControl(pokeButton, true);
            }
        }
        private void Connect(object a)
        {
            try
            {
                string ipAddress = GetTextBoxText(ipAddressTextBox);
                SetLogText("Connecting to: " + ipAddress);
                _rtm = new RealTimeMemory(ipAddress, 0, 0);//initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {
                    SetLogText("Connecting to " + ipAddress + " Failed.");
                    throw new Exception("Connection Failed!");
                }
                EnableControl(peeknpoke, true);

                statusStripLabel.Text = "Connected!";

                ShowMessageBox("Connected!", "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetLogText("Connected to " + ipAddress);

                if (!File.Exists(_filepath)) File.Create(_filepath).Dispose(); //Create the file if it doesn't exist
                using (StreamWriter objWriter = new StreamWriter(_filepath))
                {
                    //Writer Declaration
                    objWriter.Write(ipAddress); //Writes IP address to text file
                    objWriter.Close(); //Close Writer
                    SetTextBoxText(connectButton, "Re-Connect");
                }
            }
            catch (Exception ex)
            {
                SetLogText("Error: " + ex.Message);
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        //Refresh results Thread
        private void RefreshResultList()
        {
            try
            {
                EnableControl(resultRefreshButton, false);
                var value = 0;
                foreach (var item in _searchResult)
                {
                    UpdateProgressbar(0, _searchResult.Count, value, "Refreshing...");
                    value++;

                    //peekPokeAddressTextBox.Text = "0x" + _item.Offset;
                    var length = (item.Value.Length / 2).ToString("X");
                    var retvalue = _rtm.Peek("0x" + item.Offset, length, "0x" + item.Offset, length);

                    if (item.Value == retvalue) continue;//if value hasn't change continue for each loop

                    GridRowColours(value);
                    item.Value = retvalue;
                }
                SetLogText("Search List was refreshed!");
                ResultGridUpdate();
                UpdateProgressbar(0, 100, 0, "idle");
            }
            catch (Exception ex)
            {
                SetLogText("Search List Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(resultRefreshButton, true);
                UpdateProgressbar(0, 100, 0, "idle");
                Thread.CurrentThread.Abort();
            }
        }

        //Searches the memory for the specified value (Experimental)
        private void SearchRange()
        {
            try
            {
                EnableControl(searchRangeButton,false);
                EnableControl(stopSearchButton, true);
                SetLogText("Search Offset: " + GetTextBoxText(startRangeAddressTextBox) + " Search Length: " +
                           _searchRangeDumpLength);
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(startRangeAddressTextBox));
                _rtm.DumpLength = _searchRangeDumpLength;

                ResultGridClean();//Clean list view

                //The ExFindHexOffset function is a Experimental search function
                var results = _rtm.FindHexOffset(GetTextBoxText(searchRangeValueTextBox));//pointer
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0);

                if (results.Count < 1)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }
                _searchResult = results;
                ResultGridUpdate();
            }
            catch (Exception e)
            {
                SetLogText("Search Range Error: " + e.Message);
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(searchRangeButton, true);
                EnableControl(stopSearchButton, false);
                Thread.CurrentThread.Abort();
            }
        }

        //Dump memory to a file
        private void Dump()
        {
            try
            {
                EnableControl(dumpMemoryButton,false);
                SetLogText("Dump Offset: " + GetTextBoxText(dumpStartOffsetTextBox) + " Dump Length: " + GetTextBoxText(dumpLengthTextBox));
                _rtm.Dump(_dumpFilePath, GetTextBoxText(dumpStartOffsetTextBox), GetTextBoxText(dumpLengthTextBox));
                UpdateProgressbar(0, 100, 0);
            }
            catch (Exception e)
            {
                SetLogText("Dump Error: "+e.Message);
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(dumpMemoryButton, true);
                UpdateProgressbar(0, 100, 0);
                Thread.CurrentThread.Abort();
            }
        }

        private void ConstantPoking()
        {
            try
            {
                while(constantPokingCheckBox.Checked)
                {
                    int startPause = 0;
                    //Pause
                    while (startPause <= GetNumericValue(pauseBetweenPokesNumericUpDown))
                    {
                        startPause++;
                    }
                    _rtm.Poke(GetTextBoxText(constantPokingAddressTextBox), GetTextBoxText(constantPokingLengthTextBox));
                    SetConstantPokingDebugText("Successful");
                }
            }
            catch (Exception ex)
            {
                SetLogText("Constant Poking Error: " + ex.Message);
                SetConstantPokingDebugText("Constant Poking Error: " + ex.Message);
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        #endregion

        #region safeThreadingProperties
        //Getter Methods
        private string GetTextBoxText(Control control)
        {
            //recursion
            var returnVal = "";
            if (control.InvokeRequired) control.Invoke((MethodInvoker)
                  delegate { returnVal = GetTextBoxText(control); });
            else
                return control.Text;
            return returnVal;
        }
        private uint GetNumericValue(NumericUpDown control)
        {
            //recursion
            uint returnVal = 0;
            if (control.InvokeRequired) control.Invoke((MethodInvoker)
                  delegate { returnVal = GetNumericValue(control); });
            else
                return (uint)control.Value;
            return returnVal;
        }

        private void EnableControl(Control control, bool value)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)(() => EnableControl(control, value)));
            else
                control.Enabled = value;
        }
        
        //Setter Methods
        private void SetTextBoxText(Control control, string value)
        {
            if (control.InvokeRequired)
                Invoke((MethodInvoker)(() => SetTextBoxText(control,value)));
            else
            {
                control.Text = value;
            }
        }

        private void SetLogText(string value)
        {
            if (logTextBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetLogText(value)));
            else
            {
                var m = DateTime.Now.ToString("HH:mm:ss tt") + " " + value + Environment.NewLine;
                logTextBox.Text += m;
                logTextBox.Select(logTextBox.Text.Length, 0); // set the cursor to end of textbox
                logTextBox.ScrollToCaret();                     // scroll down to the cursor position
            }
        }
        private void SetConstantPokingDebugText(string value)
        {
            if (logTextBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetConstantPokingDebugText(value)));
            else
            {
                var m = DateTime.Now.ToString("HH:mm:ss tt") + " " + value + Environment.NewLine;
                constantPokingDebugTextBox.Text += m;
                constantPokingDebugTextBox.Select(logTextBox.Text.Length, 0); // set the cursor to end of textbox
                constantPokingDebugTextBox.ScrollToCaret();                     // scroll down to the cursor position
            }
        }
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }

        //Control changes
        private void GridRowColours(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(() => GridRowColours(value)));
            else
                resultGrid.Rows[value - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        }

        //Refresh the values of Search Results
        private void ResultGridClean()
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker)(ResultGridClean));
            else
                resultGrid.Rows.Clear();
        }
        private void ResultGridUpdate()
        {
            //IList or represents a collection of objects(String)
            if (resultGrid.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                resultGrid.Invoke((MethodInvoker)(ResultGridUpdate));
            else
            {
                resultGrid.DataSource = _searchResult;
                resultGrid.Refresh();
            }
        }

        //Progress bar Delegates
        private void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker)(() => UpdateProgressbar(min, max, value, text)));
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

        #region Addressbox Autocorrection
        // These will automatically add "0x" to an offset if it hasn't been added already - 8Ball
        private void FixTheAddresses(object sender, EventArgs e)
        {
            if (!peekPokeAddressTextBox.Text.StartsWith("0x")) //Peek Address Box, Formatting Check.
            {//PeekPokeAddress
                if (!peekPokeAddressTextBox.Text.Equals("")) //Empty Check
                    peekPokeAddressTextBox.Text = (string.Format("0x" + peekPokeAddressTextBox.Text)); //Formatting
            }
            if (peekLengthTextBox.Text.StartsWith("0x")) // Checks if peek length is hex value or not based on 0x
            { //Peeklength pt1
                string result = (peekLengthTextBox.Text.ToUpper().Substring(2));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(peekLengthTextBox.Text.ToUpper(), "^[A-Z]$")) //Checks if hex, based on uppercase alphabet presence.
            {//Peeklength pt2
                string result = (peekLengthTextBox.Text.ToUpper());
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            else if (peekLengthTextBox.Text.StartsWith("h")) //Checks if hex, based on starting with h.
            {//Peeklength pt3
                string result = (peekLengthTextBox.Text.ToUpper().Substring(1));
                uint result2 = UInt32.Parse(result, System.Globalization.NumberStyles.HexNumber);
                peekLengthTextBox.Text = result2.ToString();
            }
            if (!startRangeAddressTextBox.Text.StartsWith("0x"))
            {//RangeStart
                if (!startRangeAddressTextBox.Text.Equals(""))
                    startRangeAddressTextBox.Text = (string.Format("0x" + startRangeAddressTextBox.Text));
            }
            if (endRangeAddressTextBox.Text.StartsWith("0x")) return; //RangeEnd
            if (!endRangeAddressTextBox.Text.Equals(""))
                endRangeAddressTextBox.Text = (string.Format("0x" + endRangeAddressTextBox.Text));
        }

        private void FixDumpAddresses(object sender, EventArgs e)
        {
            if (dumpStartOffsetTextBox.Text.StartsWith("0x")) return;
            if (dumpStartOffsetTextBox.Text.Equals("")) return;
            dumpStartOffsetTextBox.Text = (string.Format("0x" + dumpStartOffsetTextBox.Text));
            if (!System.Text.RegularExpressions.Regex.IsMatch(dumpStartOffsetTextBox.Text.Substring(2), @"\A\b[0-9a-fA-F]+\b\Z"))
                dumpStartOffsetTextBox.Clear();
        }

        private void FixDumpLength(object sender, EventArgs e)
        {
            if (dumpLengthTextBox.Text.StartsWith("0x")) return;
            if (dumpLengthTextBox.Text.Equals("")) return;
            dumpLengthTextBox.Text = (string.Format("0x" + dumpLengthTextBox.Text));
            if (!System.Text.RegularExpressions.Regex.IsMatch(dumpLengthTextBox.Text.Substring(2), @"\A\b[0-9a-fA-F]+\b\Z"))
                dumpLengthTextBox.Clear();
        }

        #endregion

        #region Autocalculation
        private void Int32ToHex(object sender, EventArgs e)
        {
            if (!integer32CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer32CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            Int32 number;
            var validResult = Int32.TryParse(integer32CalculatorTextBox.Text, out number); //Stops things like a single "-" causing errors
            if (!validResult) return;

            
            if (!BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                var num3 = (number >> 0x10) & 0xff;
                var num4 = (number >> 0x18) & 0xff;
                number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
            }

            var hex = number.ToString("X4"); //x is for hex and 4 is padding to a 4 digit value, uppercases.
            hexCalculatorTextBox.Text = (string.Format("0x" + hex)); //Formats string, adds 0x
        }

        private void Int8ToHex(object sender, EventArgs e)
        {
            if (!integer8CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer8CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return
            
            byte number;
            var validResult = byte.TryParse(integer8CalculatorTextBox.Text, out number);
            if (!validResult) return;

            var hex = number.ToString("X2"); //x is for hex and 2 is padding to a 2 digit value, uppercases.
            hexCalculatorTextBox.Text = (string.Format("0x" + hex)); //Formats string, adds 0x
        }

        private void Int16ToHex(object sender, EventArgs e)
        {
            if (!integer16CalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(integer16CalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return
            
            short number;
            var validResult = short.TryParse(integer16CalculatorTextBox.Text, out number);
            if (!validResult) return;

            if (!BigEndianRadioButton.Checked)
            {
                var num = number & 0xff;
                var num2 = (number >> 8) & 0xff;
                number = (short)((num << 8) | num2);
            }
            hexCalculatorTextBox.Text = (string.Format("0x" + number.ToString("X3")));
        }

        private void FloatToHex(object sender, EventArgs e)
        {
            if (!floatCalculatorTextBox.Focused) return; //if the integer textbox isn't selected return
            if (System.Text.RegularExpressions.Regex.IsMatch(floatCalculatorTextBox.Text.ToUpper(), "[A-Z]")) return; //if we have characters return

            float number;
            var validResult = float.TryParse(floatCalculatorTextBox.Text, out number);
            if (!validResult) return;

            var buffer = BitConverter.GetBytes(number);//comes out as little endian
            if (BigEndianRadioButton.Checked) Array.Reverse(buffer);

            var hex = BitConverter.ToString(buffer).Replace("-", "");
            hexCalculatorTextBox.Text = (string.Format("0x" + hex));
        }

        private void HexToInt(object sender, EventArgs e)
        {
            if (!hexCalculatorTextBox.Focused) return;
            var hexycalc = hexCalculatorTextBox.Text.StartsWith("0x") ? hexCalculatorTextBox.Text.Substring(2) : hexCalculatorTextBox.Text;
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(hexycalc, @"\A\b[0-9a-fA-F]+\b\Z")) return;
            try
            {
                if (hexycalc.Length >= 0 && hexycalc.Length <= 2)
                {
                    integer8CalculatorTextBox.Text = Convert.ToSByte(hexCalculatorTextBox.Text, 16).ToString();
                    integer16CalculatorTextBox.Clear();
                    integer32CalculatorTextBox.Clear();
                    floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 2 && hexycalc.Length <= 4)
                {
                    var number = Convert.ToInt16(hexCalculatorTextBox.Text, 16);
                    if (!BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        number = (short)((num << 8) | num2);
                    }

                    integer16CalculatorTextBox.Text = number.ToString();
                    integer32CalculatorTextBox.Clear();
                    floatCalculatorTextBox.Clear();
                }
                if (hexycalc.Length >= 8)
                {
                    var number = Convert.ToInt32(hexCalculatorTextBox.Text, 16);
                    if (!BigEndianRadioButton.Checked)
                    {
                        var num = number & 0xff;
                        var num2 = (number >> 8) & 0xff;
                        var num3 = (number >> 0x10) & 0xff;
                        var num4 = (number >> 0x18) & 0xff;
                        number = ((((num << 0x18) | (num2 << 0x10)) | (num3 << 8)) | num4);
                    }
                    integer32CalculatorTextBox.Text = number.ToString();

                    var input = hexCalculatorTextBox.Text;
                    var output = new byte[(input.Length / 2)];

                    if ((input.Length % 2) != 0) input = "0" + input;
                    int index;
                    for (index = 0; index < output.Length; index++)
                    {
                        output[index] = Convert.ToByte(input.Substring((index * 2), 2), 16);
                    }
                    Array.Reverse(output);
                    floatCalculatorTextBox.Text = BitConverter.ToSingle(output, 0).ToString();
                }
            }
            catch(Exception ex)
            {
                SetLogText("Suppressed Conversion Error: " + ex.Message);
            }
        }
        #endregion
    }
}
