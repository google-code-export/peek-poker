using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace UFC3___Memory_Editor
{
    public class EntryPoint : AbstractIPlugin
    {
        #region IPlugin Members
        public override void Display(Form form)
        {
            Ufc3Form newForm = new Ufc3Form(base.IPAddress);
            newForm.MdiParent = form;
            base.FormName = newForm.Name;
            newForm.Show();
        }

        public override string ApplicationName
        {
            get { return "UFC 3"; }
        }

        public override string Description
        {
            get { return "UFC 3 xbox 360 real time memory editor"; }
        }

        public override string Author
        {
            get { return "PureIso"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }
        #endregion
    }
}
