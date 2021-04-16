using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarparkRE_Lib.Models;
using System.IO;
using Newtonsoft.Json;

namespace CarparkRE_Lib
{
    public class RateEngine
    {
        Rates _mRates = new Rates();

        public StandardRate GetStandardRates() { return _mRates.StandardRates; }
        public List<FlatRate> GetFlatRates() { return _mRates.FlatRates; }

        /// <summary>
        /// Loads the rate table, currently from a Json file.
        /// </summary>
        /// <returns></returns>
        public int LoadRates()
        {
            try
            {
                // Get the rates from Json file, normally these would be obtained from a database
                using (StreamReader r = new StreamReader("CarParkRates.json"))
                {
                    string json = r.ReadToEnd();
                    _mRates = JsonConvert.DeserializeObject<Rates>(json);
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Determines the Rate and Amount to charge the customer for Parking
        /// </summary>
        /// <param name="oRequest">Is an Object from the CarparkRE Library that contains the EntryDateTime and ExitDateTime of the customers parking session</param>
        /// <returns></returns>
        public CPRateRS CalculateParkingCharge(CPRateRQ oRequest)
        {
            CPRateRS oRet = new CPRateRS();

            try
            {
                // Make sure we have some rates loaded
                if (_mRates.RateCount() == 0)
                    return oRet;

                // Begin with the Standard Rate as the parking charge by default
                oRet.RateName = _mRates.StandardRates.Name;
                oRet.TotalPrice = GetStandardRateAmount(oRequest, GetStandardRates());

                // Find any flat rates that apply
                List<FlatRate> lstFlatRates = GetFlatRates(oRequest, GetFlatRates());

                // Select the cheapest rate for the customer, default is the Standard Rate
                foreach (var r in lstFlatRates)
                {
                    if (r.Amount < oRet.TotalPrice)
                    {
                        // Overwrite the default Standard Rate with the Flat Rate instead
                        oRet.RateName = r.Name;
                        oRet.TotalPrice = r.Amount;
                    }
                }
            }
            catch (Exception e)
            {
                // Record any errors in the return object
                oRet = new CPRateRS()
                {
                    ErrorHResult = e.HResult,
                    ErrorMsg = e.ToString()
                };
            }

            return oRet;
        }

        /// <summary>
        /// Figures out the Standard Rates to apply for the given carpark session
        /// </summary>
        /// <param name="oRequest">Is an Object from the CarparkRE Library that contains the EntryDateTime and ExitDateTime of the customers parking session</param>
        /// <param name="oStdRates">Standard Rates as loded from the LoadRates function</param>
        /// <returns></returns>
        private decimal GetStandardRateAmount(CPRateRQ oRequest, StandardRate oStdRates)
        {
            decimal dAmount = 0;

            try
            {
                // Determine the total hours parked ot match with our Standard Rates
                TimeSpan tsTimeParked = oRequest.ExitDT - oRequest.EntryDT;
                int nHoursParked = (int)Math.Ceiling(tsTimeParked.TotalSeconds / 3600.0);        // Round up to get total hours or part thereof
                TimeLimit oTimeSelected = null;

                // Check if we need to apply the max standard rate
                if (nHoursParked > oStdRates.MaxAmountHours)
                {
                    // Check if they get the max standard rate multiple times - E.g. been there for multiple calendar days
                    while (nHoursParked >= oStdRates.MaxAmountHours)
                    {
                        dAmount += oStdRates.MaxAmount;
                        nHoursParked -= oStdRates.MaxAmountHours;
                    }

                    // Make sure we add on the remainder from the Standard Rate. E.g. Charged max amount for 2 days and then for 2 hours on the last day
                    if (nHoursParked > 0)
                    {
                        oTimeSelected = oStdRates.TimeLimits.FirstOrDefault(a => nHoursParked > a.StartHours && nHoursParked <= a.EndHours);
                        dAmount += oTimeSelected != null ? oTimeSelected.Amount : 0;
                    }
                }
                else
                {
                    // Hours in the carpark are less than the maximum hours before we click over for a second flat rate, so find the appopriate one
                    oTimeSelected = oStdRates.TimeLimits.FirstOrDefault(a => nHoursParked > a.StartHours && nHoursParked <= a.EndHours);
                    dAmount = oTimeSelected != null ? oTimeSelected.Amount : 0;
                }
            }
            catch (Exception)
            {
                return dAmount;
            }

            return dAmount;
        }

        private List<FlatRate> GetFlatRates(CPRateRQ oRequest, List<FlatRate> oFlatRates)
        {
            List<FlatRate> lstRates = new List<FlatRate>();

            try
            {
                TimeSpan tsTimeParked = oRequest.ExitDT - oRequest.EntryDT;
                int nMinutesParked = (int)Math.Ceiling(tsTimeParked.TotalSeconds / 60.0);        // Round up to get total minutes or part thereof

                foreach (var r in oFlatRates)
                {
                    // Check if there exists Conditions for the Entry Day of the week
                    var conditions = r.Conditions.Where(a => a.DayOfTheWeek == oRequest.EntryDT.DayOfWeek).ToList();
                    foreach (var c in conditions)
                    {
                        // Go further and figure out if we entered during the entry period and exited during the exit period for the Condition

                        // Determine the Date range for the Entry
                        string strEntryStart = System.String.Format("{0:yyyy-MM-dd} {1}", oRequest.EntryDT, c.EntryStartTime);
                        DateTime dtEntryStart = Convert.ToDateTime(strEntryStart);

                        string strEntryEnd = System.String.Format("{0:yyyy-MM-dd} {1}", dtEntryStart.AddDays(c.EntryEndAddDays), c.EntryEndTime);
                        DateTime dtEntryEnd = Convert.ToDateTime(strEntryEnd);

                        if (dtEntryEnd <= dtEntryStart)
                            dtEntryEnd = dtEntryEnd.AddDays(1);

                        // Determine the date range for the Exit
                        string strExitStart = System.String.Format("{0:yyyy-MM-dd} {1}", dtEntryStart.AddDays(c.DaysBetweenEntryExit), c.ExitStartTime);
                        DateTime dtExitStart = Convert.ToDateTime(strExitStart);

                        string strExitEnd = System.String.Format("{0:yyyy-MM-dd} {1}", dtExitStart.AddDays(c.ExitEndAddDays), c.ExitEndTime);
                        DateTime dtExitEnd = Convert.ToDateTime(strExitEnd);

                        if (dtExitEnd <= dtExitStart)
                            dtExitEnd = dtExitEnd.AddDays(1);

                        // Put in an extra check to make sure we don't go over the time period - E.g. this would prevent them getting the weekend rate if they stayed for a week
                        int nMaxMinutes = (int)Math.Ceiling((dtExitEnd - dtEntryStart).TotalSeconds / 60.0);

                        if (oRequest.EntryDT >= dtEntryStart && oRequest.EntryDT <= dtEntryEnd && oRequest.ExitDT >= dtExitStart && oRequest.ExitDT <= dtExitEnd && nMinutesParked <= nMaxMinutes)
                        {
                            lstRates.Add(r);
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<FlatRate>();
            }

            return lstRates;
        }
    }
}
