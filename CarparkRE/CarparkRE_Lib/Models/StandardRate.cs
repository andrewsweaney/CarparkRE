using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class StandardRate
    {
        public string Name { get; set; }                // Name to be used for a standard rate
        public decimal MaxAmount { get; set; }          // Maximum amount the customer can be charged on a standard rate per AmountHours block
        public int MaxAmountHours { get; set; }         // Maximum hours before the Standard rate clicks over again. E.g. flat rate for each calendar day of parking

        public List<TimeLimit> TimeLimits { get; }      // The graduations and prices for the price increases for the longer the customer stays in the carpark

        public StandardRate()
        {
            Name = "";
            TimeLimits = new List<TimeLimit>();
        }
    }
}
