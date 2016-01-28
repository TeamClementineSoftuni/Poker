namespace Poker.Core.Database
{
    using System;

    using Poker.Constants;
    using Poker.Interfaces;

    public class PokerDatabase : IPokerDatabase
    {
        private readonly IPlayer[] players;

        private int index;

        public PokerDatabase()
        {
            this.players = new IPlayer[Common.NumberOfPlayers];
        }

        public IPlayer[] Players
        {
            get
            {
                return this.players;
            }
        }

        public void AddPlayer(IPlayer player)
        {
            if (this.index <= Common.NumberOfPlayers)
            {
                this.players[this.index] = player;
                this.index++;
            }
            else
            {
                throw new ArgumentOutOfRangeException(Messages.ExceptionMaxPlayers);
            }
        }
    }
}