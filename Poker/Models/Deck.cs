namespace Poker.Models
{
    using System;
    using System.Diagnostics;
    using System.Drawing;

    using Poker.Constants;
    using Poker.Interfaces;

    public class Deck : IDeck
    {
        private static Deck instance;
        private readonly ICard[] cards = new Card[Common.NumberOfPlayingCards];

        private string cardsImagesLocation;

        private Deck(string cardsImagesLocation = Common.ImagesDefaultPath)
        {
            this.CardsImagesLocation = cardsImagesLocation;
            this.PopulateWithCards();
            this.Shuffle();
        }

        public static Deck Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Deck();
                }
                return instance;
            }
        }

        public ICard[] Cards
        {
            get
            {
                return this.cards;
            }
        }

        public string CardsImagesLocation
        {
            get
            {
                return this.cardsImagesLocation;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("CardsImagesLocation cannot be null or empty!");
                }

                this.cardsImagesLocation = value;
            }
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int currentIndex = this.cards.Length; currentIndex > 0; currentIndex--)
            {
                int randomIndex = random.Next(currentIndex);
                ICard randomCard = this.cards[randomIndex];
                this.cards[randomIndex] = this.cards[currentIndex - 1];
                this.cards[currentIndex - 1] = randomCard;
            }
        }

        private void PopulateWithCards()
        {
            int currentIndexOfDeck = 0;

            for (int suit = 0; suit < Common.NumberOfSuits; suit++)
            {
                for (int rank = 2; rank < Common.NumberOfCardsRanks + 2; rank++)
                {
                    Suits cardSuit = (Suits)suit;
                    CardsRank cardRank = (CardsRank)rank;

                    string cardImagePath = this.CardsImagesLocation + cardRank + cardSuit + ".png";
                    Image cardImage = Image.FromFile(cardImagePath);

                    this.cards[currentIndexOfDeck] = new Card(cardSuit, cardRank, cardImage);
                    currentIndexOfDeck++;
                }
            }
        }
    }
}