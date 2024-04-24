using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirForceConsole.UI
{
    internal class UICommandingOfficers
    {
        public static void Menu()
        {
            Console.Clear(); // Clear the console
            ConsoleUtility.Header(); // Display the header

            // Display a message with the current operational command
            Console.WriteLine("Respected " + ConnectionClass.GetCurrentOC());

            // Inform the user that the OC menu is not implemented in the console
            Console.WriteLine("OC Menu is not implemented on Console. Please Work on Winform.");
        }

    }

}
