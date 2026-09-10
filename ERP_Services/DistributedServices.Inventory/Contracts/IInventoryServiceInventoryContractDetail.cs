///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Ernesto Cordoba
/// Created          : 08-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceInventoryContractDetail
    {
        /// <summary>
        /// obtiene los detalles del contrato por el id del proveedor  y la linea de distribucion
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierDistributionLineId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.InventoryContractDetail> GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId, int contractType);
    }
}
