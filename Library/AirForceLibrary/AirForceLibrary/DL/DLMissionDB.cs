using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLMissionDB:IMission
    {
        /// <summary>
        /// Stores a mission in the database.
        /// </summary>
        /// <param name="mission">The mission to store.</param>
        /// <param name="PakNo">The PakNo of the officer related to the mission.</param>
        public void StoreMission(Mission mission, int PakNo)
        {
            int bin = mission.GetIsComplete() ? 1 : 0; // Convert boolean to binary representation
                                                       // Construct SQL query to insert mission into the database
            string query = string.Format("INSERT INTO Mission VALUES ('{0}', '{1}', {2}, {3}, (SELECT Id FROM AFPersonalle WHERE PakNo = {4}))", mission.GetDate(), mission.GetDetails(), bin, mission.GetSuccessRate(), PakNo);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a mission in the database.
        /// </summary>
        /// <param name="Date">The date of the mission to update.</param>
        /// <param name="mission">The updated mission information.</param>
        public void UpdateMission(DateTime Date, Mission mission)
        {
            int bin = mission.GetIsComplete() ? 1 : 0; // Convert boolean to binary representation
                                                       // Construct SQL query to update mission in the database
            string query = string.Format("UPDATE Mission SET Date = '{0}', Details = '{1}', IsComplete = {2}, SuccessRate = {3} WHERE Date = '{4}'", mission.GetDate(), mission.GetDetails(), bin, mission.GetSuccessRate(), Date);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a mission from the database based on its date.
        /// </summary>
        /// <param name="Date">The date of the mission to retrieve.</param>
        /// <returns>The mission corresponding to the provided date.</returns>
        public Mission GetMissionFromDate(DateTime Date)
        {
            Mission mission = new Mission();
            // Construct SQL query to select mission from the database based on date
            string query = string.Format("SELECT TOP 1 * FROM Mission WHERE Date = '{0}'", Date);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool bol = reader["IsComplete"].ToString() == "1"; // Convert binary to boolean representation
                                                                       // Set mission properties based on database values
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetIsComplete(bol);
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                }
                return mission;
            }
        }

        /// <summary>
        /// Deletes a mission from the database.
        /// </summary>
        /// <param name="mission">The mission to delete.</param>
        public void DeleteMission(Mission mission)
        {
            // Construct SQL query to delete mission from the database based on its properties
            string query = string.Format("DELETE FROM Mission WHERE Date = '{0}' AND Details = '{1}' AND IsComplete = {2} AND SuccessRate = {3}", mission.GetDate(), mission.GetDetails(), mission.GetIsComplete() ? 1 : 0, mission.GetSuccessRate());
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves all missions from the database.
        /// </summary>
        /// <returns>A list of all missions stored in the database.</returns>
        public List<Mission> GetAllMission()
        {
            // Construct SQL query to select all missions from the database
            string query = "SELECT * FROM Mission";
            List<Mission> missions = new List<Mission>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool bol = reader["IsComplete"].ToString() == "1"; // Convert binary to boolean representation
                    Mission mission = new Mission();
                    // Set mission properties based on database values
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetIsComplete(bol);
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    missions.Add(mission);
                }
                return missions;
            }
        }

        /// <summary>
        /// Retrieves all missions of a specific officer from the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the officer whose missions to retrieve.</param>
        /// <returns>A list of all missions of the specified officer.</returns>
        public List<Mission> GetAllMissionsOfSpecificOfficer(int PakNo)
        {
            // Construct SQL query to select missions of a specific officer from the database
            string query = "SELECT * FROM Mission WHERE OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = " + PakNo + ")";
            List<Mission> missions = new List<Mission>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool bol = reader["IsComplete"].ToString() == "1"; // Convert binary to boolean representation
                    Mission mission = new Mission();
                    // Set mission properties based on database values
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    mission.SetIsComplete(bol);
                    missions.Add(mission);
                }
                return missions;
            }
        }

    }
}
