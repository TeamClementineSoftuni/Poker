namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IPot
    {
        /// <summary>
        /// Get and set textbox for pot.
        /// </summary>
        TextBox PotTextBox { get; set; }

        /// <summary>
        /// Hold chips in pot.
        /// </summary>
        IChipsSet ChipsSet { get; set; }
    }
}
