using Dashclass.Constants;
using DashJobs.Services.Dto;
using System;


namespace DashJobs.Services.Users
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto createUserDto);

        Task<Guid> Login(string email, string password);

        Task<bool> IsAuthenticated(string sessionCode, Roles[] roles);
    }
}
