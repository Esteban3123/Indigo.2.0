using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Generic
{
    public interface ICosmosDbRepository<T>
    {
        T GetById(string id);
        void Save(T item, string partitionKeyValue);
        IEnumerable<T> GetByFilter(string query, Dictionary<string, string> valuePairs);
        Task<IEnumerable<T>> GetByFilterAsync(string query, Dictionary<string, string> valuePairs);
    }
}
