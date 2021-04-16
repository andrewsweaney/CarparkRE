using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class CPRateRQ
    {
        public DateTime EntryDT { get; set; }               // DateTime the customer entered the carpark
        public DateTime ExitDT { get; set; }                // DateTime the customer exited tyhe carpark
    }
}
