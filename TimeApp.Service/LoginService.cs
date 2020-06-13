using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _userrepo;
        public LoginService(IRepository<User> userrepo)
        {
            _userrepo = userrepo;
        }

        public async Task<string> Login(LoginVM loginVM)
        {
            var user = await _userrepo.GetSingleEntity(x => x.Email == loginVM.Email && x.Password == loginVM.Password);
            if (user == null || user.IsActive == false)
                return null;
            return user.Status;
        }
    }
}
