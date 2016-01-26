namespace Poker.Models.Players
{
    using System.Drawing;

    public class Human : Player
    {
        public Human(Point location) 
            : base(location)
        {
            this.Turn = true;
        }
    }
}
