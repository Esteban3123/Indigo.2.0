//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Cesar Augusto Collazos
// Created          : 29/02/2024
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Infrastructure.CrossCutting.Base;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Application.Inventory.PharmaceuticalFormGrouping;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalFormGrouping> SavePharmaceuticalFormGrouping(Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalForm, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormGroupingAdminService>())
            {
                return service.SavePharmaceuticalFormGrouping(PharmaceuticalForm, audit);
            }
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGrouping(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormGroupingAdminService>())
            {
                return service.GetPharmaceuticalFormGrouping(code, audit);
            }
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGroupingById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormGroupingAdminService>())
            {
                return service.GetPharmaceuticalFormGroupingById(id, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalFormGrouping> ChangeStatePharmaceuticalFormGrouping(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormGroupingAdminService>())
            {
                return service.ChangeStatePharmaceuticalFormGrouping(code, state, audit);
            }
        }
    }
}
