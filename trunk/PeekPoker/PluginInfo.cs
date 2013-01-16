using System.Windows.Forms;

namespace PeekPoker
{
    public partial class PluginInfo : Form
    {
        public PluginInfo(ListViewItem items)
        {
            InitializeComponent();
            if(items == null)
                items = new ListViewItem();
            pluginListView.Items.Add(items);
        }
    }
}
