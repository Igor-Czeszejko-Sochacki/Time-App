using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeApp.Model;
using TimeApp.Model.DbModels;
using TimeApp.Model.Request;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userrepo;
        private readonly IRepository<Raports> _raportrepo;
        private readonly IRepository<Week> _weekrepo;
        public UserService(IRepository<User> userrepo, IRepository<Raports> raportrepo, IRepository<Week> weekrepo)
        {
            _userrepo = userrepo;
            _raportrepo = raportrepo;
            _weekrepo = weekrepo;
        }

        public async Task<ResultDTO> AddUser(UserWithoutIdVM userVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = new User
                {
                    Name = userVM.Name,
                    Surname = userVM.Surname,
                    Email = userVM.Email,
                    Password = userVM.Password,
                    Status = userVM.Status,
                    IsActive = true
                };
                await _userrepo.Add(user);

                var raport = new Raports
                {
                    Month = "Czerwiec 2020",
                    HoursInMonth = 176,
                    UserId = user.Id
                };
                await _raportrepo.Add(raport);

                for (int i=1; i<5; i++)
                {
                    await _weekrepo.Add(new Week
                    {
                        WeekNumber = i,
                        HoursInWeek = 40,
                        RaportId = raport.Id                    
                    });
                }
                await _weekrepo.Add(new Week
                {
                    WeekNumber = 5,
                    HoursInWeek = 16,
                    RaportId = raport.Id
                });

            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<ResultDTO> PatchUser(int userId, UserWithoutIdVM userWithoutIdVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = await _userrepo.GetSingleEntity(x => x.Id == userId);
                if (user == null)
                    result.Response = "User not found";
                if (userWithoutIdVM.Name != null)
                    user.Name = userWithoutIdVM.Name;
                if (userWithoutIdVM.Surname != null)
                    user.Surname = userWithoutIdVM.Surname;
                if (userWithoutIdVM.Email != null)
                    user.Email = userWithoutIdVM.Email;
                if (userWithoutIdVM.Password != null)
                    user.Password = userWithoutIdVM.Password;
                if (userWithoutIdVM.Status != null)
                    user.Status = userWithoutIdVM.Status;
                await _userrepo.Patch(user);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
       

        public async Task<ResultDTO> DeactivateUser(int userId, bool activeStatus)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = await _userrepo.GetSingleEntity(x => x.Id == userId);
                if (user == null)
                    result.Response = "User not found";
                user.IsActive = activeStatus;
                await _userrepo.Patch(user);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var userList = await _userrepo.GetAll();
            return userList;
        }
    }
}
