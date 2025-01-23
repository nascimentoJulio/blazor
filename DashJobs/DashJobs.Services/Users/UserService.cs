using Dashclass.Constants;
using Dashclass.Model;
using DashJobs.Repository.Session;
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
        private readonly ISessionRepository _sessionRepository;

        public UserService(IUsersRepository usersRepository, ISessionRepository sessionRepository)
        {
            _usersRepository = usersRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                var newPassword = HashPassword(createUserDto.Password);
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

        public async Task<Guid> Login(string email, string password)
        {
            try
            {
                var user = await _usersRepository.GetUserByEmail(email);

                if (user == null)
                    throw new Exception("Usuário ou senha incorreta");

                if (!ComparePassword(user.Password, password))
                    throw new Exception("Usuário ou senha incorreta");

                await _sessionRepository.ExpiressSessions(user.Id);
                var sessionCode = Guid.NewGuid();
                await _sessionRepository.CreateSession(user.Id, sessionCode);

                return sessionCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string HashPassword(string password)
        {
            return PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Medium);
        }

        private bool ComparePassword(string userPassword, string requestPassword)
        {
            var hashedPassword = HashPassword(requestPassword);
            return hashedPassword.Equals(userPassword);
        }

    }
}
