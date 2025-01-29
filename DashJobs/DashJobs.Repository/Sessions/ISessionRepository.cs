
namespace DashJobs.Repository.Sessions
{
    public interface ISessionRepository
    {
        Task ExpiressSessions(Guid userId);
        Task CreateSession(Guid userId, Guid sessionCode);
    }
}
