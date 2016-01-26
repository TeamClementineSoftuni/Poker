namespace Poker.Interfaces
{
    public interface IActsOnTable
    {
        /// <summary>
        /// Gets and set the raise of the player.
        /// </summary>
        double RaiseAmount { get; set; }

        /// <summary>
        /// Show is it raised.
        /// </summary>
        bool IsRaised { get; set; }

        /// <summary>
        /// Gets and set with how much the player should call.
        /// </summary>
        int CallAmount { get; set; }

        /// <summary>
        /// Hold how much rounds are passed.
        /// </summary>
        double RoundsPassed { get; set; }
    }
}
