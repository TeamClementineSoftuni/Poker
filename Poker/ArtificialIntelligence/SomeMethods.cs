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

    public class SomeMethods
    {
        public static void Check(Player player, ActsOnTable actsOnTable)
        {
            player.StatusLabel.Text = "Check";
            player.Turn = false;
            actsOnTable.IsRaised = false;
        } 

        public static void Call(Player player, ActsOnTable actsOnTable)
        {
            actsOnTable.IsRaised = false;
            player.Turn = false;
            player.ChipsSet.Amount -= actsOnTable.CallAmount;
            player.StatusLabel.Text = "Call " + actsOnTable.CallAmount;
            Pot.Instance.ChipsSet.Amount += actsOnTable.CallAmount;
        }

        public static void Fold(Player player ,  ActsOnTable actsOnTable)
        {
            actsOnTable.IsRaised = false;
            player.StatusLabel.Text = "Fold";
            player.Turn = false;
            player.FoldedTurn = true;
        }

        public static void Raised(Player player, ActsOnTable actsOnTable)
        {
            player.ChipsSet.Amount -= Convert.ToInt32(actsOnTable.RaiseAmount);
            player.StatusLabel.Text = "Raise " + actsOnTable.RaiseAmount;
            Pot.Instance.ChipsSet.Amount += Convert.ToInt32(actsOnTable.RaiseAmount);
            actsOnTable.CallAmount = Convert.ToInt32(actsOnTable.RaiseAmount);
            actsOnTable.IsRaised = true;
            player.Turn = false;
        }

        public static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
    }
}
