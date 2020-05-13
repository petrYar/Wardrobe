using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses.Classes.Response
{
    public class Response
    {
        [JsonProperty("current")]
        public Current Info { get; set; }
    }
}
