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
    public class DLRequestsDB:IRequest
    {   //This function will return all th erequests of all the personalle
        /// <summary>
        /// Retrieves all requests from the database.
        /// </summary>
        /// <returns>A list of all requests stored in the database.</returns>
        private static DLRequestsDB instance;
        private DLRequestsDB() { }
        public static DLRequestsDB SetValidInstance()
        {
            if(instance == null)
            {
                instance = new DLRequestsDB();
            }
            return instance;
        }
        public List<Requests> GetAllRequest()
        {
            string query = "SELECT * FROM Request";
            List<Requests> requests = new List<Requests>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Requests request = new Requests();
                    // Set request properties based on database values
                    request.SetRequestId(int.Parse(reader["Id"].ToString()));
                    request.SetContext(reader["Context"].ToString());
                    request.SetStatus(reader["Status"].ToString());
                    request.SetPakNo(int.Parse(reader["OfficerId"].ToString()));
                    requests.Add(request);
                }
                return requests;
            }
        }

        /// <summary>
        /// Retrieves requests of a specific officer based on their PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the officer whose requests to retrieve.</param>
        /// <returns>A list of requests belonging to the specified officer.</returns>
        public List<Requests> GetRequestsOfSpecificOfficer(int PakNo)
        {
            string query = "SELECT * FROM Request WHERE OfficerId = " + PakNo;
            List<Requests> requests = new List<Requests>();
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Requests request = new Requests();
                    // Set request properties based on database values
                    request.SetRequestId(int.Parse(reader["Id"].ToString()));
                    request.SetContext(reader["Context"].ToString());
                    request.SetStatus(reader["Status"].ToString());
                    request.SetPakNo(int.Parse(reader["OfficerId"].ToString()));
                    requests.Add(request);
                }
                return requests;
            }
        }

        /// <summary>
        /// Stores a request in the database.
        /// </summary>
        /// <param name="request">The request to store.</param>
        public void StoreRequests(Requests request)
        {
            // Construct SQL query to insert request into the database
            string query = string.Format("INSERT INTO Request VALUES({0}, {1}, '{2}', '{3}')", request.GetRequestId(), request.GetPakNo(), request.GetContext(), request.GetStatus());
            using (SqlConnection co = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                co.Open();
                SqlCommand cmd = new SqlCommand(query, co);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a request in the database.
        /// </summary>
        /// <param name="RequestId">The ID of the request to update.</param>
        /// <param name="request">The updated request information.</param>
        public void UpdateRequests(int RequestId, Requests request)
        {
            // Construct SQL query to update request in the database
            string query = string.Format("UPDATE Request SET Context = '{0}', Status = '{1}' WHERE Id = {2}", request.GetContext(), request.GetStatus(), RequestId);
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a request from the database.
        /// </summary>
        /// <param name="RequestId">The ID of the request to delete.</param>
        public void DeleteRequests(int RequestId)
        {
            // Construct SQL query to delete request from the database
            string query = "DELETE FROM Request WHERE Id = " + RequestId;
            using (SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
