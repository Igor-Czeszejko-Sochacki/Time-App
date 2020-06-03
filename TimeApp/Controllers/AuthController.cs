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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet ("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = await _authService.Login(loginVM);
            if (result.Response == null)
                return BadRequest(result);
            return Ok(result.Response);
        }
    }
}