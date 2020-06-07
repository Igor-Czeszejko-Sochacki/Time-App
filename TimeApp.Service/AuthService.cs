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
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userrepo;
        private readonly AppSettings _appSettings;
        public AuthService(IRepository<User> userrepo, IOptions<AppSettings> appSettings)
        {
            _userrepo = userrepo;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Login(LoginVM loginVM)
        {
            var user = await _userrepo.GetSingleEntity(x => x.Email == loginVM.Email && x.Password == loginVM.Password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userList = await _userrepo.GetAll();
            return userList;
        }


    }
}
