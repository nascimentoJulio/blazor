using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Repository.Session
{
    public class SessionRepository : BaseRepository<Models.Session>, ISessionRepository
    {
        public SessionRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task CreateSession(Guid userId, string session_code)
        {
            throw new NotImplementedException();
        }

        public async Task ExpiressSessions(Guid userId)
        {
            await Update(@"UPDATE sessions
                           SET expired = true
                           where user_id = @UserId", new { UserId = userId });
        }
    }
}