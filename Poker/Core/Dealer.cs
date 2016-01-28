namespace Poker.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Models.Enums;

    public class Dealer
    {
        private IList<IPlayer> winners;

        public void CheckWinners(IList<IPlayer> playersToShowDown, IUserInterface userInterface)
        {
            double bestHand =
                playersToShowDown.OrderByDescending(player => player.Result.Power)
                    .Select(player => player.Result.Power)
                    .FirstOrDefault();
            this.winners = playersToShowDown.Where(player => player.Result.Power == bestHand).ToList();

            foreach (var player in this.winners)
            {
                if (player.Result.Type >= 0 && player.Result.Type <= Common.MaxRankWinningHands)
                {
                    WinningHandsTypes handsType = (WinningHandsTypes)player.Result.Type;

                    userInterface.PrintMessage("{0} has {1}!", player, handsType.ToString());
                }
            }
        }

        public void DistributePot()
        {
            foreach (var winner in this.winners)
            {
                winner.ChipsSet.Amount += Pot.Instance.ChipsSet.Amount / this.winners.Count;
                winner.ChipsTextBox.Text = winner.ChipsSet.Amount.ToString();
            }

            Pot.Instance.ChipsSet.Amount = 0;
            Pot.Instance.PotTextBox.Text = 0.ToString();
        }
    }
}