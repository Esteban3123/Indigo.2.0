using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.InventoryControl;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryInventoryControl
    {
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> SaveInventoryControl(Domain.Entities.InventoryControl InventoryControl, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {               
                return service.SaveInventoryControl(InventoryControl, audit, idSequense);
            }
            //return _inventoryControlAdminService.SaveInventoryControl(InventoryControl, audit, idSequense);
        }

        public Domain.Base.Entities.ActionResult DeleteInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {                
                return service.DeleteInventoryControl(InventoryControl, audit);
            }
            //return _inventoryControlAdminService.DeleteInventoryControl(InventoryControl, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> ChangeStateInventoryControl(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {                
                return service.ChangeStateInventoryControl(code, state, audit);
            }
            //return _inventoryControlAdminService.ChangeStateInventoryControl(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> GetInventoryControl(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {                
                return service.GetInventoryControl(code, audit);
            }
            //return _inventoryControlAdminService.GetInventoryControl(code, audit);
        }

        public Domain.Entities.InventoryControl GetInventoryControlById(int idInventoryControl)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {
                return service.GetInventoryControlById(idInventoryControl);
            }
            //return _inventoryControlAdminService.GetInventoryControlById(idInventoryControl);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> SaveAndConfirmbInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, long idSequense, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {               
                return service.SaveAndConfirmbInventoryControl(InventoryControl, audit, idSequense, action);
            }
            //return _inventoryControlAdminService.SaveAndConfirmbInventoryControl(InventoryControl, audit, idSequense, action);
        }





        /// <summary>
        /// obtyiene el control de inventarios por id sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryControl GetInventoryControlByIdNoAdded(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {
                return service.GetInventoryControlByIdNoAdded(id);
            }
            //return _inventoryControlAdminService.GetInventoryControlByIdNoAdded(id);
        }


        /// <summary>
        /// obtyiene el control de inventarios por codigo sin agregado solo con el original value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryControl GetInventoryControlByCodeNoAdded(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {               
                return service.GetInventoryControlByCodeNoAdded(code, audit);
            }
            //return _inventoryControlAdminService.GetInventoryControlByCodeNoAdded(code, audit);
        }

        /// <summary>
        /// lista los detalles del control de inventario
        /// </summary>
        /// <param name="inventoryControlId"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryControlDetail> GetInventoryControlDetailByInventoryControlId(int inventoryControlId)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {
                return service.GetInventoryControlDetailByInventoryControlId(inventoryControlId);
            }
            //return _inventoryControlAdminService.GetInventoryControlDetailByInventoryControlId(inventoryControlId);
        }


        /// <summary>
        /// metodo para validar los items pegados en la rejilla
        /// </summary>
        /// <param name="data"></param>
        /// <param name="warehouseId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlCopyPaste(List<List<string>> data, int warehouseId, int controlType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {              
                return service.SetProductsInventoryControlCopyPaste(data, warehouseId, controlType, audit);
            }
            //return _inventoryControlAdminService.SetProductsInventoryControlCopyPaste(data, warehouseId, controlType,audit );
        }

        /// <summary>
        /// metodo para validar los items que se estan importando en el archivo de excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetail>> SetProductsInventoryControlImportFile(List<Domain.Base.Entities.ImportFileRow> data, int warehouseId, int controlType, DateTime documentDate, int operatingUnitId, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {                
                return service.SetProductsInventoryControlImportFile(data, warehouseId, controlType, audit, documentDate, operatingUnitId);
            }
            //return _inventoryControlAdminService.SetProductsInventoryControlImportFile(data, warehouseId, controlType, audit, documentDate, operatingUnitId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> GetInventoryControlByIdAndStatus(int Id, byte status)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {
                return service.GetInventoryControlByIdAndStatus(Id, status);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventoryAdjustmentId"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetailBatchSerial>> GetInventoryAdjustmentControlByInventoryAdjustmentId(int InventoryAdjustmentId)
        {
            using (var service = Container.Current.Resolve<IInventoryControlAdminService>())
            {
                return service.GetInventoryAdjustmentControlByInventoryAdjustmentId(InventoryAdjustmentId);
            }
        }
    }
}
