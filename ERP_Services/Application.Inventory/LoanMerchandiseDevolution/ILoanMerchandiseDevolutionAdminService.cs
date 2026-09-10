//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Rafael Eduardo Patiño Cabrera
// Created          : 07-05-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Inventory.Rollback;

namespace Application.Inventory.LoanMerchandiseDevolution
{
    public interface ILoanMerchandiseDevolutionAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
       /// <summary>
       /// obtiene una solicitud de devolucion de prestamo por su codigo
       /// </summary>
       /// <param name="Code"></param>
       /// <param name="audit"></param>
       /// <returns></returns>
        Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionByCode(string Code, AuditMessage audit);
        /// <summary>
        /// obtiene una solicitud de devolucion de prestamo por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionById(int id);
        /// <summary>
        /// guarda una solicitud de devolucion de prestamo
        /// </summary>
        /// <param name="loadmerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.LoanMerchandiseDevolution> SaveLoanMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// Confirmar una soliciutd de devolucion de prestamo
        /// </summary>
        /// <param name="loadMerchadise"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.LoanMerchandiseDevolution>> ConfirmLoandMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit);
        /// <summary>
        /// guardar y confirmar una solicitud de devolucion de prestamo
        /// </summary>
        /// <param name="loadMerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.LoanMerchandiseDevolution>> SaveAndConfirmLoadMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);
    }
}
