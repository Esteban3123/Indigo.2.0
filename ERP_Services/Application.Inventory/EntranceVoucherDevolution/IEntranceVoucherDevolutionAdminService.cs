//'************************************************************
//' Assembly         : Domain.Inventory.EntranceVoucherRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 09/03/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

#region Imported Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Inventory.Rollback;
#endregion

namespace Application.Inventory.AppEntranceVoucherDevolution
{
    public interface IEntranceVoucherDevolutionAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un EntranceVoucherDevolution
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina un EntranceVoucherDevolution
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de EntranceVoucherDevolution
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucherDevolution> ChangeStateEntranceVoucherDevolution(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el EntranceVoucherDevolution por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucherDevolution> GetEntranceVoucherDevolution(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un EntranceVoucherDevolution por id
        /// </summary>
        /// <param name="idEntranceVoucherDevolution"></param>
        /// <returns></returns>
        Domain.Entities.EntranceVoucherDevolution GetEntranceVoucherDevolutionById(int idEntranceVoucherDevolution);

        /// <summary>
        /// guardar y confirmar una devolucion de comprobante de entrada
        /// </summary>
        /// <param name="entranceDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveAndConfirmbEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution entranceDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);
    }
}
