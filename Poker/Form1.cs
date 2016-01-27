using System.Threading;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Constants;
    using Models;
    using Models.Players;
    using Interfaces;

    using Poker.Core.Database;
    using Poker.Core.Factories;
    using Poker.UI;

    public partial class Form1 : Form
    {
        // parallel branch
        private IPlayerFactory playerFactory = new PlayerFactory();
        private IPokerDatabase pokerDatabase = new PokerDatabase();
        private ICard[] board = new Card[Common.NumberOfBoardCards];
        private IDeck deck = new Deck("Assets\\Cards\\RenamedCards\\");
        private List<IPlayer> playersLeftToAct = new List<IPlayer>();
        private List<TextBox> playersChipsTextBoxs = new List<TextBox>();
        private PictureBox[] boardPictureBoxes = new PictureBox[Common.NumberOfBoardCards];
        private int amountRaisedTo = 0;
        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        
        private List<IPlayer> listOfWinners = new List<IPlayer>();
        private List<Label> playersStatusLabel = new List<Label>();
        private List<IResult> winnersTypes = new List<IResult>();
        private IActsOnTable actsOnTable = new ActsOnTable();
        private IUserInterface userInterface = new WindowsFormUserInterface();

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
            for (int index = 0; index < Common.NumberOfPlayers; index++)
            {
                var player = this.playerFactory.CreatePlayer();
                this.pokerDatabase.AddPlayer(player);
            }

            // Set the panel,ChipsTextBox and StatusLabel for every player
            for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
            {
                this.Controls.Add(this.pokerDatabase.Players[index].Panel);

                this.pokerDatabase.Players[index].ChipsTextBox = playersChipsTextBoxs[index];
                this.pokerDatabase.Players[index].ChipsTextBox.Enabled = false;
                this.pokerDatabase.Players[index].ChipsTextBox.Text = this.pokerDatabase.Players[index].ChipsSet.ToString();

                this.pokerDatabase.Players[index].StatusLabel = this.playersStatusLabel[index];
            }
        }

        // parallel
        async Task DealCards()
        {
            deck.Shuffle();
            // give 2 cards to every player  --> the cards are taken from the deck;
            for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
            {
                if (this.pokerDatabase.Players[index].ChipsSet.Amount > 0)
                {
                    this.pokerDatabase.Players[index].Hand.Card1 = deck.Cards[2 * index];
                    this.pokerDatabase.Players[index].Hand.Card2 = deck.Cards[2 * index + 1];
                    this.pokerDatabase.Players[index].IsFolded = false;
                    this.pokerDatabase.Players[index].AllInAmount = 0;
                    this.pokerDatabase.Players[index].RaiseAmount = 0;
                    this.pokerDatabase.Players[index].PrevRaise = 0;
                    this.pokerDatabase.Players[index].Card1PictureBox.Visible = true;
                    this.pokerDatabase.Players[index].Card2PictureBox.Visible = true;
                }
                else
                {
                    this.pokerDatabase.Players[index].IsFolded = true;
                }
            }

            // TODO: refactor
            this.pokerDatabase.Players[0].Card1PictureBox.Image = this.pokerDatabase.Players[0].Hand.Card1.CardImage;
            await Task.Delay(150);
            this.pokerDatabase.Players[0].Card2PictureBox.Image = this.pokerDatabase.Players[0].Hand.Card2.CardImage;
            await Task.Delay(150);

            for (int botIndex = 1; botIndex < this.pokerDatabase.Players.Length; botIndex++)
            {
                this.pokerDatabase.Players[botIndex].Card1PictureBox.Image = Image.FromFile("Assets\\Cards\\RenamedCards\\back.png");
                await Task.Delay(150);
                // TODO: currently it gives a player 2 cards and then moves to next player. If you want to make
                // it like in real game, it should first give every player 1 card, and only when all players are dealt 1 card, deal them another one.
                this.pokerDatabase.Players[botIndex].Card2PictureBox.Image = Image.FromFile("Assets\\Cards\\RenamedCards\\back.png");
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
            this.numericUpDown1.Maximum = this.pokerDatabase.Players[0].ChipsSet.Amount;

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

            for (int street = 0; street < 4; street++)
            {
                DisableButtons(this.buttonFold, this.buttonCheck, this.buttonCall, this.buttonRaise);
                amountRaisedTo = 0;
                playersLeftToAct = new List<IPlayer>();
                playersLeftToAct = this.pokerDatabase.Players.Where(player => player.ChipsSet.Amount > 0 && player.IsFolded == false).ToList();
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

                this.buttonCall.Text = "Call " + amountRaisedTo;

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
                                    Pot.Instance.ChipsSet.Amount += amountRaisedTo - playersLeftToAct[playerIndex].PrevRaise;
                                    break;
                                case Actions.Raise:
                                    Pot.Instance.ChipsSet.Amount += playersLeftToAct[playerIndex].RaiseAmount - playersLeftToAct[playerIndex].PrevRaise;
                                    amountRaisedTo = playersLeftToAct[playerIndex].RaiseAmount;
                                    this.buttonCall.Text = "Call " + Math.Min(amountRaisedTo - this.pokerDatabase.Players[0].RaiseAmount, this.pokerDatabase.Players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    playerIndex = 0;

                                    MessageBox.Show(playersLeftToAct.Count().ToString());
                                    break;
                                case Actions.AllIn:
                                    Pot.Instance.ChipsSet.Amount += playersLeftToAct[playerIndex].AllInAmount;

                                    if (amountRaisedTo < playersLeftToAct[playerIndex].RaiseAmount)
                                    {
                                        amountRaisedTo = playersLeftToAct[playerIndex].RaiseAmount;
                                    }

                                    this.buttonCall.Text = "Call " + Math.Min(amountRaisedTo - this.pokerDatabase.Players[0].RaiseAmount, this.pokerDatabase.Players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    playerIndex = -1;
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    break;
                            }
                        }
                        else if (!this.pokerDatabase.Players[0].IsFolded && this.pokerDatabase.Players[0].ChipsSet.Amount > 0)
                        {
                            EnableButtons(this.buttonFold);

                            if (this.amountRaisedTo > this.pokerDatabase.Players[0].RaiseAmount)
                            {
                                EnableButtons(this.buttonCall);
                            }
                            else
                            {
                                EnableButtons(this.buttonCheck);
                            }

                            if (this.amountRaisedTo - this.pokerDatabase.Players[0].RaiseAmount < this.pokerDatabase.Players[0].ChipsSet.Amount)
                            {
                                EnableButtons(this.buttonRaise);
                            }

                            MessageBox.Show(playersLeftToAct.Count().ToString());

                            await signal.WaitAsync();
                            DisableButtons(this.buttonFold, this.buttonCheck, this.buttonCall, this.buttonRaise);
                        }

                        this.potTextBox.Text = Pot.Instance.ToString();
                    }

                    playersLeftToAct.Clear();
                }


                await Task.Delay(1000);

                if (this.pokerDatabase.Players.Where(p => p.IsFolded == false).Count() <= 1)
                {
                    await Task.Delay(1000);
                    for (int boardCardIndex = 0; boardCardIndex < boardPictureBoxes.Length; boardCardIndex++)
                    {
                        boardPictureBoxes[boardCardIndex].Visible = false;
                    }

                    for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
                    {
                        this.pokerDatabase.Players[index].StatusLabel.Text = "";
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

                    for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
                    {
                        if (!this.pokerDatabase.Players[index].IsFolded)
                        {
                            this.pokerDatabase.Players[index].Card1PictureBox.Image = this.pokerDatabase.Players[index].Hand.Card1.CardImage;
                            this.pokerDatabase.Players[index].Card2PictureBox.Image = this.pokerDatabase.Players[index].Hand.Card2.CardImage;
                        }

                        this.pokerDatabase.Players[index].StatusLabel.Text = "";
                    }

                    await Task.Delay(1000);

                    // TODO: Implement winning hand algo
                    this.pokerDatabase.Players[0].ChipsSet.Amount += Pot.Instance.ChipsSet.Amount;
                    this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.Amount.ToString();

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

        private void ButtonFold_Click(object sender, EventArgs e)
        {
            this.pokerDatabase.Players[0].StatusLabel.Text = "Fold";
            this.pokerDatabase.Players[0].IsFolded = true;

            signal.Release();
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            this.pokerDatabase.Players[0].StatusLabel.Text = "Check";
            signal.Release();
        }
        private void ButtonCall_Click(object sender, EventArgs e)
        {
            string amountToCallAsString = this.buttonCall.Text.Remove(0, 5);
            int amountToCall = int.Parse(amountToCallAsString);
            Pot.Instance.ChipsSet.Amount += amountToCall;
            this.pokerDatabase.Players[0].ChipsSet.Amount = this.pokerDatabase.Players[0].ChipsSet.Amount - amountToCall;
            this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.Amount.ToString();
            this.pokerDatabase.Players[0].PrevRaise = this.pokerDatabase.Players[0].RaiseAmount;
            this.pokerDatabase.Players[0].RaiseAmount = amountToCall;
            this.buttonCall.Enabled = false;
            this.pokerDatabase.Players[0].StatusLabel.Text = "Call";

            signal.Release();
        }

        private void ButtonRaise_Click(object sender, EventArgs e)
        {
            this.pokerDatabase.Players[0].PrevRaise = this.pokerDatabase.Players[0].RaiseAmount;
            this.pokerDatabase.Players[0].RaiseAmount = this.pokerDatabase.Players[0].RaiseAmount + (int)this.numericUpDown1.Value;
            this.pokerDatabase.Players[0].ChipsSet.Amount = this.pokerDatabase.Players[0].ChipsSet.Amount - (int)this.numericUpDown1.Value;
            this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.Amount.ToString();
            this.pokerDatabase.Players[0].StatusLabel.Text = "Raised to " + this.pokerDatabase.Players[0].RaiseAmount;

            Pot.Instance.ChipsSet.Amount += (int)this.numericUpDown1.Value;
            this.amountRaisedTo = this.pokerDatabase.Players[0].RaiseAmount;

            signal.Release();
        }

        private void ButtonAddChips_Click(object sender, EventArgs e)
        {
            if (addChipsTextBox.Text == "") { }
            else
            {
                foreach (var player in this.pokerDatabase.Players)
                {
                    // TODO: unhandled exception when put string instead integer
                    player.ChipsSet.Amount += int.Parse(addChipsTextBox.Text);
                }
            }

            this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.Amount.ToString();
        }
        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            bigBlindTextBox.Text = bigBlindAmount.ToString();
            smallBlindTextBox.Text = sb.ToString();
            if (bigBlindTextBox.Visible == false)
            {
                bigBlindTextBox.Visible = true;
                smallBlindTextBox.Visible = true;
                buttonBigBlind.Visible = true;
                buttonSmallBlind.Visible = true;
            }
            else
            {
                bigBlindTextBox.Visible = false;
                smallBlindTextBox.Visible = false;
                buttonBigBlind.Visible = false;
                buttonSmallBlind.Visible = false;
            }
        }
        private void ButtonSmallBlind_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (smallBlindTextBox.Text.Contains(",") || smallBlindTextBox.Text.Contains("."))
            {
                this.userInterface.PrintMessage(Messages.SmallBlindRoundNumber);
                smallBlindTextBox.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(smallBlindTextBox.Text, out parsedValue))
            {
                this.userInterface.PrintMessage(Messages.OnlyNumbers);
                smallBlindTextBox.Text = sb.ToString();
                return;
            }
            if (int.Parse(smallBlindTextBox.Text) > 100000)
            {
                this.userInterface.PrintMessage(Messages.MaxSmallBlind);
                smallBlindTextBox.Text = sb.ToString();
            }
            if (int.Parse(smallBlindTextBox.Text) < 250)
            {
                this.userInterface.PrintMessage(Messages.MinSmallBlind);
            }
            if (int.Parse(smallBlindTextBox.Text) >= 250 && int.Parse(smallBlindTextBox.Text) <= 100000)
            {
                sb = int.Parse(smallBlindTextBox.Text);
                this.userInterface.PrintMessage(Messages.ChangesSave);
            }
        }
        private void ButtonBigBlind_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (bigBlindTextBox.Text.Contains(",") || bigBlindTextBox.Text.Contains("."))
            {
                this.userInterface.PrintMessage(Messages.BigBlindRoundNumber);
                bigBlindTextBox.Text = bigBlindAmount.ToString();
                return;
            }
            if (!int.TryParse(smallBlindTextBox.Text, out parsedValue))
            {
                this.userInterface.PrintMessage(Messages.OnlyNumbers);
                smallBlindTextBox.Text = bigBlindAmount.ToString();
                return;
            }
            if (int.Parse(bigBlindTextBox.Text) > 200000)
            {
                this.userInterface.PrintMessage(Messages.MaxBigBlind);
                bigBlindTextBox.Text = bigBlindAmount.ToString();
            }
            if (int.Parse(bigBlindTextBox.Text) < 500)
            {
                this.userInterface.PrintMessage(Messages.MinBigBlind);
            }
            if (int.Parse(bigBlindTextBox.Text) >= 500 && int.Parse(bigBlindTextBox.Text) <= 200000)
            {
                bigBlindAmount = int.Parse(bigBlindTextBox.Text);
                this.userInterface.PrintMessage(Messages.ChangesSave);
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