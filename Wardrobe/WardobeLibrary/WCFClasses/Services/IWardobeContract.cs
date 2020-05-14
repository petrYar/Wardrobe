using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WardobeLibrary.WCFClasses.Classes;

namespace WardobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IWardobeContract
    {
        [OperationContract]
        List<Clothes> GetClothes();
    }
}
