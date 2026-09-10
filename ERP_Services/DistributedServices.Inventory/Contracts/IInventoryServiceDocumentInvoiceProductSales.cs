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
    public interface IInventoryServiceDocumentInvoiceProductSales
    {
        /// <summary>
        /// obtiene una factura por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una factura por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesById(int id);

        /// <summary>
        /// guarda una factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSales"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales DocumentInvoiceProductSales, AuditMessage audit, long idSequense, Domain.Entities.BillingSequence sequenceC);

        /// <summary>
        /// guardar y confirmar una factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSales"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveAndConfirmbDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales DocumentInvoiceProductSales, Domain.Entities.CashReceipts cashReceipts, long idSequense, SessionValues session, Domain.Entities.BillingSequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);

        /// <summary>
        /// Sets the products product invoice import file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="wareHouseId">The ware house identifier.</param>
        /// <param name="operatingUnitId">The operating unit identifier.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> SetProductsProductInvoiceImportFile(List<ImportFileRow> data, int wareHouseId, int operatingUnitId, SessionValues session);

        /// <summary>
        /// Reversa un documento de factura de productos
        /// </summary>
        /// <param name="documentInvoiceProductSales">The document invoice product sales.</param>
        /// <param name="reversalReasonId">The reversal reason identifier.</param>
        /// <param name="reversalReasonDescription">The reversal reason description.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult ReverseDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, int reversalReasonId, string reversalReasonDescription, SessionValues session);
    }
}