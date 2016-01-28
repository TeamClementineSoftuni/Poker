namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Core;
    using Poker.Core.Database;
    using Poker.Core.Factories;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Models.Players;
    using Poker.UI;

    /// <summary>
    /// Initialize PokerGame and players components and performs the logic of the game
    /// </summary>
    public partial class PokerGameEngine : Form
    {
        private int amountRaisedTo;

        private int bigBlind = Common.BigBlindAmount;

        private int smallBlind = Common.SmallBlindAmount;

        private readonly double bigBlindAmount = Common.InitialCallAmount;

        private readonly ICard[] board = new Card[Common.NumberOfBoardCards];

        private readonly PictureBox[] boardPictureBoxes = new PictureBox[Common.NumberOfBoardCards];

        private readonly Dealer dealer = new Dealer();
       
        private readonly HandEvaluator handEvaluator = new HandEvaluator();

        private readonly IPokerDatabase pokerDatabase = new PokerDatabase();

        private readonly IPlayerFactory playerFactory = new PlayerFactory();

        private readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        private readonly IUserInterface userInterface = new WindowsFormUserInterface();

        private List<TextBox> playersChipsTextBoxs = new List<TextBox>();

        private List<IPlayer> playersLeftToAct = new List<IPlayer>();

        private List<Label> playersStatusLabel = new List<Label>();

        private int height;

        private int width;


        public PokerGameEngine()
        {
            this.InitializeComponent();
            this.InitializePlayersComponents();
            this.InitializePlayers();
            this.DealCards();
        }

        private void InitializePlayers()
        {
            for (int index = 0; index < Common.NumberOfPlayers; index++)
            {
                var player = this.playerFactory.CreatePlayer();
                this.pokerDatabase.AddPlayer(player);
            }

            for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
            {
                this.Controls.Add(this.pokerDatabase.Players[index].Panel);

                this.pokerDatabase.Players[index].ChipsTextBox = this.playersChipsTextBoxs[index];
                this.pokerDatabase.Players[index].ChipsTextBox.Enabled = false;
                this.pokerDatabase.Players[index].ChipsTextBox.Text =
                    this.pokerDatabase.Players[index].ChipsSet.ToString();

                this.pokerDatabase.Players[index].StatusLabel = this.playersStatusLabel[index];
            }

            Pot.Instance.PotTextBox = potTextBox;
            ((Human)this.pokerDatabase.Players[0]).CallButton = this.buttonCall;
        }

        private async Task DealCards()
        {
            Deck.Instance.Shuffle();
            // give 2 cards to every player  --> the cards are taken from the deck;
            for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
            {
                if (this.pokerDatabase.Players[index].ChipsSet.Amount > 0)
                {
                    this.pokerDatabase.Players[index].Hand.Card1 = Deck.Instance.Cards[2 * index];
                    this.pokerDatabase.Players[index].Hand.Card2 = Deck.Instance.Cards[2 * index + 1];
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
                    this.pokerDatabase.Players[index].Card1PictureBox.Visible = false;
                    this.pokerDatabase.Players[index].Card2PictureBox.Visible = false;
                }
            }

            // TODO: refactor
            this.pokerDatabase.Players[0].Card1PictureBox.Image = this.pokerDatabase.Players[0].Hand.Card1.CardImage;
            await Task.Delay(150);
            this.pokerDatabase.Players[0].Card2PictureBox.Image = this.pokerDatabase.Players[0].Hand.Card2.CardImage;
            await Task.Delay(150);

            for (int botIndex = 1; botIndex < this.pokerDatabase.Players.Length; botIndex++)
            {
                this.pokerDatabase.Players[botIndex].Card1PictureBox.Image =
                    Image.FromFile(Common.ImagesBackDefaultPath);
                await Task.Delay(150);
                // TODO: currently it gives a player 2 cards and then moves to next player. If you want to make
                // it like in real game, it should first give every player 1 card, and only when all players are dealt 1 card, deal them another one.
                this.pokerDatabase.Players[botIndex].Card2PictureBox.Image =
                    Image.FromFile(Common.ImagesBackDefaultPath);
                await Task.Delay(150);
            }
            // we have given every player 2 cards ( 6 * 2 = 12), so the first 12 cards from the deck are already reserved. 
            // Now we reserve 5 more cards for the board (the five cards visible by every player). 
            for (int index = 12; index < 12 + 5; index++)
            {
                this.board[index - 12] = Deck.Instance.Cards[index];
                PictureBox boardCardPictureBox = new PictureBox();
                boardCardPictureBox.Location = Locations.BoardCardsLocations(index - 12);
                boardCardPictureBox.Width = 80;
                boardCardPictureBox.Height = 130;
                boardCardPictureBox.Image = this.board[index - 12].CardImage;
                boardCardPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                boardCardPictureBox.Visible = false;

                this.boardPictureBoxes[index - 12] = boardCardPictureBox;
                this.Controls.Add(this.boardPictureBoxes[index - 12]);
            }

            while (true)
            {
                this.pokerDatabase.Players[4].ChipsSet.Amount -= this.smallBlind;
                this.pokerDatabase.Players[4].ChipsTextBox.Text =
                    this.pokerDatabase.Players[4].ChipsSet.ToString();
                this.pokerDatabase.Players[5].ChipsSet.Amount -= this.bigBlind;
                this.pokerDatabase.Players[5].ChipsTextBox.Text =
                    this.pokerDatabase.Players[5].ChipsSet.ToString();
                await this.ProcessHand();
                await this.DealCards();
            }
        }

        // The whole method has to be refactured, eventually getting rid of loops by extacting methods 
        // and somehow getting rid of if-s with polymorphism i guess?
        private async Task ProcessHand()
        {
            this.amountRaisedTo = 0;

            for (int street = 0; street < 4; street++)
            {
                this.DisableButtons(this.buttonFold, this.buttonCheck, this.buttonCall, this.buttonRaise);
                this.amountRaisedTo = 0;
                this.playersLeftToAct = new List<IPlayer>();
                this.playersLeftToAct =
                    this.pokerDatabase.Players.Where(player => player.ChipsSet.Amount > 0 && player.IsFolded == false)
                        .ToList();
                bool moreThanOnePlayerLeftInTheHand = this.playersLeftToAct.Count > 1;

                for (int player = 0; player < this.playersLeftToAct.Count; player++)
                {
                    this.playersLeftToAct[player].RaiseAmount = 0;
                    this.playersLeftToAct[player].PrevRaise = 0;
                    this.playersLeftToAct[player].StatusLabel.Text = "";
                }

                if (street == 0)
                {
                    this.amountRaisedTo = Common.InitialCallAmount; //or use BB or SB
                }

                this.buttonCall.Text = string.Format("{0} {1}", Actions.Call, this.amountRaisedTo);

                while (this.playersLeftToAct.Count != 0 && moreThanOnePlayerLeftInTheHand)
                {
                    for (int playerIndex = 0; playerIndex < this.playersLeftToAct.Count; playerIndex++)
                    {
                        if (this.playersLeftToAct[playerIndex] is Bot)
                        {
                            Actions act = ((Bot)this.playersLeftToAct[playerIndex]).Act(
                                street,
                                this.amountRaisedTo,
                                this.board);
                            this.playersLeftToAct[playerIndex].Panel.BackColor = Color.Aqua;
                            await Task.Delay(500);
                            this.playersLeftToAct[playerIndex].Panel.BackColor = Color.Transparent;
                            await Task.Delay(500);

                            switch (act)
                            {
                                case Actions.Fold:
                                    break;
                                case Actions.Check:
                                    this.playersLeftToAct[playerIndex].StatusLabel.Text = Actions.Check.ToString();
                                    break;
                                case Actions.Call:
                                    Pot.Instance.ChipsSet.Amount += this.amountRaisedTo
                                                                    - this.playersLeftToAct[playerIndex].PrevRaise;
                                    break;
                                case Actions.Raise:
                                    Pot.Instance.ChipsSet.Amount += this.playersLeftToAct[playerIndex].RaiseAmount
                                                                    - this.playersLeftToAct[playerIndex].PrevRaise;
                                    this.amountRaisedTo = this.playersLeftToAct[playerIndex].RaiseAmount;
                                    this.buttonCall.Text = "Call "
                                                           + Math.Min(
                                                               this.amountRaisedTo
                                                               - this.pokerDatabase.Players[0].RaiseAmount,
                                                               this.pokerDatabase.Players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(this.playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    playerIndex = 0;

                                    break;
                                case Actions.AllIn:
                                    Pot.Instance.ChipsSet.Amount += this.playersLeftToAct[playerIndex].AllInAmount;

                                    if (this.amountRaisedTo < this.playersLeftToAct[playerIndex].RaiseAmount)
                                    {
                                        this.amountRaisedTo = this.playersLeftToAct[playerIndex].RaiseAmount;
                                    }

                                    this.buttonCall.Text = "Call "
                                                           + Math.Min(
                                                               this.amountRaisedTo
                                                               - this.pokerDatabase.Players[0].RaiseAmount,
                                                               this.pokerDatabase.Players[0].ChipsSet.Amount);
                                    this.playersLeftToAct.AddRange(this.playersLeftToAct.GetRange(0, playerIndex));
                                    this.playersLeftToAct.RemoveRange(0, playerIndex);
                                    playerIndex = -1;
                                    this.playersLeftToAct =
                                        this.playersLeftToAct.Where(
                                            player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
                                    break;
                            }
                        }
                        else if (!this.pokerDatabase.Players[0].IsFolded
                                 && this.pokerDatabase.Players[0].ChipsSet.Amount > 0)
                        {
                            this.EnableButtons(this.buttonFold);

                            if (this.amountRaisedTo > this.pokerDatabase.Players[0].RaiseAmount)
                            {
                                this.EnableButtons(this.buttonCall);
                            }
                            else
                            {
                                this.EnableButtons(this.buttonCheck);
                            }

                            if (this.amountRaisedTo - this.pokerDatabase.Players[0].RaiseAmount
                                < this.pokerDatabase.Players[0].ChipsSet.Amount)
                            {
                                this.EnableButtons(this.buttonRaise);
                            }

                            await this.signal.WaitAsync();
                            this.DisableButtons(
                                this.buttonFold,
                                this.buttonCheck,
                                this.buttonCall,
                                this.buttonRaise);
                        }

                        Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();
                    }

                    this.playersLeftToAct.Clear();
                }

                await Task.Delay(1000);

                if (this.pokerDatabase.Players.Where(p => p.IsFolded == false).Count() <= 1)
                {
                    await Task.Delay(1000);
                    for (int boardCardIndex = 0; boardCardIndex < this.boardPictureBoxes.Length; boardCardIndex++)
                    {
                        this.boardPictureBoxes[boardCardIndex].Visible = false;
                    }

                    for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
                    {
                        this.pokerDatabase.Players[index].StatusLabel.Text = "";
                    }

                    this.playersLeftToAct.Clear();
                }

                if (street == 0)
                {
                    this.boardPictureBoxes[0].Visible = true;
                    this.boardPictureBoxes[1].Visible = true;
                    this.boardPictureBoxes[2].Visible = true;
                }

                if (street == 1)
                {
                    this.boardPictureBoxes[3].Visible = true;
                }

                if (street == 2)
                {
                    this.boardPictureBoxes[4].Visible = true;
                }

                // TODO: refactor
                if (street == 3)
                {
                    await Task.Delay(1000);

                    for (int index = 0; index < this.pokerDatabase.Players.Length; index++)
                    {
                        if (!this.pokerDatabase.Players[index].IsFolded)
                        {
                            this.pokerDatabase.Players[index].Card1PictureBox.Image =
                                this.pokerDatabase.Players[index].Hand.Card1.CardImage;
                            this.pokerDatabase.Players[index].Card2PictureBox.Image =
                                this.pokerDatabase.Players[index].Hand.Card2.CardImage;
                        }

                        this.pokerDatabase.Players[index].StatusLabel.Text = "";
                    }

                    await Task.Delay(1000);

                    // TODO: Implement winning hand algo
                    //this.pokerDatabase.Players[0].ChipsSet.Amount += Pot.Instance.ChipsSet.Amount;
                    //this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.Amount.ToString();

                    IList<IPlayer> playersToShowDown =
                        this.pokerDatabase.Players.Where(player => player.IsFolded == false).ToList();

                    foreach (var player in playersToShowDown)
                    {
                        player.Result = this.handEvaluator.Evaluate(player.Hand, this.board);
                    }

                    this.dealer.CheckWinners(playersToShowDown, this.userInterface);
                    this.dealer.DistributePot();

                    await Task.Delay(1000);

                    for (int boardCardIndex = 0; boardCardIndex < this.boardPictureBoxes.Length; boardCardIndex++)
                    {
                        this.boardPictureBoxes[boardCardIndex].Visible = false;
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

        private void RaiseNumericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            this.raiseNumericUpDown.Maximum = this.pokerDatabase.Players[0].ChipsSet.Amount;

            if (this.raiseNumericUpDown.Value > this.raiseNumericUpDown.Maximum)
            {
                this.raiseNumericUpDown.Value = this.raiseNumericUpDown.Maximum;
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
            this.pokerDatabase.Players[0].StatusLabel.Text = Actions.Fold.ToString();
            this.pokerDatabase.Players[0].IsFolded = true;

            this.signal.Release();
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            this.pokerDatabase.Players[0].StatusLabel.Text = Actions.Check.ToString();
            this.signal.Release();
        }

        private void ButtonCall_Click(object sender, EventArgs e)
        {
            string amountToCallAsString = this.buttonCall.Text.Remove(0, 5);
            int amountToCall = int.Parse(amountToCallAsString);
            Pot.Instance.ChipsSet.Amount += amountToCall;
            this.pokerDatabase.Players[0].ChipsSet.Amount = this.pokerDatabase.Players[0].ChipsSet.Amount - amountToCall;
            this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.ToString();
            this.pokerDatabase.Players[0].PrevRaise = this.pokerDatabase.Players[0].RaiseAmount;
            this.pokerDatabase.Players[0].RaiseAmount = amountToCall;
            this.buttonCall.Enabled = false;
            this.pokerDatabase.Players[0].StatusLabel.Text = Actions.Call.ToString();

            this.signal.Release();
        }

        private void ButtonRaise_Click(object sender, EventArgs e)
        {
            this.pokerDatabase.Players[0].PrevRaise = this.pokerDatabase.Players[0].RaiseAmount;
            this.pokerDatabase.Players[0].RaiseAmount = this.pokerDatabase.Players[0].RaiseAmount
                                                        + (int)this.raiseNumericUpDown.Value;
            this.pokerDatabase.Players[0].ChipsSet.Amount = this.pokerDatabase.Players[0].ChipsSet.Amount
                                                            - (int)this.raiseNumericUpDown.Value;
            this.pokerDatabase.Players[0].ChipsTextBox.Text = this.pokerDatabase.Players[0].ChipsSet.ToString();
            this.pokerDatabase.Players[0].StatusLabel.Text = string.Format("{0} to {1}", Actions.Raise, this.pokerDatabase.Players[0].RaiseAmount);

            Pot.Instance.ChipsSet.Amount += (int)this.raiseNumericUpDown.Value;
            this.amountRaisedTo = this.pokerDatabase.Players[0].RaiseAmount;

            this.signal.Release();
        }

        private void ButtonAddChips_Click(object sender, EventArgs e)
        {
            foreach (var player in this.pokerDatabase.Players)
            {
                player.ChipsSet.Amount += (int)this.addChipsNumericUpDown.Value;
                player.ChipsTextBox.Text = player.ChipsSet.ToString();
            }
        }

        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            this.bigBlindNumericUpDown.Text = this.bigBlindAmount.ToString();
            if (this.bigBlindNumericUpDown.Visible == false)
            {
                this.bigBlindNumericUpDown.Visible = true;
                this.smallBlindNumericUpDown.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttonSmallBlind.Visible = true;
            }
            else
            {
                this.bigBlindNumericUpDown.Visible = false;
                this.smallBlindNumericUpDown.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttonSmallBlind.Visible = false;
            }
        }

        private void ButtonSmallBlind_Click(object sender, EventArgs e)
        {
            this.smallBlind = (int)this.smallBlindNumericUpDown.Value;
            this.userInterface.PrintMessage(Messages.ChangesSave);
        }

        private void ButtonBigBlind_Click(object sender, EventArgs e)
        {
            this.bigBlind = (int)this.bigBlindNumericUpDown.Value;
            this.userInterface.PrintMessage(Messages.ChangesSave);
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

        private void InitializePlayersComponents()
        {
            this.playersChipsTextBoxs = new List<TextBox>
                                            {
                                                this.humanChipsTextBox,
                                                this.bot1ChipsTextBox,
                                                this.bot2ChipsTextBox,
                                                this.bot3ChipsTextBox,
                                                this.bot4ChipsTextBox,
                                                this.bot5ChipsTextBox
                                            };

            this.playersStatusLabel = new List<Label>
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