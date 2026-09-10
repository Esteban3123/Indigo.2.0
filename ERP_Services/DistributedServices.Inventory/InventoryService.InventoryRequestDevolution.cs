//***********************************************************************
// Assembly         : Application.DistributedServices
// Author           : Faiber Julian Mora D.
// Created          : 29/09/2016
//
// Copyright        : (c) . All rights reserved.
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
using Application.Inventory.InventoryRequestDevolution;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {

        /// <summary>
        /// Guarda o Actualiza la solicitud de devolcuión de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequestDevolution> SaveInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.SaveInventoryRequestDevolution(inventoryRequestdevolution, audit, idSequense, sequenceC);
            }
            //return _inventoryRequestDevolutionAdminService.SaveInventoryRequestDevolution(inventoryRequestdevolution, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// Guarda y Confirma una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestDevolution"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequestDevolution> SaveAndConfirmRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.SaveAndConfirmRequestDevolution(inventoryRequestDevolution, audit, idSequense, action, sequenceC);
            }
            //return _inventoryRequestDevolutionAdminService.SaveAndConfirmRequestDevolution(inventoryRequestDevolution, audit, idSequense, action, sequenceC);
        }

        /// <summary>
        /// Elimina una solicitud de devolución de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteInventoryrequestdevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.DeleteInventoryRequestDevolution(inventoryRequestdevolution, audit);
            }
            //return _inventoryRequestDevolutionAdminService.DeleteInventoryRequestDevolution(inventoryRequestdevolution, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequestDevolution> ChangeStatusInventoryRequest(string code, byte status, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.ChangeStatusInventoryRequestDevolution(code, status, audit);
            }
            //return _inventoryRequestDevolutionAdminService.ChangeStatusInventoryRequestDevolution(code, status, audit);
        }

        /// <summary>
        /// Obtiene una solicitud de devolución de inventario por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestDevolutionByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.GetRequestDevolutionByCode(code, audit);
            }
            //return _inventoryRequestDevolutionAdminService.GetRequestDevolutionByCode(code, audit);
        }

        /// <summary>
        /// Obtiene la solicitud de devolución de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestdevolutionById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDevolutionAdminService>())
            {                
                return service.GetInventoryRequestDevolutionById(id, audit);
            }
            //return _inventoryRequestDevolutionAdminService.GetInventoryRequestDevolutionById(id, audit);
        }
    }
}
