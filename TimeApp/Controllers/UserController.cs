using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Service;

namespace TimeApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserWithoutIdVM userVM)
        {
            var result = await _userService.AddUser(userVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was added");
        }

        [HttpPatch("PatchUser")]
        public async Task<IActionResult> PatchUser(int userId, UserWithoutIdVM userWithoutIdVM)
        {
            var result = await _userService.PatchUser(userId, userWithoutIdVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was patched");
        }
        [HttpPatch("PatchActiveStatus")]
        public async Task<IActionResult> PatchActiveStatus(int userId, bool activeStatus)
        {
            var result = await _userService.PatchActiveStatus(userId, activeStatus);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User active status was patched");
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _userService.GetAllUsers();
            if (userList == null)
                return BadRequest("No users to show");
            return Ok(userList);
        }
       
    }
}
