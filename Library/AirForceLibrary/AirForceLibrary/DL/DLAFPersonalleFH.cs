using AirForceLibrary.BL;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.DL
{
    public class DLAFPersonalleFH : IAFPersonalle
    {
        //private static readonly string path = "F:\\2nd semester\\OOP Lab\\Air Force Management System\\AirForce\\Library\\AirForceLibrary\\AirForceLibrary\\FileHandling\\AFPersonalle.txt";
        /// <summary>
        /// Stores an AFPersonalle's information and updates the file.
        /// </summary>
        /// <param name="aFPersonalle">The AFPersonalle to store.</param>
        public string path = ConnectionClass.GetAFFile();
        public void StoreAFPersonalle(AFPersonalle aFPersonalle)
        {
            // Open the file for appending and write AFPersonalle information
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(aFPersonalle.GetName() + "," + aFPersonalle.GetPakNo() + "," + aFPersonalle.GetRank() + "," + aFPersonalle.GetPresentlyPosted());
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
            
            if(File.Exists(path)) 
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
                                string[] AllData = record.Split(',');
                                string Name = AllData[0];
                                int PakNo = int.Parse(AllData[1]);
                                string Rank = AllData[2];
                                string Posted = AllData[3];
                                // Create an AFPersonalle object and add it to the list
                                AFPersonalle aFPersonalle = new AFPersonalle(Name, Rank, PakNo, Posted);
                                personalles.Add(aFPersonalle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }// 
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
            // Iterate through all AFPersonalles
            foreach (AFPersonalle aFPersonalle in GetAFPersonalles())
            {
                // Check if AFPersonalle's PakNo matches the provided PakNo
                if (aFPersonalle.GetPakNo() == PakNo)
                {
                    return aFPersonalle; // Return the AFPersonalle
                }
            }
            return null; // Return null if no matching AFPersonalle is found
        }

        /// <summary>
        /// Updates an existing AFPersonalle's information.
        /// </summary>
        /// <param name="PakNo">The PakNo of the AFPersonalle to update.</param>
        /// <param name="aFPersonalle">The updated AFPersonalle information.</param>
        public void UpdateAFPersonalle(int PakNo, AFPersonalle aFPersonalle)
        {
            // Get the list of AFPersonalles
            List<AFPersonalle> personalles = GetAFPersonalles();
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
                    writer.WriteLine(personalle.GetName() + "," + personalle.GetPakNo() + "," + personalle.GetRank() + "," + personalle.GetPresentlyPosted());
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
                    writer.WriteLine(personalle.GetName() + "," + personalle.GetPakNo() + "," + personalle.GetRank() + "," + personalle.GetPresentlyPosted());
                }
            }
        }

    }
}
