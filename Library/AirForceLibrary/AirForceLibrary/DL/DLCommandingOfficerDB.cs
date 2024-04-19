using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLCommandingOfficerDB:IOC
    {
        /// <summary>
        /// Stores a Commanding Officer in the database.
        /// </summary>
        /// <param name="commandingOfficers">The Commanding Officer to store.</param>
        private static DLCommandingOfficerDB Instance;
        private DLCommandingOfficerDB()
        {
            
        }
        public static DLCommandingOfficerDB SetValidInstance()
        {
            if (Instance == null)
            {
                Instance = new DLCommandingOfficerDB();
            }
            return Instance;
        }
        public void StoreOC(CommandingOfficers commandingOfficers)
        {
            IAFPersonalle IAF = DLAFPersonalleDB.SetValidInstance();
            AFPersonalle A = new AFPersonalle(commandingOfficers.GetName(), commandingOfficers.GetRank(), commandingOfficers.GetPakNo(), commandingOfficers.GetPresentlyPosted());
            A.SetBranch(commandingOfficers.GetBranch());
            A.SetPassword(commandingOfficers.GetPassword());
            IAF.StoreAFPersonalle(A);
            string query = string.Format("INSERT INTO OC VALUES('{0}', (SELECT TOP 1 Id FROM AFPersonalle WHERE PakNo = {1}))", commandingOfficers.GetSquadron(), commandingOfficers.GetPakNo());

            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a Commanding Officer by PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to retrieve.</param>
        /// <returns>The Commanding Officer with the specified PakNo.</returns>
        public CommandingOfficers GetOCbyId(int PakNo)
        {
            string query = "SELECT * FROM OC o, AFPersonalle a WHERE o.OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = " + PakNo + ") AND a.Id = o.OffId";

            try
            {
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
                        string squad = reader["Squadron"].ToString();
                        string Password = reader["Password"].ToString();
                        string branch = reader["Branch"].ToString();
                        int PakNO;
                        IGDP AFP = DLGDPDB.SetValidInstance();
                        List<GDPilot> underOff = AFP.GetAllUFofOC(int.Parse(reader["PakNo"].ToString()));

                        if (int.TryParse(reader["PakNo"].ToString(), out PakNO))
                        {
                            CommandingOfficers c = new CommandingOfficers(name, Rank, PakNO, loc, squad);
                            c.SetUnderOff(underOff);
                            c.SetBranch(branch);
                            c.SetPassword(Password);
                            return c;
                        }
                    }
                   
                }
            }
            catch (Exception )
            {

            }
            return null;
        }

        /// <summary>
        /// Retrieves all Commanding Officers from the database.
        /// </summary>
        /// <returns>A list of all Commanding Officers stored in the database.</returns>
        public List<CommandingOfficers> GetAll()
        {
            string query = "SELECT * FROM OC o, AFPersonalle a WHERE o.OffId = a.Id";
            List<CommandingOfficers> commandingOfficers = new List<CommandingOfficers>();
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
                    int PakNo = int.Parse(reader["PakNo"].ToString());
                    string squad = reader["Squadron"].ToString();
                    string Password = reader["Password"].ToString();
                    string branch = reader["Branch"].ToString();
                    IGDP AFP = DLGDPDB.SetValidInstance();
                    List<GDPilot> underOff = AFP.GetAllUFofOC(int.Parse(reader["PakNo"].ToString()));
                    CommandingOfficers c = new CommandingOfficers(name, Rank, PakNo, loc, squad);
                    c.SetUnderOff(underOff);
                    c.SetPassword(Password);
                    c.SetBranch(branch);
                    commandingOfficers.Add(c);

                }
                return commandingOfficers;
            }
        }

        /// <summary>
        /// Deletes a specific Commanding Officer from the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to delete.</param>
        public void DeleteOC(int PakNo)
        {
            string query = string.Format("DELETE FROM OC WHERE OffId IN (SELECT Id FROM AFPersonalle WHERE PakNo = {0});\r\nDELETE FROM AFPersonalle WHERE PakNo = {0};\r\nUPDATE GDP SET OCId = null WHERE OCId IN(SELECT Id FROM AFPersonalle WHERE PakNo = {0})", PakNo);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a Commanding Officer in the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to update.</param>
        /// <param name="UpdatedOC">The updated Commanding Officer information.</param>
        public void UpdateOC(int PakNo, CommandingOfficers UpdatedOC)
        {
            AFPersonalle AF = new AFPersonalle(UpdatedOC.GetName(), UpdatedOC.GetRank(), UpdatedOC.GetPakNo(), UpdatedOC.GetPresentlyPosted());
            AF.SetBranch(UpdatedOC.GetBranch());
            AF.SetPassword(UpdatedOC.GetPassword());
            IAFPersonalle Data = DLAFPersonalleDB.SetValidInstance();
            Data.UpdateAFPersonalle(PakNo, AF);
            string query = string.Format("UPDATE OC SET Squadron = '{0}' WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = {1})", UpdatedOC.GetSquadron(), PakNo);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
