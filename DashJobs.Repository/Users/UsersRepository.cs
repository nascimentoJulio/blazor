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

        public async Task<User> GetUserByEmail(string email)
        {
            return await GetById(@"SELECT
                                    id as Id, 
                                    created_at as CreatedAt, 
                                    username as Username, 
                                    email as Email, 
                                    password as Password, 
                                    role as Roles
                                   FROM users
                                   WHERE Email = @Email", new { Email = email });
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await GetById(@"SELECT
                                    id as Id, 
                                    created_at as CreatedAt, 
                                    username as Username, 
                                    email as Email, 
                                    password as Password, 
                                    role as Roles
                                   FROM users
                                   WHERE Id = @Id", new { Id = userId });
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
