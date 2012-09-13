using System;
using System.Windows.Forms;

namespace PeekPoker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PeekPokerMainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            
        }
    }
}
