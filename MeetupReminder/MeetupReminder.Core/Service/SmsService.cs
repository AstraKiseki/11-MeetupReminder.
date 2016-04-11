using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace MeetupReminder.Core.Service
{
    public class SmsService
    {
        private const string TwilioAccountSid = "";
        private const string TwilioAuthToken = "";
        private const string FromNumber = "+18582014064";

        public static void SendSms(string to, string message)
        {
            var twilio = new TwilioRestClient(TwilioAccountSid, TwilioAuthToken);

            twilio.SendMessage(FromNumber, to, message);
        }
    }
}
