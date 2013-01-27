using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.Dump
{
    public partial class Dump : Form
    {
        public event ShowMessageBoxHandler ShowMessageBox;
        public event UpdateProgressBarHandler UpdateProgressbar;
        public event EnableControlHandler EnableControl;
        public event GetTextBoxTextHandler GetTextBoxText;
        private RealTimeMemory _rtm;
        private string _dumpFilePath;

        public Dump(RealTimeMemory rtm)
        {
            this.InitializeComponent();
            this._rtm = rtm;
        }

        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!Functions.IsHex(dumpStartOffsetTextBox.Text))
                {
                    if (!this.dumpStartOffsetTextBox.Text.Equals(""))
                        this.dumpStartOffsetTextBox.Text = uint.Parse(this.dumpStartOffsetTextBox.Text).ToString("X");
                }

                if (Functions.IsHex(dumpLengthTextBox.Text)) return;
                if (!this.dumpLengthTextBox.Text.Equals(""))
                    this.dumpLengthTextBox.Text = uint.Parse(this.dumpLengthTextBox.Text).ToString("X");
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DumpMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                this._dumpFilePath = saveFileDialog.FileName;
                using (FileStream file = File.Create(this._dumpFilePath)){file.Close();}
                
                Thread oThread = new Thread(this.DumpMem);
                oThread.Start();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            this.dumpStartOffsetTextBox.Text = string.Format("82000000");
            this.dumpLengthTextBox.Text = string.Format("5000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            this.dumpStartOffsetTextBox.Text = string.Format("40000000");
            this.dumpLengthTextBox.Text = string.Format("5000000");
        }


        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            this.dumpStartOffsetTextBox.Text = string.Format("C0000000");
            this.dumpLengthTextBox.Text = string.Format("1FFF0FFF");
        }

        private void DumpMem()
        {
            try
            {
                this.EnableControl(this.dumpMemoryButton, false);
                this._rtm.Dump(this._dumpFilePath, this.GetTextBoxText(this.dumpStartOffsetTextBox), this.GetTextBoxText(this.dumpLengthTextBox));
                this.UpdateProgressbar(0, 100, 0);
            }
            catch (Exception e)
            {
                this.ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.EnableControl(this.dumpMemoryButton, true);
                this.UpdateProgressbar(0, 100, 0);
                Thread.CurrentThread.Abort();
            }
        }
    }
}
