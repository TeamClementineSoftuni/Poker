using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.Models;

    [TestClass]
    public class ChipsSetTest
    {
        [TestMethod]
        public void TestChips_SetToNegativeNumber_ShouldReturn0()
        {
           IPlayer player = new Bot(Locations.PlayersLocations[0]);
            player.ChipsSet.Amount = -10;

            Assert.AreEqual(0, player.ChipsSet.Amount, "Shouldn't set the negative amount of chips to 0");
        }
    }
}
