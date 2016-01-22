using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Models;
using Poker.Models.Players;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    class AIHands
    {
        public static void HighCard(Player player,  ref int call, ref double Raise,ref bool raising,TextBox tbPod)
        {
            HPAndPHAndSmooth.HP(player, 20, 25,ref call,ref Raise,ref raising,tbPod);
        }
        public static void PairTable(Player player,   ref int call, ref double Raise, ref bool raising, TextBox tbPod)
        {
            HPAndPHAndSmooth.HP(player,   16, 25, ref call, ref Raise, ref raising, tbPod);
        }
        public static void PairHand(Player player,   ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (player.Power <= 199 && player.Power >= 140)
            {
                HPAndPHAndSmooth.PH(player,  rCall, 6, rRaise,ref rounds,ref call,ref Raise,ref raising,tbPot);
            }
            if (player.Power <= 139 && player.Power >= 128)
            {
                HPAndPHAndSmooth.PH(player,   rCall, 7, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power < 128 && player.Power >= 101)
            {
                HPAndPHAndSmooth.PH(player,    rCall, 9, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void TwoPair(Player player,   ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (player.Power <= 290 && player.Power >= 246)
            {
                HPAndPHAndSmooth.PH(player,   rCall, 3, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power <= 244 && player.Power >= 234)
            {
                HPAndPHAndSmooth.PH(player,    rCall, 4, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power < 234 && player.Power >= 201)
            {
                HPAndPHAndSmooth.PH(player,   rCall, 4, rRaise, ref rounds, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void ThreeOfAKind(Player player,  int name, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Power <= 390 && player.Power >= 330)
            {
                HPAndPHAndSmooth.Smooth(player,    name, tCall, tRaise,ref call,ref Raise,ref raising,tbPot);
            }
            if (player.Power <= 327 && player.Power >= 321)//10  8
            {
                HPAndPHAndSmooth.Smooth(player,  name, tCall, tRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power < 321 && player.Power >= 303)//7 2
            {
                HPAndPHAndSmooth.Smooth(player,  name, tCall, tRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void Straight(Player player,  int name, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Power <= 480 && player.Power >= 410)
            {
                HPAndPHAndSmooth.Smooth(player,    name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power <= 409 && player.Power >= 407)//10  8
            {
                HPAndPHAndSmooth.Smooth(player,  name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power < 407 && player.Power >= 404)
            {
                HPAndPHAndSmooth.Smooth(player,    name, sCall, sRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void Flush(Player player,   int name, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            HPAndPHAndSmooth.Smooth(player,  name, fCall, fRaise, ref call, ref Raise, ref raising, tbPot);
        }
        public static void FullHouse(Player player,    int name, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (player.Power <= 626 && player.Power >= 620)
            {
                HPAndPHAndSmooth.Smooth(player,   name, fhCall, fhRaise, ref call, ref Raise, ref raising, tbPot);
            }
            if (player.Power < 620 && player.Power >= 602)
            {
                HPAndPHAndSmooth.Smooth(player,   name, fhCall, fhRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void FourOfAKind(Player player,  int name, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Power <= 752 && player.Power >= 704)
            {
                HPAndPHAndSmooth.Smooth(player, name, fkCall, fkRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
        public static void StraightFlush(Player player,   int name, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Power <= 913 && player.Power >= 804)
            {
                HPAndPHAndSmooth.Smooth(player, name, sfCall, sfRaise, ref call, ref Raise, ref raising, tbPot);
            }
        }
    }
}
