namespace Poker.Win
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Models;

    public  class Winner
    {
        private static int winners = 0;
        //static  List<string> CheckWinners = new List<string>();

        
        private Deck Deck;

        public static void WinnerMessege(double current, double Power, string currentText, int chips, string lastly, Image[] Deck, PictureBox[] Holder, Poker.Type sorted, List<string> CheckWinners)
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
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            
        }

        public static void NumbersOfWinners(string currentText, int chips, string lastly,List<string> CheckWinners, TextBox tbPot , int Chips,
            int  bot1Chips, int bot2Chips ,int bot3Chips,int bot4Chips,int bot5Chips,
            TextBox tbChips,TextBox tbBotChips5,TextBox tbBotChips4, TextBox tbBotChips3,TextBox tbBotChips2,TextBox tbBotChips1 ) //TextBox comes from Form1.Designer 
        {                                                                            
            if (currentText == lastly)//lastfixed                                   
            {                                                                        
                if (winners > 1)                                                     
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(tbPot.Text) / winners;
                        tbChips.Text = Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips1.Text = bot1Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips2.Text = bot2Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips3.Text = bot3Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips4.Text = bot4Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips5.Text = bot5Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }
    }
}
