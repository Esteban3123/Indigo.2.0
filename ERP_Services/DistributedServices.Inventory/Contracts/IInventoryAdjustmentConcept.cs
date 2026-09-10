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
    public interface IInventoryAdjustmentConcept
    {

        /// <summary>
        /// Guarda o actualiza un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdjustmentConcept> SaveAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit);

        /// <summary>
        /// Obtiene un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConcept(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un concepto de ajuste por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConceptById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AdjustmentConcept> ChangeStateAdjustmentConcept(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene un concepto de ajuste por cuenta contable y centro de costo.
        /// </summary>
        /// <param name="conceptType"></param>
        /// <param name="adjustmentAccountId"></param>
        /// <param name="costCenterId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.AdjustmentConcept>> GetListByAdjustmentAccountCostCenterId(Byte conceptType, int adjustmentAccountId, int costCenterId, AuditMessage audit);

    }
}
