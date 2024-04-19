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
            Console.Clear();
            ConsoleUtility.Header();
            Console.WriteLine("Respected " + ConnectionClass.GetCurrentOC());
            Console.WriteLine("OC Menu is not implemented on Console Please Work on Winform");
        }
    }

}
