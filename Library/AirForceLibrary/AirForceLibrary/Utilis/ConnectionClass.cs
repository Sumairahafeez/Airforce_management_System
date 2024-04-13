using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Utilis
{
    public class ConnectionClass
    {
        public static string ConnectionStr = "Server = localhost\\SQLEXPRESS01; Database = AirForce; trusted_connection = true;";
        public static GDPilot CurrentGDP;
        public static CommandingOfficers CurrentOC;
        public static void setCurrentGDP(GDPilot currentGDP)
        {   
            CurrentGDP = currentGDP;
        }
        public static GDPilot GetCurrentGDP()
        {
            return CurrentGDP;
        }
        public static void SetCurrentOC(CommandingOfficers ic)
        {
            CurrentOC = ic;
        }
        public static CommandingOfficers GetCurrentOC()
        {
            return CurrentOC;
        }

    }
}
