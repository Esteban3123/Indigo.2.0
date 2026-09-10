//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Faiber Julian Mora D.
// Created          : 28-09-2016
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

namespace Application.Inventory.InventoryRequestDevolution
{
    public interface IInventoryRequestDevolutionAdminService : IDisposable
    {

        /// <summary>
        /// Guarda o actualiza una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <param name="secuenceC"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequestDevolution> SaveInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit, Int64 idSecuence = 0, InventorySequence secuenceC = null);

        /// <summary>
        /// Guarda y confirma una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequestDevolution> SaveAndConfirmRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestDevolution, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequestDevolution> ChangeStatusInventoryRequestDevolution(string code, byte status, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de devolución de inventario por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestDevolutionByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de devolución de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequestDevolution> GetInventoryRequestDevolutionById(int id, AuditMessage audit);

    }
}
