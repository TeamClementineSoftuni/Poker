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

    public class HPAndPHAndSmooth
    {
        public static void HP(Player player, ActsOnTable onTable,  int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (onTable.Call <= 0)
            {
                SomeMethods.Check(player,onTable);
            }

            if (onTable.Call > 0)
            {
                if (rnd == 1)
                {
                    if (onTable.Call <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Call(player, onTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, onTable);
                    }
                }

                if (rnd == 2)
                {
                    if (onTable.Call <= SomeMethods.RoundN(player.ChipsSet.Amount, n1))
                    {
                        SomeMethods.Call(player,  onTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, onTable);
                    }
                }
            }
            if (rnd == 3)
            {
                if (onTable.Raise == 0)
                {
                    onTable.Raise = onTable.Call * 2;
                    SomeMethods.Raised(player, onTable);
                }
                else
                {
                    if (onTable.Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        onTable.Raise = onTable.Call * 2;
                        SomeMethods.Raised(player,onTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, onTable);
                    }
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }


        public static void PH(Player player,ActsOnTable onTable,   int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (onTable.Rounds < 2)
            {
                if (onTable.Call <= 0)
                {
                    SomeMethods.Check(player, onTable);
                }

                if (onTable.Call > 0)
                {
                    if (onTable.Call >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player, onTable);
                    }

                    if (onTable.Raise > SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player, onTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (onTable.Call >= SomeMethods.RoundN(player.ChipsSet.Amount, n) && onTable.Call <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                        {
                            SomeMethods.Call(player, onTable);
                        }

                        if (onTable.Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n) && onTable.Raise >= SomeMethods.RoundN(player.ChipsSet.Amount, n) / 2)
                        {
                            SomeMethods.Call(player, onTable);
                        }

                        if (onTable.Raise <= (SomeMethods.RoundN(player.ChipsSet.Amount, n)) / 2)
                        {
                            if (onTable.Raise > 0)
                            {
                                onTable.Raise = SomeMethods.RoundN(player.ChipsSet.Amount, n);
                                SomeMethods.Raised(player,onTable);
                            }
                            else
                            {
                                onTable.Raise = onTable.Call * 2;
                                SomeMethods.Raised(player, onTable);
                            }
                        }
                    }
                }
            }

            if (onTable.Rounds >= 2)
            {
                if (onTable.Call > 0)
                {
                    if (onTable.Call >= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                    {
                        SomeMethods.Fold(player, onTable);
                    }

                    if (onTable.Raise > SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd))
                    {
                        SomeMethods.Fold(player, onTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (onTable.Call >= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && onTable.Call <= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                        {
                            SomeMethods.Call(player, onTable);
                        }
                        if (onTable.Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && onTable.Raise >= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            SomeMethods.Call(player,onTable);
                        }
                        if (onTable.Raise <= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            if (onTable.Raise > 0)
                            {
                                onTable.Raise = SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd);
                                SomeMethods.Raised(player,onTable);
                            }
                            else
                            {
                                onTable.Raise = onTable.Call * 2;
                                SomeMethods.Raised(player, onTable);
                            }
                        }
                    }
                }

                if (onTable.Call <= 0)
                {
                    onTable.Raise = SomeMethods.RoundN(player.ChipsSet.Amount, r - rnd);
                    SomeMethods.Raised(player, onTable);
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }
       
        public static void Smooth(Player player, ActsOnTable onTable, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (onTable.Call <= 0)
            {
                SomeMethods.Check(player, onTable);
            }
            else
            {
                if (onTable.Call >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                {
                    if (player.ChipsSet.Amount > onTable.Call)
                    {
                        SomeMethods.Call(player,onTable);
                    }
                    else if (player.ChipsSet.Amount <= onTable.Call)
                    {
                        onTable.Raising = false;
                        player.Turn = false;
                        player.ChipsSet.Amount = 0;
                        player.StatusLabel.Text = "Call " + player.ChipsSet;
                        Pot.Instance.ChipsSet.Amount += player.ChipsSet.Amount;
                    }
                }
                else
                {
                    if (onTable.Raise > 0)
                    {
                        if (player.ChipsSet.Amount >= onTable.Raise * 2)
                        {
                            onTable.Raise *= 2;
                            SomeMethods.Raised(player, onTable);
                        }
                        else
                        {
                            SomeMethods.Call(player,onTable);
                        }
                    }
                    else
                    {
                        onTable.Raise = onTable.Call * 2;
                        
                        SomeMethods.Raised(player, onTable);
                    }
                }
            }
            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }
    }
}
