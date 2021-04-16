using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using CarparkRE.Controllers;
using CarparkRE_Lib.Models;
using Newtonsoft.Json;

namespace CarparkREAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void APITestStandardRate()
        {
            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Standard Rate - 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(9).AddMinutes(47);      // Thursday 9:47 AM
            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 5);

            // Standard Rate - 1 Hour 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(10).AddMinutes(47);     // Thursday 10:47 AM
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 10);

            // Standard Rate - 2 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(11).AddMinutes(47);     // Thursday 11:47 AM
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 15);

            // Standard Rate - 3 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(12).AddMinutes(47);     // Thursday 12:47
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 20);

            // Standard Rate - 2 Days 7 Hours 47 Mins - Maximum Rate $20 per day
            rq.EntryDT = dtTest.AddHours(9);                                // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddDays(2).AddHours(16).AddMinutes(47);     // Saturday 16:47 (4:47 PM)
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
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

            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Early Bird");
            Assert.IsTrue(rs.TotalPrice == 13);
        }

        [TestMethod]
        public void APITestNightRate()
        {
            // Requirements:
            //         Name: Night Rate
            //         Type: Flat Rate
            //         Total Price: $6.50
            //         Entry Condition: Enter between 6:00 PM to Midnight
            //         Exit Condition: Exit before 8 AM
            //                          Assumption: Exiting before 8 AM is intended to be 8 AM the next day

            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Night Rate - Normal weekday test
            rq.EntryDT = dtTest.AddHours(18).AddMinutes(30);         // Thursday 18:30 (6:30PM)
            rq.ExitDT = dtTest.AddDays(1).AddHours(7);               // Friday 07:00 AM
            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Night Rate");
            Assert.IsTrue(rs.TotalPrice == 6.5m);

            // Night Rate - Friday night test
            rq.EntryDT = dtTest.AddDays(1).AddHours(18).AddMinutes(30);     // Friday 18:30 (6:30PM)
            rq.ExitDT = dtTest.AddDays(2).AddHours(7);                      // Saturday 07:00 AM
            rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Night Rate");
            Assert.IsTrue(rs.TotalPrice == 6.5m);
        }

        [TestMethod]
        public void APITestWeekendRate()
        {
            // Requirements:
            //         Name: Weekend Rate
            //         Type: Flat Rate
            //         Total Price: $10.00
            //         Entry Condition: Enter anytime past midnight on Friday
            //         Exit Condition: Exit any time before midnight on Sunday

            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 17);
            CPRateRQ rq = new CPRateRQ();

            // Weekend Rate
            rq.EntryDT = dtTest.AddMinutes(12);                             // Saturday 00:12 AM
            rq.ExitDT = dtTest.AddDays(1).AddHours(12).AddMinutes(47);      // Sunday 12:47 PM
            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Weekend Rate");
            Assert.IsTrue(rs.TotalPrice == 10);
        }

        [TestMethod]
        public void APITestCheapestRate()
        {
            // Requirements:
            //         The customer should get the cheapest deal based on the rules which apply to the time period

            var engine = new RateEngineController();

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Crossover Case: Qualifies for Standard Rate - 7 Hours 47 Mins ($20) or Early Bird Rate ($13)
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(16).AddMinutes(47);     // Thursday 16:47 (4:47 PM)
            var rs = JsonConvert.DeserializeObject<CPRateRS>(engine.GetRate(rq.EntryDT, rq.ExitDT, GetRates()));
            Assert.IsTrue(rs.RateName == "Early Bird");
            Assert.IsTrue(rs.TotalPrice == 13);
        }


        // Load the Test data used...I.e. the RateCard
        private Rates GetRates()
        {
            Rates oRates = new Rates();

            try
            {
                // Get the rates from Json file
                using (StreamReader r = new StreamReader("CarParkRates.json"))
                {
                    string json = r.ReadToEnd();
                    oRates = JsonConvert.DeserializeObject<Rates>(json);
                }
            }
            catch (Exception)
            {
                return oRates;
            }

            return oRates;
        }
    }
}
