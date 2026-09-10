using Application.Inventory.BlockRecordInventory;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Obtiene un registro bloqueado
        /// </summary>
        /// <param name="IdForm"></param>
        /// <param name="IdRecord"></param>
        /// <returns></returns>
        public Domain.Entities.BlockRecordInventory GetBlockRecordInventoryByIdformAndIdRecord(string IdForm, string IdRecord)
        {
            using (var service = Container.Current.Resolve<IBlockRecordInventoryAdminService>())
            {
                return service.GetBlockRecordInventoryByIdformAndIdRecord(IdForm, IdRecord);
            }
            //return _blockRecordInventoryAdminService.GetBlockRecordInventoryByIdformAndIdRecord(IdForm, IdRecord);
        }

        /// <summary>
        /// Guarda un registro para bloquear
        /// </summary>
        /// <param name="blockRecordInventory"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BlockRecordInventory> SaveBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecordInventory)
        {
            using (var service = Container.Current.Resolve<IBlockRecordInventoryAdminService>())
            {
                return service.SaveBlockRecordInventory(blockRecordInventory);
            }
            //return _blockRecordInventoryAdminService.SaveBlockRecordInventory(blockRecordInventory);
        }

        /// <summary>
        /// Elimina un registro bloqueado
        /// </summary>
        /// <param name="blockRecorInventory"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecorInventory)
        {
            using (var service = Container.Current.Resolve<IBlockRecordInventoryAdminService>())
            {
                return service.DeleteBlockRecordInventory(blockRecorInventory);
            }
            //return _blockRecordInventoryAdminService.DeleteBlockRecordInventory(blockRecorInventory);
        }
    }
}
