using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.Request;
using TimeApp.Model.Response;

namespace TimeApp.Service
{
    public interface IUserService
    {
        Task<ResultDTO> AddUser(UserWithoutIdVM userVM);
    }
}
