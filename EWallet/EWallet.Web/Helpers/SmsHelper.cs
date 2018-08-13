using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWallet.Web.Helpers
{
    public interface ISmsHelper
    {
        bool SendSms(string phoneNumber, string content);
    }
    public class SmsHelper : ISmsHelper
    {
        private readonly ILogger _log;

        public SmsHelper(ILogger<SmsHelper> log)
        {
            _log = log;
        }
        public bool SendSms(string phoneNumber, string content)
        {
            bool result = true;
            try
            {
                //Send 
            }
            catch(Exception ex)
            {

            }
            return result;          
        }
    }
}
