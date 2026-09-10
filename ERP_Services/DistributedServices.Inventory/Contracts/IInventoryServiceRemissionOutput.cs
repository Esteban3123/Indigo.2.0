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
    public interface IInventoryServiceRemissionOutput
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.RemissionOutput GetRemissionOutputByCode(string code, AuditMessage audit);
        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.RemissionOutput GetRemissionOutputById(int id);
        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.RemissionOutput> SaveRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC);
        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.RemissionOutput> SaveAndConfirmbRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);

    }
}
