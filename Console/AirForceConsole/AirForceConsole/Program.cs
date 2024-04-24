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
            
            // File paths for various data files
            string AFPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
            string GDPPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\GDPilot.txt";
            string OCPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Commanders.txt";
            string MissionPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Mission.txt";
            string ReportPath = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Requests.txt";

            // Setting file paths in the ConnectionClass
            ConnectionClass.SetAFFile(AFPath);
            ConnectionClass.SetGDPFile(GDPPath);
            ConnectionClass.SetMissionFile(MissionPath);
            ConnectionClass.SetReportFile(ReportPath);
            ConnectionClass.SetOCFile(OCPath);

            // Displaying header and first page of the application
            ConsoleUtility.Header();
            ConsoleUtility.FirstPage();
            
            // Getting user's choice for database usage
            int choice = ConsoleUtility.DataBaseChoice();
            if (choice == 1)
            {
                ConnectionClass.SetIsUsingDB(true);
            }
            else if (choice == 2)
            {
                ConnectionClass.SetIsUsingDB(false);
            }
            else if (choice == -1)
            {
                ConsoleUtility.Error();
            }
            bool isExit;

            // Main loop for the application
            while (!(Keyboard.IsKeyPressed(Key.Escape)))
            {
                if (Keyboard.IsKeyPressed(Key.Escape))
                {
                    break;
                }
                ConsoleUtility.TakeSignIn();
                // Checking if the provided credentials are valid for an IT user
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
                            int Option = 0;
                            // Loop for handling GDPilot's menu options
                            while (Option != 10)
                            {
                                ConnectionClass.SetCurrentGDP(CurrentPilot);
                                Option = UIGDP.MainMenu();
                                // Handling different options chosen by the GDPilot
                                if (Option == 1)
                                {
                                    UIMission.ViewMissions();
                                }
                                else if (Option == 2)
                                {
                                    UIMission.CompleteMissions();
                                }
                                // Similar handling for other options...
                                else if(Option == 3)
                                {
                                    UIMission.EditMission();
                                }
                                else if( Option == 4)
                                {
                                    UIGDP.ViewFlyingHours();
                                }
                                else if(Option == 5)
                                {
                                    UIGDP.CompleteFlyingHours();
                                }
                                else if(Option == 6)
                                {
                                    UIGDP.EditFlyingHours();
                                }
                                else if(Option == 7)
                                {
                                    UIRequests.ViewRequests();
                                }
                                else if(Option == 8)
                                {
                                    UIRequests.NewRequest();
                                }
                                else if(Option == 9)
                                {
                                    UIRequests.DeleteRequest();
                                }
                                Console.ReadKey();
                            }
                        }
                    }
                    else
                    {
                        bool IsOC = Validations.IsValidOC(ConsoleUtility.PakNo);
                        if (IsOC)
                        {
                            UICommandingOfficers.Menu();
                        }
                        else
                        {
                            ConsoleUtility.UserError();
                        }
                    }
                }
                Console.WriteLine("Enter ESC to Exit");
                if (Keyboard.IsKeyPressed(Key.Escape))
                {
                    break;
                }
                Console.ReadKey();
            }
        }
}

}
