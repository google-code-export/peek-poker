using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PeekPoker.About;
using PeekPoker.Interface;
using PeekPoker.Plugin;

namespace PeekPoker
{
    public partial class PeekPokerMainForm : Form
    {
        #region global varibales

        private List<ListViewItem> _listviewItem;
        private PluginService _pluginService;
        private RealTimeMemory _rtm; //DLL is now in the Important File Folder

        #endregion

        public PeekPokerMainForm()
        {
            InitializeComponent();
            LoadPlugins();
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            Process.GetCurrentProcess().Kill(); //Immediately stop the process
        }

        //TODO: All config will be loaded at start up so this should be removed
        private void Form1Load(Object sender, EventArgs e)
        {
            try
            {
                string ip = "";

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
                if (!(File.Exists(filePath)))
                {
                    using (FileStream str = File.Create(filePath)) { str.Close(); }
                }
                using (StreamReader file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        switch (line)
                        {
                            case "#IP#":
                                ip = file.ReadLine();
                                break;

                            case "#BG#":
                                var bgstorage = file.ReadLine();
                                if (bgstorage != "None")
                                { BGBox.ImageLocation = bgstorage;
                                    BGBox.Visible = true;
                                }
                                break;

                            case "#BGSize#":
                                var sizestorage = file.ReadLine();
                                switch (sizestorage)
                                {                                    
                                    case "AutoSize":
                                BGBox.SizeMode = PictureBoxSizeMode.AutoSize;
                                SizeBox.SelectedIndex = 0;
                                       break;
                                   case "CenterImage":
                                BGBox.SizeMode = PictureBoxSizeMode.CenterImage;
                                SizeBox.SelectedIndex = 1;
                                        break;
                                    case "Normal":
                                        BGBox.SizeMode = PictureBoxSizeMode.Normal;
                                        SizeBox.SelectedIndex = 2;
                                        break;
                                    case "StretchImage":
                                BGBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                        SizeBox.SelectedIndex = 3;
                                        break;
                                    case "Zoom":
                                        BGBox.SizeMode = PictureBoxSizeMode.Zoom;
                                        SizeBox.SelectedIndex = 4; 
                                        break;
                                }
                                break;
                        }
                    }
                }

                ipAddressTextBox.Text = ip;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message,"Peek Poker",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region button clicks

        private void showHideOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionPanel.Visible)
                optionPanel.Hide();
           else
                optionPanel.Show();
        }

        private void showHidePluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pluginPanel.Visible)
                pluginPanel.Hide();
            else
                pluginPanel.Show();
        }
        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        //The click handler for the plugins
        private void PluginClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                var item = (Button)sender; // get the menu item
                foreach (AbstractPlugin plugin in _pluginService.PluginDatas)
                {
                    if (plugin.ApplicationName != item.Name) continue;

                    //Setting Values
                    plugin.APRtm = this._rtm;
                    plugin.IsMdiChild = !this.displayOutsideParentBox.Checked;
                    plugin.APShowMessageBox += this.ShowMessageBox;
                    plugin.APEnableControl += this.EnableControl;
                    plugin.APUpdateProgressBar += this.UpdateProgressbar;
                    plugin.APGetTextBoxText += this.GetTextBoxText;
                    plugin.APSetTextBoxText += this.SetTextBoxText;
                    plugin.Display(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //TODO: Once user connect save the ip to config file
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
            PeekNPoke.PeekNPoke form = displayOutsideParentBox.Checked
                                 ? new PeekNPoke.PeekNPoke(_rtm)
                                 : new PeekNPoke.PeekNPoke(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.UpdateProgressbar += UpdateProgressbar;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.SetTextBoxText += SetTextBoxText;
            form.Show();
            if (form.MdiParent == this) BGPanel.Controls.Add(form);
            BGBox.SendToBack();
            
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            Dump.Dump form = displayOutsideParentBox.Checked
                            ? new Dump.Dump(_rtm)
                            : new Dump.Dump(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
            if (form.MdiParent == this) BGPanel.Controls.Add(form);
                BGBox.SendToBack();
            }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            Search.Search form = displayOutsideParentBox.Checked
                              ? new Search.Search(_rtm)
                              : new Search.Search(_rtm) {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.EnableControl += EnableControl;
            form.GetTextBoxText += GetTextBoxText;
            form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
            if (form.MdiParent == this) BGPanel.Controls.Add(form);
                BGBox.SendToBack();
           }

        private void converterButton_Click(object sender, EventArgs e)
        {
            Converter.Converter form = displayOutsideParentBox.Checked
                                 ? new Converter.Converter()
                                 : new Converter.Converter {MdiParent = this};
            form.ShowMessageBox += ShowMessageBox;
            form.Show();
            if (form.MdiParent == this) BGPanel.Controls.Add(form);
                BGBox.SendToBack();
           }

        private void pluginInfoButton_Click(object sender, EventArgs e)
        {
            PluginInfo.PluginInfo form = displayOutsideParentBox.Checked
                                  ? new PluginInfo.PluginInfo(_listviewItem)
                                  : new PluginInfo.PluginInfo(_listviewItem) {MdiParent = this};
            form.Show();
            if (form.MdiParent == this) BGPanel.Controls.Add(form);
                BGBox.SendToBack();
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
                _listviewItem = new List<ListViewItem>(_pluginService.PluginDatas.Count);

                foreach (AbstractPlugin pluginData in _pluginService.PluginDatas)
                {
                    EnableControl(pluginInfoButton, true);
                    Button item = new Button
                                      {
                                          Name = pluginData.ApplicationName,
                                          Tag = pluginData.ApplicationName,
                                          Text = pluginData.ApplicationName,
                                          Image = pluginData.Icon.ToBitmap(),
                                          Size = new Size(108, 57),
                                          ImageAlign = ContentAlignment.TopCenter,
                                          MaximumSize = new Size(108, 50),
                                          Dock = DockStyle.Left,
                                          TextAlign = ContentAlignment.BottomCenter,
                                          
                                      };
                    item.Click += PluginClickEventHandler;
                    pluginPanel.Controls.Add(item);

                    //Plugin Details
                    ListViewItem listviewItem = new ListViewItem(pluginData.ApplicationName);
                    listviewItem.SubItems.Add(pluginData.Description);
                    listviewItem.SubItems.Add(pluginData.Author);
                    listviewItem.SubItems.Add(pluginData.Version);
                    _listviewItem.Add(listviewItem);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save()
        {
            string ipAddress = GetTextBoxText(ipAddressTextBox);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
            if (!(File.Exists(filePath)))
            {
                using (FileStream str = File.Create(filePath)) { str.Close(); }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("#License#");
            stringBuilder.AppendLine("Accept");
            stringBuilder.AppendLine("#IP#");
            stringBuilder.AppendLine(ipAddress);
            stringBuilder.AppendLine("#BG#");
            stringBuilder.AppendLine(BGBox.ImageLocation ?? "None");
            stringBuilder.AppendLine("#BGSize#");
            stringBuilder.AppendLine(BGBox.SizeMode.ToString());

            string line = stringBuilder.ToString();
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.Write(line);
            }
        }
        private void BgControl(object sender, EventArgs e)
        {
            switch (SizeBox.SelectedIndex)
            {
                case 0:
                    BGBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case 1:
                    BGBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case 2:
                    BGBox.SizeMode = PictureBoxSizeMode.Normal;
                    break;
                case 3:
                    BGBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 4:
                    BGBox.SizeMode = PictureBoxSizeMode.Zoom;
                    break;
            }
        }

        #endregion

        #region Thread Functions

        private void Connect(object a)
        {
            if (ipAddressTextBox.Text.ToUpper() == "DEBUG") //For debugging PP without a connection to xbox
            {
                EnableControl(mainGroupBox, true); EnableControl(pluginPanel, true); 
                return; //Bypass needing to connect to xbox for debugging purposes.
            }
            try
            {
                if (!Regex.IsMatch(ipAddressTextBox.Text, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")) //Checks if valid IP
            {
                MessageBox.Show(" IP Address is not valid!");
                return;
            }

               Save(); //Moved to own function
               _rtm = new RealTimeMemory(ipAddressTextBox.Text, 0, 0); //initialize real time memory
                _rtm.ReportProgress += UpdateProgressbar;

                if (!_rtm.Connect())
                {
                    throw new Exception("Connection Failed!");
                }
                EnableControl(mainGroupBox, true);

                SetToolStripLabel("Connected!");
                //#CODE FOR SAVING IP IN REGISTRY#
                //Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\XenonSDK", "DefaultIP",
                //                  ipAddress.ToString(CultureInfo.InvariantCulture)); //Store IP in registry
                ShowMessageBox("Connected!", "Peek Poker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetTextBoxText(connectButton, "Re-Connect");
                this.EnableControl(pluginPanel,true);
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

        #region Background Image
        private void SelectBg(object sender, EventArgs e)
        {
            var open = new OpenFileDialog {Title = "Select an image as your background."};
            open.ShowDialog();
                if (open.FileName == null) return;
                BGBox.SizeMode = PictureBoxSizeMode.StretchImage;
                BGBox.ImageLocation = open.FileName;
            BGBox.Visible = true;
            
            Save();
            }
       
        private void ClearBg(object sender, EventArgs e)
        {
            BGBox.ImageLocation = null;
            BGBox.Visible = false;
        }

        private void fromScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    #endregion


}
}