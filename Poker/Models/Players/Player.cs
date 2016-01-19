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
        private const string defaultPlayerName = "Player";

        private double power;

        private double type = -1;

        private bool turn = false;

        private bool isFolded;

        private int call = 0;

        private int raise = 0;

        protected Player(string name = defaultPlayerName)
        {
            this.Panel = new Panel();
            this.ChipsTextBox = new TextBox();
            this.ChipsSet = new ChipsSet();
            this.Hand = new Hand();
        }

        public string Name { get; set; }

        public Hand Hand { get; set; }

        public Panel Panel { get; set; }

        public TextBox ChipsTextBox { get; set; }

        public ChipsSet ChipsSet { get; set; }


        public bool IsFolded { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Turn { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

    }
}
