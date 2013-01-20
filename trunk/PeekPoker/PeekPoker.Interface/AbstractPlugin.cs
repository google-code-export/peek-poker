
using System.Drawing;
using System.Windows.Forms;

namespace PeekPoker.Interface
{
    public abstract class AbstractPlugin : IPlugin
    {
        public virtual void Display(Form parent)
        {
            Form form = IsMdiChild ?
                new Form { MdiParent = parent } : new Form();
            //form.ShowMessageBox += ShowMessageBox;
           // form.EnableControl += EnableControl;
          //  form.GetTextBoxText += GetTextBoxText;
          //  form.UpdateProgressbar += UpdateProgressbar;
            form.Show();
        }

        public RealTimeMemory RTM { get; set; }
        public bool IsMdiChild { get; set; }

        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public Icon Icon { get; set; }
    }
}