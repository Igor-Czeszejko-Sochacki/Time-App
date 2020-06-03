using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.Request;
using TimeApp.Model.Response;

namespace TimeApp.Service
{
    public interface IUserService
    {
        Task<ResultDTO> AddUser(UserWithoutIdVM userVM);
        Task<ResultDTO> PatchUser(int userId, UserWithoutIdVM userWithoutIdVM);
        Task<ResultDTO> PatchActiveStatus(int userId, bool activeStatus);
        Task<List<User>> GetAllUsers();
    }
}
