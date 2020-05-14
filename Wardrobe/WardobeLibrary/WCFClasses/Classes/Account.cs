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
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        public List<string> Clothes { get; set; }

        public string Password { get; set; }
    }
}
