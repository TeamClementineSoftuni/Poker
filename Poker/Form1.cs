﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using Poker.Constants;
using Poker.Models;
using Poker.Models.Players;


namespace Poker
{
    using Poker.Core;

    public partial class Form1 : Form
    {
        // parallel branch
        private Card[] board = new Card[5];
        private Deck deck = new Deck("Assets\\Cards\\RenamedCards\\");
        private Player[] players = new Player[6];
        private List<Bot> playersNotFolded = new List<Bot>(); // Bot object should hava indicators for that(by Maria)
        private List<Bot> playersLeftToAct = new List<Bot>();

        private List<TextBox> playersChipsTextBoxs = new List<TextBox>();
        private List<Player> listOfWinners = new List<Player>();
        private List<Label> playersStatusLabel = new List<Label>();

        private List<Type> winnersTypes = new List<Type>();

        //TODO: initialize arrays and lists
        // parallel branch

        #region Variables
        public int Nm;

        int call = 500, foldedPlayers = 5;

        private double type;
        double rounds = 0, Raise = 0;

        bool intsadded, changed;

        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;

        List<bool?> bools = new List<bool?>();
        
        List<int> ints = new List<int>();

        bool restart = false, raising = false;

        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/

        int[] Reserve = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] Holder = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();
        int t = 60, i, bb = 500, sb = 250, up = 10000000, turnCount = 0;

        #endregion

        public Form1()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            call = bb;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            InitializeComponent();
            //parallel branch
            InitializePlayersComponents();
            DealCards();
            //ProcessHand();
            // parallel

            width = this.Width;
            height = this.Height;
            Shuffle();

            Pot.Instance.PotTextBox = potTextBox;
            Pot.Instance.PotTextBox.Enabled = false;

            timer.Interval = (1 * 1 * 1000);
            timer.Tick += timer_Tick;
            Updates.Interval = (1 * 1 * 100);
            Updates.Tick += Update_Tick;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = false;
            tbSB.Visible = false;
            bBB.Visible = false;
            bSB.Visible = false;
            tbRaise.Text = (bb * 2).ToString();
        }


        // parallel
        private void DealCards()
        {
            Point humanLocation = Locations.PlayersLocations[0];
            players[0] = new Human(humanLocation);

            for (int bot = 1; bot < players.Length; bot++)
            {
                Point botLocation = Locations.PlayersLocations[bot];
                players[bot] = new Bot(botLocation);
            }

            // Set the panel,ChipsTextBox and StatusLabel for every player
            for (int index = 0; index < players.Length; index++)
            {
                this.Controls.Add(players[index].Panel);

                players[index].ChipsTextBox = playersChipsTextBoxs[index];
                players[index].ChipsTextBox.Enabled = false;
                players[index].ChipsTextBox.Text = players[index].ChipsSet.ToString();

                this.players[index].StatusLabel = this.playersStatusLabel[index];


            }

            // give 2 cards to every player  --> the cards are taken from the deck;
            for (int botIndex = 0; botIndex < players.Length; botIndex++)
            {
                if (players[botIndex].ChipsSet.Amount != 0)
                {
                    players[botIndex].Hand.Card1 = deck.Cards[2 * botIndex];
                    players[botIndex].Hand.Card2 = deck.Cards[2 * botIndex + 1];
                }
            }

            // we have given every player 2 cards ( 6 * 2 = 12), so the first 12 cards from the deck are already reserved. 
            // Now we reserve 5 more cards for the board (the five cards visible by every player). 
            for (int index = 12; index < 12 + 5; index++)
            {
                board[index - 12] = deck.Cards[index];
            }
        }

        // The whole method has to be refactured, eventually getting rid of loops by extacting methods 
        // and somehow getting rid of if-s with polymorphism i guess?
        //private void ProcessHand()
        //{
        //    int amountRaisedTo = 0;
        //    int prevAmountraisedTo = 0;

        //    for (int street = 0; street < 4; street++)
        //    {
        //        if (street == 0)
        //        {
        //            amountRaisedTo = 500;//or use BB or SB
        //        }
        //        if (((street == 1) && amountRaisedTo > 500) || street > 1)
        //        {
        //            amountRaisedTo -= prevAmountraisedTo;
        //        }

        //        for (int player = 1; player < players.Length; player++)
        //        {
        //            //activate next line when we run the game through this...
        //            // MessageBox.Show("Bot "+player+"'s Turn");
        //            if (!players[player].IsFolded || players[player].ChipsSet.Amount > 0)
        //            {
        //                Actions act = ((Bot)players[player]).Act(street, amountRaisedTo, board);
        //                switch (act)
        //                {
        //                    case Actions.Fold:
        //                        //players[player].StatusLabel.Text = "Fold";
        //                        break;
        //                    case Actions.Check:
        //                        //set card if last player
        //                        //players[player].StatusLabel.Text = "Check";
        //                        break;
        //                    case Actions.Call:
        //                        //players[player].StatusLabel.Text = "Call";
        //                        //players[player].ChipsTextBox.Text = ""+players[player].Chips;
        //                        //Pot.Instance.ChipsSet.Amount += amountRaisedTo - players[player].PrevRaise;
        //                        break;
        //                    case Actions.Raise:
        //                        //Pot.Instance.ChipsSet.Amount += amountRaisedTo - players[player].PrevRaise + players[player].Raise;
        //                        //amountRaisedTo += players[player].Raise;
        //                        //set bot textfield for raise to players[player].Raise
        //                        //set bot textfield for action text Raise
        //                        //players[player].StatusLabel.Text = "Raise "+players[player].Raise;
        //                        break;
        //                    case Actions.AllIn:
        //                        //Pot.Instance.ChipsSet.Amount += players[player].AllIn;
        //                        //players[player].StatusLabel.Text = "All In";
        //                        break;
        //                }
        //            }
        //        }
        //        if (street == 3)
        //        {
        //            //show all bots cards which are not Fold
        //        }
        //        if (street == 1)
        //        {
        //            board[0].Show();
        //            board[1].Show();
        //            board[2].Show();
        //        }
        //        if ((street == 2) && (prevAmountraisedTo == amountRaisedTo))
        //        {
        //            board[3].Show();
        //        }
        //        if ((street == 3) && (prevAmountraisedTo == amountRaisedTo))
        //        {
        //            board[4].Show();
        //        }
        //        prevAmountraisedTo = amountRaisedTo;

        //        //while (playersLeftToAct.Count != 0)
        //        //{
        //        //    foreach (var player in playersLeftToAct)
        //        //    {
        //        //        GameStatus currentGameStatus = player.Act(street, amountRaisedTo);

        //        //        if (currentGameStatus.Action == Actions.Fold)
        //        //        {
        //        //            this.playersLeftToAct.Remove(player);
        //        //        }
        //        //        else if (currentGameStatus.Action == Actions.Raise)
        //        //        {
        //        //            amountRaisedTo = currentGameStatus.AmountRaisedTo;
        //        //        }

        //        //        this.pot += currentGameStatus.ChipsAddedToPot;
        //        //    }
        //        //}

        //        // TODO: figure out how to show common cards (board) after each street. Maybe solved?
        //        // after the first street 3 cards are shown (flop)
        //        //after the last street, no more cards from the board are left to be shown. That's why
        //        //there are 2 conditions in the loop
        //        for (int boardCard = 0; boardCard < street + 3 && boardCard < board.Length; boardCard++)
        //        {
        //            //Show() method is not implemented
        //            board[boardCard].Show();
        //        }
        //    }
        //}
        //parallel

        async Task Shuffle()
        {
            foreach (var player in players)
            {
                bools.Add(player.FoldedTurn);
            }

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
                }
                if (players[1].ChipsSet.Amount > 0)
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
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (players[2].ChipsSet.Amount > 0)
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
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (players[3].ChipsSet.Amount > 0)
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
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (players[4].ChipsSet.Amount > 0)
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
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (players[5].ChipsSet.Amount > 0)
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
                if (players[1].ChipsSet.Amount <= 0)
                {
                    this.players[1].FoldedTurn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    this.players[1].FoldedTurn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }
                if (players[2].ChipsSet.Amount <= 0)
                {
                    this.players[2].FoldedTurn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    this.players[2].FoldedTurn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }
                if (players[3].ChipsSet.Amount <= 0)
                {
                    this.players[3].FoldedTurn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    this.players[3].FoldedTurn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }
                if (players[4].ChipsSet.Amount <= 0)
                {
                    this.players[4].FoldedTurn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    this.players[4].FoldedTurn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }
                if (players[5].ChipsSet.Amount <= 0)
                {
                    this.players[5].FoldedTurn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    this.players[5].FoldedTurn = false;
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


        async Task Turns()
        {
            #region Rotating
            if (!this.players[0].FoldedTurn)
            {
                if (this.players[0].Turn)
                {
                    FixCall(this.players[0], 1);
                    //MessageBox.Show("Player's Turn");
                    pbTimer.Visible = true;
                    pbTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
                    timer.Start();
                    bRaise.Enabled = true;
                    bCall.Enabled = true;
                    bRaise.Enabled = true;
                    bRaise.Enabled = true;
                    bFold.Enabled = true;
                    turnCount++;
                    FixCall(this.players[0], 2);
                }
            }
            if (this.players[0].FoldedTurn || !this.players[0].Turn)
            {
                await AllIn();
                if (this.players[0].FoldedTurn && !players[0].IsFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        players[0].IsFolded = true;
                    }
                }
                await CheckRaise(0, 0);
                pbTimer.Visible = false;
                bRaise.Enabled = false;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                timer.Stop();
                this.players[1].Turn = true;

                int cardCount = 2;
                for (int index = 0; index < players.Length; index++)
                {
                    if (players[index] is Bot)
                    {
                        if (!this.players[index].FoldedTurn)
                        {
                            if (this.players[index].Turn)
                            {
                                FixCall(this.players[index], 1);
                                FixCall(this.players[index], 2);
                                Rules.Apply(cardCount, cardCount + 1, players[index], Reserve, Holder, winnersTypes, type);
                                MessageBox.Show(players[index].Name + "'s Turn");
                                ArtificialIntelligence.ArtificialIntelligence.AI(cardCount, cardCount + 1, players[index], 0, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising);
                                Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();
                                turnCount++;
                                last = index;
                                this.players[index].Turn = false;
                                if (index != players.Length - 1)
                                {
                                    this.players[index + 1].Turn = true;
                                }

                            }

                        }

                        if (this.players[index].FoldedTurn && !players[index].IsFolded)
                        {
                            bools.RemoveAt(index);
                            bools.Insert(index, null);
                            maxLeft--;
                            players[index].IsFolded = true;
                        }
                        if (this.players[index].FoldedTurn || !this.players[index].Turn)
                        {
                            await CheckRaise(index, index);

                            if (index != players.Length - 1)
                            {
                                this.players[index + 1].Turn = true;
                            }
                            else
                            {
                                players[0].Turn = true;
                            }

                        }

                        cardCount += 2;
                    }
                }


                if (this.players[0].FoldedTurn && !players[0].IsFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        players[0].IsFolded = true;
                    }
                }
            #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        foreach (var player in players)
                        {
                            if (!player.FoldedTurn)
                            {
                                player.StatusLabel.Text = "";
                            }

                        }
                    }
                }
            }
            if (rounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];

                        foreach (var player in this.players)
                        {
                            player.Call = 0;
                            player.Raise = 0;
                        }
                    }
                }
            }
            if (rounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];

                        foreach (var player in this.players)
                        {
                            player.Call = 0;
                            player.Raise = 0;
                        }
                    }
                }
            }
            if (rounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        foreach (var player in this.players)
                        {
                            player.Call = 0;
                            player.Raise = 0;
                        }
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                int cardIndex = 0;
                foreach (var player in players)
                {
                    if (!player.StatusLabel.Text.Contains("Fold"))
                    {
                        fixedLast = player.Name;
                        Rules.Apply(cardIndex, cardIndex + 1, player, Reserve, Holder, winnersTypes, type);
                    }
                    cardIndex += 2;
                }
                
                    Winner.CheckWinners(players, fixedLast, this.Deck, this.Holder, winnersTypes, this.listOfWinners);
                  

                restart = true;
                this.players[0].Turn = true;
                foreach (var player in players)
                {
                    player.FoldedTurn = false;
                }

                if (players[0].ChipsSet.Amount <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        foreach (var player in players)
                        {
                            if (player is Bot)
                            {
                                player.ChipsSet.Amount += f2.a;
                            }
                            else
                            {
                                player.ChipsSet.Amount = f2.a;
                            }
                        }

                        this.players[0].FoldedTurn = false;
                        this.players[0].Turn = true;
                        bRaise.Enabled = true;
                        bFold.Enabled = true;
                        bCheck.Enabled = true;
                        bRaise.Text = "Raise";
                    }
                }

                foreach (var player in this.players)
                {
                    player.Panel.Visible = false;
                    player.Power = 0;
                    player.Type = -1;
                    player.Call = 0;
                    player.Raise = 0;
                }
                last = 0;
                call = bb;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                type = 0;

                ints.Clear();
                listOfWinners.Clear();
                winners = 0;
                winnersTypes.Clear();

                for (int os = 0; os < 17; os++)
                {
                    Holder[os].Image = null;
                    Holder[os].Invalidate();
                    Holder[os].Visible = false;
                }

                Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();
                this.players[0].StatusLabel.Text = "";
                await Shuffle();
                await Turns();
            }
        }


        void FixCall(Player player, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (player.StatusLabel.Text.Contains("Raise"))
                    {
                        var changeRaise = player.StatusLabel.Text.Substring(6);
                        player.Raise = int.Parse(changeRaise);
                    }
                    if (player.StatusLabel.Text.Contains("Call"))
                    {
                        var changeCall = player.StatusLabel.Text.Substring(5);
                        player.Call = int.Parse(changeCall);
                    }
                    if (player.StatusLabel.Text.Contains("Check"))
                    {
                        player.Raise = 0;
                        player.Call = 0;
                    }
                }
                if (options == 2)
                {
                    if (player.Raise != Raise && player.Raise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - player.Raise;
                    }
                    if (player.Call != call || player.Call <= call)
                    {
                        call = call - player.Call;
                    }
                    if (player.Raise == Raise && Raise > 0)
                    {
                        call = 0;
                        bCall.Enabled = false;
                        bCall.Text = "Callisfuckedup";
                    }
                }
            }
        }


        async Task AllIn()
        {
            #region All in
            if (players[0].ChipsSet.Amount <= 0 && !intsadded)
            {
                if (this.players[0].StatusLabel.Text.Contains("Raise"))
                {
                    ints.Add(players[0].ChipsSet.Amount);
                    intsadded = true;
                }
                if (this.players[0].StatusLabel.Text.Contains("Call"))
                {
                    ints.Add(players[0].ChipsSet.Amount);
                    intsadded = true;
                }
            }
            intsadded = false;

            for (int index = 0; index < players.Length; index++)
            {
                if (this.players[index] is Bot)
                {
                    if (players[index].ChipsSet.Amount <= 0 && !players[index].FoldedTurn)
                    {
                        if (!intsadded)
                        {
                            ints.Add(players[index].ChipsSet.Amount);
                            intsadded = true;
                        }
                        if (index != this.players.Length - 1)
                        {
                            intsadded = false;
                        }

                    }
                }
            }



            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                for (int playerNumber = 0; playerNumber < players.Length; playerNumber++)
                {
                    if (index == playerNumber)
                    {
                        players[playerNumber].ChipsSet.Amount += Pot.Instance.ChipsSet.Amount;
                        players[0].ChipsTextBox.Text = players[playerNumber].ChipsSet.ToString();
                        this.players[playerNumber].Panel.Visible = true;
                        MessageBox.Show(players[playerNumber].Name + " Wins");
                    }
                }

                for (int j = 0; j <= 16; j++)
                {
                    Holder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= End)
            {
                await Finish(2);
            }
            #endregion
        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            foreach (var player in players)
            {
                player.Panel.Visible = false;
                player.Power = 0;
                player.Type = -1;
            }

            call = bb; Raise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0;
            Raise = 0;



            foreach (var player in this.players)
            {
                player.FoldedTurn = false;
                player.IsFolded = false;
                player.Call = 0;
                player.Raise = 0;
                if (player is Bot)
                {
                    player.Turn = false;
                }
                else
                {
                    player.Turn = true;
                }
            }

            restart = false; raising = false;
            height = 0; width = 0; winners = 0; Flop = 1; Turn = 2; River = 3; End = 4; maxLeft = 6;
            last = 123; raisedTurn = 1;
            bools.Clear();
            listOfWinners.Clear();
            ints.Clear();
            winnersTypes.Clear();

            Pot.Instance.ChipsSet.Amount = 0;
            t = 60; up = 10000000; turnCount = 0;

            foreach (var player in this.players)
            {
                player.StatusLabel.Text = "";
            }

            if (players[0].ChipsSet.Amount <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {

                    foreach (var player in players)
                    {
                        if (player is Bot)
                        {
                            player.ChipsSet.Amount += f2.a;
                        }
                        else
                        {
                            player.ChipsSet.Amount = f2.a;
                        }
                    }

                    this.players[0].FoldedTurn = false;
                    this.players[0].Turn = true;
                    bRaise.Enabled = true;
                    bFold.Enabled = true;
                    bCheck.Enabled = true;
                    bRaise.Text = "Raise";
                }
            }
            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                Holder[os].Image = null;
                Holder[os].Invalidate();
                Holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            winnersTypes.Clear();

            string fixedLast = "qwerty";

            int cardIndex = 0;
            foreach (var player in players)
            {
                if (!player.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = player.Name;
                    Rules.Apply(cardIndex, cardIndex + 1, player, Reserve, Holder, winnersTypes, type);
                }
                cardIndex += 2;
            }

                Winner.CheckWinners(players, fixedLast, this.Deck, this.Holder, winnersTypes, this.listOfWinners);
        }

        // ---> Events in separate folder
        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (pbTimer.Value <= 0)
            {
                this.players[0].FoldedTurn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                pbTimer.Value = (t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            foreach (var player in players)
            {
                player.ChipsTextBox.Text = player.ChipsSet.ToString();
            }

            if (players[0].ChipsSet.Amount <= 0)
            {
                this.players[0].Turn = false;
                this.players[0].FoldedTurn = true;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                bCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (players[0].ChipsSet.Amount >= call)
            {
                bCall.Text = "Call " + call.ToString();
            }
            else
            {
                bCall.Text = "All in";
                bRaise.Enabled = false;
            }
            if (call > 0)
            {
                bCheck.Enabled = false;
            }
            if (call <= 0)
            {
                bCheck.Enabled = true;
                bCall.Text = "Call";
                bCall.Enabled = false;
            }
            if (players[0].ChipsSet.Amount <= 0)
            {
                bRaise.Enabled = false;
            }
            int parsedValue;

            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (players[0].ChipsSet.Amount <= int.Parse(tbRaise.Text))
                {
                    bRaise.Text = "All in";
                }
                else
                {
                    bRaise.Text = "Raise";
                }
            }
            if (players[0].ChipsSet.Amount < call)
            {
                bRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            this.players[0].StatusLabel.Text = "Fold";
            this.players[0].Turn = false;
            this.players[0].FoldedTurn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                this.players[0].Turn = false;
                this.players[0].StatusLabel.Text = "Check";
            }
            else
            {
                //humanStatusLabel.Text = "All in " + Chips;

                bCheck.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules.Apply(0, 1, players[0], Reserve, Holder, winnersTypes, type);
            if (players[0].ChipsSet.Amount >= call)
            {
                players[0].ChipsSet.Amount -= call;
                Pot.Instance.ChipsSet.Amount += call;

                this.players[0].StatusLabel.Text = "Call " + call;
                players[0].Call = call;
            }
            else if (players[0].ChipsSet.Amount <= call && call > 0)
            {
                Pot.Instance.ChipsSet.Amount += players[0].ChipsSet.Amount;

                this.players[0].StatusLabel.Text = "All in " + players[0].ChipsSet.Amount;
                players[0].ChipsSet.Amount = 0;

                bFold.Enabled = false;
                players[0].Call = players[0].ChipsSet.Amount;
            }

            players[0].ChipsTextBox.Text = players[0].ChipsSet.ToString();
            Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();
            this.players[0].Turn = false;

            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules.Apply(0, 1, players[0], Reserve, Holder, winnersTypes, type);
            int parsedValue;
            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (players[0].ChipsSet.Amount > call)
                {
                    if (Raise * 2 > int.Parse(tbRaise.Text))
                    {
                        tbRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (players[0].ChipsSet.Amount >= int.Parse(tbRaise.Text))
                        {
                            call = int.Parse(tbRaise.Text);
                            Raise = int.Parse(tbRaise.Text);
                            this.players[0].StatusLabel.Text = "Raise " + call.ToString();
                            Pot.Instance.ChipsSet.Amount += call;

                            bCall.Text = "Call";
                            players[0].ChipsSet.Amount -= int.Parse(tbRaise.Text);
                            raising = true;
                            last = 0;
                            this.players[0].Raise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = players[0].ChipsSet.Amount;
                            Raise = players[0].ChipsSet.Amount;
                            Pot.Instance.ChipsSet.Amount += players[0].ChipsSet.Amount;

                            this.players[0].StatusLabel.Text = "Raise " + call.ToString();
                            players[0].ChipsSet.Amount = 0;
                            raising = true;
                            last = 0;
                            this.players[0].Raise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.players[0].Turn = false;
            Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();

            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbAdd.Text == "") { }
            else
            {
                foreach (var player in players)
                {
                    player.ChipsSet.Amount += int.Parse(tbAdd.Text);
                }
            }

            players[0].ChipsTextBox.Text = players[0].ChipsSet.Amount.ToString();
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bb.ToString();
            tbSB.Text = sb.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                bSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                bSB.Visible = false;
            }
        }
        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = sb.ToString();
                return;
            }
            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = sb.ToString();
            }
            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                sb = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bb.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bb.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bb = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion

        private void InitializePlayersComponents()
        {
            this.playersChipsTextBoxs = new List<TextBox>()
            {
                this.humanChipsTextBox,
                this.bot1ChipsTextBox,
                this.bot2ChipsTextBox,
                this.bot3ChipsTextBox,
                this.bot4ChipsTextBox,
                this.bot5ChipsTextBox
            };

            this.playersStatusLabel = new List<Label>()
            {
                this.humanStatusLabel,
                this.bot1StatusLabel,
                this.bot2StatusLabel,
                this.bot3StatusLabel,
                this.bot4StatusLabel,
                this.bot5StatusLabel
            };
        }
    }
}