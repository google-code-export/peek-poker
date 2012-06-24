using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeekPoker.Interface
{
    //======================================================//
    // Interface that would allow other applications/tools  //
    // to be plugged into the peekpoker application.        //
    // =====================================================//
    public interface IPlugin
    {
        #region plugin description
        string Name { get; }        // Plugin Name used for the Plugin Browser and Downloader/Updater
        string Description { get; } // Plugin Description for the About Window
        string Author { get; }      // Plugin Author(s)
        Version Version { get; }    // Plugin Version
        #endregion


    }
}
