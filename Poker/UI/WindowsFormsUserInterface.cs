namespace Poker.UI
{
    using System.Windows.Forms;

    using Poker.Interfaces;
    public class WindowsFormsUserInterface : IMessagePrintable
    {
        public void PrintMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
