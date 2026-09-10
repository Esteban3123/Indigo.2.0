///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Hector Rodriguez Rubiano
/// Created          : 06-04-2020
/// 
/// Copyright        : (c) . All rights reserved.
/// About            : PBI8934
///***********************************************************************

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceBacterialResistanceMedication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.BacterialResistanceMedication> SaveBacterialResistanceMedication(List<Domain.Entities.BacterialResistanceMedication> listaBRM, AuditMessage audit);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<List<Domain.Entities.BacterialResistanceMedication>> GetBacterialResistanceMedicationByStates(string statesBRM, AuditMessage audit);
    }
}

