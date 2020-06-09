using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeApp.Model.Request;
using TimeApp.Service;

namespace TimeApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _authService;
        public LoginController(ILoginService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            //var result = await _authService.Login(loginVM);
            //if (result.Response == null)
            //    return BadRequest(result);
            //return Ok(result.Response);

            var user = await _authService.Login(loginVM);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }


        [HttpGet("GetAllUsersAuth")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _authService.GetAllUsers();
            if (userList == null)
                return BadRequest("No users to show");
            return Ok(userList);
        }
    }
}