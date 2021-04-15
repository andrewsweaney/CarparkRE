using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public enum RateType
    {
        FlatRate = 0,
        HourlyRate
    }

    public class Rate
    {
        public RateType RateType { get; set; }
        public decimal Amount { get; set; }
    }
}
