//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Rafael Eduardo Patiño Cabrera
// Created          : 24-04-2015
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

namespace Application.Inventory.LoanMerchandise
{
    public interface ILoanMerchandiseAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
       /// <summary>
       /// obtiene una solicitud de prestamo por su codigo
       /// </summary>
       /// <param name="Code"></param>
       /// <param name="audit"></param>
       /// <returns></returns>
        Domain.Entities.LoanMerchandise GetLoanMerchadiseByCode(string Code, AuditMessage audit);
        /// <summary>
        /// otiene una soliciutd de prestamo por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.LoanMerchandise GetLoanMerchadiseById(int id);
        /// <summary>
        /// guarda una solicitud de prestamo
        /// </summary>
        /// <param name="loadmerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.LoanMerchandise> SaveLoanMerchadise(Domain.Entities.LoanMerchandise loadmerchadise, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// Confirmar una soliciutd de prestamo
        /// </summary>
        /// <param name="loadMerchadise"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.LoanMerchandise> ConfirmLoandMerchadise(Domain.Entities.LoanMerchandise loadMerchadise, AuditMessage audit);
        /// <summary>
        /// guardar y confirmar una solicitud de prestamo
        /// </summary>
        /// <param name="loadMerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.LoanMerchandise> SaveAndConfirmLoadMerchadise(Domain.Entities.LoanMerchandise loadMerchadise, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);
    }
}
