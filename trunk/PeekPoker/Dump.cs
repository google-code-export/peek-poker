using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker
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
            InitializeComponent();
            _rtm = rtm;
        }

        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!dumpStartOffsetTextBox.Text.StartsWith("0x"))
                {
                    if (!dumpStartOffsetTextBox.Text.Equals(""))
                        dumpStartOffsetTextBox.Text = "0x" + uint.Parse(dumpStartOffsetTextBox.Text).ToString("X");
                }

                if (dumpLengthTextBox.Text.StartsWith("0x")) return;
                if (!dumpLengthTextBox.Text.Equals(""))
                    dumpLengthTextBox.Text = "0x" + uint.Parse(dumpLengthTextBox.Text).ToString("X");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DumpMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                _dumpFilePath = saveFileDialog.FileName;
                using (FileStream file = File.Create(_dumpFilePath)){file.Close();}
                
                Thread oThread = new Thread(DumpMem);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0x82000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0x40000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }


        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            dumpStartOffsetTextBox.Text = string.Format("0xC0000000");
            dumpLengthTextBox.Text = string.Format("0x1FFF0FFF");
        }

        private void DumpMem()
        {
            try
            {
                EnableControl(dumpMemoryButton, false);
                _rtm.Dump(_dumpFilePath, GetTextBoxText(dumpStartOffsetTextBox), GetTextBoxText(dumpLengthTextBox));
                UpdateProgressbar(0, 100, 0);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(dumpMemoryButton, true);
                UpdateProgressbar(0, 100, 0);
                Thread.CurrentThread.Abort();
            }
        }
    }
}
