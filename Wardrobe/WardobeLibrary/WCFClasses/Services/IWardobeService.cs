using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WardobeLibrary.WCFClasses
{
    [ServiceContract]
    public interface IWardobeService
    {
        //getting temperature of this region from the site
        double GetTemperature();
    }
}
