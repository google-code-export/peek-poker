﻿using System.Drawing;
using System.Windows.Forms;

namespace PeekPoker.Interface
{
    public abstract class AbstractPlugin : IPlugin
    {
        #region Delegate Handlers
        /// <summary>
        /// Show MessageBox - Thread Safe
        /// </summary>
        public ShowMessageBoxHandler ShowMessageBox { get; set; }
        /// <summary>
        /// Work with the progressbar - Thread Safe
        /// </summary>
        public UpdateProgressBarHandler UpdateProgressBar { get; set; }
        /// <summary>
        /// Enable Controls - Thread Safe
        /// </summary>
        public EnableControlHandler EnableControl { get; set; }
        /// <summary>
        /// Get a text from a textbox - Thread Safe
        /// </summary>
        public GetTextBoxTextHandler GetTextBoxText { get; set; }
        /// <summary>
        /// Set a text into a textbox - Thread Safe
        /// </summary>
        public SetTextBoxTextDelegateHandler SetTextBoxText { get; set; }
        #endregion

        protected AbstractPlugin()
        {
            this.ApplicationName = "Unavailable";
            this.Description = "Unavailable";
            this.Author = "Unavailable";
            this.Version = "Unavailable";
            this.Icon = null;
        }

        //Values Set From peek poker Application
        #region Abstract Plugin Methods
        /// <summary>
        /// Display Method - Should be overridden
        /// </summary>
        /// <param name="parent">The Peek Poker Application</param>
        public virtual void Display(Form parent)
        {
            Form form = IsMdiChild ?
                new Form { MdiParent = parent } : new Form();
            form.Show();
        }
        /// <summary>
        /// The real time memory property
        /// </summary>
        public RealTimeMemory Rtm { get; set; }
        /// <summary>
        /// Set or Get if the plugin loads as a MDI
        /// </summary>
        public bool IsMdiChild { get; set; }
        #endregion

        #region IPlugin
        /// <summary>
        /// The Application Name
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// The application Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Application Author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// The Application Version
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The Application Icon
        /// </summary>
        public Icon Icon { get; set; }
        #endregion
    }
}