///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 26-12-2014
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
    public interface IInventoryContractType
    {
        /// <summary>
        /// Guarda o actualiza un InventoryContractType
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractType> SaveInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un InventoryContractType
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryContractType
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractType> ChangeStateInventoryContractType(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryContractType por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractType> GetInventoryContractType(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryContractType por id
        /// </summary>
        /// <param name="idInventoryContractType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryContractType GetInventoryContractTypeById(int idInventoryContractType);
        
    }
}
