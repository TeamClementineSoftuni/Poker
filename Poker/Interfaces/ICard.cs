namespace Poker.Interfaces
{
    using System.Drawing;
    using Poker.Models;
    public interface ICard
    {
        /// <summary>
        /// Takes the card suit.
        /// </summary>
        Suits Suit { get; }

        /// <summary>
        /// Takes the card rank.
        /// </summary>
        CardsRank Rank { get; }

        /// <summary>
        /// Load card image.
        /// </summary>
        Image CardImage { get; }
    }
}
