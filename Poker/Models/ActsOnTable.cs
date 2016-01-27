namespace Poker.Models
{
    using Constants;
    using Interfaces;

    public class ActsOnTable : IActsOnTable
    {
        public ActsOnTable()
        {
            this.RaiseAmount = 0;
            this.RoundsPassed = 0;
            this.CallAmount = Common.InitialCallAmount;
            this.IsRaised = false;
        }

        public double RaiseAmount { get; set; }

        public bool IsRaised { get; set; }

        public int CallAmount { get; set; }

        public double RoundsPassed { get; private set; }
    }
}
