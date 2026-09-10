using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.Manufacturer;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Manufacturer> SaveManufacturer(Domain.Entities.Manufacturer manufacturer, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IManufacturerAdminService>())
            {                
                return service.SaveManufacturer(manufacturer, audit, idSequense);
            }
            //return _manufacturerAdminService.SaveManufacturer(manufacturer, audit, idSequense);
        }

        /// <summary>
        /// Elimina un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IManufacturerAdminService>())
            {               
                return service.DeleteManufacturer(manufacturer, audit);
            }
            //return _manufacturerAdminService.DeleteManufacturer(manufacturer, audit);
        }

        /// <summary>
        /// Obtiene un fabricante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Manufacturer> GetManufacturer(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IManufacturerAdminService>())
            {                
                return service.GetManufacturer(code, audit);
            }
            //return _manufacturerAdminService.GetManufacturer(code, audit);
        }

        /// <summary>
        /// Obtiene un fabricante por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Manufacturer> GetManufacturerById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IManufacturerAdminService>())
            {               
                return service.GetManufacturerById(id, audit);
            }
            //return _manufacturerAdminService.GetManufacturerById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.Manufacturer> ChangeStateManufacturer(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IManufacturerAdminService>())
            {                
                return service.ChangeStateManufacturer(code, state, audit);
            }
            //return _manufacturerAdminService.ChangeStateManufacturer(code, state, audit);
        }


    }
}
