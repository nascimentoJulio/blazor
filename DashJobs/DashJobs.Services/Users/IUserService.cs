using DashJobs.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Services.Users
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto createUserDto);
    }
}
