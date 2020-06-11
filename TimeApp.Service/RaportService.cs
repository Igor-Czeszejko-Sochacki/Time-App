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
        private readonly IRepository<MainProject> _mainprojectrepo;
        public RaportService(IRepository<Raports> raportrepo, IRepository<Project> projectrepo, IRepository<Week> weekrepo, IRepository<User> userrepo, IRepository<MainProject> mainprojectrepo)
        {
            _raportrepo = raportrepo;
            _projectrepo = projectrepo;
            _weekrepo = weekrepo;
            _userrepo = userrepo;
            _mainprojectrepo = mainprojectrepo;
        }
        public async Task<ResultDTO> AddRaport(int userId,string monthName)
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
                    Month = monthName
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> AddProject(ProjectVM projectVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                bool exists = false;
                var projectsList = await _mainprojectrepo.GetAll();
                foreach (MainProject project in projectsList)
                {
                    if (project.Name == projectVM.Name)
                    {
                        exists = true;
                    }
                }
                if (projectVM.WorkedHours >= 0 && exists == true)
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
                else
                    result.Response = "Cant add the project";
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
        public async Task<ResultDTO> AddMainProject(string name)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _mainprojectrepo.Add(new MainProject
                {
                   Name = name
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        
        public async Task<ResultDTO> Close(int raportId)
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
                raport.IsClosed = true;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> Reject(int raportId)
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
                raport.IsAccepted = false;
                raport.IsClosed = false;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> Accept(int raportId)
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
                raport.IsAccepted = true;
                await _raportrepo.Patch(raport);
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
                bool exists = false;
                var projectsList = await _mainprojectrepo.GetAll();
                foreach (MainProject mainProject in projectsList)
                {
                    if (mainProject.Name == projectVM.Name)
                    {
                        exists = true;
                    }
                }
                var project = await _projectrepo.GetSingleEntity(x => x.Id == projectId);
                var hours = project.WorkedHours;
                if (project == null)
                    result.Response = "Project not found";
                if (projectVM.Name != null && exists == true)
                    project.Name = projectVM.Name;
                if (projectVM.WorkedHours > project.WorkedHours)
                    project.WorkedHours = projectVM.WorkedHours;
                else if (projectVM.WorkedHours < project.WorkedHours)
                    result.Response = "Cant assign lower value";
                await _projectrepo.Patch(project);

                var week = await _weekrepo.GetSingleEntity(x => x.Id == projectVM.WeekId);
                week.WorkedHours = week.WorkedHours - hours + projectVM.WorkedHours;
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

        public async Task<RaportListDTO> GetAllRaports()
        {
            var separator = " ";
            var raportsList = await _raportrepo.GetAll();
            var projectList = await _projectrepo.GetAll();
            var weekList = await _weekrepo.GetAll();
            var userList = await _userrepo.GetAll();
            var finalRaportList = new List<RaportDTO>();
            foreach (User user in userList)
            {
                var raportList = new List<Raports>();
                string name = string.Concat(user.Name, separator);
                string finalName = string.Concat(name, user.Surname);
                foreach (Raports raport in raportsList)
                {
                    if (raport.UserId == user.Id)
                        raportList.Add(raport);
                }

                foreach (Raports raport in raportList)
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
                                Week = week.WeekNumber,
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
                        UserEmail = user.Email,
                        User = finalName,
                        Month = raport.Month,
                        HoursInMonth = raport.HoursInMonth,
                        WorkedHours = raport.WorkedHours,
                        Projects = finalProjectListForModel,
                        Weeks = finalWeekList,
                        IsClosed = raport.IsClosed,
                        IsAccepted = raport.IsAccepted
                    };
                    finalRaportList.Add(raportDTO);
                }
            }          
            var final = new RaportListDTO()
            {
                Raports = finalRaportList
            };
            return final;
        }

        public async Task<RaportListDTO> GetCurrentUserRaports(string userEmail)
        {
            var user = await _userrepo.GetSingleEntity(x => x.Email == userEmail);
            var raportsList = await _raportrepo.GetAll();
            var projectList = await _projectrepo.GetAll();
            var weekList = await _weekrepo.GetAll();
            var raportList = new List<Raports>();
            var finalRaportList = new List<RaportDTO>();
       
            var separator = " ";
            string name = string.Concat(user.Name, separator);
            string finalName = string.Concat(name, user.Surname);
            
            foreach(Raports raport in raportsList)
            {
                if (raport.UserId == user.Id)
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
                            Week = week.WeekNumber,
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
                    UserEmail = user.Email,
                    User = finalName,
                    Month = raport.Month,
                    HoursInMonth = raport.HoursInMonth,
                    WorkedHours = raport.WorkedHours,
                    Projects = finalProjectListForModel,
                    Weeks = finalWeekList,
                    IsClosed = raport.IsClosed,
                    IsAccepted = raport.IsAccepted
                };
                finalRaportList.Add(raportDTO);
            }
            var final = new RaportListDTO()
            {
                Raports = finalRaportList
            };
            return final;
        }

        public async Task<RaportListDTO> GetClosedRaports()
        {
            var raportsList = await _raportrepo.GetAll();
            var projectList = await _projectrepo.GetAll();
            var weekList = await _weekrepo.GetAll();
            var raportList = new List<Raports>();
            var finalRaportList = new List<RaportDTO>();

            foreach (Raports raport in raportsList)
            {
                if (raport.IsClosed == true)
                    raportList.Add(raport);
            }
            foreach (Raports raport in raportList)
            {
                var user = await _userrepo.GetSingleEntity(x => x.Id == raport.UserId);
                var separator = " ";
                string name = string.Concat(user.Name, separator);
                string finalName = string.Concat(name, user.Surname);

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
                            Week = week.WeekNumber,
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
                    UserEmail = user.Email,
                    User = finalName,
                    Month = raport.Month,
                    HoursInMonth = raport.HoursInMonth,
                    WorkedHours = raport.WorkedHours,
                    Projects = finalProjectListForModel,
                    Weeks = finalWeekList,
                    IsClosed = raport.IsClosed,
                    IsAccepted = raport.IsAccepted
                };
                finalRaportList.Add(raportDTO);
            }
            var final = new RaportListDTO()
            {
                Raports = finalRaportList
            };
            return final;
        }
        public async Task<List<Project>> GetAllProjects()
        {
            var projectList = await _projectrepo.GetAll();
            return projectList;
        }

        public async Task<List<Week>> GetAllWeeks()
        {
            var weekList = await _weekrepo.GetAll();
            return weekList;
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
