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
using WardrobeLibrary.WCFClasses;
using Newtonsoft.Json;
using WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite;

namespace WardobeLibrary.WCFClasses
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IAccountContract, IWardobeContract
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

        public string Login(string login, string password)
        {
            using (EFContext context = new EFContext())
            {
                var token = Guid.NewGuid().ToString();

                var user = context.Accounts.FirstOrDefault(x => x.UserName == login && x.Password == password);
                if (user != null)
                {
                    user.Token = token;
                    context.SaveChanges();
                    return token;
                }
                return null;
            }
        }


        public List<Clothes> WhatToWear()
        {
            //On temperature
            double temperature = Weather_GetTemperature(DateTime.Now);

            using (EFContext con = new EFContext())
            {
                var result = con.Clothes.Where(s =>
                s.CategoryOf.TemperatureMin < temperature &&
                s.CategoryOf.TemperatureMax > temperature);

                return result.Select(x => new Clothes() { Name = x.Name, Color = x.Color, Description = x.Description }).ToList();
            }
        }

        #region Response
        public string GetGeolocation()
        {
            string line = "";
            WebRequest request = WebRequest.Create("https://api.ipdata.co?api-key=8bc655ff52d837a2bc4550aff7dbde0b3990a59e7d9ca809aa6bcc9b");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    line = reader.ReadToEnd();
                }
            }
            response.Close();
            return line;
        }
        public string GetLatitude(string geolocation)
        {
            string[] array = { "\"latitude\": " };
            string[] text = geolocation.Split(array, StringSplitOptions.RemoveEmptyEntries);

            string[] array2 = { "," };
            string[] text2 = text[1].Split(array2, StringSplitOptions.RemoveEmptyEntries);

            return text2[0];
        }
        public string GetLongitude(string geolocation)
        {
            string[] array = { "\"longitude\": " };
            string[] text = geolocation.Split(array, StringSplitOptions.RemoveEmptyEntries);

            string[] array2 = { "," };
            string[] text2 = text[1].Split(array2, StringSplitOptions.RemoveEmptyEntries);

            return text2[0];
        }
        public void GetAndSetResponse(string api)
        {
            string geolocation = GetGeolocation();
            string latitude = GetLatitude(geolocation);
            string longitude = GetLongitude(geolocation);
            string line = string.Empty;

            //current   Minute forecast for 1 hour
            //minutely  Hourly forecast for 48 hours
            //hourly    Daily forecast for 7 days
            //daily     Historical weather data for 5 previous days
            string part = "hourly";

            WebRequest request = WebRequest.Create($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude={part}&appid={api}&units=metric");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    line = reader.ReadToEnd();
                }
            }
            response.Close();

            using (EFContext con = new EFContext())
            {
                Response resp = JsonConvert.DeserializeObject<Response>(line);
                con.Weather.Add(new WeatherDB()
                {
                    TimeOfInfo = DateTime.Now,
                    City = resp.Timezone.Split('/')[1],
                    Region = resp.Timezone.Split('/')[0],
                    Weather = resp.Info
                });
            }
        }
        #endregion

        public DateTime DateTimeFromDT(int dt)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dt).ToLocalTime();
            return dtDateTime;
        }

        //NOT NECESSARY
        //public DateTime Weather_GetDateTime(DateTime date)
        //{
        //    using (EFContext con = new EFContext())
        //    {
        //        return DateTimeFromDT(
        //            con.Weather.FirstOrDefault(
        //                x => x.Weather.FirstOrDefault(
        //                    t => DateTimeFromDT(t.DateTime) == date)).Weather[hour].DateTime);
        //    }
        //}
        public double Weather_GetTemperature(DateTime time)
        {
            using (EFContext con = new EFContext())
            {
                return GetDailyOnTime(time).Temperature;
            }
        }
        public double Weather_GetFeelsLike(DateTime time)
        {
            using (EFContext con = new EFContext())
            {
                return GetDailyOnTime(time).Feels_like;
            }
        }
        public double Weather_GetWindSpeed(DateTime time)
        {
            using (EFContext con = new EFContext())
            {
                return GetDailyOnTime(time).Wind_speed;
            }
        }
        public string[] Weather_GetClouds(DateTime time)
        {//Info about statement of the sky
            using (EFContext con = new EFContext())
            {
                string[] texts = { };
                int i = 0;
                foreach (var item in GetDailyOnTime(time).WeatherInfo)
                {
                    texts[i] = item.Main;
                    i++;
                }
                return texts;
            }
        }

        private Daily GetDailyOnTime (DateTime time)
        {
            using (EFContext con = new EFContext())
            {
                return con.Weather.FirstOrDefault(
                    x => DateTimeFromDT(x.Weather[time.Hour].DateTime) == time).Weather[time.Hour];
            }
        }
    }
}
