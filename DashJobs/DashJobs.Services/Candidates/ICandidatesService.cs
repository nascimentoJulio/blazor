using Dashclass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Services.Candidates
{
    public interface ICandidatesService
    {
        Task<IEnumerable<Candidate>> GetCandidates();
    }
}

