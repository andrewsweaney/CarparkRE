using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class Rates
    {
        public StandardRate StandardRates { get; set; }
        public List<FlatRate> FlatRates { get; set; }

        public Rates()
        {
            StandardRates = new StandardRate();
            FlatRates = new List<FlatRate>();
        }

        public int RateCount()
        {
            if (StandardRates.TimeLimits.Count > 0)
                return FlatRates.Count + 1;

            return FlatRates.Count;
        }
    }
}
