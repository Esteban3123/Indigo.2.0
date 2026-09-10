using Application.Inventory.ProductInTransitDetail;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ProductInTransitId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ProductInTransitDetail> GetProductInTransitDetailByProductInTransitId(int ProductInTransitId)
        {
            using (var service = Container.Current.Resolve<IProductInTransitDetailAdminService>())
            {
                return service.GetProductInTransitDetailByProductInTransitId(ProductInTransitId);
            }
        }

    }
}
