using Poker.Models.Players;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class ArtificialIntelligence
    {
        public static void AI(int c1, int c2, Player player , ref bool sTurn,   int name,
            PictureBox[] Holder, ref double rounds,ref int call, ref double Raise, ref bool raising,TextBox tbPot)
        {
            if (!player.FoldedTurn)
            {
                if (player.Type == -1)
                {
                    AIHands.HighCard(player, ref sTurn,  ref call,ref Raise,ref raising,tbPot);
                }
                if (player.Type == 0)
                {
                    AIHands.PairTable(player, ref sTurn,   ref call, ref Raise,ref raising, tbPot);
                }
                if (player.Type == 1)
                {
                    AIHands.PairHand(player, ref sTurn,  ref rounds ,ref call, ref Raise,ref raising, tbPot);
                }
                if (player.Type == 2)
                {
                    AIHands.TwoPair(player, ref sTurn,   ref rounds,ref call,ref Raise,ref raising,tbPot);
                }
                if (player.Type == 3)
                {
                    AIHands.ThreeOfAKind(player, ref sTurn,   name, ref rounds, ref call, ref Raise, ref raising, tbPot);
                }
                if (player.Type == 4)
                {
                    AIHands.Straight(player, ref sTurn,   name,  ref call, ref Raise, ref raising, tbPot);
                }
                if (player.Type == 5 || player.Type == 5.5)
                {
                    AIHands.Flush(player, ref sTurn,   name, ref call, ref Raise, ref raising, tbPot);
                }
                if (player.Type == 6)
                {
                    AIHands.FullHouse(player, ref sTurn,  name, ref call, ref Raise, ref raising, tbPot);
                }
                if (player.Type == 7)
                {
                    AIHands.FourOfAKind(player, ref sTurn,   name, ref call, ref Raise, ref raising, tbPot);
                }
                if (player.Type == 8 || player.Type == 9)
                {
                    AIHands.StraightFlush(player, ref sTurn,  name, ref call, ref Raise, ref raising, tbPot);
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
