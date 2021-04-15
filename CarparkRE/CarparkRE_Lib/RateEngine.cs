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

        public int LoadRates()
        {
            // Get the rates from Json file, normally these would be obtained from a database
            using (StreamReader r = new StreamReader("CarParkRates.json"))
            {
                string json = r.ReadToEnd();
                _mRates = JsonConvert.DeserializeObject<Rates>(json);
            }

            return 0;
        }

        public CPRateRS CalculateParkingCharge(CPRateRQ oRequest)
        {
            CPRateRS oRet = new CPRateRS();

            try
            {
                if (_mRates.RateCount() == 0)
                    return oRet;

                // Begin with the Standard Rate as the parking charge by default
                oRet.RateName = _mRates.StandardRates.Name;
                oRet.TotalPrice = GetStandardRateAmount(oRequest, _mRates.StandardRates);

                // Find any flat rates that apply
                List<FlatRate> lstFlatRates = GetFlatRates(oRequest, _mRates.FlatRates);

                // Select the cheapest rate for the customer, default is the Standard Rate
                foreach (var r in lstFlatRates)
                {
                    if (r.Amount < oRet.TotalPrice)
                    {
                        // Use the Flat Rate instead
                        oRet.RateName = r.Name;
                        oRet.TotalPrice = r.Amount;
                    }
                }
            }
            catch (Exception e)
            {
                oRet = new CPRateRS()
                {
                    ErrorHResult = e.HResult,
                    ErrorMsg = e.ToString()
                };
            }

            return oRet;
        }

        private decimal GetStandardRateAmount(CPRateRQ oRequest, StandardRate oStdRates)
        {
            decimal dAmount = 0;

            try
            {
                TimeSpan tsTimeParked = oRequest.ExitDT - oRequest.EntryDT;
                int nHoursParked = (int)Math.Ceiling(tsTimeParked.TotalSeconds / 3600.0);        // Round up to get total hours or part thereof
                TimeLimit oTimeSelected = null;

                if (nHoursParked > oStdRates.MaxAmountHours)
                {
                    while (nHoursParked >= oStdRates.MaxAmountHours)
                    {
                        dAmount += oStdRates.MaxAmount;
                        nHoursParked -= oStdRates.MaxAmountHours;
                    }

                    if (nHoursParked > 0)
                    {
                        oTimeSelected = oStdRates.TimeLimits.FirstOrDefault(a => nHoursParked >= a.StartHours && nHoursParked < a.EndHours);
                        dAmount += oTimeSelected != null ? oTimeSelected.Amount : 0;
                    }
                }
                else
                {
                    oTimeSelected = oStdRates.TimeLimits.FirstOrDefault(a => nHoursParked >= a.StartHours && nHoursParked < a.EndHours);
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

                foreach (var r in oFlatRates)
                {
                    // Check if there exists Conditions for the Entry Day of the week
                    var conditions = r.Conditions.Where(a => a.DayOfTheWeek == oRequest.EntryDT.DayOfWeek).ToList();
                    foreach (var c in conditions)
                    {
                        // Go further and figure out if we entered during the entry period and exited during the exit period for the Condition
                        string strEntryStart = System.String.Format("{0:yyyy-MM-dd} {1}", oRequest.EntryDT, c.EntryStartTime);
                        string strEntryEnd = System.String.Format("{0:yyyy-MM-dd} {1}", oRequest.EntryDT, c.EntryEndTime);
                        DateTime dtEntryStart = Convert.ToDateTime(strEntryStart);
                        DateTime dtEntryEnd = Convert.ToDateTime(strEntryEnd);

                        string strExitStart = System.String.Format("{0:yyyy-MM-dd} {1}", oRequest.ExitDT, c.ExitStartTime);
                        string strExitEnd = System.String.Format("{0:yyyy-MM-dd} {1}", oRequest.ExitDT, c.ExitEndTime);
                        DateTime dtExitStart = Convert.ToDateTime(strExitStart);
                        DateTime dtExitEnd = Convert.ToDateTime(strExitEnd);

                        int nMinutesParked = (int)Math.Ceiling(tsTimeParked.TotalSeconds / 60.0);        // Round up to get total minutes or part thereof

                        if (oRequest.EntryDT >= dtEntryStart && oRequest.EntryDT <= dtEntryEnd && oRequest.ExitDT >= dtExitStart && oRequest.ExitDT <= dtExitEnd && nMinutesParked <= c.MaxMinutes)
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
