using Infrastructure.CrossCutting.Base;
using Application.Inventory.DevolutionCause;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using System;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {

        /// <summary>
        /// Consulta la causa de devolución por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDevolutionCauseAdminService>())
            {                
                return service.GetDevolutionCauseByCode(code, audit);
            }
        }

        /// <summary>
        /// Consulta la causa de devolución por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDevolutionCauseAdminService>())
            {                
                return service.GetDevolutionCauseById(id, audit);
            }
        }

        /// <summary>
        /// Guarda o actualiza una causa de devolución
        /// </summary>
        /// <param name="devolutionCause"></param>
        /// <param name="audit"></param>
        /// <param name="secuenceId"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DevolutionCause> SaveDevolutionCause(Domain.Entities.DevolutionCause devolutionCause, AuditMessage audit, Int64 secuenceId = 0)
        {
            using (var service = Container.Current.Resolve<IDevolutionCauseAdminService>())
            {
                return service.SaveDevolutionCause(devolutionCause, audit, secuenceId);
            }
        }

        /// <summary>
        /// Cambia el estado de una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.DevolutionCause> ChangeStateDevolutionCause(int id, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDevolutionCauseAdminService>())
            {                
                return service.ChangeStateDevolutionCause(id, state, audit);
            }
        }

        /// <summary>
        /// Elimina una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteDevolutionCause(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IDevolutionCauseAdminService>())
            {
                return service.DeleteDevolutionCause(id, audit);
            }
        }

    }
}
