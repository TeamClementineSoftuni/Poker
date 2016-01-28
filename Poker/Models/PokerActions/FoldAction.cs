namespace Poker.Models.PokerActions
{
    using Poker.Interfaces;

    public class FoldAction : Action
    {
        public FoldAction(IPlayer player)
            : base(player, Actions.Fold)
        {
        }

        public override void ApplyAction()
        {
            this.Player.StatusLabel.Text = Actions.Fold.ToString();
            this.Player.IsFolded = true;
            this.Player.Card1PictureBox.Visible = false;
            this.Player.Card2PictureBox.Visible = false;
        }
    }
}