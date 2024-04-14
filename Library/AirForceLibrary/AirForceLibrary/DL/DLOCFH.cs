using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLOCFH:IOC
    {
       private static readonly string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\Commanders.txt";

        /// <summary>
        /// Stores a Commanding Officer's information and updates the file.
        /// </summary>
        /// <param name="officers">The Commanding Officer to store.</param>
        public void StoreOC(CommandingOfficers officers)
        {
            // Store Commanding Officer information in the personnel database
            IAFPersonalle AFP = new DLAFPersonalleFH();
            AFPersonalle AF = new AFPersonalle(officers.GetName(), officers.GetRank(), officers.GetPakNo(), officers.GetPresentlyPosted());
            AFP.StoreAFPersonalle(AF);

            // Write Commanding Officer information to the file
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(officers.GetSquadron() + "," + officers.GetPakNo());
            }
        }

        /// <summary>
        /// Retrieves all Commanding Officers from the file.
        /// </summary>
        /// <returns>A list of Commanding Officers.</returns>
        public List<CommandingOfficers> GetAll()
        {
            List<CommandingOfficers> officers = new List<CommandingOfficers>();

            // Read Commanding Officer information from the file
            using (StreamReader reader = new StreamReader(path))
            {
                if (File.Exists(path))
                {
                    string record;
                    while ((record = reader.ReadLine()) != null)
                    {
                        string[] AllData = record.Split(',');
                        string Squad = AllData[0];
                        int PakNo = int.Parse(AllData[1]);

                        // Retrieve Commanding Officer's information from the personnel database
                        IAFPersonalle aFPersonalle = new DLAFPersonalleFH();
                        AFPersonalle A = aFPersonalle.GetAFPersonalleByID(PakNo);

                        // Retrieve Commanding Officer's subordinate pilots
                        IGDP Pilots = new DLGDPFH();
                        List<GDPilot> Unders = Pilots.GetAllUFofOC(PakNo);

                        // Create and populate Commanding Officer object
                        CommandingOfficers OC = new CommandingOfficers(A.GetName(), A.GetRank(), A.GetPakNo(), A.GetPresentlyPosted(), Squad);
                        if(Unders != null)
                        {
                            OC.SetUnderOff(Unders);
                        }
                       
                        officers.Add(OC);
                    }
                }
            }

            return officers;
        }

        /// <summary>
        /// Retrieves the Commanding Officer with the specified PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the Commanding Officer to retrieve.</param>
        /// <returns>The Commanding Officer object if found, otherwise null.</returns>
        public CommandingOfficers GetOCbyId(int PakNo)
        {
            // Iterate through all Commanding Officers
            foreach (CommandingOfficers OC in GetAll())
            {
                // Check if the Commanding Officer's PakNo matches the provided PakNo
                if (OC.GetPakNo() == PakNo)
                {
                    return OC; // Return the Commanding Officer
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
                        IAFPersonalle NewAF = new DLAFPersonalleFH();
                        NewAF.UpdateAFPersonalle(PakNo, AF);

                        // Update the Squadron and UnderOfficer properties of the Commanding Officer
                        OC.SetSquadron(NewOC.GetSquadron());
                        OC.SetUnderOff(NewOC.GetUnderOfficer());
                    }

                    // Write the Squadron and PakNo of the Commanding Officer to the file
                    writer.WriteLine(OC.GetSquadron() + "," + OC.GetPakNo());
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
                        continue;
                    }

                    // Write the Squadron and PakNo of the Commanding Officer to the file
                    writer.WriteLine(OC.GetSquadron() + "," + OC.GetPakNo());
                }
            }
        }

    }
}
