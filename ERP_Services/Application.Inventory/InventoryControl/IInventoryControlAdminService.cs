//'************************************************************
//' Assembly         : Domain.Inventory.InventoryControlRepository
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
using Application.Payments;
using Domain.Entities.Service;

namespace Application.Inventory.InventoryControl
{
    public interface IInventoryControlAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un InventoryControl
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryControl> SaveInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un InventoryControl
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryControl
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryControl> ChangeStateInventoryControl(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryControl por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryControl> GetInventoryControl(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryControl por id
        /// </summary>
        /// <param name="idInventoryControl"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.InventoryControl GetInventoryControlById(int idInventoryControl);

        /// <summary>
        /// guardar y confirmar un comprobante de entrada
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryControl> SaveAndConfirmbInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);
        /// <summary>
        /// metodo para validar los items que se estan importando en el archivo de excel
        /// </summary>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlImportFile(List<ImportFileRow> data, int warehouseId, int controlType, AuditMessage audit,DateTime documentDate,int operatingUnitId);
        /// <summary>
        /// metodo para validar los items pegados en la rejilla
        /// </summary>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlCopyPaste(List<List<string>> data, int warehouseId, int controlType, AuditMessage audit);

        /// <summary>
        /// obtyiene el control de inventarios por id sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.InventoryControl GetInventoryControlByIdNoAdded(int id);
        /// <summary>
        /// obtyiene el control de inventarios por codigo sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.InventoryControl GetInventoryControlByCodeNoAdded(string code, AuditMessage audit);
        /// <summary>
        /// lista los detalles del control de inventario
        /// </summary>
        /// <param name="inventoryControlId"></param>
        /// <returns></returns>
        List<Domain.Entities.InventoryControlDetail> GetInventoryControlDetailByInventoryControlId(int inventoryControlId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idInventoryControl"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> GetInventoryControlByIdAndStatus(int idInventoryControl, byte status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventoryAdjustmentId"></param>
        /// <returns></returns>
        Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetailBatchSerial>> GetInventoryAdjustmentControlByInventoryAdjustmentId(int InventoryAdjustmentId);

    }
}
