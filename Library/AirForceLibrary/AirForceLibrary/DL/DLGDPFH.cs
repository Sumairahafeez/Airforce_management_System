using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForceLibrary.DL
{
    public class DLGDPFH:IGDP
    {
       //private  static readonly string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\GDPilot.txt";
        /// <summary>
        /// Stores a GDPilot in the database and writes the information to a file.
        /// </summary>
        /// <param name="Pilot">The GDPilot to store.</param>
        private static string path = ConnectionClass.GetGDPFile();
        private static DLGDPFH Instance;
        private DLGDPFH()
        {
            
        }
        public static DLGDPFH SetValidInstance()
        {
            if(Instance == null)
            {
                Instance = new DLGDPFH();
            }
            return Instance;
        }
        public void StoreGDP(GDPilot Pilot)
        {
            // Store GDPilot information in the personnel database
            IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
            AFPersonalle AF = new AFPersonalle(Pilot.GetName(), Pilot.GetRank(), Pilot.GetPakNo(), Pilot.GetPresentlyPosted());
            AFP.StoreAFPersonalle(AF);

            // Write GDPilot information to the file
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                if (Pilot.GetOC() != null)
                    writer.WriteLine(Pilot.GetSquadron() + "," + Pilot.GetFlyingHours() + "," + Pilot.GetPakNo() + "," + Pilot.GetOC().GetPakNo());
                else
                    writer.WriteLine(Pilot.GetSquadron() + "," + Pilot.GetFlyingHours() + "," + Pilot.GetPakNo() + "," + -1);
            }
        }

        /// <summary>
        /// Retrieves all GDPilots from the file and database.
        /// </summary>
        /// <returns>A list of all GDPilots.</returns>
        public List<GDPilot> GetAllGdps()
        {
            List<GDPilot> Gdps = new List<GDPilot>();

            // Read GDPilot information from the file
            using (StreamReader reader = new StreamReader(path))
            {
                //if (File.Exists(path))
                {
                    string record;
                    while ((record = reader.ReadLine()) != null)
                    {
                        string[] AllData = record.Split(',');
                        string squadron = AllData[0];
                        int FlyingHours = int.Parse(AllData[1]);
                        int PakNo = int.Parse(AllData[2]);
                        int OCPakNo = int.Parse(AllData[3]);

                        // Retrieve GDPilot's information from the personnel database
                        IAFPersonalle aFPersonalle = DLAFPersonalleFH.SetValidInstance();
                        AFPersonalle A = aFPersonalle.GetAFPersonalleByID(PakNo);
                        if (A != null)
                        {
                            GDPilot G = new GDPilot(A.GetName(), A.GetRank(), A.GetPakNo(), A.GetPresentlyPosted(), squadron);
                            // Set Commanding Officer if exists
                            if (OCPakNo != -1)
                            {
                                IOC OC = DLOCFH.SetValidInstance();
                                CommandingOfficers MyOC = OC.GetOCbyId(PakNo);
                                G.SetCommandingOfficer(MyOC);
                            }
                            else
                            {
                                G.SetCommandingOfficer(null);
                            }
                            // Retrieve GDPilot's missions and requests
                            IMission missions = DLMissionFH.SetValidInstance();
                            IRequest request = DLRequestsFH.SetValidInstance();
                            List<Mission> Mymissions = missions.GetAllMissionsOfSpecificOfficer(PakNo);
                            List<Requests> MyRequests = request.GetRequestsOfSpecificOfficer(PakNo);
                            G.SetFlyingHours(FlyingHours);
                            G.SetMission(Mymissions);
                            G.SetRequests(MyRequests);
                            Gdps.Add(G);

                        }

                    }
                   
                }
                return Gdps;
            }

           
        }

        /// <summary>
        /// Retrieves a GDPilot by PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to retrieve.</param>
        /// <returns>The GDPilot with the specified PakNo.</returns>
        public GDPilot GetGDPThroughPakNo(int PakNo)
        {
            // Iterate through all GDPilots
            foreach (GDPilot Pilot in GetAllGdps())
            {
                // Check if GDPilot's PakNo matches the provided PakNo
                if (Pilot.GetPakNo() == PakNo)
                {
                    return Pilot; // Return the GDPilot
                }
            }

            return null; // Return null if no matching GDPilot is found
        }

        /// <summary>
        /// Retrieves all GDPilots under a specific Commanding Officer.
        /// </summary>
        /// <param name="OCPakNo">The PakNo of the Commanding Officer.</param>
        /// <returns>A list of all GDPilots under the specified Commanding Officer.</returns>
        public List<GDPilot> GetAllUFofOC(int OCPakNo)
        {
            List<GDPilot> Pilots = new List<GDPilot>();

            // Retrieve all GDPilots
            foreach (GDPilot GDPilot in GetAllGdps())
            {  // it checks if it's OC Exists
                if(GDPilot.GetOC() != null)
                {
                    // Check if GDPilot's Commanding Officer PakNo matches the provided PakNo
                    if (GDPilot.GetOC().GetPakNo() == OCPakNo)
                    {
                        Pilots.Add(GDPilot);
                    }
                }
               
            }

            return Pilots;
        }

        /// <summary>
        /// Updates a GDPilot in the file and database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to update.</param>
        /// <param name="Pilot">The updated GDPilot information.</param>
        public void UpdateGDP(int PakNo, GDPilot Pilot)
        {
            // Retrieve all GDPilots
            List<GDPilot> gDPilots = GetAllGdps();

            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (GDPilot G in gDPilots)
                {
                    // Check if GDPilot's PakNo matches the provided PakNo
                    if (G.GetPakNo() == PakNo)
                    {
                        // Update GDPilot information in the personnel database
                        IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
                        AFPersonalle AF = new AFPersonalle(Pilot.GetName(), Pilot.GetRank(), Pilot.GetPakNo(), Pilot.GetPresentlyPosted());
                        AFP.UpdateAFPersonalle(PakNo, AF);

                        // Update GDPilot properties
                        G.SetSquadron(Pilot.GetSquadron());
                        G.SetFlyingHours(Pilot.GetFlyingHours());
                        if (Pilot.GetOC() != null)
                        {
                            G.SetOC(Pilot.GetOC());
                        }
                    }

                    // Write GDPilot information to the file
                    if (G.GetOC() != null)
                        writer.WriteLine(G.GetSquadron() + "," + G.GetFlyingHours() + "," + G.GetPakNo() + "," + G.GetOC().GetPakNo());
                    else
                        writer.WriteLine(G.GetSquadron() + "," + G.GetFlyingHours() + "," + G.GetPakNo() + "," + -1);
                }
            }
        }

        /// <summary>
        /// Deletes a GDPilot from the file and database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to delete.</param>
        public void DeleteGDP(int PakNo)
        {
            // Retrieve all GDPilots
            List<GDPilot> gDPilots = GetAllGdps();

            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (GDPilot G in gDPilots)
                {
                    // Check if GDPilot's PakNo matches the provided PakNo
                    if (G.GetPakNo() == PakNo)
                    {
                        // Delete GDPilot information from the personnel database
                        IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
                        AFPersonalle AF = new AFPersonalle(G.GetName(), G.GetRank(), G.GetPakNo(), G.GetPresentlyPosted());
                        AFP.DeleteAFPersonalle(PakNo);
                    }

                    // Write GDPilot information to the file
                    if (G.GetOC() != null)
                        writer.WriteLine(G.GetSquadron() + "," + G.GetFlyingHours() + "," + G.GetPakNo() + "," + G.GetOC().GetPakNo());
                    else
                        writer.WriteLine(G.GetSquadron() + "," + G.GetFlyingHours() + "," + G.GetPakNo() + "," + -1);
                }
            }
        }


    }
}
