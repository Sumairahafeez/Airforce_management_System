using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{   //This Class is for GDP it is child or base class of AFPersonalle class and is specifically for GDP Officers Under the rank of Group Captain
    public class GDPilot:InFieldPersonalle
    {
       
        private int FlyingHours=0;
        
        //SET ITS CONSTRUCTORS
       
        public GDPilot(string Name,string Rank,int PakNO,string PresentlyLocated,string Squadron):base(Name,Rank,PakNO,PresentlyLocated,Squadron)
        {
            SetSquadron(Squadron);
           
        }
        //Define its Getters And Setters
        public void SetFlyingHours(int FlyingHours)
        {
            this.FlyingHours = FlyingHours;
        }
        public int GetFlyingHours()

        { return this.FlyingHours; }
        //Define The Behaviors Of the GDP
        public new void CompleteMission(DateTime Date, bool IsCompleted, float SuccessRate,Mission mymission)
        {
            if (IsCompleted)
            {
                mymission.SetIsComplete(true);
                mymission.SetSuccessRate(SuccessRate/10);
            }
        }
       
        public new bool UpdateMission(DateTime Date, bool IsCompleted, float SuccessRate,Mission mymission)
        {
           
            if (IsCompleted)
            {

                mymission.SetSuccessRate(SuccessRate/10);
                return true;
            }
            return false;
        }
        public new bool SetIncompleted(DateTime Date, bool IsCompleted,Mission mymission)
        {
           
            if (IsCompleted)
            {

                mymission.SetIsComplete(false);
                return true;
            }
            return false;
        }
        public override bool AddRequest(Requests req)
        {   foreach(Requests requ in GetRequests())
            {
                if(req.GetRequestId() != requ.GetRequestId())
                {
                    if (req.GetPakNo() == GetPakNo())
                    {
                        SetRequest(req);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
