namespace Poker.Models
{
    using Constants;
    using Interfaces;
    public class ChipsSet : IChipsSet
    {
        private int amount;

        public ChipsSet(int amount = Common.DefaultChipsSetAmount)
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
            return $"Chips : {this.Amount}";
        }
    }
}
