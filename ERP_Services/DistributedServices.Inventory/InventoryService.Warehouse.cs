using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.Warehouse;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Warehouse> SaveWarehouse(Domain.Entities.Warehouse warehouse, long idSequense, AuditMessage audit, List<Domain.Entities.DecreaseMaximumLimit> Decrease = null)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {                
                return service.SaveWarehouse(warehouse, audit, Decrease, idSequense);
            }
            //return _warehouseAdminService.SaveWarehouse(warehouse, audit, idSequense);
        }

        /// <summary>
        /// Elimina un almacen
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {                
                return service.DeleteWarehouse(warehouse, audit);
            }
            //return _warehouseAdminService.DeleteWarehouse(warehouse, audit);
        }

        /// <summary>
        /// Obtiene un almacen por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Warehouse> GetWarehouse(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {                
                return service.GetWarehouse(code, audit);
            }
            //return _warehouseAdminService.GetWarehouse(code, audit);
        }

        /// <summary>
        /// Obtiene un almacen por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Warehouse> GetWarehouseById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {                
                return service.GetWarehouseById(id, audit);
            }
            //return _warehouseAdminService.GetWarehouseById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Warehouse> ChangeStateWarehouse(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {                
                return service.ChangeStateWarehouse(code, state, audit);
            }
            //return _warehouseAdminService.ChangeStateWarehouse(code, state, audit);
        }


        public List<string> ListPrefixs()
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {
                return service.ListPrefixs();
            }
            //return this._warehouseAdminService.ListPrefixs();
        }

        /// <summary>
        /// Obtiene la bodega de un proveedor por tipo
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="type"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Warehouse> GetWarehouseSupplierByType(int supplierId, int type, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IWarehouseAdminService>())
            {
                return service.GetWarehouseSupplierByType(supplierId, type, audit);
            }
        }
    }
}
