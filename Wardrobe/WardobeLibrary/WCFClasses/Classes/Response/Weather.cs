using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes.Response
{
    public class Weather
    {
        [JsonProperty("main")]
        public string Main { get; set; }
    }
}
