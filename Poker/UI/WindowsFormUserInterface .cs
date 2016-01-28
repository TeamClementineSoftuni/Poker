namespace Poker.UI
{
    using System;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class WindowsFormUserInterface : IUserInterface
    {
        public void PrintMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void PrintMessage(string format, params string[] args)
        {
            MessageBox.Show(String.Format(format, (object)args));
        }
    }
}
