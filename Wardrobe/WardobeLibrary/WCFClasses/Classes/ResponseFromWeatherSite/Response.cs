using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes.ResponseFromWeatherSite
{
    public class Response
    {
        [JsonProperty("current")]
        public Daily[] Info { get; set; }
        
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}
