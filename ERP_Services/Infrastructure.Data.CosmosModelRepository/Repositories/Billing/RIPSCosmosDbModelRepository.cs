using Domain.Billing.POCO;
using Domain.Billing.POCO.E_RIPS;
using Infrastructure.CrossCutting.AzureBlobStorage;
using Infrastructure.CrossCutting.AzureBlobStorage.Factory;
using Infrastructure.CrossCutting.Base;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using Infrastructure.Data.CosmosModelRepository.UnitOfWork;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Billing
{
    public class RIPSCosmosDbModelRepository :
         CosmosDbRepository<RIPSCosmosDbModel>, IRIPSCosmosDbModelRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IFactoryStorage _factoryStorage;
        private readonly IStorage _storage;

        public RIPSCosmosDbModelRepository(IUnitOfWork context,
                                           IFactoryStorage factoryStorage) : base(context)
        {
            this._context = context;
            this._factoryStorage = factoryStorage;
            this._storage = this._factoryStorage.CreateStorageControl();
        }

        /// <summary>
        /// Metodo que consulta un Json por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RIPSCosmosDbModel> GetJsonRIPSByIdAsync(string Id)
        {
            try
            {

                var results = await this.GetByFilterAsync(" SELECT * " +
                                    " FROM c WHERE c.id = @Id AND c.Container = @Container",
                                    new Dictionary<string, string> { { "@Id", Id }, { "@Container", ServerSessionValues.Current.CurrentContainer } });

                var query = results?.FirstOrDefault();
                if (query is null) return query;

                EnsureJsonRIPSIsRIPSModel(query);
                return query;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// consulta un json por numero de factura
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<RIPSCosmosDbModel> GetJsonRIPSByDocNumber(string docNumber)
        {
            try
            {
                var results = await this.GetByFilterAsync(
                    "SELECT * FROM c WHERE c.JsonRIPS.rips.numFactura = @NumFactura AND c.Container = @Container ORDER BY c._ts DESC",
                    new Dictionary<string, string> {
                        { "@NumFactura", docNumber },
                        { "@Container", ServerSessionValues.Current.CurrentContainer }
                    }
                );

                var query = results?.FirstOrDefault();
                if (query is null) return query;

                EnsureJsonRIPSIsRIPSModel(query);
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Garantiza que query.JsonRIPS tenga la estructura RIPSModel.
        /// Si viene de Cosmos como JObject, JsonElement u otro tipo, lo deserializa a RIPSModel.
        /// Si no está disponible y existe BlobUrl, lo obtiene del Blob Storage.
        /// </summary>
        private void EnsureJsonRIPSIsRIPSModel(RIPSCosmosDbModel query)
        {
            if (query is null) return;

            // Ya es RIPSModel (incluye subclases)
            if (query.JsonRIPS is RIPSModel) return;

            // Intentar obtener desde Blob si existe la URL
            if (!string.IsNullOrEmpty(query.BlobUrl))
            {
                query.JsonRIPS = GetDataFromBlob(query.BlobUrl);
                return;
            }

            return;
        }

        private RIPSModel GetDataFromBlob(string blobUrl)
        {
            try
            {
                string stringRipsJson = _storage.ReadFileFromBlobUrl(blobUrl);

                return JsonConvert.DeserializeObject<RIPSModel>(stringRipsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos del Blob: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Upsert masivo de envelopes RIPS usando Cosmos bulk mode. Partition key = id.
        /// </summary>
        public Task<BulkUpsertResult<RIPSCosmosDbModel>> UpsertManyAsync(
            IEnumerable<RIPSCosmosDbModel> envelopes,
            IProgress<BulkProgress> progress = null,
            CancellationToken cancellationToken = default)
        {
            var pairs = (envelopes ?? Enumerable.Empty<RIPSCosmosDbModel>())
                .Select(e => (item: e, partitionKey: e.id));

            return SaveBulkAsync(pairs, progress, cancellationToken);
        }

        /// <summary>
        /// Devuelve info mínima (id + _ts) de docs existentes filtrados por numFactura y container.
        /// Cosmos no soporta IN con parámetros nombrados; se hace en chunks usando ARRAY_CONTAINS.
        /// </summary>
        public async Task<Dictionary<string, RipsExistsInfo>> GetExistingByNumFacturaAsync(
            IEnumerable<string> numFacturas,
            string container)
        {
            var result = new Dictionary<string, RipsExistsInfo>(StringComparer.OrdinalIgnoreCase);

            var distinctFacturas = (numFacturas ?? Enumerable.Empty<string>())
                .Where(n => !string.IsNullOrEmpty(n))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (distinctFacturas.Count == 0) return result;

            this._context.VerifyConnectionCosmoDBContainer();

            const int chunkSize = 100;

            for (int i = 0; i < distinctFacturas.Count; i += chunkSize)
            {
                var chunk = distinctFacturas.Skip(i).Take(chunkSize).ToList();

                var query = new QueryDefinition(
                    "SELECT c.id, c.JsonRIPS.rips.numFactura AS numFactura, c._ts AS ts " +
                    "FROM c " +
                    "WHERE c.Container = @Container " +
                    "AND ARRAY_CONTAINS(@NumFacturas, c.JsonRIPS.rips.numFactura)")
                    .WithParameter("@Container", container ?? string.Empty)
                    .WithParameter("@NumFacturas", chunk);

                var iterator = _context.ContainerDB.GetItemQueryIterator<ExistsRow>(query);

                while (iterator.HasMoreResults)
                {
                    var page = await iterator.ReadNextAsync().ConfigureAwait(false);
                    foreach (var row in page)
                    {
                        if (string.IsNullOrEmpty(row.numFactura)) continue;

                        if (!result.ContainsKey(row.numFactura))
                        {
                            result[row.numFactura] = new RipsExistsInfo
                            {
                                Id = row.id,
                                NumFactura = row.numFactura,
                                Ts = row.ts
                            };
                        }
                        else if (row.ts > result[row.numFactura].Ts)
                        {
                            result[row.numFactura].Id = row.id;
                            result[row.numFactura].Ts = row.ts;
                        }
                    }
                }
            }

            return result;
        }

        private class ExistsRow
        {
            public string id { get; set; }
            public string numFactura { get; set; }
            public long ts { get; set; }
        }

    }
}
