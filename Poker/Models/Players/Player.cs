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
        private const int defaultStartingChips = 10000;
        private const string defaultPlayerName = "Player";

        private double type = -1;

        private bool turn = false;

        private bool isFolded;

        private int chips;
        private int call = 0;
        private int raise = 0;
        private int prevRaise = 0;
        private int allIn = 0;


        protected Player(string name = defaultPlayerName, int chips = defaultStartingChips)
        {
            this.Panel = new Panel();
            this.ChipsTextBox = new TextBox();
            this.ChipsSet = new ChipsSet();
            this.Hand = new Hand();
            this.Name = name;
            this.Chips = chips;
        }

        public string Name { get; set; }
        public int Chips
        {
            get { return this.chips; }
            set
            {
                if (value < 1)
                {
                    this.chips = 0;
                }
                else
                {
                    this.chips = value;
                }
            }
        }

        public Hand Hand { get; set; }

        public Panel Panel { get; set; }

        public TextBox ChipsTextBox { get; set; }

        public ChipsSet ChipsSet { get; set; }

        public Label StatusLabel { get; set; }

        public bool IsFolded { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Turn { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }
        public int PrevRaise { get; set; }
        public int AllIn { get; set; }

    }
}
