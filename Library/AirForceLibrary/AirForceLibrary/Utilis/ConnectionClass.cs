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
        // Connection string for the database
        private static string ConnectionStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        // Current GDPilot and Commanding Officer
        private static GDPilot CurrentGDP;
        private static CommandingOfficers CurrentOC;

        // File paths for various data files
        private static string AFFile;
        private static string GDPFile;
        private static string OCFile;
        private static string MissionFile;
        private static string RequestFile;

        // Flag indicating whether the application is using a database
        private static bool IsUsingDB;

        // Counter for tracking
        public static int Count = 0;

        // Setters and getters for file paths
        public static void SetAFFile(string path)
        {
            AFFile = path;
        }
        public static string GetAFFile()
        {
            return AFFile;
        }
        public static void SetGDPFile(string path)
        {
            GDPFile = path;
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
        public static string GetRequestFile()
        {
            return RequestFile;
        }

        // Setters and getters for current GDPilot and Commanding Officer
        public static void SetCurrentGDP(GDPilot currentGDP)
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

        // Setter and getter for using database flag
        public static bool GetIsUsingDB()
        {
            return IsUsingDB;
        }
        public static void SetIsUsingDB(bool Isit)
        {
            IsUsingDB = Isit;
        }

        // Setters and getters for connection string
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
