//'************************************************************
//' Assembly         : Domain.Inventory.EntranceVoucherRepository
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
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Inventory.Rollback;

namespace Application.Inventory.EntranceVoucher
{
    public interface IEntranceVoucherAdminService : IAdminServiceRollbackStrategy,  IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un EntranceVoucher
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucher> SaveEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina un EntranceVoucher
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de EntranceVoucher
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucher> ChangeStateEntranceVoucher(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el EntranceVoucher por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucher> GetEntranceVoucher(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un EntranceVoucher por id
        /// </summary>
        /// <param name="idEntranceVoucher"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.EntranceVoucher GetEntranceVoucherById(int idEntranceVoucher);

        /// <summary>
        ///Obtiene un EntranceVoucherDetailBatchSerial por IdEntrancevoucher
        ///</summary>
        ///<param name="EntranceVoucherId"></param>
        ///<returns></returns>
        ///<remarks></remarks>
        List<Domain.Entities.EntranceVoucherDetailBatchSerial> GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(int EntranceVoucherId);

        /// <summary>
        /// confirmar un comprobante de entrada
        /// </summary>
        /// <param name="entranceVoucher"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.EntranceVoucher>> ConfirmEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, AuditMessage audit, string ContainerNameCrystal, Boolean controlCost = false);

        /// <summary>
        /// guardar y confirmar un comprobante de entrada
        /// </summary>
        /// <param name="entranceVoucher"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.EntranceVoucher>> SaveAndConfirmbEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, string ContainerNameCrystal, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false);

        /// <summary>
        /// Método con el que se genera el objeto de la cuenta por pagar para el comprobante de entrada
        /// </summary>
        /// <param name="entranceVoucher"></param>
        /// <returns></returns>
        Task<ActionResult<AccountPayable>> CreateAccountPayableEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, ConsignmentRelationControl consignmentRelationControl = null);


    }
}
