using Dapper;
using DashJobs.Repository.Models;

using System.Data;

namespace DashJobs.Repository.Sessions
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
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
    }



}
