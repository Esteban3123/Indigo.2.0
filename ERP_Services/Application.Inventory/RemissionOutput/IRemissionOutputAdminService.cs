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

namespace Application.Inventory.RemissionOutput
{
    public interface IRemissionOutputAdminService : IDisposable
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.RemissionOutput GetRemissionOutputByCode(string code, AuditMessage audit);

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.RemissionOutput GetRemissionOutputById(int id);
        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionOutput> SaveRemissionOutput(Domain.Entities.RemissionOutput remissionOutput, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="RemissionOutputId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionOutput> ConfirmRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit);
        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.RemissionOutput> SaveAndConfirmRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);

    }
}
