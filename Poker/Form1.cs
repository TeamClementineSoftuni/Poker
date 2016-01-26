using System.Threading;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Constants;
    using Models;
    using Models.Players;
    using Core;

    using Poker.Interfaces;

    public partial class Form1 : Form
    {
        // parallel branch
        private ICard[] board = new Card[5];
        private IDeck deck = new Deck("Assets\\Cards\\RenamedCards\\");
        private IPlayer[] players = new Player[6];
        private List<IPlayer> playersNotFolded = new List<IPlayer>(); // Bot object should hava indicators for that(by Maria)
        private List<IPlayer> playersLeftToAct = new List<IPlayer>();
        private List<TextBox> playersChipsTextBoxs = new List<TextBox>();
        private PictureBox[] boardPictureBoxes = new PictureBox[5];
        private int pot = 0;
        private int amountRaisedTo = 0;
        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        private List<IPlayer> listOfWinners = new List<IPlayer>();
        private List<Label> playersStatusLabel = new List<Label>();
        private List<IResult> winnersTypes = new List<IResult>();
        private IActsOnTable actsOnTable = new ActsOnTable();
        
        private double bigBlindAmount = Common.InitialCallAmount;
        
        //TODO: initialize arrays and lists
        // parallel branch

        int height, width;

        private int sb = 250;
        private int bb = 500;

        public Form1()
        {
           
            InitializeComponent();
            //parallel branch
            InitializePlayersComponents();
            InitializePlayers();
            DealCards();
            // parallel
        }

        private void InitializePlayers()
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
        }

        // parallel
        async Task DealCards()
        {
            deck.Shuffle();
            // give 2 cards to every player  --> the cards are taken from the deck;
            for (int index = 0; index < players.Length; index++)
            {
                if (players[index].ChipsSet.Amount > 0)
                {
                    players[index].Hand.Card1 = deck.Cards[2 * index];
                    players[index].Hand.Card2 = deck.Cards[2 * index + 1];
                    this.players[index].IsFolded = false;
                    this.players[index].AllInAmount = 0;
                    this.players[index].RaiseAmount = 0;
                    this.players[index].PrevRaise = 0;
                    this.players[index].Card1PictureBox.Visible = true;
                    this.players[index].Card2PictureBox.Visible = true;
                }
                else
                {
                    this.players[index].IsFolded = true;
                }
            }

            // TODO: refactor
            players[0].Card1PictureBox.Image = players[0].Hand.Card1.CardImage;
            await Task.Delay(150);
            players[0].Card2PictureBox.Image = players[0].Hand.Card2.CardImage;
            await Task.Delay(150);

            for (int botIndex = 1; botIndex < players.Length; botIndex++)
            {
                players[botIndex].Card1PictureBox.Image = Image.FromFile("Assets\\Cards\\RenamedCards\\back.png");
                await Task.Delay(150);
                // TODO: currently it gives a player 2 cards and then moves to next player. If you want to make
                // it like in real game, it should first give every player 1 card, and only when all players are dealt 1 card, deal them another one.
                players[botIndex].Card2PictureBox.Image = Image.FromFile("Assets\\Cards\\RenamedCards\\back.png");
                await Task.Delay(150);
            }
            // we have given every player 2 cards ( 6 * 2 = 12), so the first 12 cards from the deck are already reserved. 
            // Now we reserve 5 more cards for the board (the five cards visible by every player). 
            for (int index = 12; index < 12 + 5; index++)
            {
                board[index - 12] = deck.Cards[index];
                PictureBox boardCardPictureBox = new PictureBox();
                boardCardPictureBox.Location = Locations.BoardCardsLocations(index - 12);
                boardCardPictureBox.Width = 80;
                boardCardPictureBox.Height = 130;
                boardCardPictureBox.Image = board[index - 12].CardImage;
                boardCardPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                boardCardPictureBox.Visible = false;

                boardPictureBoxes[index - 12] = boardCardPictureBox;
                this.Controls.Add(boardPictureBoxes[index - 12]);

            }

            while (true)
            {
                await ProcessHand();
                await DealCards();
            }
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            this.numericUpDown1.Maximum = this.players[0].ChipsSet.Amount;

            if (this.numericUpDown1.Value > this.numericUpDown1.Maximum)
            {
                this.numericUpDown1.Value = this.numericUpDown1.Maximum;
            }
        }

        // The whole method has to be refactured, eventually getting rid of loops by extacting methods 
        // and somehow getting rid of if-s with polymorphism i guess?
        async Task ProcessHand()
        {
            this.amountRaisedTo = 0;
            this.pot = 0;

            for (int street = 0; street < 4; street++)
            {
                DisableButtons(this.bFold, this.bCheck, this.bCall, this.bRaise);
                amountRaisedTo = 0;
                playersLeftToAct = new List<IPlayer>();
                playersLeftToAct = players.Where(player => player.ChipsSet.Amount > 0 && player.IsFolded == false).ToList();
                bool moreThanOnePlayerLeftInTheHand = this.playersLeftToAct.Count > 1;

                for (int player = 0; player < playersLeftToAct.Count; player++)
                {
                    this.playersLeftToAct[player].RaiseAmount = 0;
                    this.playersLeftToAct[player].PrevRaise = 0;
                    this.playersLeftToAct[player].StatusLabel.Text = "";
                }

                if (street == 0)
                {
                    amountRaisedTo = 500;//or use BB or SB
                }

                this.bCall.Text = "Call " + amountRaisedTo;

                while (playersLeftToAct.Count != 0 && moreThanOnePlayerLeftInTheHand)
                {

                    for (int playerIndex = 0; playerIndex < playersLeftToAct.Count; playerIndex++)
                    {
                        if (playersLeftToAct[playerIndex] is Bot)
                        {
                            Actions act = ((Bot)playersLeftToAct[playerIndex]).Act(street, amountRaisedTo, board);
                            playersLeftToAct[playerIndex].Panel.BackColor = Color.Aqua;
                            await Task.Delay(500);
                            playersLeftToAct[playerIndex].Panel.BackColor = Color.Transparent;
                            await Task.Delay(500);

                            switch (act)
                            {
                                case Actions.Fold:
                                    break;
                                case Actions.Check:
                                    playersLeftToAct[playerIndex].StatusLabel.Text = "Check";
                                    break;
                                case Actions.Call:
                                    this.pot += amountRaisedTo - playersLeftToAct[playerIndex].PrevRaise;
                                    break;
                                case Actions.Raise:
                                    this.pot += playersLeftToAct[playerIndex].RaiseAmount - playersLeftToAct[playerIndex].PrevRaise;
                                    amountRaisedTo = playersLeftToAct[playerIndex].RaiseAmount;
                                    this.bCall.Text = "Call " + Math.Min(amountRaisedTo - players[0].RaiseAmount, this.players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    playerIndex = 0;

                                    MessageBox.Show(playersLeftToAct.Count().ToString());
                                    break;
                                case Actions.AllIn:
                                    this.pot += playersLeftToAct[playerIndex].AllInAmount;

                                    if (amountRaisedTo < playersLeftToAct[playerIndex].RaiseAmount)
                                    {
                                        amountRaisedTo = playersLeftToAct[playerIndex].RaiseAmount;
                                    }

                                    this.bCall.Text = "Call " + Math.Min(amountRaisedTo - players[0].RaiseAmount, this.players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    playerIndex = -1;
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    break;
                            }
                        }
                        else if (!players[0].IsFolded && players[0].ChipsSet.Amount > 0)
                        {
                            EnableButtons(this.bFold);

                            if (this.amountRaisedTo > this.players[0].RaiseAmount)
                            {
                                EnableButtons(this.bCall);
                            }
                            else
                            {
                                EnableButtons(this.bCheck);
                            }

                            if (this.amountRaisedTo - this.players[0].RaiseAmount < this.players[0].ChipsSet.Amount)
                            {
                                EnableButtons(this.bRaise);
                            }

                            MessageBox.Show(playersLeftToAct.Count().ToString());

                            await signal.WaitAsync();
                            DisableButtons(this.bFold, this.bCheck, this.bCall, this.bRaise);
                        }

                        this.potTextBox.Text = this.pot.ToString();
                    }

                    playersLeftToAct.Clear();
                }


                await Task.Delay(1000);

                if (players.Where(p => p.IsFolded == false).Count() <= 1)
                {
                    await Task.Delay(1000);
                    for (int boardCardIndex = 0; boardCardIndex < boardPictureBoxes.Length; boardCardIndex++)
                    {
                        boardPictureBoxes[boardCardIndex].Visible = false;
                    }

                    for (int player = 0; player < players.Length; player++)
                    {
                        this.players[player].StatusLabel.Text = "";
                    }

                    this.playersLeftToAct.Clear();

                }

                if (street == 0)
                {
                    boardPictureBoxes[0].Visible = true;
                    boardPictureBoxes[1].Visible = true;
                    boardPictureBoxes[2].Visible = true;
                }

                if (street == 1)
                {
                    boardPictureBoxes[3].Visible = true;
                }

                if (street == 2)
                {
                    boardPictureBoxes[4].Visible = true;
                }

                // TODO: refactor
                if (street == 3)
                {
                    await Task.Delay(1000);

                    for (int player = 0; player < players.Length; player++)
                    {
                        if (!players[player].IsFolded)
                        {
                            players[player].Card1PictureBox.Image = players[player].Hand.Card1.CardImage;
                            players[player].Card2PictureBox.Image = players[player].Hand.Card2.CardImage;
                        }

                        this.players[player].StatusLabel.Text = "";
                    }

                    await Task.Delay(1000);

                    // TODO: Implement winning hand algo
                    this.players[0].ChipsSet.Amount += this.pot;
                    this.players[0].ChipsTextBox.Text = this.players[0].ChipsSet.Amount.ToString();

                    await Task.Delay(1000);

                    for (int boardCardIndex = 0; boardCardIndex < boardPictureBoxes.Length; boardCardIndex++)
                    {
                        boardPictureBoxes[boardCardIndex].Visible = false;
                    }
                    //show all bots cards which are not Fold
                }

                // TODO: figure out how to show common cards (board) after each street. Maybe solved?
                // after the first street 3 cards are shown (flop)
                //after the last street, no more cards from the board are left to be shown. That's why
                //there are 2 conditions in the loop
                //for (int boardCard = 0; boardCard < street + 3 && boardCard < board.Length; boardCard++)
                //{
                //    //Show() method is not implemented
                //    board[boardCard].Show();
                //}
            }
        }

        private void DisableButtons(params Button[] buttons)
        {
            for (int buttonIndex = 0; buttonIndex < buttons.Length; buttonIndex++)
            {
                buttons[buttonIndex].Enabled = false;
            }
        }

        private void EnableButtons(params Button[] buttons)
        {
            for (int buttonIndex = 0; buttonIndex < buttons.Length; buttonIndex++)
            {
                buttons[buttonIndex].Enabled = true;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            this.players[0].StatusLabel.Text = "Fold";
            this.players[0].IsFolded = true;

            signal.Release();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            this.players[0].StatusLabel.Text = "Check";
            signal.Release();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            string amountToCallAsString = this.bCall.Text.Remove(0, 5);
            int amountToCall = int.Parse(amountToCallAsString);
            this.pot += amountToCall;
            this.players[0].ChipsSet.Amount = this.players[0].ChipsSet.Amount - amountToCall;
            this.players[0].ChipsTextBox.Text = this.players[0].ChipsSet.Amount.ToString();
            this.players[0].PrevRaise = this.players[0].RaiseAmount;
            this.players[0].RaiseAmount = amountToCall;
            this.bCall.Enabled = false;
            this.players[0].StatusLabel.Text = "Call";

            signal.Release();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            this.players[0].PrevRaise = this.players[0].RaiseAmount;
            this.players[0].RaiseAmount = this.players[0].RaiseAmount + (int)this.numericUpDown1.Value;
            this.players[0].ChipsSet.Amount = this.players[0].ChipsSet.Amount - (int)this.numericUpDown1.Value;
            this.players[0].ChipsTextBox.Text = this.players[0].ChipsSet.Amount.ToString();
            this.players[0].StatusLabel.Text = "Raised to " + this.players[0].RaiseAmount;

            this.pot += (int)this.numericUpDown1.Value;
            this.amountRaisedTo = this.players[0].RaiseAmount;

            signal.Release();
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
            tbBB.Text = bigBlindAmount.ToString();
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
                tbBB.Text = bigBlindAmount.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bigBlindAmount.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bigBlindAmount.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bigBlindAmount = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        //#endregion

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