using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AirForceLibrary.DL
{
    public class DLGDPDB:IGDP
    {
        /// <summary>
        /// Stores a GDPilot in the database.
        /// </summary>
        /// <param name="G">The GDPilot to store.</param>
        private static DLGDPDB Instance;
        private DLGDPDB()
        {
            
        }
        public static DLGDPDB SetValidInstance()
        {
            if (Instance == null)
            {
                Instance = new DLGDPDB();
            }
            return Instance;
        }
        public void StoreGDP(GDPilot G)
        {
            // Store AFPersonalle information
            IAFPersonalle IAF = DLAFPersonalleDB.SetValidInstance();
            AFPersonalle A = new AFPersonalle(G.GetName(), G.GetRank(), G.GetPakNo(), G.GetPresentlyPosted());
            A.SetPassword(G.GetPassword());
            A.SetBranch(G.GetBranch());
            IAF.StoreAFPersonalle(A);

            // Construct SQL query to insert GDPilot into the database
            
            string query = string.Format("INSERT INTO GDP VALUES({0}, (SELECT TOP 1 Id FROM AFPersonalle WHERE PakNo = {1}), null, '{2}')", G.GetFlyingHours(), G.GetPakNo(), G.GetSquadron());
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves all GDPilots from the database.
        /// </summary>
        /// <returns>A list of all GDPilots stored in the database.</returns>
        public List<GDPilot> GetAllGdps()
        {
            string query = "SELECT * FROM GDP g, AFPersonalle a WHERE g.OfficerId = a.Id";
            List<GDPilot> gdps = new List<GDPilot>();
           // try
            {
               
                using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Read GDPilot properties from database
                        string name = reader["Name"].ToString();
                        string Rank = reader["Rank"].ToString();
                        int PakNo = int.Parse(reader["PakNo"].ToString());
                        string loc = reader["PresentlyPosted"].ToString();
                        string sq = reader["Squadron"].ToString();
                        string Password = reader["Password"].ToString();
                        string Branch = reader["Branch"].ToString();
                        GDPilot G = new GDPilot(name, Rank, PakNo, loc, sq);
                        G.SetFlyingHours(int.Parse(reader["FlyingHours"].ToString()));
                        G.SetBranch(Branch);
                        G.SetPassword(Password);
                        // Retrieve associated objects
                        IOC OC = DLCommandingOfficerDB.SetValidInstance();
                        IMission mission = DLMissionDB.SetValidInstance();
                        IRequest req = DLRequestsDB.SetValidInstance();
                        //if (reader["OCId"].ToString() != null)
                        //G.SetCommandingOfficer(OC.GetOCbyId(int.Parse(reader["OCId"].ToString())));
                        G.SetMission(mission.GetAllMissionsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                        G.SetRequests(req.GetRequestsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));

                        gdps.Add(G);
                    }
                   
                }
           
            }
            //catch(Exception ex)
            {
                //
            }
            return gdps;
        }

        /// <summary>
        /// Retrieves all GDPilots under a specific Officer.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Officer.</param>
        /// <returns>A list of GDPilots under the specified Officer.</returns>
        public List<GDPilot> GetAllUFofOC(int PakNo)
        {
            string query = "SELECT * FROM GDP g, AFPersonalle a WHERE g.OfficerId = a.Id AND g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = " + PakNo + "))";
            List<GDPilot> gdps = new List<GDPilot>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Read GDPilot properties from database
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    int PakNO = int.Parse(reader["PakNo"].ToString());
                    string loc = reader["PresentlyPosted"].ToString();
                    string sq = reader["Squadron"].ToString();
                    string Password = reader["Password"].ToString();
                    string branch = reader["Branch"].ToString();
                    GDPilot G = new GDPilot(name, Rank, PakNO, loc, sq);
                    G.SetPassword(Password);
                    G.SetBranch(branch);
                    G.SetFlyingHours(int.Parse(reader["FlyingHours"].ToString()));

                    // Retrieve associated objects
                    IOC OC = DLCommandingOfficerDB.SetValidInstance();
                    IMission mission = DLMissionDB.SetValidInstance();
                    IRequest req = DLRequestsDB.SetValidInstance();

                    G.SetCommandingOfficer(OC.GetOCbyId(int.Parse(reader["OCId"].ToString())));
                    G.SetMission(mission.GetAllMissionsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    G.SetRequests(req.GetRequestsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));

                    gdps.Add(G);
                }
                return gdps;
            }
        }

        /// <summary>
        /// Deletes a GDPilot from the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to delete.</param>
        public void DeleteGDP(int PakNo)
        {
            string query = string.Format("DELETE FROM GDP WHERE OfficerId IN (SELECT Id FROM AFPersonalle WHERE PakNo = {0}); DELETE FROM AFPersonalle WHERE PakNo = {0};", PakNo);
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand mcd = new SqlCommand(query, con);
                mcd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a GDPilot through its PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to retrieve.</param>
        /// <returns>The GDPilot with the specified PakNo.</returns>
        public GDPilot GetGDPThroughPakNo(int PakNo)
        {
            string query = "SELECT * FROM GDP g, AFPersonalle a WHERE g.OfficerId = a.Id AND a.PakNo = " + PakNo;
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Read GDPilot properties from database
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    string loc = reader["PresentlyPosted"].ToString();
                    string sq = reader["Squadron"].ToString();
                    string Password = reader["Password"].ToString();
                    string branch = reader["Branch"].ToString();
                    GDPilot G = new GDPilot(name, Rank, PakNo, loc, sq);
                    G.SetPassword(Password);
                    G.SetBranch(branch);
                    return G;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates a GDPilot in the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to update.</param>
        /// <param name="Gdp">The updated GDPilot information.</param>
        public void UpdateGDP(int PakNo, GDPilot Gdp)
        {
            // Update AFPersonalle information
            AFPersonalle AF = new AFPersonalle(Gdp.GetName(), Gdp.GetRank(), Gdp.GetPakNo(), Gdp.GetPresentlyPosted());
            AF.SetBranch(Gdp.GetBranch());
            AF.SetPassword(Gdp.GetPassword());
            IAFPersonalle Data = DLAFPersonalleDB.SetValidInstance();
            Data.UpdateAFPersonalle(PakNo, AF);

            // Construct SQL query to update GDPilot in the database
            string query;
            if (Gdp.GetOC() != null)
            {
                query = string.Format("UPDATE GDP SET Squadron = '{0}', OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = {1})), FlyingHours = {2} WHERE OfficerId = (SELECT Id FROM AFPersonalle WHERE PakNo = {3})", Gdp.GetSquadron(), Gdp.GetOC().GetPakNo(), Gdp.GetFlyingHours(), PakNo);
            }
            else
            {
                query = string.Format("UPDATE GDP SET Squadron = '{0}', FlyingHours = {1} WHERE OfficerId = (SELECT Id FROM AFPersonalle WHERE PakNo = {2})", Gdp.GetSquadron(), Gdp.GetFlyingHours(), PakNo);
            }
            using (SqlConnection con = new SqlConnection(ConnectionClass.GetConnectionStr()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
