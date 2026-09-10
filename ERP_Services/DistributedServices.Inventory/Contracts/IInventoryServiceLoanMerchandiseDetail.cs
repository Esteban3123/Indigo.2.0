///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Rafael Eduardo Patiño Cabrera
/// Created          : 24-04-2015
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
   public  interface IInventoryServiceLoanMerchandiseDetail
    {
        /// <summary>
        /// obtiene los detalles del prestamo
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.LoanMerchandiseDetail> ListLoanMerchandiseDetailByIdLoanMerchandise(int IdLoanMerchandise, bool isDevolution );
    }
}
