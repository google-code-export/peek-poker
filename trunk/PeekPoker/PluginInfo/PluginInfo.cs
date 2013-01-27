using System.Windows.Forms;

namespace PeekPoker.PluginInfo
{
    public partial class PluginInfo : Form
    {
        public PluginInfo(ListViewItem items)
        {
            this.InitializeComponent();
            if(items == null)
                items = new ListViewItem();
            this.pluginListView.Items.Add(items);
        }
    }
}
