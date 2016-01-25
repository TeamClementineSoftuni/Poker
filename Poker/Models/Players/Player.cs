using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models.Players
{
    public abstract class Player
    {
        private const int defaultPlayerChipsSetAmount = 10000;
        private const string defaultPlayerName = "Player";
        private const double defaultHandType = -1;

        private bool isFolded;

        private int prevRaise = 0;
        private int allIn = 0;
        
        private PictureBox card1PictureBox = new PictureBox();
        private PictureBox card2PictureBox = new PictureBox();


        protected Player(Point location, string name = defaultPlayerName)
        {
            this.Location = location;
            InitializePanel();
            InitializeCardPictureBox(this.Card1PictureBox, this.Location);
            int a = this.Location.X + this.Card1PictureBox.Width;
            Point point = new Point(this.Location.X + this.Card1PictureBox.Width, this.Location.Y);
            InitializeCardPictureBox(this.Card2PictureBox, new Point(this.Location.X + this.Card1PictureBox.Width, this.Location.Y));  //TODO: refactor 
            this.ChipsTextBox = new TextBox();
            this.ChipsSet = new ChipsSet(defaultPlayerChipsSetAmount);
            this.Hand = new Hand();
            this.Name = name;
            this.FoldedTurn = false;
            this.Turn = false;
            this.Result = new Result();
            this.Result.Type = defaultHandType;
            this.Call = 0;
            this.Raise = 0;

        }

        private void InitializePanel()
        {
            this.Panel = new Panel();
            this.Panel.Location = this.Location;
            this.Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.Panel.Name = "PlayerPanel";
            this.Panel.Size = new System.Drawing.Size(180, 150);
            this.Panel.Visible = false;
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

        public Point Location { get; set; }
        public PictureBox Card1PictureBox
        {
            get { return this.card1PictureBox; }
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
            get { return this.card2PictureBox; }
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

        public Hand Hand { get; set; }

        public Panel Panel { get; set; }

        public TextBox ChipsTextBox { get; set; }

        public ChipsSet ChipsSet { get; set; }

        public Label StatusLabel { get; set; }

        public Result Result { get; set; }

        public bool IsFolded { get; set; }

        public bool Turn { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public int PrevRaise { get; set; }

        public int AllIn { get; set; }

        public bool FoldedTurn { get; set; }
    }
}
