namespace Poker.Interfaces
{
    public interface IActsOnTable
    {
        /// <summary>
        /// Gets and sets the raise amount.
        /// </summary>
        double RaiseAmount { get; set; }

        /// <summary>
        /// Show if it was raised.
        /// </summary>
        bool IsRaised { get; set; }

        /// <summary>
        /// Gets and sets the call amount.
        /// </summary>
        int CallAmount { get; set; }

        /// <summary>
        /// Holds how many rounds are passed.
        /// </summary>
        double RoundsPassed { get; }
    }
}
