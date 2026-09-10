using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.AppEntranceVoucherDevolution;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryEntranceVoucherDevolution
    {
        /// <summary>
        /// Guarda la devolucion del comprobante
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {                
                return service.SaveEntranceVoucherDevolution(EntranceVoucherDevolution, audit, idSequense, sequenceC);
            }
            //return _entranceVoucherDevolutionAdminService.SaveEntranceVoucherDevolution(EntranceVoucherDevolution, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// Elimina la devolucion del comprobante
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {                
                return service.DeleteEntranceVoucherDevolution(EntranceVoucherDevolution, audit);
            }
            //return _entranceVoucherDevolutionAdminService.DeleteEntranceVoucherDevolution(EntranceVoucherDevolution, audit);
        }

        /// <summary>
        /// Cambia el estado del registro de la devolucion
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucherDevolution> ChangeStateEntranceVoucherDevolution(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {                
                return service.ChangeStateEntranceVoucherDevolution(code, state, audit);
            }
            //return _entranceVoucherDevolutionAdminService.ChangeStateEntranceVoucherDevolution(code, state, audit);
        }

        /// <summary>
        /// Obtiene la devolucion del comprobante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucherDevolution> GetEntranceVoucherDevolution(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {               
                return service.GetEntranceVoucherDevolution(code, audit);
            }
            //return _entranceVoucherDevolutionAdminService.GetEntranceVoucherDevolution(code, audit);
        }

        /// <summary>
        /// Obtiene la devolucion del comprobante por id
        /// </summary>
        /// <param name="idEntranceVoucherDevolution"></param>
        /// <returns></returns>
        public Domain.Entities.EntranceVoucherDevolution GetEntranceVoucherDevolutionById(int idEntranceVoucherDevolution)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {
                return service.GetEntranceVoucherDevolutionById(idEntranceVoucherDevolution);
            }
            //return _entranceVoucherDevolutionAdminService.GetEntranceVoucherDevolutionById(idEntranceVoucherDevolution);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entranceDevolution"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveAndConfirmbEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution entranceDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IEntranceVoucherDevolutionAdminService>())
            {               
                return service.SaveAndConfirmbEntranceVoucherDevolution(entranceDevolution, audit, idSequense, action, sequenceC);
            }
            //return _entranceVoucherDevolutionAdminService.SaveAndConfirmbEntranceVoucherDevolution(entranceDevolution, audit, idSequense, action, sequenceC);
        }
    }
}
