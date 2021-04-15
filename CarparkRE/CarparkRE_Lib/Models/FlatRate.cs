using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class FlatRate
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public List<Condition> Conditions { get; }

        public FlatRate()
        {
            Conditions = new List<Condition>();
        }
    }
}
