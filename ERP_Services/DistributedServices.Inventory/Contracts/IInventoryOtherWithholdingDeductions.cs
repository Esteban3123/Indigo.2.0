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
    public interface IInventoryOtherWithholdingDeductions
    {

        /// <summary>
        /// Guarda o actualiza otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.OtherWithholdingDeduction> SaveOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit);

        /// <summary>
        /// Obtiene otras deducciones y retenciones por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeduction(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene otras deducciones y retenciones por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeductionById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.OtherWithholdingDeduction> ChangeStateOtherWithholdingDeduction(string code, bool state, AuditMessage audit);


        /// <summary>
        /// Obtiene un listado de las deducciones y retenciones
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.OtherWithholdingDeduction>> ListOtherWithholdingDeduction();

    }
}
