using Infrastructure.Data.CosmosModelRepository.Context;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       CosmosDbContext DbContext { get;}

        Container ContainerDB { get; }

        Task<int> CompleteAsync();

        void VerifyConnectionCosmoDBContainer();

    }
}
