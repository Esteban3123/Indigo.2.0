//***********************************************************************
// Assembly         : DistributedServices.Inventory
// Author           : Andres Alarcon
// Created          : 29/11/2024
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryStorageTemperature
    {

        /// <summary>
        /// Guarda o actualiza un rango de temperatura
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<StorageTemperature> SaveStorageTemperature(StorageTemperature warehouse, long idSequense, AuditMessage audit);

        /// <summary>
        /// Obtiene un rango de temperatura por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<StorageTemperature> GetStorageTemperature(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un rango de temperatura por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<StorageTemperature> GetStorageTemperatureById(int id, AuditMessage audit);
    }
}
