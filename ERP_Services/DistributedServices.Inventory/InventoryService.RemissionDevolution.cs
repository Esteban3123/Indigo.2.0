using Application.Inventory.RemissionDevolution;
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
        /// Gets the remission devolution by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Entities.RemissionDevolution GetRemissionDevolutionByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IRemissionDevolutionAdminService>())
            {                
                return service.GetRemissionDevolutionByCode(code, audit);
            }
            //return _remissionDevolutionAdminService .GetRemissionDevolutionByCode (code, audit);
        }

        /// <summary>
        /// Saves the remission devolution.
        /// </summary>
        /// <param name="RemissionDevolution">The remission devolution.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.RemissionDevolution> SaveRemissionDevolution(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IRemissionDevolutionAdminService>())
            {               
                return service.SaveRemissionDevolution(RemissionDevolution, audit, idSequense, sequenceC);
            }
            //return _remissionDevolutionAdminService.SaveRemissionDevolution(RemissionDevolution, audit, idSequense, sequenceC);
        }

        /// <summary>
        /// Saves the and confirmb remission devolution.
        /// </summary>
        /// <param name="RemissionDevolution">The remission devolution.</param>
        /// <param name="action">The action.</param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.RemissionDevolution>> SaveAndConfirmbRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, bool controlCost = false)
        {
            using (var service = Container.Current.Resolve<IRemissionDevolutionAdminService>())
            {                
                return await service.SaveAndConfirmRemissionDevolutionAsync(RemissionDevolution, audit, idSequense, action, sequenceC, controlCost);
            }
        }
    }
}
