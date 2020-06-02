using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Service;

namespace TimeApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
