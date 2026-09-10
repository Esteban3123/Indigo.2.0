//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Carlos Ernesto Cordoba
// Created          : 08-01-2015
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

namespace Application.Inventory.RemissionDevolution
{
    public interface IRemissionDevolutionAdminService : IAdminServiceRollbackStrategy ,IDisposable
    {
        /// <summary>
        /// obtiene una devolucion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.RemissionDevolution GetRemissionDevolutionByCode(string code, AuditMessage audit);
        /// <summary>
        /// guarda una devolucion
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionDevolution> SaveRemissionDevolution(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// guardar y confirmar una devolucion
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        Task<ActionResult<Domain.Entities.RemissionDevolution>> SaveAndConfirmRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false);

        Task<ActionResult<Domain.Entities.RemissionDevolution>> ConfirmRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, Boolean controlCost = false);
    }
}
