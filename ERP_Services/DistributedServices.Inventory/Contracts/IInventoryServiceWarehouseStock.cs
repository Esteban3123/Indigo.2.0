///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez 
/// Created          : 06-06-2015
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
    public interface IInventoryServiceWarehouseStock
    {

        /// <summary>
        /// Lista los stock de almacen por id de almacen
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.WarehouseStock> ListWarehouseStocksByWarehouseId(int warehouseId);

        /// <summary>
        /// Guarda una lista de stock de almacen
        /// </summary>
        /// <param name="listWarhouseStock"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult SaveListWarehouseStock(List<Domain.Entities.WarehouseStock> listWarhouseStock, AuditMessage audit);

    }
}
