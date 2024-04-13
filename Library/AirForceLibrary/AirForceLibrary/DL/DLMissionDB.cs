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
        public void StoreMission(Mission mission,int PakNo)
        {
            int bin;
            if(mission.GetIsComplete() == true)
            {
                bin = 1;
            }
            else
            {
                bin = 0;
            }
            string query = string.Format("INSERT INTO Mission Values('{0}','{1}',{2},{3},(SELECT Id From AFPersonalle Where PakNo = {4}))",mission.GetDate(),mission.GetDetails(),bin,mission.GetSuccessRate(),PakNo);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateMission(DateTime Date,Mission mission)
        {
            int bin;
            if (mission.GetIsComplete() == true)
            {
                bin = 1;
            }
            else
            {
                bin = 0;
            }
            string query = string.Format("UPDATE Mission Set Date = '{0}',Details = '{1}',IsComplete = {2}, SuccessRate = {3} WHERE Date='{4}'", mission.GetDate(), mission.GetDetails(), bin, mission.GetSuccessRate(),Date);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
              
                command.ExecuteNonQuery ();
            }
        }
        public Mission GetMissionFromDate(DateTime Date)
        {
            Mission mission = new Mission();
            string query = string.Format("SELECT TOP 1 * FROM Mission Where Date = '{0}'", Date);
            using(SqlConnection con = new SqlConnection (ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool bol;
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    if (reader["IsComplete"].ToString() == "1")
                    {
                        bol = true;
                    }
                    else
                    {
                        bol = false;
                    }
                    mission.SetIsComplete(bol);
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                }
                return mission;
            }
        }
        public void DeleteMission(Mission mission)
        {
            string query = string.Format("DELETE Mission WHERE Date = '{0}' AND  Details = '{1}' AND  IsComplete = {2} AND SuccessRate{3} "+mission.GetDate()+mission.GetDetails()+mission.GetIsComplete()+mission.GetSuccessRate());
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
                { bool bol;
                    Mission mission = new Mission();
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    if (reader["IsComplete"].ToString() == "1")
                    {
                        bol = true;
                    }
                    else
                    {
                        bol = false;
                    }
                    mission.SetIsComplete(bol);
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    missions.Add(mission);
                }
                return missions; 
            }
        }
        public List<Mission> GetAllMissionsOfSpecificOfficer(int PakNo)
        {
            string query = "SELECT * FROM Mission Where OffId = (SELECT Id FROM AFPersonalle WHERE PakNo = "+PakNo+")";
            List<Mission> missions = new List<Mission>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool bol;
                    Mission mission = new Mission();
                    mission.SetDate(DateTime.Parse(reader["Date"].ToString().ToString()));
                    mission.SetDetails(reader["Details"].ToString());
                    mission.SetSuccessRate(float.Parse(reader["SuccessRate"].ToString()));
                    if (reader["IsComplete"].ToString() == "1")
                    {
                        bol = true;
                    }
                    else
                    {
                        bol = false;
                    }
                    mission.SetIsComplete(bol);
                    missions.Add((mission) );
                }
                return missions;
            }
        }
    }
}
