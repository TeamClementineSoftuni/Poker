namespace Poker.Core.Factories
{
    using System;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Models.Players;

    public class PlayerFactory : IPlayerFactory
    {
        private int index;

        public IPlayer CreatePlayer()
        {
            if (this.index <= Common.NumberOfPlayers)
            {
                if (this.index != 0)
                {
                    var bot = new Bot(Locations.PlayersLocations[this.index]);
                    this.index++;
                    return bot;
                }
                var human = new Human(Locations.PlayersLocations[this.index]);
                this.index++;
                return human;
            }
            throw new ArgumentOutOfRangeException(Messages.ExceptionMaxPlayers);
        }
    }
}