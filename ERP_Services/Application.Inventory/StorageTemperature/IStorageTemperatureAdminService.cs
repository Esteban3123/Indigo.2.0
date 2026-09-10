//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Andres Alarcon
// Created          : 29-11-2024
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.StorageTemperature
{
    public interface IStorageTemperatureAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un rango de temperatura
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.StorageTemperature> SaveStorageTemperature(Domain.Entities.StorageTemperature product, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Obtiene un rango de temperatura por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        ActionResult<Domain.Entities.StorageTemperature> GetStorageTemperature(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un rango de temperatura por Id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ActionResult <Domain.Entities.StorageTemperature> GetStorageTemperatureById(int id);

    }
}
