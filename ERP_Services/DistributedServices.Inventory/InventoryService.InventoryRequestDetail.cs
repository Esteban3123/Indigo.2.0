
using Application.Inventory.InventoryRequestDetail;
using DistributedServices.Inventory.Unity;
///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 25-05-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// obtiene una lista de detalles de solicitud de inventario por unidad funcional o almacen de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryRequestDetail> ListInventoryRequestDetailByTarget(int idFilter, int orderType, int dispatchTo)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDetailAdminService>())
            {
                return service.ListInventoryRequestDetailByTarget(idFilter, orderType, dispatchTo);
            }
            //return _inventoryRequestDetailAdminService.ListInventoryRequestDetailByTarget(idFilter, orderType, dispatchTo);
        }

        /// <summary>
        /// Consulta un detalle de solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryRequestDetail GetInventoryRequestDetailById(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDetailAdminService>())
            {
                return service.GetInventoryRequestDetailById(id);
            }
            //return _inventoryRequestDetailAdminService.GetInventoryRequestDetailById(id);
        }

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionMessageResult ChangeQuantityInventoryRequestDetail(int id, int QuantityExported)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDetailAdminService>())
            {
                return service.ChangeQuantityInventoryRequestDetail(id, QuantityExported);
            }
            //return _inventoryRequestDetailAdminService.ChangeQuantityInventoryRequestDetail(id, QuantityExported);
        }

        /// <summary>
        /// Actualiza la cantidad de la solicitud seleccionada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionMessageResult UpdateQuantityAuthorizedInventoryRequestDetail(int id, int Quantity, string UserCode)
        {
            using (var service = Container.Current.Resolve<IInventoryRequestDetailAdminService>())
            {
                return service.UpdateQuantityAuthorizedInventoryRequestDetail( id, Quantity, UserCode);
            }
        }

    }
}
