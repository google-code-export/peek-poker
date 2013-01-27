using System.Windows.Forms;

namespace PeekPoker.License
{
    public partial class License : Form
    {
        public License()
        {
            InitializeComponent();
        }

        private void agreeButton_Click(object sender, System.EventArgs e)
        {
            PeekPokerMainForm form = new PeekPokerMainForm();
            form.Show();
            this.Hide();
        }

        private void notAgreeButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
