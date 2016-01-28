namespace Poker.Models
{
    using Poker.Interfaces;

    public class GameStatus : IGameStatus
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