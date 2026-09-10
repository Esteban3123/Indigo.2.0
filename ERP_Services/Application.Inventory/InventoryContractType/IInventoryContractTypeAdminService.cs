//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractTypeRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 26/12/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.InventoryContractType
{
    public interface IInventoryContractTypeAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un InventoryContractType
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractType> SaveInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un InventoryContractType
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryContractType
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractType> ChangeStateInventoryContractType(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryContractType por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractType> GetInventoryContractType(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryContractType por id
        /// </summary>
        /// <param name="idInventoryContractType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.InventoryContractType GetInventoryContractTypeById(int idInventoryContractType);
    }
}
