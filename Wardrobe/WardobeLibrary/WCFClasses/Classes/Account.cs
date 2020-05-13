using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses
{
    [DataContract]
    public class Account
    {
        public int Id;

        [DataMember]
        public string UserName { get; private set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
