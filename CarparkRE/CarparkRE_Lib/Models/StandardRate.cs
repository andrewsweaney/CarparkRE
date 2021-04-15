using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class StandardRate
    {
        public string Name { get; set; }
        public decimal MaxAmount { get; set; }
        public int MaxAmountHours { get; set; }

        public List<TimeLimit> TimeLimits { get; }

        public StandardRate()
        {
            TimeLimits = new List<TimeLimit>();
        }
    }
}
