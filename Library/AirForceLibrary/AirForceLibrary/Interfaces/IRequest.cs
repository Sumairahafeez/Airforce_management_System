using AirForceLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForceLibrary.Interfaces
{
    public interface IRequest
    {
        List<Requests> GetAllRequest();
        List<Requests> GetRequestsOfSpecificOfficer(int PakNo);
        void StoreRequests(Requests request);
        void UpdateRequests(int requestId,Requests request);
        void DeleteRequests(int RequestId);
    }
}
