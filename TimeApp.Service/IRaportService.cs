using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.DbModels;
using TimeApp.Model.Request;
using TimeApp.Model.Response;

namespace TimeApp.Service
{
    public interface IRaportService
    {
        Task<ResultDTO> AddRaport(int userId);
        Task<ResultDTO> PatchClosedStatus(int raportId, bool closedStatus);
        Task<ResultDTO> PatchAcceptedStatus(int raportId, bool acceptedStatus);
        Task<List<Raports>> GetAllRaports();
        Task<List<Raports>> GetCurrentUserRaports(int userId);
        Task<ResultDTO> AddProject(ProjectVM projectVM);
        Task<ResultDTO> PatchProject(int projectId, ProjectVM projectVM);
        Task<ResultDTO> DeleteProject(int projectId);
        Task<ResultDTO> AddWeek(WeekVM weekVM);
        Task<ResultDTO> PatchWeek(int weekId, WeekVM weekVM);
        Task<ResultDTO> DeleteWeek(int weekId);

    }
}
