///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 18/09/2014
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
    public interface IInventoryAttributeProductType
    {

        /// <summary>
        /// Guarda o actualiza un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AttributeProductType> SaveAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit);

        /// <summary>
        /// Obtiene un atributo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductType(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un atributo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductTypeById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AttributeProductType> ChangeStateAttributeProductType(string code, bool state, AuditMessage audit);

    }
}
