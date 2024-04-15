using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.BL
{
    public class ITPersonalle:AFPersonalle
    {
        private string Password;
        public ITPersonalle()
        {

        }
        public void SetPassword(string Password)
        {
            this.Password = "123";
        }
        public string GetPassword()
        {
            return this.Password;
        }
    }
}
