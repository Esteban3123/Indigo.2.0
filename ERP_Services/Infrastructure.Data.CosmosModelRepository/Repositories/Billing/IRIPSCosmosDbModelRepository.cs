using Domain.Billing.POCO.E_RIPS;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
