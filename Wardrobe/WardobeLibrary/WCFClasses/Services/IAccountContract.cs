using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IAccountContract
    {
        [OperationContract]
        string Login(string login, string password);

        [OperationContract]
        void Register(Account account, string password);

        [OperationContract]
        Account GetInfo(string token);

        [OperationContract]
        void Disconnect(string token);
    }
}
