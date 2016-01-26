namespace Poker.Interfaces
{
    public interface IHand
    {
        /// <summary>
        /// First card in the hand.
        /// </summary>
        ICard Card1 { get; set; }

        /// <summary>
        /// Second card in the hand.
        /// </summary>
        ICard Card2 { get; set; }

        /// <summary>
        /// Calculate the hand power.
        /// </summary>
        /// <returns>Hand power.</returns>
        int HandPower();

        /// <summary>
        /// Shows faces on the cards in hand.
        /// </summary>
        void ShowHand();
    }
}
