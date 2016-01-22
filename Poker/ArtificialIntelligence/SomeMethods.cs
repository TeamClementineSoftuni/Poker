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
        public static void Check(Player player, ref bool raising  )
        {
            player.StatusLabel.Text = "Check";
            player.Turn = false;
            raising = false;
        } 

        public static void Call(Player player,  int call,ref  bool raising,TextBox tbPot)
        {
            raising = false;
            player.Turn = false;
            player.ChipsSet.Amount -= call;
            player.StatusLabel.Text = "Call " + call;
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }

        public static void Fold(Player player ,  ref bool raising)
        {
            raising = false;
            player.StatusLabel.Text = "Fold";
            player.Turn = false;
            player.FoldedTurn = true;
        }

        public static void Raised(Player player,  ref int call, ref bool raising, TextBox tbPot,ref double Raise)
        {
            player.ChipsSet.Amount -= Convert.ToInt32(Raise);
            player.StatusLabel.Text = "Raise " + Raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            player.Turn = false;
        }

        public static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

      
    }

   
}
