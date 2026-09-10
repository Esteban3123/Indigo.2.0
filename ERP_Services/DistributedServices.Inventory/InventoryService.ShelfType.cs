using Application.Inventory.ShelfType;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {

		/// <summary>
		/// Obtiene todos los tipos de estante 
		/// </summary>
		/// <returns></returns>
		public List<Domain.Entities.ShelfType> ListAllShelfType(AuditMessage audit)
		{
			using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
			{
				return service.ListAllShelfType( audit);
			}
		}

		/// <summary>
		/// Guarda o actualiza un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <returns></returns>
		public Domain.Base.Entities.ActionResult<Domain.Entities.ShelfType> SaveShelfType(Domain.Entities.ShelfType shelfType, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
            {               
                return service.SaveShelfType(shelfType, audit, idSequense);
            }           
        }

		/// <summary>
		/// Elimina un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <returns></returns>
		public Domain.Base.Entities.ActionResult DeleteShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
            {                
                return service.DeleteShelfType(shelfType, audit);
            }            
        }

        /// <summary>
        /// Obtiene un tipo de estante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ShelfType> GetShelfType(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
            {                
                return service.GetShelfType(code, audit);
            }            
        }

        /// <summary>
        /// Obtiene una unidad de medida por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ShelfType> GetShelfTypeById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
            {                
                return service.GetShelfTypeById(id, audit);
            }            
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ShelfType> UpdateStateShelfType(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IShelfTypeAdminService>())
            {                
                return service.UpdateStateShelfType(code, state, audit);
            }            
        }
    }
}
