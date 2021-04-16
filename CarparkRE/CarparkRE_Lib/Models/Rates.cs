using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    /// <summary>
    /// Holds the rate table for the carpark 
    /// </summary>
    public class Rates
    {
        public StandardRate StandardRates { get; set; }         // Standard rates with a list of Limits that states when different charges should apply
        public List<FlatRate> FlatRates { get; set; }           // List of the Flat Rates. E.g. Early Bird, Night Rate, Weekend Rate

        public Rates()
        {
            StandardRates = new StandardRate();
            FlatRates = new List<FlatRate>();
        }

        /// <summary>
        /// Helper function used as a test to see if this Rate object contains anything
        /// </summary>
        /// <returns></returns>
        public int RateCount()
        {
            if (StandardRates.TimeLimits.Count > 0)
                return FlatRates.Count + 1;

            return FlatRates.Count;
        }
    }
}
