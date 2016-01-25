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
        public static void Check(Player player, ActsOnTable onTable)
        {
            player.StatusLabel.Text = "Check";
            player.Turn = false;
            onTable.Raising = false;
        } 

        public static void Call(Player player, ActsOnTable onTable)
        {
            onTable.Raising = false;
            player.Turn = false;
            player.ChipsSet.Amount -= onTable.Call;
            player.StatusLabel.Text = "Call " + onTable.Call;
            Pot.Instance.ChipsSet.Amount += onTable.Call;
        }

        public static void Fold(Player player ,  ActsOnTable onTable)
        {
            onTable.Raising = false;
            player.StatusLabel.Text = "Fold";
            player.Turn = false;
            player.FoldedTurn = true;
        }

        public static void Raised(Player player, ActsOnTable onTable)
        {
            player.ChipsSet.Amount -= Convert.ToInt32(onTable.Raise);
            player.StatusLabel.Text = "Raise " + onTable.Raise;
            Pot.Instance.ChipsSet.Amount += Convert.ToInt32(onTable.Raise);
            onTable.Call = Convert.ToInt32(onTable.Raise);
            onTable.Raising = true;
            player.Turn = false;
        }

        public static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
    }
}
