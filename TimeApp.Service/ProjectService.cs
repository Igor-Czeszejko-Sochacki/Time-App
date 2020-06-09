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
        public ProjectService(IRepository<Project> projectrepo)
        {
            _projectrepo = projectrepo;
        }

        public async Task<List<ProjectDTO>> GetAllProjectsTotal()
        {
            var projectList = await _projectrepo.GetAll();
            var finalList = new List<ProjectDTO>();
            foreach (Project project in projectList)
            {
                if (!finalList.Any(name => name.Name == project.Name))
                {
                    finalList.Add(new ProjectDTO()
                    {
                        Name = project.Name,
                        WorkedHours = project.WorkedHours
                    });
                }
                else
                {
                    var temp = finalList.Find(name => name.Name == project.Name);
                    temp.WorkedHours = temp.WorkedHours + project.WorkedHours;
                }
            }
            return finalList;
        }
    }
}
