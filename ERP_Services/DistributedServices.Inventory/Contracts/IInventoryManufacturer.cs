///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 17/09/2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryManufacturer
    {

        /// <summary>
        /// Guarda o actualiza un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Manufacturer> SaveManufacturer(Domain.Entities.Manufacturer manufacturer, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit);

        /// <summary>
        /// Obtiene un fabricante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Manufacturer> GetManufacturer(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un fabricante por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Manufacturer> GetManufacturerById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.Manufacturer> ChangeStateManufacturer(string code, bool state, AuditMessage audit);

    }
}
