using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class FlatRate
    {
        public string Name { get; set; }                // Name of the rate
        public decimal Amount { get; set; }             // Flat rate charge

        public List<Condition> Conditions { get; }      // A list of conditions that need to be met for the rate to come into effect

        public FlatRate()
        {
            Name = "";
            Conditions = new List<Condition>();
        }
    }
}
