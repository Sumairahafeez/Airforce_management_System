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
        public static List<GDPilot> Pilots = new List<GDPilot>();
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
                    writer.WriteLine(Pilot.GetName()+";"+Pilot.GetPakNo()+";"+Pilot.GetRank()+";"+Pilot.GetPresentlyPosted()+";"+Pilot.GetBranch()+";"+Pilot.GetPassword()+";"+Pilot.GetSquadron() + ";" + Pilot.GetFlyingHours() + ";"  + Pilot.GetOC().GetPakNo());
                else
                    writer.WriteLine(Pilot.GetName() + ";" + Pilot.GetPakNo() + ";" + Pilot.GetRank() + ";" + Pilot.GetPresentlyPosted() + ";" + Pilot.GetBranch() + ";" + Pilot.GetPassword() + ";" + Pilot.GetSquadron() + ";" + Pilot.GetFlyingHours() + ";" + -1);
            }
        }
        private void LoadList()
        {
            List<GDPilot> Gdps = new List<GDPilot>();

            // Read GDPilot information from the file
            using (StreamReader reader = new StreamReader(path))
            {
                //if (File.Exists(path))
                {
                    string read;
                    while ((read = reader.ReadLine()) != null)
                    {
                        string[] AllData = read.Split(';');
                        string name = AllData[0];
                        int PakNo = int.Parse(AllData[1]);
                        string Rank = AllData[2];
                        string PresentlyPosted = AllData[3];
                        string BRanch = AllData[4];
                        string Password = AllData[5];
                        string squadron = AllData[6];
                        int FlyingHours = int.Parse(AllData[7]);

                        int OCPakNo = int.Parse(AllData[8]);

                        // Retrieve GDPilot's information from the personnel database
                        //IAFPersonalle aFPersonalle = DLAFPersonalleFH.SetValidInstance();
                        //AFPersonalle A = aFPersonalle.GetAFPersonalleByID(PakNo);
                        //if (A != null)
                        {
                            GDPilot G = new GDPilot(name, Rank, PakNo, PresentlyPosted, squadron);
                            G.SetBranch(BRanch);
                            G.SetPassword(Password);
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
                            if (Mymissions != null)
                            {
                                G.SetMission(Mymissions);
                            }
                            if (MyRequests != null)
                            {
                                G.SetRequests(MyRequests);
                            }
                            G.SetFlyingHours(FlyingHours);


                            Gdps.Add(G);

                        }

                    }

                }
                Pilots = Gdps;

            }
        }
        /// <summary>
        /// Retrieves all GDPilots from the file and database.
        /// </summary>
        /// <returns>A list of all GDPilots.</returns>
        public List<GDPilot> GetAllGdps()
        {
                LoadList();
                return Pilots;
            

           
        }

        /// <summary>
        /// Retrieves a GDPilot by PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to retrieve.</param>
        /// <returns>The GDPilot with the specified PakNo.</returns>
        public GDPilot GetGDPThroughPakNo(int PakNo)
        {
            //IGDP Gd = DLGDPFH.SetValidInstance();
           
            //List<GDPilot> GDP = Gd.GetAllGdps();
            // Iterate through all GDPilots
            foreach (GDPilot Pilot in Pilots)
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
            List<GDPilot> pilots = new List<GDPilot>();
            //IGDP gDP = DLGDPFH.SetValidInstance();
            //List<GDPilot> list = gDP.GetAllGdps();
            // Retrieve all GDPilots
            foreach (GDPilot GDPilot in Pilots)
            {  // it checks if it's OC Exists
                if(GDPilot.GetOC() != null)
                {
                    // Check if GDPilot's Commanding Officer PakNo matches the provided PakNo
                    if (GDPilot.GetOC().GetPakNo() == OCPakNo)
                    {
                        pilots.Add(GDPilot);
                    }
                }
               
            }

            return pilots;
        }

        /// <summary>
        /// Updates a GDPilot in the file and database.
        /// </summary>
        /// <param name="PakNo">The PakNo of the GDPilot to update.</param>
        /// <param name="Pilot">The updated GDPilot information.</param>
        public void UpdateGDP(int PakNo, GDPilot Pilot)
        {
            // Retrieve all GDPilots
            //List<GDPilot> gDPilots = GetAllGdps();

            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (GDPilot G in Pilots)
                {
                    // Check if GDPilot's PakNo matches the provided PakNo
                    if (G.GetPakNo() == PakNo)
                    {
                        // Update GDPilot information in the personnel database
                        IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
                        AFPersonalle AF = new AFPersonalle(Pilot.GetName(), Pilot.GetRank(), Pilot.GetPakNo(), Pilot.GetPresentlyPosted());
                        AFP.UpdateAFPersonalle(PakNo, AF);

                        // Update GDPilot properties
                        G.SetName(Pilot.GetName());
                        G.SetPassword(Pilot.GetPassword());
                        G.SetBranch(Pilot.GetBranch());
                        G.SetCommandingOfficer(Pilot.GetOC());
                        G.SetRank(Pilot.GetRank());
                        G.SetPresentlyPosted(Pilot.GetPresentlyPosted());
                        G.SetSquadron(Pilot.GetSquadron());
                        G.SetFlyingHours(Pilot.GetFlyingHours());
                        if (Pilot.GetOC() != null)
                        {
                            G.SetOC(Pilot.GetOC());
                        }
                    }

                    // Write GDPilot information to the file
                    if (G.GetOC() != null)
                        writer.WriteLine(G.GetName() + ";" +G.GetPakNo()+";"+G.GetRank()+";"+G.GetPresentlyPosted()+";"+G.GetBranch()+";"+G.GetPassword()+";"+G.GetSquadron()+";"+ G.GetFlyingHours() +  ";" + G.GetOC().GetPakNo());
                    else
                        writer.WriteLine(G.GetName() + ";" + G.GetPakNo() + ";" + G.GetRank() + ";" + G.GetPresentlyPosted() + ";" + G.GetBranch() + ";" + G.GetPassword() + ";" + G.GetSquadron() + ";" + G.GetFlyingHours() + ";" + -1);

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
                foreach (GDPilot G in Pilots)
                {
                    // Check if GDPilot's PakNo matches the provided PakNo
                    if (G.GetPakNo() == PakNo)
                    {
                        // Delete GDPilot information from the personnel database
                        IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
                        //AFPersonalle AF = new AFPersonalle(G.GetName(), G.GetRank(), G.GetPakNo(), G.GetPresentlyPosted());
                        AFP.DeleteAFPersonalle(PakNo);
                    }

                    // Write GDPilot information to the file
                    if (G.GetOC() != null)
                        writer.WriteLine(G.GetName() + ";" + G.GetPakNo() + ";" + G.GetRank() + ";" + G.GetPresentlyPosted() + ";" + G.GetBranch() + ";" + G.GetPassword() + ";" + G.GetSquadron() + ";" + G.GetFlyingHours() + ";" + G.GetOC().GetPakNo());
                    else
                        writer.WriteLine(G.GetName() + ";" + G.GetPakNo() + ";" + G.GetRank() + ";" + G.GetPresentlyPosted() + ";" + G.GetBranch() + ";" + G.GetPassword() + ";" + G.GetSquadron() + ";" + G.GetFlyingHours() + ";" + -1);
                }
            }
        }


    }
}
