using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.DbModels;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class RaportService : IRaportService
    {
        private readonly IRepository<Raports> _raportrepo;
        private readonly IRepository<Project> _projectrepo;
        private readonly IRepository<Week> _weekrepo;

        public RaportService(IRepository<Raports> raportrepo, IRepository<Project> projectrepo, IRepository<Week> weekrepo)
        {
            _raportrepo = raportrepo;
            _projectrepo = projectrepo;
            _weekrepo = weekrepo;
    }
        public async Task<ResultDTO> AddRaport(int userId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _raportrepo.Add(new Raports
                {
                    UserId = userId,             
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchClosedStatus(int raportId, bool closedStatus)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == raportId);
                if (raport == null)
                    result.Response = "Raport not found";
                    raport.IsClosed =closedStatus;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchAcceptedStatus(int raportId, bool acceptedStatus)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == raportId);
                if (raport == null)
                    result.Response = "Raport not found";
                raport.IsAccepted = acceptedStatus;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<List<Raports>> GetAllRaports()
        {
            var raportsList = await _raportrepo.GetAll();
            return raportsList;
        }

        public async Task<List<Raports>> GetCurrentUserRaports(int userId)
        {
            var raportsList = await _raportrepo.GetAll();
            var raportList = new List<Raports>();

            foreach(Raports raport in raportsList)
            {
                if (raport.UserId == userId)
                    raportList.Add(raport);
            }
            return raportList;
        }

        public async Task<ResultDTO> AddProject(ProjectVM projectVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _projectrepo.Add(new Project
                {
                    Name = projectVM.Name,
                    WorkedHours = projectVM.WorkedHours,
                    WeekId = projectVM.WeekId
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchProject(int projectId, ProjectVM projectVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var project = await _projectrepo.GetSingleEntity(x => x.Id == projectId);
                if (project == null)
                    result.Response = "Project not found";
                if (projectVM.Name != null)
                    project.Name = projectVM.Name;
                if (projectVM.WorkedHours != null)
                    project.WorkedHours = projectVM.WorkedHours;

                await _projectrepo.Patch(project);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> DeleteProject(int projectId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var project = await _projectrepo.GetSingleEntity(x => x.Id == projectId);
                if (project == null)
                    result.Response = "Project not found";
                await _projectrepo.Delete(project);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }


        public async Task<ResultDTO> AddWeek(WeekVM weekVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _weekrepo.Add(new Week
                {
                    WeekNumber = weekVM.WeekNumber,
                    WorkedHours = weekVM.WorkedHours,
                    HoursInWeek = weekVM.HoursInWeek,
                    RaportId = weekVM.RaportId
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchWeek(int weekId, WeekVM weekVM)
        { 
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var week = await _weekrepo.GetSingleEntity(x => x.Id == weekId);
                if (week == null)
                    result.Response = "Week not found";
                if (weekVM.WeekNumber != null)
                    week.WeekNumber = weekVM.WeekNumber;
                if (weekVM.WorkedHours != null)
                    week.WorkedHours = weekVM.WorkedHours;
                if (weekVM.HoursInWeek != null)
                    week.HoursInWeek = weekVM.HoursInWeek;

                await _weekrepo.Patch(week);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> DeleteWeek(int weekId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var week = await _weekrepo.GetSingleEntity(x => x.Id == weekId);
                if (week == null)
                    result.Response = "Week not found";
                await _weekrepo.Delete(week);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        
    }
}
