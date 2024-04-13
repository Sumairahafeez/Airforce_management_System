using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLCommandingOfficerDB:IOC
    {
        //This class will implement crud operations on all the OCs
        //First crud operations will store AF Personalle then this will work
        public void StoreOC(CommandingOfficers commandingOfficers)
        {
            IAFPersonalle IAF = new DLAFPersonalleDB();
            AFPersonalle A = new AFPersonalle(commandingOfficers.GetName(), commandingOfficers.GetRank(), commandingOfficers.GetPakNo(), commandingOfficers.GetPresentlyPosted());
            IAF.StoreAFPersonalle(A);
            string query = string.Format("INSERT INTO OC Values('{0}',(SELECT TOP 1 Id from AFPersonalle WHERE PakNo = {1}))",commandingOfficers.GetSquadron(),commandingOfficers.GetPakNo());
            
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        //This will give OC by Id
        public CommandingOfficers GetOCbyId(int PakNo)
        {
            string query = "SELECT * FROM OC o,AFPersonalle a where o.OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = " + PakNo +") AND a.Id = o.OffId";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    string loc = reader["PresentlyPosted"].ToString();
                    string squad = reader["Squadron"].ToString();
                    int PakNO ;
                    IGDP AFP = new DLGDPDB();
                    List<GDPilot> underOff = AFP.GetAllUFofOC(int.Parse(reader["PakNo"].ToString()));

                    if (int.TryParse(reader["PakNo"].ToString(),out PakNO))
                    {
                        CommandingOfficers c = new CommandingOfficers(name, Rank, PakNO, loc, squad);
                        c.SetUnderOff(underOff);
                        return c;
                    }
                    
                }
                return null;
            }
        }
       //This class dont have a delete query because when oc is to be deleted then its corresponding AFpersonalle will delete as well
       public List<CommandingOfficers> GetAll()
        {
            string query = "SELECT * FROM OC o,AFPersonalle a WHERE o.OffId = a.Id";
            List<CommandingOfficers> commandingOfficers = new List<CommandingOfficers>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    string loc = reader["PresentlyPosted"].ToString();
                    int PakNo = int.Parse(reader["PakNo"].ToString());
                    string squad = reader["Squadron"].ToString();
                    IGDP AFP = new DLGDPDB();
                    List<GDPilot> underOff = AFP.GetAllUFofOC(int.Parse(reader["PakNo"].ToString()));
                    CommandingOfficers c = new CommandingOfficers(name,Rank,PakNo,loc, squad);
                    c.SetUnderOff(underOff);

                    commandingOfficers.Add(c);

                }
                return commandingOfficers;
            }
        }
        //this function deletes an OC
        public void DeleteOC(int PakNo)
        {
            string query = string.Format("DELETE FROM OC WHERE OffId IN (SELECT Id FROM AFPersonalle WHERE PakNo = {0});\r\nDELETE FROM AFPersonalle WHERE PakNo={0};\r\nUPDATE GDP SET OCId = null WHERE OCId IN(SELECT Id FROM AFPersonalle WHERE PakNo = {0})", PakNo);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateOC(int PakNo, CommandingOfficers UpdatedOC)
        {
           AFPersonalle AF = new AFPersonalle(UpdatedOC.GetName(),UpdatedOC.GetRank(),UpdatedOC.GetPakNo(),UpdatedOC.GetPresentlyPosted());
            IAFPersonalle Data = new DLAFPersonalleDB();
            Data.UpdateAFPersonalle(PakNo, AF);
            string query = string.Format("UPDATE OC SET Squadron = '{0}' WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = {1})",UpdatedOC.GetSquadron(),PakNo);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }

        }

    }
}
