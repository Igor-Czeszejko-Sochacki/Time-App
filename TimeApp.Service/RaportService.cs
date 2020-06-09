using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
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
        private readonly IRepository<User> _userrepo;

        public RaportService(IRepository<Raports> raportrepo, IRepository<Project> projectrepo, IRepository<Week> weekrepo, IRepository<User> userrepo)
        {
            _raportrepo = raportrepo;
            _projectrepo = projectrepo;
            _weekrepo = weekrepo;
            _userrepo = userrepo;
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

        public async Task<RaportListDTO> GetCurrentUserRaports(int userId)
        {
            var raportsList = await _raportrepo.GetAll();
            var raportList = new List<Raports>();
            var finalRaportList = new List<RaportDTO>();
            var projectList = await _projectrepo.GetAll();
            
            var weekList = await _weekrepo.GetAll();
            var user = await _userrepo.GetSingleEntity(x => x.Id == userId);
            var separator = " ";
            string name = string.Concat(user.Name, separator);
            string finalName = string.Concat(name, user.Surname);
            
            foreach(Raports raport in raportsList)
            {
                if (raport.UserId == userId)
                    raportList.Add(raport);
            }


            foreach(Raports raport in raportList)
            {
                var finalProjectListForModel = new List<ProjectDTO>();
                var finalWeekList = new List<WeekDTO>();
                foreach (Week week in weekList)
                {
                    if (week.RaportId == raport.Id)
                    {
                        var finalProjectList = new List<ProjectDTO>();
                        foreach (Project project in projectList)
                        {
                            if (project.WeekId == week.Id)
                            {
                                finalProjectList.Add(new ProjectDTO
                                {
                                    Name = project.Name,
                                    WorkedHours = project.WorkedHours
                                });
                            }
                        }
                        finalWeekList.Add(new WeekDTO
                        {
                            WeekNumber = week.WeekNumber,
                            WorkedHours = week.WorkedHours,
                            HoursInWeek = week.HoursInWeek,
                            Projects = finalProjectList 
                        });

                        foreach (ProjectDTO project in finalProjectList)
                        {
                            if (!finalProjectListForModel.Any(name => name.Name == project.Name))
                            {
                                finalProjectListForModel.Add(new ProjectDTO()
                                {
                                    Name = project.Name,
                                    WorkedHours = project.WorkedHours
                                });
                            }
                            else
                            {
                                var temp = finalProjectListForModel.Find(name => name.Name == project.Name);
                                temp.WorkedHours = temp.WorkedHours + project.WorkedHours;
                            }
                        }
                    }
                }
                var raportDTO = new RaportDTO()
                {
                    Id = raport.Id,
                    Email = user.Email,
                    User = finalName,
                    Month = raport.Month,
                    HoursInMonth = raport.HoursInMonth,
                    WorkedHours = raport.WorkedHours,
                    ProjetList = finalProjectListForModel,
                    WeekList = finalWeekList,
                    IsClosed = raport.IsClosed,
                    IsAccepted = raport.IsAccepted
                };
                finalRaportList.Add(raportDTO);
            }
            var final = new RaportListDTO()
            {
                RaportList = finalRaportList
            };
            return final;
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
                var week = await _weekrepo.GetSingleEntity(x => x.Id == projectVM.WeekId);
                week.WorkedHours = week.WorkedHours + projectVM.WorkedHours;
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == week.RaportId);
                raport.WorkedHours = raport.WorkedHours + projectVM.WorkedHours;
                await _raportrepo.Patch(raport); 
                await _weekrepo.Patch(week);
                
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projectList = await _projectrepo.GetAll();
            return projectList;
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
                var hours = project.WorkedHours;
                if (project == null)
                    result.Response = "Project not found";
                if (projectVM.Name != null)
                    project.Name = projectVM.Name;
                if (projectVM.WorkedHours != null)
                    project.WorkedHours = projectVM.WorkedHours;
                await _projectrepo.Patch(project);

                var week = await _weekrepo.GetSingleEntity(x => x.Id == projectVM.WeekId);
                week.WorkedHours = week.WorkedHours - hours +  projectVM.WorkedHours;
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == week.RaportId);
                raport.WorkedHours = raport.WorkedHours - hours + projectVM.WorkedHours;
                await _raportrepo.Patch(raport);
                await _weekrepo.Patch(week);
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

        public async Task<List<Week>> GetAllWeeks()
        {
            var weekList = await _weekrepo.GetAll();
            return weekList;
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
