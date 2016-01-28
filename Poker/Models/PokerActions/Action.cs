namespace Poker.Models.PokerActions
{
    using Interfaces;

    public abstract class Action : IPokerAction
    {
        protected Action(IPlayer player, Actions action)
        {
            this.ActionType = action;
            this.Player = player;
        }  

        public Actions ActionType { get; set; }

        public IPlayer Player { get; protected set; }

        public abstract void ApplyAction();
    }
}
