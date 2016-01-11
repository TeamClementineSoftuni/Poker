namespace Poker.Models
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Bot
    {
        private Panel botPanel = new Panel();

        private int botChips = 10000;

        private double botPower;

        private double botType = -1;

        private bool botTurn = false;

        private bool botFolded;

        private int botCall = 0;

        private int botRaise = 0;

        public Panel BotPanel
        {
            get { return this.BotPanel; }
            set { this.botPanel = value; }
        }

        public int BotChips
        {
            get { return this.botChips; }
            set { this.botChips += value; }
        }

        public double BotPower
        {
            get { return this.botPower; }
            set { this.botPower = value; }
        }

        public double BotType
        {
            get { return this.botType; }
            set { this.botType = value; }
        }

        public bool BotTurn
        {
            get { return this.botTurn; }
            set { this.botTurn = value; }
        }

        public bool BotFolded
        {
            get { return this.botFolded; }
            set { this.botFolded = value; }
        }

        public int BotCall
        {
            get { return this.botCall; }
            set { this.botCall = value; }
        }

        public int BotRaise
        {
            get { return this.botRaise; }
            set { this.botRaise = value; }
        }

        public bool CheckChips()
        {
            if (this.botChips < 0)
            {
                return false;
            }

            return true;
        }
    }
}
