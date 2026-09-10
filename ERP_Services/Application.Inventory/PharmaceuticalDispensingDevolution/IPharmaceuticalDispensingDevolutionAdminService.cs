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
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Application.Inventory.Rollback;


namespace Application.Inventory.PharmaceuticalDispensingDevolution
{
    public interface IPharmaceuticalDispensingDevolutionAdminService : IAdminServiceRollbackStrategy ,IDisposable
    {
        /// <summary>
        /// metodo para hacer la devolucion del sumistro desde el dashboard
        /// </summary>
        /// <param name="ListPharmaceuticalDispensingDevolution"></param>
        /// <param name="ListDetailAnnular"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<string> SaveDashboardPharmacyDevolution(List<Domain.Entities.PharmaceuticalDispensingDevolution > ListPharmaceuticalDispensingDevolution, List<ViewDashboardPharmacyDetailDevolution > ListDetailAnnular, AuditMessage audit, long idSecuence = 0);
        /// <summary>
        /// obtiene una devolucion por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionById(int id);
        /// <summary>
        /// obtiene una devolucion por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionByCode(string code, AuditMessage audit);
        /// <summary>
        /// guarda una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution">The pharmaceutical dispensing devolution.</param>
        /// <param name="audit">The audit.</param>
        /// <param name="idSequence">The identifier sequence.</param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SavePharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null);
        /// <summary>
        /// confirma una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> ConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit);
        /// <summary>
        /// guarda y confirma una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SaveAndConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null);

    }
}
