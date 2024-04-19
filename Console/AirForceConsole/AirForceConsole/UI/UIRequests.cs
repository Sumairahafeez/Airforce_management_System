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
            Console.Clear();
            ConsoleUtility.Header();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            GDPilot G = ConnectionClass.GetCurrentGDP();
            Console.WriteLine("WELCOME " + G.GetRank() + " " + G.GetName());
            List<Requests> request = Interfaces.GetRequestInterface().GetRequestsOfSpecificOfficer(G.GetPakNo());
            Console.WriteLine("RequestID \t \t Context \t \t Status");
            foreach(Requests requests in request)
            {
                Console.WriteLine(requests.ToString());
            }
        }
        public static void NewRequest()
        {
            try
            {
                ViewRequests();
                GDPilot G = ConnectionClass.GetCurrentGDP();
                Console.Write("Enter Your RequestId: ");
                int Id = int.Parse(Console.ReadLine());
                bool isValid = Validations.IsValidRequestId(Id, G.GetPakNo());
                if (!(isValid))
                {
                    Console.WriteLine("Enter the context of your Request: ");
                    string context = Console.ReadLine();
                    Requests New = new Requests(Id, context, G.GetPakNo());
                    Interfaces.GetRequestInterface().StoreRequests(New);
                }
                else
                {
                    Console.WriteLine("Request Id Already Exists");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           

        }
        public static void DeleteRequest()
        {
            try
            {
                ViewRequests();
                GDPilot G = ConnectionClass.GetCurrentGDP();
                Console.Write("Enter Your RequestId: ");
                int Id = int.Parse(Console.ReadLine());
                Requests Request = Validations.IsValidRequest(G.GetPakNo(),Id);
                if (Request != null)
                {
                    Interfaces.GetRequestInterface().DeleteRequests(Id);
                    Console.WriteLine("Request Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("Request Id Doesnot Exists");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
