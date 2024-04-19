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
            int op=0;
            Console.Clear();
            ConsoleUtility.Header();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName());
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
                 op = int.Parse(Console.ReadLine());
               
                 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (op >= 1 && op < 10)
                return op;
            return -1;
        }
        public static void ViewFlyingHours()
        {
            Console.Clear();
            ConsoleUtility.Header();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName());
            if(ConnectionClass.GetCurrentGDP().GetSquadron() == "No 2 Minhas")
            {
                Console.WriteLine("JF- 17 FLYING HOURS: "+ConnectionClass.GetCurrentGDP().GetFlyingHours());
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
                Console.WriteLine("JF- 17 FLYING HOURS: " + ConnectionClass.GetCurrentGDP().GetFlyingHours());
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
                Console.Clear();
                ConsoleUtility.Header();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName());
                GDPilot G = ConnectionClass.GetCurrentGDP();
                if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 2 Minhas")
                {
                    Console.Write("JF- 17 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine()));
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 5 Falcons")
                {
                    Console.Write("F-16 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine()));
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 9 Griffins")
                {
                    Console.Write("F-16 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine()));
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 15 Cobras")
                {
                    Console.Write("Mirage FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine()));
                }
                else if (ConnectionClass.GetCurrentGDP().GetSquadron() == "No 27 Zarrars")
                {
                    Console.Write("JF- 17 FLYING HOURS: ");
                    G.SetFlyingHours(int.Parse(Console.ReadLine()));
                }
                else
                {
                    Console.Write("Your Jet Craft is not Included Yet");
                    G.SetFlyingHours(0);
                }
                Interfaces.GetGdpInterface().UpdateGDP(G.GetPakNo(), G);
                Console.WriteLine("Flying Hours Added SuccessFully");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
         
        }
        public static void EditFlyingHours()
        {
            try
            {
                ViewFlyingHours();
                GDPilot G = ConnectionClass.GetCurrentGDP();
                Console.Write("Enter new Flying Hours: ");
                G.SetFlyingHours(int.Parse(Console.ReadLine()));
                Interfaces.GetGdpInterface().UpdateGDP(G.GetPakNo(), G);
                Console.WriteLine("Flying Hours Updated SuccessFully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void Error()
        {
            Console.WriteLine("Option not found");
        }

    }
}
