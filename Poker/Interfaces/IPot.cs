namespace Poker.Interfaces
{
    /// <summary>
    /// Interface for pot in the game.
    /// </summary>
    public interface IPot
    {
        /// <summary>
        /// Holds the chips set in the pot.
        /// </summary>
        IChipsSet ChipsSet { get; set; }
    }
}
