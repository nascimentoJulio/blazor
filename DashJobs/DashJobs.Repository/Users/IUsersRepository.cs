using Dashclass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Repository.Users
{
    public interface IUsersRepository
    {
        Task Insert(User user);
    }
}
