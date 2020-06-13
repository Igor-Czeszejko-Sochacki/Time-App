using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.DbModels;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectrepo;
        private readonly IRepository<MainProject> _mainprojectrepo;
        public ProjectService(IRepository<Project> projectrepo, IRepository<MainProject> mainprojectrepo)
        {
            _projectrepo = projectrepo;
            _mainprojectrepo = mainprojectrepo;
        }

        public async Task<List<ProjectDTO>> GetAllProjectsTotal()
        {
            var projectList = await _projectrepo.GetAll();
            var finalList = new List<ProjectDTO>();
            var mainProjectList = await _mainprojectrepo.GetAll();
            foreach (MainProject mainProject in mainProjectList)
            {
                finalList.Add(new ProjectDTO()
                {
                    Name = mainProject.Name,
                    WorkedHours = 0
                });
            }

            foreach (Project project in projectList)
            {

                if (finalList.Any(name => name.Name == project.Name))
                {
                    var temp = finalList.Find(name => name.Name == project.Name);
                    temp.WorkedHours = temp.WorkedHours + project.WorkedHours;
                }
            }
            return finalList;
        }
    }
}
