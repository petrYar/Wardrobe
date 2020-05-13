using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes.Response
{
    public class Current
    {
        [JsonProperty("dt")]
        public int DateTime { get; set; }

        [JsonProperty("temp")]
        public float Temp { get; set; }

        [JsonProperty("feels_like")]
        public float Feels_like { get; set; }

        [JsonProperty("weather")]
        public Weather[] WeatherInfo { get; set; }

        [JsonProperty("wind_speed")]
        public float Wind_speed { get; set; }
    }
}
