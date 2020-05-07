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
    public class WardrobeService : IWardrobeService
    {
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
        public List<Clothes> WhatToWear()
        {
            double temperature = GetTemperature();
            
            using(EFContext con = new EFContext())
            {
                var result = con.Clothes.Where(s => 
                s.CategoryOf.TemperatureMin < temperature &&
                s.CategoryOf.TemperatureMax > temperature);

                return result.Select(x => new Clothes() {Name = x.Name, Color = x.Color,Description = x.Description}).ToList();
            }
        }
    }
}
