using System.Linq;
using Poker.Models;

namespace Poker.Core
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Models.Players;

    public class Winner
    {
        public static void CheckWinners(Player[] players, string lastly, Image[] Deck, PictureBox[] Holder, List<Result> winnersTypes, List<Player> winners)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (Holder[j].Visible)
                    Holder[j].Image = Deck[j];
            }

            Result higherType = winnersTypes.OrderByDescending(op1 => op1.Type).ThenByDescending(op1 => op1.Power).First();

            foreach (var player in players)
            {
                if (player.Result.Type == higherType.Type)
                {
                    if (player.Result.Power == higherType.Power)
                    {
                        winners.Add(player);
                        if (player.Result.Type == -1)
                        {
                            MessageBox.Show(player.Name + " High Card ");
                        }
                        if (player.Result.Type == 1 || player.Result.Type == 0)
                        {
                            MessageBox.Show(player.Name + " Pair ");
                        }
                        if (player.Result.Type == 2)
                        {
                            MessageBox.Show(player.Name + " Two Pair ");
                        }
                        if (player.Result.Type == 3)
                        {
                            MessageBox.Show(player.Name + " Three of a Kind ");
                        }
                        if (player.Result.Type == 4)
                        {
                            MessageBox.Show(player.Name + " Straight ");
                        }
                        if (player.Result.Type == 5 || player.Result.Type == 5.5)
                        {
                            MessageBox.Show(player.Name + " Flush ");
                        }
                        if (player.Result.Type == 6)
                        {
                            MessageBox.Show(player.Name + " Full House ");
                        }
                        if (player.Result.Type == 7)
                        {
                            MessageBox.Show(player.Name + " Four of a Kind ");
                        }
                        if (player.Result.Type == 8)
                        {
                            MessageBox.Show(player.Name + " Straight Flush ");
                        }
                        if (player.Result.Type == 9)
                        {
                            MessageBox.Show(player.Name + " Royal Flush ! ");
                        }
                    }
                }
            }

            foreach (var winner in winners)
            {
                if (winner.Name == lastly)//lastfixed 
                {
                    if (winners.Count >= 1)
                    { 
                        winner.ChipsSet.Amount += Pot.Instance.ChipsSet.Amount / winners.Count;
                        winner.ChipsTextBox.Text = winner.ChipsSet.Amount.ToString();
                        Pot.Instance.ChipsSet.Amount -= winner.ChipsSet.Amount;
                        Pot.Instance.PotTextBox.Text = Pot.Instance.ChipsSet.Amount.ToString();
                    }
                }
            }
        }
    }
}
