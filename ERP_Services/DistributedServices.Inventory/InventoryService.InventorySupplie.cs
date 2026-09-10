//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Hector Rodriguez R
// Created          : 08/01/2020
//
// Copyright        : (c) . All rights reserved.
// About            : PBI7129
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
using Application.Inventory.InventorySupplie;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceInventorySupplie
    {
        /// <summary>
        /// Guarda o Actualiza un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="idSequense"></param>
        /// <param name="audit"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventorySupplie> SaveInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IInventorySupplieAdminService>())
            {
                return service.SaveInventorySupplie(oInventorySupplie, audit, idSequense, sequenceC);
            }
        }

        /// <summary>
        /// Elimina un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventorySupplieAdminService>())
            {
                return service.DeleteInventorySupplie(oInventorySupplie, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventorySupplie> ChangeStateInventorySupplie(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventorySupplieAdminService>())
            {
                return service.ChangeStateInventorySupplie(code, state, audit);
            }
        }

        /// <summary>
        /// Consulta un insumo de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventorySupplie> GetInventorySupplieByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventorySupplieAdminService>())
            {
                return service.GetInventorySupplieByCode(code, audit);
            }
        }

        /// <summary>
        /// Consulta un insumo de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventorySupplie GetInventorySupplieById(int id)
        {
            using (var service = Container.Current.Resolve<IInventorySupplieAdminService>())
            {
                return service.GetInventorySupplieById(id);
            }
        }

    }
}
