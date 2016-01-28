namespace Poker.Interfaces
{
    /// <summary>
    ///   Interface for hand in the game.
    /// </summary>
    public interface IHand
    {
        /// <summary>
        ///   First card in a hand.
        /// </summary>
        ICard Card1 { get; set; }

        /// <summary>
        ///   Second card in a hand.
        /// </summary>
        ICard Card2 { get; set; }
    }
}