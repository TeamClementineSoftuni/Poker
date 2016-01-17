using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models.Players
{
    public abstract class Player
    {
        private const string defaultPlayerName = "Player";
        private static int instanceCounter = 1;

        private Panel panel;

        private ChipsSet chip;

        private double power;

        private double type = -1;

        private bool turn = false;

        private bool isFolded;

        private int call = 0;

        private int raise = 0;

        private string name;

        protected Player(string name = defaultPlayerName)
        {
            Panel = new Panel();
            ChipsSet = new ChipsSet();
            this.Hand = new Hand();
        }

        public Hand Hand { get; set; }

        public Panel Panel { get; set; }

        public ChipsSet ChipsSet { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Turn { get; set; }

        public bool IsFolded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public string Name { get; set; }
    }
}
