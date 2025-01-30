﻿using Dashclass.Constants;
using Dashclass.Model;
using DashJobs.Repository.Session;
using DashJobs.Repository.Users;
using DashJobs.Services.Dto;
using DashJobs.Services.Exceptions;
using Sodium;

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
            return PasswordHash.ArgonHashStringVerify(userPassword, requestPassword);
        }

        public async Task<bool> IsAuthenticated(string sessionCode, Roles role)
        {

            var session = await _sessionRepository.GetSession(sessionCode);
            if (session == null)
                throw new UnauthenticatedException();

            var user = await _usersRepository.GetUserById(session.UserId);
            if(user == null)
                throw new UnauthenticatedException();

            var isAdmin = user.Roles.Equals(Roles.ADMINISTRATIVE);
            bool isSessionExpired = (DateTime.UtcNow - session.CreatedAt).TotalHours >= 2;
            if (session.Expired || isSessionExpired)
                throw new UnauthenticatedException();

            if (!isAdmin || !user.Roles.Equals(role))
                throw new UnauthorizedException();


            return true;
        }
    }
}
