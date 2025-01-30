
using DashJobs.Repository.Models;

namespace DashJobs.Repository.Session
{
    public interface ISessionRepository
    {

        Task ExpiressSessions(Guid userId);

        Task<Models.Session> GetSession(string sessionCode);

        Task CreateSession(Guid userId, Guid sessionCode);
    }
}
