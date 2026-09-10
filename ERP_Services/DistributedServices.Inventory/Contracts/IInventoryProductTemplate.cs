///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryProductTemplate
    {
        /// <summary>
        /// Guarda un cubrimiento de producto
        /// </summary>
        [OperationContract]
        Task<ActionResult<Domain.Entities.ProductRate>> SaveProductTemplate(Domain.Entities.ProductRate productTemplate, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un cubrimiento de producto
        /// </summary>
        [OperationContract]
        ActionResult DeleteProductTemplate(Domain.Entities.ProductRate productTemplate, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del cubrimiento de producto
        /// </summary>
        [OperationContract]
        Task <ActionResult<Domain.Entities.ProductRate>> UpdateStateProductTemplate(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene un cubrimiento de producto por codigo
        /// </summary>
        [OperationContract]
        Task <ActionResult<Domain.Entities.ProductRate>> GetProductTemplate(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un cubrimiento de producto por id
        /// </summary>
        [OperationContract]
        Domain.Entities.ProductRate GetProductTemplateById(int id);
    }
}
