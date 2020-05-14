using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.Models;

namespace WardobeLibrary.WCFClasses
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AccountService : IAccountService
    {

        public void Disconnect(string token)
        {
            using (EFContext context = new EFContext())
            {
                context.Accounts.FirstOrDefault(x => x.Token == token).Token = string.Empty;
                context.SaveChanges();
            }
        }

        public Account GetInfo(string token)
        {
            using (EFContext context = new EFContext())
            {
                var account = context.Accounts.FirstOrDefault(x => x.Token == token);
                var list = account.ClothesOf.Select(name => name.Name);

                return new Account() { Clothes = list.ToList(), Id = account.Id, Password = account.Password, UserName = account.UserName };
            }

        }

        public bool Register(Account account, string password)
        {
            using (var context = new EFContext())
            {
                if (context.Accounts.FirstOrDefault(x => x.Password == account.Password && x.UserName == account.UserName) != null)
                {
                    return false;
                }
                context.Accounts.Add(new AccountDB()
                { UserName = account.UserName, Password = password, Token = string.Empty });

                context.SaveChanges();
                return true;
            }
        }

        public bool Login(string login, string password)
        {
            using (EFContext context = new EFContext())
            {
                var token = Guid.NewGuid().ToString();

                if (context.Accounts.FirstOrDefault(x => x.UserName == login && x.Password == password) != null)
                {
                    context.Accounts.FirstOrDefault(x => x.UserName == login && x.Password == password).Token = token;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
