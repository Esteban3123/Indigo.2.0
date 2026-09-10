///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Miguel Angel Fonseca Castro
/// Created          : 2018-04-14
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System.ServiceModel;
using Domain.Base.Entities;
using System;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryInventoryControlService
    {
        /// <summary>
        /// Consulta el InventoryControlService por EntityId y EntityName
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControlService> GetInventoryControlServiceByEntityIdAndEntityName(int entityId, string entityName);

        /// <summary>
        /// Consulta el InventoryControlService por EntityCode y EntityName
        /// </summary>
        /// <param name="entityCode"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControlService> GetInventoryControlServiceByEntityCodeAndEntityName(string entityCode, string entityName);

        /// <summary>
        /// Guarda o actualiza un InventoryControlService
        /// </summary>
        /// <param name="inventoryControlService"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControlService> SaveInventoryControlService(Domain.Entities.InventoryControlService inventoryControlService);
    }
}