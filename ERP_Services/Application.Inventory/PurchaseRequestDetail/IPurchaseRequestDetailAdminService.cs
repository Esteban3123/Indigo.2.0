//***********************************************************************
// Assembly         : Application.Inventory.PurchaseRequestDetail
// Author           : Hector Rodriguez R
// Created          : 10-04-2019
//
// Copyright        : (c) . All rights reserved.
// About            : PBI3499
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.PurchaseRequestDetail
{
    public interface IPurchaseRequestDetailAdminService : IDisposable
    {

        /// <summary>
        /// Obtiene los detalles de una solicitud de compra de inventario por unidad funcional
        /// </summary>
        /// <param name="idFunctionalUnit"></param>
        /// <returns></returns>
        List<Domain.Entities.PurchaseRequestDetail> ListPurchaseRequestDetailByFunctionalUnit(int idFunctionalUnit);

        /// <summary>
        /// Obtiene un detalle de solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.PurchaseRequestDetail GetPurchaseRequestDetailById(int id);

       /// <summary>
       /// Cambia La cantidad pendiente del item
       /// </summary>
       /// <param name="id"></param>
       /// <param name="QuantityExport"></param>
       /// <param name="audit"></param>
       /// <returns></returns>
        ActionMessageResult ChangeQuantityPurchaseRequestDetail(int id, int QuantityExported);

    }
}
