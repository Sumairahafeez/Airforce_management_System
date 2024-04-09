using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Interfaces
{
    public interface IGDP
    {
        void StoreGDP(GDPilot newGDP);
        List<GDPilot> GetAllGdps();
        void DeleteGDP(int PakNo);
    }
}
