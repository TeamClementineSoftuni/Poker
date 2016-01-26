namespace Poker.Interfaces
{
    public interface IResult
    {
        /// <summary>
        /// Gets and sets the power of a hand.
        /// </summary>
        double Power { get; set; }

        /// <summary>
        /// Gets and sets the type of a hand.
        /// </summary>
        double Type { get; set; }
    }
}
