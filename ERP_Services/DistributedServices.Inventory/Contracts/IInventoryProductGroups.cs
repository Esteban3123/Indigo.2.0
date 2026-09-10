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
    public interface IInventoryProductGroups
    {

        /// <summary>
        /// Guarda o actualiza un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductGroup> SaveProductGroup(Domain.Entities.ProductGroup productGroup, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit);

        /// <summary>
        /// Obtiene un grupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductGroup> GetProductGroup(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un grupo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductGroup> GetProductGroupById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductGroup> ChangeState(string code, bool state, AuditMessage audit);

    }
}
