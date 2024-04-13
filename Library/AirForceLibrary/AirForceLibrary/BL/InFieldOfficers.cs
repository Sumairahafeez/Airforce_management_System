using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{   //This Class is for all officers as well as Airmen working on the field like CAE etc
    public abstract class InFieldPersonalle:AFPersonalle
    {
        private string Squadron;//This attribute refers to the squadron the officer is belongigng to
        private List<Mission> Missions;
        private List<Requests> requests;
        private CommandingOfficers OC;
        public InFieldPersonalle(string Name, string Rank, int PakNO, string PresentlyLocated, string Squadron) : base(Name, Rank, PakNO, PresentlyLocated)
        {
            SetSquadron(Squadron);
            Missions = new List<Mission>();
            requests = new List<Requests>();
        }
        public InFieldPersonalle() { }
        //Define the getters and Setters
        public void SetSquadron(string Squadron)
        {
            this.Squadron = Squadron;
        }
        
        public string GetSquadron()
        {
            return this.Squadron;
        }
        public CommandingOfficers GetOC()
        {
            return OC;
        }
        public void SetOC(CommandingOfficers c)
        {
            OC = c;
        }
        public void SetMission(List<Mission> missions)
        {
            Missions = missions;
        }
        public void AddMission(Mission mission)
        {
            Missions.Add(mission);
        }
        public void SetRequests(List<Requests> requests)
        {
            this.requests = requests;
        }
        public void SetRequest(Requests req)
        {
            requests.Add(req);
        }
        public List<Requests> GetRequests()
        {
            return requests;
        }
        public void SetCommandingOfficer(CommandingOfficers officer)
        {
            this.OC = officer;
        }
        public CommandingOfficers GetOfficer()
        {
            return this.OC;
        }
        //Define the behaviours of InFiled Personalle
        public virtual void CompleteMission(DateTime Date, bool IsCompleted, float SuccessRate,Mission mymission)
        {
           
            if (IsCompleted)
            {
                mymission.SetIsComplete(true);
                mymission.SetSuccessRate(SuccessRate);
            }
        }
        public virtual bool UpdateMission(DateTime Date, bool IsCompleted, float SuccessRate, Mission mymission)
        {
          
            if (IsCompleted)
            {

                mymission.SetSuccessRate(SuccessRate);
                return true;
            }
            return false;
        }
        public virtual bool SetIncompleted(DateTime Date, bool IsCompleted, Mission mymission)
        {
           
            if (IsCompleted)
            {

                mymission.SetIsComplete(false);
                return true;
            }
            return false;
        }
        public abstract bool AddRequest(Requests requests);
       
       
    }
}
