namespace Poker.Win
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Models.Players;

    public class Winner
    {
        private static int winnersCounter = 0;

        public static void WinnerMessege(Player player, string lastly, Image[] Deck, PictureBox[] Holder, Poker.Type sorted, List<Player> winners)
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

            if (player.Type == sorted.Current)
            {
                if (player.Power == sorted.Power)
                {
                    winnersCounter++;
                    winners.Add(player);
                    if (player.Type == -1)
                    {
                        MessageBox.Show(player.Name + " High Card ");
                    }
                    if (player.Type == 1 || player.Type == 0)
                    {
                        MessageBox.Show(player.Name + " Pair ");
                    }
                    if (player.Type == 2)
                    {
                        MessageBox.Show(player.Name + " Two Pair ");
                    }
                    if (player.Type == 3)
                    {
                        MessageBox.Show(player.Name + " Three of a Kind ");
                    }
                    if (player.Type == 4)
                    {
                        MessageBox.Show(player.Name + " Straight ");
                    }
                    if (player.Type == 5 || player.Type == 5.5)
                    {
                        MessageBox.Show(player.Name + " Flush ");
                    }
                    if (player.Type == 6)
                    {
                        MessageBox.Show(player.Name + " Full House ");
                    }
                    if (player.Type == 7)
                    {
                        MessageBox.Show(player.Name + " Four of a Kind ");
                    }
                    if (player.Type == 8)
                    {
                        MessageBox.Show(player.Name + " Straight Flush ");
                    }
                    if (player.Type == 9)
                    {
                        MessageBox.Show(player.Name + " Royal Flush ! ");
                    }
                }
            }
        }


        public static void NumbersOfWinners(string lastly, List<Player> winners, TextBox tbPot)
        {
            foreach (var winner in winners)
            {
                if (winner.Name == lastly)//lastfixed 
                {
                    winner.ChipsSet.Amount += int.Parse(tbPot.Text) / winnersCounter;

                    if (winnersCounter > 1)
                    {
                        winner.ChipsTextBox.Text = winner.ChipsSet.Amount.ToString();
                    }
                }

            }
        }
    }
}
