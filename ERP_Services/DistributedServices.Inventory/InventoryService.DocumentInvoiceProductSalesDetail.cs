using Application.Inventory.DocumentInvoiceProductSalesDetail;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Domain.Base.Entities;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public List<Domain.Entities.DocumentInvoiceProductSalesDetail> GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(int DocumentInvoiceProductSalesId)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesDetailAdminService>())
            {
                return service.GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSalesId);
            }
            //return _documentInvoiceProductSalesDetailAdminService.GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSalesId);
        }

        public ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> ImportRemissionOutput(List<int> ListIds, int ContractExternalClientId, int FunctionalUnitId, int? DocumentInvoiceProductSalesId = null)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesDetailAdminService>())
            {
                return service.ImportRemissionOutput(ListIds, ContractExternalClientId, FunctionalUnitId, DocumentInvoiceProductSalesId);
            }
        }

        public ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> CalculateRateProduct(List<Domain.Entities.DocumentInvoiceProductSalesDetail> ListDocumentInvoiceProductSalesDetail, int ContractExternalClientId, int FunctionalUnitId)
        {
            using (var service = Container.Current.Resolve<IDocumentInvoiceProductSalesDetailAdminService>())
            {
                return service.CalculateRateProduct(ListDocumentInvoiceProductSalesDetail, ContractExternalClientId, FunctionalUnitId);
            }
        }
    }
}
