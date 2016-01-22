using System;
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
    using Poker.Rules;
    using Poker.Win;

    public partial class Form1 : Form
    {
        // parallel branch
        private Card[] board = new Card[5];
        private Deck deck = new Deck("Assets\\Cards\\RenamedCards\\");
        private Player[] players = new Player[6];
        private List<Bot> playersNotFolded = new List<Bot>(); // Bot object should hava indicators for that(by Maria)
        private List<Bot> playersLeftToAct = new List<Bot>();
        private int pot = 0;
        private List<TextBox> playersChipsTextBoxs = new List<TextBox>();
        private List<Player> listOfWinners = new List<Player>();

        private List<Label> playersStatusLabel = new List<Label>();

        //TODO: initialize arrays and lists
        // parallel branch

        #region Variables
        ProgressBar asd = new ProgressBar();
        public int Nm;

        int call = 500, foldedPlayers = 5;

        private double type;
        double rounds = 0, Raise = 0;

        bool B1turn = false, B2turn = false, B3turn = false, B4turn = false, B5turn = false;
        bool B1Fturn = false, B2Fturn = false, B3Fturn = false, B4Fturn = false, B5Fturn = false;

        bool pFolded, b1Folded, b2Folded, b3Folded, b4Folded, b5Folded, intsadded, changed;

        int pCall = 0, b1Call = 0, b2Call = 0, b3Call = 0, b4Call = 0, b5Call = 0, pRaise = 0, b1Raise = 0, b2Raise = 0, b3Raise = 0, b4Raise = 0, b5Raise = 0;

        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;

        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();

        List<int> ints = new List<int>();
        bool PFturn = false, Pturn = true, restart = false, raising = false;
        Poker.Type sorted;
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
            ProcessHand();
            // parallel

            width = this.Width;
            height = this.Height;
            Shuffle();

            tbPot.Enabled = false;

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
        private void ProcessHand()
        {
            int amountRaisedTo = 0;
            int prevAmountraisedTo = 0;

            for (int street = 0; street < 4; street++)
            {
                if (street==0)
                {
                    amountRaisedTo = 500;//or use BB or SB
                }
                if (((street == 1) && amountRaisedTo>500)||street>1)
                {
                    amountRaisedTo -= prevAmountraisedTo;
                }

                for (int player = 1; player < players.Length; player++)
                {
                    //activate next line when we run the game through this...
                   // MessageBox.Show("Bot "+player+"'s Turn");
                    if (!players[player].IsFolded || players[player].Chips > 0)
                    {
                        Actions act = ((Bot)players[player]).Act(street, amountRaisedTo, board);
                        switch (act)
                        {
                            case Actions.Fold:
                                //players[player].StatusLabel.Text = "Fold";
                                break;
                            case Actions.Check:
                                //set card if last player
                                //players[player].StatusLabel.Text = "Check";
                                break;
                            case Actions.Call:
                                //players[player].StatusLabel.Text = "Call";
                                //players[player].ChipsTextBox.Text = ""+players[player].Chips;
                                this.pot += amountRaisedTo - players[player].PrevRaise;
                                break;
                            case Actions.Raise:
                                this.pot += amountRaisedTo - players[player].PrevRaise + players[player].Raise;
                                amountRaisedTo += players[player].Raise;
                                //set bot textfield for raise to players[player].Raise
                                //set bot textfield for action text Raise
                                //players[player].StatusLabel.Text = "Raise "+players[player].Raise;
                                break;
                            case Actions.AllIn:
                                this.pot += players[player].AllIn;
                                //players[player].StatusLabel.Text = "All In";
                                break;
                        }
                    }
                }
                if (street == 3)
                {
                    //show all bots cards which are not Fold
                }
                if (street == 1)
                {
                    board[0].Show();
                    board[1].Show();
                    board[2].Show();
                }
                if ((street == 2) && (prevAmountraisedTo == amountRaisedTo))
                {
                    board[3].Show();
                }
                if ((street == 3) && (prevAmountraisedTo == amountRaisedTo))
                {
                    board[4].Show();
                }
                prevAmountraisedTo = amountRaisedTo;

                //while (playersLeftToAct.Count != 0)
                //{
                //    foreach (var player in playersLeftToAct)
                //    {
                //        GameStatus currentGameStatus = player.Act(street, amountRaisedTo);

                //        if (currentGameStatus.Action == Actions.Fold)
                //        {
                //            this.playersLeftToAct.Remove(player);
                //        }
                //        else if (currentGameStatus.Action == Actions.Raise)
                //        {
                //            amountRaisedTo = currentGameStatus.AmountRaisedTo;
                //        }

                //        this.pot += currentGameStatus.ChipsAddedToPot;
                //    }
                //}

                // TODO: figure out how to show common cards (board) after each street. Maybe solved?
                // after the first street 3 cards are shown (flop)
                //after the last street, no more cards from the board are left to be shown. That's why
                //there are 2 conditions in the loop
                for (int boardCard = 0; boardCard < street + 3 && boardCard < board.Length; boardCard++)
                {
                    //Show() method is not implemented
                    board[boardCard].Show();
                }
            }
        }
        //parallel

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
                if (players[2].ChipsSet.Amount <= 0)
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
                if (players[3].ChipsSet.Amount <= 0)
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
                if (players[4].ChipsSet.Amount <= 0)
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
                if (players[5].ChipsSet.Amount <= 0)
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


        async Task Turns()
        {
            #region Rotating
            if (!PFturn)
            {
                if (Pturn)
                {
                    FixCall(this.players[0], ref pCall, ref pRaise, 1);
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
                    FixCall(this.players[0], ref pCall, ref pRaise, 2);
                }
            }
            if (PFturn || !Pturn)
            {
                await AllIn();
                if (PFturn && !pFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
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
                B1turn = true;
                if (!B1Fturn)
                {
                    if (B1turn)
                    {
                        FixCall(this.players[1], ref b1Call, ref b1Raise, 1);
                        FixCall(this.players[1], ref b1Call, ref b1Raise, 2);
                        AllRules.Rules(2, 3, players[1], B1Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                        MessageBox.Show("Bot 1's Turn");
                        ArtificialIntelligence.ArtificialIntelligence.AI(2, 3, players[1], ref B1turn, ref  B1Fturn, 0, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising, this.tbPot);
                        turnCount++;
                        last = 1;
                        B1turn = false;
                        B2turn = true;
                    }
                }
                if (B1Fturn && !b1Folded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    b1Folded = true;
                }
                if (B1Fturn || !B1turn)
                {
                    await CheckRaise(1, 1);
                    B2turn = true;
                }
                if (!B2Fturn)
                {
                    if (B2turn)
                    {
                        FixCall(this.players[2], ref b2Call, ref b2Raise, 1);
                        FixCall(this.players[2], ref b2Call, ref b2Raise, 2);
                        AllRules.Rules(4, 5, players[2], B2Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                        MessageBox.Show("Bot 2's Turn");
                        ArtificialIntelligence.ArtificialIntelligence.AI(4, 5, players[2], ref B2turn, ref  B2Fturn, 1, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising, this.tbPot);
                        turnCount++;
                        last = 2;
                        B2turn = false;
                        B3turn = true;
                    }
                }
                if (B2Fturn && !b2Folded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    b2Folded = true;
                }
                if (B2Fturn || !B2turn)
                {
                    await CheckRaise(2, 2);
                    B3turn = true;
                }
                if (!B3Fturn)
                {
                    if (B3turn)
                    {
                        FixCall(this.players[3], ref b3Call, ref b3Raise, 1);
                        FixCall(this.players[3], ref b3Call, ref b3Raise, 2);
                        AllRules.Rules(6, 7, players[3], B3Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                        MessageBox.Show("Bot 3's Turn");
                        ArtificialIntelligence.ArtificialIntelligence.AI(6, 7, players[3], ref B3turn, ref  B3Fturn, 2, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising, this.tbPot);
                        turnCount++;
                        last = 3;
                        B3turn = false;
                        B4turn = true;
                    }
                }
                if (B3Fturn && !b3Folded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    b3Folded = true;
                }
                if (B3Fturn || !B3turn)
                {
                    await CheckRaise(3, 3);
                    B4turn = true;
                }
                if (!B4Fturn)
                {
                    if (B4turn)
                    {
                        FixCall(this.players[4], ref b4Call, ref b4Raise, 1);
                        FixCall(this.players[4], ref b4Call, ref b4Raise, 2);
                        AllRules.Rules(8, 9, players[4], B4Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                        MessageBox.Show("Bot 4's Turn");
                        ArtificialIntelligence.ArtificialIntelligence.AI(8, 9, players[4], ref B4turn, ref  B4Fturn, 3, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising, this.tbPot);
                        turnCount++;
                        last = 4;
                        B4turn = false;
                        B5turn = true;
                    }
                }
                if (B4Fturn && !b4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    b4Folded = true;
                }
                if (B4Fturn || !B4turn)
                {
                    await CheckRaise(4, 4);
                    B5turn = true;
                }
                if (!B5Fturn)
                {
                    if (B5turn)
                    {
                        FixCall(this.players[5], ref b5Call, ref b5Raise, 1);
                        FixCall(this.players[5], ref b5Call, ref b5Raise, 2);
                        AllRules.Rules(10, 11, players[5], B5Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                        MessageBox.Show("Bot 5's Turn");
                        ArtificialIntelligence.ArtificialIntelligence.AI(10, 11, players[5], ref B5turn, ref  B5Fturn, 4, this.Holder, ref this.rounds, ref call, ref this.Raise, ref this.raising, this.tbPot);
                        turnCount++;
                        last = 5;
                        B5turn = false;
                    }
                }
                if (B5Fturn && !b5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    b5Folded = true;
                }
                if (B5Fturn || !B5turn)
                {
                    await CheckRaise(5, 5);
                    Pturn = true;
                }
                if (PFturn && !pFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
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
                        if (!PFturn)
                            this.players[0].StatusLabel.Text = "";
                        if (!B1Fturn)
                            this.players[1].StatusLabel.Text = "";
                        if (!B2Fturn)
                            this.players[2].StatusLabel.Text = "";
                        if (!B3Fturn)
                            this.players[3].StatusLabel.Text = "";
                        if (!B4Fturn)
                            this.players[4].StatusLabel.Text = "";
                        if (!B5Fturn)
                            this.players[5].StatusLabel.Text = "";
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.players[0].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    AllRules.Rules(0, 1, players[0], PFturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }
                if (!this.players[1].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    AllRules.Rules(2, 3, players[1], B1Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }
                if (!this.players[2].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    AllRules.Rules(4, 5, players[2], B2Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }
                if (!this.players[3].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    AllRules.Rules(6, 7, players[3], B3Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }
                if (!this.players[4].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    AllRules.Rules(8, 9, players[4], B4Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }
                if (!this.players[5].StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    AllRules.Rules(10, 11, players[5], B5Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
                }

                foreach (var player in players)
                {
                    Poker.Win.Winner.WinnerMessege(player, fixedLast, this.Deck, this.Holder, this.sorted, this.listOfWinners);
                    Poker.Win.Winner.NumbersOfWinners(fixedLast, this.listOfWinners, this.tbPot);
                }

                restart = true;
                Pturn = true;
                PFturn = false;
                B1Fturn = false;
                B2Fturn = false;
                B3Fturn = false;
                B4Fturn = false;
                B5Fturn = false;
                if (players[0].ChipsSet.Amount <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        players[0].ChipsSet.Amount = f2.a;
                        players[1].ChipsSet.Amount += f2.a;
                        players[2].ChipsSet.Amount += f2.a;
                        players[3].ChipsSet.Amount += f2.a;
                        players[4].ChipsSet.Amount += f2.a;
                        players[5].ChipsSet.Amount += f2.a;
                        PFturn = false;
                        Pturn = true;
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
                }

                pCall = 0; pRaise = 0;
                b1Call = 0; b1Raise = 0;
                b2Call = 0; b2Raise = 0;
                b3Call = 0; b3Raise = 0;
                b4Call = 0; b4Raise = 0;
                b5Call = 0; b5Raise = 0;
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
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    Holder[os].Image = null;
                    Holder[os].Invalidate();
                    Holder[os].Visible = false;
                }
                tbPot.Text = "0";
                this.players[0].StatusLabel.Text = "";
                await Shuffle();
                await Turns();
            }
        }


        void FixCall(Player player, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (player.StatusLabel.Text.Contains("Raise"))
                    {
                        var changeRaise = player.StatusLabel.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (player.StatusLabel.Text.Contains("Call"))
                    {
                        var changeCall = player.StatusLabel.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (player.StatusLabel.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != Raise && cRaise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == Raise && Raise > 0)
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
            if (players[1].ChipsSet.Amount <= 0 && !B1Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(players[1].ChipsSet.Amount);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (players[2].ChipsSet.Amount <= 0 && !B2Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(players[2].ChipsSet.Amount);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (players[3].ChipsSet.Amount <= 0 && !B3Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(players[3].ChipsSet.Amount);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (players[4].ChipsSet.Amount <= 0 && !B4Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(players[4].ChipsSet.Amount);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (players[5].ChipsSet.Amount <= 0 && !B5Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(players[5].ChipsSet.Amount);
                    intsadded = true;
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
                if (index == 0)
                {
                    players[0].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[0].ChipsSet.Amount.ToString();
                    this.players[0].Panel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    players[1].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[1].ChipsSet.Amount.ToString();
                    this.players[1].Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    players[2].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[2].ChipsSet.Amount.ToString();
                    this.players[2].Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    players[3].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[3].ChipsSet.Amount.ToString();
                    this.players[3].Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    players[4].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[4].ChipsSet.Amount.ToString();
                    this.players[4].Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    players[5].ChipsSet.Amount += int.Parse(tbPot.Text);
                    players[0].ChipsTextBox.Text = players[5].ChipsSet.Amount.ToString();
                    this.players[5].Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
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

            B1turn = false; B2turn = false; B3turn = false; B4turn = false; B5turn = false;
            B1Fturn = false; B2Fturn = false; B3Fturn = false; B4Fturn = false; B5Fturn = false;
            pFolded = false; b1Folded = false; b2Folded = false; b3Folded = false; b4Folded = false; b5Folded = false;
            PFturn = false; Pturn = true; restart = false; raising = false;
            pCall = 0; b1Call = 0; b2Call = 0; b3Call = 0; b4Call = 0; b5Call = 0; pRaise = 0; b1Raise = 0; b2Raise = 0; b3Raise = 0; b4Raise = 0; b5Raise = 0;
            height = 0; width = 0; winners = 0; Flop = 1; Turn = 2; River = 3; End = 4; maxLeft = 6;
            last = 123; raisedTurn = 1;
            bools.Clear();
            listOfWinners.Clear();
            ints.Clear();
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            tbPot.Text = "0";
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
                    players[0].ChipsSet.Amount = f2.a;
                    players[1].ChipsSet.Amount += f2.a;
                    players[2].ChipsSet.Amount += f2.a;
                    players[3].ChipsSet.Amount += f2.a;
                    players[4].ChipsSet.Amount += f2.a;
                    players[5].ChipsSet.Amount += f2.a;
                    PFturn = false;
                    Pturn = true;
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
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";



            if (!this.players[0].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                AllRules.Rules(0, 1, players[0], PFturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }
            if (!this.players[1].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                AllRules.Rules(2, 3, players[1], B1Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }
            if (!this.players[2].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                AllRules.Rules(4, 5, players[2], B2Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }
            if (!this.players[3].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                AllRules.Rules(6, 7, players[3], B3Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }
            if (!this.players[4].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                AllRules.Rules(8, 9, players[4], B4Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }
            if (!this.players[5].StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                AllRules.Rules(10, 11, players[5], B5Fturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            }

            foreach (var player in players)
            {
                Poker.Win.Winner.WinnerMessege(player, fixedLast, this.Deck, this.Holder, this.sorted, this.listOfWinners);
                Poker.Win.Winner.NumbersOfWinners(fixedLast, this.listOfWinners, this.tbPot);
            }
        }

        // ---> Events in separate folder
        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (pbTimer.Value <= 0)
            {
                PFturn = true;
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
                player.ChipsTextBox.Text = player.ChipsSet.Amount.ToString();
            }

            if (players[0].ChipsSet.Amount <= 0)
            {
                Pturn = false;
                PFturn = true;
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
            Pturn = false;
            PFturn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                Pturn = false;
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
            AllRules.Rules(0, 1, players[0], PFturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
            if (players[0].ChipsSet.Amount >= call)
            {
                players[0].ChipsSet.Amount -= call;
                players[0].ChipsTextBox.Text = players[0].ChipsSet.Amount.ToString();

                if (tbPot.Text != "")
                {
                    tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                }
                else
                {
                    tbPot.Text = call.ToString();
                }

                Pturn = false;
                this.players[0].StatusLabel.Text = "Call " + call;
                pCall = call;
            }
            else if (players[0].ChipsSet.Amount <= call && call > 0)
            {
                tbPot.Text = (int.Parse(tbPot.Text) + players[0].ChipsSet.Amount).ToString();
                this.players[0].StatusLabel.Text = "All in " + players[0].ChipsSet.Amount;
                players[0].ChipsSet.Amount = 0;
                players[0].ChipsTextBox.Text = players[0].ChipsSet.Amount.ToString();
                Pturn = false;
                bFold.Enabled = false;
                pCall = players[0].ChipsSet.Amount;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            AllRules.Rules(0, 1, players[0], PFturn, Reserve, i, this.players[0].StatusLabel, Holder, Win, ref sorted, type);
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
                            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                            bCall.Text = "Call";
                            players[0].ChipsSet.Amount -= int.Parse(tbRaise.Text);
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = players[0].ChipsSet.Amount;
                            Raise = players[0].ChipsSet.Amount;
                            tbPot.Text = (int.Parse(tbPot.Text) + players[0].ChipsSet.Amount).ToString();
                            this.players[0].StatusLabel.Text = "Raise " + call.ToString();
                            players[0].ChipsSet.Amount = 0;
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            Pturn = false;
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