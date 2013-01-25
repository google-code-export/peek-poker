using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PeekPoker.Interface;
using PeekPoker.Plugin;

namespace PeekPoker
{
    public partial class PeekPokerMainForm : Form
    {
        #region global varibales

        private ListViewItem _listviewItem;
        private PluginService _pluginService;
        private RealTimeMemory _rtm; //DLL is now in the Important File Folder

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
            Process.GetCurrentProcess().Kill(); //Immediately stop the process
        }

        private void Form1Load(Object sender, EventArgs e)
        {
            try
            {
                ipAddressTextBox.Text =
                    (string)
                    Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "DefaultIP", "0.0.0.0");
            }
            catch (Exception)
            {
            }
        }

        #region button clicks

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("Peek Poker - Open Source Memory Editor :Revision 8"));
            stringBuilder.AppendLine(string.Format("================By===================="));
            stringBuilder.AppendLine(string.Format("Cybersam, 8Ball, PureIso & Jappi88"));
            stringBuilder.AppendLine(string.Format("=============Special Thanks To========="));
            stringBuilder.AppendLine(
                string.Format("optantic (tester), Mojobojo (codes), Natelx (xbdm), Be.Windows.Forms.HexBox.dll"));
            stringBuilder.AppendLine(string.Format("Fairchild (codes), actualmanx (tester), ioritree (tester), 360Haven"));
            ShowMessageBox(stringBuilder.ToString(), string.Format("Peek Poker"), MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }

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

        private void peekNpokeButton_Click(object sender, EventArgs e)
        {
            PeekNPoke form = displayOutsideParentBox.Checked
                                 ? new PeekNPoke(_rtm)
                                 : new PeekNPoke(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.UpdateProgressbar += UpdateProgressbar;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.SetTextBoxText += SetTextBoxText;
            form.Show();
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            Dump form = displayOutsideParentBox.Checked
                            ? new Dump(_rtm)
                            : new Dump(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            Search form = displayOutsideParentBox.Checked
                              ? new Search(_rtm)
                              : new Search(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        private void converterButton_Click(object sender, EventArgs e)
        {
            Converter form = displayOutsideParentBox.Checked
                                 ? new Converter()
                                 : new Converter {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.Show();
        }

        private void pluginInfoButton_Click(object sender, EventArgs e)
        {
            PluginInfo form = displayOutsideParentBox.Checked
                                  ? new PluginInfo(_listviewItem)
                                  : new PluginInfo(_listviewItem) {MdiParent = this};
            form.Show();
        }

        #endregion

        #region Functions

        private void LoadPlugins()
        {
            try
            {
                string pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plugins";
                if (!(Directory.Exists(pathToPlugins))) Directory.CreateDirectory(pathToPlugins);

                _pluginService = new PluginService(pathToPlugins);

                foreach (AbstractPlugin pluginData in _pluginService.PluginDatas)
                {
                    EnableControl(pluginInfoButton, true);
                    var item = new ToolStripMenuItem
                                   {
                                       Name = pluginData.ApplicationName,
                                       Tag = pluginData.ApplicationName,
                                       Text = pluginData.ApplicationName,
                                       Image = pluginData.Icon.ToBitmap(),
                                       Size = new Size(170, 22)
                                   };
                    item.Click += PluginClickEventHandler; //Event handler if you click on the cheat code
                    pluginsToolStripMenuItem.DropDownItems.Add(item);

                    //Plugin Details
                    _listviewItem = new ListViewItem(pluginData.ApplicationName);
                    _listviewItem.SubItems.Add(pluginData.Description);
                    _listviewItem.SubItems.Add(pluginData.Author);
                    _listviewItem.SubItems.Add(pluginData.Version);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //The click handler for the plugins
        private void PluginClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                var item = (ToolStripMenuItem) sender; // get the menu item
                foreach (AbstractPlugin plugin in _pluginService.PluginDatas)
                {
                    if (plugin.ApplicationName == item.Name)
                    {
                        plugin.RTM = _rtm;
                        plugin.IsMdiChild = !displayOutsideParentBox.Checked;
                        plugin.Display(this);
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
                _rtm = new RealTimeMemory(ipAddress, 0, 0); //initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {
                    throw new Exception("Connection Failed!");
                }
                EnableControl(mainGroupBox, true);

                SetToolStripLabel("Connected!");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "DefaultIP",
                                  ipAddress.ToString(CultureInfo.InvariantCulture)); //Store IP in registry
                ShowMessageBox("Connected!", "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetTextBoxText(connectButton, "Re-Connect");
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
            string returnVal = "";
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)
                               delegate { returnVal = GetTextBoxText(control); });
            else
                return control.Text;
            return returnVal;
        }

        private void EnableControl(Control control, bool value)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker) (() => EnableControl(control, value)));
            else
                control.Enabled = value;
        }

        //Setter Methods
        private void SetToolStripLabel(string value)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker) (() => SetToolStripLabel(value)));
            else
            {
                statusStripLabel.Text = value;
            }
        }

        private void SetTextBoxText(Control control, string value)
        {
            if (control.InvokeRequired)
                Invoke((MethodInvoker) (() => SetTextBoxText(control, value)));
            else
            {
                control.Text = value;
            }
        }

        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker) (() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }

        //Progress bar Delegates
        private void UpdateProgressbar(int min, int max, int value, string text = "Idle")
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker) (() => UpdateProgressbar(min, max, value, text)));
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
    }
}