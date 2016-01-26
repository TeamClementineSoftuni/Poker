using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Constants
{
    public static class Locations
    {
        private const int HumanPlayerLocationWidth = 569;
        private const int HumanPlayerLocationHeight = 461;
        private const int Bot1LocationWidth = -5;
        private const int Bot1LocationHeight = 409;
        private const int Bot2LocationWidth = 73;
        private const int Bot2LocationHeight = 59;
        private const int Bot3LocationWidth = 569;
        private const int Bot3LocationHeight = 12;
        private const int Bot4LocationWidth = 1113;
        private const int Bot4LocationHeight = 59;
        private const int Bot5LocationWidth = 1158;
        private const int Bot5LocationHeight = 397;
        private const int BoardLocationWidth = 410;
        private const int BoardLocationHeigt = 265;
        private const int cardWidth = 90;

        private static readonly List<Point> playersLocations = new List<Point>()
        {
            new Point(HumanPlayerLocationWidth, HumanPlayerLocationHeight),
            new Point(Bot1LocationWidth , Bot1LocationHeight),
            new Point(Bot2LocationWidth, Bot2LocationHeight),
            new Point(Bot3LocationWidth, Bot3LocationHeight),
            new Point(Bot4LocationWidth, Bot4LocationHeight),
            new Point(Bot5LocationWidth, Bot5LocationHeight)

        };

        public static IList<Point> PlayersLocations
        {
            get { return playersLocations; }
        }

        public static Point BoardCardsLocations(int cardIndex)
        {
            Point cardLocation = new Point(BoardLocationWidth + cardIndex * cardWidth, BoardLocationHeigt);
            return cardLocation;
        }

    }
}
