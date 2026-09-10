//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Carlos Ernesto Cordoba
// Created          : 08-01-2015
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

namespace Application.Inventory.PurchaseOrderDetail
{
    public interface IPurchaseOrderDetailAdminService : IDisposable
    {
        /// <summary>
        /// obtiene los detalles de la orden de compra por el id del proveedor  y la linea de distribucion
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierDistributionLineId"></param>
        /// <returns></returns>
        List<Domain.Entities.PurchaseOrderDetail> GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId);
    }
}
