using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeApp.Service;

namespace TimeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {

        private readonly IProjectService _projectService;
        public projectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProjectsTotal()
        {
            var projectList = await _projectService.GetAllProjectsTotal();
            if (projectList == null)
                return BadRequest("No projects to show");
            return Ok(projectList);
        }
    }
}