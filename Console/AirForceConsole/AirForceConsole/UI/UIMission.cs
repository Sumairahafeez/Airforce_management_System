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
            Console.Clear();
            ConsoleUtility.Header();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("WELCOME "+ConnectionClass.GetCurrentGDP().GetRank()+" "+ConnectionClass.GetCurrentGDP().GetName());
            Console.WriteLine("                                                                                                            \r\n# #             # #                 ###      #   #           #              # #  #           #              \r\n# # ### # #     # #  ## # # ###     #   ###  #   #  ### # #     ##  ###     ###      ##  ##     ### ##   ## \r\n #  # # # #     ### # # # # ##      ##  # #  #   #  # # ###  #  # # # #     ###  #   #   #   #  # # # #  #  \r\n #  ### ###     # # ###  #  ###     #   ###  ##  ## ### ###  ## # #  ##     # #  ## ##  ##   ## ### # # ##  \r\n #              # #                 #                               ###     # #                             \r\n");
            List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());
            Console.WriteLine("Date \t \t \t \t Details \t \t  Is Completed  \t \t Success Rate");
            foreach(Mission mission in missions)
            {
                Console.WriteLine(mission.ToString());
            }
        }
        public static void CompleteMissions()
        {
            try
            {
                ViewMissions();
                Console.WriteLine("Enter The Date of the Mission You Want to Complete: ");
                DateTime Date = DateTime.Parse(Console.ReadLine());
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date);
                Console.WriteLine("Details: " + mission.GetDetails());
                Console.WriteLine("IsComplete: ");
                mission.SetIsComplete(bool.Parse(Console.ReadLine()));
                Console.WriteLine("SuccessRate: ");
                mission.SetSuccessRate(float.Parse(Console.ReadLine()));
                Interfaces.GetMissionInterface().UpdateMission(Date, mission);
                Console.WriteLine("Mission Updated Successfully");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        public static void EditMission()
        {
            try
            {
                ViewMissions();
                Console.WriteLine("Enter The Date of the Mission You Want to Complete: ");
                DateTime Date = DateTime.Parse(Console.ReadLine());
                Mission mission = Interfaces.GetMissionInterface().GetMissionFromDate(Date);
                Console.WriteLine("Details: " + mission.GetDetails());
                Console.WriteLine("IsComplete: "+mission.GetIsComplete());
                Console.WriteLine("SuccessRate: "+mission.GetSuccessRate());
                Console.WriteLine("Enter 1 if you want to set it as incomplete and 2 to change successRate");
                int op =int.Parse(Console.ReadLine());
                if(op ==1)
                {
                    mission.SetIsComplete(false);
                    mission.SetSuccessRate(0);
                    Interfaces.GetMissionInterface().UpdateMission(Date,mission);
                    Console.WriteLine("Mission Updated Successfully");
                }
                else if(op ==2)
                {
                    Console.WriteLine("Enter the SuccessRate: ");
                    mission.SetSuccessRate(float.Parse(Console.ReadLine()));
                    Interfaces.GetMissionInterface() .UpdateMission(Date,mission);
                    Console.WriteLine("Mission Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Incorrect option");
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
