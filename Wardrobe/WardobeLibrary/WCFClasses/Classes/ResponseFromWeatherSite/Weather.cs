using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite
{
    public class Weather
    {
        [JsonProperty("main")]
        public string Main { get; set; }
    }
}
