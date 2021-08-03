using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Domain.Common;
namespace Domain.Interfaces
{
    public interface IElasticWriteRepository<T> : IWriteRepository<T> where T : Entity
    {
        Task InsertBulkAsync(IEnumerable<T> bulk);
        Task UpdateAsyncPartially(string id, ExpandoObject dynamicUpdate);
    }
}