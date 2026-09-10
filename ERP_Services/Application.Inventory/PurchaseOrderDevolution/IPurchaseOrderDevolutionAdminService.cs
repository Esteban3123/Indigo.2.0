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

namespace Application.Inventory.PurchaseOrderDevolution
{
    public interface IPurchaseOrderDevolutionAdminService : IDisposable
    {

        List<Domain.Entities.PurchaseOrderDevolutionDetail> GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(int PurchaseOrderDevolutionId);
        /// <summary>
        /// Obtiene una Orden de Compra por Código
        /// </summary>
        /// <param name="Code">Código</param>
        /// <returns>PurchaseOrder</returns>
        Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionByCode(String Code);

        /// <summary>
        /// Obtiene una Orden de Compra por Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>PurchaseOrder</returns>
        Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionById(int Id);

        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseOrderDevolution> SavePurchaseOrderDevolution(Domain.Entities.PurchaseOrderDevolution PurchaseOrderDevolution, AuditMessage audit, Int64 idSecuence = 0);
    }
}
