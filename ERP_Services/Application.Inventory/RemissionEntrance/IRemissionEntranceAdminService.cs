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

namespace Application.Inventory.RemissionEntrance
{
    public interface IRemissionEntranceAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.RemissionEntrance GetRemissionEntranceByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.RemissionEntrance GetRemissionEntranceById(int id);
        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionEntrance> SaveRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionEntrance> ConfirmRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, Boolean controlCost = false);
        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionEntrance> SaveAndConfirmbRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false);

    }
}
