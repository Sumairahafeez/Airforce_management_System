using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{   //This class refers to any type of application send by an officer to its commanding officer
    public class Requests
    {
        private int PakNo;
        private int RequestId;
        private string Context;
        private string Status;
        //Define its constructor
        public Requests() { }
        public Requests(int RequestId, string Context,int PAkNo)
        {
            SetContext(Context);
            SetPakNo(PAkNo);
            SetRequestId(RequestId);
        }
        //Define Getters and Setters
        public int GetPakNo()
        {
            return PakNo;
        }
        public void SetPakNo(int PakNo)
        {
            this.PakNo = PakNo;
        }
        public int GetRequestId()
        {
            return RequestId;
        }
        public void SetRequestId(int Id)
        {
            RequestId = Id;
        }
        public string GetContext()
        {
            return Context;
        }
        public void SetContext(string Context)
        {
            this.Context = Context;
        }
        public string GetStatus()
        {
            return Status;
        }
        public void SetStatus(string status)
        {
            Status = status;
        }
        public new  string ToString()
        {
            return  RequestId+"\t \t "+Context+"\t \t"+Status;
        }
    }
}
