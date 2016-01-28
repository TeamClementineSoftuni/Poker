namespace Poker.Interfaces
{
    /// <summary>
    ///   Interface for pot in the game.
    /// </summary>
    public interface IPot
    {
        /// <summary>
        ///   Gets and sets the chips set in the pot.
        /// </summary>
        IChipsSet ChipsSet { get; set; }

        /// <summary>
        ///   Gets and sets the amount to which should be raised to.
        /// </summary>
        int AmountRaisedTo { get; set; }
    }
}