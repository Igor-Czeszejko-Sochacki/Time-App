using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.DbModels;
using TimeApp.Model.Response;

namespace TimeApp.Service
{
    public interface IRaportService
    {
        Task<ResultDTO> AddRaport(int userId);
        Task<ResultDTO> PatchClosedStatus(int raportId, bool closedStatus);
        Task<ResultDTO> PatchAcceptedStatus(int raportId, bool acceptedStatus);
        Task<List<Raports>> GetAllRaports();
    }
}
