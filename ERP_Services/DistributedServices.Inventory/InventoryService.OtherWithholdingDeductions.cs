using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory.OtherWithholdingDeduction;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> SaveOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {                
                return service.SaveOtherWithholdingDeduction(otherWithholdingDeduction, audit, idSequense);
            }
            //return _otherWithholdingDeductions.SaveOtherWithholdingDeduction(otherWithholdingDeduction, audit, idSequense);
        }

        /// <summary>
        /// Elimina otras deducciones y retenciones
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {               
                return service.DeleteOtherWithholdingDeduction(otherWithholdingDeduction, audit);
            }
            //return _otherWithholdingDeductions.DeleteOtherWithholdingDeduction(otherWithholdingDeduction, audit);
        }

        /// <summary>
        /// Obtiene otras deducciones y retenciones por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeduction(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {                
                return service.GetOtherWithholdingDeduction(code, audit);
            }
            //return _otherWithholdingDeductions.GetOtherWithholdingDeduction(code, audit);
        }

        /// <summary>
        /// Obtiene otras deducciones y retenciones por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeductionById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {               
                return service.GetOtherWithholdingDeductionById(id, audit);
            }
            //return _otherWithholdingDeductions.GetOtherWithholdingDeductionById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> ChangeStateOtherWithholdingDeduction(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {                
                return service.ChangeStateOtherWithholdingDeduction(code, state, audit);
            }
            //return _otherWithholdingDeductions.ChangeStateOtherWithholdingDeduction(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<List<Domain.Entities.OtherWithholdingDeduction>> ListOtherWithholdingDeduction()
        {
            using (var service = Container.Current.Resolve<IOtherWithholdingDeductionAdminService>())
            {
                return service.ListOtherWithholdingDeduction();
            }
            //return _otherWithholdingDeductions.ListOtherWithholdingDeduction();
        }

    }
}
