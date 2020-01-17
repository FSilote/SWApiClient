using System;
using Kneat.SW.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kneat.SW.Domain.Tests
{
    [TestClass]
    public class StarshipTests
    {
        [TestMethod]
        public void GetRangeDaysFromConsumablesByDays_ShouldBe10()
        {
            var starship = new Starship { Consumables = "10 days" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(10, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeDaysFromConsumablesByWeek_ShouldBe14()
        {
            var starship = new Starship { Consumables = "2 weeks" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(14, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeDaysFromConsumablesByMonth_ShouldBe90()
        {
            var starship = new Starship { Consumables = "3 months" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(90, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeDaysFromConsumablesByYear_ShouldBe90()
        {
            var starship = new Starship { Consumables = "3 years" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(1095, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeDaysFromConsumablesNotCovered_ShouldBeZero()
        {
            var starship = new Starship { Consumables = "n/a" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(0, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeDaysFromConsumablesWithoutValue_ShouldBeZero()
        {
            var starship = new Starship { Consumables = "years" };
            var consumablesRangeInDays = starship.GetAutonomyInDaysFromConsumables();
            Assert.AreEqual(0, consumablesRangeInDays);
        }

        [TestMethod]
        public void GetRangeHoursFromConsumablesByDays_ShouldBe240()
        {
            var starship = new Starship { Consumables = "10 days" };
            var consumablesRangeInHours = starship.GetAutonomyInHoursFromConsumables();
            Assert.AreEqual(240, consumablesRangeInHours);
        }

        [TestMethod]
        public void GetRangeHoursFromConsumablesNotCovered_ShouldBe0()
        {
            var starship = new Starship { Consumables = "n.a" };
            var consumablesRangeInHours = starship.GetAutonomyInHoursFromConsumables();
            Assert.AreEqual(0, consumablesRangeInHours);
        }

        [TestMethod]
        public void GetMgltCleanValue_ShouldBe80()
        {
            var starship = new Starship { Mglt = "80" };
            var mglt = starship.GetMgltCleanValue();
            Assert.AreEqual(80, mglt);
        }

        [TestMethod]
        public void GetMgltCleanValueWithoutValue_ShouldBe0()
        {
            var starship = new Starship { Mglt = "n/a" };
            var mglt = starship.GetMgltCleanValue();
            Assert.AreEqual(0, mglt);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyYWing_ShouldBe74()
        {
            var starship = new Starship { Mglt = "80", Consumables = "1 week" };
            var stops = starship.GetStopsNeededToResupply(1000000);
            Assert.AreEqual(74, stops);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyMillenniumFalcon_ShouldBe9()
        {
            var starship = new Starship { Mglt = "75", Consumables = "2 months" };
            var stops = starship.GetStopsNeededToResupply(1000000);
            Assert.AreEqual(9, stops);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyDistanceZero_ShouldBeZero()
        {
            var starship = new Starship { Mglt = "75", Consumables = "2 months" };
            var stops = starship.GetStopsNeededToResupply(0);
            Assert.AreEqual(0, stops);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyDistanceNegative_ShouldBeMinus1()
        {
            var starship = new Starship { Mglt = "75", Consumables = "2 months" };
            var stops = starship.GetStopsNeededToResupply(-10);
            Assert.AreEqual(-1, stops);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyWithoutMgltValue_ShouldBeMinus1()
        {
            var starship = new Starship { Mglt = "n.a", Consumables = "2 months" };
            var stops = starship.GetStopsNeededToResupply(1000000);
            Assert.AreEqual(-1, stops);
        }

        [TestMethod]
        public void GetStopsNeededToResupplyWithoutConsumablesValue_ShouldBeMinus1()
        {
            var starship = new Starship { Mglt = "75", Consumables = "months" };
            var stops = starship.GetStopsNeededToResupply(1000000);
            Assert.AreEqual(-1, stops);
        }
    }
}
