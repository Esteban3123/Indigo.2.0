using DistributedServices.Inventory.Contracts;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Inventory.DocumentInvoiceProductSales;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService : IInventoryServiceDocumentInvoiceProductSales
    {
        /// <summary>
        /// Gets the document invoice product sales by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {                
                return service.GetDocumentInvoiceProductSalesByCode(code, audit);
            }
            //return _documentInvoiceProductSalesAdminService.GetDocumentInvoiceProductSalesByCode(code, audit);
        }

        public Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesById(int id)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {
                return service.GetDocumentInvoiceProductSalesById(id);
            }
            //return _documentInvoiceProductSalesAdminService.GetDocumentInvoiceProductSalesById(id);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales DocumentInvoiceProductSales, AuditMessage audit, long idSequense, Domain.Entities.BillingSequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {                
                return service.SaveDocumentInvoiceProductSales(DocumentInvoiceProductSales, audit, idSequense, sequenceC);
            }
            //return _documentInvoiceProductSalesAdminService.SaveDocumentInvoiceProductSales(DocumentInvoiceProductSales, audit, idSequense, sequenceC);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveAndConfirmbDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales DocumentInvoiceProductSales, Domain.Entities.CashReceipts cashReceipts, long idSequense, SessionValues session, Domain.Entities.BillingSequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {
                return service.SaveAndConfirmDocumentInvoiceProductSales(DocumentInvoiceProductSales, cashReceipts, session.AuditMessageWcf, idSequense, action, sequenceC, session);
            }
            //return _documentInvoiceProductSalesAdminService.SaveAndConfirmDocumentInvoiceProductSales(DocumentInvoiceProductSales,cashReceipts , audit, idSequense, action, sequenceC);
        }

        public ActionResult<List<DocumentInvoiceProductSalesDetail>> SetProductsProductInvoiceImportFile(List<ImportFileRow> data, int wareHouseId, int operatingUnitId, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {
                return service.SetProductsProductInvoiceImportFile(data, wareHouseId, operatingUnitId, session.AuditMessageWcf);
            }
            //return _documentInvoiceProductSalesAdminService.SetProductsProductInvoiceImportFile(data, wareHouseId, operatingUnitId, session.AuditMessageWcf);
        }

        public ActionResult ReverseDocumentInvoiceProductSales(DocumentInvoiceProductSales documentInvoiceProductSales, int reversalReasonId, string reversalReasonDescription, SessionValues session)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesAdminService>())
            {
                return service.ReverseDocumentInvoiceProductSales(documentInvoiceProductSales, reversalReasonId, reversalReasonDescription, session.AuditMessageWcf, session);
            }
            //return _documentInvoiceProductSalesAdminService.ReverseDocumentInvoiceProductSales(documentInvoiceProductSales, reversalReasonId, reversalReasonDescription, session.AuditMessageWcf);
        }
    }
}