using AirForceLibrary.BL;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirForceConsole.UI
{
    internal class UIGDP
    {
        public static int MainMenu()
        {
            int op = 0;
            Console.Clear(); // Clear the console
            ConsoleUtility.Header(); // Display the header
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set console text color

            // Display welcome message with GDP rank and name
            Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName());

            // Display menu options
            Console.WriteLine("1. View Missions");
            Console.WriteLine("2. Complete Missions");
            Console.WriteLine("3. Edit Missions");
            Console.WriteLine("4. View FlyingHours");
            Console.WriteLine("5. Complete Flying Hours");
            Console.WriteLine("6. Edit Flying Hours");
            Console.WriteLine("7. View Your Requests");
            Console.WriteLine("8. Write New Request");
            Console.WriteLine("9. Delete Request");

            try
            {
                op = int.Parse(Console.ReadLine()); // Read user input for menu choice
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Display any exception message
            }

            if (op >= 1 && op < 10)
                return op; // Return valid menu choice
            return -1; // Return -1 for invalid choice
        }

        public static void ViewFlyingHours()
        {
            Console.Clear(); // Clear the console
            ConsoleUtility.Header(); // Display the header
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set console text color
            Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName()); // Display welcome message with GDP rank and name

            // Display flying hours based on squadron
            if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 2 Minhas")
            {
                Console.WriteLine("JF-17 FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
            }
            else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 5 Falcons")
            {
                Console.WriteLine("F-16 FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
            }
            else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 9 Griffins")
            {
                Console.WriteLine("F-16 FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
            }
            else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 15 Cobras")
            {
                Console.WriteLine("Mirage FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
            }
            else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 27 Zarrars")
            {
                Console.WriteLine("JF-17 FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
            }
            else
            {
                Console.WriteLine("Your Jet Craft is not Included Yet");
            }
        }

        public static void CompleteFlyingHours()
        {
            try
            {
                Console.Clear(); // Clear the console
                ConsoleUtility.Header(); // Display the header
                Console.ForegroundColor = ConsoleColor.DarkBlue; // Set console text color
                Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName()); // Display welcome message with GDP rank and name
                GDPilot G = ConnectionClass.GetCurrentGDP(); // Get current GDPilot object

                // Display appropriate message based on squadron
                if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 2 Minhas")
                {
                    Console.Write("JF-17 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set flying hours
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 5 Falcons")
                {
                    Console.Write("F-16 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set flying hours
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 9 Griffins")
                {
                    Console.Write("F-16 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set flying hours
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 15 Cobras")
                {
                    Console.Write("Mirage FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set flying hours
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 27 Zarrars")
                {
                    Console.Write("JF-17 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set flying hours
                }
                else
                {
                    Console.Write("Your Jet Craft is not Included Yet");
                    G.SetFlyingHours(0); // Set flying hours to 0 if squadron not recognized
                }

                Interfaces.GetGdpInterface().UpdateGDP(G.GetPakNo(), G); // Update GDPilot object
                Console.WriteLine("Flying Hours Added Successfully"); // Display success message
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); // Display exception message
            }
        }

        public static void EditFlyingHours()
        {
            try
            {
                ViewFlyingHours(); // Display flying hours
                GDPilot G = ConnectionClass.GetCurrentGDP(); // Get current GDPilot object
                Console.Write("Enter new Flying Hours: ");
                G.SetFlyingHours(int.Parse(Console.ReadLine())); // Set new flying hours
                Interfaces.GetGdpInterface().UpdateGDP(G.GetPakNo(), G); // Update GDPilot object
                Console.WriteLine("Flying Hours Updated Successfully"); // Display success message
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); // Display exception message
            }
        }

        public static void Error()
        {
            Console.WriteLine("Option not found"); // Display error message
        }
    }

}
