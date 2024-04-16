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
            Console.WriteLine("WELCOME "+ConnectionClass.GetCurrentGDP().GetRank()+" "+ConnectionClass.GetCurrentGDP().GetName());
            Console.WriteLine("                                                                                                            \r\n# #             # #                 ###      #   #           #              # #  #           #              \r\n# # ### # #     # #  ## # # ###     #   ###  #   #  ### # #     ##  ###     ###      ##  ##     ### ##   ## \r\n #  # # # #     ### # # # # ##      ##  # #  #   #  # # ###  #  # # # #     ###  #   #   #   #  # # # #  #  \r\n #  ### ###     # # ###  #  ###     #   ###  ##  ## ### ###  ## # #  ##     # #  ## ##  ##   ## ### # # ##  \r\n #              # #                 #                               ###     # #                             \r\n");
            List<Mission> missions = Interfaces.GetMissionInterface().GetAllMissionsOfSpecificOfficer(ConnectionClass.GetCurrentGDP().GetPakNo());
            Console.WriteLine("Date \\t Details \\t \\t \\t Is Completed \\t Success Rate");
            foreach(Mission mission in missions)
            {
                Console.WriteLine(mission.ToString());
            }
        }
    }
}
