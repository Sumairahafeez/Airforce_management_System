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
            string query = "SELECT * FROM GDP g,AFPersonalle a WHERE g.OffId  = a.Id";
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
                    string loc = reader["PresentlyLocated"].ToString() ;
                    string sq = reader["Squadron"].ToString();

                    GDPilot G = new GDPilot(name,Rank,PakNo,loc,sq);
                    G.SetFlyingHours(int.Parse(reader["FlyingHours"].ToString()));
                    IOC OC = new DLCommandingOfficerDB();
                    IMission mission = new DLMissionDB();
                    IRequest req = new DLRequestsDB();
                    G.SetCommandingOfficer(OC.GetOCbyId(int.Parse(reader["OCId"].ToString())));
                    G.SetMission(mission.GetAllMissionsOfSpecificOfficer(int.Parse(reader["a.Id"].ToString())));
                    G.SetRequests(req.GetRequestsOfSpecificOfficer(int.Parse(reader["PakNo"].ToString())));
                    gdps.Add(G);
                }
                return gdps;
            }
        }
        public void DeleteGDP(int PakNo)
        {
            string query = "DELETE GDP WHERE OffId = (SELECT Id From InfieldOfficer = " + PakNo + ")";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand mcd = new SqlCommand(query,con);
                mcd.ExecuteNonQuery();
            }
        }
    }
}
