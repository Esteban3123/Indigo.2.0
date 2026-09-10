//'***********************************************************************
//' Assembly         : Application.Payments
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 10/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.BlockRecordInventory
{
    public interface IBlockRecordInventoryAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene el registro bloqueado
        /// </summary>
        /// <param name="IdForm"></param>
        /// <param name="IdRecord"></param>
        /// <returns></returns>
        Domain.Entities.BlockRecordInventory GetBlockRecordInventoryByIdformAndIdRecord(string IdForm, string IdRecord);

        /// <summary>
        /// Guarda un registro bloqueado
        /// </summary>
        /// <param name="blockRecordInventory"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.BlockRecordInventory> SaveBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecordInventory);

        /// <summary>
        /// Elimina un registro bloqueado
        /// </summary>
        /// <param name="blockRecorInventory"></param>
        /// <returns></returns>
        ActionResult DeleteBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecorInventory);
    }
}
