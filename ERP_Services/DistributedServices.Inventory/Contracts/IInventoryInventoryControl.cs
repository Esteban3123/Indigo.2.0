///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 15-01-2015
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
    public interface IInventoryInventoryControl
    {
        /// <summary>
        /// Guarda o actualiza un InventoryControl
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControl> SaveInventoryControl(Domain.Entities.InventoryControl InventoryControl, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un InventoryControl
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryControl
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControl> ChangeStateInventoryControl(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryControl por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControl> GetInventoryControl(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryControl por id
        /// </summary>
        /// <param name="idInventoryControl"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryControl GetInventoryControlById(int idInventoryControl);
        
        /// <summary>
        /// guardar y confirmar un comprobante
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryControl> SaveAndConfirmbInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, long idSequense, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);
        /// <summary>
        /// metodo para validar los items que se estan importando en el archivo de excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlImportFile(List<Domain.Base.Entities.ImportFileRow> data, int warehouseId, int controlType, DateTime documentDate, int operatingUnitId, AuditMessage audit);
        /// <summary>
        /// metodo para validar los items pegados en la rejilla
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlCopyPaste(List<List<string>> data, int warehouseId, int controlType, AuditMessage audit);
        /// <summary>
        /// obtyiene el control de inventarios por id sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryControl GetInventoryControlByIdNoAdded(int id);

        /// <summary>
        /// obtyiene el control de inventarios por codigo sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryControl GetInventoryControlByCodeNoAdded(string code, AuditMessage audit);
        /// <summary>
        /// lista los detalles del control de inventario
        /// </summary>
        /// <param name="inventoryControlId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.InventoryControlDetail> GetInventoryControlDetailByInventoryControlId(int inventoryControlId);

        [OperationContract]
        Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> GetInventoryControlByIdAndStatus(int Id, byte status);

        [OperationContract]
        Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetailBatchSerial>> GetInventoryAdjustmentControlByInventoryAdjustmentId(int InventoryAdjustmentId);

    }
}
