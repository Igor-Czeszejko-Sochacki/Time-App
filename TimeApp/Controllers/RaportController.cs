﻿using System;
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
    public class RaportController : ControllerBase
    {
        private readonly IRaportService _raportService;
        public RaportController(IRaportService raportService)
        {
            _raportService = raportService;
        }

        [HttpPost("AddRaport")]
        public async Task<IActionResult> AddRaport(int userId)
        {
            var result = await _raportService.AddRaport(userId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Raport was added");
        }

        [HttpPatch("PatchClosedStatus")]
        public async Task<IActionResult> PatchClosedStatus(int raportId, bool closedStatus)
        {
            var result = await _raportService.PatchClosedStatus(raportId, closedStatus);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Closed status was patched");
        }

        [HttpPatch("PatchAcceptedStatus")]
        public async Task<IActionResult> PatchAcceptedStatus(int raportId, bool acceptedStatus)
        {
            var result = await _raportService.PatchAcceptedStatus(raportId, acceptedStatus);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Accepted status was patched");
        }

        [HttpGet("GetAllRaports")]
        public async Task<IActionResult> GetAllRaports()
        {
            var raportList = await _raportService.GetAllRaports();
            if (raportList == null)
                return BadRequest("No raports to show");
            return Ok(raportList);
        }


        [HttpGet("GetCurrentUserRaports")]
        public async Task<IActionResult> GetCurrentUserRaports(int userId)
        {
            var raportList = await _raportService.GetCurrentUserRaports(userId);
            if (raportList == null)
                return BadRequest("User has no raports");
            return Ok(raportList);
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectVM projectVM)
        {
            var result = await _raportService.AddProject(projectVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was added");
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectList = await _raportService.GetAllProjects();
            if (projectList == null)
                return BadRequest("No projects to show");
            return Ok(projectList);
        }

        [HttpPatch("PatchProject")]
        public async Task<IActionResult> PatchProject(int projectId, ProjectVM projectVM)
        {
            var result = await _raportService.PatchProject(projectId,projectVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was patched");
        }

        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var result = await _raportService.DeleteProject(projectId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Project was deleted");
        }


        [HttpPost("AddWeek")]
        public async Task<IActionResult> AddWeek(WeekVM weekVm)
        {
            var result = await _raportService.AddWeek(weekVm);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Week was added");
        }

        [HttpGet("GetAllWeeks")]
        public async Task<IActionResult> GetAllWeeks()
        {
            var weekList = await _raportService.GetAllWeeks();
            if (weekList == null)
                return BadRequest("No weeks to show");
            return Ok(weekList);
        }

        [HttpPatch("PatchWeek")]
        public async Task<IActionResult> PatchWeek(int weekId, WeekVM weekVm)
        {
            var result = await _raportService.PatchWeek(weekId, weekVm);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Week was patched");
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