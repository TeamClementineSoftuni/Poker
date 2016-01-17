using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Models;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class HPAndPHAndSmooth
    {
        public static void HP(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1,ref int call, ref double Raise, ref bool raising, TextBox tbPod)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                SomeMethods.Check(ref sTurn, sStatus,ref  raising);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= SomeMethods.RoundN(sChips.Amount, n))
                    {
                        SomeMethods.Call(sChips, ref sTurn, sStatus,call,ref raising, tbPod);
                    }
                    else
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus,ref raising);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= SomeMethods.RoundN(sChips.Amount, n1))
                    {
                        SomeMethods.Call(sChips, ref sTurn, sStatus, call, ref raising, tbPod);
                    }
                    else
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
            }
            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    SomeMethods.Raised(sChips, ref sTurn, sStatus,ref call,ref raising,tbPod, ref Raise);
                }
                else
                {
                    if (Raise <= SomeMethods.RoundN(sChips.Amount, n))
                    {
                        Raise = call * 2;
                        SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPod, ref Raise);
                    }
                    else
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
            }
            if (sChips.Amount <= 0)
            {
                sFTurn = true;
            }
        }


        public static void PH(ChipsSet sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    SomeMethods.Check(ref sTurn, sStatus,ref raising);
                }
                if (call > 0)
                {
                    if (call >= SomeMethods.RoundN(sChips.Amount, n))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(sChips.Amount, n))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= SomeMethods.RoundN(sChips.Amount, n) && call <= SomeMethods.RoundN(sChips.Amount, n))
                        {
                            SomeMethods.Call(sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(sChips.Amount, n) && Raise >= SomeMethods.RoundN(sChips.Amount, n) / 2)
                        {
                            SomeMethods.Call(sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(sChips.Amount, n)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(sChips.Amount, n);
                                SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= SomeMethods.RoundN(sChips.Amount, n1 - rnd))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(sChips.Amount, n - rnd))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= SomeMethods.RoundN(sChips.Amount, n - rnd) && call <= SomeMethods.RoundN(sChips.Amount, n1 - rnd))
                        {
                            SomeMethods.Call(sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(sChips.Amount, n - rnd) && Raise >= (SomeMethods.RoundN(sChips.Amount, n - rnd)) / 2)
                        {
                            SomeMethods.Call(sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(sChips.Amount, n - rnd)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(sChips.Amount, n - rnd);
                                SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    Raise = SomeMethods.RoundN(sChips.Amount, r - rnd);
                    SomeMethods.Raised(sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                }
            }
            if (sChips.Amount <= 0)
            {
                sFTurn = true;
            }
        }
       
        public static void Smooth(ChipsSet botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r, ref int call, ref double Raise,ref bool raising, TextBox tbPot)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                SomeMethods.Check(ref botTurn, botStatus, ref raising);
            }
            else
            {
                if (call >= SomeMethods.RoundN(botChips.Amount, n))
                {
                    if (botChips.Amount > call)
                    {
                        SomeMethods.Call(botChips, ref botTurn, botStatus, call, ref raising, tbPot);
                    }
                    else if (botChips.Amount <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips.Amount = 0;
                        botStatus.Text = "Call " + botChips;
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips.Amount).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips.Amount >= Raise * 2)
                        {
                            Raise *= 2;
                            SomeMethods.Raised(botChips, ref botTurn, botStatus, ref call, ref raising, tbPot, ref Raise);
                        }
                        else
                        {
                            SomeMethods.Call(botChips, ref botTurn, botStatus, call, ref raising, tbPot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        
                        SomeMethods.Raised(botChips, ref botTurn, botStatus, ref call, ref raising, tbPot, ref Raise);
                    }
                }
            }
            if (botChips.Amount <= 0)
            {
                botFTurn = true;
            }
        }

    }
}
