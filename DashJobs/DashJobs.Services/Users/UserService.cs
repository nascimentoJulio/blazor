using Dashclass.Constants;
using Dashclass.Model;
using DashJobs.Repository.Users;
using DashJobs.Services.Dto;
using Sodium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                var newPassword = hashPassword(createUserDto.Password);
                var role = createUserDto.IsAdmin ? Roles.ADMINISTRATIVE : Roles.RECRUITER;

                await _usersRepository.Insert(new User
                {
                    Name = createUserDto.Username,
                    Password = newPassword,
                    Email = createUserDto.Email,
                    Roles = role
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string hashPassword(string password)
        {
            return PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Medium);
        }

    }
}
