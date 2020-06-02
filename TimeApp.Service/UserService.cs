using System;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userrepo;

        public UserService(IRepository<User> userrepo)
        {
            _userrepo = userrepo;
        }

        public async Task<ResultDTO> AddUser(UserWithoutIdVM userVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _userrepo.Add(new User
                {
                    Name = userVM.Name,
                    Surname = userVM.Surname,
                    Email = userVM.Email,
                    Password =  userVM.Password,
                    Status = userVM.Status,
                    IsActive = true
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
    }
}
