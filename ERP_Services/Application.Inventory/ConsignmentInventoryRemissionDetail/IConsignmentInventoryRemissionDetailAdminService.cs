//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Miguel Angel Fonseca
// Created          : 2017-12-12
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

#region Imports

using System;
using System.Collections.Generic;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemissionDetail
{
    public interface IConsignmentInventoryRemissionDetailAdminService : IDisposable
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        List<Domain.Entities.ConsignmentInventoryRemissionDetail> GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId);

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        List<Domain.Entities.ConsignmentInventoryRemissionDetail> ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId);
    }
}