using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForceLibrary.DL
{
    public class DLOCFH : IOC
    {

        private static string path = ConnectionClass.GetOCFile();
        public static List<CommandingOfficers> officers = new List<CommandingOfficers>();
        /// <summary>
        /// Stores a Commanding Officer's information and updates the file.
        /// </summary>
        /// <param name="officers">The Commanding Officer to store.</param>
        private static DLOCFH Instance;
        private DLOCFH() { }
        public static DLOCFH SetValidInstance()
        {
            if (Instance == null)
            {
                Instance = new DLOCFH();
            }
            return Instance;
        }
        public void StoreOC(CommandingOfficers officers)
        {
            // Store Commanding Officer information in the personnel database
            IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
            AFPersonalle AF = new AFPersonalle(officers.GetName(), officers.GetRank(), officers.GetPakNo(), officers.GetPresentlyPosted());
            AF.SetBranch(officers.GetBranch());
            AF.SetPassword(officers.GetPassword());
            AFP.StoreAFPersonalle(AF);

            // Write Commanding Officer information to the file
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(officers.GetName()+";"+officers.GetPakNo()+";"+officers.GetRank()+";"+officers.GetPresentlyPosted()+";"+officers.GetBranch()+";"+officers.GetPassword()+";"+officers.GetSquadron());
            }
        }
        private  void LoadList()
        {
            List<CommandingOfficers> OCs = new List<CommandingOfficers>();
           
            // Read Commanding Officer information from the file
            using (StreamReader reader = new StreamReader(path))
            {
                //if (File.Exists(path))
                {
                    string record;

                    while ((record = reader.ReadLine()) != null)
                    {
                        string[] AllData = record.Split(';');
                        string Name = AllData[0];
                        int PakNo = int.Parse(AllData[1]);
                        string Rank = AllData[2];
                        string Posting = AllData[3];
                        string Branch = AllData[4];
                        string Pass = AllData[5];
                        string Squad = AllData[6];

                        // Retrieve Commanding Officer's information from the personnel database
                        //IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
                        //AFPersonalle A = AFP.GetAFPersonalleByID(PakNo);
                        // Retrieve Commanding Officer's subordinate pilots
                        IGDP Pilots = DLGDPFH.SetValidInstance();
                        List<GDPilot> Unders = Pilots.GetAllUFofOC(PakNo);

                        // Create and populate Commanding Officer object


                        CommandingOfficers OC = new CommandingOfficers(Name, Rank, PakNo, Posting, Squad);
                        OC.SetBranch(Branch);
                        OC.SetPassword(Pass);
                        if (Unders != null)
                        {

                            OC.SetUnderOff(Unders);
                        }

                        OCs.Add(OC);
                    }
                }
                officers = OCs;
            }
        }
        /// <summary>
        /// Retrieves all Commanding Officers from the file.
        /// </summary>
        /// <returns>A list of Commanding Officers.</returns>
        public List<CommandingOfficers> GetAll()
        {
            LoadList();
            return officers;
        }

        /// <summary>
        /// Retrieves the Commanding Officer with the specified PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to retrieve.</param>
        /// <returns>The Commanding Officer object if found, otherwise null.</returns>
        public CommandingOfficers GetOCbyId(int PakNo)
        {
            IOC OC = DLOCFH.SetValidInstance();
            List<CommandingOfficers> Oc = OC.GetAll();
            // Iterate through all Commanding Officers
            foreach (CommandingOfficers OCs in Oc)
            {
                // Check if the Commanding Officer's PakNo matches the provided PakNo
                if (OCs.GetPakNo() == PakNo)
                {
                    return OCs; // Return the Commanding Officer
                }
            }

            // Return null if no matching Commanding Officer is found
            return null;
        }

        /// <summary>
        /// Updates the Commanding Officer information with the provided PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to update.</param>
        /// <param name="NewOC">The new Commanding Officer information.</param>
        public void UpdateOC(int PakNo, CommandingOfficers NewOC)
        {
            // Retrieve all Commanding Officers from the file
            List<CommandingOfficers> AllOcs = GetAll();

            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through all Commanding Officers
                foreach (CommandingOfficers OC in AllOcs)
                {
                    // Check if the Commanding Officer matches the provided PakNo
                    if (OC.GetPakNo() == PakNo)
                    {
                        // Create a new AFPersonalle instance with updated information
                        AFPersonalle AF = new AFPersonalle(NewOC.GetName(), NewOC.GetRank(), NewOC.GetPakNo(), NewOC.GetPresentlyPosted());
                        IAFPersonalle NewAF = DLAFPersonalleFH.SetValidInstance();
                        NewAF.UpdateAFPersonalle(PakNo, AF);

                        // Update the Squadron and UnderOfficer properties of the Commanding Officer
                        OC.SetName(NewOC.GetName());
                        OC.SetPakNo(NewOC.GetPakNo());
                        OC.SetRank(NewOC.GetRank());
                        OC.SetPresentlyPosted(NewOC.GetPresentlyPosted());
                        OC.SetBranch(NewOC.GetBranch());
                        OC.SetPassword(NewOC.GetPassword());
                        OC.SetSquadron(NewOC.GetSquadron());
                        OC.SetUnderOff(NewOC.GetUnderOfficer());
                    }

                    // Write the Squadron and PakNo of the Commanding Officer to the file
                    writer.WriteLine(OC.GetName() + ";" + OC.GetPakNo() + ";" + OC.GetRank() + ";" + OC.GetPresentlyPosted() + ";" + OC.GetBranch() + ";" + OC.GetPassword() + ";" + OC.GetSquadron());

                }
            }
        }

        /// <summary>
        /// Deletes a Commanding Officer from the file based on PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to delete.</param>
        public void DeleteOC(int PakNo)
        {
            // Retrieve all Commanding Officers from the file
            List<CommandingOfficers> AllOcs = GetAll();

            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through all Commanding Officers
                foreach (CommandingOfficers OC in AllOcs)
                {
                    // Skip the Commanding Officer with the matching PakNo
                    if (OC.GetPakNo() == PakNo)
                    {
                        //AFPersonalle AF = new AFPersonalle(NewOC.GetName(), NewOC.GetRank(), NewOC.GetPakNo(), NewOC.GetPresentlyPosted());
                        IAFPersonalle NewAF = DLAFPersonalleFH.SetValidInstance();
                        NewAF.DeleteAFPersonalle(PakNo);

                        continue;
                    }

                    // Write the Squadron and PakNo of the Commanding Officer to the file
                    writer.WriteLine(OC.GetName() + ";" + OC.GetPakNo() + ";" + OC.GetRank() + ";" + OC.GetPresentlyPosted() + ";" + OC.GetBranch() + ";" + OC.GetPassword() + ";" + OC.GetSquadron());

                }
            }
        }

    }
}
