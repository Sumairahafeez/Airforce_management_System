using AirForceLibrary.DL;
using AirForceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Utilis
{
    public class Interfaces
    {
        //ALL DB INTERFACES
       public static IGDP GdpInterface = new DLGDPDB();
       public static IOC OCInterface = new DLCommandingOfficerDB();
       public static IAFPersonalle AFInterface = new DLAFPersonalleDB();
       public static IMission MissionInterface = new DLMissionDB();
       public static IRequest RequestInterface = new DLRequestsDB();
    }
}
