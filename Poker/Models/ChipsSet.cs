using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public class ChipsSet
    {
        private const int defaultChipsSetAmount = 10000;
        private int amount;

        public ChipsSet(int amount = defaultChipsSetAmount)
        {
            this.amount = amount;
        }

        public int Amount
        {
            get { return this.amount; }
            set
            {
                this.amount = value < 0 ? 0 : value;
            }
        }

        public override string ToString()
        {
            return string.Format("Chips : {0}", Amount);
        }
    }
}
