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
    public interface IInventoryProductSubGroups
    {

        /// <summary>
        /// Guarda o actualiza un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductSubGroup> SaveProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, AuditMessage audit);

        /// <summary>
        /// Obtiene un subgrupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroup(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un subgrupo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroupById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductSubGroup> ChangeStateSubGroup(string code, bool state, AuditMessage audit);

    }
}
