
namespace DashJobs.Repository.Models
{
    public class Session
    {
        public Guid Id { get; set; }

        public string SessionCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public bool Expired { get; set; }
    }
}
