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
            string query = string.Format("INSERT INTO OC Values('{0}',(SELECT Id from AFPersonalle WHERE PakNo = {1}))",commandingOfficers.GetSquadron(),commandingOfficers.GetPakNo());
            
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        //This will give OC by Id
        public CommandingOfficers GetOCbyId(int id)
        {
            string query = "SELECT * FROM OC WHERE Id = " + id;
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {   IAFPersonalle Data = new DLAFPersonalleDB();
                    AFPersonalle A = Data.GetAFPersonalleByID(int.Parse(reader["OffId"].ToString()));
                    string squad = reader["Squadron"].ToString();
                    CommandingOfficers c = new CommandingOfficers(A.GetName(), A.GetRank(), A.GetPakNo(), A.GetPresentlyPosted(), squad);
                    return c;
                }
                return null;
            }
        }
       //This class dont have a delete query because when oc is to be deleted then its corresponding AFpersonalle will delete as well
       public List<CommandingOfficers> GetAll()
        {
            string query = "SELECT * FROM OC ";
            List<CommandingOfficers> commandingOfficers = new List<CommandingOfficers>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IAFPersonalle Data = new DLAFPersonalleDB();
                    AFPersonalle A = Data.GetAFPersonalleByID(int.Parse(reader["OffId"].ToString()));
                    string squad = reader["Squadron"].ToString();
                    CommandingOfficers c = new CommandingOfficers(A.GetName(), A.GetRank(), A.GetPakNo(), A.GetPresentlyPosted(), squad);
                    commandingOfficers.Add(c);

                }
                return commandingOfficers;
            }
        }

    }
}
