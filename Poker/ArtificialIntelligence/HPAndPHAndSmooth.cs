using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class HPAndPHAndSmooth
    {
        public static void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1,ref int call, ref double Raise, ref bool raising, TextBox tbPod)
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
                    if (call <= SomeMethods.RoundN(sChips, n))
                    {
                        SomeMethods.Call(ref sChips, ref sTurn, sStatus,call,ref raising, tbPod);
                    }
                    else
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus,ref raising);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= SomeMethods.RoundN(sChips, n1))
                    {
                        SomeMethods.Call(ref sChips, ref sTurn, sStatus, call, ref raising, tbPod);
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
                    SomeMethods.Raised(ref sChips, ref sTurn, sStatus,ref call,ref raising,tbPod, ref Raise);
                }
                else
                {
                    if (Raise <= SomeMethods.RoundN(sChips, n))
                    {
                        Raise = call * 2;
                        SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPod, ref Raise);
                    }
                    else
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }


        public static void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r, ref double rounds, ref int call, ref double Raise, ref bool raising, TextBox tbPot)
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
                    if (call >= SomeMethods.RoundN(sChips, n))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(sChips, n))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= SomeMethods.RoundN(sChips, n) && call <= SomeMethods.RoundN(sChips, n))
                        {
                            SomeMethods.Call(ref sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(sChips, n) && Raise >= SomeMethods.RoundN(sChips, n) / 2)
                        {
                            SomeMethods.Call(ref sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(sChips, n)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(sChips, n);
                                SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= SomeMethods.RoundN(sChips, n1 - rnd))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > SomeMethods.RoundN(sChips, n - rnd))
                    {
                        SomeMethods.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= SomeMethods.RoundN(sChips, n - rnd) && call <= SomeMethods.RoundN(sChips, n1 - rnd))
                        {
                            SomeMethods.Call(ref sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= SomeMethods.RoundN(sChips, n - rnd) && Raise >= (SomeMethods.RoundN(sChips, n - rnd)) / 2)
                        {
                            SomeMethods.Call(ref sChips, ref sTurn, sStatus, call, ref raising, tbPot);
                        }
                        if (Raise <= (SomeMethods.RoundN(sChips, n - rnd)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = SomeMethods.RoundN(sChips, n - rnd);
                                SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                            else
                            {
                                Raise = call * 2;
                                SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    Raise = SomeMethods.RoundN(sChips, r - rnd);
                    SomeMethods.Raised(ref sChips, ref sTurn, sStatus, ref call, ref raising, tbPot, ref Raise);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
       
        public static void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r, ref int call, ref double Raise,ref bool raising, TextBox tbPot)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                SomeMethods.Check(ref botTurn, botStatus, ref raising);
            }
            else
            {
                if (call >= SomeMethods.RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        SomeMethods.Call(ref botChips, ref botTurn, botStatus, call, ref raising, tbPot);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips >= Raise * 2)
                        {
                            Raise *= 2;
                            SomeMethods.Raised(ref botChips, ref botTurn, botStatus, ref call, ref raising, tbPot, ref Raise);
                        }
                        else
                        {
                            SomeMethods.Call(ref botChips, ref botTurn, botStatus, call, ref raising, tbPot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        
                        SomeMethods.Raised(ref botChips, ref botTurn, botStatus, ref call, ref raising, tbPot, ref Raise);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

    }
}
