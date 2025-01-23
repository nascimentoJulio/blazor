using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Repository
{
    public interface IBaseRepository<T>
    {
        Task<int> Insert(string query, object[] parameters);
        
        Task<int> Update(string query, object[] parameters);

        Task<int> Delete(string query, object[] parameters);

        Task<IEnumerable<T>> GetAll(string query, object[] parameters);

        Task<T> GetById(string query, object[] parameters);
    }
}
