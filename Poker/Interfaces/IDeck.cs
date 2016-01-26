namespace Poker.Interfaces
{
    public interface IDeck
    {
        /// <summary>
        /// Gets the location of the card image.
        /// </summary>
        string CardsImagesLocation { get; }

        /// <summary>
        /// Gets the array of cards in a deck.
        /// </summary>
        ICard[] Cards { get; }

        /// <summary>
        /// Shuffled the deck.
        /// </summary>
        void Shuffle();       
    }
}
