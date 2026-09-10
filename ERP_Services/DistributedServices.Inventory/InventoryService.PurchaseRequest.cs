//***********************************************************************
// Assembly         : Application.Payments
// Author           : Hector Rodriguez R
// Created          : 10/04/2019
//
// Copyright        : (c) . All rights reserved.
// About            : PBI3499
//***********************************************************************

using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.PurchaseRequest;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServicePurchaseRequest
    {
        /// <summary>
        /// Guarda o Actualiza una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="idSequense"></param>
        /// <param name="audit"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseRequest> SavePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestAdminService>())
            {                
                return service.SavePurchaseRequest(oPurchaseRequest, audit, idSequense, sequenceC);
            }
        }

        /// <summary>
        /// Elimina una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeletePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestAdminService>())
            {               
                return service.DeletePurchaseRequest(oPurchaseRequest, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseRequest> ChangeStatePurchaseRequest(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestAdminService>())
            {              
                return service.ChangeStatePurchaseRequest(code, state, audit);
            }
        }

        /// <summary>
        /// Consulta una solicitud de compra de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseRequest> GetPurchaseRequestByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestAdminService>())
            {               
                return service.GetPurchaseRequestByCode(code, audit);
            }
        }

        /// <summary>
        /// Consulta una solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.PurchaseRequest GetPurchaseRequestById(int id)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestAdminService>())
            {
                return service.GetPurchaseRequestById(id);
            }
        }

    }
}
