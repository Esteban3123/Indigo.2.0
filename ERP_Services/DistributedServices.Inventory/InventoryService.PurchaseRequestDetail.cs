
using Application.Inventory.PurchaseRequestDetail;
using DistributedServices.Inventory.Unity;
///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Hector Rodriguez R
/// Created          : 10-04-2019
///
/// Copyright        : (c) . All rights reserved.
/// About            : PBI3499
///***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DistributedServices.Inventory.Contracts;

namespace DistributedServices.Inventory
{
    partial class InventoryService: IInventoryServicePurchaseRequestDetail
    {

        /// <summary>
        /// obtiene una lista de detalles de solicitud de compra de inventario por unidad funcional
        /// </summary>
        /// <param name="idFunctionalUnit"></param>
        /// <returns></returns>
        public List<Domain.Entities.PurchaseRequestDetail> ListPurchaseRequestDetailByFunctionalUnit(int idFunctionalUnit)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestDetailAdminService>())
            {
                return service.ListPurchaseRequestDetailByFunctionalUnit(idFunctionalUnit);
            }
        }

        /// <summary>
        /// Consulta un detalle de solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.PurchaseRequestDetail GetPurchaseRequestDetailById(int id)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestDetailAdminService>())
            {
                return service.GetPurchaseRequestDetailById(id);
            }
        }

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionMessageResult ChangeQuantityPurchaseRequestDetail(int id, int QuantityExported)
        {
            using (var service = Container.Current.Resolve<IPurchaseRequestDetailAdminService>())
            {
                return service.ChangeQuantityPurchaseRequestDetail(id, QuantityExported);
            }
        }

    }
}
