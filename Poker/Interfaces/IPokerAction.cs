namespace Poker.Interfaces
{
    using Poker.Models;

    public interface IPokerAction
    {
        Actions ActionType { get; }

        IPlayer Player { get; }

        void ApplyAction();
    }
}