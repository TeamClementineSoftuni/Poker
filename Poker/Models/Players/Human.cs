namespace Poker.Models.Players
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Human : Player
    {
        public Human(Point location) 
            : base(location)
        {
            this.Turn = true;
        }

        public Button CallButton { get; set; }
    }
}
