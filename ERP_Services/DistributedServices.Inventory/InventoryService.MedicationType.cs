using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.Warehouse;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Application.Inventory.MedicationType;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {

        /// <summary>
        /// Obtiene el tipo de medicamento por su código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.MedicationType> GetMedicationTypeByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMedicationTypeAdminService>())
            {
                return service.GetMedicationTypeByCode(code, audit);
            }
        }

        /// <summary>
        /// Guarda un nuevo tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="audit"></param>
        /// <param name="idSequense"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.MedicationType> SaveMedicationType(Domain.Entities.MedicationType medicationType, AuditMessage audit, long idSequense)
        {
            using (var service = Container.Current.Resolve<IMedicationTypeAdminService>())
            {
                return service.SaveMedicationType(medicationType, audit, idSequense);
            }
        }

        /// <summary>
        /// Elimina un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteMedicationType(Domain.Entities.MedicationType medicationType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMedicationTypeAdminService>())
            {
                return service.DeleteMedicationType(medicationType, audit);
            }
        }


        /// <summary>
        /// Cambia el estado de un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.MedicationType> ChangeStateMedicationType(Domain.Entities.MedicationType medicationType, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMedicationTypeAdminService>())
            {
                return service.ChangeStateMedicationType(medicationType, state, audit);
            }
        }

    }
}
