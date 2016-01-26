namespace Poker.UI
{
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class WindowsForms : IUserInterface
    {
        public void PrintMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
