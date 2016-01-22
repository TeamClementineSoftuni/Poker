using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models
{
    public class Pot
    {
        private static Pot instance;

        private Pot()
        { 
            ChipsSet  = new ChipsSet();
            this.PotTextBox = new TextBox();
        }

        public static Pot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Pot();
                }
                return instance;
            }
        }

        public TextBox PotTextBox { get; set; }

        public ChipsSet ChipsSet { get; set; }

        public override string ToString()
        {
            string potChipSetToString = this.ChipsSet.Amount.ToString();
            return potChipSetToString;
        }
    }
}
