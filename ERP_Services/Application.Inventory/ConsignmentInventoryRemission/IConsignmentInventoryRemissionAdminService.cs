//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Miguel Angel Fonseca
// Created          : 2017-12-12
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

#region Imports

using Application.Inventory.Rollback;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemission
{
    public interface IConsignmentInventoryRemissionAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionById(int id);

        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionByCode(string code, AuditMessage audit);

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentInventoryRemission> ConfirmConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, Boolean controlCost = false);

        /// <summary>
        /// guardar y confirmar una remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveAndConfirmbConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false);

        /// <summary>
        /// guardar y confirmar una remision al igual que su dispensación, esto para los tipos de remisión de gastos directos
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="sequenceC"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        ActionResult<string> SaveAndConfirmConsignmentInventoryRemissionAndPharmaceuticalDispensing(
            Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission,
            Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing,
            AuditMessage audit,
            long idSequense,
            Domain.Entities.InventorySequence sequenceC,
            Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert
        );
    }
}