using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class TimeLimit
    {
        public int StartHours { get; set; }
        public int EndHours { get; set; }
        public decimal Amount { get; set; }
    }
}
