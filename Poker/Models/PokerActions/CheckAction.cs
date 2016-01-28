namespace Poker.Models.PokerActions
{
    using Poker.Interfaces;

    public class CheckAction : Action
    {
        public CheckAction(IPlayer player)
            : base(player, Actions.Check)
        {
        }

        public override void ApplyAction()
        {
            this.Player.StatusLabel.Text = Actions.Check.ToString();
        }
    }
}