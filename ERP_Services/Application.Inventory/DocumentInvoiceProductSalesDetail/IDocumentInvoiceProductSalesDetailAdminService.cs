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

namespace Application.Inventory.DocumentInvoiceProductSalesDetail
{
    public interface IDocumentInvoiceProductSalesDetailAdminService : IDisposable
    {
        /// <summary>
        /// obtiene los detalles de la factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSalesId"></param>
        /// <returns></returns>
        List<Domain.Entities.DocumentInvoiceProductSalesDetail> GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(int DocumentInvoiceProductSalesId);

        /// <summary>
        /// funcion para importar remisiones de salida a el detalle de factura de producto
        /// </summary>
        /// <param name="ListIds"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> ImportRemissionOutput(List<int> ListIds, int ContractExternalClientId,int FunctionalUnitId ,int? DocumentInvoiceProductSalesId = null);

        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> CalculateRateProduct(List<Domain.Entities.DocumentInvoiceProductSalesDetail> ListDocumentInvoiceProductSalesDetail, int ContractExternalClientId, int FunctionalUnitId);
    }
}
