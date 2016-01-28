namespace Poker.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Models.Enums;

    /// <summary>
    /// Defines the winners and prints a message
    /// </summary>
    public class Dealer
    {
        private IList<IPlayer> winners;

        /// <summary>
        /// Check the winners from list of players
        /// </summary>
        /// <param name="playersToShowDown">List of players to check for winner</param>
        /// <param name="userInterface">Get the UserInterface where to print the message</param>
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

                    userInterface.PrintMessage("{0} wins with {1}!", player, handsType.ToString());
                }
            }
        }

        /// <summary>
        /// Handles the pot amount to the winners and set to zero the pot
        /// </summary>
        public void DistributePot()
        {
            foreach (var winner in this.winners)
            {
                winner.ChipsSet.Amount += Pot.Instance.ChipsSet.Amount / this.winners.Count;
                winner.ChipsTextBox.Text = winner.ChipsSet.Amount.ToString();
                Pot.Instance.ChipsSet.Amount -= winner.ChipsSet.Amount;
                Pot.Instance.PotTextBox.Text = Pot.Instance.ToString();
            }
        }
    }
}