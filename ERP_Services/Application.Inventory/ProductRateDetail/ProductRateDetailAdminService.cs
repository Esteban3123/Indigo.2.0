//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 03-02-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.ProductRateDetail
{
    public class ProductRateDetailAdminService : IProductRateDetailAdminService
    {
        #region Builder
        private IProductRateDetailRepository _productRateDetailRepository;
        private IBillingServices _billingServices;
        private IInventoryService _inventoryService;
        private IPackageRepository _packageRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRateDetailAdminService"/> class.
        /// </summary>
        public ProductRateDetailAdminService(IProductRateDetailRepository productRateDetailRepository, IBillingServices billingServices, IInventoryService inventoryService,
                                                IPackageRepository PackageRepository)
        {
            _productRateDetailRepository = productRateDetailRepository;
            _billingServices = billingServices;
            _inventoryService = inventoryService;
            _packageRepository = PackageRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
        /// </summary>
        public ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, int ProductId, DateTime ServiceDate)
        {
            if (CareGroupId == 0)
            {
                throw new ArgumentNullException("CareGroupId");
            }
            if (ProductId == 0)
            {
                throw new ArgumentNullException("ProductId");
            }
            try
            {
                return _billingServices.GetProductRateDetail(CareGroupId, ProductId, ServiceDate);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Funcion que tarifica un paquete en base a los productos que contiene
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="PackageIds"></param>
        /// <param name="ServiceDate"></param>
        /// <param name="DoseQuantity"></param>
        /// <param name="MedicineQuantity"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageValuePerProduct(int CareGroupId, List<int> PackageIds, DateTime ServiceDate, List<ViewPharmaDoseMixingStation> ListviewPharmaDoseMixingStations)
        {
            if (CareGroupId == 0)
            {
                throw new ArgumentNullException("CareGroupId");
            }
            if (PackageIds.Count == 0)
            {
                throw new ArgumentNullException("PackageId");
            }
            if (ListviewPharmaDoseMixingStations.Count == 0) { throw new ArgumentNullException("ListviewPharmaDoseMixingStations"); }
            try
            {
                List<Domain.Entities.ProductRateDetail> ListProductRateDetail = _productRateDetailRepository.GetListProductRateDetailByCareGroupIdPackageServiceDate(CareGroupId, PackageIds, ServiceDate);

                if (ListProductRateDetail is null || ListProductRateDetail.Count == 0)
                {
                    List<Package> Package = _packageRepository.GetByFilter(x => PackageIds.Contains(x.Id), false).ToList();

                    string PackageCode = string.Join(", ",Package.Select(x=> x.Code));

                   return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = $"No se encuentra(n) Parametrizado(s) el o los Paquete(s) : {PackageCode} de central de mezclas en el formulario Tarifa de Producto" };
                }

                List<ProductRateDetailPackage> ListProductRateDetailPackage = new List<ProductRateDetailPackage>();

                StringBuilder sb = new StringBuilder();

                foreach (var item in ListProductRateDetail)
                {
                    foreach(var x in item.ProductRateDetailPackage)
                    {
                        var viewPharmaDoseMixingStations = ListviewPharmaDoseMixingStations.Where(p => p.PackageId == item.PackageId).ToList();
                        x.ProductCodeName = string.Format("{0}-{1}", x.InventoryProduct.Code, x.InventoryProduct.Name);
                        if (x.PackageDetail.MainMedicine!=null && x.PackageDetail.MainMedicine.Value)
                        {
                            x.TotalQuantities = viewPharmaDoseMixingStations.Sum(f=> f.QuantityReceivable);
                        }
                        else
                        {
                            var _count = viewPharmaDoseMixingStations.Count();
                            decimal Result = (_count * (Convert.ToDecimal(x.Quantity.Value) / Convert.ToDecimal(x.DoseNumber.Value)));
                            x.TotalQuantities =  Convert.ToInt32(Math.Ceiling(Result)) ;
                        }
                        Domain.Entities.ProductRateDetail productRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, x.ProductId, ServiceDate);

                        if (productRateDetail is null || productRateDetail.Id==0)
                        {
                            sb.AppendLine($"{x.ProductCodeName}");
                            break;
                        }
                        decimal SalesValue = ReturnProductValue(productRateDetail, ServiceDate, viewPharmaDoseMixingStations.FirstOrDefault().FunctionalUnitId, CareGroupId);
                        x.UnitValue = SalesValue;
                        x.TotalValue = SalesValue * x.TotalQuantities;
                        x.FunctionalUnitId = viewPharmaDoseMixingStations.FirstOrDefault().FunctionalUnitId;
                        x.SurchargeApply = viewPharmaDoseMixingStations.FirstOrDefault().SurchargeApply;
                        x.OrderedHealthProfessionalCode = viewPharmaDoseMixingStations.FirstOrDefault().OrderedHealthProfessionalCode;
                        x.OrderedHealthProfessionalThirdPartyId = viewPharmaDoseMixingStations.FirstOrDefault().OrderedHealthProfessionalThirdPartyId;
                        x.PerformsProfessionalSpecialty = viewPharmaDoseMixingStations.FirstOrDefault().PerformsProfessionalSpecialty;
                        x.WarehouseId = viewPharmaDoseMixingStations.FirstOrDefault().WarehouseId;
                        x.HealthAdministratorId = viewPharmaDoseMixingStations.FirstOrDefault()?.HealthAdministratorId;

                        ListProductRateDetailPackage.Add(x);
                    }
                }

                if (sb.Length > 0)
                {
                     return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = $"El o Los Producto :({sb}) No tiene(n) definida una Tarifa, para el grupo del paciente en la fecha de este proceso" };
                }
                if (ListProductRateDetailPackage is null || ListProductRateDetailPackage.Count==0) { return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = "El proceso de tarificación ha fallado" }; }

                return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = true, Message = "Proceso de tarificación exitoso", ObjectEmbbeded = ListProductRateDetailPackage };

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
              return  new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// funcion para retornar el valor de un producto desde tarificacion (ya sea que este parametrizado por tarifa o tarifa + servicio)
        /// </summary>
        /// <param name="productRateDetail"></param>
        /// <param name="ServiceDate"></param>
        /// <param name="FunctionalUnitId"></param>
        /// <param name="CareGroupId"></param>
        /// <returns></returns>
        public decimal ReturnProductValue(Domain.Entities.ProductRateDetail productRateDetail, DateTime ServiceDate,int? FunctionalUnitId, int CareGroupId)
        {
            decimal SalePrice = 0;

            if (productRateDetail == null || productRateDetail.Id==0 ) { return SalePrice; }

            switch (productRateDetail.RateType)
            {
                case 1:
                    SalePrice = productRateDetail.SalesValue == 0 ? productRateDetail.SalesValueWithSurcharge : productRateDetail.SalesValue;
                    break;
                case 2:
                    if (productRateDetail.PercentageBasedOn == 1)
                    {
                        SalePrice = (productRateDetail.InventoryProduct.ProductCost * (productRateDetail.Percentage.Value / 100)) + (productRateDetail.InventoryProduct.ProductCost);
                    }
                    else if (productRateDetail.PercentageBasedOn == 2)
                    {
                        SalePrice = (productRateDetail.InventoryProduct.FinalProductCost.Value * (productRateDetail.Percentage.Value / 100)) + (productRateDetail.InventoryProduct.FinalProductCost.Value);
                    }
                    else { SalePrice = 0; };
                    break;
            }
            return SalePrice;
        }

        public ActionResult<List<Domain.Entities.ProductRateDetail>> SetCopyPasteOrImportFileProductRate(List<ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste)
        {
            try
            {
                return _inventoryService.SetCopyPasteOrImportFileProductRate(dataImportFile, dataCopyPaste);
            }
            catch (Exception ex)
            {
                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.ProductRateDetail>> { StatusCode = eStatusResult.EXCEPTION , Message = ex.Message };
            }

        }
        #endregion
        
        public List<Domain.Entities.ProductRateDetail> GetListProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, List<int> ProductId, DateTime ServiceDate)
        {
            try
            {
                return _productRateDetailRepository.GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);             
            }
            catch (Exception ex)
            {
                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ProductRateDetail>();
            }
        }

        /// <summary>
        /// Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
        /// retorna el valor del producto mediante las propiedades de SalesValue o SalesValueWithSurcharge, para no implementar logica adicional en presentacion
        /// </summary>
        public ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailWithValue(int CareGroupId, int ProductId, DateTime ServiceDate)
        {
            if (CareGroupId == 0)
            {
                throw new ArgumentNullException("CareGroupId");
            }
            if (ProductId == 0)
            {
                throw new ArgumentNullException("ProductId");
            }
            try
            {
                Domain.Entities.ProductRateDetail Result = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate);

                if (Result is null || Result is null) { return new ActionResult<Domain.Entities.ProductRateDetail> { StateResult = false, Message = "No se ecnontró tarifa del producto" }; }

                if (Result?.LiquidationType == 1 && Result?.RateType == 1) { return new ActionResult<Domain.Entities.ProductRateDetail> { StateResult=true,ObjectEmbbeded= Result, Message = "Se encontró la tarifa del producto exitosamente" }; }

                Result.SalesValue = ReturnProductValue(Result, ServiceDate, null, CareGroupId);

                return new ActionResult<Domain.Entities.ProductRateDetail> { StateResult = true, Message = "Se encontró la tarifa del producto exitosamente", ObjectEmbbeded=Result };

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductRateDetail> { StateResult=false,ObjectEmbbeded=null, StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }
        }

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _billingServices.Dispose();
                    _inventoryService.Dispose();
                }
                _productRateDetailRepository = null;
                _billingServices = null;
                _inventoryService = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
