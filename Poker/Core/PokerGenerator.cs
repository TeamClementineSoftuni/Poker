using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Poker.Models;

namespace Poker.Core
{
    public class PokerGenerator
    {

        Panel pPanel = new Panel(); Panel b1Panel = new Panel(); Panel b2Panel = new Panel(); Panel b3Panel = new Panel();
        Panel b4Panel = new Panel(); Panel b5Panel = new Panel();

        public int Chips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;

        double type, rounds = 0, b1Power, b2Power, b3Power, b4Power, b5Power, pPower = 0, pType = -1, Raise = 0,
        b1Type = -1, b2Type = -1, b3Type = -1, b4Type = -1, b5Type = -1;

        bool B1turn = false, B2turn = false, B3turn = false, B4turn = false, B5turn = false;
        bool B1Fturn = false, B2Fturn = false, B3Fturn = false, B4Fturn = false, B5Fturn = false;

        bool pFolded, b1Folded, b2Folded, b3Folded, b4Folded, b5Folded, intsadded, changed;

        int pCall = 0, b1Call = 0, b2Call = 0, b3Call = 0, b4Call = 0, b5Call = 0, pRaise = 0, b1Raise = 0, b2Raise = 0, b3Raise = 0, b4Raise = 0, b5Raise = 0;


        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        bool PFturn = false, Pturn = true, restart = false, raising = false;
        Poker.Type sorted;
        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

        async Task Shuffle()
        {
            bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            bCall.Enabled = false;
            bRaise.Enabled = false;
            bFold.Enabled = false;
            bCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (i = ImgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }
            for (i = 0; i < 17; i++)
            {

                Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }
                Reserve[i] = int.Parse(ImgLocation[i]) - 1;
                Holder[i] = new PictureBox();
                Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                Holder[i].Height = 130;
                Holder[i].Width = 80;
                this.Controls.Add(Holder[i]);
                Holder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (Holder[0].Tag != null)
                    {
                        Holder[1].Tag = Reserve[1];
                    }
                    Holder[0].Tag = Reserve[0];
                    Holder[i].Image = Deck[i];
                    Holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += Holder[i].Width;
                    this.Controls.Add(pPanel);
                    pPanel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
                    pPanel.BackColor = Color.DarkBlue;
                    pPanel.Height = 150;
                    pPanel.Width = 180;
                    pPanel.Visible = false;
                }


                if (bot1Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (Holder[2].Tag != null)
                        {
                            Holder[3].Tag = Reserve[3];
                        }
                        Holder[2].Tag = Reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(b1Panel);
                        b1Panel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
                        b1Panel.BackColor = Color.DarkBlue;
                        b1Panel.Height = 150;
                        b1Panel.Width = 180;
                        b1Panel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (bot2Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (Holder[4].Tag != null)
                        {
                            Holder[5].Tag = Reserve[5];
                        }
                        Holder[4].Tag = Reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(b2Panel);
                        b2Panel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
                        b2Panel.BackColor = Color.DarkBlue;
                        b2Panel.Height = 150;
                        b2Panel.Width = 180;
                        b2Panel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (bot3Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (Holder[6].Tag != null)
                        {
                            Holder[7].Tag = Reserve[7];
                        }
                        Holder[6].Tag = Reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(b3Panel);
                        b3Panel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
                        b3Panel.BackColor = Color.DarkBlue;
                        b3Panel.Height = 150;
                        b3Panel.Width = 180;
                        b3Panel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (bot4Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (Holder[8].Tag != null)
                        {
                            Holder[9].Tag = Reserve[9];
                        }
                        Holder[8].Tag = Reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(b4Panel);
                        b4Panel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
                        b4Panel.BackColor = Color.DarkBlue;
                        b4Panel.Height = 150;
                        b4Panel.Width = 180;
                        b4Panel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (bot5Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (Holder[10].Tag != null)
                        {
                            Holder[11].Tag = Reserve[11];
                        }
                        Holder[10].Tag = Reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(b5Panel);
                        b5Panel.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
                        b5Panel.BackColor = Color.DarkBlue;
                        b5Panel.Height = 150;
                        b5Panel.Width = 180;
                        b5Panel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    Holder[12].Tag = Reserve[12];
                    if (i > 12) Holder[13].Tag = Reserve[13];
                    if (i > 13) Holder[14].Tag = Reserve[14];
                    if (i > 14) Holder[15].Tag = Reserve[15];
                    if (i > 15)
                    {
                        Holder[16].Tag = Reserve[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (Holder[i] != null)
                    {
                        Holder[i].Anchor = AnchorStyles.None;
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (bot1Chips <= 0)
                {
                    B1Fturn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    B1Fturn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }
                if (bot2Chips <= 0)
                {
                    B2Fturn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    B2Fturn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }
                if (bot3Chips <= 0)
                {
                    B3Fturn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    B3Fturn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }
                if (bot4Chips <= 0)
                {
                    B4Fturn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    B4Fturn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }
                if (bot5Chips <= 0)
                {
                    B5Fturn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    B5Fturn = false;
                    if (i == 11)
                    {
                        if (Holder[11] != null)
                        {
                            Holder[10].Visible = true;
                            Holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                bRaise.Enabled = true;
                bCall.Enabled = true;
                bRaise.Enabled = true;
                bRaise.Enabled = true;
                bFold.Enabled = true;
            }
        }

    }
}
