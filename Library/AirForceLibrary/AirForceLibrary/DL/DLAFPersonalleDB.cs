using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLAFPersonalleDB:IAFPersonalle
    {   //This class will store AFPersonalles
        public  void StoreAFPersonalle(AFPersonalle a)
        {
            string query = string.Format("INSERT INTO AFPersonalle VALUES('{0}','{1}',{2},'{3}')",a.GetName(),a.GetRank(),a.GetPakNo(),a.GetPresentlyPosted());
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
               
                cmd.ExecuteNonQuery ();
            }
        }
        //This fuction will get all th afpersonalle
        public  List<AFPersonalle> GetAFPersonalles()
        {
            string query = "SELECT * FROM AFPersonalle";
            List<AFPersonalle> aFPersonalles = new List<AFPersonalle>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AFPersonalle a = new AFPersonalle();
                    a.SetName(reader["Name"].ToString());
                    a.SetRank(reader["Rank"].ToString());
                    a.SetPresentlyPosted(reader["PresentlyPosted"].ToString());
                    a.SetPakNo(int.Parse(reader["PakNo"].ToString()));
                    aFPersonalles.Add(a);
                }
                return aFPersonalles;
            }
        }
        //This Function will give only those AFPersonals whose id are given
        public  AFPersonalle GetAFPersonalleByID(int  PakNo)
        {
            string query = "SELECT * FROM AFPersonalle WHERE PakNo = " + PakNo;
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open ();
                SqlCommand cmd = new SqlCommand (query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    
                    string Present = reader["PresentlyPosted"].ToString() ;
                    AFPersonalle A = new AFPersonalle(name,Rank,PakNo,Present);
                    return A;
                }
                return null;
            }
        }
        //This Function will Update afpersonalles
        public  void UpdateAFPersonalle(int PakNo,AFPersonalle a)
        {
            string query =string.Format("UPDATE AFPersonalle SET Name = '{0}',Rank = '{1}',PresentlyPosted = '{2}' WHERE PakNo = {3}",a.GetName(),a.GetRank(),a.GetPresentlyPosted(),a.GetPakNo() );
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cd = new SqlCommand(query,con);
                cd.ExecuteNonQuery();
            }
        }
        //This Function will delete a specific AFPersonalle
        public  void DeleteAFPersonalle(int PakNo)
        {
            string query = "DELETE AFPersonalle WHERE PakNo = " + PakNo;
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
