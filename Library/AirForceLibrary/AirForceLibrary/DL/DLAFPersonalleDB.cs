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
        /// <summary>
        /// Stores an AFPersonalle in the database.
        /// </summary>
        /// <param name="a">The AFPersonalle to store.</param>
        public void StoreAFPersonalle(AFPersonalle a)
        {
            string query = string.Format("INSERT INTO AFPersonalle VALUES('{0}','{1}',{2},'{3}')", a.GetName(), a.GetRank(), a.GetPakNo(), a.GetPresentlyPosted());
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves all AFPersonalles from the database.
        /// </summary>
        /// <returns>A list of all AFPersonalles stored in the database.</returns>
        public List<AFPersonalle> GetAFPersonalles()
        {
            string query = "SELECT * FROM AFPersonalle";
            List<AFPersonalle> aFPersonalles = new List<AFPersonalle>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
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

        /// <summary>
        /// Retrieves an AFPersonalle by its PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to retrieve.</param>
        /// <returns>The AFPersonalle with the specified PakNo.</returns>
        public AFPersonalle GetAFPersonalleByID(int PakNo)
        {
            string query = "SELECT * FROM AFPersonalle WHERE PakNo = " + PakNo;
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    string Present = reader["PresentlyPosted"].ToString();
                    AFPersonalle A = new AFPersonalle(name, Rank, PakNo, Present);
                    return A;
                }
                return null;
            }
        }

        /// <summary>
        /// Updates an AFPersonalle in the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to update.</param>
        /// <param name="a">The updated AFPersonalle information.</param>
        public void UpdateAFPersonalle(int PakNo, AFPersonalle a)
        {
            string query = string.Format("UPDATE AFPersonalle SET Name = '{0}', Rank = '{1}', PresentlyPosted = '{2}' WHERE PakNo = {3}", a.GetName(), a.GetRank(), a.GetPresentlyPosted(), a.GetPakNo());
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cd = new SqlCommand(query, con);
                cd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a specific AFPersonalle from the database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to delete.</param>
        public void DeleteAFPersonalle(int PakNo)
        {
            string query = "DELETE FROM AFPersonalle WHERE PakNo = " + PakNo;
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /*
        // this function is used to give af personalles that work under an oc
        public List<AFPersonalle> GetAllAFofOC(int OCId)
        {   List<AFPersonalle> list = new List<AFPersonalle> ();
            string query = string.Format("SELECT * FROM AFPersonalle WHERE Id = (SELECT OfficerId FROM GDP Where OCId = (SELECT o.Id FROM OC o,AFPersonalle a WHERE o.OffId = a.Id AND a.PakNo ={0}))", OCId);
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string Rank = reader["Rank"].ToString();
                    int PakNo = int.Parse(reader["PakNo"].ToString());
                    string Present = reader["PresentlyPosted"].ToString();
                    AFPersonalle A = new AFPersonalle(name, Rank, PakNo, Present);
                    list.Add(A);
                }
                return list;
            }
        }
        */
    }
}
