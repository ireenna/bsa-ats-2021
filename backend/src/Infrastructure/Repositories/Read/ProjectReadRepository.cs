using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Read;
using Domain.Entities;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Repositories.Read
{
    public class ProjectReadRepository : ReadRepository<Project>, IProjectReadRepository
    {
        public ProjectReadRepository(IConnectionFactory connectionFactory) : base("Projects", connectionFactory) { }

        public async Task<List<Project>> GetByCompanyIdAsync(string companyId)
        {
            var connection = _connectionFactory.GetSqlConnection();
            await connection.OpenAsync();
            string sql = $"SELECT * FROM {_tableName} WHERE CompanyId = '{companyId}'";

            var projects = (await connection.QueryAsync<Project>(sql)).ToList();

            await connection.CloseAsync();
            return projects;
        }
    }
}
