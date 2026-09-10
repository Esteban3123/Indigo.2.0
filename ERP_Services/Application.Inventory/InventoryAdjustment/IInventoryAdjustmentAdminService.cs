//'************************************************************
//' Assembly         : Domain.Inventory.InventoryAdjustmentRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 15/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.ProductSubGroups;
using Domain.Entities;
using System.Data;
using Infrastructure.CrossCutting.Resources;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Data.Entity.Validation;
using Application.Inventory.PhysicalInventory;
using Domain.Entities.Service;
using Application.Inventory.Rollback;

namespace Application.Inventory.InventoryAdjustment
{
    public interface IInventoryAdjustmentAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// metodo para confirmar ajustes de inventario
        /// </summary>
        /// <param name="inventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="OperatingUnitId"></param>
        /// <returns></returns>
        Task <ActionResult<Domain.Entities.InventoryAdjustment>> ConfirmInventoryAdjustment(Domain.Entities.InventoryAdjustment inventoryAdjustment, AuditMessage audit, int OperatingUnitId, int Type = 0);
        /// <summary>
        /// Guarda o actualiza un InventoryAdjustment
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.InventoryAdjustment>> SaveInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina un InventoryAdjustment
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryAdjustment
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryAdjustment> ChangeStateInventoryAdjustment(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryAdjustment por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryAdjustment> GetInventoryAdjustment(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryAdjustment por id
        /// </summary>
        /// <param name="idInventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.InventoryAdjustment GetInventoryAdjustmentById(int idInventoryAdjustment);

        /// <summary>
        /// guardar y confirmar un comprobante de entrada
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        Task <ActionResult<Domain.Entities.InventoryAdjustment>> SaveAndConfirmbInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit, int OperatingUnitId, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, int Type = 0);
    }
}
