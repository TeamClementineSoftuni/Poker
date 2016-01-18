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
        private const int PanelWidth = 180;
        private const int PanelHeight = 150;

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
            InitializePanel();
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

        public void InitializePanel()
        {
            this.Panel = new Panel();
            this.Panel.Width = PanelWidth;
            this.Panel.Height = PanelHeight;
            this.Panel.BackColor = Color.DarkBlue;
            this.Panel.Visible = false;
        }
    }
}
