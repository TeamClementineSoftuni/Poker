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
        public static void ApplyRules(int card1, int card2, Player player, int[] Reserve, PictureBox[] Holder,
            List<Type> Win, double type)
        {
            if (!player.FoldedTurn)
            {
                #region Variables

                bool done = false;
                bool vf = false;

                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[card1];
                Straight[1] = Reserve[card2];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];

                var st1 = Straight.Where(o => o % 4 == 0).Select(o => o / 4).Distinct().ToArray();
                var st2 = Straight.Where(o => o % 4 == 1).Select(o => o / 4).Distinct().ToArray();
                var st3 = Straight.Where(o => o % 4 == 2).Select(o => o / 4).Distinct().ToArray();
                var st4 = Straight.Where(o => o % 4 == 3).Select(o => o / 4).Distinct().ToArray();        

                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                #endregion
                for (int i = 0; i < 16; i++)
                {
                    if (Reserve[i] == int.Parse(Holder[card1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[card2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        CheckPairFromHand(player, Win, Reserve,i);

                        #region Pair or Two Pair from Table current = 2 || 0
                        CheckPairTwoPair(player, Win, Reserve, i);
                        #endregion

                        #region Two Pair current = 2
                        CheckTwoPair(player, Win, Reserve, i);
                        #endregion

                        #region Three of a kind current = 3
                        CheckThreeOfAKind(player, Straight, Win);
                        #endregion

                        #region Straight current = 4
                        CheckStraight(player, Straight, Win);
                        #endregion

                        #region Flush current = 5 || 5.5
                        CheckFlush(player, ref vf, Straight1, Win, Reserve, i);
                        #endregion

                        #region Full House current = 6
                        CheckFullHouse(player, ref done, Straight, Win, type);
                        #endregion

                        #region Four of a Kind current = 7
                        CheckFourOfAKind(player, Straight, Win);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        CheckStraightFlush(player, st1, st2, st3, st4, Win);
                        #endregion

                        #region High Card current = -1
                        CheckHighCard(player, Win, Reserve, i);
                        #endregion

                       
                    }
                }
            }
        }

        private static void CheckStraightFlush(Player player, int[] st1, int[] st2, int[] st3, int[] st4, List<Type> Win)
        {
            if (player.Type >= -1)
            {
                ProcessHandsArrays(player, st1, Win);
                ProcessHandsArrays(player, st2, Win);
                ProcessHandsArrays(player, st3, Win);
                ProcessHandsArrays(player, st4, Win); 
            }
        }

        private static void ProcessHandsArrays(Player player, int[] handsArray, List<Type> Win)
        {
            if (handsArray.Length >= 5)
            {
                if (handsArray[0] + 4 == handsArray[4])
                {
                    player.Type = 8;
                    player.Power = handsArray.Max()/4 + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 8});
                }

                if (handsArray[0] == 0 && handsArray[1] == 9 && handsArray[2] == 10 && handsArray[3] == 11 && handsArray[0] + 12 == handsArray[4])
                {
                    player.Type = 9;
                    player.Power = handsArray.Max()/4 + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 9});
                }
            }
        }

        private static void CheckFourOfAKind(Player player, int[] Straight, List<Type> Win)
        {
            if (player.Type >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        player.Type = 7;
                        player.Power = (Straight[j] / 4) * 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 7 });
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        player.Type = 7;
                        player.Power = 13 * 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 7 });
                    }
                }
            }
        }

        private static void CheckFullHouse(Player player, ref bool done, int[] Straight, List<Type> Win, double type)
        {
            if (player.Type >= -1)
            {
                type = player.Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type = 6;
                                player.Power = 13 * 2 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 6 });
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                player.Type = 6;
                                player.Power = fh.Max() / 4 * 2 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 6 });
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                player.Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (player.Type != 6)
                {
                    player.Power = type;
                }
            }
        }

        private static void CheckFlush(Player player, ref bool vf, int[] Straight1, List<Type> Win, int[] Reserve, int i)
        {
            if (player.Type >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();

                ProcessBoardHandArray(player, ref vf, Win, Reserve, i, f1);
                ProcessBoardHandArray(player, ref vf, Win, Reserve, i, f2);
                ProcessBoardHandArray(player, ref vf, Win, Reserve, i, f3);
                ProcessBoardHandArray(player, ref vf, Win, Reserve, i, f4);
            }
        }

        private static void ProcessBoardHandArray(Player player, ref bool vf, List<Type> Win, int[] Reserve, int index,
            int[] handsArray)
        {
            if (handsArray.Length == 3 || handsArray.Length == 4)
            {
                if (Reserve[index] %4 == Reserve[index + 1]%4 && Reserve[index] %4 == handsArray[0]%4)
                {
                    if (Reserve[index] /4 > handsArray.Max()/4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[index] + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                    if (Reserve[index + 1]/4 > handsArray.Max()/4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[index + 1] + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                    else if (Reserve[index] /4 < handsArray.Max()/4 && Reserve[index + 1]/4 < handsArray.Max()/4)
                    {
                        player.Type = 5;
                        player.Power = handsArray.Max() + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                }
            }

            if (handsArray.Length == 4) //different cards in hand
            {
                if (Reserve[index] %4 != Reserve[index + 1]%4 && Reserve[index] %4 == handsArray[0]%4)
                {
                    if (Reserve[index] /4 > handsArray.Max()/4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[index] + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                    else
                    {
                        player.Type = 5;
                        player.Power = handsArray.Max() + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                }
                if (Reserve[index + 1]%4 != Reserve[index] %4 && Reserve[index + 1]%4 == handsArray[0]%4)
                {
                    if (Reserve[index + 1]/4 > handsArray.Max()/4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[index + 1] + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                    else
                    {
                        player.Type = 5;
                        player.Power = handsArray.Max() + player.Type*100;
                        Win.Add(new Type() {Power = player.Power, Current = 5});
                        vf = true;
                    }
                }
            }

            if (handsArray.Length == 5)
            {
                if (Reserve[index] %4 == handsArray[0]%4 && Reserve[index] /4 > handsArray.Min()/4)
                {
                    player.Type = 5;
                    player.Power = Reserve[index] + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 5});
                    vf = true;
                }
                if (Reserve[index + 1]%4 == handsArray[0]%4 && Reserve[index + 1]/4 > handsArray.Min()/4)
                {
                    player.Type = 5;
                    player.Power = Reserve[index + 1] + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 5});
                    vf = true;
                }
                else if (Reserve[index] /4 < handsArray.Min()/4 && Reserve[index + 1]/4 < handsArray.Min())
                {
                    player.Type = 5;
                    player.Power = handsArray.Max() + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 5});
                    vf = true;
                }
            }

            if (handsArray.Length > 0 && vf)
            {
                if (Reserve[index] /4 == 0 && Reserve[index] %4 == handsArray[0]%4)
                {
                    player.Type = 5.5;
                    player.Power = 13 + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 5.5});
                }
                if (Reserve[index + 1]/4 == 0 && Reserve[index + 1]%4 == handsArray[0]%4)
                {
                    player.Type = 5.5;
                    player.Power = 13 + player.Type*100;
                    Win.Add(new Type() {Power = player.Power, Current = 5.5});
                }
            }
        }

        private static void CheckStraight(Player player, int[] Straight, List<Type> Win)
        {
            if (player.Type >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {

                            player.Type = 4;
                            player.Power = op.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 4 });
                        }
                        else
                        {
                            player.Type = 4;
                            player.Power = op[j + 4] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 4 });
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        player.Type = 4;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 4 });
                    }
                }
            }
        }

        private static void CheckThreeOfAKind(Player player, int[] Straight, List<Type> Win)
        {
            if (player.Type >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            player.Type = 3;
                            player.Power = 13 * 3 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 3 });
                        }
                        else
                        {
                            player.Type = 3;
                            player.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 3 });
                        }
                    }
                }
            }
        }

        private static void CheckTwoPair(Player player, List<Type> Win, int[] Reserve, int i)
        {
            if (player.Type >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (Reserve[i] / 4 != Reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (Reserve[i] / 4 == Reserve[tc] / 4 && Reserve[i + 1] / 4 == Reserve[tc - k] / 4 ||
                                    Reserve[i + 1] / 4 == Reserve[tc] / 4 && Reserve[i] / 4 == Reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = 13 * 4 + (Reserve[i + 1] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = 13 * 4 + (Reserve[i] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                        else
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[i] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CheckPairTwoPair(Player player, List<Type> Win, int[] Reserve, int i)
        {
            if (player.Type >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (Reserve[tc] / 4 == Reserve[tc - k] / 4)
                            {
                                if (Reserve[tc] / 4 != Reserve[i] / 4 && Reserve[tc] / 4 != Reserve[i + 1] / 4 && player.Type == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[i] / 4) * 2 + 13 * 4 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[i + 1] / 4) * 2 + 13 * 4 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                        if (Reserve[i + 1] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[tc] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                        if (Reserve[i] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[tc] / 4) * 2 + (Reserve[i] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                        }
                                    }
                                    msgbox = true;
                                }

                                if (player.Type == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                player.Type = 0;
                                                player.Power = 13 + Reserve[i] / 4 + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.Power = Reserve[tc] / 4 + Reserve[i] / 4 + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                player.Type = 0;
                                                player.Power = 13 + Reserve[i + 1] + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.Power = Reserve[tc] / 4 + Reserve[i + 1] / 4 + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CheckPairFromHand(Player player, List<Type> Win, int[] Reserve, int i)
        {
            if (player.Type >= -1)
            {
                bool msgbox = false;
                if (Reserve[i] / 4 == Reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (Reserve[i] / 4 == 0)
                        {
                            player.Type = 1;
                            player.Power = 13 * 4 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 1 });
                        }
                        else
                        {
                            player.Type = 1;
                            player.Power = (Reserve[i + 1] / 4) * 4 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 1 });
                        }
                    }
                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (Reserve[i + 1] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i + 1] / 4 == 0)
                            {
                                player.Type = 1;
                                player.Power = 13 * 4 + Reserve[i] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                            }
                            else
                            {
                                player.Type = 1;
                                player.Power = (Reserve[i + 1] / 4) * 4 + Reserve[i] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                            }
                        }
                        msgbox = true;
                    }

                    if (Reserve[i] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i] / 4 == 0)
                            {
                                player.Type = 1;
                                player.Power = 13 * 4 + Reserve[i + 1] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                            }
                            else
                            {
                                player.Type = 1;
                                player.Power = (Reserve[tc] / 4) * 4 + Reserve[i + 1] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private static void CheckHighCard(Player player, List<Type> Win, int[] Reserve, int i)
        {
            if (player.Type == -1)
            {
                if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                {
                    player.Type = -1;
                    player.Power = Reserve[i] / 4;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                }
                else
                {
                    player.Type = -1;
                    player.Power = Reserve[i + 1] / 4;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                }

                if (Reserve[i] / 4 == 0 || Reserve[i + 1] / 4 == 0)
                {
                    player.Type = -1;
                    player.Power = 13;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                }
            }
        }
    }
}

