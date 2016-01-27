namespace Poker.Interfaces
{
    using Models;

    /// <summary>
    /// Interface
    /// </summary>
    public interface IGameStatus
    {
        /// <summary>
        /// Gets and sets the action.
        /// </summary>
        Actions Action { get; set; }

        /// <summary>
        /// Gets and sets the chips in the pot.
        /// </summary>
        int ChipsAddedToPot { get; set; }

       int AmountRaisedTo { get; set; }
    }
}
