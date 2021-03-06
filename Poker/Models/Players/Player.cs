﻿namespace Poker.Models.Players
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Interfaces;

    public abstract class Player : IPlayer
    {
        private const double defaultHandType = -1;

        private PictureBox card1PictureBox = new PictureBox();

        private PictureBox card2PictureBox = new PictureBox();

        protected Player(Point location, string name = Common.DefaultPlayerName)
        {
            this.Location = location;
            this.InitializePanel();
            this.InitializeCardPictureBox(this.Card1PictureBox, new Point(0, 0));
            this.InitializeCardPictureBox(this.Card2PictureBox, new Point(0 + this.Card1PictureBox.Width, 0));
                //TODO: refactor 
            this.ChipsTextBox = new TextBox();
            this.ChipsSet = new ChipsSet(Common.DefaultPlayerChipsSetAmount);
            this.Hand = new Hand();
            this.Name = name;
            this.FoldedTurn = false;
            this.Turn = false;
            this.CallAmount = 0;
            this.RaiseAmount = 0;
            this.PrevRaise = 0;
        }

        public Point Location { get; set; }

        public PictureBox Card1PictureBox
        {
            get
            {
                return this.card1PictureBox;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("PictureBox cannot be null!");
                }

                this.card1PictureBox = value;
            }
        }

        public PictureBox Card2PictureBox
        {
            get
            {
                return this.card2PictureBox;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("PictureBox cannot be null!");
                }

                this.card2PictureBox = value;
            }
        }

        public string Name { get; set; }

        public IHand Hand { get; set; }

        public Panel Panel { get; set; }

        public TextBox ChipsTextBox { get; set; }

        public ChipsSet ChipsSet { get; set; }

        public Label StatusLabel { get; set; }

        public IResult Result { get; set; }

        public bool IsFolded { get; set; }

        public bool Turn { get; set; }

        public int CallAmount { get; set; }

        public int RaiseAmount { get; set; }

        public int PrevRaise { get; set; }

        public int AllInAmount { get; set; }

        public bool FoldedTurn { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        private void InitializePanel()
        {
            this.Panel = new Panel();
            this.Panel.Location = this.Location;
            this.Panel.BackColor = Color.Transparent;
            this.Panel.Name = "PlayerPanel";
            this.Panel.Size = new Size(180, 150);
            this.Panel.Visible = true;
        }

        private void InitializeCardPictureBox(PictureBox pictureBox, Point location)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Width = 80;
            pictureBox.Height = 130;
            pictureBox.Visible = true;
            this.Panel.Controls.Add(pictureBox);
            // pictureBox.Anchor = (AnchorStyles.Bottom);
            pictureBox.Location = location;
        }
    }
}