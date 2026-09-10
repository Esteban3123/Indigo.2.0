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
    public interface IInventoryEntranceVoucher
    {
        /// <summary>
        /// Guarda o actualiza un EntranceVoucher
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucher> SaveEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina un EntranceVoucher
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de EntranceVoucher
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucher> ChangeStateEntranceVoucher(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el EntranceVoucher por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucher> GetEntranceVoucher(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un EntranceVoucher por id
        /// </summary>
        /// <param name="idEntranceVoucher"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.EntranceVoucher GetEntranceVoucherById(int idEntranceVoucher);

        /// <summary>
        ///Obtiene un EntranceVoucherDetailBatchSerial por IdEntrancevoucher
        ///</summary>
        ///<param name="EntranceVoucherId"></param>
        ///<returns></returns>
        ///<remarks></remarks>
        [OperationContract]
        List<Domain.Entities.EntranceVoucherDetailBatchSerial> GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(int EntranceVoucherId);

        /// <summary>
        /// guardar y confirmar un comprobante
        /// </summary>
        /// <param name="entranceVoucher"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.EntranceVoucher>> SaveAndConfirmbEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, string ContainerNameCrystal, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false);
    }
}
