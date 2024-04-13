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
            string query = "SELECT * FROM Request WHERE OfficerId = " + PakNo;
            List<Requests> requests = new List<Requests>();
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
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
            string query = string.Format("INSERT INTO Request VALUES( {0},{1},'{2}','{3}')",request.GetRequestId(),request.GetPakNo(),request.GetContext(),request.GetStatus());
            using(SqlConnection co = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                co.Open();
                SqlCommand cmd = new SqlCommand(query, co);
                cmd.ExecuteNonQuery();
            }
        }
        //This function will be used bu both ends officer as well as commander
        public void UpdateRequests(int RequestId,Requests request)
        {
            string query = string.Format("UPDATE Request SET Context = '{0}', Status = '{1}'", request.GetContext(), request.GetStatus());
            using(SqlConnection con = new SqlConnection(ConnectionClass.ConnectionStr))
            {
                con.Open();
                SqlCommand cmdm = new SqlCommand(query, con);
                
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
