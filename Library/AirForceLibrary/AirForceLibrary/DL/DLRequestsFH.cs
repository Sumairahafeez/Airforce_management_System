using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLRequestsFH:IRequest
    {
        public string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Requests.txt";
        /// <summary>
        /// Stores a request in the file.
        /// </summary>
        /// <param name="Request">The request to store.</param>
        public void StoreRequests(Requests Request)
        {
            // Open the file for appending and write request information
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(Request.GetRequestId() + "," + Request.GetContext() + "," + Request.GetStatus() + "," + Request.GetPakNo());
            }
        }

        /// <summary>
        /// Retrieves all requests from the file.
        /// </summary>
        /// <returns>A list of requests.</returns>
        public List<Requests> GetAllRequest()
        {
            List<Requests> Requests = new List<Requests>();
            // Open the file for reading
            using (StreamReader reader = new StreamReader(path))
            {
                string record;
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read each line of the file
                    while ((record = reader.ReadLine()) != null)
                    {
                        // Split the record into individual pieces of information
                        string[] splitRecord = record.Split(',');
                        int Id = int.Parse(splitRecord[0]);
                        string context = splitRecord[1];
                        string status = splitRecord[2];
                        int PakNo = int.Parse(splitRecord[3]);
                        // Create a request object and add it to the list
                        Requests req = new Requests(Id, context, PakNo);
                        req.SetStatus(status);
                        Requests.Add(req);
                    }
                }
            }
            return Requests;
        }

        /// <summary>
        /// Retrieves all requests of a specific officer from the file.
        /// </summary>
        /// <param name="PkNo">The PakNo of the officer whose requests to retrieve.</param>
        /// <returns>A list of requests of the specified officer.</returns>
        public List<Requests> GetRequestsOfSpecificOfficer(int PkNo)
        {
            // Get all requests from the file
            List<Requests> GetRequests = GetAllRequest();
            List<Requests> offRequest = new List<Requests>();
            // Iterate through each request
            foreach (Requests req in GetRequests)
            {
                // Check if the PakNo matches the officer ID
                if (req.GetPakNo() == PkNo)
                {
                    offRequest.Add(req); // Add the request to the list
                }
            }
            return offRequest;
        }

        /// <summary>
        /// Updates a request in the file.
        /// </summary>
        /// <param name="id">The ID of the request to update.</param>
        /// <param name="req">The updated request information.</param>
        public void UpdateRequests(int id, Requests req)
        {
            // Get all requests from the file
            List<Requests> requests = GetAllRequest();
            // Open the file for writing, overwriting existing content
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through each request
                foreach (Requests requests1 in requests)
                {
                    // Check if the request ID matches the given ID
                    if (requests1.GetRequestId() == id)
                    {
                        // Update request properties
                        requests1.SetContext(req.GetContext());
                        requests1.SetStatus(req.GetStatus());
                        requests1.SetRequestId(req.GetRequestId());
                    }
                    // Write the updated request information to the file
                    writer.WriteLine(requests1.GetRequestId() + "," + requests1.GetContext() + "," + requests1.GetStatus() + "," + requests1.GetPakNo());
                }
            }
        }

        /// <summary>
        /// Deletes a request from the file.
        /// </summary>
        /// <param name="id">The ID of the request to delete.</param>
        public void DeleteRequests(int id)
        {
            // Get all requests from the file
            List<Requests> requests = GetAllRequest();
            // Open the file for writing, overwriting existing content
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through each request
                foreach (Requests requests1 in requests)
                {
                    // Check if the request ID matches the given ID
                    if (requests1.GetRequestId() == id)
                    {
                        continue; // Skip writing this request to the file
                    }
                    // Write the request information to the file (excluding the one to delete)
                    writer.WriteLine(requests1.GetRequestId() + "," + requests1.GetContext() + "," + requests1.GetStatus() + "," + requests1.GetPakNo());
                }
            }
        }

    }
}
