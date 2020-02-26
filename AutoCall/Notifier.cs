using Newtonsoft.Json;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCall
{
    class Notifier
    {
        public void Notify(FormattedAccount a)
        {
            List<KeyValuePair<string, string>> parameter = CreateHttpClient(a);
            HttpClient c = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("ACd94bd9b8e31ed6694de7dd40c54aecb9:59f7b2ddd269337fbb2aaed2b418b420");
            c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            Task<HttpResponseMessage> result = c.PostAsync("https://studio.twilio.com/v1/Flows/FW1d06862490392d787d2713759909ecd6/Executions",
                    new FormUrlEncodedContent(parameter));
            result.Wait();
            System.Timers.Timer timer = new System.Timers.Timer(1500);
            timer.Start();
        }
        public string AccountFormater(double input)
        {
            string output = input.ToString().Substring(0, 1) + " " + input.ToString().Substring(1, 1) + " " + input.ToString().Substring(2, 1) + " "
                + input.ToString().Substring(3, 1) + " " + input.ToString().Substring(4, 1) + " " + input.ToString().Substring(5, 1)
                + " " + input.ToString().Substring(6, 1);
            return output;
        }

        public string MoneyFormater(double amount)
        {
            string money = "$" + amount.ToString();
            return money;
        }

        public string DateFormater(DateTime date)
        {
            string newDate = date.ToShortDateString();
            return newDate;
        }

        public string PhoneFormater(double phone)
        {
            string newPhone = "+" + phone;
            return newPhone;
        }

        public List<KeyValuePair<string, string>> CreateHttpClient(FormattedAccount a)
        {
            List<KeyValuePair<string, string>> p = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> phoneNum = new KeyValuePair<string, string>("To", a.ToPhone);
            p.Add(phoneNum);
            KeyValuePair<string, string> accnVal = new KeyValuePair<string, string>("From", a.FromPhone);
            p.Add(accnVal);
            KeyValuePair<string, string> jsonParameters = new KeyValuePair<string, string>("Parameters", JsonConvert.SerializeObject(a));
            p.Add(jsonParameters);
            return p;
        }
    }
}
