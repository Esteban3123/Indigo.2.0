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
    public interface IInventoryServiceRemissionEntranceDetail
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.RemissionEntranceDetail> GetRemissionEntranceDetailByRemissionEntranceId(int RemissionEntranceId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="SupplierDistributionLineId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.RemissionEntranceDetail> ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId);
    }
}
