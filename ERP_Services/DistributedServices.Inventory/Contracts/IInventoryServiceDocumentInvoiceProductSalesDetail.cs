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
    public interface IInventoryServiceDocumentInvoiceProductSalesDetail
    {
        /// <summary>
        /// obtiene los detalles de la factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSalesId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.DocumentInvoiceProductSalesDetail> GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(int DocumentInvoiceProductSalesId);

        [OperationContract]
        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> ImportRemissionOutput(List<int> ListIds, int ContractExternalClientId, int FuntionalUnitId, int? DocumentInvoiceProductSalesId = null);

        [OperationContract]
        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> CalculateRateProduct(List<Domain.Entities.DocumentInvoiceProductSalesDetail> ListDocumentInvoiceProductSalesDetail, int ContractExternalClientId, int FunctionalUnitId);


    }
}
