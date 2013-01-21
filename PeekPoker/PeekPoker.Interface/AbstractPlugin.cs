
using System.Drawing;
using System.Windows.Forms;

namespace PeekPoker.Interface
{
    public abstract class AbstractPlugin : IPlugin
    {
        private string _applicationName;
        private string _description;
        private string _author;
        private string _version;
        private Icon _icon;

        public AbstractPlugin()
        {
            _applicationName = "Unavailable";
            _description = "Unavailable";
            _author = "Unavailable";
            _version = "Unavailable";
            _icon = null;
        }

        public virtual void Display(Form parent)
        {
            Form form = IsMdiChild ?
                new Form { MdiParent = parent } : new Form();
            form.Show();
        }

        public RealTimeMemory RTM { get; set; }
        public bool IsMdiChild { get; set; }

        public string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public Icon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}