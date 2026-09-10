using Application.Inventory.RemissionEntrance;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.RemissionEntrance GetRemissionEntranceByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceAdminService>())
            {                
                return service.GetRemissionEntranceByCode(code, audit);
            }
            //return _remissionEntranceAdminService.GetRemissionEntranceByCode(code, audit);
        }
        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.RemissionEntrance GetRemissionEntranceById(int id)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceAdminService>())
            {
                return service.GetRemissionEntranceById(id);
            }
            //return _remissionEntranceAdminService.GetRemissionEntranceById(id);
        }
        /// <summary>
        /// guarda unan renmision
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.RemissionEntrance> SaveRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceAdminService>())
            {                
                return service.SaveRemissionEntrance(remissionEntrance, audit, idSequense, sequenceC);
            }
            //return _remissionEntranceAdminService.SaveRemissionEntrance(remissionEntrance, audit, idSequense, sequenceC);
        }
        /// <summary>
        /// guarda y confirma la remision
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.RemissionEntrance> SaveAndConfirmbRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, Boolean controlCost = false)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceAdminService>())
            {                
                return service.SaveAndConfirmbRemissionEntrance(remissionEntrance, audit, idSequense, action, sequenceC, controlCost);
            }
            //return _remissionEntranceAdminService.SaveAndConfirmbRemissionEntrance(remissionEntrance, audit, idSequense, action, sequenceC, controlCost);
        }
    }
}
