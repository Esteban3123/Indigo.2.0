using Application.Inventory.ConsignmentTransfer;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceConsignmentTransfer
    {
        public ActionResult<ConsignmentTransfer> ConfirmConsignmentTransfer(ConsignmentTransfer consignmentTransfer, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.ConfirmConsignmentTransfer(consignmentTransfer, audit);
            }
        }

        public ConsignmentMovementInventory GetConsignmentInventoryQuantities(int warehouseId, int productId)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.GetConsignmentInventoryQuantities(warehouseId, productId);
            }
        }

        public ConsignmentTransfer GetConsignmentTransferByCode(string code)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.GetConsignmentTransferByCode(code);
            }
        }

        public ConsignmentTransfer GetConsignmentTransferById(int id)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.GetConsignmentTransferById(id);
            }
        }

        public ActionResult<ConsignmentTransfer> SaveAndConfirmConsignmentTransfer(ConsignmentTransfer consignmentTransfer, AuditMessage audit, long idSequence = 0)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.SaveAndConfirmConsignmentTransfer(consignmentTransfer, audit, idSequence);
            }
        }

        public ActionResult<ConsignmentTransfer> SaveConsignmentTransfer(ConsignmentTransfer consignmentTransfer, AuditMessage audit, long idSequence = 0)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.SaveConsignmentTransfer(consignmentTransfer, audit, idSequence);
            }
        }

        public ActionResult<List<ConsignmentTransferDetail>> SetConsignmentTransferImportFile(List<ImportFileRow> listRows, int sourceWarehouseId, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.SetConsignmentTransferImportFile(listRows, sourceWarehouseId);
            }
        }
        public ActionResult<List<ConsignmentTransferDetail>> SetConsignmentTransferDetailFromCopyPaste(List<List<string>> dataImport, int sourceWarehouseId, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentTransferAdminService>())
            {
                return service.SetConsignmentTransferDetailFromCopyPaste(dataImport, sourceWarehouseId);
            }
        }
    }

}
