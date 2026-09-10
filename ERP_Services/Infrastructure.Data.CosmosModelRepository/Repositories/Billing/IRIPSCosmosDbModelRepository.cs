using Domain.Billing.POCO.E_RIPS;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Billing
{
    public interface IRIPSCosmosDbModelRepository : ICosmosDbRepository<RIPSCosmosDbModel>
    {
        /// <summary>
        /// Metodo que consulta un Json por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RIPSCosmosDbModel> GetJsonRIPSByIdAsync(string Id);
        Task<RIPSCosmosDbModel> GetJsonRIPSByDocNumber(string docNumber);

        /// <summary>
        /// Upsert masivo de envelopes RIPS usando Cosmos bulk mode.
        /// </summary>
        Task<BulkUpsertResult<RIPSCosmosDbModel>> UpsertManyAsync(
            IEnumerable<RIPSCosmosDbModel> envelopes,
            IProgress<BulkProgress> progress = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Devuelve info mínima (id + _ts) de docs existentes filtrados por numFactura y container.
        /// Clave del diccionario: numFactura.
        /// </summary>
        Task<Dictionary<string, RipsExistsInfo>> GetExistingByNumFacturaAsync(
            IEnumerable<string> numFacturas,
            string container);
    }
}
