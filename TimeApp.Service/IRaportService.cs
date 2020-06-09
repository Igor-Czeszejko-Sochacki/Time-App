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
        Task<ResultDTO> AddRaport(int userId, string monthName);
        Task<ResultDTO> AddProject(ProjectVM projectVM);
        Task<ResultDTO> AddWeek(WeekVM weekVM);
        Task<ResultDTO> PatchClosedStatus(int raportId, bool closedStatus);
        Task<ResultDTO> PatchAcceptedStatus(int raportId, bool acceptedStatus);
        Task<ResultDTO> PatchProject(int projectId, ProjectVM projectVM);
        Task<ResultDTO> PatchWeek(int weekId, WeekVM weekVM);
        Task<RaportListDTO> GetAllRaports();
        Task<RaportListDTO> GetCurrentUserRaports(int userId);
        Task<List<Project>> GetAllProjects();
        Task<List<Week>> GetAllWeeks();
        Task<ResultDTO> DeleteProject(int projectId);
        Task<ResultDTO> DeleteWeek(int weekId);

    }
}
