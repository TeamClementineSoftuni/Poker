namespace Poker.Core.Factories
{
    using Constants;
    using Interfaces;
    using Models;
    using Models.Players;
    using System;

    public class PlayerFactory : IPlayerFactory
    {
        private int index = 0;

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
                else
                {
                    var human = new Human(Locations.PlayersLocations[this.index]);
                    this.index++;
                    return human;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(Messages.ExceptionMaxPlayers);
            }
        }
    }
}
