using System;
using System.IO;
using System.Windows.Forms;
using PeekPoker.Interface;
using PeekPoker.Plugin;

namespace PeekPoker.PeekPokerLitePartialClasses
{
    public partial class PeekPokerLite
    {
        private PluginService _pluginService;

        private void LoadPlugins(object threadObject)
        {
            try
            {
                string pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plug-in";
                if (!(Directory.Exists(pathToPlugins)))
                    Directory.CreateDirectory(pathToPlugins);
                _pluginService = new PluginService(pathToPlugins);

                string[] data = new string[2];
                foreach (IPlugin plugin in _pluginService.PluginDatas)
                {
                    data[0] = plugin.ApplicationName;
                    data[1] = plugin.Version;
                    AddItemListView(new ListViewItem(data));
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "Peek Poker Lite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Thread Delegates

        private void AddItemListView(ListViewItem item)
        {
            try
            {
                if (pluginListView.InvokeRequired)
                {
                    pluginListView.Invoke((MethodInvoker) delegate { AddItemListView(item); });
                }
                else
                {
                    pluginListView.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //Using lambda express - I believe its slower - Just an example
            if (InvokeRequired)
                Invoke((MethodInvoker) (() => ShowMessageBox(text, caption, buttons, icon)));
            else MessageBox.Show(this, text, caption, buttons, icon);
        }

        #endregion
    }
}