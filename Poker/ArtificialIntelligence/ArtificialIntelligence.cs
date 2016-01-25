using Poker.Models;
using Poker.Models.Players;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class ArtificialIntelligence
    {
        public static void AI(int c1, int c2, Player player, int name,
            PictureBox[] Holder, ref double rounds,ref int call, ref double Raise, ref bool raising)
        {
            if (!player.FoldedTurn)
            {
                if (player.Result.Type == -1)
                {
                    AIHands.HighCard(player, ref call,ref Raise,ref raising);
                }
                if (player.Result.Type == 0)
                {
                    AIHands.PairTable(player,  ref call, ref Raise,ref raising);
                }
                if (player.Result.Type == 1)
                {
                    AIHands.PairHand(player,  ref rounds ,ref call, ref Raise,ref raising);
                }
                if (player.Result.Type == 2)
                {
                    AIHands.TwoPair(player, ref rounds,ref call,ref Raise,ref raising);
                }
                if (player.Result.Type == 3)
                {
                    AIHands.ThreeOfAKind(player, name, ref rounds, ref call, ref Raise, ref raising);
                }
                if (player.Result.Type == 4)
                {
                    AIHands.Straight(player, name,  ref call, ref Raise, ref raising);
                }
                if (player.Result.Type == 5 || player.Result.Type == 5.5)
                {
                    AIHands.Flush(player, name, ref call, ref Raise, ref raising);
                }
                if (player.Result.Type == 6)
                {
                    AIHands.FullHouse(player, name, ref call, ref Raise, ref raising);
                }
                if (player.Result.Type == 7)
                {
                    AIHands.FourOfAKind(player, name, ref call, ref Raise, ref raising);
                }
                if (player.Result.Type == 8 || player.Result.Type == 9)
                {
                    AIHands.StraightFlush(player, name, ref call, ref Raise, ref raising);
                }
            }

            if (player.FoldedTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }
    }
}
