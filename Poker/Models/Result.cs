namespace Poker
{
    using Interfaces;

    public class Result : IResult
    {
        public Result(double type, double power)
        {
            Type = type;
            Power = power;
        }

        public double Type { get; set; }

        public double Power { get; set; }
    }
}
