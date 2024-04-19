using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace AirForceLibrary.Utilis
{
    public class ConnectionClass
    {
        public static string ConnectionStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //public static string ConnectionStr = "Server = localhost\\SQLEXPRESS01; Database = AirForce; trusted_connection = true;";
        public static GDPilot CurrentGDP;
        public static CommandingOfficers CurrentOC;
        private static string AFFile;
        private static string GDPFile;
        private static string OCFile;
        private static string MissionFile;
        private static string RequestFile;
        private static bool IsUsingDB;
        public static int Count = 0;
        public static void SetCurrentGDP(GDPilot currentGDP)
        {   
            CurrentGDP = currentGDP;
        }
        public static void SetAFFile(string path)
        {
            AFFile = path;
        }
        public static void SetGDPFile(string path)
        {
            GDPFile = path;
        }
        public static string GetAFFile()
        {

            return AFFile;
        }
        public static string GetRequestFile()
        {
            return RequestFile;
        }
        public static GDPilot GetCurrentGDP()
        {
           
            return CurrentGDP;
        }
        public static string GetGDPFile()
        {
            
            return GDPFile;
        }
        public static void SetOCFile(string path)
        {
            OCFile = path;
        }
        public static string GetOCFile()
        {
            return OCFile;
        }
        public static void SetMissionFile(string path)
        {
            MissionFile = path;
        }
        public static string GetMissionFile()
        {
            return MissionFile;
        }
        public static void SetReportFile(string path)
        {
            RequestFile = path;
        }
        public string GetReportPath()
        {
            return RequestFile;
        }
        public static void SetCurrentOC(CommandingOfficers ic)
        {
            CurrentOC = ic;
        }
        public static CommandingOfficers GetCurrentOC()
        {
            return CurrentOC;
        }
        public static bool GetIsUsingDB()
        {
            return IsUsingDB;
        }
        public static void SetIsUsingDB(bool Isit)
        {
            IsUsingDB = Isit;
        }
        public static string GetConnectionStr()
        {
            return ConnectionStr;
        }
        public static void SetConnectionStr(string ConnStr)
        {
            ConnectionStr = ConnStr;
        }

    }
}
