using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Interfaces
{
    public interface IAFPersonalle
    {
        void StoreAFPersonalle(AFPersonalle personalle);
        List<AFPersonalle> GetAFPersonalles();
        void DeleteAFPersonalle(int PakNo);
        void UpdateAFPersonalle(int PakNo, AFPersonalle a);
        AFPersonalle GetAFPersonalleByID(int id);
       
        
    }
}
