//'************************************************************
//' Assembly         : DistributedService
//' Author           : Diego A. Roldán L.
//' Created          : 2023-05-08
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.Collections.Generic;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceConsignmentTransfer
    {

        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ConsignmentTransfer GetConsignmentTransferById(int id);
        /// <summary>
        /// Obtener por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ConsignmentTransfer GetConsignmentTransferByCode(string code);
        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentTransfer> SaveConsignmentTransfer(
            Domain.Entities.ConsignmentTransfer consignmentTransfer,
            AuditMessage audit,
            long idSequence = 0
        );
        /// <summary>
        /// Guardar y confirmar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentTransfer> SaveAndConfirmConsignmentTransfer(
           Domain.Entities.ConsignmentTransfer consignmentTransfer,
           AuditMessage audit,
           long idSequence = 0
        );
        /// <summary>
        /// Confirmar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentTransfer> ConfirmConsignmentTransfer(
            Domain.Entities.ConsignmentTransfer consignmentTransfer,
            AuditMessage audit
        );
        /// <summary>
        /// Consulta las cantidades en el almacén de consignación
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [OperationContract]
        ConsignmentMovementInventory GetConsignmentInventoryQuantities(int warehouseId, int productId);
        /// <summary>
        /// Valida información extraída de Excel para ConsignmentTransfer
        /// </summary>
        /// <param name="listRows">Lista de filas del archivo Excel</param>
        /// <param name="sourceWarehouseId">ID del almacén de origen</param>
        /// <param name="audit">Mensaje de auditoría</param>
        /// <returns>Resultado de la validación</returns>
        [OperationContract]
        ActionResult<List<ConsignmentTransferDetail>> SetConsignmentTransferImportFile(List<ImportFileRow> listRows, int sourceWarehouseId, AuditMessage audit);
        /// <summary>
        /// Valida información pegada desde Excel (Copy/Paste) para ConsignmentTransfer
        /// </summary>
        /// <param name="dataImport">Lista de listas de strings con los datos pegados</param>
        /// <param name="sourceWarehouseId">ID del almacén de origen</param>
        /// <param name="audit">Mensaje de auditoría</param>
        /// <returns>Resultado de la validación</returns>
        [OperationContract]
        ActionResult<List<ConsignmentTransferDetail>> SetConsignmentTransferDetailFromCopyPaste(List<List<string>> dataImport, int sourceWarehouseId, AuditMessage audit);
    }
}
