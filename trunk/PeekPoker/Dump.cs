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
        public event SetTextBoxTextDelegateHandler SetTextBoxText;

        private RealTimeMemory _rtm;
        private string _dumpFilePath;

        public Dump(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
        }

        private void FixDumpLength(object sender, EventArgs e)
        {
            if (dumpLengthTextBox.Text.StartsWith("0x")) return;
            if (dumpLengthTextBox.Text.Equals("")) return;
            dumpLengthTextBox.Text = (string.Format("0x" + dumpLengthTextBox.Text));
            if (!System.Text.RegularExpressions.Regex.IsMatch(dumpLengthTextBox.Text.Substring(2), @"\A\b[0-9a-fA-F]+\b\Z"))
                dumpLengthTextBox.Clear();
        }

        private void FixDumpAddresses(object sender, EventArgs e)
        {
            if (dumpStartOffsetTextBox.Text.StartsWith("0x")) return;
            if (dumpStartOffsetTextBox.Text.Equals("")) return;
            dumpStartOffsetTextBox.Text = (string.Format("0x" + dumpStartOffsetTextBox.Text));
            if (!System.Text.RegularExpressions.Regex.IsMatch(dumpStartOffsetTextBox.Text.Substring(2), @"\A\b[0-9a-fA-F]+\b\Z"))
                dumpStartOffsetTextBox.Clear();
        }


        private void DumpMemoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                _dumpFilePath = saveFileDialog.FileName;
                FileStream file = File.Create(_dumpFilePath);
                file.Close();

                var oThread = new Thread(DumpMem);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BaseFileButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0x82000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }

        private void AllocatedDataButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0x40000000");
            dumpLengthTextBox.Text = string.Format("0x5000000");
        }


        private void PhysicalRamButtonClick(object sender, EventArgs e)
        {
            dumpMemoryButton.Text = string.Format("0xC0000000");
            dumpLengthTextBox.Text = string.Format("0x1FFF0FFF");
        }

        private void QuickCalculatorPlusButtonClick(object sender, EventArgs e)
        {
            try
            {
                //convert text to uint the format results to hex string
                SetTextBoxText(quickCalculatorAnswerTextBox,
                    String.Format("0x{0:X}",
                    Functions.Convert(GetTextBoxText(quickCalculatorValueOneTextBox)) +
                    Functions.Convert(GetTextBoxText(quickCalculatorValueTwoTextBox))));
            }
            catch (Exception ex)
            {
                quickCalculatorAnswerTextBox.Clear();
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuickCalculatorMinusButtonClick(object sender, EventArgs e)
        {
            try
            {
                //convert text to uint the format results to hex string
                quickCalculatorAnswerTextBox.Text = String.Format("0x{0:X}", Functions.Convert(quickCalculatorValueOneTextBox.Text) -
                                                     Functions.Convert(quickCalculatorValueTwoTextBox.Text));
            }
            catch (Exception ex)
            {
                quickCalculatorAnswerTextBox.Clear();
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DumpMem()
        {
            try
            {
                EnableControl(dumpMemoryButton, false);
                _rtm.Dump(_dumpFilePath, GetTextBoxText(dumpStartOffsetTextBox), GetTextBoxText(dumpLengthTextBox));
                UpdateProgressbar(0, 100, 0,"");
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(dumpMemoryButton, true);
                UpdateProgressbar(0, 100, 0,"");
                Thread.CurrentThread.Abort();
            }
        }
    }
}
