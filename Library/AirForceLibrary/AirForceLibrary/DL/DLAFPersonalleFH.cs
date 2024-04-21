﻿using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForceLibrary.DL
{
    public class DLAFPersonalleFH : IAFPersonalle
    {
        //private static readonly string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
        //Signleton implementation so that only one instance of the object is made
        private static string path = ConnectionClass.GetAFFile();
        private static DLAFPersonalleFH Instance;
        private DLAFPersonalleFH() { }
        public static DLAFPersonalleFH SetValidInstance()
        {
            if (Instance == null)
            {
                Instance = new DLAFPersonalleFH();
               
            }
            return Instance;
        }
       
        /// <summary>
        /// Stores an AFPersonalle's information and updates the file.
        /// </summary>
        /// <param name="aFPersonalle">The AFPersonalle to store.</param>
        public void StoreAFPersonalle(AFPersonalle aFPersonalle)
        {
            // Open the file for appending and write AFPersonalle information
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(aFPersonalle.GetName() + ";" + aFPersonalle.GetPakNo() + ";" + aFPersonalle.GetRank() + ";" + aFPersonalle.GetPresentlyPosted() + ";"+aFPersonalle.GetPassword() +";"+aFPersonalle.GetBranch());
            }
        }

        /// <summary>
        /// Retrieves all AFPersonalle from the file.
        /// </summary>
        /// <returns>A list of AFPersonalle.</returns>
        public List<AFPersonalle> GetAFPersonalles()
        {
            List<AFPersonalle> personalles = new List<AFPersonalle>();
            // Open the file for reading
            
            if (File.Exists(path)) 
            {
               
                using (StreamReader reader = new StreamReader(path))
                {   
                    string record;
                    try
                    {
                        // if (File.Exists(path))
                        {  
                            // Read each line of the file
                            while ((record = reader.ReadLine()) != null)
                            {
                               
                                // Split the record into individual pieces of information
                                string[] AllData = record.Split(';');
                                string Name = AllData[0];
                                int PakNo = int.Parse(AllData[1]);
                                string Rank = AllData[2];
                                string Posted = AllData[3];
                                string Password = AllData[4];
                                string Branch = AllData[5];
                              
                                // Create an AFPersonalle object and add it to the list
                                AFPersonalle aFPersonalle = new AFPersonalle(Name, Rank, PakNo, Posted);
                                aFPersonalle.SetPassword(Password);
                                aFPersonalle.SetBranch(Branch);
                                personalles.Add(aFPersonalle);
                            }
                           
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
          
               
            }
            return personalles;
        }

        /// <summary>
        /// Retrieves an AFPersonalle by PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to retrieve.</param>
        /// <returns>The AFPersonalle with the specified PakNo.</returns>
        public AFPersonalle GetAFPersonalleByID(int PakNo)
        {
            if (File.Exists(path))
            {
              

                using (StreamReader reader = new StreamReader(path))
                {
                    string record;
                    try
                    {
                       
                        // if (File.Exists(path))
                        {
                            // Read each line of the file
                            while ((reader.ReadLine() != null))
                            {

                               
                                // Split the record into individual pieces of information
                                string[] AllData = reader.ReadLine().Split(';');
                                for(int i = 0; i<AllData.Length; i++)
                                {
                                }
                                string Name = AllData[0];
                                int PakNO = int.Parse(AllData[1]);
                                string Rank = AllData[2];
                                string Posted = AllData[3];
                                string Password = AllData[4];
                                string Branch = AllData[5];
                                
                                if(PakNO == PakNo)
                                {
                                    AFPersonalle aFPersonalle = new AFPersonalle(Name, Rank, PakNo, Posted);
                                    aFPersonalle.SetPassword(Password);
                                    aFPersonalle.SetBranch(Branch);
                                    return aFPersonalle;
                                }
                                // Create an AFPersonalle object and add it to the list
                              
                               
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }
                }


            }
            return null;

        }

        /// <summary>
        /// Updates an existing AFPersonalle's information.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to update.</param>
        /// <param name="aFPersonalle">The updated AFPersonalle information.</param>
        public void UpdateAFPersonalle(int PakNo, AFPersonalle aFPersonalle)
        {
            // Get the list of AFPersonalles
            IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
            List<AFPersonalle> personalles = AFP.GetAFPersonalles();
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through all AFPersonalles
                foreach (AFPersonalle personalle in personalles)
                {
                    // Check if AFPersonalle's PakNo matches the provided PakNo
                    if (personalle.GetPakNo() == PakNo)
                    {
                       
                        // Update AFPersonalle properties
                        personalle.SetName(aFPersonalle.GetName());
                        personalle.SetRank(aFPersonalle.GetRank());
                        personalle.SetPresentlyPosted(aFPersonalle.GetPresentlyPosted());
                    }
                    // Write AFPersonalle information to the file
                    writer.WriteLine(personalle.GetName() + "," + personalle.GetPakNo() + "," + personalle.GetRank() + "," + personalle.GetPresentlyPosted() +","+personalle.GetPassword()+","+personalle.GetBranch());
                }
            }
        }

        /// <summary>
        /// Deletes an AFPersonalle with the specified PakNo.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to delete.</param>
        public void DeleteAFPersonalle(int PakNo)
        {
            // Get the list of AFPersonalles
            List<AFPersonalle> personalles = GetAFPersonalles();
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through all AFPersonalles
                foreach (AFPersonalle personalle in personalles)
                {
                    // Skip writing if AFPersonalle's PakNo matches the provided PakNo
                    if (personalle.GetPakNo() == PakNo)
                    {
                        continue;
                    }
                    // Write AFPersonalle information to the file
                    writer.WriteLine(personalle.GetName() + "," + personalle.GetPakNo() + "," + personalle.GetRank() + "," + personalle.GetPresentlyPosted() + "," + personalle.GetPassword() + "," + personalle.GetBranch());
                }
            }
        }

    }
}
