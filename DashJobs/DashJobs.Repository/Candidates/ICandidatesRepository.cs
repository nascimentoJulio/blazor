using Dashclass.Model;


namespace DashJobs.Repository.Candidates
{
    public interface ICandidatesRepository
    {
        Task<IEnumerable<Candidate>> GetCandidates();
    }
}
