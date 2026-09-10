///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 08/09/2014
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
    public interface IInventoryMeasureUnit
    {

        /// <summary>
        /// Guarda o actualiza una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryMeasurementUnit> SaveMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, AuditMessage audit);

        /// <summary>
        /// Obtiene una unidad de medida por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnit(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una unidad de medida por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnitById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryMeasurementUnit> ChangeStateMeasureUnit(string code, bool state, AuditMessage audit);

    }
}
