using Application.Inventory.RemissionOutput;
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
        /// Gets the remission output by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Entities.RemissionOutput GetRemissionOutputByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputAdminService>())
            {                
                return service.GetRemissionOutputByCode(code, audit);
            }
            //return _remissionOutputAdminService.GetRemissionOutputByCode(code, audit);
        }

        /// <summary>
        /// Gets the remission output by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.RemissionOutput GetRemissionOutputById(int id)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputAdminService>())
            {
                return service.GetRemissionOutputById(id);
            }
            //return _remissionOutputAdminService.GetRemissionOutputById(id);
        }

        /// <summary>
        /// Saves the remission output.
        /// </summary>
        /// <param name="RemissionOutput">The remission output.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.RemissionOutput> SaveRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputAdminService>())
            {                
                return service.SaveRemissionOutput(RemissionOutput, audit, idSequense, sequenceC);
            }
            //return _remissionOutputAdminService.SaveRemissionOutput(RemissionOutput, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// Saves the and confirmb remission output.
        /// </summary>
        /// <param name="RemissionOutput">The remission output.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.RemissionOutput> SaveAndConfirmbRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputAdminService>())
            {                
                return service.SaveAndConfirmRemissionOutput(RemissionOutput, audit, idSequense, action, sequenceC);
            }
            //return _remissionOutputAdminService.SaveAndConfirmRemissionOutput(RemissionOutput, audit, idSequense, action, sequenceC);
        }
    }
}
