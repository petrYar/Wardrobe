﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes
{
    public class Categories
    {
        private int id { get; set; }

        public string Name { get; set; }

        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
    }
}
