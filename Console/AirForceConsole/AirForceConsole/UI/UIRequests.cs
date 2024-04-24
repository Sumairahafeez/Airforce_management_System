using AirForceLibrary.BL;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirForceConsole.UI
{
    internal class UIRequests
    {
        public static void ViewRequests()
        {
            Console.Clear(); // Clear the console
            ConsoleUtility.Header(); // Display the header
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set console text color
            GDPilot G = ConnectionClass.GetCurrentGDP(); // Get current GDP officer
            Console.WriteLine("WELCOME " + G.GetRank() + " " + G.GetName());
            List<Requests> request = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(G.GetPakNo()); // Get requests of specific officer
            Console.WriteLine("RequestID \t \t Context \t \t Status");
            foreach (Requests requests in request)
            {
                Console.WriteLine(requests.ToString()); // Display request details
            }
        }

        public static void NewRequest()
        {
            try
            {
                ViewRequests(); // Display requests
                GDPilot G = ConnectionClass.GetCurrentGDP(); // Get current GDP officer
                Console.Write("Enter Your RequestId: ");
                int Id = int.Parse(Console.ReadLine()); // Read request ID
                bool isValid = Validations.IsValidRequestId(Id, G.GetPakNo()); // Check if request ID is valid
                if (!(isValid))
                {
                    Console.WriteLine("Enter the context of your Request: ");
                    string context = Console.ReadLine(); // Read request context
                    Requests New = new Requests(Id, context, G.GetPakNo()); // Create new request object
                    Interfaces.GetRequestInterface().StoreRequests(New); // Store new request
                }
                else
                {
                    Console.WriteLine("Request Id Already Exists"); // Display error message for duplicate request ID
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); // Display exception message
            }
        }

        public static void DeleteRequest()
        {
            try
            {
                ViewRequests(); // Display requests
                GDPilot G = ConnectionClass.GetCurrentGDP(); // Get current GDP officer
                Console.Write("Enter Your RequestId: ");
                int Id = int.Parse(Console.ReadLine()); // Read request ID
                Requests Request = Validations.IsValidRequest(G.GetPakNo(), Id); // Validate request ID
                if (Request != null)
                {
                    Interfaces.GetRequestInterface().DeleteRequests(Id); // Delete request
                    Console.WriteLine("Request Deleted Successfully"); // Display success message
                }
                else
                {
                    Console.WriteLine("Request Id Doesnot Exists"); // Display error message for non-existing request ID
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Display exception message
            }
        }
    }

}
