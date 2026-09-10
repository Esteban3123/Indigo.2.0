using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Application.Inventory.ProductRateDetail;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
        /// </summary>
        public ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, int ProductId, DateTime ServiceDate)
        {
            using (var service = Container.Current.Resolve<IProductRateDetailAdminService>())
            {
                return service.GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);
            }
            //return _productRateDetailAdminService.GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);
        }

        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ProductRateDetail>> SetCopyPasteOrImportFileProductRate(List<Domain.Base.Entities.ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste)
        {
            using (var service = Container.Current.Resolve<IProductRateDetailAdminService>())
            {
                return service.SetCopyPasteOrImportFileProductRate(dataImportFile, dataCopyPaste);
            }
            //return _productRateDetailAdminService.SetCopyPasteOrImportFileProductRate(dataImportFile, dataCopyPaste);
        }

        public List<Domain.Entities.ProductRateDetail> GetListProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, List<int> ProductId, DateTime ServiceDate)
        {
            using (var service = Container.Current.Resolve<IProductRateDetailAdminService>())
            {
                return service.GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);
            }
            //return _productRateDetailAdminService.GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);
        }

        public ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageValuePerProduct(int CareGroupId, List<int> PackageIds, DateTime ServiceDate, List<Domain.Entities.ViewPharmaDoseMixingStation> ListviewPharmaDoseMixingStations)
        {
            using (var service = Container.Current.Resolve<IProductRateDetailAdminService>())
            {
                return service.GetPackageValuePerProduct(CareGroupId, PackageIds, ServiceDate, ListviewPharmaDoseMixingStations);
            }
        }

        /// <summary>
        /// Funcion para retornar dirrectamente el valor del producto independientemente si espor tarifa fija o por porcentaje 
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailWithValue(int CareGroupId, int ProductId, DateTime ServiceDate)
        {
            using (var service = Container.Current.Resolve<IProductRateDetailAdminService>())
            {
                return service.GetProductRateDetailWithValue(CareGroupId, ProductId, ServiceDate);
            }
        }
    }
}