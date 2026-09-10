///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Daniel Eduardo Arévalo
/// Created          : 13/01/2015
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
    public interface IInventoryPurchaseOrder
    {
        /// <summary>
        /// Guarda o actualiza una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PurchaseOrder> SavePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeletePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit);

        /// <summary>
        /// Obtiene uan Orden de Compra por Código
        /// </summary>
        /// <param name="Code">Code</param>
        /// <returns>PurchaseOrder</returns>
        [OperationContract]
        Domain.Entities.PurchaseOrder GetPurchaseOrderByCode(String Code, AuditMessage audit);

        /// <summary>
        /// Obtiene una Orden de Compra por ID
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>PurchaseOrder</returns>
        [OperationContract]
        Domain.Entities.PurchaseOrder GetPurchaseOrderById(int Id, AuditMessage audit);

        /// <summary>
        /// desconfirma una Orden de Compra
        /// </summary>
        /// <param name="purchaseOrder">purchaseOrder</param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PurchaseOrder> DisconfirmPurchaseOrder(Domain.Entities.PurchaseOrder purchaseOrder, AuditMessage audit);

        [OperationContract]
        ActionResult<List<Domain.Entities.PurchaseOrderDetail>> SetProductsPurchaseOrderImportFile(List<ImportFileRow> data, int operatingUnitId, SessionValues session, decimal roundingType=0.01m);

    }
}

