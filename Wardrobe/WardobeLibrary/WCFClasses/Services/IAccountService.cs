using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        bool Login(string login, string password);

        [OperationContract]
        bool Register(Account account,string password);

        [OperationContract]
        Account GetInfo(string token);

        [OperationContract]
        void Disconnect(string token);
    }
}
