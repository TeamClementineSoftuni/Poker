namespace Poker.Models
{
    using System;

    using Poker.Interfaces;

    public class Hand : IHand
    {
        public ICard Card1 { get; set; }

        public ICard Card2 { get; set; }
    }
}