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
                return BadRequest("No users to show");
            return Ok(raportList);
        }

    }
}