#region

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace PeekPoker.Interface
{
    //======================================================//
    // Interface that would allow other applications/tools  //
    // to be plugged into the peekpoker application.        //
    // =====================================================//
    public interface IPlugin
    {
        #region App Properties

        /// <summary>Application name</summary>
        string ApplicationName { get; }

        /// <summary>Application's Description</summary>
        string Description { get; }

        /// <summary>The Author of the application</summary>
        string Author { get; }

        /// <summary>The version of the application</summary>
        string Version { get; }

        /// <summary>The application's Icon</summary>
        Icon Icon { get; }

        #endregion
    }
}