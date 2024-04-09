using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Interfaces
{
    public interface IMission
    {
       void StoreMission(Mission mission,int PakNO);
        List<Mission> GetAllMissionsOfSpecificOfficer(int OffId);
       void UpdateMission(DateTime Date,Mission mission);
       void DeleteMission(Mission mission);
       Mission GetMissionFromDate(DateTime Date);
    }
}
