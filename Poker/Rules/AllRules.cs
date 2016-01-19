namespace Poker.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Models.Players;

    using Type = Poker.Type;

    public class AllRules
    {
        //(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn, Reserve, i, pStatus, Holder, Win, ref sorted, type);
        //string currentText, ref double current, ref double Power -> to Player
        public static void Rules(int c1, int c2, Player player , bool foldedTurn, int[] Reserve, int i, Label pStatus, PictureBox[] Holder,
            List<Type> Win, ref Type sorted, double type)
        {
            if (c1 == 0 && c2 == 1)
            {
            }
            if (!foldedTurn || c1 == 0 && c2 == 1 && pStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[c1];
                Straight[1] = Reserve[c2];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (Reserve[i] == int.Parse(Holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        RulesWinningHands.PairFromHandRules(player, Win , ref sorted,Reserve,i);

                        #region Pair or Two Pair from Table current = 2 || 0
                        RulesWinningHands.PairTwoPairRules(player, Win, ref sorted, Reserve, i);
                        #endregion

                        #region Two Pair current = 2
                        RulesWinningHands.TwoPairRules(player, Win, ref sorted, Reserve, i);
                        #endregion

                        #region Three of a kind current = 3
                        RulesWinningHands.ThreeOfAKindRules(player, Straight,Win ,ref  sorted);
                        #endregion

                        #region Straight current = 4
                        RulesWinningHands.StraightRules(player, Straight, Win,ref sorted);
                        #endregion

                        #region Flush current = 5 || 5.5
                        RulesWinningHands.FlushRules(player, ref vf, Straight1, Win, ref sorted, Reserve, i);
                        #endregion

                        #region Full House current = 6
                        RulesWinningHands.FullHouseRules(player, ref done, Straight, Win, ref sorted, Reserve, type);
                        #endregion

                        #region Four of a Kind current = 7
                        RulesWinningHands.FourOfAKindRules(player, Straight, Win, ref sorted);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        RulesWinningHands.StraightFlushRules(player, st1, st2, st3, st4, Win, ref sorted);
                        #endregion

                        #region High Card current = -1
                        RulesWinningHands.HighCardRules(player, Win, ref sorted, Reserve, i);
                        #endregion
                    }
                }
                
            }
        }
    }
}
