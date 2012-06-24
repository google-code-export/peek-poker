using System;
using System.Windows;

namespace PeekPoker.Interface
{
    //======================================================//
    // Interface that would allow other applications/tools  //
    // to be plugged into the peekpoker application.        //
    // =====================================================//
    public interface IPlugin
    {
        #region App Functions
        /// <summary>Function to show the application or initialise it.</summary>
        /// <example>Display:- Form1 form = new Form1 -- form.showDialog();</example>
        void Display();
        #endregion

        #region App Properties
        /// <summary>Application name</summary>
        string ApplicationName { get; }
        /// <summary>Application's Description</summary>
        string Description { get; }
        /// <summary>The Author of the application</summary>
        string Author { get; }
        /// <summary>The version of the application</summary>
        string Version { get; }
        #endregion
    }
}
