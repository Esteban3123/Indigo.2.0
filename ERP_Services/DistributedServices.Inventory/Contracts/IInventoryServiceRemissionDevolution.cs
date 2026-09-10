///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Ernesto Cordoba
/// Created          : 08-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceRemissionDevolution
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.RemissionDevolution GetRemissionDevolutionByCode(string code, AuditMessage audit);
        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.RemissionDevolution> SaveRemissionDevolution(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC);
        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Domain.Entities.RemissionDevolution>> SaveAndConfirmbRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, bool controlCost = false);

    }
}
