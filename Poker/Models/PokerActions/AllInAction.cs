namespace Poker.Models.PokerActions
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class AllInAction : Action
    {
        public AllInAction(IPlayer player, List<IPlayer> players, int playerIndex)
            : base(player, Actions.AllIn)
        {
            this.Players = players;
            this.PlayerIndex = playerIndex;
        }

        public int PlayerIndex { get; private set; }

        public List<IPlayer> Players { get; private set; }

        public override void ApplyAction()
        {
            this.Player.StatusLabel.Text = Actions.AllIn.ToString();
            this.Player.AllInAmount = this.Player.ChipsSet.Amount;
            this.Player.RaiseAmount = this.Player.PrevRaise + this.Player.AllInAmount;
            this.Player.ChipsSet.Amount -= this.Player.AllInAmount;
            this.Player.ChipsTextBox.Text = this.Player.ChipsSet.ToString();

            Pot.Instance.ChipsSet.Amount += this.Player.AllInAmount;

            if (Pot.Instance.AmountRaisedTo < this.Player.RaiseAmount)
            {
                Pot.Instance.AmountRaisedTo = this.Player.RaiseAmount;
            }

            // TODO:
            // // Human should have button for CAll + If ()
            //this.buttonCall.Text = "Call " + Math.Min(Pot.Instance.AmountRaisedTo - this.Player.RaiseAmount, this.Player.ChipsSet.Amount);
            this.Players.AddRange(Players.GetRange(0, this.PlayerIndex));
            this.Players.RemoveRange(0, this.PlayerIndex);
            this.PlayerIndex = -1;
            this.Players = this.Players.Where(player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
        }
    }
}
