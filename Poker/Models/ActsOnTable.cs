namespace Poker.Models
{
    using System.Windows.Forms;

    public class ActsOnTable
    {
        public ActsOnTable()
        {
            this.Raise = 0;
            this.Rounds = 0;
            this.Call = 500;
            this.Raising = false;
        }
        public double Raise { get; set; }

        public bool Raising { get; set; }

        public int Call { get; set; }

        public double Rounds { get; set; }
    }
}
