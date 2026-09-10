using Domain.Billing.POCO.E_RIPS;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Billing
{
    public interface IDocumentsAssociatedRIPSRepository : ICosmosDbRepository<DocumentsAssociatedRIPS>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cosmoDBId"></param>
        /// <returns></returns>
        Task<DocumentsAssociatedRIPS> GetDocumentsAssociatedRIPSByRIPSId(string cosmoDBId);
    }
}
