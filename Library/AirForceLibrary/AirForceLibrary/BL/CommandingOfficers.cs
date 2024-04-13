using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirForceLibrary.Utilis;

namespace AirForceLibrary.BL
{   //This class is for all the OC's Commanding officers who are the commanders and incharges of other officers they are the personalles of ranks higher than group captain
    // or wing commander in some cases every class of officer has a single commanding offer like GDP class has one OC CAE class has one OC similarly other classes
    public class CommandingOfficers:AFPersonalle
    {   //This consist of the attributes of OC
        private string Squadron;
        private List<GDPilot> UnderOfficers;
        //Define constructors
        public CommandingOfficers() { }
        public CommandingOfficers(string Name, string Rank, int PAkNo, string PresentlyPosted, string Sqadron):base(Name, Rank, PAkNo,PresentlyPosted)
        {
            SetSquadron(Sqadron);
            UnderOfficers = new List<GDPilot>();
        }
        //Define Getters And Setters
        public void SetSquadron(string Squadron)
        {
            this.Squadron = Squadron;
        }
        public string GetSquadron()
        {
            return this.Squadron;
        }
        public List<GDPilot>GetUnderOfficer()
        {
            return UnderOfficers.ToList();
        }
        public void SetUnderOff(List<GDPilot> UnderOfficer)
        {
            this.UnderOfficers = UnderOfficer;
        }
        //Now Define the behaviours of Commanding Officers
        //1. Define the CRUD of its under officers
        public void AddUnderOfficer(GDPilot inFieldPersonalle)
        {
            UnderOfficers.Add(inFieldPersonalle);
            
        }
        //2. They can assign missions to their under officers
        public void AssignMission(int PakNo,Mission TheMission)
        {
            foreach(InFieldPersonalle Officer in UnderOfficers)
            {
                if(Officer.GetPakNo()==PakNo)
                {
                    Officer.AddMission(TheMission);
                }
            }
        }
        //3. They can check requests and approve and reject them
        public void CheckRequests(int PakNo,Requests request,string status)
        {  
            foreach (InFieldPersonalle Officer in UnderOfficers)
            {
                if (Officer.GetPakNo() == PakNo)
                {
                    request.SetStatus(status);
                }
            }
        }
        //4. They can Assign their under officers to different posting locations first they will ask the OC of that Location fro approval
        public bool SetPosting(int PakNo, string LOcation,CommandingOfficers NewOC)
        {
            foreach(GDPilot Officer in UnderOfficers)
            {
                if(Officer.GetPakNo().Equals(PakNo) && !(NewOC.IsValidUnderOfficer(Officer) && (NewOC.GetSquadron() == GetSquadron())))
                {
                    
                     bool IsOK =  NewOC.AskForApproval(Officer);
                     if(IsOK)
                     {
                        Officer.SetPresentlyPosted(LOcation);
                        Officer.SetCommandingOfficer(NewOC);
                        UnderOfficers.Remove(Officer);
                        return true;
                     }
                     
                }
            }
            return false;
        }
        //5. It can Approve new Officers
        public bool AskForApproval(GDPilot NewOFFICER)
        {
            if(UnderOfficers.Count <10 && !(IsValidUnderOfficer(NewOFFICER)))
            {
                AddUnderOfficer(NewOFFICER);
                return true;
            }
            return false;
        }
        //6. Traversing the UnderOFFICERS AND CHECK IF IT IS VALID This function can also be used for validation
        public bool IsValidUnderOfficer(GDPilot Officer)
        {
            foreach(GDPilot aFPersonalle in UnderOfficers)
            {
                if (aFPersonalle == Officer)
                    return true;
               
            }
            return false;
        }
        
        
    }
}
