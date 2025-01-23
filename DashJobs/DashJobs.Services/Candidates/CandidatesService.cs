using Dashclass.Model;
using DashJobs.Repository.Candidates;

namespace DashJobs.Services.Candidates
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ICandidatesRepository _candidatesRepository;

        public CandidatesService(ICandidatesRepository candidatesRepository)
        {
            _candidatesRepository = candidatesRepository;
        }
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await _candidatesRepository.GetCandidates();
        }
    }
}
