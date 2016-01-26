namespace Poker.Interfaces
{
    public interface IPot
    {
        /// <summary>
        /// Holds the chips set in the pot.
        /// </summary>
        IChipsSet ChipsSet { get; set; }
    }
}
