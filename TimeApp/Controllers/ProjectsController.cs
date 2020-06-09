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
    public class ProjectsController : ControllerBase
    {

        private readonly IRaportService _raportService;
        public ProjectsController(IRaportService raportService)
        {
            _raportService = raportService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProjectsTotal()
        {
            var projectList = await _raportService.GetAllProjectsTotal();
            if (projectList == null)
                return BadRequest("No projects to show");
            return Ok(projectList);
        }
    }
}