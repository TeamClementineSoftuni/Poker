using System.Runtime.InteropServices;
using Poker.Models.Players;

namespace Poker.Models
{
    public class Bot : Player
    {
        private static int instanceCounter = 1;
        private const string defaultBotName = "Bot ";

        private string name;

        public Bot()
            : base(defaultBotName + instanceCounter)
        {
            instanceCounter++;
        }

        public GameStatus Act(int street, int raisedToAmount)
        {
            //TODO: implementation
            return new GameStatus(Actions.Fold, 0, 0);
        }
    }
}
