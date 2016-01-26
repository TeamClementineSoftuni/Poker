namespace Poker.Core
{
    using Models;
    using System;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class ArtificialIntelligence
    {
        public static void Apply(int c1, int c2, IPlayer player, IActsOnTable actsOnTable, int name, PictureBox[] Holder)
        {
            if (!player.FoldedTurn)
            {
                if (player.Result.Type == -1)
                {
                    //HighCard
                    ActHighCard(player, actsOnTable, 20, 25);
                }
                if (player.Result.Type == 0)
                {
                    //PairTable
                    ActHighCard(player, actsOnTable, 16, 25);
                }
                if (player.Result.Type == 1)
                {
                    ActPairHand(player, actsOnTable);
                }
                if (player.Result.Type == 2)
                {
                    ActTwoPair(player, actsOnTable);
                }
                if (player.Result.Type == 3)
                {
                    ActThreeOfAKind(player, actsOnTable, name);
                }
                if (player.Result.Type == 4)
                {
                    ActStraight(player, actsOnTable,name);
                }
                if (player.Result.Type == 5 || player.Result.Type == 5.5)
                {
                    ActFlush(player, actsOnTable, name);
                }
                if (player.Result.Type == 6)
                {
                    ActFullHouse(player, actsOnTable, name);
                }
                if (player.Result.Type == 7)
                {
                    ActFourOfAKind(player, actsOnTable, name);
                }
                if (player.Result.Type == 8 || player.Result.Type == 9)
                {
                    ActStraightFlush(player, actsOnTable, name);
                }
            }

            if (player.FoldedTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }

      private static void ActPairHand(IPlayer player, IActsOnTable actsOnTable)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (player.Result.Power <= 199 && player.Result.Power >= 140)
            {
                PH(player, actsOnTable, rCall, 6, rRaise);
            }
            if (player.Result.Power <= 139 && player.Result.Power >= 128)
            {
                PH(player, actsOnTable, rCall, 7, rRaise);
            }
            if (player.Result.Power < 128 && player.Result.Power >= 101)
            {
                PH(player, actsOnTable, rCall, 9, rRaise);
            }
        }

        private static void ActTwoPair(IPlayer player, IActsOnTable actsOnTable)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (player.Result.Power <= 290 && player.Result.Power >= 246)
            {
                PH(player, actsOnTable, rCall, 3, rRaise);
            }
            if (player.Result.Power <= 244 && player.Result.Power >= 234)
            {
                PH(player, actsOnTable, rCall, 4, rRaise);
            }
            if (player.Result.Power < 234 && player.Result.Power >= 201)
            {
                PH(player, actsOnTable, rCall, 4, rRaise);
            }
        }

        private static void ActThreeOfAKind(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Result.Power <= 390 && player.Result.Power >= 330)
            {
                Smooth(player, actsOnTable, name, tCall, tRaise);
            }
            if (player.Result.Power <= 327 && player.Result.Power >= 321)//10  8
            {
                Smooth(player, actsOnTable, name, tCall, tRaise);
            }
            if (player.Result.Power < 321 && player.Result.Power >= 303)//7 2
            {
                Smooth(player, actsOnTable, name, tCall, tRaise);
            }
        }

        private static void ActStraight(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Result.Power <= 480 && player.Result.Power >= 410)
            {
                Smooth(player, actsOnTable, name, sCall, sRaise);
            }
            if (player.Result.Power <= 409 && player.Result.Power >= 407)//10  8
            {
                Smooth(player, actsOnTable, name, sCall, sRaise);
            }
            if (player.Result.Power < 407 && player.Result.Power >= 404)
            {
                Smooth(player, actsOnTable, name, sCall, sRaise);
            }
        }

        private static void ActFlush(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(player, actsOnTable, name, fCall, fRaise);
        }

        private static void ActFullHouse(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (player.Result.Power <= 626 && player.Result.Power >= 620)
            {
                Smooth(player, actsOnTable, name, fhCall, fhRaise);
            }
            if (player.Result.Power < 620 && player.Result.Power >= 602)
            {
                Smooth(player, actsOnTable, name, fhCall, fhRaise);
            }
        }

        private static void ActFourOfAKind(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Result.Power <= 752 && player.Result.Power >= 704)
            {
                Smooth(player, actsOnTable, name, fkCall, fkRaise);
            }
        }

        private static void ActStraightFlush(IPlayer player, IActsOnTable actsOnTable, int name)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Result.Power <= 913 && player.Result.Power >= 804)
            {
                Smooth(player, actsOnTable, name, sfCall, sfRaise);
            }
        }


        private static void ActHighCard(IPlayer player, IActsOnTable actsOnTable, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);

            if (actsOnTable.CallAmount <= 0)
            {
                Check(player, actsOnTable);
            }

            if (actsOnTable.CallAmount > 0)
            {
                if (rnd == 1)
                {
                    if (actsOnTable.CallAmount <= RoundN(player.ChipsSet.Amount, n))
                    {
                        Call(player, actsOnTable);
                    }
                    else
                    {
                        Fold(player, actsOnTable);
                    }
                }

                if (rnd == 2)
                {
                    if (actsOnTable.CallAmount <= RoundN(player.ChipsSet.Amount, n1))
                    {
                        Call(player, actsOnTable);
                    }
                    else
                    {
                        Fold(player, actsOnTable);
                    }
                }
            }

            if (rnd == 3)
            {
                if (actsOnTable.RaiseAmount == 0)
                {
                    actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                    Raised(player, actsOnTable);
                }
                else
                {
                    if (actsOnTable.RaiseAmount <= RoundN(player.ChipsSet.Amount, n))
                    {
                        actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                        Raised(player, actsOnTable);
                    }
                    else
                    {
                        Fold(player, actsOnTable);
                    }
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }


        private static void PH(IPlayer player, IActsOnTable actsOnTable, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);

            if (actsOnTable.RoundsPassed < 2)
            {
                if (actsOnTable.CallAmount <= 0)
                {
                    Check(player, actsOnTable);
                }

                if (actsOnTable.CallAmount > 0)
                {
                    if (actsOnTable.CallAmount >= RoundN(player.ChipsSet.Amount, n))
                    {
                        Fold(player, actsOnTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (actsOnTable.CallAmount == RoundN(player.ChipsSet.Amount, n))
                        {
                            Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= RoundN(player.ChipsSet.Amount, n) && actsOnTable.RaiseAmount >= RoundN(player.ChipsSet.Amount, n) / 2)
                        {
                            Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= RoundN(player.ChipsSet.Amount, n) / 2)
                        {
                            if (actsOnTable.RaiseAmount > 0)
                            {
                                actsOnTable.RaiseAmount = RoundN(player.ChipsSet.Amount, n);
                                Raised(player, actsOnTable);
                            }
                            else
                            {
                                actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                                Raised(player, actsOnTable);
                            }
                        }
                    }
                }
            }

            if (actsOnTable.RoundsPassed >= 2)
            {
                if (actsOnTable.CallAmount > 0)
                {
                    if (actsOnTable.CallAmount >= RoundN(player.ChipsSet.Amount, n1 - rnd))
                    {
                        Fold(player, actsOnTable);
                    }

                    if (actsOnTable.RaiseAmount > RoundN(player.ChipsSet.Amount, n - rnd))
                    {
                        Fold(player, actsOnTable);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (actsOnTable.CallAmount >= RoundN(player.ChipsSet.Amount, n - rnd) && actsOnTable.CallAmount <= RoundN(player.ChipsSet.Amount, n1 - rnd))
                        {
                            Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= RoundN(player.ChipsSet.Amount, n - rnd) && actsOnTable.RaiseAmount >= (RoundN(player.ChipsSet.Amount, n - rnd)) / 2)
                        {
                            Call(player, actsOnTable);
                        }

                        if (actsOnTable.RaiseAmount <= RoundN(player.ChipsSet.Amount, n - rnd) / 2)
                        {
                            if (actsOnTable.RaiseAmount > 0)
                            {
                                actsOnTable.RaiseAmount = RoundN(player.ChipsSet.Amount, n - rnd);
                                Raised(player, actsOnTable);
                            }
                            else
                            {
                                actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;
                                Raised(player, actsOnTable);
                            }
                        }
                    }
                }

                if (actsOnTable.CallAmount <= 0)
                {
                    actsOnTable.RaiseAmount = RoundN(player.ChipsSet.Amount, r - rnd);
                    Raised(player, actsOnTable);
                }
            }

            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        private static void Smooth(IPlayer player, IActsOnTable actsOnTable, int name, int n, int r)
        {
            if (actsOnTable.CallAmount <= 0)
            {
                Check(player, actsOnTable);
            }
            else
            {
                if (actsOnTable.CallAmount >= RoundN(player.ChipsSet.Amount, n))
                {
                    if (player.ChipsSet.Amount > actsOnTable.CallAmount)
                    {
                        Call(player, actsOnTable);
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
                            Raised(player, actsOnTable);
                        }
                        else
                        {
                            Call(player, actsOnTable);
                        }
                    }
                    else
                    {
                        actsOnTable.RaiseAmount = actsOnTable.CallAmount * 2;

                        Raised(player, actsOnTable);
                    }
                }
            }
            if (player.ChipsSet.Amount <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        private static void Check(IPlayer player, IActsOnTable actsOnTable)
        {
            player.StatusLabel.Text = "Check";
            player.Turn = false;
            actsOnTable.IsRaised = false;
        }

        private static void Call(IPlayer player, IActsOnTable actsOnTable)
        {
            actsOnTable.IsRaised = false;
            player.Turn = false;
            player.ChipsSet.Amount -= actsOnTable.CallAmount;
            player.StatusLabel.Text = "Call " + actsOnTable.CallAmount;
            Pot.Instance.ChipsSet.Amount += actsOnTable.CallAmount;
        }

        private static void Fold(IPlayer player, IActsOnTable actsOnTable)
        {
            actsOnTable.IsRaised = false;
            player.StatusLabel.Text = "Fold";
            player.Turn = false;
            player.FoldedTurn = true;
        }

        private static void Raised(IPlayer player, IActsOnTable actsOnTable)
        {
            player.ChipsSet.Amount -= Convert.ToInt32(actsOnTable.RaiseAmount);
            player.StatusLabel.Text = "Raise " + actsOnTable.RaiseAmount;
            Pot.Instance.ChipsSet.Amount += Convert.ToInt32(actsOnTable.RaiseAmount);
            actsOnTable.CallAmount = Convert.ToInt32(actsOnTable.RaiseAmount);
            actsOnTable.IsRaised = true;
            player.Turn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
    }
}

