using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class CPRateRS
    {
        public string RateName { get; set; }
        public decimal TotalPrice { get; set; }

        public int ErrorHResult { get; set; }
        public string ErrorMsg { get; set; }
    }
}
