#region

using System.Windows.Forms;

#endregion

namespace PeekPoker.Interface
{
    public delegate void UpdateProgressBarHandler(int min, int max, int value, string text);

    public delegate void ShowMessageBoxHandler(
        string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

    public delegate string GetTextBoxTextHandler(Control control);

    public delegate void EnableControlHandler(Control control, bool value);

    public delegate void SetTextBoxTextDelegateHandler(Control control, string value);
}