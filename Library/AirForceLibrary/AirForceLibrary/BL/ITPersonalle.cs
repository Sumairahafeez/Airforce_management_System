using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirForceLibrary.Interfaces;
using AirForceLibrary.Utilis;

namespace AirForceLibrary.BL
{
    public class ITPersonalle:AFPersonalle
    {
        //This class will have only one instance because there will be a single server for IT that will add or update officers instead of Different officers
        private static ITPersonalle Instance;
        private ITPersonalle()
        {

        }
        public static ITPersonalle GetValidInstance()
        {
            if(Instance == null)
            {
                Instance = new ITPersonalle();
            }
            return Instance;
        }
        public bool AssignPassword(string Password,AFPersonalle Personalle)
        {
            if(Validations.IsValidAFPersonalle(Personalle.GetPakNo()))
            {
                Personalle.SetPassword(Password);
                return true;
            }
            return false;

        }
    }
}
