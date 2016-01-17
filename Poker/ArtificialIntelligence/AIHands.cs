using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Models;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    class AIHands
    {
        public static void HighCard(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, ref int call, ref double Raise,ref bool raising,TextBox tbPod)
        {
            HPAndPHAndSmooth.HP(sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25,ref call,ref Raise,ref raising,tbPod);
        }
        public static void PairTable(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPod)
        {
            HPAndPHAndSmooth.HP(sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25, ref call, ref Raise, ref raising, tbPod);
        }
        public static void PairHand(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise,ref rounds,ref call,ref Raise,ref raising,tbPot);
            }
            if (botPower <= 139 && botPower >= 128)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower < 128 && botPower >= 101)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void TwoPair(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower <= 244 && botPower >= 234)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower < 234 && botPower >= 201)
            {
                HPAndPHAndSmooth.PH(sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void ThreeOfAKind(ref ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise,ref call,ref Raise,ref raising,tbPot);
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void Straight(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower < 407 && botPower >= 404)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void Flush(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise, ref call, ref Raise, ref raising, tbPot);
        }
        public static void FullHouse(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (botPower < 620 && botPower >= 602)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void FourOfAKind(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void StraightFlush(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                HPAndPHAndSmooth.Smooth(sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
    }
}
