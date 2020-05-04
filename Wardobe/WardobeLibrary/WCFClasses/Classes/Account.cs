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
        private int id;

        public string IdGuid { get; set; }

        [DataMember]
        public string UserName { get; private set; }

        public string Password { get; set; }

        private decimal _money;

        [DataMember]
        public decimal Money { get => _money; private set => this._money = value; }

        public string Token { get; set; }
    }
}
