using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Context
{
   public interface ICosmosDbContext
    {
        Database GetDatabase(string databaseId);
        Database GetDatabaseBulk(string databaseId);
    }
}
