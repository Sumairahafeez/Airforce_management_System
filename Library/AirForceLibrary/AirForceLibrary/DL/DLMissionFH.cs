using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLMissionFH:IMission
    {
        //string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Mission.txt";
        /// <summary>
        /// Stores a mission in the file.
        /// </summary>
        /// <param name="mission">The mission to store.</param>
        /// <param name="PakNo">The PakNo associated with the mission.</param>
        string path = ConnectionClass.GetMissionFile();
        public void StoreMission(Mission mission, int PakNo)
        {
            // Open the file for appending and write mission information
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(PakNo + "," + mission.GetDate() + "," + mission.GetDetails() + "," + mission.GetIsComplete() + "," + mission.GetSuccessRate());
            }
        }

        /// <summary>
        /// Retrieves all missions from the file.
        /// </summary>
        /// <returns>A list of missions.</returns>
        public List<Mission> GetAllMission()
        {
            List<Mission> missions = new List<Mission>();
            // Open the file for reading
            using (StreamReader reader = new StreamReader(path))
            {
                string record;
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read each line of the file
                    while ((record = reader.ReadLine()) != null)
                    {
                        // Split the record into individual pieces of information
                        string[] AllData = record.Split(',');
                        int PakNo = int.Parse(AllData[0]);
                        DateTime date = DateTime.Parse(AllData[1]);
                        string details = AllData[2];
                        bool isComplete = bool.Parse(AllData[3]);
                        float Success = float.Parse(AllData[4]);
                        // Create a mission object and add it to the list
                        Mission mission = new Mission(date, details);
                        mission.SetIsComplete(isComplete);
                        mission.SetSuccessRate(Success);
                        missions.Add(mission);
                    }
                }
            }
            return missions;
        }

        /// <summary>
        /// Retrieves all missions of a specific officer from the file.
        /// </summary>
        /// <param name="officerId">The ID of the officer whose missions to retrieve.</param>
        /// <returns>A list of missions of the specified officer.</returns>
        public List<Mission> GetAllMissionsOfSpecificOfficer(int officerId)
        {
            List<Mission> missions = new List<Mission>();
            // Open the file for reading
            using (StreamReader reader = new StreamReader(path))
            {
                string record;
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read each line of the file
                    while ((record = reader.ReadLine()) != null)
                    {
                        // Split the record into individual pieces of information
                        string[] AllData = record.Split(',');
                        int PakNo = int.Parse(AllData[0]);
                        // Check if the PakNo matches the officer ID
                        if (PakNo == officerId)
                        {
                            DateTime date = DateTime.Parse(AllData[1]);
                            string details = AllData[2];
                            bool isComplete = bool.Parse(AllData[3]);
                            float Success = float.Parse(AllData[4]);
                            // Create a mission object and add it to the list
                            Mission mission = new Mission(date, details);
                            mission.SetIsComplete(isComplete);
                            mission.SetSuccessRate(Success);
                            missions.Add(mission);
                        }
                    }
                }
            }
            return missions;
        }

        /// <summary>
        /// Retrieves a mission from the file by its date.
        /// </summary>
        /// <param name="date">The date of the mission to retrieve.</param>
        /// <returns>The mission with the specified date.</returns>
        public Mission GetMissionFromDate(DateTime date)
        {
            // Open the file for reading
            using (StreamReader reader = new StreamReader(path))
            {
                string record;
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read each line of the file
                    while ((record = reader.ReadLine()) != null)
                    {
                        // Split the record into individual pieces of information
                        string[] AllData = record.Split(',');
                        int PakNo = int.Parse(AllData[0]);
                        DateTime date1 = DateTime.Parse(AllData[1]);
                        // Check if the dates match
                        if (date1 == date)
                        {
                            string details = AllData[2];
                            bool isComplete = bool.Parse(AllData[3]);
                            float Success = float.Parse(AllData[4]);
                            // Create and return the mission object
                            Mission mission = new Mission(date, details);
                            mission.SetIsComplete(isComplete);
                            mission.SetSuccessRate(Success);
                            return mission;
                        }
                    }
                }
            }
            return null; // Return null if the mission is not found
        }

        /// <summary>
        /// Updates an existing mission in the file.
        /// </summary>
        /// <param name="date">The date of the mission to update.</param>
        /// <param name="updatedMission">The updated mission information.</param>
        public void UpdateMission(DateTime date, Mission updatedMission)
        {
            // Get all missions from the file
            List<Mission> missions = GetAllMission();
            // Get all PakNos
            List<int> PakNos = GetAllPakNos();
            // Open the file for writing, overwriting existing content
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through each mission
                for (int i = 0; i < missions.Count; i++)
                {
                    // Check if the date matches the mission's date
                    if (missions[i].GetDate() == date)
                    {
                        // Update mission properties
                        missions[i].SetIsComplete(updatedMission.GetIsComplete());
                        missions[i].SetDetails(updatedMission.GetDetails());
                        missions[i].SetSuccessRate(updatedMission.GetSuccessRate());
                    }
                    // Write mission information to the file
                    writer.WriteLine(PakNos[i] + "," + missions[i].GetDate() + "," + missions[i].GetDetails() + "," + missions[i].GetIsComplete() + "," + missions[i].GetSuccessRate());
                }
            }
        }

        /// <summary>
        /// Retrieves all PakNos from the file.
        /// </summary>
        /// <returns>A list of PakNos.</returns>
        public List<int> GetAllPakNos()
        {
            List<int> PakNos = new List<int>();
            // Open the file for reading
            using (StreamReader reader = new StreamReader(path))
            {
                string record;
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read each line of the file
                    while ((record = reader.ReadLine()) != null)
                    {
                        // Split the record into individual pieces of information and add the PakNo to the list
                        string[] AllData = record.Split(',');
                        int PakNo = int.Parse(AllData[0]);
                        PakNos.Add(PakNo);
                    }
                }
            }
            return PakNos;
        }

        /// <summary>
        /// Deletes a mission from the file.
        /// </summary>
        /// <param name="mission">The mission to delete.</param>
        public void DeleteMission(Mission mission)
        {
            // Get all missions from the file
            List<Mission> missions = GetAllMission();
            // Get all PakNos
            List<int> PakNos = GetAllPakNos();
            // Open the file for writing, overwriting existing content
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through each mission
                for (int i = 0; i < missions.Count; i++)
                {
                    // Compare mission properties to find the one to delete
                    if (missions[i].GetDate() == mission.GetDate() &&
                        mission.GetDetails() == missions[i].GetDetails() &&
                        mission.GetIsComplete() == missions[i].GetIsComplete() &&
                        mission.GetSuccessRate() == missions[i].GetSuccessRate())
                    {
                        // If properties match, skip writing this mission to the file
                        PakNos.Remove(PakNos[i]);
                        continue;
                    }
                    // Write the mission to the file (excluding the one to delete)
                    writer.WriteLine(PakNos[i] + "," + missions[i].GetDate() + "," + missions[i].GetDetails() + "," + missions[i].GetIsComplete() + "," + missions[i].GetSuccessRate());
                }
            }
        }


    }
}
