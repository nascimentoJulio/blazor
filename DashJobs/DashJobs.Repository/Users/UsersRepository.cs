using Dapper;
using Dashclass.Constants;
using Dashclass.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Repository.Users
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task Insert(User user)
        {
            await _connection.ExecuteAsync(@"INSERT INTO users
                                                (username, email, password, role)
                                             Values
                                                (@Username, @Email, @Password, @Role)", new
            {
                Username = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Roles
            });
        }
    }
}
