namespace Poker.Models
{
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class Pot : IPot
    {
        private static Pot instance;

        private Pot()
        {
            this.ChipsSet = new ChipsSet();
            this.PotTextBox = new TextBox();
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

        public TextBox PotTextBox { get; set; }

        public IChipsSet ChipsSet { get; set; }

        public int AmountRaisedTo { get; set; }

        public override string ToString()
        {
            string potChipSetToString = this.ChipsSet.Amount.ToString();
            return potChipSetToString;
        }
    }
}