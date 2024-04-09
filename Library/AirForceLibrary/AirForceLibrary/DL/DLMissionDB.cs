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
    public class DLMissionDB:IMission
    {
        public void StoreMission(Mission mission,int PakNo)
        {
            string query = "INSERT INTO Mission Values(@Date,@Details,(SELECT Id From AFPersonalle Where PakNo = "+PakNo+")";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Date",mission.GetDate());
                cmd.Parameters.AddWithValue("@Details",mission.GetDetails());
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateMission(DateTime Date,Mission mission)
        {
            string query = "UPDATE Mission Set(Date = @Date,Details = @Details,IsComplete = @IsComp, SuccessRate = @Success) WHERE (SELECT Top 1 Id FROM Mission WHERE Date = Date)";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Date",mission.GetDate());
                command.Parameters.AddWithValue("@Detail",mission.GetDetails());
                command.Parameters.AddWithValue("@IsComp",mission.GetIsComplete());
                command.Parameters.AddWithValue("@Success",mission.GetSuccessRate());
                command.ExecuteNonQuery ();
            }
        }
        public Mission GetMissionFromDate(DateTime Date)
        {
            Mission mission = new Mission();
            string query = string.Format("SELECT TOP 1 * FROM Mission Where Date = {0}", Date);
            using(SqlConnection con = new SqlConnection (ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetIsComplete(bool.Parse(reader["IsComplete"].ToString()));
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                }
                return mission;
            }
        }
        public void DeleteMission(Mission mission)
        {
            string query = string.Format("DELETE Mission WHERE Date = {0} AND  Details = {1} AND  IsComplete = {2} AND SuccessRate{3} "+mission.GetDate()+mission.GetDetails()+mission.GetIsComplete()+mission.GetSuccessRate());
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Mission> GetAllMission()
        {
            string query = "SELECT * FROM Mission";
            List<Mission> missions = new List<Mission>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mission mission = new Mission();
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetIsComplete(bool.Parse(reader["IsComplete"].ToString()));
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    missions.Add(mission);
                }
                return missions; 
            }
        }
        public List<Mission> GetAllMissionsOfSpecificOfficer(int officerId)
        {
            string query = "SELECT * FROM Mission Where OffId = "+officerId;
            List<Mission> missions = new List<Mission>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mission mission = new Mission();
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString().ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    mission.SetIsComplete(bool.Parse(reader["IsComplete"].ToString()));
                    missions.Add((mission) );
                }
                return missions;
            }
        }
    }
}
