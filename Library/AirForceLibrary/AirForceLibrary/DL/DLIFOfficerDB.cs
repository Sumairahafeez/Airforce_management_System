using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLIFOfficerDB
    {   //This Class will Store IFOfficer
        public void StoreIFOfficer(InFieldPersonalle I)
        {
            string query = string.Format("INSERT INTO InfieldOfficer VALUES({0},(SELECT Id FROM OC WHERE PakNo = {1}),(SELECT Id FROM AFPersonalle WHERE PakNo = {2}) ",I.GetSquadron(),I.GetOC().GetPakNo(),I.GetPakNo());
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        //This is used to get All OFOfficer
        /*
        public List<InFieldPersonalle> GetAllIFOfficer()
        {   List<InFieldPersonalle> inFields = new List<InFieldPersonalle>();
            string query = "SELECT * FROM InfieldOfficer ";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    
                    string squad = (reader["Squadron"].ToString());
                    
             
                }
            }
        }*/
        
    }
}
