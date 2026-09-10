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
        private IPackagePersonalizedDetailRepository _packagePersonalizedDetailRepository;
        private IRequestPackageDetailStatusRepository _requestPackageDetailStatusRepository;
        private IInventoryProductRepository _inventoryProductRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRateDetailAdminService"/> class.
        /// </summary>
        public ProductRateDetailAdminService(IProductRateDetailRepository productRateDetailRepository, IBillingServices billingServices, IInventoryService inventoryService,
                                                IPackageRepository PackageRepository, IPackagePersonalizedDetailRepository packagePersonalizedDetailRepository,
                                                IRequestPackageDetailStatusRepository requestPackageDetailStatusRepository,
                                                IInventoryProductRepository inventoryProductRepository)
        {
            _productRateDetailRepository = productRateDetailRepository;
            _billingServices = billingServices;
            _inventoryService = inventoryService;
            _packageRepository = PackageRepository;
            _packagePersonalizedDetailRepository = packagePersonalizedDetailRepository;
            _requestPackageDetailStatusRepository = requestPackageDetailStatusRepository;
            _inventoryProductRepository = inventoryProductRepository;
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
                List<Package> packages = _packageRepository.GetByFilter(
                    x => PackageIds.Contains(x.Id),
                    false,
                    new[] { "PackageDetail.InventoryProduct", "PackageDetail.ATC", "PackageDetail.InventorySupplie", "UnitDoseType" }).ToList();

                if (packages is null || packages.Count == 0)
                {
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = "No se encontró información del paquete de central de mezclas" };
                }

                var packageIdsNotFound = PackageIds
                    .Distinct()
                    .Where(packageId => !packages.Any(package => package.Id == packageId))
                    .ToList();
                if (packageIdsNotFound.Any())
                {
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>>
                    {
                        StateResult = false,
                        Message = $"No se encontró información para el/los paquete(s): {string.Join(", ", packageIdsNotFound)}"
                    };
                }

                List<ProductRateDetailPackage> ListProductRateDetailPackage = new List<ProductRateDetailPackage>();

                StringBuilder sb = new StringBuilder();
                StringBuilder rawMaterialSb = new StringBuilder();

                foreach (var package in packages)
                {
                    int rawMaterialLengthBefore = rawMaterialSb.Length;
                    int tariffLengthBefore = sb.Length;
                    int expectedPackageComponentCount = 0;
                    var packageComponents = new List<ProductRateDetailPackage>();

                    // Datos de dispensacion y paquete personalizado: se resuelven una sola vez por paquete
                    var viewPharmaDoseMixingStations = ListviewPharmaDoseMixingStations.Where(p => p.PackageId == package.Id).ToList();
                    var firstStation = viewPharmaDoseMixingStations.FirstOrDefault();
                    if (firstStation == null)
                    {
                        rawMaterialSb.AppendLine($"No se encontró información de dispensación para el paquete ({package.Code} - {package.Name}).");
                        continue;
                    }

                    var packagePersonalizedId = firstStation?.PackagePersonalizedId;
                    int? packagePersonalizedProductId = null;
                    var packageRateDetail = BuildPackageRateDetail(package);

                    List<PackagePersonalizedDetail> personalizedDetails = null;
                    bool isAntibiotics = false;
                    bool isParenteralNutrition = package.UnitDoseType?.MSClass == (int)EUnitDoseTypeClass.ParenteralNutrition;
                    bool isCytostatic = false;
                    if (packagePersonalizedId.HasValue)
                    {
                        personalizedDetails = _packagePersonalizedDetailRepository.GetPackageDetailListByPackageId(packagePersonalizedId.Value);
                        packagePersonalizedProductId = personalizedDetails?.FirstOrDefault()?.PackagePersonalized?.ProductId;
                        var personalizedMsClass = personalizedDetails?.FirstOrDefault()?.PackagePersonalized?.UnitDoseType?.MSClass;
                        isAntibiotics = personalizedMsClass == (int)EUnitDoseTypeClass.Antibiotics;
                        isParenteralNutrition = personalizedMsClass == (int)EUnitDoseTypeClass.ParenteralNutrition;
                        isCytostatic = personalizedMsClass == (int)EUnitDoseTypeClass.Cytostatic;
                    }

                    if (personalizedDetails != null)
                    {
                        // --- Componentes AtcId: el producto se resuelve desde materia prima validada.
                        // La cantidad del paquete personalizado corresponde a dosis; para liquidar unidades
                        // del producto se convierte por el divisor del ATC cuando esta parametrizado.
                        // En citostaticos, los medicamentos complementarios con ATC se procesan en esta misma agrupacion.
                        var personalizedDetailsWithoutComponent = personalizedDetails
                            .Where(pd => !pd.AtcId.HasValue && !pd.SupplieId.HasValue)
                            .ToList();
                        if (personalizedDetailsWithoutComponent.Any())
                        {
                            rawMaterialSb.AppendLine($"El paquete personalizado ({package.Code} - {package.Name}) tiene detalle(s) sin ATC ni insumo parametrizado.");
                            continue;
                        }

                        var groupedByAtc = personalizedDetails
                            .Where(pd => pd.AtcId.HasValue)
                            .GroupBy(pd => pd.AtcId.Value)
                            .Select(g => new {
                                AtcId    = g.Key,
                                Quantity = g.Where(pd => pd.Quantity.HasValue).Sum(pd => pd.Quantity.Value),
                                Detail   = g.FirstOrDefault()
                            })
                            .ToList();
                        expectedPackageComponentCount += groupedByAtc.Count;

                        foreach (var pd in groupedByAtc)
                        {
                            var product = ResolveRawMaterialInventoryProduct(pd.Detail, firstStation, packagePersonalizedProductId);
                            if (product == null || product.Id == 0)
                            {
                                rawMaterialSb.AppendLine($"No se encontró producto validado para el medicamento ({GetAtcCodeName(pd.Detail)}) del lote {firstStation?.BatchCode}");
                                continue;
                            }

                            var x = BuildPackageRateDetailPackage(packageRateDetail, pd.Detail, product);

                            x.ProductCodeName = $"{x.InventoryProduct.Code}-{x.InventoryProduct.Name}";
                            x.Quantity = Convert.ToInt32(Math.Ceiling(pd.Quantity));
                            if (x.PackageDetail != null)
                            {
                                x.PackageDetail.Quantity = pd.Quantity;
                            }

                            int count = viewPharmaDoseMixingStations.Count();
                            decimal divisor = GetAtcQuantityDivisor(pd.Detail?.ATC, isParenteralNutrition);
                            x.TotalQuantities = divisor > 0
                                ? Convert.ToInt32(Math.Ceiling((pd.Quantity * count) / divisor))
                                : isAntibiotics
                                    ? Convert.ToInt32(Math.Ceiling(count * (Convert.ToDecimal(x.Quantity.Value) / Convert.ToDecimal(x.DoseNumber.Value))))
                                    : viewPharmaDoseMixingStations.Sum(f => f.QuantityReceivable);

                            ApplyTariffToPackageDetail(x, CareGroupId, ServiceDate, firstStation, sb, packageComponents);
                        }

                        // --- Componentes SupplieId: cantidad directa desde PackagePersonalizedDetail.Quantity ---
                        var groupedBySupply = personalizedDetails
                            .Where(pd => !pd.AtcId.HasValue && pd.SupplieId.HasValue)
                            .GroupBy(pd => pd.SupplieId.Value)
                            .Select(g => new {
                                SupplieId = g.Key,
                                Quantity  = g.Where(pd => pd.Quantity.HasValue).Sum(pd => pd.Quantity.Value),
                                Detail    = g.FirstOrDefault()
                            })
                            .ToList();
                        expectedPackageComponentCount += groupedBySupply.Count;

                        foreach (var pd in groupedBySupply)
                        {
                            var product = ResolveSupplyInventoryProduct(pd.Detail);
                            if (product == null || product.Id == 0)
                            {
                                rawMaterialSb.AppendLine($"No se encontró producto para el insumo ({pd.SupplieId}) del paquete personalizado.");
                                continue;
                            }

                            var x = BuildPackageRateDetailPackage(packageRateDetail, pd.Detail, product);

                            x.ProductCodeName = $"{x.InventoryProduct.Code}-{x.InventoryProduct.Name}";
                            x.TotalQuantities = Convert.ToInt32(Math.Ceiling(pd.Quantity));

                            ApplyTariffToPackageDetail(x, CareGroupId, ServiceDate, firstStation, sb, packageComponents);
                        }
                    }
                    else if (firstStation?.Source == 4)
                    {
                        // Dosis estandar (Source=4): fuente de verdad = PackageDetail para todos los componentes,
                        // incluido el medicamento principal (MainMedicine=true).
                        // FormulationType=2 (Volumen) -> divisor=ATC.Volume
                        // FormulationType=1 (Peso) o 3 (Peso-Volumen) -> divisor=ATC.Weight
                        // Componentes SupplieId -> cantidad directa desde PackageDetail.Quantity
                        expectedPackageComponentCount = package.PackageDetail.Count(pd => pd.AtcId.HasValue || pd.SupplieId.HasValue);
                        var packageDetailsWithoutComponent = package.PackageDetail
                            .Where(pd => !pd.AtcId.HasValue && !pd.SupplieId.HasValue)
                            .ToList();
                        if (packageDetailsWithoutComponent.Any())
                        {
                            rawMaterialSb.AppendLine($"El paquete ({package.Code} - {package.Name}) tiene detalle(s) sin ATC ni insumo parametrizado.");
                        }

                        foreach (var packageDetail in package.PackageDetail)
                        {
                            if (!packageDetail.AtcId.HasValue && !packageDetail.SupplieId.HasValue) continue;

                            var product = packageDetail.AtcId.HasValue
                                ? ResolveRawMaterialInventoryProduct(packageDetail, firstStation, package.ProductId)
                                : ResolveSupplyInventoryProduct(packageDetail);
                            if (product == null || product.Id == 0)
                            {
                                rawMaterialSb.AppendLine(packageDetail.AtcId.HasValue
                                    ? $"No se encontró producto validado para el medicamento ({GetAtcCodeName(packageDetail)}) del lote {firstStation?.BatchCode}"
                                    : $"No se encontró producto para el insumo del paquete ({package.Code} - {package.Name}).");
                                continue;
                            }

                            var x = BuildPackageRateDetailPackage(packageRateDetail, packageDetail, product);
                            x.ProductCodeName = $"{x.InventoryProduct.Code}-{x.InventoryProduct.Name}";

                            x.TotalQuantities = GetStandardPackageComponentQuantity(
                                packageDetail,
                                viewPharmaDoseMixingStations.Count(),
                                viewPharmaDoseMixingStations.Sum(f => f.QuantityReceivable),
                                isParenteralNutrition);

                            ApplyTariffToPackageDetail(x, CareGroupId, ServiceDate, firstStation, sb, packageComponents);
                        }
                    }
                    else
                    {
                        // Paquete estandar sin Source=4: conserva la cantidad original, pero los medicamentos
                        // se liquidan con la materia prima validada para el lote de la preparacion.
                        expectedPackageComponentCount = package.PackageDetail.Count(pd => pd.AtcId.HasValue || pd.SupplieId.HasValue);
                        var packageDetailsWithoutComponent = package.PackageDetail
                            .Where(pd => !pd.AtcId.HasValue && !pd.SupplieId.HasValue)
                            .ToList();
                        if (packageDetailsWithoutComponent.Any())
                        {
                            rawMaterialSb.AppendLine($"El paquete ({package.Code} - {package.Name}) tiene detalle(s) sin ATC ni insumo parametrizado.");
                        }

                        foreach (var packageDetail in package.PackageDetail)
                        {
                            if (!packageDetail.AtcId.HasValue && !packageDetail.SupplieId.HasValue) continue;

                            var product = packageDetail.AtcId.HasValue
                                ? ResolveRawMaterialInventoryProduct(packageDetail, firstStation, package.ProductId)
                                : ResolveSupplyInventoryProduct(packageDetail);
                            if (product == null || product.Id == 0)
                            {
                                rawMaterialSb.AppendLine(packageDetail.AtcId.HasValue
                                    ? $"No se encontró producto validado para el medicamento ({GetAtcCodeName(packageDetail)}) del lote {firstStation?.BatchCode}"
                                    : $"No se encontró producto para el insumo del paquete ({package.Code} - {package.Name}).");
                                continue;
                            }

                            var x = BuildPackageRateDetailPackage(packageRateDetail, packageDetail, product);
                            x.ProductCodeName = $"{x.InventoryProduct.Code}-{x.InventoryProduct.Name}";

                            if (x.PackageDetail?.MainMedicine == true)
                            {
                                x.TotalQuantities = viewPharmaDoseMixingStations.Sum(f => f.QuantityReceivable);
                            }
                            else
                            {
                                int count = viewPharmaDoseMixingStations.Count();
                                x.TotalQuantities = Convert.ToInt32(Math.Ceiling(count * (Convert.ToDecimal(x.Quantity.Value) / Convert.ToDecimal(x.DoseNumber.Value))));
                            }

                            ApplyTariffToPackageDetail(x, CareGroupId, ServiceDate, firstStation, sb, packageComponents);
                        }
                    }

                    bool packageHasErrors = rawMaterialSb.Length > rawMaterialLengthBefore || sb.Length > tariffLengthBefore;
                    if (!packageHasErrors && expectedPackageComponentCount == 0)
                    {
                        rawMaterialSb.AppendLine($"No se encontraron componentes parametrizados para el paquete ({package.Code} - {package.Name}).");
                        packageHasErrors = true;
                    }

                    if (!packageHasErrors && packageComponents.Count != expectedPackageComponentCount)
                    {
                        rawMaterialSb.AppendLine($"No se obtuvieron todos los componentes del paquete ({package.Code} - {package.Name}). Esperados: {expectedPackageComponentCount}, obtenidos: {packageComponents.Count}.");
                        packageHasErrors = true;
                    }

                    if (!packageHasErrors)
                    {
                        ListProductRateDetailPackage.AddRange(packageComponents);
                    }
                }

                if (rawMaterialSb.Length > 0)
                {
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = rawMaterialSb.ToString() };
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
        /// Obtiene el texto identificador del ATC asociado a un detalle de paquete personalizado.
        /// </summary>
        /// <param name="personalizedDetail">Detalle de paquete personalizado que contiene el ATC.</param>
        /// <returns>Codigo y nombre del ATC, o el identificador del ATC cuando la entidad no esta cargada.</returns>
        private string GetAtcCodeName(PackagePersonalizedDetail personalizedDetail)
        {
            if (personalizedDetail?.ATC != null)
                return $"{personalizedDetail.ATC.Code}-{personalizedDetail.ATC.Name}";

            return personalizedDetail?.AtcId?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Obtiene el texto identificador del ATC asociado a un detalle de paquete estandar.
        /// </summary>
        /// <param name="packageDetail">Detalle de paquete estandar que contiene el ATC.</param>
        /// <returns>Codigo y nombre del ATC, o el identificador del ATC cuando la entidad no esta cargada.</returns>
        private string GetAtcCodeName(PackageDetail packageDetail)
        {
            if (packageDetail?.ATC != null)
                return $"{packageDetail.ATC.Code}-{packageDetail.ATC.Name}";

            return packageDetail?.AtcId?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Construye una instancia base de tarifa de paquete para asociar los componentes liquidados.
        /// </summary>
        /// <param name="package">Paquete de central de mezclas que origina los componentes.</param>
        /// <returns>Detalle de tarifa base con la referencia al paquete.</returns>
        private Domain.Entities.ProductRateDetail BuildPackageRateDetail(Package package)
        {
            return new Domain.Entities.ProductRateDetail
            {
                Id = 0,
                PackageId = package.Id,
                Package = package
            };
        }

        /// <summary>
        /// Construye el componente liquidable a partir de un detalle de paquete estandar.
        /// </summary>
        /// <param name="packageRateDetail">Tarifa base del paquete que agrupa los componentes.</param>
        /// <param name="packageDetail">Detalle parametrizado en el paquete estandar.</param>
        /// <param name="inventoryProduct">Producto de inventario que se debe liquidar para el componente.</param>
        /// <returns>Componente del paquete con producto, cantidad base, tipo de item y marcadores de preparacion.</returns>
        private ProductRateDetailPackage BuildPackageRateDetailPackage(
            Domain.Entities.ProductRateDetail packageRateDetail,
            PackageDetail packageDetail,
            Domain.Entities.InventoryProduct inventoryProduct)
        {
            return new ProductRateDetailPackage
            {
                ProductRateDetailId = packageRateDetail.Id,
                PackageDetailId = packageDetail.Id,
                ProductId = inventoryProduct.Id,
                Quantity = packageDetail.Quantity.HasValue
                    ? Convert.ToInt32(Math.Ceiling(packageDetail.Quantity.Value))
                    : 0,
                DoseNumber = 1,
                InventoryProduct = inventoryProduct,
                ProductRateDetail = packageRateDetail,
                PackageDetail = packageDetail,
                ItemType = GetPackageDetailItemType(packageDetail),
                ItemId = packageDetail.AtcId ?? packageDetail.SupplieId,
                MainMedicine = packageDetail.MainMedicine.GetValueOrDefault(false),
                Thinner = packageDetail.Thinner,
                Vehicle = packageDetail.Vehicle
            };
        }

        /// <summary>
        /// Construye el componente liquidable a partir de un detalle de paquete personalizado.
        /// </summary>
        /// <param name="packageRateDetail">Tarifa base del paquete que agrupa los componentes.</param>
        /// <param name="personalizedDetail">Detalle parametrizado en el paquete personalizado.</param>
        /// <param name="inventoryProduct">Producto de inventario que se debe liquidar para el componente.</param>
        /// <returns>Componente del paquete con un PackageDetail equivalente para conservar el contrato de liquidacion.</returns>
        private ProductRateDetailPackage BuildPackageRateDetailPackage(
            Domain.Entities.ProductRateDetail packageRateDetail,
            PackagePersonalizedDetail personalizedDetail,
            Domain.Entities.InventoryProduct inventoryProduct)
        {
            return new ProductRateDetailPackage
            {
                ProductRateDetailId = packageRateDetail.Id,
                PackageDetailId = 0,
                ProductId = inventoryProduct.Id,
                Quantity = personalizedDetail.Quantity.HasValue
                    ? Convert.ToInt32(Math.Ceiling(personalizedDetail.Quantity.Value))
                    : 0,
                DoseNumber = 1,
                InventoryProduct = inventoryProduct,
                ProductRateDetail = packageRateDetail,
                PackageDetail = new PackageDetail
                {
                    PackageId = packageRateDetail.PackageId.GetValueOrDefault(),
                    Quantity = personalizedDetail.Quantity,
                    MeasurementUnitId = personalizedDetail.MeasurementUnitId,
                    AtcId = personalizedDetail.AtcId,
                    ATC = personalizedDetail.ATC,
                    SupplieId = personalizedDetail.SupplieId,
                    ComponentType = personalizedDetail.ComponentType,
                    MainMedicine = personalizedDetail.MainMedicine,
                    ComplementaryMedicine = personalizedDetail.ComplementaryMedicine,
                    Thinner = personalizedDetail.Thinner,
                    Vehicle = personalizedDetail.Vehicle
                },
                ItemType = GetPackagePersonalizedDetailItemType(personalizedDetail),
                ItemId = personalizedDetail.AtcId ?? personalizedDetail.SupplieId,
                MainMedicine = personalizedDetail.MainMedicine.GetValueOrDefault(false),
                Thinner = personalizedDetail.Thinner,
                Vehicle = personalizedDetail.Vehicle
            };
        }

        /// <summary>
        /// Determina el tipo de item de un detalle de paquete estandar segun el origen del componente.
        /// </summary>
        /// <param name="packageDetail">Detalle de paquete estandar a clasificar.</param>
        /// <returns>1 para ATC, 2 para insumo y 0 cuando no se puede clasificar.</returns>
        private byte GetPackageDetailItemType(PackageDetail packageDetail)
        {
            if (packageDetail.AtcId.HasValue) return 1;
            if (packageDetail.SupplieId.HasValue) return 2;
            return 0;
        }

        /// <summary>
        /// Determina el tipo de item de un detalle de paquete personalizado segun el origen del componente.
        /// </summary>
        /// <param name="personalizedDetail">Detalle de paquete personalizado a clasificar.</param>
        /// <returns>1 para ATC, 2 para insumo y 0 cuando no se puede clasificar.</returns>
        private byte GetPackagePersonalizedDetailItemType(PackagePersonalizedDetail personalizedDetail)
        {
            if (personalizedDetail.AtcId.HasValue) return 1;
            if (personalizedDetail.SupplieId.HasValue) return 2;
            return 0;
        }

        /// <summary>
        /// Resuelve el producto de inventario asociado a un insumo de paquete estandar.
        /// </summary>
        /// <param name="packageDetail">Detalle del paquete que referencia un insumo.</param>
        /// <returns>Producto de inventario activo asociado al insumo, o null si no se encuentra.</returns>
        private Domain.Entities.InventoryProduct ResolveSupplyInventoryProduct(PackageDetail packageDetail)
        {
            if (packageDetail == null) return null;

            if (packageDetail.SupplieId.HasValue)
                return _inventoryProductRepository.Query(p => p.SupplieId == packageDetail.SupplieId.Value && p.Status, false).FirstOrDefault();

            return null;
        }

        /// <summary>
        /// Resuelve el producto de inventario asociado a un insumo de paquete personalizado.
        /// </summary>
        /// <param name="personalizedDetail">Detalle del paquete personalizado que referencia un insumo.</param>
        /// <returns>Producto de inventario activo asociado al insumo, o null si no se encuentra.</returns>
        private Domain.Entities.InventoryProduct ResolveSupplyInventoryProduct(PackagePersonalizedDetail personalizedDetail)
        {
            if (personalizedDetail == null) return null;

            if (personalizedDetail.SupplieId.HasValue)
                return _inventoryProductRepository.Query(p => p.SupplieId == personalizedDetail.SupplieId.Value && p.Status, false).FirstOrDefault();

            return null;
        }

        /// <summary>
        /// Resuelve la materia prima validada para un componente ATC de paquete personalizado.
        /// </summary>
        /// <param name="personalizedDetail">Detalle personalizado que contiene el ATC a liquidar.</param>
        /// <param name="firstStation">Registro de dispensacion usado para obtener el lote de la preparacion.</param>
        /// <param name="packagePersonalizedProductId">Producto terminado del paquete personalizado.</param>
        /// <returns>Producto de inventario validado para el ATC y lote, o null si no existe validacion.</returns>
        private Domain.Entities.InventoryProduct ResolveRawMaterialInventoryProduct(
            PackagePersonalizedDetail personalizedDetail,
            ViewPharmaDoseMixingStation firstStation,
            int? packagePersonalizedProductId)
        {
            return ResolveRawMaterialInventoryProduct(personalizedDetail?.AtcId, firstStation?.BatchCode, packagePersonalizedProductId);
        }

        /// <summary>
        /// Resuelve la materia prima validada para un componente ATC de paquete estandar usando la estacion de mezcla.
        /// </summary>
        /// <param name="packageDetail">Detalle estandar que contiene el ATC a liquidar.</param>
        /// <param name="firstStation">Registro de dispensacion usado para obtener el lote de la preparacion.</param>
        /// <param name="packageProductId">Producto terminado del paquete estandar.</param>
        /// <returns>Producto de inventario validado para el ATC y lote, o null si no existe validacion.</returns>
        private Domain.Entities.InventoryProduct ResolveRawMaterialInventoryProduct(
            PackageDetail packageDetail,
            ViewPharmaDoseMixingStation firstStation,
            int? packageProductId)
        {
            return ResolveRawMaterialInventoryProduct(packageDetail?.AtcId, firstStation?.BatchCode, packageProductId);
        }

        /// <summary>
        /// Resuelve la materia prima validada para un componente ATC de paquete estandar usando un lote explicito.
        /// </summary>
        /// <param name="packageDetail">Detalle estandar que contiene el ATC a liquidar.</param>
        /// <param name="batchCode">Lote del producto terminado dispensado.</param>
        /// <param name="packageProductId">Producto terminado del paquete estandar.</param>
        /// <returns>Producto de inventario validado para el ATC y lote, o null si no existe validacion.</returns>
        private Domain.Entities.InventoryProduct ResolveRawMaterialInventoryProduct(
            PackageDetail packageDetail,
            string batchCode,
            int? packageProductId)
        {
            return ResolveRawMaterialInventoryProduct(packageDetail?.AtcId, batchCode, packageProductId);
        }

        /// <summary>
        /// Busca el producto de inventario validado como materia prima para una preparacion, cruzando lote, producto terminado y ATC.
        /// </summary>
        /// <param name="atcId">ATC del componente a liquidar.</param>
        /// <param name="batchCode">Lote del producto terminado dispensado o preparado.</param>
        /// <param name="packageProductId">Producto terminado del paquete que origina la preparacion.</param>
        /// <returns>Producto de inventario validado para la preparacion, o null cuando faltan datos o no hay validacion.</returns>
        private Domain.Entities.InventoryProduct ResolveRawMaterialInventoryProduct(
            int? atcId,
            string batchCode,
            int? packageProductId)
        {
            if (atcId.HasValue != true
                || string.IsNullOrWhiteSpace(batchCode)
                || !packageProductId.HasValue
                || packageProductId.Value == 0)
            {
                return null;
            }

            var rawMaterial = _requestPackageDetailStatusRepository.GetComplementaryRawMaterialByBatchProductAndAtc(
                batchCode,
                packageProductId.Value,
                atcId.Value);

            if (rawMaterial == null || rawMaterial.ProductValidationId == 0)
            {
                return null;
            }

            return rawMaterial.InventoryProduct ?? _inventoryProductRepository.GetInventoryProductById(rawMaterial.ProductValidationId, false);
        }

        /// <summary>
        /// Devuelve el divisor para calcular unidades a facturar segun el FormulationType del ATC.
        /// Nutrición parenteral siempre usa ATC.Volume como divisor.
        /// FormulationType = 2 (Volumen)       -> ATC.Volume
        /// FormulationType = 1 (Peso)          -> ATC.Weight
        /// FormulationType = 3 (Peso-Volumen)  -> ATC.Weight
        /// Retorna 0 si el campo correspondiente no esta parametrizado (el llamador debe aplicar fallback).
        /// </summary>
        /// <param name="atc">ATC usado para determinar el divisor de cantidad.</param>
        /// <param name="isParenteralNutrition">Indica si el paquete corresponde a nutricion parenteral.</param>
        /// <returns>Valor divisor para la cantidad del componente, o 0 si no aplica.</returns>
        private decimal GetAtcQuantityDivisor(Domain.Entities.ATC atc, bool isParenteralNutrition = false)
        {
            if (atc == null) return 0m;
            if (isParenteralNutrition || atc.FormulationType == 2) // NPT y Volumen siempre usan Volume
                return atc.Volume.HasValue && atc.Volume.Value > 0 ? atc.Volume.Value : 0m;
            // Peso (1) o Peso-Volumen (3)
            return atc.Weight.HasValue && atc.Weight.Value > 0 ? atc.Weight.Value : 0m;
        }

        /// <summary>
        /// Calcula la cantidad total a liquidar para un componente de paquete estandar conservando la regla previa de Source=4.
        /// </summary>
        /// <param name="packageDetail">Detalle de paquete que contiene la cantidad base del componente.</param>
        /// <param name="quantityMultiplier">Cantidad de preparaciones o unidades dispensadas usada para medicamentos con divisor.</param>
        /// <param name="quantityFallback">Cantidad a usar cuando no hay cantidad de paquete ni divisor aplicable.</param>
        /// <param name="isParenteralNutrition">Indica si el paquete corresponde a nutricion parenteral.</param>
        /// <returns>Cantidad calculada para tarifar el componente.</returns>
        private int GetStandardPackageComponentQuantity(PackageDetail packageDetail, int quantityMultiplier, int quantityFallback, bool isParenteralNutrition = false)
        {
            if (packageDetail?.AtcId.HasValue == true
                && packageDetail.ATC != null
                && packageDetail.Quantity.HasValue
                && packageDetail.Quantity.Value > 0)
            {
                decimal divisor = GetAtcQuantityDivisor(packageDetail.ATC, isParenteralNutrition);
                if (divisor > 0)
                {
                    return Convert.ToInt32(Math.Ceiling((packageDetail.Quantity.Value * quantityMultiplier) / divisor));
                }

                return quantityFallback;
            }

            return packageDetail?.Quantity.HasValue == true
                ? Convert.ToInt32(Math.Ceiling(packageDetail.Quantity.Value))
                : quantityFallback;
        }

        /// <summary>
        /// Busca la tarifa del producto, asigna valores y datos de estacion al detalle de paquete y lo agrega a la lista.
        /// Si el producto no tiene tarifa definida, registra el faltante y no agrega el componente al resultado.
        /// </summary>
        private void ApplyTariffToPackageDetail(
            ProductRateDetailPackage x,
            int careGroupId,
            DateTime serviceDate,
            ViewPharmaDoseMixingStation firstStation,
            StringBuilder sb,
            List<ProductRateDetailPackage> result)
        {
            Domain.Entities.ProductRateDetail productRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(careGroupId, x.ProductId, serviceDate);

            if (productRateDetail is null || productRateDetail.Id == 0)
            {
                sb.AppendLine($"{x.ProductCodeName}");
                return;
            }

            decimal salesValue = ReturnProductValue(productRateDetail, serviceDate, firstStation.FunctionalUnitId, careGroupId);
            x.UnitValue                                = salesValue;
            x.TotalValue                               = salesValue * x.TotalQuantities;
            x.DispensingDate                           = firstStation.DispensingDate;
            x.FunctionalUnitId                         = firstStation.FunctionalUnitId;
            x.SurchargeApply                           = firstStation.SurchargeApply;
            x.OrderedHealthProfessionalCode            = firstStation.OrderedHealthProfessionalCode;
            x.OrderedHealthProfessionalThirdPartyId    = firstStation.OrderedHealthProfessionalThirdPartyId;
            x.PerformsProfessionalSpecialty            = firstStation.PerformsProfessionalSpecialty;
            x.WarehouseId                              = firstStation.WarehouseId;
            x.HealthAdministratorId                    = firstStation.HealthAdministratorId;
            result.Add(x);
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

        /// <summary>
        /// Calcula el precio de venta de un ítem de producción (producto terminado de central de mezclas)
        /// sumando las tarifas de cada componente real usado como materia prima para el lote.
        /// Flujo: ProductId → PackageId → PackageDetail → materia prima validada por lote/ATC → suma tarifas individuales
        /// </summary>
        public ActionResult<decimal> GetPackageSalePriceByProductId(int CareGroupId, int ProductId, DateTime ServiceDate, string BatchCode)
        {
            if (CareGroupId == 0) throw new ArgumentNullException("CareGroupId");
            if (ProductId == 0) throw new ArgumentNullException("ProductId");
            if (string.IsNullOrWhiteSpace(BatchCode)) throw new ArgumentNullException("BatchCode");

            try
            {
                var packageId = _productRateDetailRepository.GetPackageIdByProductId(ProductId);
                if (packageId == null || packageId == 0)
                    return new ActionResult<decimal> { StateResult = false, Message = "No se encontró un paquete asociado al producto terminado en el historial de producción." };

                var package = _packageRepository.GetByFilter(
                    x => x.Id == packageId.Value,
                    false,
                    new[] { "PackageDetail.InventoryProduct", "PackageDetail.ATC", "PackageDetail.InventorySupplie", "UnitDoseType" }).FirstOrDefault();

                if (package == null || package.Id == 0)
                    return new ActionResult<decimal> { StateResult = false, Message = "No se encontró información del paquete asociado al producto terminado." };

                if (package.PackageDetail == null || !package.PackageDetail.Any())
                    return new ActionResult<decimal> { StateResult = false, Message = "El paquete asociado al producto terminado no tiene componentes parametrizados." };

                decimal totalSalePrice = 0;
                var missingProductSb = new StringBuilder();
                var tariffSb = new StringBuilder();
                int expectedComponentCount = package.PackageDetail.Count(pd => pd.AtcId.HasValue || pd.SupplieId.HasValue);
                int pricedComponentCount = 0;
                int? packageProductId = package.ProductId ?? ProductId;

                foreach (var packageDetail in package.PackageDetail)
                {
                    if (!packageDetail.AtcId.HasValue && !packageDetail.SupplieId.HasValue)
                    {
                        missingProductSb.AppendLine($"Detalle del paquete ({package.Code} - {package.Name}) sin ATC ni insumo parametrizado");
                        continue;
                    }

                    var inventoryProduct = packageDetail.AtcId.HasValue
                        ? ResolveRawMaterialInventoryProduct(packageDetail, BatchCode, packageProductId)
                        : ResolveSupplyInventoryProduct(packageDetail);

                    if (inventoryProduct == null || inventoryProduct.Id == 0)
                    {
                        missingProductSb.AppendLine(packageDetail.AtcId.HasValue
                            ? $"Medicamento ({GetAtcCodeName(packageDetail)}) del lote {BatchCode}"
                            : $"Insumo del paquete ({package.Code} - {package.Name})");
                        continue;
                    }

                    var componentRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(
                        CareGroupId, inventoryProduct.Id, ServiceDate);

                    if (componentRateDetail == null || componentRateDetail.Id == 0)
                    {
                        tariffSb.AppendLine($"{inventoryProduct.Code} - {inventoryProduct.Name}");
                        continue;
                    }

                    decimal componentValue = ReturnProductValue(componentRateDetail, ServiceDate, null, CareGroupId);
                    totalSalePrice += componentValue;
                    pricedComponentCount++;
                }

                if (missingProductSb.Length > 0)
                    return new ActionResult<decimal> { StateResult = false, Message = $"No se encontró producto para el o los componente(s): ({missingProductSb.ToString().Trim()})." };

                if (tariffSb.Length > 0)
                    return new ActionResult<decimal> { StateResult = false, Message = $"El o Los Producto(s): ({tariffSb.ToString().Trim()}) no tienen definida una Tarifa para el grupo de atención en la fecha indicada." };

                if (expectedComponentCount == 0 || pricedComponentCount != expectedComponentCount)
                    return new ActionResult<decimal> { StateResult = false, Message = $"No se calcularon todos los componentes del paquete asociado al producto terminado. Esperados: {expectedComponentCount}, calculados: {pricedComponentCount}." };

                return new ActionResult<decimal> { StateResult = true, ObjectEmbbeded = totalSalePrice, Message = "Precio calculado exitosamente." };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<decimal> { StateResult = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retorna los componentes tarifados del paquete estandar asociado a un producto terminado de dosis estandar.
        /// Uso exclusivo del flujo de dispensacion farmaceutica para generar orden de servicio con DatasourceType = 11.
        /// No debe usarse para paquetes personalizados ni para el flujo de control de cuentas hospitalario.
        /// Flujo: ProductId → PackageId → PackageDetail → materia prima validada por lote/ATC → tarifa individual por componente.
        /// </summary>
        /// <param name="CareGroupId">Id del grupo de atencion.</param>
        /// <param name="ProductId">Id del producto terminado dispensado.</param>
        /// <param name="DispensedQuantity">Cantidad dispensada del producto terminado para el lote indicado.</param>
        /// <param name="ServiceDate">Fecha del servicio usada para buscar la tarifa individual.</param>
        /// <param name="BatchCode">Lote del producto terminado dispensado.</param>
        /// <returns>Componentes del paquete estandar con producto, cantidad y valor calculados.</returns>
        public ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageRateDetailListByProductId(int CareGroupId, int ProductId, int DispensedQuantity, DateTime ServiceDate, string BatchCode)
        {
            if (CareGroupId == 0) throw new ArgumentNullException("CareGroupId");
            if (ProductId == 0) throw new ArgumentNullException("ProductId");
            if (string.IsNullOrWhiteSpace(BatchCode)) throw new ArgumentNullException("BatchCode");

            try
            {
                var packageId = _productRateDetailRepository.GetPackageIdByProductId(ProductId);
                if (packageId == null || packageId == 0)
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = "No se encontró un paquete asociado al producto terminado." };

                var package = _packageRepository.GetByFilter(
                    x => x.Id == packageId.Value,
                    false,
                    new[] { "PackageDetail.InventoryProduct", "PackageDetail.ATC", "PackageDetail.InventorySupplie", "UnitDoseType" }).FirstOrDefault();

                if (package == null || package.Id == 0)
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = "No se encontró información del paquete asociado al producto terminado." };

                if (package.PackageDetail == null || !package.PackageDetail.Any())
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = "El paquete asociado al producto terminado no tiene componentes parametrizados." };

                var result = new List<Domain.Entities.ProductRateDetailPackage>();
                var missingProductSb = new StringBuilder();
                var tariffSb = new StringBuilder();
                var packageRateDetail = BuildPackageRateDetail(package);
                int? packageProductId = package.ProductId ?? ProductId;
                bool isParenteralNutrition = package.UnitDoseType?.MSClass == (int)EUnitDoseTypeClass.ParenteralNutrition;

                foreach (var packageDetail in package.PackageDetail)
                {
                    var inventoryProduct = packageDetail.AtcId.HasValue
                        ? ResolveRawMaterialInventoryProduct(packageDetail, BatchCode, packageProductId)
                        : ResolveSupplyInventoryProduct(packageDetail);
                    if (inventoryProduct == null || inventoryProduct.Id == 0)
                    {
                        missingProductSb.AppendLine(packageDetail.AtcId.HasValue
                            ? $"Medicamento ({GetAtcCodeName(packageDetail)}) del lote {BatchCode}"
                            : $"Insumo del paquete ({package.Code} - {package.Name})");
                        continue;
                    }

                    var component = BuildPackageRateDetailPackage(packageRateDetail, packageDetail, inventoryProduct);

                    var componentRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(
                        CareGroupId, component.ProductId, ServiceDate);

                    if (componentRateDetail == null || componentRateDetail.Id == 0)
                    {
                        tariffSb.AppendLine($"{inventoryProduct.Code} - {inventoryProduct.Name}");
                        continue;
                    }

                    decimal unitValue = ReturnProductValue(componentRateDetail, ServiceDate, null, CareGroupId);
                    component.TotalQuantities = GetStandardPackageComponentQuantity(
                        packageDetail,
                        DispensedQuantity,
                        DispensedQuantity,
                        isParenteralNutrition);
                    component.UnitValue = unitValue;
                    component.TotalValue = unitValue * component.TotalQuantities;
                    component.ProductCodeName = $"{inventoryProduct.Code}-{inventoryProduct.Name}";
                    result.Add(component);
                }

                if (missingProductSb.Length > 0)
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = $"No se encontró producto para el o los componente(s): ({missingProductSb.ToString().Trim()})." };

                if (tariffSb.Length > 0)
                    return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = $"El o Los Producto(s): ({tariffSb.ToString().Trim()}) no tienen definida una Tarifa para el grupo de atención." };

                return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = true, ObjectEmbbeded = result, Message = "Componentes obtenidos exitosamente." };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.ProductRateDetailPackage>> { StateResult = false, Message = ex.Message };
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
