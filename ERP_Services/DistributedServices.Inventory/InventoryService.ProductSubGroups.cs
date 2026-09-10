using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.ProductSubGroups;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductSubGroup> SaveProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductSubGroupsAdminService>())
            {                
                return service.SaveProductSubGroup(productSubGroup, audit, idSequense);
            }
            //return _productSubGroupAdminService.SaveProductSubGroup(productSubGroup, audit, idSequense);
        }

        /// <summary>
        /// Elimina un subgrupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteProductSubGroup(Domain.Entities.ProductSubGroup productSubGroup, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductSubGroupsAdminService>())
            {               
                return service.DeleteProductSubGroup(productSubGroup, audit);
            }
            //return _productSubGroupAdminService.DeleteProductSubGroup(productSubGroup, audit);
        }

        /// <summary>
        /// Obtiene un subgrupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroup(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductSubGroupsAdminService>())
            {               
                return service.GetProductSubGroup(code, audit);
            }
            //return _productSubGroupAdminService.GetProductSubGroup(code, audit);
        }

        /// <summary>
        /// Obtiene un subgrupo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductSubGroup> GetProductSubGroupById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductSubGroupsAdminService>())
            {               
                return service.GetProductSubGroupById(id, audit);
            }
            //return _productSubGroupAdminService.GetProductSubGroupById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductSubGroup> ChangeStateSubGroup(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductSubGroupsAdminService>())
            {                
                return service.ChangeStateSubGroup(code, state, audit);
            }
            //return _productSubGroupAdminService.ChangeStateSubGroup(code, state, audit);
        }
    }
}
