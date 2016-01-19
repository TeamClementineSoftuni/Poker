using Poker.Models.Players;

namespace Poker.Rules
{
    using System.Collections.Generic;
    using System.Linq;

    using Type = Poker.Type;

    public class RulesWinningHands
    {

        public static void StraightFlushRules(Player player, int[] st1, int[] st2, int[] st3, int[] st4, List<Type> Win, ref Type sorted)
        {
            if (player.Type >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        player.Type = 8;
                        player.Power = (st1.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        player.Type = 9;
                        player.Power = (st1.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        player.Type = 8;
                        player.Power = (st2.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        player.Type = 9;
                        player.Power = (st2.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        player.Type = 8;
                        player.Power = (st3.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        player.Type = 9;
                        player.Power = (st3.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        player.Type = 8;
                        player.Power = (st4.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        player.Type = 9;
                        player.Power = (st4.Max()) / 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void FourOfAKindRules(Player player, int[] Straight, List<Type> Win, ref Type sorted)
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
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First(); 
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        player.Type = 7;
                        player.Power = 13 * 4 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void FullHouseRules(Player player, ref bool done, int[] Straight, List<Type> Win, ref Type sorted, int[] Reserve, double type)
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
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First(); 
                             
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                player.Type = 6;
                                player.Power = fh.Max() / 4 * 2 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                
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

        public static void FlushRules(Player player, ref bool vf, int[] Straight1, List<Type> Win, ref Type sorted, int[] Reserve, int i)
        {
            
            if (player.Type >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();

                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });

                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();

                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f1.Max() / 4 && Reserve[i + 1] / 4 < f1.Max() / 4)
                        {

                            player.Type = 5;
                            player.Power = f1.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    { 
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                           
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f1.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f1.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (Reserve[i] % 4 == f1[0] % 4 && Reserve[i] / 4 > f1.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f1[0] % 4 && Reserve[i + 1] / 4 > f1.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i + 1] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f1.Min() / 4 && Reserve[i + 1] / 4 < f1.Min())
                    {
                        player.Type = 5;
                        player.Power = f1.Max() + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f2.Max() / 4 && Reserve[i + 1] / 4 < f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = f2.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f2.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f2.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (Reserve[i] % 4 == f2[0] % 4 && Reserve[i] / 4 > f2.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f2[0] % 4 && Reserve[i + 1] / 4 > f2.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i + 1] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f2.Min() / 4 && Reserve[i + 1] / 4 < f2.Min())
                    {
                        player.Type = 5;
                        player.Power = f2.Max() + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f3.Max() / 4 && Reserve[i + 1] / 4 < f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = f3.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f3.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f3.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (Reserve[i] % 4 == f3[0] % 4 && Reserve[i] / 4 > f3.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f3[0] % 4 && Reserve[i + 1] / 4 > f3.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i + 1] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f3.Min() / 4 && Reserve[i + 1] / 4 < f3.Min())
                    {
                        player.Type = 5;
                        player.Power = f3.Max() + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f4.Max() / 4 && Reserve[i + 1] / 4 < f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = f4.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f4.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.Power = Reserve[i + 1] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.Power = f4.Max() + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (Reserve[i] % 4 == f4[0] % 4 && Reserve[i] / 4 > f4.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f4[0] % 4 && Reserve[i + 1] / 4 > f4.Min() / 4)
                    {
                        player.Type = 5;
                        player.Power = Reserve[i + 1] + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f4.Min() / 4 && Reserve[i + 1] / 4 < f4.Min())
                    {
                        player.Type = 5;
                        player.Power = f4.Max() + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        player.Type = 5.5;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void StraightRules(Player player, int[] Straight, List<Type> Win, ref Type sorted)
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
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            player.Type = 4;
                            player.Power = op[j + 4] + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        player.Type = 4;
                        player.Power = 13 + player.Type * 100;
                        Win.Add(new Type() { Power = player.Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void ThreeOfAKindRules(Player player, int[] Straight, List<Type> Win, ref Type sorted)
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
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type = 3;
                            player.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        public static void TwoPairRules(Player player, List<Type> Win, ref Type sorted, int[] Reserve, int i)
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
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = 13 * 4 + (Reserve[i] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0 && Reserve[i] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[i] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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

        public static void PairTwoPairRules(Player player, List<Type> Win, ref Type sorted, int[] Reserve, int i)
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
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[i + 1] / 4) * 2 + 13 * 4 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[tc] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.Power = (Reserve[tc] / 4) * 2 + (Reserve[i] / 4) * 2 + player.Type * 100;
                                            Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.Power = Reserve[tc] / 4 + Reserve[i] / 4 + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                player.Type = 0;
                                                player.Power = 13 + Reserve[i + 1] + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.Power = Reserve[tc] / 4 + Reserve[i + 1] / 4 + player.Type * 100;
                                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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

        public static void PairFromHandRules(Player player, List<Type> Win, ref Type sorted, int[] Reserve, int i)
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
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type = 1;
                            player.Power = (Reserve[i + 1] / 4) * 4 + player.Type * 100;
                            Win.Add(new Type() { Power = player.Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                player.Type = 1;
                                player.Power = (Reserve[i + 1] / 4) * 4 + Reserve[i] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                player.Type = 1;
                                player.Power = (Reserve[tc] / 4) * 4 + Reserve[i + 1] / 4 + player.Type * 100;
                                Win.Add(new Type() { Power = player.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        public static void HighCardRules(Player player, List<Type> Win, ref Type sorted, int[] Reserve, int i)
        {
            if (player.Type == -1)
            {
                if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                {
                    player.Type = -1;
                    player.Power = Reserve[i] / 4;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
       
                }
                else
                {
                    player.Type = -1;
                    player.Power = Reserve[i + 1] / 4;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                  
                }
                if (Reserve[i] / 4 == 0 || Reserve[i + 1] / 4 == 0)
                {
                    player.Type = -1;
                    player.Power = 13;
                    Win.Add(new Type() { Power = player.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
           
                }
            }
        }
    }
}
