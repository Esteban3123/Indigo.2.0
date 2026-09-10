using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdjustmentConcept> SaveAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {                
                return service.SaveAdjustmentConcept(adjustmentConcept, audit, idSequense);
            }
            //return _adjustmentConceptAdminService.SaveAdjustmentConcept(adjustmentConcept, audit, idSequense);
        }
        /// <summary>
        /// Elimina un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {                
                return service.DeleteAdjustmentConcept(adjustmentConcept, audit);
            }
            //return _adjustmentConceptAdminService.DeleteAdjustmentConcept(adjustmentConcept, audit);
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConcept(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {               
                return service.GetAdjustmentConcept(code, audit);
            }
            //return _adjustmentConceptAdminService.GetAdjustmentConcept(code, audit);
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConceptById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {               
                return service.GetAdjustmentConceptById(id, audit);
            }
            //return _adjustmentConceptAdminService.GetAdjustmentConceptById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdjustmentConcept> ChangeStateAdjustmentConcept(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {                
                return service.ChangeStateAdjustmentConcept(code, state, audit);
            }
            //return _adjustmentConceptAdminService.ChangeStateAdjustmentConcept(code, state, audit);
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por cuenta contable y centro de costo.
        /// </summary>
        /// <param name="conceptType"></param>
        /// <param name="adjustmentAccountId"></param>
        /// <param name="costCenterId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.AdjustmentConcept>> GetListByAdjustmentAccountCostCenterId(Byte conceptType, int adjustmentAccountId, int costCenterId, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdjustmentConceptAdminService>())
            {
                return service.GetListByAdjustmentAccountCostCenterId(conceptType, adjustmentAccountId, costCenterId, audit);
            }
        }

    }
}
