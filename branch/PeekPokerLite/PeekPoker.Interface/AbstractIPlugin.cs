using System.Drawing;
using System.Windows.Forms;

namespace PeekPoker.Interface
{
    public abstract class AbstractIPlugin : IPlugin
    {
        #region IPlugin

        public virtual void Display(Form form)
        {
            Form newForm = new Form();
            newForm.MdiParent = form;
            FormName = newForm.Name;
            newForm.Show();
        }

        public virtual string ApplicationName
        {
            get { return "Unknown"; }
        }

        public virtual string Description
        {
            get { return "Unknown"; }
        }

        public virtual string Author
        {
            get { return "Unknown"; }
        }

        public virtual string Version
        {
            get { return "0.0.0.0"; }
        }

        public virtual Icon Icon
        {
            get { return null; }
        }

        #endregion

        public virtual string IPAddress { get; set; }

        public virtual string FormName { get; set; }

        public virtual RealTimeMemory Rtm { get; set; }
    }
}