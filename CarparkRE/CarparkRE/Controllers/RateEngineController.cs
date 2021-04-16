using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CarparkRE_Lib.Models;
using Newtonsoft.Json;

namespace CarparkRE.Controllers
{
    public class RateEngineController : ApiController
    {
        /// <summary>
        /// Returns the rate to charge the customer as a Json string. Uses the Carpark library.
        /// </summary>
        /// <param name="oInput">Takes an object defined in the CarparkRE Library from the body of the post message</param>
        /// <returns></returns>
        [HttpPost]
        public string GetRate([FromBody] CPRateRQ oInput)
        {
            string sRate = GetCalculatedRate(oInput);
            
            return sRate;
        }

        /// <summary>
        /// Returns the rate to charge the customer as a Json string. Uses the Carpark library.
        /// </summary>
        /// <param name="dtEntry">Customer Entry DateTime to the carpark</param>
        /// <param name="dtExit">Customer Exit DateTime from the carpark</param>
        /// <returns></returns>
        [HttpPost]
        public string GetRate([FromUri] DateTime dtEntry, DateTime dtExit)
        {
            CPRateRQ oInput = new CPRateRQ() { EntryDT = dtEntry, ExitDT = dtExit };

            string sRate = GetCalculatedRate(oInput);

            return sRate;
        }


        /// <summary>
        /// Starts up the Carpark Rate Engine Library, loads the rates and gets the calculated rate to charge the customer for the given carpark session
        /// </summary>
        /// <param name="oInput"></param>
        /// <returns></returns>
        private string GetCalculatedRate(CPRateRQ oInput)
        {
            // Create a return object
            CPRateRS oRate = new CPRateRS();

            try
            {
                // Create and instance of the Rate Engine and load the Rate table
                var cpEngine = new CarparkRE_Lib.RateEngine();
                if (cpEngine.LoadRates() >= 0)
                {
                    // Get the Rate and Total Amount to charge the customer
                    oRate = cpEngine.CalculateParkingCharge(oInput);
                }
                else
                {
                    // Add a custom message if the rates failed to load
                    oRate.ErrorHResult = -1;
                    oRate.ErrorMsg = "Failed to load rates.";
                }
            }
            catch (Exception e)
            {
                // Record any errors in the return object
                oRate = new CPRateRS()
                {
                    ErrorHResult = e.HResult,
                    ErrorMsg = e.ToString()
                };
            }

            // Return as a Json string
            return JsonConvert.SerializeObject(oRate);
        }
    }
}
