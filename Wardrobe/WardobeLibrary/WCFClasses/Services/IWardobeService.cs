﻿using System;
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
        //getting temperature of this region from the site
        double GetTemperature();
        List<Clothes> WhatToWear();
    }
}
