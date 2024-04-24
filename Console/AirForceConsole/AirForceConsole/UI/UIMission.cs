using AirForceLibrary.BL;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceConsole.UI
{
    internal class UIMission
    {
        public static void ViewMissions()
        {
            Console.Clear(); // Clear the console
            ConsoleUtility.Header(); // Display the header
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set console text color
            Console.WriteLine("WELCOME " + ConnectionClass.GetCurrentGDP().GetRank() + " " + ConnectionClass.GetCurrentGDP().GetName());
            // Display ASCII art
            Console.WriteLine("                                                                                                            \r\n# #             # #                 ###      #   #           #              # #  #           #              \r\n# # ### # #     # #  ## # # ###     #   ###  #   #  ### # #     ##  ###     ###      ##  ##     ### ##   ## \r\n #  # # # #     ### # # # # ##      ##  # #  #   #  # # ###  #  # # # #     ###  #   #   #   #  # # # #  #  \r\n #  ### ###     # # ###  #  ###     #   ###  ##  ## ### ###  ## # #  ##     # #  ## ##  ##   ## ### # # ##  \r\n #              # #                 #                               ###     # #                             \r\n");
            List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo()); // Get all missions of the current officer
            Console.WriteLine("Date \t \t \t \t Details \t \t  Is Completed  \t \t Success Rate");
            foreach (Mission mission in missions)
            {
                Console.WriteLine(mission.ToString()); // Display mission details
            }
        }

        public static void CompleteMissions()
        {
            try
            {
                ViewMissions(); // Display missions
                Console.WriteLine("Enter The Date of the Mission You Want to Complete: ");
                DateTime Date = DateTime.Parse(Console.ReadLine()); // Read mission date
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date); // Get mission object
                Console.WriteLine("Details: " + mission.GetDetails()); // Display mission details
                Console.WriteLine("IsComplete: ");
                mission.SetIsComplete(bool.Parse(Console.ReadLine())); // Set mission completion status
                Console.WriteLine("SuccessRate: ");
                mission.SetSuccessRate(float.Parse(Console.ReadLine())); // Set mission success rate
                Interfaces.GetMissionInterface().UpdateMission(Date, mission); // Update mission
                Console.WriteLine("Mission Updated Successfully"); // Display success message
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Display exception message
            }
        }

        public static void EditMission()
        {
            try
            {
                ViewMissions(); // Display missions
                Console.WriteLine("Enter The Date of the Mission You Want to Complete: ");
                DateTime Date = DateTime.Parse(Console.ReadLine()); // Read mission date
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date); // Get mission object
                Console.WriteLine("Details: " + mission.GetDetails()); // Display mission details
                Console.WriteLine("IsComplete: " + mission.GetIsComplete()); // Display mission completion status
                Console.WriteLine("SuccessRate: " + mission.GetSuccessRate()); // Display mission success rate
                Console.WriteLine("Enter 1 if you want to set it as incomplete and 2 to change successRate");
                int op = int.Parse(Console.ReadLine()); // Read user option
                if (op == 1)
                {
                    mission.SetIsComplete(false); // Set mission as incomplete
                    mission.SetSuccessRate(0); // Reset success rate
                    Interfaces.GetMissionInterface().UpdateMission(Date, mission); // Update mission
                    Console.WriteLine("Mission Updated Successfully"); // Display success message
                }
                else if (op == 2)
                {
                    Console.WriteLine("Enter the SuccessRate: ");
                    mission.SetSuccessRate(float.Parse(Console.ReadLine())); // Set new success rate
                    Interfaces.GetMissionInterface().UpdateMission(Date, mission); // Update mission
                    Console.WriteLine("Mission Updated Successfully"); // Display success message
                }
                else
                {
                    Console.WriteLine("Incorrect option"); // Display error message for incorrect option
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Display exception message
            }
        }
    }

}
