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
        /// <returns></returns>
        IPlayer CreatePlayer();
    }
}