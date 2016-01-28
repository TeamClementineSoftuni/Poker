namespace Poker.Interfaces
{
    /// <summary>
    ///   Interface for poker database.
    /// </summary>
    public interface IPokerDatabase
    {
        /// <summary>
        ///   Holds array from all players in database.
        /// </summary>
        IPlayer[] Players { get; }

        /// <summary>
        ///   Add new player in the Database.
        /// </summary>
        /// <param name="player">Player to add.</param>
        void AddPlayer(IPlayer player);
    }
}