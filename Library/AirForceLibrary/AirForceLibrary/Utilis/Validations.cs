using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Utilis
{
    public class Validations
    {
        public static bool IsValidOC(string Rank)
        {
            if (Rank == "Group Captain" || Rank == "Wing Commander" || Rank == "Air Commander")
            {
                return true;
            }
            return false;
        }
        public static bool IsValidIFOfficer(string Rank) {
            if(Rank == "Yes")
            {
                return true;
            }
            return false;
        }
        public static bool IsValidIT(string name,int PakNo,string Password)
        {
            if (PakNo == 123 && Password == "123")
            {
                return true;
            }
            return false;
        }
        public static bool IsValidPakNo(int PakNO)
        {
            List<AFPersonalle> AF = Interfaces.AFInterface.GetAFPersonalles();
            foreach(AFPersonalle personalle in AF)
            {
                if(personalle.GetPakNo() ==  PakNO)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
