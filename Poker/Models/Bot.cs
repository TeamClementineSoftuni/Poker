using System.Runtime.InteropServices;

namespace Poker.Models
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Bot
    {
        private static int instanceCounter = 1;

        private Panel panel;

        private Chip chip;

        private double power;

        private double type = -1;

        private bool turn = false;

        private bool isFolded;

        private int call = 0;

        private int raise = 0;

        private string name;

        public Bot()
        {
            this.name = "Bot " + instanceCounter;
            Panel = new Panel();
            Chip = new Chip();
            instanceCounter++;
            this.Hand = new Hand();
        }

        public Hand Hand { get; set; }

        public Panel Panel { get; set; }

        public Chip Chip { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Turn { get; set; }

        public bool IsFolded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public GameStatus Act(int street, int raisedToAmount)
        {
            //TODO: implementation
            return new GameStatus(Actions.Fold, 0, 0);
        }
    }
}
