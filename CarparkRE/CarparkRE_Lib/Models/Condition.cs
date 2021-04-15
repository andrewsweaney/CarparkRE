using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    public class Condition
    {
        public DayOfWeek DayOfTheWeek { get; set; }

        public string EntryStartTime { get; set; }
        public string EntryEndTime { get; set; }


        public string ExitStartTime { get; set; }
        public string ExitEndTime { get; set; }

        public int MaxMinutes { get; set; }
    }
}
