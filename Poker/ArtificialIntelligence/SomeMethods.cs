using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.ArtificialIntelligence
{
    using System.Windows.Forms;

    public class SomeMethods
    {
        public static void Check(ref bool cTurn,Label cStatus,ref bool raising  )
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        } 

        public static void Call(ref int sChips, ref bool sTurn, Label sStatus , int call,ref  bool raising,TextBox tbPot)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }

        public static void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus, ref bool raising)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        public static void Raised(ref int sChips, ref bool sTurn, Label sStatus, ref int call, ref bool raising, TextBox tbPot,ref double Raise)
        {
            sChips -= Convert.ToInt32(Raise);
            sStatus.Text = "Raise " + Raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            sTurn = false;
        }
        public static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

      
    }

   
}
