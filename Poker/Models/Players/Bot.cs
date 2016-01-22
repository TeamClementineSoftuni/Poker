using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Poker.Models.Players;
using System.Drawing;

namespace Poker.Models
{
    public class Bot : Player
    {
        private static int instanceCounter = 1;
        private const string DefaultBotName = "Bot ";

        public Bot(Point location)
            : base(location, DefaultBotName + instanceCounter)
        {
            instanceCounter++;
        }

        //public Actions Act(int street, int raisedToAmount, Card[] board)
        //{
        //    this.PrevRaise = this.Raise;
        //    List<Card> cards = new List<Card>() { this.Hand.Card1, this.Hand.Card2 };
        //    switch (street)
        //    {
        //        case 2:
        //            cards.Add(board[0]);
        //            cards.Add(board[1]);
        //            cards.Add(board[2]);
        //            break;
        //        case 3:
        //            cards.Add(board[0]);
        //            cards.Add(board[1]);
        //            cards.Add(board[2]);
        //            cards.Add(board[3]);
        //            break;
        //        case 4:
        //            cards.Add(board[0]);
        //            cards.Add(board[1]);
        //            cards.Add(board[2]);
        //            cards.Add(board[3]);
        //            cards.Add(board[4]);
        //            break;
        //    }
        //    int power = CalcPower(cards);
        //    //this.Power = power;//obsolete
        //    switch (power)
        //    {
        //        case 2:
        //            if (street < 3)
        //            {
        //                if (this.ChipsSet.Amount < 500)
        //                {
        //                    this.AllInWhenNotEnough();
        //                    return Actions.AllIn;
        //                }
        //                this.ChipsSet.Amount -= 500;
        //                this.Raise = 0;
        //                return Actions.Call;
        //            }
        //            else
        //            {
        //                return Actions.Check;
        //            }
        //        case 3:
        //            if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise)
        //            {
        //                this.AllInWhenNotEnough();
        //                return Actions.AllIn;
        //            }
        //            else
        //            {
        //                if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise + 1000)
        //                {
        //                    AllInWhenNotEnoughForTargetRaise(raisedToAmount);
        //                }
        //                else
        //                {
        //                    this.Raise = 1000;
        //                    this.ChipsSet.Amount -= raisedToAmount - this.PrevRaise - 1000;
        //                }
        //            }
        //            return Actions.Raise;
        //        case 4:
        //            if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise)
        //            {
        //                this.AllInWhenNotEnough();
        //                return Actions.AllIn;
        //            }
        //            else
        //            {
        //                if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise + 2000)
        //                {
        //                    AllInWhenNotEnoughForTargetRaise(raisedToAmount);
        //                }
        //                else
        //                {
        //                    this.Raise = 2000;
        //                    this.ChipsSet.Amount -= raisedToAmount - this.PrevRaise - 2000;
        //                }
        //            }
        //            return Actions.Raise;
        //        case 5:
        //            if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise)
        //            {
        //                this.AllInWhenNotEnough();
        //                return Actions.AllIn;
        //            }
        //            else
        //            {
        //                if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise + 3000)
        //                {
        //                    AllInWhenNotEnoughForTargetRaise(raisedToAmount);
        //                }
        //                else
        //                {
        //                    this.Raise = 3000;
        //                    this.ChipsSet.Amount -= raisedToAmount - this.PrevRaise - 3000;
        //                }
        //            }
        //            return Actions.Raise;
        //        case 6:
        //            if (this.ChipsSet.Amount < raisedToAmount - this.PrevRaise)
        //            {
        //                this.AllInWhenNotEnough();
        //                return Actions.AllIn;
        //            }
        //            this.Raise = this.ChipsSet.Amount - (raisedToAmount - this.PrevRaise);
        //            this.ChipsSet.Amount = 0;
        //            return Actions.Raise;
        //        default:
        //            this.IsFolded = true;
        //            return Actions.Fold;
        //    }
        //}

        private int CalcPower(List<Card> cards)
        {
            //bluffing bot :)
            return new Random().Next(1, 6);
        }

        private void AllInWhenNotEnough()
        {
            this.AllIn = this.ChipsSet.Amount;
            this.ChipsSet.Amount = 0;
            this.Raise = 0;
        }

        private void AllInWhenNotEnoughForTargetRaise(int raisedTo)
        {
            this.Raise = this.ChipsSet.Amount - this.PrevRaise - raisedTo;
            this.ChipsSet.Amount = 0;
        }
    }
}
