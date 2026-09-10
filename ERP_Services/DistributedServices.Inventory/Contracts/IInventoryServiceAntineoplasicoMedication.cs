///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Hector Rodriguez Rubiano
/// Created          : 04-08-2020
/// 
/// Copyright        : (c) . All rights reserved.
/// About            : PBI10044
///***********************************************************************

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceAntineoplasicoMedication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaAPM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.AntineoplasicoMedication> SaveAntineoplasicoMedication(List<Domain.Entities.AntineoplasicoMedication> listaAPM, AuditMessage audit);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesAPM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<List<Domain.Entities.AntineoplasicoMedication>> GetAntineoplasicoMedicationByStates(string statesAPM, AuditMessage audit);
    }
}

