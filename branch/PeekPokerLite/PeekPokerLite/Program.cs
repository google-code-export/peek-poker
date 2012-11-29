using System;
using System.Windows.Forms;
using PeekPoker.PeekPokerLitePartialClasses;

namespace PeekPoker
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PeekPokerLite());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Peek Poker Lite"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}