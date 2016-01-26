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
        public static void HP(Player player, ActsOnTable actsOnTable,  int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (actsOnTable.CallAmount <= 0)
            {
                SomeMethods.Check(player,actsOnTable);
            }

            if (actsOnTable.CallAmount > 0)
            {
                if (rnd == 1)
                {
                    if (actsOnTable.CallAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Call(player, actsOnTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }
                }

                if (rnd == 2)
                {
                    if (actsOnTable.CallAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n1))
                    {
                        SomeMethods.Call(player,  actsOnTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }
                }
            }
            if (rnd == 3)
            {
                if (actsOnTable.RaiseAmount == 0)
                {
                    actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                    SomeMethods.Raised(player, actsOnTable);
                }
                else
                {
                    if (actsOnTable.RaiseAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                        SomeMethods.Raised(player,actsOnTable);
                    }
                    else
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }


        public static void PH(Player player, ActsOnTable actsOnTable, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (actsOnTable.RoundsPassed < 2)
            {
                if (actsOnTable.CallAmount <= 0)
                {
                    SomeMethods.Check(player, actsOnTable);
                }

                if (actsOnTable.CallAmount > 0)
                {
                    if (actsOnTable.CallAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }

                    if (actsOnTable.CallAmount > SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (actsOnTable.CallAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n) && actsOnTable.CallAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                        {
                            SomeMethods.Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n) && actsOnTable.RaiseAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n) / 2)
                        {
                            SomeMethods.Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= (SomeMethods.RoundN(player.ChipsSet.Amount, n)) / 2)
                        {
                            if (actsOnTable.RaiseAmount > 0)
                            {
                                actsOnTable.RaiseAmount = SomeMethods.RoundN(player.ChipsSet.Amount, n);
                                SomeMethods.Raised(player,actsOnTable);
                            }
                            else
                            {
                                actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                                SomeMethods.Raised(player, actsOnTable);
                            }
                        }
                    }
                }
            }

            if (actsOnTable.RoundsPassed >= 2)
            {
                if (actsOnTable.CallAmount > 0)
                {
                    if (actsOnTable.CallAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }

                    if (actsOnTable.RaiseAmount > SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd))
                    {
                        SomeMethods.Fold(player, actsOnTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (actsOnTable.CallAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && actsOnTable.CallAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                        {
                            SomeMethods.Call(player, actsOnTable);
                        }
                        if (actsOnTable.RaiseAmount <= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && actsOnTable.RaiseAmount >= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            SomeMethods.Call(player,actsOnTable);
                        }
                        if (actsOnTable.RaiseAmount <= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            if (actsOnTable.RaiseAmount > 0)
                            {
                                actsOnTable.RaiseAmount = SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd);
                                SomeMethods.Raised(player,actsOnTable);
                            }
                            else
                            {
                                actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                                SomeMethods.Raised(player, actsOnTable);
                            }
                        }
                    }
                }

                if (actsOnTable.CallAmount <= 0)
                {
                    actsOnTable.RaiseAmount = SomeMethods.RoundN(player.ChipsSet.Amount, r - rnd);
                    SomeMethods.Raised(player, actsOnTable);
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }
       
        public static void Smooth(Player player, ActsOnTable actsOnTable, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (actsOnTable.CallAmount <= 0)
            {
                SomeMethods.Check(player, actsOnTable);
            }
            else
            {
                if (actsOnTable.CallAmount >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                {
                    if (player.ChipsSet.Amount > actsOnTable.CallAmount)
                    {
                        SomeMethods.Call(player,actsOnTable);
                    }
                    else if (player.ChipsSet.Amount <= actsOnTable.CallAmount)
                    {
                        actsOnTable.IsRaised = false;
                        player.Turn = false;
                        player.ChipsSet.Amount = 0;
                        player.StatusLabel.Text = "Call " + player.ChipsSet;
                        Pot.Instance.ChipsSet.Amount += player.ChipsSet.Amount;
                    }
                }
                else
                {
                    if (actsOnTable.RaiseAmount > 0)
                    {
                        if (player.ChipsSet.Amount >= actsOnTable.RaiseAmount * 2)
                        {
                            actsOnTable.RaiseAmount *= 2;
                            SomeMethods.Raised(player, actsOnTable);
                        }
                        else
                        {
                            SomeMethods.Call(player,actsOnTable);
                        }
                    }
                    else
                    {
                        actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                        
                        SomeMethods.Raised(player, actsOnTable);
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
