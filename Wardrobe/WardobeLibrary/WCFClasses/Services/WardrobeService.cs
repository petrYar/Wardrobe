using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using WardobeLibrary.WCFClasses.Classes;
using WardobeLibrary.Models;
using Newtonsoft.Json;
using WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite;

namespace WardrobeLibrary.WCFClasses
{
    public class WardrobeS
    {
        public List<Clothes> WhatToWear()
        {
            double temperature = Weather_GetTemperature(DateTime.Now.Hour);

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
        public void GetAndSetResponse(string id)
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

            WebRequest request = WebRequest.Create($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude={part}&appid={id}&units=metric");
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
                con.Weather.TimeOfInfo = DateTime.Now;
                Response resp = JsonConvert.DeserializeObject<Response>(line);
                con.Weather.City = resp.Timezone.Split('/')[1];
                con.Weather.Region = resp.Timezone.Split('/')[0];
                con.Weather.Weather = resp.Info;
            }
        }
        #endregion

        public DateTime DateTimeFromDT(int dt)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dt).ToLocalTime();
            return dtDateTime;
        }

        public DateTime Weather_GetDateTime(int hour)
        {
            using (EFContext con = new EFContext())
            {
                return DateTimeFromDT(con.Weather.Weather[hour].DateTime);
            }
        }
        public double Weather_GetTemperature(int hour)
        {
            using (EFContext con = new EFContext())
            {
                return con.Weather.Weather[hour].Temperature;
            }
        }
        public double Weather_GetFeelsLike(int hour)
        {
            using (EFContext con = new EFContext())
            {
                return con.Weather.Weather[hour].Feels_like;
            }
        }
        public double Weather_GetWindSpeed(int hour)
        {
            using (EFContext con = new EFContext())
            {
                return con.Weather.Weather[hour].Wind_speed;
            }
        }
        public string[] Weather_GetClouds(int hour)
        {//Info about statement of the sky
            using (EFContext con = new EFContext())
            {
                string[] texts = {};
                int i = 0;
                foreach (var item in con.Weather.Weather[hour].WeatherInfo)
                {
                    texts[i] = item.Main;
                    i++;
                }
                return texts;
            }
        }
    }
}
