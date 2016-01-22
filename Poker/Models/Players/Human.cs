using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models.Players
{
    public class Human : Player
    {
        public Human(Point location) 
            : base(location)
        {
            this.Turn = true;
        }
    }
}
