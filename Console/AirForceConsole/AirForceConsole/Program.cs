using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AirForceConsole.UI;
using AirForceLibrary;
using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using EZInput;

namespace AirForceConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string AFPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
            string GDPPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\GDPilot.txt";
            string OCPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Commanders.txt";
            string MissionPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Mission.txt";
            string ReportPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Requests.txt";
            ConnectionClass.SetAFFile(AFPath);
            ConnectionClass.SetGDPFile(GDPPath);
            ConnectionClass.SetMissionFile(MissionPath);
            ConnectionClass.SetReportFile(ReportPath);
            ConnectionClass.SetOCFile(OCPath);
            ConsoleUtility.Header();
            ConsoleUtility.FirstPage();
            int choice = ConsoleUtility.DataBaseChoice();
            if (choice == 1)
            {
                ConnectionClass.SetIsUsingDB(true);
            }
            else if (choice == 2)
            {
                ConnectionClass.SetIsUsingDB(false);
            }
            bool isExit = false;
           
            while (isExit == false)
            {
                if (Keyboard.IsKeyPressed(Key.S))
                {
                    isExit = true;
                }
                ConsoleUtility.TakeSignIn();
                // Check if the provided credentials are valid for an IT user
                bool IsValid = Validations.IsValidIT(ConsoleUtility.name, ConsoleUtility.PakNo, ConsoleUtility.Password);

                // If the credentials are valid for an IT user
                if (IsValid)
                {
                    UIIT.MainPage();
                }
                else
                {
                    bool IsValidGDP = Validations.IsValidGDP(ConsoleUtility.name, ConsoleUtility.PakNo, ConsoleUtility.Password);
                    if (IsValidGDP)
                    {
                        GDPilot CurrentPilot = Interfaces.GetGdpInterface().GetGDPThroughPakNo(ConsoleUtility.PakNo);
                        if (CurrentPilot != null)
                        {
                            ConnectionClass.SetCurrentGDP(CurrentPilot);
                            int Option = UIGDP.MainMenu();
                            if (Option == 1)
                            {
                                UIMission.ViewMissions();
                            }

                        }

                    }
                }
                Console.WriteLine("Enter CAPS 'S' to Exit");
                if (Keyboard.IsKeyPressed(Key.S))
                {
                    isExit = true;
                }
                Console.ReadKey();
            }
           
        }





          
        }
    
}
