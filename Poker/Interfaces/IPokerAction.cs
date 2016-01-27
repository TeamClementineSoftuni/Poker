namespace Poker.Interfaces
{
    using Models;

    public interface IPokerAction
    {
        Actions ActionType { get; }

        IPlayer Player { get; }

        void ApplyAction();
    }
}
