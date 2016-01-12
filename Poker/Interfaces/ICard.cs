using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Models;

namespace Poker.Interfaces
{
    public interface ICard
    {
        Suits Suit { get; }
        CardsRank Rank { get; }
        Image CardImage { get; }
    }
}
