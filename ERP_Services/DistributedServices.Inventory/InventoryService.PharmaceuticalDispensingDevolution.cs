using Application.Inventory.PharmaceuticalDispensingDevolution;
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
        /// Gets the pharmaceutical dispensing devolution by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionById(int id)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionAdminService>())
            {
                return service.GetPharmaceuticalDispensingDevolutionById(id);
            }
            //return _pharmaceuticalDispensingDevolutionAdminService.GetPharmaceuticalDispensingDevolutionById(id);
        }

        /// <summary>
        /// Gets the pharmaceutical dispensing devolution by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionAdminService>())
            {               
                return service.GetPharmaceuticalDispensingDevolutionByCode(code, audit);
            }
            //return _pharmaceuticalDispensingDevolutionAdminService.GetPharmaceuticalDispensingDevolutionByCode(code, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SavePharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionAdminService>())
            {                
                return service.SavePharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, audit, idSequense, sequenceC);
            }
            //return _pharmaceuticalDispensingDevolutionAdminService.SavePharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, audit, idSequense, sequenceC);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SaveAndConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionAdminService>())
            {                
                return service.SaveAndConfirmPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, audit, idSequense, action, sequenceC);
            }
            //return _pharmaceuticalDispensingDevolutionAdminService.SaveAndConfirmPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, audit, idSequense, action, sequenceC);
        }

        /// <summary>
        /// Saves the dashboard pharmacy devolution.
        /// </summary>
        /// <param name="ListPharmaceuticalDispensingDevolution">The list pharmaceutical dispensing devolution.</param>
        /// <param name="ListDetailAnnular">The list detail annular.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<string> SaveDashboardPharmacyDevolution(List<Domain.Entities.PharmaceuticalDispensingDevolution> ListPharmaceuticalDispensingDevolution, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution> ListDetailAnnular, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionAdminService>())
            {                
                return service.SaveDashboardPharmacyDevolution(ListPharmaceuticalDispensingDevolution, ListDetailAnnular, audit, idSequense);
            }
            //return _pharmaceuticalDispensingDevolutionAdminService .SaveDashboardPharmacyDevolution(ListPharmaceuticalDispensingDevolution , ListDetailAnnular, audit, idSequense);
        }
    }
}
