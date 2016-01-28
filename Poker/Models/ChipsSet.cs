namespace Poker.Models
{
    using Poker.Constants;
    using Poker.Interfaces;

    public class ChipsSet : IChipsSet
    {
        private int amount;

        public ChipsSet(int amount = Common.DefaultChipsSetAmount)
        {
            this.Amount = amount;
        }

        public int Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value < 0 ? 0 : value;
            }
        }

        public override string ToString()
        {
            return string.Format("Chips : {0}", this.Amount);
        }
    }
}