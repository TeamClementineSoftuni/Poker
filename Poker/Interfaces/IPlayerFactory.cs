namespace Poker.Interfaces
{
    /// <summary>
    ///   Interface for player factory.
    /// </summary>
    public interface IPlayerFactory
    {
        /// <summary>
        ///   Creates player.
        /// </summary>
        /// <returns>Returns new player</returns>
        IPlayer CreatePlayer();
    }
}