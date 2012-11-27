using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PeekPoker.PeekPokerLitePartialClasses
{
    public partial class PeekPokerLite : Form
    {
        private int _currentlySelectedItem;

        public PeekPokerLite()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(LoadPlugins);
        }

        private void PluginListViewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return; //If not right click return
            ListViewItem item = pluginListView.GetItemAt(e.X, e.Y); //Get item and coordinates
            if (item == null) return; //If item is null return

            _currentlySelectedItem = item.Index; //Get the index of the item
            Point point = pluginListView.PointToScreen(e.Location); //Point to display context menu
            contextMenu.Show(point);
        }

        private void LoadToolStripMenuItemClick(object sender, EventArgs e)
        {
            Form n = (Form) _pluginService.PluginDatas[_currentlySelectedItem];
            n.MdiParent = this;
            n.Show();
        }
    }
}