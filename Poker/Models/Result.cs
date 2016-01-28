namespace Poker
{
    using Poker.Interfaces;

    public class Result : IResult
    {
        public Result(double type, double power)
        {
            this.Type = type;
            this.Power = power;
        }

        public double Type { get; set; }

        public double Power { get; set; }
    }
}