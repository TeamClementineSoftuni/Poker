using System.Runtime.InteropServices;
using Poker.Models.Players;

namespace Poker.Models
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Bot : Player
    {
        private static int instanceCounter = 1;

        private string name;

        public Bot()
            : base("Bot " + instanceCounter)
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
