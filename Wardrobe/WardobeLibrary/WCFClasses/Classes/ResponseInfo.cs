using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite;

namespace WardobeLibrary.WCFClasses.Classes
{
    public class ResponseInfo
    {
        public string Region { get; set; }

        public string City { get; set; }

        public Current Weather { get; set; }

        public DateTime TimeOfInfo { get; set; }
    }
}
