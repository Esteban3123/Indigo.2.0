///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 07-01-2015
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
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryContract
    {
        /// <summary>
        /// Guarda o actualiza un InventoryContract
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContract> SaveInventoryContract(Domain.Entities.InventoryContract InventoryContract, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un InventoryContract
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryContract(Domain.Entities.InventoryContract InventoryContract, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryContract
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContract> ChangeStateInventoryContract(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryContract por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContract> GetInventoryContract(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryContract por id
        /// </summary>
        /// <param name="idInventoryContract"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryContract GetInventoryContractById(int idInventoryContract);
    }
}
