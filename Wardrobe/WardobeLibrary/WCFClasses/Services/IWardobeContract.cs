using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.WCFClasses.Classes;
using WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite;

namespace WardrobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IWardobeContract
    {
        List<Clothes> WhatToWear();

        string GetGeolocation();
        string GetLatitude(string geolocation);
        string GetLongitude(string geolocation);
        void GetAndSetResponse(string api);

        DateTime DateTimeFromDT(int dt);

        DateTime Weather_GetDateTime(DateTime time);
        double Weather_GetTemperature(DateTime time);
        double Weather_GetFeelsLike(DateTime time);
        double Weather_GetWindSpeed(DateTime time);
        string[] Weather_GetClouds(DateTime time);

        Daily GetDailyOnTime(DateTime time);
    }
}
