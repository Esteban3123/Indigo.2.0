///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Daniel Eduardo Arévalo
/// Created          : 11/04/2019
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
    public interface IInventoryATCEntity
    {
        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<Domain.Entities.ATCEntity> SaveATCEntity(Domain.Entities.ATCEntity ATCEntity, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult DeleteATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit);

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ATCEntity> GetATCEntity(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ATCEntity> GetATCEntityById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ATCEntity> ChangeStateATCEntity(string code, bool state, AuditMessage audit);
    }
}
