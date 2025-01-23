using DashJobs.Repository.Models;

namespace DashJobs.Repository.Session
{
    public interface ISessionRepository
    {

        Task ExpiressSessions(Guid userId);


        Task CreateSession(Guid userId, Guid session_code);
    }
}
