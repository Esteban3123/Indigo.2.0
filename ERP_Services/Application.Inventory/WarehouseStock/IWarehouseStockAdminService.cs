//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Juan Carlos Bermudez
// Created          : 03-06-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.WarehouseStock
{
    public interface IWarehouseStockAdminService : IDisposable
    {

        /// <summary>
        /// Lista los stock de almacen por id de almacen
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        List<Domain.Entities.WarehouseStock> ListWarehouseStocksByWarehouseId(int warehouseId);

        /// <summary>
        /// Guarda una lista de stock de almacen
        /// </summary>
        /// <param name="listWarhouseStock"></param>
        /// <returns></returns>
        ActionResult SaveListWarehouseStock(List<Domain.Entities.WarehouseStock> listWarhouseStock, AuditMessage audit);

    }
}
