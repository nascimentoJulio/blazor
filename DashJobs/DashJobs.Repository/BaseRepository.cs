using Dapper;

using System.Data;


namespace DashJobs.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        public readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Delete(string query, object parameters)
        {
            return await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<T>> GetAll(string query, object parameters)
        {
            return await _connection.QueryAsync<T>(query, parameters);
        }

        public async Task<T> GetById(string query, object parameters)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<int> Insert(string query, object parameters)
        {
            return await _connection.ExecuteAsync(query);
        }

        public async Task<int> Update(string query, object parameters)
        {
            return await _connection.ExecuteAsync(query, parameters);
        }
    }
}
