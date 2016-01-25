using Poker.Models;
using Poker.Models.Players;

namespace Poker.ArtificialIntelligence
{
    using System;
    using System.Windows.Forms;

    public class ArtificialIntelligence
    {
        public static void AI(int c1, int c2, Player player, ActsOnTable onTable, int name,PictureBox[] Holder)
        {
            if (!player.FoldedTurn)
            {
                if (player.Result.Type == -1)
                {
                    //HighCard
                    HPAndPHAndSmooth.HP(player, onTable, 20, 25);
                }
                if (player.Result.Type == 0)
                {
                    //PairTable
                    HPAndPHAndSmooth.HP(player, onTable, 16, 25);
                }
                if (player.Result.Type == 1)
                {
                    PairHand(player, onTable);
                }
                if (player.Result.Type == 2)
                {
                    TwoPair(player,onTable);
                }
                if (player.Result.Type == 3)
                {
                    ThreeOfAKind(player,onTable, name);
                }
                if (player.Result.Type == 4)
                {
                    Straight(player, onTable,name);
                }
                if (player.Result.Type == 5 || player.Result.Type == 5.5)
                {
                    Flush(player,onTable, name);
                }
                if (player.Result.Type == 6)
                {
                    FullHouse(player,onTable, name);
                }
                if (player.Result.Type == 7)
                {
                    FourOfAKind(player,onTable, name);
                }
                if (player.Result.Type == 8 || player.Result.Type == 9)
                {
                    StraightFlush(player,onTable, name);
                }
            }

            if (player.FoldedTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }

      private static void PairHand(Player player, ActsOnTable onTable)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (player.Result.Power <= 199 && player.Result.Power >= 140)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 6, rRaise);
            }
            if (player.Result.Power <= 139 && player.Result.Power >= 128)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 7, rRaise);
            }
            if (player.Result.Power < 128 && player.Result.Power >= 101)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 9, rRaise);
            }
        }

        private static void TwoPair(Player player, ActsOnTable onTable)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (player.Result.Power <= 290 && player.Result.Power >= 246)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 3, rRaise);
            }
            if (player.Result.Power <= 244 && player.Result.Power >= 234)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 4, rRaise);
            }
            if (player.Result.Power < 234 && player.Result.Power >= 201)
            {
                HPAndPHAndSmooth.PH(player, onTable, rCall, 4, rRaise);
            }
        }

        private static void ThreeOfAKind(Player player, ActsOnTable onTable, int name)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Result.Power <= 390 && player.Result.Power >= 330)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, tCall, tRaise);
            }
            if (player.Result.Power <= 327 && player.Result.Power >= 321)//10  8
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, tCall, tRaise);
            }
            if (player.Result.Power < 321 && player.Result.Power >= 303)//7 2
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, tCall, tRaise);
            }
        }

        private static void Straight(Player player, ActsOnTable onTable, int name)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Result.Power <= 480 && player.Result.Power >= 410)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, sCall, sRaise);
            }
            if (player.Result.Power <= 409 && player.Result.Power >= 407)//10  8
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, sCall, sRaise);
            }
            if (player.Result.Power < 407 && player.Result.Power >= 404)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, sCall, sRaise);
            }
        }

        private static void Flush(Player player, ActsOnTable onTable, int name)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            HPAndPHAndSmooth.Smooth(player, onTable, name, fCall, fRaise);
        }

        private static void FullHouse(Player player, ActsOnTable onTable, int name)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (player.Result.Power <= 626 && player.Result.Power >= 620)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, fhCall, fhRaise);
            }
            if (player.Result.Power < 620 && player.Result.Power >= 602)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, fhCall, fhRaise);
            }
        }

        private static void FourOfAKind(Player player, ActsOnTable onTable, int name)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Result.Power <= 752 && player.Result.Power >= 704)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, fkCall, fkRaise);
            }
        }

        private static void StraightFlush(Player player, ActsOnTable onTable, int name)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Result.Power <= 913 && player.Result.Power >= 804)
            {
                HPAndPHAndSmooth.Smooth(player, onTable, name, sfCall, sfRaise);
            }
        }
    }
}
