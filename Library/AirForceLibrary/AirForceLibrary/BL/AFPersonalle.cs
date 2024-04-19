using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{   //THIS CLASS IS FOR ALL THE AIR FORCE PERSONALLES EITHER BELONGING TO TH EOFFICER CATEGORY OR NOT IT IS PARENT CLASS FOR ALL OTHER CLASSES
    public class AFPersonalle
    {
        private string Name;
        private string Rank;//their designantion
        private int PakNo;//The number assigned to them by AIR Force
        private string PresentlyPosted;
        private string Password;
        private string Branch;//It tells the location of the personalle
        //It will include two contsructors one by default and other parameterized
        public AFPersonalle()
        {

        }
        public AFPersonalle(string name, string rank, int pakNo, string presentlyPosted)
        {
            SetName(name);
            SetPakNo(pakNo);
            SetRank(rank);
            SetPresentlyPosted(presentlyPosted);
        }
        //Define getters and setters for your class attribute
        //First Setters
        public void SetName(string Name)
        {
            this.Name = Name; 
        }
        public void SetRank(string Rank)
        {
            this.Rank = Rank;
        }
        public void SetPakNo(int PakNo)
        {
            this.PakNo = PakNo;
        }
        public void SetPresentlyPosted(string PresentlyPosted)
        {
            this.PresentlyPosted = PresentlyPosted;
        }
        public void SetPassword(string Password)
        {
            this.Password = Password;
        }
        public void SetBranch(string Branch)
        {
            this.Branch = Branch;
        }
        //Then getters
        public string GetName()
        {
            return Name;
        }
        public string GetRank()
        {
            return Rank;
        }
        public int GetPakNo()
        {
            return PakNo;
        }
        public string GetPresentlyPosted()
        {
            return PresentlyPosted;
        }
        public string GetPassword()
        {
            return Password;
        }
        public string GetBranch()
        {
            return Branch;
        }
    }
}
