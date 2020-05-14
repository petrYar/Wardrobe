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
<<<<<<< HEAD
        public int Id;
=======
        public int Id { get; set; }
>>>>>>> develop

        [DataMember]
        public string UserName { get; set; }

<<<<<<< HEAD
        public string Token { get; set; }
=======
        public List<string> Clothes { get; set; }

        public string Password { get; set; }
>>>>>>> develop
    }
}
