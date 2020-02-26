using System;

namespace AutoCall
{
    public class Account
    {
        public double accountNum { get; set; }
        public double amount { get; set; }
        public DateTime disconnectDate { get; set; }
        public double phone { get; set; }

    }

    public class FormattedAccount
    {
        public string Account { get; set; }
        public string Amount { get; set; }
        public string friendly_name { get; set; }
        public string Disconnect { get; set; }
        public string ToPhone { get; set; }
        public string FromPhone { get; set; }
    }
}
