using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;
namespace Poker.Core
{
    public class HandEvaluator
    {
        private int straightRank = 0;

        public IResult Evaluate(IHand hand, ICard[] boardCards)
        {
            this.straightRank = 0;
            double resultType = 0;
            double resultPower = 0;
            List<ICard> allCards = new List<ICard>(boardCards);
            allCards.Add(hand.Card1);
            allCards.Add(hand.Card2);
            allCards = allCards.OrderBy(c => c.Rank).ToList();
            bool hasStraight = CheckForStraight(allCards);

            //TODO: case A2345
            bool hasFlush = false;
            int flushRank = 0;
            int cardsFromOneSuit = allCards.GroupBy(h => h.Suit).Select(group => group.Count()).FirstOrDefault();
            int flushSuit = 0;
            if (cardsFromOneSuit >= 5)
            {
                hasFlush = true;
                flushSuit = (int)allCards.GroupBy(ha => ha.Suit).Where(ha => ha.Count() == cardsFromOneSuit).Select(ha => ha.Key).FirstOrDefault();
                flushRank = (int)allCards.Where(c => (int)c.Suit == flushSuit).OrderBy(c => c.Rank).Select(c => c.Rank).LastOrDefault();
            }
            if (hasFlush && hasStraight)
            {
                bool hasStraightFlush = CheckForStraight(allCards.Where(c => (int)c.Suit == flushSuit).ToList());
                if (hasStraightFlush)
                {
                    resultType = 8;
                    resultPower = this.straightRank / 4.0 + resultType * 100;
                    return new Result(resultType, resultPower);
                }
            }
            int sameCardsCount = allCards.GroupBy(c => c.Rank).Select(group => group.Count()).Max();
            int sameCardsRank =
               (int)allCards.GroupBy(c => c.Rank)
                    .Where(group => group.Count() == sameCardsCount)
                    .Select(group => group.Key)
                    .Max();
            if (sameCardsCount == 4)
            {
                int kickerRank =
                   (int)allCards.Where(c => (int)c.Rank != sameCardsRank)
                        .OrderBy(c => c.Rank)
                        .Select(c => c.Rank)
                        .Max();
                resultType = 7;
                resultPower = sameCardsRank * 4 + kickerRank / 4.0 + resultType * 100;
                return new Result(resultType, resultPower);
            }
            if (sameCardsCount == 3)
            {
                int otherSameCardsCount = allCards.Where(c => (int)c.Rank != sameCardsRank).GroupBy(c => c.Rank).Select(group => group.Count()).Max();
                if (otherSameCardsCount >= 2)
                {
                    int otherSameCardsRank = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).GroupBy(c => c.Rank)
                                                .Where(group => group.Count() == otherSameCardsCount)
                                                .Select(group => group.Key)
                                                .Max();
                    resultType = 6;
                    resultPower = sameCardsRank * 4 + otherSameCardsRank / 4.0 + resultType * 100;
                    return new Result(resultType, resultPower);
                }
            }
            if (hasFlush)
            {
                resultType = 5;
                resultPower = flushRank + resultType * 100;
                return new Result(resultType, resultPower);
            }
            if (hasStraight)
            {
                resultType = 4;
                resultPower = straightRank + resultType * 100;
                return new Result(resultType, resultPower);
            }
            if (sameCardsCount == 3)
            {
                int kicker1 = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).Select(c => c.Rank).Max();
                int kicker2 = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).Select(c => c.Rank).Where(rank => (int)rank != kicker1).Max();
                resultType = 3;
                resultPower = sameCardsRank * 5 + (kicker1 + kicker2) / 8.0 + resultType * 100;
                return new Result(resultType, resultPower);
            }
            if (sameCardsCount == 2)
            {
                int otherSameCardsCount = allCards.Where(c => (int)c.Rank != sameCardsRank).GroupBy(c => c.Rank).Select(group => group.Count()).Max();
                if (otherSameCardsCount == 2)
                {
                    int otherSameCardsRank = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).GroupBy(c => c.Rank)
                                                .Where(group => group.Count() == otherSameCardsCount)
                                                .Select(group => group.Key)
                                                .Max();
                    int kicker =
                       (int)allCards.Where(c => (int)c.Rank != sameCardsRank && (int)c.Rank != otherSameCardsRank)
                            .Select(c => c.Rank)
                            .Max();
                    resultType = 2;
                    resultPower = sameCardsRank * 5 + otherSameCardsRank / 4.0 + kicker / 60.0 + resultType * 100;
                    return new Result(resultType, resultPower);
                }
                int kicker1 = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).Select(c => c.Rank).Max();
                int kicker2 = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).Select(c => c.Rank).Where(rank => (int)rank != kicker1).Max();
                int kicker3 = (int)allCards.Where(c => (int)c.Rank != sameCardsRank).Select(c => c.Rank).Where(rank => (int)rank != kicker1 && (int)rank != kicker2).Max();
                resultType = 1;
                resultPower = sameCardsRank * 5 + kicker1 / 4.0 + kicker2 / 60.0 + kicker3 / 1200.0 + resultType * 100;
                return new Result(resultType, resultPower);
            }
            int highCard1 = (int)allCards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Max();
            int highCard2 = (int)allCards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Skip(1).Max();
            int highCard3 = (int)allCards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Skip(2).Max();
            int highCard4 = (int)allCards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Skip(3).Max();
            int highCard5 = (int)allCards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Skip(4).Max();
            resultType = 0;
            resultPower = highCard1 + highCard2 + highCard3 + highCard4 + highCard5;
            return new Result(resultType, resultPower);
        }
        private bool CheckForStraight(IList<ICard> cards)
        {
            bool has2 = cards.Where(card => (int)card.Rank == 2).Count() > 0;
            bool has3 = cards.Where(card => (int)card.Rank == 3).Count() > 0;
            bool has4 = cards.Where(card => (int)card.Rank == 4).Count() > 0;
            bool has5 = cards.Where(card => (int)card.Rank == 5).Count() > 0;
            bool hasA = cards.Where(card => (int)card.Rank == 14).Count() > 0;
            bool hasNo6 = cards.Where(card => (int)card.Rank == 6).Count() == 0;

            if (has2 && has3 && has4 && has5 && hasA && hasNo6)
            {
                this.straightRank = 5;
                return true;
            }

            int straightCounter = 1;
            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[i].Rank - cards[i - 1].Rank == 1)
                {
                    straightCounter++;
                    if (straightCounter >= 5)
                    {
                        this.straightRank = (int)cards[i].Rank;
                        return true;
                    }
                }
                else if (cards[i].Rank - cards[i - 1].Rank != 0)
                {
                    straightCounter = 1;
                }
            }
            return false;
        }
    }
}