using Dashclass.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Repository.Candidates
{
    public class CandidatesRepository : BaseRepository<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await GetAll(@"SELECT * FROM candidates", null);
        }
    }
}
