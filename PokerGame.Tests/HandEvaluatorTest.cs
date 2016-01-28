using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Core;
using Poker.Interfaces;
using Poker.Models;

namespace PokerGame.Tests
{
    using Moq;

    [TestClass]
    public class HandEvaluatorTests
    {
        private static HandEvaluator handEvaluator = new HandEvaluator();
        private Hand hand;
        private Image mockedImageOfCard;
        private ICard[] board;


        [ClassInitialize]
        public static void TestInitializeHandEvaluator(TestContext testContext)
        {
            handEvaluator = new HandEvaluator();

            Assert.IsNotNull(handEvaluator, "handEvaluator should not be null!");
        }

        [TestInitialize]
        public void TestInitializeHand()
        {
            this.hand = new Hand();
            this.mockedImageOfCard = new Bitmap(4, 4);
            this.board = new Card[5];

            Assert.IsNotNull(board, "board should not be null!");
            Assert.IsNotNull(hand, "mockedImageOfCard should not be null!");
            Assert.IsNotNull(hand, "hand should not be null!");
        }

        [TestMethod]
        public void TestEvaluate_HighCard_ShouldReturnResultWithType0()
        {
            // Assign

            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Clubs, CardsRank.Six, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Nine, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Jack, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);


            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 0, "result.Type should be 0 for a High Card!");
            Debug.Assert(result.Power == (14 + 11 + 9 + 6 + 5), "result.Power should be 45!");
        }

        [TestMethod]
        public void TestEvaluate_PairKings_ShouldReturnResultType1()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Clubs, CardsRank.Six, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.King, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.King, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 1, "result.Type should be 1 for a pair!");
            Debug.Assert(Math.Round(result.Power, 9, MidpointRounding.AwayFromZero) == 168.604166667, "result.Power should be 168.604166667!");
        }

        [TestMethod]
        public void TestEvaluate_TwoPairs_ShouldReturnResultType2()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.King, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.King, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Hearts, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 2, "result.Type should be 2 for two pairs!");
            Debug.Assert(Math.Round(result.Power, 9, MidpointRounding.AwayFromZero) == 273.333333333, "result.Power should be 273.333333333!");
        }

        [TestMethod]
        public void TestEvaluate_ThreeOfAKind_ShouldReturnResultType3()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.King, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Ace, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Hearts, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 3, "result.Type should be 3 for three of a kind!");
            Debug.Assert(result.Power == 372.25, "result.Power should be 372.25!");
        }

        [TestMethod]
        public void TestEvaluate_Straight_ShouldReturnResultType4()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.Six, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Seven, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Hearts, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 4, "result.Type should be 4 for a straight!");
            Debug.Assert(result.Power == 407, "result.Power should be 407!");
        }

        [TestMethod]
        public void TestEvaluate_Flush_ShouldReturnResultType5()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.Six, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Diamonds, CardsRank.Seven, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Diamonds, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 5, "result.Type should be 5 for a flush!");
            Debug.Assert(result.Power == 514, "result.Power should be 514!");
        }

        [TestMethod]
        public void TestEvaluate_FullHouse_ShouldReturnResultType6()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Seven, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.Seven, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Seven, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Clubs, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 6, "result.Type should be 6 for a full house!");
            Debug.Assert(result.Power == 631.5, "result.Power should be 631.5!");
        }

        [TestMethod]
        public void TestEvaluate_FourOfAKind_ShouldReturnResultType7()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Seven, mockedImageOfCard);
            this.board[1] = new Card(Suits.Hearts, CardsRank.Seven, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Seven, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Seven, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Clubs, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 7, "result.Type should be 7 for four of a kind!");
            Debug.Assert(result.Power == 731.5, "result.Power should be 731.5!");
        }

        [TestMethod]
        public void TestEvaluate_StraightFlush_ShouldReturnResultType8()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Four, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Five, mockedImageOfCard);
            this.board[1] = new Card(Suits.Diamonds, CardsRank.Six, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Diamonds, CardsRank.Seven, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Seven, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Diamonds, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 8, "result.Type should be 8 for a straight flush!");
            Debug.Assert(result.Power == 801.75, "result.Power should be 801.75!");
        }

        [TestMethod]
        public void TestEvaluate_StraightTo5_ShouldReturnResultWithType4()
        {
            // Assign
            var mockHand = new Mock<IHand>();
            mockHand.Setup(h => h.Card1).Returns(new Card(Suits.Diamonds, CardsRank.Two, this.mockedImageOfCard));
            mockHand.Setup(h => h.Card2).Returns(new Card(Suits.Diamonds, CardsRank.Three, this.mockedImageOfCard));

            this.board[0] = new Card(Suits.Diamonds, CardsRank.Four, mockedImageOfCard);
            this.board[1] = new Card(Suits.Clubs, CardsRank.Five, this.mockedImageOfCard);
            this.board[2] = new Card(Suits.Clubs, CardsRank.Nine, this.mockedImageOfCard);
            this.board[3] = new Card(Suits.Spades, CardsRank.Jack, this.mockedImageOfCard);
            this.board[4] = new Card(Suits.Spades, CardsRank.Ace, this.mockedImageOfCard);

            // Act
            IResult result = handEvaluator.Evaluate(mockHand.Object, this.board);

            // Assert
            Debug.Assert(result.Type == 4, "result.Type should be 4 for straight!");
            Debug.Assert(result.Power == 405, "result.Power should be 405!");
        }
    }
}
