using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkRE_Lib.Models
{
    /// <summary>
    /// This model encapsulates the Entry and Exit Conditions for each of the Flate Rates
    /// </summary>
    public class Condition
    {
        public DayOfWeek DayOfTheWeek { get; set; }         // Which day of the week the entry applies to

        public string EntryStartTime { get; set; }          // Condition is valid for entering the carpark after this time
        public string EntryEndTime { get; set; }            // Condition is valid for entering the carpark before this time
        public int EntryEndAddDays { get; set; }            // Days offset between the EntryStart and EntryEnd


        public string ExitStartTime { get; set; }           // Condition is valid for exiting the carpark after this time
        public string ExitEndTime { get; set; }             // Condition is valid for exiting the carpark before this time
        public int ExitEndAddDays { get; set; }             // Days offset between the ExitStart and ExitEnd

        public int DaysBetweenEntryExit { get; set; }       // Days offset between the EntryStart and ExitStart
    }
}
