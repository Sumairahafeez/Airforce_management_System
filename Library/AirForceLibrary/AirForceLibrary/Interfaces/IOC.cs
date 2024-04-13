using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Interfaces
{
    public interface IOC
    {
        void StoreOC(CommandingOfficers officers);
        CommandingOfficers GetOCbyId(int id);
        List<CommandingOfficers> GetAll();
        void DeleteOC(int PakNo);
        void UpdateOC(int  PakNo,CommandingOfficers OC);
    }
}
