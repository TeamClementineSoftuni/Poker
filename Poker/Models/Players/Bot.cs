using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Poker.Models.Players;
using System.Drawing;
using Poker.Interfaces;

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

        public Actions Act(int street, int raisedToAmount, ICard[] board)
        {
            this.PrevRaise = this.RaiseAmount;
            List<ICard> cards = new List<ICard>() { this.Hand.Card1, this.Hand.Card2 };
            switch (street)
            {
                case 2:
                    cards.Add(board[0]);
                    cards.Add(board[1]);
                    cards.Add(board[2]);
                    break;
                case 3:
                    cards.Add(board[0]);
                    cards.Add(board[1]);
                    cards.Add(board[2]);
                    cards.Add(board[3]);
                    break;
                case 4:
                    cards.Add(board[0]);
                    cards.Add(board[1]);
                    cards.Add(board[2]);
                    cards.Add(board[3]);
                    cards.Add(board[4]);
                    break;
            }
            int power = CalcPower(cards);
            //this.Power = power;//obsolete
            switch (power)
            {
                case 1:
                case 2:
                case 3:
                    if (hasEnoughChips(raisedToAmount))
                    {
                        if (raisedToAmount > this.RaiseAmount)
                        {
                            ApplyCallEffects(raisedToAmount);
                            return Actions.Call;
                        }
                        else
                        {
                            this.StatusLabel.Text = "Check";
                            return Actions.Check;
                        }
                    }
                    else
                    {
                        ApplyAllInEffects(raisedToAmount);
                        return Actions.AllIn;
                    }
                case 4:
                    if (hasEnoughChips(raisedToAmount - this.RaiseAmount + 1000 - 1))
                    {
                        ApplyRaiseEffects(raisedToAmount, 1000);
                        return Actions.Raise;
                    }
                    else
                    {
                        ApplyAllInEffects(raisedToAmount);
                        return Actions.AllIn;
                    }
                case 5:
                    if (hasEnoughChips(raisedToAmount - this.RaiseAmount + 2000 - 1))
                    {
                        ApplyRaiseEffects(raisedToAmount, 2000);
                        return Actions.Raise;
                    }
                    else
                    {
                        ApplyAllInEffects(raisedToAmount);
                        return Actions.AllIn;
                    }
                case 6:
                    if (hasEnoughChips(raisedToAmount - this.RaiseAmount + 3000 - 1))
                    {
                        ApplyRaiseEffects(raisedToAmount, 3000);
                        return Actions.Raise;
                    }
                    else
                    {
                        ApplyAllInEffects(raisedToAmount);
                        return Actions.AllIn;
                    }
                case 7:
                    if (raisedToAmount > 5000)
                    {
                        ApplyAllInEffects(raisedToAmount);
                        return Actions.AllIn;
                    }
                    else
                    {
                        ApplyFoldEffects();
                        return Actions.Fold;
                    }
                default:
                    if (raisedToAmount == this.RaiseAmount)
                    {
                        this.StatusLabel.Text = "Check";
                        return Actions.Check;
                    }

                    ApplyFoldEffects();
                    return Actions.Fold;
            }
        }
        private int CalcPower(List<ICard> cards)
        {
            //bluffing bot :)
            return new Random().Next(0, 10);
        }
        private void AllInWhenNotEnough()
        {
            this.AllInAmount = this.ChipsSet.Amount;
            this.ChipsSet.Amount = 0;
            this.RaiseAmount = 0;
        }
        private void AllInWhenNotEnoughForTargetRaise(int raisedTo)
        {
            this.RaiseAmount = this.ChipsSet.Amount - this.PrevRaise - raisedTo;
            this.ChipsSet.Amount = 0;
        }

        private void ApplyFoldEffects()
        {
            this.StatusLabel.Text = "Fold";
            this.IsFolded = true;
            this.Card1PictureBox.Visible = false;
            this.Card2PictureBox.Visible = false;
        }

        private void ApplyCallEffects(int amountRaisedTo)
        {
            this.StatusLabel.Text = "Call";
            this.ChipsSet.Amount = this.ChipsSet.Amount - (amountRaisedTo - this.RaiseAmount);
            this.PrevRaise = this.RaiseAmount;
            this.RaiseAmount = amountRaisedTo;
            this.ChipsTextBox.Text = this.ChipsSet.Amount.ToString();
        }

        private void ApplyRaiseEffects(int amountRaisedTo, int raiseAmount)
        {

            this.ChipsSet.Amount = this.ChipsSet.Amount - ((amountRaisedTo + raiseAmount) - this.RaiseAmount);
            this.PrevRaise = this.RaiseAmount;
            this.RaiseAmount = amountRaisedTo + raiseAmount;
            this.ChipsTextBox.Text = this.ChipsSet.Amount.ToString();

            this.StatusLabel.Text = "Raised to " + this.RaiseAmount;
        }

        private void ApplyAllInEffects(int amountRaisedTo)
        {
            this.AllInAmount = this.ChipsSet.Amount;
            this.RaiseAmount = this.PrevRaise + this.AllInAmount;
            this.ChipsSet.Amount = 0;
            this.ChipsTextBox.Text = this.ChipsSet.Amount.ToString();

            this.StatusLabel.Text = "All in";
            this.ChipsTextBox.Text = 0.ToString();
        }

        private bool hasEnoughChips(int chipsNeeded)
        {
            if (this.ChipsSet.Amount < chipsNeeded)
            {
                return false;
            }

            return true;
        }

    }
}