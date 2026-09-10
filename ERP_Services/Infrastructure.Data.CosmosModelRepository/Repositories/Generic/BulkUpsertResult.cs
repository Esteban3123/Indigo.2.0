using System.Collections.Generic;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Generic
{
    public class BulkUpsertResult<T> where T : class
    {
        public List<BulkUpsertItemResult<T>> Succeeded { get; set; } = new List<BulkUpsertItemResult<T>>();
        public List<BulkUpsertItemResult<T>> Failed { get; set; } = new List<BulkUpsertItemResult<T>>();
        public int TotalProcessed => Succeeded.Count + Failed.Count;
    }

    public class BulkUpsertItemResult<T> where T : class
    {
        public T Item { get; set; }
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int? StatusCode { get; set; }
    }

    public class BulkProgress
    {
        public int Total { get; set; }
        public int Sent { get; set; }
        public int Succeeded { get; set; }
        public int Failed { get; set; }
    }
}
