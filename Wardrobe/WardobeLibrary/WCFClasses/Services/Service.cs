using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.Models;
using WardobeLibrary.WCFClasses.Classes;

namespace WardobeLibrary.WCFClasses
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IAccountContract, IWardobeContract
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
            account.IdGuid = Guid.NewGuid().ToString();
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

        public double GetTemperature()
        {
            string line = "";
            WebRequest request = WebRequest.Create("https://world-weather.ru/pogoda/ukraine/rivne/");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    line = reader.ReadToEnd();
                }
            }
            response.Close();

            string[] array = { "<div id=\"weather-now-number\">" };
            string[] text = line.Split(array, StringSplitOptions.RemoveEmptyEntries);

            string[] array2 = { "<span>" };
            string[] text2 = text[1].Split(array2, StringSplitOptions.RemoveEmptyEntries);

            string result = text2[0];

            if (result[0] == '+')
                result = result.Trim('+');

            return double.Parse(result);
        }
        public List<Clothes> GetClothes()
        {
            using (EFContext context = new EFContext())
            {
                var clothesFromDb = context.Clothes;

                return clothesFromDb.Select(t => new Clothes { Name = t.Name, Color = t.Color, Description = t.Description }).ToList();

            }

        }
    }
}
