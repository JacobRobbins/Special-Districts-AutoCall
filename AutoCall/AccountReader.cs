using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace AutoCall
{
    class AccountReader
    {
        string file;
        public AccountReader(string filePath)
        {
            file = filePath;
        }
        public List<Account> readAccounts()
        {
            List<Account> account = new List<Account>();
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        reader.Read();
                        while (reader.Read())
                        {
                            account.Add(readAccount(reader));
                        }
                    } while (reader.NextResult());
                }
            }
            return account;
        }
        private Account readAccount(IExcelDataReader r)
        {
            Account i = new Account()
            {
                accountNum = r.GetDouble(0),
                amount = r.GetDouble(1),
                disconnectDate = r.GetDateTime(2),
                phone = r.GetDouble(3)
            };
            return i;
        }
        public List<FormattedAccount> formatAccount(List<Account> a, Properties.Settings b)
        {
            Notifier formater = new Notifier();
            List<FormattedAccount> accns = new List<FormattedAccount>();
            foreach (Account x in a)
            {
                FormattedAccount formAcc = new FormattedAccount();
                formAcc.Account = formater.AccountFormater(x.accountNum);
                formAcc.Amount = formater.MoneyFormater(x.amount);
                formAcc.friendly_name = "San Bernardino";
                formAcc.Disconnect = formater.DateFormater(x.disconnectDate);
                formAcc.ToPhone = formater.PhoneFormater(x.phone);
                formAcc.FromPhone = b.fromPhone;
                accns.Add(formAcc);
            }
            return accns;
        }
    }
}
