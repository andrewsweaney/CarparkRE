using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CarparkRE_Lib;
using Newtonsoft.Json;

namespace CarparkRE.Controllers
{
    public class RateEngineController : ApiController
    {
        [HttpPost]
        public string GetRate([FromBody] CPRateRQ oInput)
        {
            string sRate = GetCalculatedRate(oInput);
            
            return sRate;
        }

        [HttpPost]
        public string GetRate([FromUri] DateTime dtEntry, DateTime dtExit)
        {
            CPRateRQ oInput = new CPRateRQ() { EntryDT = dtEntry, ExitDT = dtExit };

            string sRate = GetCalculatedRate(oInput);

            return sRate;
        }



        private string GetCalculatedRate(CPRateRQ oInput)
        {
            CPRateRS oRate = new CPRateRS();

            try
            {
                var cpEngine = new CarparkRE_Lib.RateEngine();
                if (cpEngine.LoadRates() >= 0)
                {
                    oRate = cpEngine.CalculateTotal(oInput);
                }
                else
                {
                    oRate.ErrorHResult = -1;
                    oRate.ErrorMsg = "Failed to load rates.";
                }
            }
            catch (Exception e)
            {
                oRate = new CPRateRS();
                oRate.ErrorHResult = e.HResult;
                oRate.ErrorMsg = e.ToString();
            }

            return JsonConvert.SerializeObject(oRate);
        }
    }
}
