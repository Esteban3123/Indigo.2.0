using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.DCI;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using DistributedServices.Inventory.Contracts;
using Domain.Entities;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryDCI
    {
        /// <summary>
        /// Guarda o actualiza un dci
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DCI> SaveDCI(Domain.Entities.DCI DCI, List<Domain.Entities.DrugInteraction> ListDeleteDrugInteraction, List<Domain.Entities.DrugActive> ListDeleteDrugActive, List<LethalDoseLimits> ListDeleteLethalDoseLimits, List<RisksDescription> ListDeleteRisksDescription, List<DCIRiskFactors> ListDeleteDCIRiskFactors, long idSequense, SessionValues audit)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {               
                return service.SaveDCI(DCI, ListDeleteDrugInteraction, ListDeleteDrugActive, ListDeleteLethalDoseLimits, ListDeleteRisksDescription, ListDeleteDCIRiskFactors, audit, idSequense);
            }
            //return _dciAdminService.SaveDCI(DCI, audit, idSequense);
        }

        /// <summary>
        /// Elimina un dci
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteDCI(Domain.Entities.DCI DCI, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {                
                return service.DeleteDCI(DCI, audit);
            }
            //return _dciAdminService.DeleteDCI(DCI, audit);
        }

        /// <summary>
        /// Obtiene un dci por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DCI> GetDCI(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {                
                return service.GetDCI(code, audit);
            }
            //return _dciAdminService.GetDCI(code, audit);
        }

        /// <summary>
        /// Obtiene un dci por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DCI> GetDCIById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {                
                return service.GetDCIById(id, audit);
            }
            //return _dciAdminService.GetDCIById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DCI> ChangeStateDCI(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {               
                return service.ChangeStateDCI(code, state, audit);
            }
            //return _dciAdminService.ChangeStateDCI(code, state, audit);
        }

        /// <summary>
        /// Obtiene una interacción de medicamento por el Id del DCI padre
        /// </summary>
        /// <param name="ParentDCIid"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public List<Domain.Entities.DrugInteraction> GetDrugInteractionByDCIParentId(int ParentDCIid)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {
                return service.GetDrugInteractionByDCIParentId(ParentDCIid);
            }
        }

        public IEnumerable<WarningHighRiskDrugModel> GetHighRiskDrugsByAtcCode(List<string> atcCodes)
        {
            using (var service = Container.Current.Resolve<IDCIAdminService>())
            {
                return service.GetHighRiskDrugsByAtcCode(atcCodes);
            }
        }
    }
}
