namespace Poker.Models
{
    using Poker.Interfaces;
    public class ChipsSet : IChipsSet
    {
        private const int DefaultChipsSetAmount = 0;
        private int amount;

        public ChipsSet(int amount = DefaultChipsSetAmount)
        {
            this.Amount = amount;
        }

        public int Amount
        {
            get { return this.amount; }
            set
            {
                this.amount = value < 0 ? 0 : value;
            }
        }

        public override string ToString()
        {
            return string.Format("Chips : {0}", Amount);
        }
    }
}
