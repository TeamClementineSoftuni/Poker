using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;

namespace Poker.Models
{
    public class Deck : IDeck
    {
        //TODO: rename cards images files to fit the format --> example: AceSpades.png
        private const int SuitsCount = 4;
        private const int CardsRanksCount = 12;
        private readonly Card[] cards = new Card[52];
        private string cardsImagesLocation;

        public Deck(string cardsImagesLocation)
        {
            this.CardsImagesLocation = cardsImagesLocation;
            this.PopulateWithCards();
            this.Shuffle();
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

            for (int suit = 0; suit < 4; suit++)
            {
                for (int rank = 0; rank < 13; rank++)
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
                int RandomIndex = random.Next(currentIndex);
                Card randomCard = cards[RandomIndex];
                cards[RandomIndex] = cards[currentIndex - 1];
                cards[currentIndex - 1] = randomCard;
            }
        }

        public Card[] Cards
        {
            get { return this.cards; }
        }
    }
}
