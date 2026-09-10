///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Rafael Eduardo Patiño Cabrera
/// Created          : 08-05-2015
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
    public interface IInventoryServiceLoanMerchandiseDevolutionDetail
    {
        /// <summary>
        /// obtiene los detalles del prestamo
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.LoanMerchandiseDevolutionDetail> ListLoanMerchandiseDevolutionDetailByIdLoanMerchandiseDevolution(int IdLoanMerchandiseDevolution);
    }
}
