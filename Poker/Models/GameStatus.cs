using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public class GameStatus
    {
        public GameStatus(Actions action, int chipsAddedToPot, int amountRaisedTo)
        {
            this.Action = action;
            this.ChipsAddedToPot = chipsAddedToPot;
            this.AmountRaisedTo = amountRaisedTo;
        }

        public Actions Action { get; set; }
        public int ChipsAddedToPot { get; set; }
        public int AmountRaisedTo { get; set; }
    }
}
