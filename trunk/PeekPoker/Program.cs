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
                //TODO: Load Config.ini file, Load user agreement option, load and pass ip address aswell
                if(true)
                    Application.Run(new License.License());
                else
                {
                    //if agreed
                    Application.Run(new PeekPokerMainForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Peek Poker"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            
        }
    }
}
