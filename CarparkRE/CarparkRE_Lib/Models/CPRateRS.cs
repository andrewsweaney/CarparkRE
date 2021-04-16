using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class CPRateRS
    {
        public string RateName { get; set; }                // The rate being applied
        public decimal TotalPrice { get; set; }             // The total price for the customer to pay

        public int ErrorHResult { get; set; }               // if any exceptions are encountered this will have the HResult or -1 for custom errors
        public string ErrorMsg { get; set; }                // Holds the error message should any errors occur

        public CPRateRS()
        {
            RateName = "";
            ErrorMsg = "";
        }
    }
}
