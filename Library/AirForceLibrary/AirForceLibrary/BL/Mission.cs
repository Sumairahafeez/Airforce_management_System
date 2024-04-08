using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{   //This class is for mission category
    public class Mission
    {
        private DateTime Date;
        private string Details;
        private bool IsComplete;
        private float SuccessRate;
        //It will have a single parameterized Constructor and one default constructor
        public Mission()
        {

        }
        public Mission(DateTime date, string details)
        {
           SetDate(date);
           SetDetails(details);
        }
        //Define Getters Anf Setters
        public void SetDate(DateTime date)
        {
            this.Date = date;
        }
        public void SetDetails(string details)
        {
            this.Details = details;
        }
        public void SetIsComplete(bool isComplete)
        {
            this.IsComplete = isComplete;
        }
        public void SetSuccessRate(float successRate)
        {
            this.SuccessRate = successRate;
        }
        public DateTime GetDate()
        {
            return Date;
        }
        public string GetDetails()
        {
            return Details;
        }
        public bool GetIsComplete()
        {
            return IsComplete;
        }
        public float GetSuccessRate()
        {
            return SuccessRate;
        }
    }
}
