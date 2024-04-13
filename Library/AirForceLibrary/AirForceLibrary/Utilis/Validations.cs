using AirForceLibrary.BL;
using AirForceLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirForceLibrary.Utilis
{
    public class Validations
    {   //It checks through ranks if the given set is a valid OC
        public static bool IsValidOC(string Rank)
        {
            if (Rank == "Group Captain" || Rank == "Wing Commander" || Rank == "Air Commander")
            {
                return true;
            }
            return false;
        }
        public static bool IsValidIFOfficer(string Rank)
        {
            if (Rank == "Yes")
            {
                return true;
            }
            return false;
        }
        //It checks the valid IT Officers
        public static bool IsValidIT(string name, int PakNo, string Password)
        {
            if (PakNo == 123 && Password == "123")
            {
                return true;
            }
            return false;
        }
        //IT VALIDATES TH EPAKNO SO TAHT it dont repeats
        public static bool IsValidPakNo(int PakNO)
        {
            List<AFPersonalle> AF = Interfaces.AFInterface.GetAFPersonalles();
            foreach (AFPersonalle personalle in AF)
            {
                if (personalle.GetPakNo() == PakNO)
                {
                    return false;
                }
            }
            return true;
        }
        //This checks if the given name and pakno are valid GDP
        public static bool IsValidGDP(string name, int PakNO, string Password) 
        {
            List<GDPilot> gDPilots = Interfaces.GdpInterface.GetAllGdps();
            foreach (GDPilot G in gDPilots)
            { 
                if(G.GetName() == name && G.GetPakNo() == PakNO )
                {
                    return true;
                }
            }
            return false;
        }
        //This is to check if the given name pakno is an valid OC it is an eample of static polymorphism
        public static bool IsValidOC(string name,int PakNO, string Password)
        {
            List<CommandingOfficers> OCs = Interfaces.OCInterface.GetAll();
            foreach(CommandingOfficers OC in OCs)
            {
                if(OC.GetName() == name && OC.GetPakNo()==PakNO)
                {
                    return true;
                }
            }
            return false;
        }
        //This one fetches dat ain dataTable
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }
        //Another Example of static Polymorphism
        public static bool IsValidOC(int PakNo,string Name)
        {
            List<CommandingOfficers> OCs = Interfaces.OCInterface.GetAll();
            foreach (CommandingOfficers OC in OCs)
            {
                if (OC.GetName() == Name && OC.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }
        //Same for GDP Class
        public static bool IsValidGDP(int PakNo,string Name)
        {

            List<GDPilot> gDPilots = Interfaces.GdpInterface.GetAllGdps();
            foreach (GDPilot G in gDPilots)
            {
                if (G.GetName() == Name && G.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }
        //This function checks GDP only through its PakNo
        public static bool IsValidGDP(int PakNo)
        {
            List<GDPilot> gDPilots = Interfaces.GdpInterface.GetAllGdps();
            foreach (GDPilot G in gDPilots)
            {
                if ( G.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }
        //This function checks the OC only through its PakNo
        public static bool IsValidOC(int PakNo)
        {
            List<CommandingOfficers> OCs = Interfaces.OCInterface.GetAll();
            foreach (CommandingOfficers OC in OCs)
            {
                if ( OC.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }
        //Lets check foro a valid AFPersonalle
        public static bool IsValidAFPersonalle(int PakNo)
        {
            List<AFPersonalle> AFs = Interfaces.AFInterface.GetAFPersonalles();
            foreach(AFPersonalle AF in AFs)
            {
                if(AF.GetPakNo()==PakNo)
                {
                    return true;
                }
            }
            return false;
        }
        //Lets check if the squadron and their current posted locations match this function wil work if an Infield Officer want sto add under officer however if an outfield like IT OC wants to do the same
        //another fucntion would implement that will check only presemtly posted
        public static bool IsFitForTHEOC(CommandingOfficers OC,AFPersonalle AF,string squadron)
        {
           
            if(OC.GetSquadron() == squadron && OC.GetPresentlyPosted() == AF.GetPresentlyPosted())
            {
                return true;
            }
            return false;
        }
        /* IF AN OUT FIELD OC WANTS TO ADD UNDER OFFICER THEN THIS FUCNTION WILL IMPLEMENT
         * Public static bool IsFitForTHEOC(string OCLocation,string OfficerLOc)
         * {
         *   if(OCLocation == OfficerLOc)
         *   {
         *      return true;
         *   }
         *   return false
         * }*/
       //This will check the valididty of the requests
         public static Requests IsValidRequest(int PakNo,int id)
        {
            List<Requests> req = Interfaces.RequestInterface.GetRequestsOfSpecificOfficer(PakNo);
            foreach(Requests Req in req)
            {
                if(Req.GetRequestId() == id)
                {
                    return Req;
                }
            }
            return null;
        }
        
    }
}
