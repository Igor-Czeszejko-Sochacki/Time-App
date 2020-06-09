using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeApp.Model.Request;
using TimeApp.Service;

namespace TimeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaportsController : ControllerBase
    {
        private readonly IRaportService _raportService;
        public RaportsController(IRaportService raportService)
        {
            _raportService = raportService;
        }

        [HttpPost("AddRaport")]
        public async Task<IActionResult> AddRaport(int userId, string monthName)
        {
            var result = await _raportService.AddRaport(userId,monthName);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Raport was added");
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectVM projectVM)
        {
            var result = await _raportService.AddProject(projectVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was added");
        }

        [HttpPost("AddWeek")]
        public async Task<IActionResult> AddWeek(WeekVM weekVm)
        {
            var result = await _raportService.AddWeek(weekVm);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Week was added");
        }

        [HttpPost("AddMainProject")]
        public async Task<IActionResult> AddMainProject(string name)
        {
            var result = await _raportService.AddMainProject(name);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Main project was added");
        }
        [HttpPatch("Close")]
        public async Task<IActionResult> Close(int raportId)
        {
            var result = await _raportService.Close(raportId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Closed status was patched");
        }

        [HttpPatch("Reject")]
        public async Task<IActionResult> Reject(int raportId)
        {
            var result = await _raportService.Reject(raportId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Raport was rejected");
        }

        [HttpPatch("Accept")]
        public async Task<IActionResult> Accept(int raportId)
        {
            var result = await _raportService.Accept(raportId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Raport was accepted");
        }

        [HttpPatch("PatchProject")]
        public async Task<IActionResult> PatchProject(int projectId, ProjectVM projectVM)
        {
            var result = await _raportService.PatchProject(projectId, projectVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was patched");
        }

        [HttpPatch("PatchWeek")]
        public async Task<IActionResult> PatchWeek(int weekId, WeekVM weekVm)
        {
            var result = await _raportService.PatchWeek(weekId, weekVm);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Week was patched");
        }

        [HttpGet("GetAllRaports")]
        public async Task<IActionResult> GetAllRaports()
        {
            var raportList = await _raportService.GetAllRaports();
            if (raportList == null)
                return BadRequest("No raports to show");
            return Ok(raportList);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserRaports(int userId)
        {
            var raportList = await _raportService.GetCurrentUserRaports(userId);
            if (raportList == null)
                return BadRequest("User has no raports");
            return Ok(raportList);
        }

        [HttpGet("GetClosedRaports")]
        public async Task<IActionResult> GetClosedRaports(int userId)
        {
            var projectList = await _raportService.GetClosedRaports(userId);
            if (projectList == null)
                return BadRequest("No projects to show");
            return Ok(projectList);
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectList = await _raportService.GetAllProjects();
            if (projectList == null)
                return BadRequest("No projects to show");
            return Ok(projectList);
        }

        

        [HttpGet("GetAllWeeks")]
        public async Task<IActionResult> GetAllWeeks()
        {
            var weekList = await _raportService.GetAllWeeks();
            if (weekList == null)
                return BadRequest("No weeks to show");
            return Ok(weekList);
        }

        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var result = await _raportService.DeleteProject(projectId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was deleted");
        }

        [HttpDelete("DeleteWeek")]
        public async Task<IActionResult> DeleteWeek(int weekId)
        {
            var result = await _raportService.DeleteWeek(weekId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Week was deleted");
        }
    }
}