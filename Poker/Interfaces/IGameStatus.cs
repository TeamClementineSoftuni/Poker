namespace Poker.Interfaces
{
    using Poker.Models;

    public interface IGameStatus
    {
        /// <summary>
        /// Gets and set the action.
        /// </summary>
        Actions Action { get; set; }

        /// <summary>
        /// Gets and set the chips in pot.
        /// </summary>
        int ChipsAddedToPot { get; set; }

       int AmountRaisedTo { get; set; }
    }
}
