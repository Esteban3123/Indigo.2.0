using Domain.Base.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceSurgicalPackageProcess
    {
        [OperationContract]
        ActionResult<string> AllSurgicalPackageTransaction(AllSurgicalPackageProcessWrapper objParams, AuditMessage audit, DateTime serverDate, string container);
    }
}
