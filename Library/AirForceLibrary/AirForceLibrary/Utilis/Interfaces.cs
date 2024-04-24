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
       
       //object handler class that returns the right object according to the demand of the user 
        private static IGDP GdpInterface ;
        private static IOC OCInterface ;
        private static IAFPersonalle AFInterface;
        private static IMission MissionInterface;
        private static IRequest RequestInterface;
       

        public static IGDP GetGdpInterface()
        {
            if(ConnectionClass.GetIsUsingDB())
            {
                GdpInterface = DLGDPDB.SetValidInstance();
                return GdpInterface;
            }
            else
            {
                GdpInterface = DLGDPFH.SetValidInstance();
            }
            return GdpInterface;
        }
        public static IOC GetOCInterface()
        {
            if(ConnectionClass.GetIsUsingDB() )
            {
                OCInterface = DLCommandingOfficerDB.SetValidInstance() ;
            }
            else
            {
                OCInterface =  DLOCFH.SetValidInstance();
            }
            return OCInterface;
        }
        public static IAFPersonalle GetAFInterface()
        {
            if(ConnectionClass.GetIsUsingDB() )
            {
                AFInterface = DLAFPersonalleDB.SetValidInstance();
            }
            else
            {
                AFInterface = DLAFPersonalleFH.SetValidInstance();
            }
            return AFInterface;
        }
        public static IRequest GetRequestInterface()
        {
            if (ConnectionClass.GetIsUsingDB())
            {
                RequestInterface = DLRequestsDB.SetValidInstance();
            }
            else
            {
                RequestInterface = DLRequestsFH.SetValidInstance();
            }
            return RequestInterface;
        }
        public static IMission GetMissionInterface()
        {
            if (ConnectionClass.GetIsUsingDB())
            {
                MissionInterface = DLMissionDB.SetValidInstance();
            }
            else
            {
                MissionInterface = DLMissionFH.SetValidInstance();
            }
            return MissionInterface;
        }
        
    }
}
