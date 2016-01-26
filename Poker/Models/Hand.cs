namespace Poker.Models
{
    using System;
    using Interfaces;

    public class Hand : IHand
    {
        public ICard Card1 { get; set; }

        public ICard Card2 { get; set; }

        public int HandPower()
        {
            //TODO: implementation

            throw new NotImplementedException();
        }

        public void ShowHand()
        {
            //TODO: implementation

            throw new NotImplementedException();
        }
    }
}
