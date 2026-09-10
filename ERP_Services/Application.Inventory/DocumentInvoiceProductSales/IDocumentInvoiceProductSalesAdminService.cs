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
using Domain.Entities;

namespace Application.Inventory.DocumentInvoiceProductSales
{
    public interface IDocumentInvoiceProductSalesAdminService : IDisposable
    {
        /// <summary>
        /// obtiene una factura por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una factura por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesById(int id);

        /// <summary>
        /// guarda una factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSales"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, AuditMessage audit, long idSequence = 0, BillingSequence sequenceC = null);

        /// <summary>
        /// confirmar una factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSalesId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> ConfirmDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, CashReceipts cashReceipts, AuditMessage audit, SessionValues session);

        /// <summary>
        /// guardar y confirmar una factura
        /// </summary>
        /// <param name="DocumentInvoiceProductSales"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveAndConfirmDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, CashReceipts cashReceipts, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, BillingSequence sequenceC = null, SessionValues session = null);

        /// <summary>
        /// Sets the products product invoice import file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="wareHouseId">The ware house identifier.</param>
        /// <param name="operatingUnitId">The operating unit identifier.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> SetProductsProductInvoiceImportFile(List<ImportFileRow> data, int wareHouseId, int operatingUnitId, AuditMessage audit);

        /// <summary>
        /// Reversa un documento de factura de productos
        /// </summary>
        /// <param name="documentInvoiceProductSales">The document invoice product sales.</param>
        /// <param name="reversalReasonId">The reversal reason identifier.</param>
        /// <param name="reversalReasonDescription">The reversal reason description.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        ActionResult ReverseDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, int reversalReasonId, string reversalReasonDescription, AuditMessage audit, SessionValues session);
        /// <summary>
        /// Funcion que agregara la actividad econonomica
        /// </summary>
        /// <param name="documentInvoiceProductSales"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DocumentInvoiceProductSales> AddEconomicActivity(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales);

    }
}