using Poker.Models;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class ArtificialIntelligence
    {
        public static void AI(int c1, int c2, ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent,
            PictureBox[] Holder, ref double rounds,ref int call, ref double Raise, ref bool raising,TextBox tbPot)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    AIHands.HighCard(sChips, ref sTurn, ref sFTurn, sStatus, botPower,ref call,ref Raise,ref raising,tbPot);
                }
                if (botCurrent == 0)
                {
                    AIHands.PairTable(sChips, ref sTurn, ref sFTurn, sStatus, botPower, ref call, ref Raise,ref raising, tbPot);
                }
                if (botCurrent == 1)
                {
                    AIHands.PairHand(sChips, ref sTurn, ref sFTurn, sStatus, botPower,ref rounds ,ref call, ref Raise,ref raising, tbPot);
                }
                if (botCurrent == 2)
                {
                    AIHands.TwoPair(sChips, ref sTurn, ref sFTurn, sStatus, botPower,ref rounds,ref call,ref Raise,ref raising,tbPot);
                }
                if (botCurrent == 3)
                {
                    AIHands.ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, ref rounds, ref call, ref Raise, ref raising, tbPot);
                }
                if (botCurrent == 4)
                {
                    AIHands.Straight(sChips, ref sTurn, ref sFTurn, sStatus, name, botPower,  ref call, ref Raise, ref raising, tbPot);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    AIHands.Flush(sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, ref call, ref Raise, ref raising, tbPot);
                }
                if (botCurrent == 6)
                {
                    AIHands.FullHouse(sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, ref call, ref Raise, ref raising, tbPot);
                }
                if (botCurrent == 7)
                {
                    AIHands.FourOfAKind(sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, ref call, ref Raise, ref raising, tbPot);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    AIHands.StraightFlush(sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, ref call, ref Raise, ref raising, tbPot);
                }
            }
            if (sFTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }
    }
}
