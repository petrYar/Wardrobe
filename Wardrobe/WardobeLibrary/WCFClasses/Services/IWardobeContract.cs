using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.WCFClasses.Classes;

namespace WardrobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IWardobeContract
    {
        List<Clothes> WhatToWear();

        string GetGeolocation();
        string GetLatitude(string geolocation);
        string GetLongitude(string geolocation);
        void GetAndSetResponse(string id);

        DateTime DateTimeFromDT(int dt);

        DateTime Weather_GetDateTime(int hour);
        double Weather_GetTemperature(int hour);
        double Weather_GetFeelsLike(int hour);
        double Weather_GetWindSpeed(int hour);
        string[] Weather_GetClouds(int hour);
    }
}
