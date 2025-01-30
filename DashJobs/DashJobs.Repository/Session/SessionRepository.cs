using Dapper;
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

        public async Task CreateSession(Guid userId, Guid sessionCode)
        {
            await _connection.ExecuteAsync(@"INSERT INTO sessions
                                (user_id, session_code)
                           VALUES
                                (@UserId,@SessionCode)", new { UserId = userId, SessionCode = sessionCode });
        }

        public async Task ExpiressSessions(Guid userId)
        {
            await _connection.ExecuteAsync(@"UPDATE sessions
                           SET expired = true
                           where user_id = @UserId", new { UserId = userId });
        }

        public async Task<Models.Session> GetSession(string sessionCode)
        {
           return await GetById(@"SELECT 
                                id as Id,
                                created_at as CreatedAt,
                                user_id as UserId,
                                expired as Expired,
                                session_code as SessionCode
                            FROM sessions
                            WHERE session_code = @SessionCode
                           ", new { SessionCode = sessionCode });
        }
    }
}