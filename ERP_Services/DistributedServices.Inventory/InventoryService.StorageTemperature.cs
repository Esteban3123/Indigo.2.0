//************************************************************
// Assembly         : DistributedServices.Inventory
// Author           : Andres Alaron
// Created          : 29-11-2024
//
// Copyright        : (c) . All rights reserved.
//************************************************************

using Application.Inventory.StorageTemperature;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un rango de temperatura
        /// </summary>
        public Domain.Base.Entities.ActionResult<Domain.Entities.StorageTemperature> SaveStorageTemperature(Domain.Entities.StorageTemperature warehouse, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IStorageTemperatureAdminService>())
            {                
                return service.SaveStorageTemperature(warehouse, audit, idSequense);
            }
        }

        /// <summary>
        /// Obtiene un rango de temperatura por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.StorageTemperature> GetStorageTemperature(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IStorageTemperatureAdminService>())
            {                
                return service.GetStorageTemperature(code, audit);
            }
        }

        /// <summary>
        /// Obtiene un rango de temperatura por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.StorageTemperature> GetStorageTemperatureById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IStorageTemperatureAdminService>())
            {                
                return service.GetStorageTemperatureById(id);
            }
        }
    }
}
