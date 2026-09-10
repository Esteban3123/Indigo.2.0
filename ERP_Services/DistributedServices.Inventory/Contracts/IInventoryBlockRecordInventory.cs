///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Mario Arias Rubiano
/// Created          : 10/09/2014
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
    public interface IInventoryBlockRecordInventory
    {
        /// <summary>
        /// Obtiene el registro bloqueado
        /// </summary>
        /// <param name="IdForm"></param>
        /// <param name="IdRecord"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.BlockRecordInventory GetBlockRecordInventoryByIdformAndIdRecord(string IdForm, string IdRecord);

        /// <summary>
        /// Guarda un registro bloqueado
        /// </summary>
        /// <param name="blockRecordInventory"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.BlockRecordInventory> SaveBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecordInventory);

        /// <summary>
        /// Elimina un registro bloqueado
        /// </summary>
        /// <param name="blockRecorInventory"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecorInventory);
    }
}
