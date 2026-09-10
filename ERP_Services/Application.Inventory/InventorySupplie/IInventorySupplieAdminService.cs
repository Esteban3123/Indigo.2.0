//'************************************************************
//' Assembly         : Application.Inventory.InventorySupplie
//' Author           : Hector Rodriguez Rubiano
//' Created          : 08/01/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI7129
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.InventorySupplie
{
    public interface IInventorySupplieAdminService : IDisposable
    {

        /// <summary>
        /// Guarda o actualiza un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventorySupplie> SaveInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventorySupplie> ChangeStateInventorySupplie(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta un insumo de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventorySupplie> GetInventorySupplieByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un insumo de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.InventorySupplie GetInventorySupplieById(int id);

    }
}
