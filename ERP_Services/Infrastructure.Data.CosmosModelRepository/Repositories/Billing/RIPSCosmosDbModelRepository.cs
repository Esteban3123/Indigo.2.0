using Domain.Billing.POCO;
using Domain.Billing.POCO.E_RIPS;
using Infrastructure.CrossCutting.AzureBlobStorage;
using Infrastructure.CrossCutting.AzureBlobStorage.Factory;
using Infrastructure.CrossCutting.Base;
using Infrastructure.Data.CosmosModelRepository.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

    }
}
