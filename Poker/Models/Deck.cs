namespace Poker.Models
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using Constants;
    using Interfaces;

    public class Deck : IDeck
    {
        //TODO: rename cards images files to fit the format --> example: AceSpades.png
        private readonly ICard[] cards = new Card[Common.NumberOfPlayingCards];
        private string cardsImagesLocation;

        public Deck(string cardsImagesLocation)
        {
            this.CardsImagesLocation = cardsImagesLocation;
            this.PopulateWithCards();
            this.Shuffle();
        }

        public ICard[] Cards
        {
            get { return this.cards; }
        }

        public string CardsImagesLocation
        {
            get { return this.cardsImagesLocation; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("CardsImagesLocation", "CardsImagesLocation cannot be null or empty!");
                }

                this.cardsImagesLocation = value;
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

                    cards[currentIndexOfDeck] = new Card(cardSuit, cardRank, cardImage);
                    currentIndexOfDeck++;
                }
            }

            Debug.Assert(cards[0] != null, "Deck was not populated! No card at index 0!");
            Debug.Assert(cards[51] != null, "Deck doesn't have 52 cards!");
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int currentIndex = cards.Length; currentIndex > 0; currentIndex--)
            {
                int randomIndex = random.Next(currentIndex);
                ICard randomCard = cards[randomIndex];
                cards[randomIndex] = cards[currentIndex - 1];
                cards[currentIndex - 1] = randomCard;
            }
        }   
    }
}
