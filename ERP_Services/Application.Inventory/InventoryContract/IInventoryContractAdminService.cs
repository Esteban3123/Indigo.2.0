//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 07/01/2015
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

namespace Application.Inventory.InventoryContract
{
    public interface IInventoryContractAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un InventoryContract
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContract> SaveInventoryContract(Domain.Entities.InventoryContract InventoryContract, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un InventoryContract
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryContract(Domain.Entities.InventoryContract InventoryContract, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryContract
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContract> ChangeStateInventoryContract(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryContract por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContract> GetInventoryContract(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryContract por id
        /// </summary>
        /// <param name="idInventoryContract"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.InventoryContract GetInventoryContractById(int idInventoryContract);
    }
}
