namespace Poker.Interfaces
{
    public interface IDeck
    {
        /// <summary>
        /// Gets the location of the card image.
        /// </summary>
        string CardsImagesLocation { get; }

        /// <summary>
        /// Shuffle the deck.
        /// </summary>
        void Shuffle();

        /// <summary>
        /// Gets all cards.
        /// </summary>
        ICard[] Cards { get; }
    }
}
