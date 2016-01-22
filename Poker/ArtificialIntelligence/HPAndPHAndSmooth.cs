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
        public static void HP(Player player, ref bool sTurn, int n, int n1,ref int call, ref double Raise, ref bool raising, TextBox tbPod)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                SomeMethods.Check( player,ref sTurn,ref  raising);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Call(player, ref sTurn,call,ref raising, tbPod);
                    }
                    else
                    {
                        SomeMethods.Fold(player,ref sTurn, ref raising);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= SomeMethods.RoundN(player.ChipsSet.Amount, n1))
                    {
                        SomeMethods.Call(player, ref sTurn,  call, ref raising, tbPod);
                    }
                    else
                    {
                        SomeMethods.Fold(player,ref sTurn,   ref raising);
                    }
                }
            }
            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    SomeMethods.Raised(player, ref sTurn, ref call,ref raising,tbPod, ref Raise);
                }
                else
                {
                    if (Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        Raise = call * 2;
                        SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPod, ref Raise);
                    }
                    else
                    {
                        SomeMethods.Fold(player,ref sTurn,   ref raising);
                    }
                }
            }
            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }


        public static void PH(Player player, ref bool sTurn,  int n, int n1, int r, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    SomeMethods.Check(player,ref sTurn, ref raising);
                }
                if (call > 0)
                {
                    if (call >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player,ref sTurn,   ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(player.ChipsSet.Amount, n))
                    {
                        SomeMethods.Fold(player,ref sTurn,   ref raising);
                    }
                    if (!player.FoldedTurn)
                    {
                        if (call >= SomeMethods.RoundN(player.ChipsSet.Amount, n) && call <= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                        {
                            SomeMethods.Call(player, ref sTurn,  call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n) && Raise >= SomeMethods.RoundN(player.ChipsSet.Amount, n) / 2)
                        {
                            SomeMethods.Call(player, ref sTurn,  call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(player.ChipsSet.Amount, n)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(player.ChipsSet.Amount, n);
                                SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPot, ref Raise);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                    {
                        SomeMethods.Fold(player,ref sTurn,  ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd))
                    {
                        SomeMethods.Fold(player,ref sTurn,   ref raising);
                    }
                    if (!player.FoldedTurn)
                    {
                        if (call >= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && call <= SomeMethods.RoundN(player.ChipsSet.Amount, n1 - rnd))
                        {
                            SomeMethods.Call(player, ref sTurn,  call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd) && Raise >= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            SomeMethods.Call(player, ref sTurn,  call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(player.ChipsSet.Amount, n - rnd);
                                SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPot, ref Raise);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    Raise = SomeMethods.RoundN(player.ChipsSet.Amount, r - rnd);
                    SomeMethods.Raised(player, ref sTurn,  ref call, ref raising, tbPot, ref Raise);
                }
            }
            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }
       
        public static void Smooth(Player player, ref bool botTurn,  int name, int n, int r, ref int call, ref double Raise,ref bool raising, TextBox tbPot)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                SomeMethods.Check(player,ref botTurn,  ref raising);
            }
            else
            {
                if (call >= SomeMethods.RoundN(player.ChipsSet.Amount, n))
                {
                    if (player.ChipsSet.Amount > call)
                    {
                        SomeMethods.Call(player, ref botTurn,  call, ref raising, tbPot);
                    }
                    else if (player.ChipsSet.Amount <= call)
                    {
                        raising = false;
                        botTurn = false;
                        player.ChipsSet.Amount = 0;
                        player.StatusLabel.Text = "Call " + player.ChipsSet;
                        tbPot.Text = (int.Parse(tbPot.Text) + player.ChipsSet.Amount).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (player.ChipsSet.Amount >= Raise * 2)
                        {
                            Raise *= 2;
                            SomeMethods.Raised(player, ref botTurn,  ref call, ref raising, tbPot, ref Raise);
                        }
                        else
                        {
                            SomeMethods.Call(player, ref botTurn,  call, ref raising, tbPot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        
                        SomeMethods.Raised(player, ref botTurn,  ref call, ref raising, tbPot, ref Raise);
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
