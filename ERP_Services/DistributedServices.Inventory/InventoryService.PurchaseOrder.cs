using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Inventory.PurchaseOrder;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryPurchaseOrder
    {
        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseOrder> SavePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {                
                return service.SavePurchaseOrder(PurchaseOrder, audit, idSequense, sequenceC);
            }
            //return _purchaseOrderAdminService.SavePurchaseOrder(PurchaseOrder, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// Elimina una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeletePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {                
                return service.DeletePurchaseOrder(PurchaseOrder, audit);
            }
            //return _purchaseOrderAdminService.DeletePurchaseOrder(PurchaseOrder, audit);
        }

        /// <summary>
        /// Obtiene la Orden de Compra x Código
        /// </summary>
        /// <param name="Code">Code</param>
        /// <returns></returns>
        public Domain.Entities.PurchaseOrder GetPurchaseOrderByCode(String Code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {                
                return service.GetPurchaseOrderByCode(Code);
            }
            //return _purchaseOrderAdminService.GetPurchaseOrderByCode(Code);
        }

        /// <summary>
        /// Obtiene la Orden de Compra x Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>PurchaseOrder</returns>
        public Domain.Entities.PurchaseOrder GetPurchaseOrderById(int Id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {               
                return service.GetPurchaseOrderById(Id);
            }
            //return _purchaseOrderAdminService.GetPurchaseOrderById(Id);
        }

        /// <summary>
        /// Elimina una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseOrder> DisconfirmPurchaseOrder(Domain.Entities.PurchaseOrder purchaseOrder, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {                
                return service.DisconfirmPurchaseOrder(purchaseOrder, audit);
            }
            //return _purchaseOrderAdminService.DisconfirmPurchaseOrder(purchaseOrder, audit);
        }

        public ActionResult<List<PurchaseOrderDetail>> SetProductsPurchaseOrderImportFile(List<ImportFileRow> data, int operatingUnitId, SessionValues session, decimal roundingType=0.01m)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderAdminService>())
            {
                return service.SetProductsPurchaseOrderImportFile(data, operatingUnitId, session.AuditMessageWcf, roundingType);
            }
            //return _purchaseOrderAdminService.SetProductsPurchaseOrderImportFile(data, operatingUnitId, session.AuditMessageWcf);
        }
    }
}
