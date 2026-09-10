using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.MeasureUnit;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryMeasurementUnit> SaveMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMeasureUnitAdminService>())
            {               
                return service.SaveMeasureUnit(measureUnit, audit, idSequense);
            }
            //return _measureUnitAdminService.SaveMeasureUnit(measureUnit, audit, idSequense);
        }

        /// <summary>
        /// Elimina una unidad de medida
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteMeasureUnit(Domain.Entities.InventoryMeasurementUnit measureUnit, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMeasureUnitAdminService>())
            {                
                return service.DeleteMeasureUnit(measureUnit, audit);
            }
            //return _measureUnitAdminService.DeleteMeasureUnit(measureUnit, audit);
        }

        /// <summary>
        /// Obtiene una unidad de medida por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnit(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMeasureUnitAdminService>())
            {                
                return service.GetMeasureUnit(code, audit);
            }
            //return _measureUnitAdminService.GetMeasureUnit(code, audit);
        }

        /// <summary>
        /// Obtiene una unidad de medida por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryMeasurementUnit> GetMeasureUnitById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMeasureUnitAdminService>())
            {                
                return service.GetMeasureUnitById(id, audit);
            }
            //return _measureUnitAdminService.GetMeasureUnitById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryMeasurementUnit> ChangeStateMeasureUnit(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IMeasureUnitAdminService>())
            {                
                return service.ChangeStateMeasureUnit(code, state, audit);
            }
            //return _measureUnitAdminService.ChangeStateMeasureUnit(code, state, audit);
        }


    }
}
