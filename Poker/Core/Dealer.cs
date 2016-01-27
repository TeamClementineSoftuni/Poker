namespace Poker.Core
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Linq;
    using Poker.Models;

    using Poker.Interfaces;

    public class Dealer
    {
        private IList<IPlayer> winners; 

        public void CheckWinners(IList<IPlayer> playersToShowDown)
        {
            double bestHand = playersToShowDown.OrderByDescending(player => player.Result.Power).Select(player => player.Result.Power).FirstOrDefault();
            this.winners = playersToShowDown.Where(player => player.Result.Power == bestHand).ToList();

            foreach (var player in winners)
            {

                if (player.Result.Type == 0)
                {
                    MessageBox.Show(player.Name + " High Card ");
                }
                if (player.Result.Type == 1)
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

                //MessageBox.Show(player.Result.Power.ToString());
                //MessageBox.Show(player.Hand.Card1.Rank.ToString() + player.Hand.Card2.Rank.ToString());
            }

           
        }

        public void DistributePot()
        {
             foreach (var winner in this.winners)
            {
                winner.ChipsSet.Amount += Pot.Instance.ChipsSet.Amount / winners.Count;
                winner.ChipsTextBox.Text = winner.ChipsSet.Amount.ToString();

            }

            Pot.Instance.ChipsSet.Amount = 0;
            Pot.Instance.PotTextBox.Text = 0.ToString();
        }
    }
}
