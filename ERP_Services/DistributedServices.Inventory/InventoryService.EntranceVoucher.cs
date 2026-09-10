using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.EntranceVoucher;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryEntranceVoucher
    {
        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucher> SaveEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
         {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {                
                return service.SaveEntranceVoucher(EntranceVoucher, audit, idSequense, sequenceC);
            }
            //return _entranceVoucherAdminService.SaveEntranceVoucher(EntranceVoucher, audit, idSequense, sequenceC);
        }

        public Domain.Base.Entities.ActionResult DeleteEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {              
                return service.DeleteEntranceVoucher(EntranceVoucher, audit);
            }
            //return _entranceVoucherAdminService.DeleteEntranceVoucher(EntranceVoucher, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucher> ChangeStateEntranceVoucher(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {               
                return service.ChangeStateEntranceVoucher(code, state, audit);
            }
            //return _entranceVoucherAdminService.ChangeStateEntranceVoucher(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucher> GetEntranceVoucher(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {               
                return service.GetEntranceVoucher(code, audit);
            }
            //return _entranceVoucherAdminService.GetEntranceVoucher(code, audit);
        }

        public Domain.Entities.EntranceVoucher GetEntranceVoucherById(int idEntranceVoucher)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {
                return service.GetEntranceVoucherById(idEntranceVoucher);
            }
            //return _entranceVoucherAdminService.GetEntranceVoucherById(idEntranceVoucher);
        }

        public List<Domain.Entities.EntranceVoucherDetailBatchSerial> GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(int EntranceVoucherId)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {
                return service.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId);
            }
            //return _entranceVoucherAdminService.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId);
        }

        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucher>> SaveAndConfirmbEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, string ContainerNameCrystal, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherAdminService>())
            {                
                return await service.SaveAndConfirmbEntranceVoucherAsync(entranceVoucher, ContainerNameCrystal, audit, idSequense, action, sequenceC, controlCost);
            }
        }
    }
}
