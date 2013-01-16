using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PeekPoker.Interface;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PeekPoker
{
    public partial class PeekPokerMainForm : Form
    {
        #region global varibales

        private ListViewItem _listviewItem;
        private RealTimeMemory _rtm;//DLL is now in the Important File Folder
        private readonly string _filepath = (Application.StartupPath + "\\XboxIP.txt"); //For IP address loading - 8Ball
        #endregion

        public PeekPokerMainForm()
        {
            InitializeComponent();
            //SetLogText("Welcome to Peek Poker.");
            //SetLogText("Please make sure you have the xbdm xbox 360 plug-in loaded.");
            //SetLogText("All the information provided in this application is for educational purposes only. Neither the application nor host are in any way responsible for any misuse of the information.");
            LoadPlugins();
         
        }
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();//Immediately stop the process
        }
        private void Form1Load(Object sender, EventArgs e)
        {
            try
            {

                //feature suggested by Fairchild
                var ipRegex = new Regex(@"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"); //IP Check for XDK Name
                var xboxname = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "XboxName", "NotFound"); //feature suggested by fairchild -Introduced regex to accomodate the possiblity of a name instead of ip, which is very possible.
                var validIp = ipRegex.Match(xboxname); // For regex to check if there's a match
                if (validIp.Success) ipAddressTextBox.Text = xboxname;// If the registry contains a valid IP, send to textbox.
                if (File.Exists(_filepath)) ipAddressTextBox.Text = File.ReadAllText(_filepath);
                //else SetLogText("XboxIP.txt was not detected, will be created upon connection.");
            }
            catch (Exception)
            {
                //SetLogText("Error: XenonSDK not installed XboxName is null! You can still use PeekPoker");
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
       
        #region button clicks
        //When you click on the connect button
        private void ConnectButtonClick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Connect);
        }
        //When you click on the www.360haven.com
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            Process.Start("www.360haven.com");
        }
        #endregion

        #region Functions
        private void LoadPlugins()
        {
            try
            {
                var pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plugins";
                if (!(Directory.Exists(pathToPlugins))) Directory.CreateDirectory(pathToPlugins);
                Plugin.Plugin.Init(pathToPlugins);

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
                    _listviewItem = new ListViewItem(plugin.Name);
                    _listviewItem.SubItems.Add(plugin.Description);
                    _listviewItem.SubItems.Add(plugin.Author);
                    _listviewItem.SubItems.Add(plugin.Version);
                    //
                }
            }
            catch (Exception e)
            {
            }
        }
        //The click handler for the plugins
        private void PluginClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                var item = (ToolStripMenuItem)sender;       // get the menu item
                foreach (var plugin in Plugin.Plugin.PluginService.PluginList)
                {
                    if (plugin.Name == item.Name)
                    {
                        plugin.Instance.Display(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion

        #region Thread Functions
        private void Connect(object a)
        {
            try
            {
                string ipAddress = GetTextBoxText(ipAddressTextBox);
                _rtm = new RealTimeMemory(ipAddress, 0, 0);//initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {

                    throw new Exception("Connection Failed!");
                }
                //EnableControl(peeknpoke, true);

                statusStripLabel.Text = "Connected!";

                ShowMessageBox("Connected!", "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ShowMessageBox(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
        /*private uint GetNumericValue(NumericUpDown control)
        {
            //recursion
            uint returnVal = 0;
            if (control.InvokeRequired) control.Invoke((MethodInvoker)
                  delegate { returnVal = GetNumericValue(control); });
            else
                return (uint)control.Value;
            return returnVal;
        }
         * */
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
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
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
        

        private void peekNpokeButton_Click(object sender, EventArgs e)
        {
            PeekNPoke form = new PeekNPoke(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.Show();
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            Dump form = new Dump(_rtm) { MdiParent = this };
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.SetTextBoxText += SetTextBoxText;
            form.Show();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Search form = new Search(_rtm) { MdiParent = this };
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        private void converterButton_Click(object sender, EventArgs e)
        {
            Converter form = new Converter { MdiParent = this };
            form.Show();
        }

        private void pluginInfoButton_Click(object sender, EventArgs e)
        {
            PluginInfo form = new PluginInfo(_listviewItem) { MdiParent = this };
            form.Show();
        }


    }
}
