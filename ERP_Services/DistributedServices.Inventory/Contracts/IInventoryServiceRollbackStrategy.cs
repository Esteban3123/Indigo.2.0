using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    public interface IInventoryServiceRollbackStrategy
    {
        Task<bool> ExecuteRollbackAsync(string rollbackPayload, string  entityName);
    }
}
