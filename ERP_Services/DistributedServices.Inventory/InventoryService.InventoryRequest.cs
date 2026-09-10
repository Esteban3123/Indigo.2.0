//***********************************************************************
// Assembly         : Application.Payments
// Author           : Juan Carlos Bermudez Gutierrez
// Created          : 02/05/2015
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
using Application.Inventory.InventoryRequest;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Domain.Base.Entities;
using Domain.Entities;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryRequest
    {
        /// <summary>
        /// Guarda o Actualiza una solicitud de inventario
        /// </summary>
        /// <param name="EntranceVoucher"></param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequest>> SaveInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {                
                return await service.SaveInventoryRequestAsync(inventoryRequest, audit, idSequense, sequenceC);
            }
        }

        /// <summary>
        /// Elimina una solicitud de inventario
        /// </summary>
        /// <param name="EntranceVoucher"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {               
                return service.DeleteInventoryRequest(inventoryRequest, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequest>> ChangeStateInventoryRequest(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {              
                return await service.ChangeStateInventoryRequestAsync(code, state, audit);
            }
        }

        /// <summary>
        /// Consulta una solicitud de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRequest> GetInventoryRequestByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {               
                return service.GetInventoryRequestByCode(code, audit);
            }
        }

        /// <summary>
        /// Consulta una solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryRequest GetInventoryRequestById(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {
                return service.GetInventoryRequestById(id);
            }
        }

        /// <summary>
        /// CopyPaste/Import solicitudes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult<List<InventoryRequestDetail>> SP_CopyPasteAndImportRequests(List<List<string>> data)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {
                return service.SP_CopyPasteAndImportRequests(data);
            }
        }

        /// <summary>
        /// CopyPaste/Import medicamentos,insumos,otros
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult<List<InventoryRequestDetailOther>> SP_CopyPasteAndImportRequestsOtherDetail(List<List<string>> data)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestAdminService>())
            {
                return service.SP_CopyPasteAndImportRequestsOtherDetail(data);
            }
        }

    }
}
