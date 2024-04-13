using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLGDPDB:IGDP
    { 
        public void StoreGDP(GDPilot G)
        {
            IAFPersonalle IAF = new DLAFPersonalleDB();
            AFPersonalle A = new AFPersonalle(G.GetName(), G.GetRank(), G.GetPakNo(), G.GetPresentlyPosted());
            IAF.StoreAFPersonalle(A);
           
            string query = string.Format("INSERT INTO GDP VALUES({0},(SELECT Top 1 Id FROM AFPersonalle WHERE PakNo = {1}),null, '{2}')",G.GetFlyingHours(),G.GetPakNo(),G.GetSquadron());
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public List<GDPilot> GetAllGdps()
        {
            string query = "SELECT * FROM GDP g,AFPersonalle a WHERE g.OfficerId  = a.Id ";
            List<GDPilot> gdps = new List<GDPilot>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd .ExecuteReader();
                while(reader.Read())
                {   string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    int PakNo = int.Parse(reader["PakNo"].ToString());
                    string loc = reader["PresentlyPosted"].ToString() ;
                    string sq = reader["Squadron"].ToString();

                    GDPilot G = new GDPilot(name,Rank,PakNo,loc,sq);
                    G.SetFlyingHours(int.Parse(reader["FlyingHours"].ToString()));
                    IOC OC = new DLCommandingOfficerDB();
                    IMission mission = new DLMissionDB();
                    IRequest req = new DLRequestsDB();
                   // if (reader["OCId"].ToString() != null)
                   // G.SetCommandingOfficer(OC.GetOCbyId(int.Parse(reader["OCId"].ToString())));
                    
                   
                    G.SetMission(mission.GetAllMissionsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    G.SetRequests(req.GetRequestsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    gdps.Add(G);
                }
                return gdps;
            }
        }
        public List<GDPilot> GetAllUFofOC(int PakNo)
        {
            string query = "SELECT * FROM  GDP g,AFPersonalle a where g.OfficerId = a.Id and g.OCId = (SELECT Id FROM OC WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo= " + PakNo + "))\r\n";

            List<GDPilot> gdps = new List<GDPilot>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    int PakNO = int.Parse(reader["PakNo"].ToString());
                    string loc = reader["PresentlyPosted"].ToString();
                    string sq = reader["Squadron"].ToString();

                    GDPilot G = new GDPilot(name, Rank, PakNO, loc, sq);
                    G.SetFlyingHours(int.Parse(reader["FlyingHours"].ToString()));
                    IOC OC = new DLCommandingOfficerDB();
                    IMission mission = new DLMissionDB();
                    IRequest req = new DLRequestsDB();

                    G.SetCommandingOfficer(OC.GetOCbyId(int.Parse(reader["OCId"].ToString())));


                    G.SetMission(mission.GetAllMissionsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    G.SetRequests(req.GetRequestsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    gdps.Add(G);
                }
                return gdps;
            }
         }
        public void DeleteGDP(int PakNo)
        {
            string query = string.Format("DELETE FROM GDP WHERE OfficerId IN (SELECT Id FROM AFPersonalle WHERE PakNo = {0});\r\nDELETE FROM AFPersonalle WHERE PakNo = {0};\r\n",PakNo);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand mcd = new SqlCommand(query,con);
                mcd.ExecuteNonQuery();
            }
        }
        //This function returns a GDP throiugh its PakNo
        public GDPilot GetGDPThroughPakNo(int PakNo)
        {
            string query = "SELECT * FROM GDP g,AFPersonalle a WHERE g.OfficerId  = a.Id AND a.PakNo = "+PakNo;
            List<GDPilot> gdps = new List<GDPilot>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    string loc = reader["PresentlyPosted"].ToString();
                    string sq = reader["Squadron"].ToString();

                    GDPilot G = new GDPilot(name, Rank, PakNo, loc, sq);
                    return G;
                }
                
            }
            return null;
        }
        //This is update function for GDP
        public void UpdateGDP(int PakNo,GDPilot Gdp)
        {
            AFPersonalle AF = new AFPersonalle(Gdp.GetName(), Gdp.GetRank(), Gdp.GetPakNo(), Gdp.GetPresentlyPosted());
            IAFPersonalle Data = new DLAFPersonalleDB();
            Data.UpdateAFPersonalle(PakNo, AF);
            string query;
            if (Gdp.GetOC() != null)
            {
                query = string.Format("UPDATE GDP SET Squadron = '{0}',OCId = (SELECT Id FROM OC Where OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = {1})), FlyingHours = {2} WHERE OfficerId = (SELECT Id FROM AFPersonalle WHERE PakNo = {3})", Gdp.GetSquadron(),Gdp.GetOC().GetPakNo(),Gdp.GetFlyingHours(),PakNo);
            }
            else
            {
                query = string.Format("UPDATE GDP SET Squadron = '{0}', FlyingHours = {1} WHERE OfficerId = (SELECT Id FROM AFPersonalle WHERE PakNo = {2})", Gdp.GetSquadron(), Gdp.GetFlyingHours(), PakNo);

            }
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }

        }
    }
}
