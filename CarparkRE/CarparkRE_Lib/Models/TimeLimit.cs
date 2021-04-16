using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class TimeLimit
    {
        public int StartHours { get; set; }             // Starting Hour the amount applies to (>=)
        public int EndHours { get; set; }               // Ending hours the Amount applies to (<)
        public decimal Amount { get; set; }             // Amount to charge the customer
    }
}
