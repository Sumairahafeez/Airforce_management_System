using AirForceLibrary.BL;
using AirForceLibrary.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace AirForceLibrary.Utilis
{
    public class Validations
    {
        // Checks if the given rank is valid for an OC
        public static bool IsValidOC(string Rank)
        {
            return Rank == "Group Captain" || Rank == "Wing Commander" || Rank == "Air Commander";
        }

        // Checks if the given rank indicates an IF Officer
        public static bool IsValidIFOfficer(string Rank)
        {
            return Rank == "Yes";
        }

        // Validates IT Officer credentials
        public static bool IsValidIT(string name, int PakNo, string Password)
        {
            if (PakNo == 123 && Password == "123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Validates the uniqueness of PakNo
        public static bool IsValidPakNo(int PakNO)
        {
            List<AFPersonalle> AF = Interfaces.GetAFInterface().GetAFPersonalles();
            foreach (AFPersonalle personalle in AF)
            {
                if (personalle.GetPakNo() == PakNO)
                {
                    return false;
                }
            }
            return true;
        }

        // Checks if the given name, PakNo, and password match a valid GDPilot
        public static bool IsValidGDP(string name, int PakNO, string Password)
        {
            List<GDPilot> gDPilots = Interfaces.GetGdpInterface().GetAllGdps();
            foreach (GDPilot G in gDPilots)
            {
                if (G.GetName() == name && G.GetPakNo() == PakNO && G.GetPassword() == Password)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if the given name, PakNo, and password match a valid OC
        public static bool IsValidOC(string name, int PakNO, string Password)
        {
            try
            {
                List<CommandingOfficers> OCs = Interfaces.GetOCInterface().GetAll();
                foreach (CommandingOfficers OC in OCs)
                {
                    if (OC.GetName() == name && OC.GetPakNo() == PakNO && OC.GetPassword() == Password)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        // Retrieves data from a DataTable using the provided query
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        // Checks if the given name and PakNo match a valid OC
        public static bool IsValidOC(int PakNo, string Name)
        {
            List<CommandingOfficers> OCs = Interfaces.GetOCInterface().GetAll();
            foreach (CommandingOfficers OC in OCs)
            {
                if (OC.GetName() == Name && OC.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if the given name and PakNo match a valid GDPilot
        public static bool IsValidGDP(int PakNo, string Name)
        {
            List<GDPilot> gDPilots = Interfaces.GetGdpInterface().GetAllGdps();
            foreach (GDPilot G in gDPilots)
            {
                if (G.GetName() == Name && G.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if a GDPilot with the given PakNo exists
        public static bool IsValidGDP(int PakNo)
        {
            List<GDPilot> gDPilots = Interfaces.GetGdpInterface().GetAllGdps();
            foreach (GDPilot G in gDPilots)
            {
                if (G.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if an OC with the given PakNo exists
        public static bool IsValidOC(int PakNo)
        {
            List<CommandingOfficers> OCs = Interfaces.GetOCInterface().GetAll();
            foreach (CommandingOfficers OC in OCs)
            {
                if (OC.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if an AFPersonalle with the given PakNo exists
        public static bool IsValidAFPersonalle(int PakNo)
        {
            List<AFPersonalle> AFs = Interfaces.GetAFInterface().GetAFPersonalles();
            foreach (AFPersonalle AF in AFs)
            {
                if (AF.GetPakNo() == PakNo)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if an AFPersonalle is fit to be under an OC based on squadron and currently posted location
        public static bool IsFitForTHEOC(CommandingOfficers OC, AFPersonalle AF, string squadron)
        {
             if(OC.GetSquadron() == squadron && OC.GetPresentlyPosted() == AF.GetPresentlyPosted())
            {
                return true;
            }
            return false;
        }

        // Validates a request based on PakNo and ID
        public static Requests IsValidRequest(int PakNo, int id)
        {
            List<Requests> req = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(PakNo);
            foreach (Requests Req in req)
            {
                if (Req.GetRequestId() == id)
                {
                    return Req;
                }
            }
            return null;
        }
        public static bool IsValidRequestId(int Id, int PakNo)
        {
            List<Requests> req = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(PakNo);
            foreach (Requests Req in req)
            {
                if (Req.GetRequestId() == Id)
                {
                    return true;
                }
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
    }

}
