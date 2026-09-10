///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Angi Camila Duran Vargas
/// Created          : 27-07-2023
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
    public interface IInventoryServiceProductInTransitDetail
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ProductInTransitId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ProductInTransitDetail> GetProductInTransitDetailByProductInTransitId(int ProductInTransitId);

    }
}
