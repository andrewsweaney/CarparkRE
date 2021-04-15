using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarparkRE_Lib.Models;

namespace CarparkRE_Lib
{
    public class CPRateRS
    {
        public string RateName { get; set; }
        public Rate RateType { get; set; }
        public DateTime EntryDT { get; set; }
        public DateTime ExitDT { get; set; }
        public decimal TotalPrice { get; set; }

        public int ErrorHResult { get; set; }
        public string ErrorMsg { get; set; }
    }
}
