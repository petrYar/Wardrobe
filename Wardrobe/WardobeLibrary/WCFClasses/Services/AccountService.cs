using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AccountService : IAccountService
    {
        public List<Account> Accounts = new List<Account>();

        public void Disconnect(string token)
        {
            Accounts.FirstOrDefault(t => t.Token == token).Token = null;
        }

        public Account GetInfo(string token)
        {
            var loginedUser = Accounts.FirstOrDefault(t => t.Token == token);
            return loginedUser;
        }

        public void Register(Account account, string password)
        {
            //account.IdGuid = Guid.NewGuid().ToString();
            account.Password = password;
            this.Accounts.Add(account);
        }

        public string Login(string login, string password)
        {
            var loginedUser = this.Accounts.FirstOrDefault(t => t.UserName == login && t.Password == password);
            if (loginedUser != null)
            {
                string token = Guid.NewGuid().ToString();
                this.Accounts.FirstOrDefault(t => t == loginedUser).Token = token;
                return token;
            }
            return null;
        }
    }
}
