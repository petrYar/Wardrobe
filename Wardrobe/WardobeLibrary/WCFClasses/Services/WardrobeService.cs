using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using WardobeLibrary.WCFClasses.Classes;
using WardobeLibrary.Models;

namespace WardrobeLibrary.WCFClasses
{
    public class WardrobeS
    {
        public List<Clothes> WhatToWear()
        {
            double temperature = GetTemperature();

            using (EFContext con = new EFContext())
            {
                var result = con.Clothes.Where(s =>
                s.CategoryOf.TemperatureMin < temperature &&
                s.CategoryOf.TemperatureMax > temperature);

                return result.Select(x => new Clothes() { Name = x.Name, Color = x.Color, Description = x.Description }).ToList();
            }
        }

        #region Response
        public void FASD()
        {
            string geolocation = GetGeolocation();
            string latitude = GetLatitude(geolocation);
            string longitude = GetLongitude(geolocation);
            string line = string.Empty;
            string id = "024fcef23bd432769b6fb06ff0d2f069";//need to rework

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


            PreResponse resp = JsonConvert.DeserializeObject<PreResponse>(line);
            tb4.Text = "DateTime " + resp.Current.DateTime + "\nFeels_like " + resp.Current.Feels_like +
                "\nTemp " + resp.Current.Temp + "\nWeather.Main " + resp.Current.Weather[0].Main
                + "\nWind_speed " + resp.Current.Wind_speed

                + "\nDateTime = " + DateTimeFromDT(resp.Current.DateTime).Day + "." +
                DateTimeFromDT(resp.Current.DateTime).Month + "." +
                DateTimeFromDT(resp.Current.DateTime).Year;
        }

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

        public DateTime DateTimeFromDT(double dt)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dt).ToLocalTime();
            return dtDateTime;
        }
        #endregion

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
    }
}
