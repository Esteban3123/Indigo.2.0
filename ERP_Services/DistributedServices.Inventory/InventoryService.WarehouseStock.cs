///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez 
/// Created          : 05-06-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;
using System.Runtime.Serialization;
using Application.Inventory.WarehouseStock;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {

        /// <summary>
        /// Lista los stock de almacen por id de almacen
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.WarehouseStock> ListWarehouseStocksByWarehouseId(int warehouseId)
        {
            using (var service = Container.Current.Resolve<IWarehouseStockAdminService>())
            {
                return service.ListWarehouseStocksByWarehouseId(warehouseId);
            }
            //return _warehouseStockAdminService.ListWarehouseStocksByWarehouseId(warehouseId);
        }

        /// <summary>
        /// Guarda o Actualiza un listado de Stock de almacen
        /// </summary>
        /// <param name="listWarhouseStock"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult SaveListWarehouseStock(List<Domain.Entities.WarehouseStock> listWarhouseStock, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseStockAdminService>())
            {               
                return service.SaveListWarehouseStock(listWarhouseStock, audit);
            }
            //return _warehouseStockAdminService.SaveListWarehouseStock(listWarhouseStock, audit);
        }

    }
}
