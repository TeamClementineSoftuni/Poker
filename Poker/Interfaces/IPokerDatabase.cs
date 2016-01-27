using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IPokerDatabase
    {
        IPlayer[] Players { get; }

        void AddPlayer(IPlayer player);
    }
}
