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
            Console.Clear();
            ConsoleUtility.Header();
            Console.ForegroundColor = ConsoleColor.White;
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
            int op = int.Parse(Console.ReadLine());
            return op;
        }
    }
}
