using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CarparkRE_Lib.Models;

namespace CarparkRE_Lib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRateLoad()
        {
            // Requirements:
            //         The rates as provided are loaded ionto the software

            var engine = new CarparkRE_Lib.RateEngine();

            /*
            Rates r = new Rates();
            r.StandardRates.Name = "Standard Rate";
            r.StandardRates.MaxAmount = 20;
            r.StandardRates.MaxAmountHours = 24;
            r.StandardRates.TimeLimits.Add(new TimeLimit() { StartHours = 0, EndHours = 1, Amount = 5 });
            r.StandardRates.TimeLimits.Add(new TimeLimit() { StartHours = 1, EndHours = 2, Amount = 10 });
            r.StandardRates.TimeLimits.Add(new TimeLimit() { StartHours = 2, EndHours = 3, Amount = 15 });
            r.StandardRates.TimeLimits.Add(new TimeLimit() { StartHours = 3, EndHours = 24, Amount = 20 });

            FlatRate fr = new FlatRate() { Name = "Early Bird", Amount = 13 };
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Sunday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Monday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Tuesday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Wednesday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Thursday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Friday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Saturday, EntryStartTime = "06:00", EntryEndTime = "09:00", EntryEndAddDays = 0, ExitStartTime = "15:30", ExitEndTime = "23:30", ExitEndAddDays = 0, DaysBetweenEntryExit = 0 });
            r.FlatRates.Add(fr);

            fr = new FlatRate() { Name = "Night Rate", Amount = 6.5m };
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Monday, EntryStartTime = "18:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "08:00", ExitEndAddDays = 0, DaysBetweenEntryExit = 1 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Tuesday, EntryStartTime = "18:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "08:00", ExitEndAddDays = 0, DaysBetweenEntryExit = 1 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Wednesday, EntryStartTime = "18:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "08:00", ExitEndAddDays = 0, DaysBetweenEntryExit = 1 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Thursday, EntryStartTime = "18:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "08:00", ExitEndAddDays = 0, DaysBetweenEntryExit = 1 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Friday, EntryStartTime = "18:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "08:00", ExitEndAddDays = 0, DaysBetweenEntryExit = 1 });
            r.FlatRates.Add(fr);

            fr = new FlatRate() { Name = "Weekend Rate", Amount = 10 };
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Saturday, EntryStartTime = "00:00", EntryEndTime = "00:00", EntryEndAddDays = 2, ExitStartTime = "00:00", ExitEndTime = "00:00", ExitEndAddDays = 2, DaysBetweenEntryExit = 0 });
            fr.Conditions.Add(new Condition() { DayOfTheWeek = DayOfWeek.Sunday, EntryStartTime = "00:00", EntryEndTime = "00:00", EntryEndAddDays = 1, ExitStartTime = "00:00", ExitEndTime = "00:00", ExitEndAddDays = 1, DaysBetweenEntryExit = 0 });
            r.FlatRates.Add(fr);

            string json = JsonConvert.SerializeObject(r);
            */


            // Test loading the rates to ensure its getting some
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);
            Assert.IsTrue(engine.GetFlatRates().Count > 0);
            Assert.IsTrue(engine.GetStandardRates() != null);
            Assert.IsTrue(engine.GetStandardRates().TimeLimits.Count > 0);
        }

        [TestMethod]
        public void TestStandardRates()
        {
            // Requirement:
            //         0 - 1 Hours : $5
            //         1 - 2 Hours : $10
            //         2 - 3 Hours : $15
            //         3+ Hours : $20      flat rate for each calendar day of parking

            var engine = new CarparkRE_Lib.RateEngine();
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Standard Rate - 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(9).AddMinutes(47);      // Thursday 9:47 AM
            var rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 5);

            // Standard Rate - 1 Hour 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(10).AddMinutes(47);     // Thursday 10:47 AM
            rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 10);

            // Standard Rate - 2 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(11).AddMinutes(47);     // Thursday 11:47 AM
            rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 15);

            // Standard Rate - 3 Hours 47 Mins
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(12).AddMinutes(47);     // Thursday 12:47
            rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 20);

            // Standard Rate - 2 Days 7 Hours 47 Mins - Maximum Rate $20 per day
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddDays(2).AddHours(16).AddMinutes(47);     // Saturday 16:47 (4:47 PM)
            rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Standard Rate");
            Assert.IsTrue(rs.TotalPrice == 60);
        }

        [TestMethod]
        public void TestEarlyBirdRate()
        {
            // Requirements:
            //         Name: Early Bird
            //         Type: Flat Rate
            //         Total Price: $13.00
            //         Entry Condition: Enter between 6:00 AM to 9:00 AM
            //         Exit Condition: Exit between 3:30 PM to 11:30 PM

            var engine = new CarparkRE_Lib.RateEngine();
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();
            rq.EntryDT = dtTest.AddHours(6).AddMinutes(30);        // Thursday 06:30 AM
            rq.ExitDT = dtTest.AddHours(16).AddMinutes(30);        // Thursday 16:30 (4:30 PM)

            var rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Early Bird");
            Assert.IsTrue(rs.TotalPrice == 13);
        }


        [TestMethod]
        public void TestNightRate()
        {
            // Requirements:
            //         Name: Night Rate
            //         Type: Flat Rate
            //         Total Price: $6.50
            //         Entry Condition: Enter between 6:00 PM to Midnight
            //         Exit Condition: Exit before 8 AM
            //                          Assumption: Exiting before 8 AM is intended to be 8 AM the next day

            var engine = new CarparkRE_Lib.RateEngine();
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Night Rate - Normal weekday test
            rq.EntryDT = dtTest.AddHours(18).AddMinutes(30);         // Thursday 18:30 (6:30PM)
            rq.ExitDT = dtTest.AddDays(1).AddHours(7);               // Friday 07:00 AM
            var rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Night Rate");
            Assert.IsTrue(rs.TotalPrice == 6.5m);

            // Night Rate - Friday night test
            rq.EntryDT = dtTest.AddDays(1).AddHours(18).AddMinutes(30);     // Friday 18:30 (6:30PM)
            rq.ExitDT = dtTest.AddDays(2).AddHours(7);                      // Saturday 07:00 AM
            rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Night Rate");
            Assert.IsTrue(rs.TotalPrice == 6.5m);
        }

        [TestMethod]
        public void TestWeekendRate()
        {
            // Requirements:
            //         Name: Weekend Rate
            //         Type: Flat Rate
            //         Total Price: $10.00
            //         Entry Condition: Enter anytime past midnight on Friday
            //         Exit Condition: Exit any time before midnight on Sunday

            var engine = new CarparkRE_Lib.RateEngine();
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);

            DateTime dtTest = new DateTime(2021, 4, 17);
            CPRateRQ rq = new CPRateRQ();

            // Weekend Rate
            rq.EntryDT = dtTest.AddMinutes(12);                             // Saturday 00:12 AM
            rq.ExitDT = dtTest.AddDays(1).AddHours(12).AddMinutes(47);      // Sunday 12:47 PM
            var rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Weekend Rate");
            Assert.IsTrue(rs.TotalPrice == 10);
        }

        [TestMethod]
        public void TestCheapestRate()
        {
            // Requirements:
            //         The customer should get the cheapest deal based on the rules which apply to the time period

            var engine = new CarparkRE_Lib.RateEngine();
            var ret = engine.LoadRates("CarParkRates.json");
            Assert.IsTrue(ret == 0);

            DateTime dtTest = new DateTime(2021, 4, 15);
            CPRateRQ rq = new CPRateRQ();

            // Crossover Case: Qualifies for Standard Rate - 7 Hours 47 Mins ($20) or Early Bird Rate ($13)
            rq.EntryDT = dtTest.AddHours(9);                    // Thursday 9:00 AM
            rq.ExitDT = dtTest.AddHours(16).AddMinutes(47);     // Thursday 16:47 (4:47 PM)
            var rs = engine.CalculateParkingCharge(rq);
            Assert.IsTrue(rs.RateName == "Early Bird");
            Assert.IsTrue(rs.TotalPrice == 13);
        }
    }
}
