namespace Poker.Models
{
    using System.Drawing;

    using Poker.Interfaces;

    public class Card : ICard
    {
        public Card(Suits suit, CardsRank rank, Image cardImage)
        {
            this.Suit = suit;
            this.Rank = rank;
            this.CardImage = cardImage;
        }

        public Suits Suit { get; private set; }

        public CardsRank Rank { get; private set; }

        public Image CardImage { get; private set; }
    }
}