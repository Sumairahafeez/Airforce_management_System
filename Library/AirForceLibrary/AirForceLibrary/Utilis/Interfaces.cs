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
       
        
        private static IGDP GdpInterface ;
        private static IOC OCInterface ;
        private static IAFPersonalle AFInterface;
        private static IMission MissionInterface;
        private static IRequest RequestInterface;
       

        public static IGDP GetGdpInterface()
        {
            if(ConnectionClass.GetIsUsingDB())
            {
                GdpInterface = new DLGDPDB();
                return GdpInterface;
            }
            else
            {
                GdpInterface = new DLGDPDB();
            }
            return GdpInterface;
        }
        public static IOC GetOCInterface()
        {
            if(ConnectionClass.GetIsUsingDB() )
            {
                OCInterface = new DLCommandingOfficerDB();
            }
            else
            {
                OCInterface = new DLCommandingOfficerDB();
            }
            return OCInterface;
        }
        public static IAFPersonalle GetAFInterface()
        {
            if(ConnectionClass.GetIsUsingDB() )
            {
                AFInterface = new DLAFPersonalleDB();
            }
            else
            {
                AFInterface = new DLAFPersonalleDB();
            }
            return AFInterface;
        }
        public static IRequest GetRequestInterface()
        {
            if (ConnectionClass.GetIsUsingDB())
            {
                RequestInterface = new DLRequestsDB();
            }
            else
            {
                RequestInterface = new DLRequestsFH();
            }
            return RequestInterface;
        }
        public static IMission GetMissionInterface()
        {
            if (ConnectionClass.GetIsUsingDB())
            {
                MissionInterface = new DLMissionDB();
            }
            else
            {
                MissionInterface = new DLMissionFH();
            }
            return MissionInterface;
        }
        
    }
}
