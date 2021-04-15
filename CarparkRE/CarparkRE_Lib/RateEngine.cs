using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarparkRE_Lib.Models;

namespace CarparkRE_Lib
{
    public class RateEngine
    {
        List<Rate> _mlstRates = new List<Rate>();

        public int LoadRates()
        {
            // Get the rates from the web.config, normally these would be obtained from a database


            return 0;
        }

        public CPRateRS CalculateTotal(CPRateRQ oRequest)
        {
            CPRateRS oRet = new CPRateRS();

            try
            {

                
            }
            catch (Exception e)
            {
                oRet = new CPRateRS()
                {
                    ErrorHResult = e.HResult,
                    ErrorMsg = e.ToString()
                };
            }

            return oRet;
        }

        private Rate GetCurrentRate(CPRateRQ oRequest)
        {
            Rate r = new Rate();

            try
            {
                if (_mlstRates.Count == 0)
                    return r;

            }
            catch (Exception)
            {
                return new Rate();
            }

            return r;
        }
    }
}
