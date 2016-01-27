using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models.PokerActions
{
    using Interfaces;

    public class CallAction : Action
    {
        public CallAction(IPlayer player)
                : base(player, Actions.Call)
            {
        }

        public override void ApplyAction()
        {
            this.Player.StatusLabel.Text = Actions.Call.ToString();
            this.Player.ChipsSet.Amount -= Pot.Instance.AmountRaisedTo - this.Player.RaiseAmount;
            this.Player.PrevRaise = this.Player.RaiseAmount;
            this.Player.RaiseAmount = Pot.Instance.AmountRaisedTo;
            this.Player.ChipsTextBox.Text = this.Player.ChipsSet.ToString();

            Pot.Instance.ChipsSet.Amount += Pot.Instance.AmountRaisedTo - this.Player.PrevRaise;
        }
    }
}

