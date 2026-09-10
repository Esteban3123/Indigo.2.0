//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Daniel Eduardo Arévalo Bonilla
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

namespace Application.Inventory.PurchaseOrder
{
    public interface IPurchaseOrderAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene una Orden de Compra por Código
        /// </summary>
        /// <param name="Code">Código</param>
        /// <returns>PurchaseOrder</returns>
        Domain.Entities.PurchaseOrder GetPurchaseOrderByCode(String Code);

        /// <summary>
        /// Obtiene una Orden de Compra por Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>PurchaseOrder</returns>
        Domain.Entities.PurchaseOrder GetPurchaseOrderById(int Id);

        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseOrder> SavePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeletePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit);

        /// <summary>
        /// Desconfirma una orden de compra
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <param name="audti"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseOrder> DisconfirmPurchaseOrder(Domain.Entities.PurchaseOrder purchaseOrder, AuditMessage audti);
        /// <summary>
        /// Sets the documents crossing import file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.PurchaseOrderDetail>> SetProductsPurchaseOrderImportFile(List<ImportFileRow> data, int operatingUnitId, AuditMessage audit, decimal roundingType=0.01m);

    }
}
