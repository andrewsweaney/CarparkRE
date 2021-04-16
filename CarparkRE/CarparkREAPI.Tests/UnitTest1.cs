using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//using CarparkRE.Controllers;
//using CarparkRE_Lib.Models;
using Newtonsoft.Json;

namespace CarparkREAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /*
        [TestMethod]
        public void APITestStandardRate()
        {
            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Standard Rate - 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(9).AddMinutes(47);      // Thursday 9:47 AM
            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 5);

            // Standard Rate - 1 Hour 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(10).AddMinutes(47);     // Thursday 10:47 AM
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 10);

            // Standard Rate - 2 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(11).AddMinutes(47);     // Thursday 11:47 AM
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 15);

            // Standard Rate - 3 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(12).AddMinutes(47);     // Thursday 12:47
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 20);

            // Standard Rate - 2 Days 7 Hours 47 Mins - Maximum Rate $20 per day
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddDays(2).AddHours(16).AddMinutes(47);     // Thursday 16:47 (4:47 PM)
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 60);
        }

        [TestMethod]
        public void APITestEarlyBird()
        {
            // Requirements:
            //         Name: Early Bird
            //         Type: Flat Rate
            //         Total Price: $13.00
            //         Entry Condition: Enter between 6:00 AM to 9:00 AM
            //         Exit Condition: Exit between 3:30 PM to 11:30 PM

            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();
            rq.EntryDT = dtTest.AddHours(6).AddMinutes(30);        // Thursday 06:30 AM
            rq.ExitDT = dtTest.AddHours(16).AddMinutes(30);        // Thursday 16:30 (4:30 PM)

            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq));
            Assert.IsTrue(rs.RateName == "Early Bird");
            Assert.IsTrue(rs.TotalPrice == 13);
        }

        [TestMethod]
        public void APITestNightRate()
        {

        }

        [TestMethod]
        public void APITestWeekendRate()
        {

        }

        [TestMethod]
        public void APITestCheapestRate()
        {

        }*/
    }
}
