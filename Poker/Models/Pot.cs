namespace Poker.Models
{
    using System.Windows.Forms;
    using Interfaces;

    public class Pot : IPot
    {
        private static Pot instance;

        private Pot()
        { 
            ChipsSet  = new ChipsSet();
        }

        public static Pot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Pot();
                }
                return instance;
            }
        }

        public IChipsSet ChipsSet { get; set; }

        public int AmountRaisedTo { get; set; }

        public override string ToString()
        {
            string potChipSetToString = this.ChipsSet.Amount.ToString();
            return potChipSetToString;
        }
    }
}
