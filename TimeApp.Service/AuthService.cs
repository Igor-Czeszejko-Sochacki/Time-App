using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userrepo;
        public AuthService(IRepository<User> userrepo)
        {
            _userrepo = userrepo;
        }
        public async Task<ResultDTO> Login(LoginVM loginVM)
        {
            var result = new ResultDTO();
            var userList = await _userrepo.GetAll();
            foreach (User user in userList)
            {
                if (user.Email == loginVM.Email && user.Password == loginVM.Password)
                {
                    result.Response = user.Status;
                }
            }
            return result;
        }
    }
}
