using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Generic
{
    public interface ICosmosDbRepository<T> where T : class
    {
        T GetById(string id);
        void Save(T item, string partitionKeyValue);
        IEnumerable<T> GetByFilter(string query, Dictionary<string, string> valuePairs);
        Task<IEnumerable<T>> GetByFilterAsync(string query, Dictionary<string, string> valuePairs);

        /// <summary>
        /// Upsert masivo en CosmosDB usando AllowBulkExecution. Tolera fallos por item.
        /// </summary>
        Task<BulkUpsertResult<T>> SaveBulkAsync(
            IEnumerable<(T item, string partitionKey)> items,
            IProgress<BulkProgress> progress = null,
            CancellationToken cancellationToken = default);
    }
}
