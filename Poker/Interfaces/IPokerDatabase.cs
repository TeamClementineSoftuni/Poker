namespace Poker.Interfaces
{
    /// <summary>
    ///   Interface for poker database.
    /// </summary>
    public interface IPokerDatabase
    {
        /// <summary>
        ///   Holds all players.
        /// </summary>
        IPlayer[] Players { get; }

        /// <summary>
        ///   Add player.
        /// </summary>
        /// <param name="player">Player to add.</param>
        void AddPlayer(IPlayer player);
    }
}