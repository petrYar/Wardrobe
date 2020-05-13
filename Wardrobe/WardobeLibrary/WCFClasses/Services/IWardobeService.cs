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
    public interface IWardrobeService
    {
        List<Clothes> WhatToWear();

        string GetGeolocation();
        string GetLatitude(string geolocation);
        string GetLongitude(string geolocation);
        void GetAndSetResponse(string id);

        DateTime DateTimeFromDT(int dt);

        DateTime Weather_GetDateTime();
        double Weather_GetTemperature();
        double Weather_GetFeelsLike();
        double Weather_GetWindSpeed();
        string[] Weather_GetClouds();
    }
}
