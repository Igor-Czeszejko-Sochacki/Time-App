using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.Request;
using TimeApp.Model.Response;

namespace TimeApp.Service
{
    public interface IAuthService
    {
        Task<User> Login(LoginVM loginVM);
        Task<List<User>> GetAllUsers();
    }
}
