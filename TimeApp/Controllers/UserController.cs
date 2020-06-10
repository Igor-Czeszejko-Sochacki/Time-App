﻿using System;
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

    [Route("api/user")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Kierownik")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserWithoutIdVM userVM)
        {
            var result = await _userService.AddUser(userVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was added");
        }

        [HttpPatch("patchUser")]
        public async Task<IActionResult> PatchUser(int userId, UserWithoutIdVM userWithoutIdVM)
        {
            var result = await _userService.PatchUser(userId, userWithoutIdVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was patched");
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("deactivateUser")]
        public async Task<IActionResult> DeactivateUser(int userId, bool activeStatus)
        {
            var result = await _userService.DeactivateUser(userId, activeStatus);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User active status was patched");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _userService.GetAllUsers();
            if (userList == null)
                return BadRequest("No users to show");
            return Ok(userList);
        }
       
    }
}
