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
    public interface IInventoryProductType
    {

        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductType> SaveProductType(Domain.Entities.ProductType productType, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteProductType(Domain.Entities.ProductType productType, AuditMessage audit);

        /// <summary>
        /// Obtiene un tipo producto por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductType> GetProductType(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un tipo producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductType> GetProductTypeById(int id, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductType> ChangeStateProductType(string code, bool state, AuditMessage audit);

    }
}
