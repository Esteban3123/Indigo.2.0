///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 22/09/2014
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
    public interface IInventoryPharmaceuticalForm
    {

        /// <summary>
        /// Guarda o actualiza una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalForm> SavePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeletePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit);

        /// <summary>
        /// Obtiene una forma farmaceutica por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalForm(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una forma farmaceutica por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalFormById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalForm> ChangeStatePharmaceuticalForm(string code, bool state, AuditMessage audit);

    }
}
