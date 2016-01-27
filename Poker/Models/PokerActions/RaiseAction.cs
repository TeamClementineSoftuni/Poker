namespace Poker.Models.PokerActions
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public class RaiseAction : Action
    {
        public RaiseAction(IPlayer player, int raiseAmount, List<IPlayer> players, int playerIndex)
            : base(player, Actions.Raise)
        {
            this.RaiseAmount = raiseAmount;
            this.Players = players;
            this.PlayerIndex = playerIndex;
        }

        public List<IPlayer> Players { get; set; }

        public int RaiseAmount { get; set; }

        public int PlayerIndex { get; set; }

        public override void ApplyAction()
        {
            this.Player.ChipsSet.Amount -= (Pot.Instance.AmountRaisedTo + this.RaiseAmount) - this.Player.RaiseAmount;
            this.Player.PrevRaise = this.Player.RaiseAmount;
            this.Player.RaiseAmount = Pot.Instance.AmountRaisedTo + this.RaiseAmount;
            this.Player.ChipsTextBox.Text = this.Player.ChipsSet.ToString();
            this.Player.StatusLabel.Text = Actions.Raise.ToString() + this.Player.RaiseAmount;

            Pot.Instance.ChipsSet.Amount += this.Player.RaiseAmount - this.Player.PrevRaise;
            Pot.Instance.AmountRaisedTo = this.Player.RaiseAmount;

            // TODO:
            // Human should have button + if ()
            //this.buttonCall.Text = "Call " + Math.Min(Pot.Instance.AmountRaisedTo - this.Player.RaiseAmount, this.Player.ChipsSet.Amount);
            this.Players.AddRange(Players.GetRange(0, this.PlayerIndex));
            this.Players.RemoveRange(0, this.PlayerIndex);
            this.Players = this.Players.Where(player => (player.IsFolded == false) && player.ChipsSet.Amount > 0).ToList();
            this.PlayerIndex = 0;
        }
    }
}


