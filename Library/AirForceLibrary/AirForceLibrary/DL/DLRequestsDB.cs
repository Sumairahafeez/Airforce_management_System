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
        public List<Requests> GetAllRequest()
        {
            string query = "SELECT * FROM Request";
            List<Requests> requests = new List<Requests>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Requests request = new Requests();

                    request.SetRequestId(int.Parse(reader["Id"].ToString()));
                    request.SetContext(reader["Context"].ToString());
                    request.SetStatus(reader["Status"].ToString());
                    request.SetPakNo(int.Parse(reader["OfficerId"].ToString()));
                    requests.Add(request);
                }
                return requests;
            }
        }
        //This function returns requests of a specific Officer Taking its PakNo
        public List<Requests> GetRequestsOfSpecificOfficer(int PakNo)
        {
            string query = "SELECT * FROM Request WHERE PakNo = " + PakNo;
            List<Requests> requests = new List<Requests>();
            using(SqlConnection con = new SqlConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Requests request = new Requests();
                    request.SetRequestId(int.Parse(reader["Id"].ToString()));
                    request.SetContext(reader["Context"].ToString());
                    request.SetStatus(reader["Status"].ToString());
                    request.SetPakNo(int.Parse(reader["OfficerId"].ToString()));
                    requests.Add(request);
                }
                return requests;
            }
        }
        //This function will store request everytime a new request is made
        public void StoreRequests(Requests request)
        {
            string query = "INSERT INTO Request VALUE(@Id,@PakNo,@Context,@Status)";
            using(SqlConnection co = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                co.Open();
                SqlCommand cmd = new SqlCommand(query, co);
                cmd.Parameters.AddWithValue("@Id", request.GetRequestId());
                cmd.Parameters.AddWithValue("@PakNo", request.GetPakNo());
                cmd.Parameters.AddWithValue("@con", request.GetContext());
                cmd.Parameters.AddWithValue("@Status", request.GetStatus());
                cmd.ExecuteNonQuery();
            }
        }
        //This function will be used bu both ends officer as well as commander
        public void UpdateRequests(int RequestId,Requests request)
        {
            string query = "UPDATE Request SET (Id = @Id,OficerId = @PakNo,Context = @con, Status = @Status)";
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmdm = new SqlCommand(query, con);
                cmdm.Parameters.AddWithValue("@Id",request.GetRequestId());
                cmdm.Parameters.AddWithValue("@PakNo",request.GetPakNo());
                cmdm.Parameters.AddWithValue("@con",request.GetContext());
                cmdm.Parameters.AddWithValue("@Status",request.GetStatus());
                cmdm.ExecuteNonQuery();
            }
        }
        //This Function is used to delete all requests
        public void DeleteRequests(int RequestId)
        {
            string query = "DELETE Request WHERE Id = " + RequestId;
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
