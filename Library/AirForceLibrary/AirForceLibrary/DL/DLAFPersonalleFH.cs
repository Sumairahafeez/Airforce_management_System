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
using System.Windows.Forms;

namespace AirForceLibrary.DL
{
    public class DLAFPersonalleFH : IAFPersonalle
    {
        //Signleton implementation so that only one instance of the object is made
        private static string path = ConnectionClass.GetAFFile();
        private static DLAFPersonalleFH Instance;
        private static List<AFPersonalle> personalle;
        private DLAFPersonalleFH() { }
        public static DLAFPersonalleFH SetValidInstance()
        {
            if (Instance == null)
            {
                Instance = new DLAFPersonalleFH();
               
            }
            return Instance;
        }
       
       //It stores the AFPersonalle
        public void StoreAFPersonalle(AFPersonalle aFPersonalle)
        {
            // Open the file for appending and write AFPersonalle information
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(aFPersonalle.GetName() + ";" + aFPersonalle.GetPakNo() + ";" + aFPersonalle.GetRank() + ";" + aFPersonalle.GetPresentlyPosted() + ";"+aFPersonalle.GetPassword() +";"+aFPersonalle.GetBranch());
            }
        }
        private void Loadlist()
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

                    catch (Exception)
                    {
                    }
                }
            }
            personalle = personalles;
        }
       //It returns the list of AFPersonalle 
        public List<AFPersonalle> GetAFPersonalles()
        {
            Loadlist();
            return personalle;
        }

       //this function returns an AFPersonalle by Id
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
                            while ((record = reader.ReadLine()) != null)
                            {

                               
                                // Split the record into individual pieces of information
                                string[] AllData = record.Split(';');
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
           //This fucntion is to update AFPersonalle on the basis of pakNo
        public void UpdateAFPersonalle(int PakNo, AFPersonalle aFPersonalle)
        {
            // Get the list of AFPersonalles
            IAFPersonalle AFP = DLAFPersonalleFH.SetValidInstance();
            List<AFPersonalle> personalles = AFP.GetAFPersonalles();
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // Iterate through all AFPersonalles
                foreach (AFPersonalle personalle in personalle)
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
                    writer.WriteLine(personalle.GetName() + ";" + personalle.GetPakNo() + ";" + personalle.GetRank() + ";" + personalle.GetPresentlyPosted() +";"+personalle.GetPassword()+";"+personalle.GetBranch());
                }
            }
        }
        //this function deltes the AFPersonalle on the basis of PakNo
        public void DeleteAFPersonalle(int PakNo)
        {
            // Get the list of AFPersonalles
            List<AFPersonalle> personalles = GetAFPersonalles();
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(path))
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
                    writer.WriteLine(personalle.GetName() + ";" + personalle.GetPakNo() + ";" + personalle.GetRank() + ";" + personalle.GetPresentlyPosted() + ";" + personalle.GetPassword() + ";" + personalle.GetBranch());
                }
            }
        }

    }
}
