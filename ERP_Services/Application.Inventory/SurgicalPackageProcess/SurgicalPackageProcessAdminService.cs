using Application.Inventory.DashBoardPharmacy;
using Application.Inventory.PharmaceuticalDispensing;
using Application.Inventory.PhysicalInventory;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.SurgicalPackageProcess
{
    public class SurgicalPackageProcessAdminService : ISurgicalPackageProcessAdminService
    {
        #region Variables
        private IPharmaceuticalDispensingRepository _pharmaceuticalDispensingRepository;
        private IDashBoardPharmacyAdminService _dashBoardPharmacyAdminService;
        private IGeneralLedgerIVARepository _generalLedgerIVARepository;
        private IDashboardPharmacyDetailSurgicalPackageRepository _dashboardPharmacyDetailSurgicalPackageRepository;
        private ICompanySettingsRepository _companySettingsRepository;
        private IProductRateDetailRepository _productRateDetailRepository;
        private ISupplierRepository _supplierRepository;
        private IWarehouseRepository _wareHouseRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IThirdPartyRepository _thirdPartyRepository;
        private IInventoryProductRepository _inventoryProductRepository;
        private IPharmacyRepository _pharmacyRepository;
        private IPharmaceuticalDispensingAdminService _pharmaceuticalDispensingAdminService;
        private ISurgicalExpenseSheetRepository _surgicalExpenseSheetRepository;


        #endregion Variables

        #region Builder

        /// <summary>
        /// inicializa los repos
        /// </summary>
        public SurgicalPackageProcessAdminService(IPharmaceuticalDispensingRepository pharmaceuticalDispensingRepository, IDashBoardPharmacyAdminService dashBoardPharmacyAdminService, IGeneralLedgerIVARepository generalLedgerIVARepository,
            IDashboardPharmacyDetailSurgicalPackageRepository dashboardPharmacyDetailSurgicalPackageRepository, ICompanySettingsRepository companySettingsRepository, IProductRateDetailRepository productRateDetailRepository, ISupplierRepository supplierRepository,
            IWarehouseRepository wareHouseRepository, IPhysicalInventoryAdminService physicalInventoryAdminService, IThirdPartyRepository thirdPartyRepository, IInventoryProductRepository inventoryProductRepository, IPharmacyRepository pharmacyRepository,
            IPharmaceuticalDispensingAdminService pharmaceuticalDispensingAdminService, ISurgicalExpenseSheetRepository surgicalExpenseSheetRepository
            )
        {
            if ((pharmaceuticalDispensingRepository == null))
            {
                throw new ArgumentNullException("Repositorio de dispensación farmacéutica vacio");
            }
            if ((generalLedgerIVARepository == null))
            {
                throw new ArgumentNullException("Repositorio de generalLedgerIVARepository vacio");
            }
            if ((dashboardPharmacyDetailSurgicalPackageRepository == null))
            {
                throw new ArgumentNullException("Repositorio de dashboardPharmacyDetailSurgicalPackageRepository vacio");
            }
            if ((companySettingsRepository == null))
            {
                throw new ArgumentNullException("Repositorio de companySettingsRepository vacio");
            }
            if ((supplierRepository == null))
            {
                throw new ArgumentNullException("Repositorio de supplierRepository vacio");
            }
            if ((wareHouseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de wareHouseRepository vacio");
            }
            if ((thirdPartyRepository == null))
            {
                throw new ArgumentNullException("Repositorio de thirdPartyRepository vacio");
            }
            if ((inventoryProductRepository == null))
            {
                throw new ArgumentNullException("Repositorio de inventoryProductRepository vacio");
            }
            if ((pharmacyRepository == null))
            {
                throw new ArgumentNullException("Repositorio de pharmacyRepository vacio");
            }
            if ((surgicalExpenseSheetRepository == null))
            {
                throw new ArgumentNullException("Repositorio de surgicalExpenseSheetRepository vacio");
            }
            _pharmaceuticalDispensingRepository = pharmaceuticalDispensingRepository;
            _dashBoardPharmacyAdminService = dashBoardPharmacyAdminService;
            _generalLedgerIVARepository = generalLedgerIVARepository;
            _dashboardPharmacyDetailSurgicalPackageRepository = dashboardPharmacyDetailSurgicalPackageRepository;
            _companySettingsRepository = companySettingsRepository;
            _productRateDetailRepository = productRateDetailRepository;
            _supplierRepository = supplierRepository;
            _wareHouseRepository = wareHouseRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _thirdPartyRepository = thirdPartyRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _pharmacyRepository = pharmacyRepository;
            _pharmaceuticalDispensingAdminService = pharmaceuticalDispensingAdminService;
            _surgicalExpenseSheetRepository = surgicalExpenseSheetRepository;


        }

        #endregion Builder

        #region Methods

        /// <summary>
        /// Metodo que se encarga de realizar el proceso de solicitud de paquete qx, dispensación y confirmación hoja de gasto qx, para la integración
        /// </summary>
        /// <param name="objParams"></param>
        /// <param name="audit"></param>
        /// <param name="serverDate"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public ActionResult<string> AllSurgicalPackageTransaction(AllSurgicalPackageProcessWrapper objParams, AuditMessage audit, DateTime serverDate, string container)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    objParams.Components.TableName = "CirugiaPrincipalPaquetesQX";
                    DataSet dataset = new DataSet("ListaProductos");
                    dataset.Tables.Add(objParams.Components);
                    string xmlProductos = dataset.GetXml();

                    var surgicalPackageResult =  ConfirmSurgicalPackage(objParams.ProgramationId, audit.CodeUser, xmlProductos, txSettings);

                    if (!surgicalPackageResult.StateResult)
                    {
                        return BuilderErrorResult(surgicalPackageResult.Message);
                    }

                    objParams.DataUnilogMovement.CodigoDocumento = surgicalPackageResult.ObjectEmbbeded.ConsecutivoFarmacia.ToString();

                    DataUnilogMovementModel movementData = objParams.DataUnilogMovement;
                    var pharmaDetails = PharmaceuticalDispensingDetailObject(movementData, serverDate, audit);
                    var pharmaceuticalDispensingObject = JsonConvert.SerializeObject(pharmaDetails);
                    List <Domain.Entities.PharmaceuticalDispensing> pharmaceuticalDispensing = Utils.DeserializeJsonToEntity<List<Domain.Entities.PharmaceuticalDispensing>>(pharmaceuticalDispensingObject);
                    
                    var listDetailAnnular = new List<ViewDashBoardPharmacy_SurgicalPackageDeatils>();

                    var pharmaceuticalDispensingSurgicalPackageResult =  _pharmaceuticalDispensingAdminService.SaveDashboardPharmacySurgicalPackage(pharmaceuticalDispensing, listDetailAnnular, audit, 0);


                    if (!pharmaceuticalDispensingSurgicalPackageResult.StateResult)
                    {
                        return BuilderErrorResult(pharmaceuticalDispensingSurgicalPackageResult.Message);
                    }

                    var surgicalExpenseSheet = CreateSurgicalExpenseSheetObject(movementData, audit);
                    var surgicalExpenseSheetResult =  SaveConfirmSurgicalExpenseSheet(surgicalExpenseSheet, audit.CodeUser, txSettings, container);

                    if (!surgicalExpenseSheetResult.StateResult)
                    {
                        return BuilderErrorResult(surgicalExpenseSheetResult.Message);
                    }

                    scope.Complete();
                    return new ActionResult<string>
                    {
                        StatusCode = eStatusResult.SUCCESS,
                        StateResult = true,
                        Message = $"Se realiza exitosamente el proceso de solicitud de paquetes quirúrgicos; {pharmaceuticalDispensingSurgicalPackageResult.Message} y {surgicalExpenseSheetResult.Message}"
                    };
                }
                catch (Exception ex)
                {
                    return new ActionResult<String>
                    {
                        StatusCode = eStatusResult.EXCEPTION,
                        StateResult = false,
                        StateResultAux = false,
                        Message = ex.Message
                    };
                }
            }
        }

        /// <summary>
        /// Método para devolver los errores de las transacciones
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ActionResult<string> BuilderErrorResult(string message)
        {
            return new ActionResult<string>
            {
                StatusCode = eStatusResult.WARNING,
                StateResult = false,
                Message = message
            };
        }

        /// <summary>
        /// Método que confirma la hoja de gasto qx
        /// </summary>
        /// <param name="surgicalExpenseSheet"></param>
        /// <param name="userCode"></param>
        /// <param name="txSettings"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private ActionResult<SPHC_ConfirmarHojaGastoQX_Result> SaveConfirmSurgicalExpenseSheet(SurgicalExpenseSheetModel surgicalExpenseSheet, string userCode, TransactionOptions txSettings, string container)
        {
                try
                {
                    DataSet dataset = new DataSet("Root");
                    DataTable productTable = ConvertToDataTable(surgicalExpenseSheet.Products);
                    dataset.Tables.Add(productTable);
                    string xmlProductos = dataset.GetXml();

                    var resultProcess = _surgicalExpenseSheetRepository.SPHC_ConfirmarHojaGastoQX(5, string.Empty, surgicalExpenseSheet.FunctionalUnitName, surgicalExpenseSheet.UserName, surgicalExpenseSheet.WarehouseCode, surgicalExpenseSheet.CostCenterCode, userCode, surgicalExpenseSheet.CenterOfAttentionCode,
                                                                                                  surgicalExpenseSheet.FunctionalUnitCode, surgicalExpenseSheet.AdmissionNumber, surgicalExpenseSheet.PatientCode, surgicalExpenseSheet.ProfessionalCode, xmlProductos, userCode, surgicalExpenseSheet.SurgicalExpenseSheetId);
                    if (resultProcess.CodeMessage == "999")
                    {
                        return new ActionResult<SPHC_ConfirmarHojaGastoQX_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = resultProcess.Message };
                    }
                    return new ActionResult<SPHC_ConfirmarHojaGastoQX_Result> { ObjectEmbbeded = resultProcess, StatusCode = eStatusResult.SUCCESS, StateResult = true, Message = resultProcess.Message };
                }
                catch (Exception ex)
                {
                    throw new Exception("Ocurrio un Error al Consultar : " + Environment.NewLine + ex.Message);
                }
        }

        /// <summary>
        /// Método que convierte en DataTable el listado de productos de la hoja de gasto qx
        /// </summary>
        /// <param name="productList"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable(List<ProductSurgicalExpenseSheetModel> productList)
        {
            DataTable productTable = new DataTable("Products");

            productTable.Columns.Add("Orden");
            productTable.Columns.Add("ID");
            productTable.Columns.Add("IDHCHOJAGASTOQX");
            productTable.Columns.Add("CodigoProducto");
            productTable.Columns.Add("CodigoP");
            productTable.Columns.Add("Producto");
            productTable.Columns.Add("NombreP");
            productTable.Columns.Add("CANTIDADENTREGADA");
            productTable.Columns.Add("CANTIDADACEPTADADEV");
            productTable.Columns.Add("CANTIDADGASTADA");
            productTable.Columns.Add("CantidadGastadaInicial");
            productTable.Columns.Add("CANTIDADDEVOLVER");
            productTable.Columns.Add("ORIGENSOLICITUD");
            productTable.Columns.Add("FECHAREGISTRO");
            productTable.Columns.Add("OrigenProducto");
            productTable.Columns.Add("IDAGEPROGQX");
            productTable.Columns.Add("CONSEKARDEX");
            productTable.Columns.Add("RequestType");
            productTable.Columns.Add("StatusOrder");

            foreach (var product in productList)
            {
                productTable.Rows.Add(
                    product.Orden,
                    product.ID,
                    product.IDHCHOJAGASTOQX,
                    product.CodigoProducto,
                    product.CodigoP,
                    product.Producto,
                    product.NombreP,
                    product.CANTIDADENTREGADA,
                    product.CANTIDADACEPTADADEV,
                    product.CANTIDADGASTADA,
                    product.CantidadGastadaInicial,
                    product.CANTIDADDEVOLVER,
                    product.ORIGENSOLICITUD,
                    product.FECHAREGISTRO,
                    product.OrigenProducto,
                    product.IDAGEPROGQX,
                    product.CONSEKARDEX,
                    product.RequestType,
                    product.StatusOrder
                );
            }

            return productTable;

        }

        /// <summary>
        /// Método que lista los productos asociados a la hoja de gasto Qx y crea el modelo de datos a retornar
        /// </summary>
        /// <param name="movementData"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        private SurgicalExpenseSheetModel CreateSurgicalExpenseSheetObject(DataUnilogMovementModel movementData, AuditMessage audit)
        {

            List<HCHOJAGASTOQX_Model> surgicalExpenseObject = _surgicalExpenseSheetRepository.GetListSurgicalExpenseSheetByScheduleId(Convert.ToInt32(movementData.CodigoProgramacao), movementData.Atendimento);

            List<ProductSurgicalExpenseSheetModel> productSurgicalExpenseSheets = _surgicalExpenseSheetRepository.SPHC_ListarProductosHojaGastoQX(surgicalExpenseObject[0].ID).ToList();

            SurgicalExpenseSheetModel surgicalExpenseSheetModel = new SurgicalExpenseSheetModel()
            {
                FunctionalUnitName = surgicalExpenseObject[0].UnidadFuncional,
                UserName = audit.CodeUser,
                WarehouseCode = movementData.CentroCustoOrigem.Substring(1),
                CostCenterCode = movementData.CentroCustoDestino,
                CenterOfAttentionCode = "11011",
                FunctionalUnitCode = movementData.CentroCustoDestino,
                AdmissionNumber = movementData.Atendimento,
                PatientCode = surgicalExpenseObject[0].Identificacion,
                ProfessionalCode = surgicalExpenseObject[0].Profesional.Split('-')[0].Trim(),
                Products = productSurgicalExpenseSheets,
                SurgicalExpenseSheetId = surgicalExpenseObject[0].ID,
                TimeStamp = surgicalExpenseObject[0].ConcurrencyControl
            };

            return surgicalExpenseSheetModel;
        }

        /// <summary>
        /// Retorna el objeto de dispensación farmacéutica 
        /// </summary>
        /// <param name="movementData"></param>
        /// <param name="serverDate"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        private PharmaceuticalDispensingModel[] PharmaceuticalDispensingDetailObject(DataUnilogMovementModel movementData, DateTime serverDate, AuditMessage audit)
        {
            PharmaceuticalDispensingModel[] pharmaceuticalDispensing = new PharmaceuticalDispensingModel[1];

            var surgicalPackageModel = _pharmacyRepository.SurgicalPackageByConsecutive(Convert.ToDecimal(movementData.CodigoDocumento));
            movementData.CentroCustoDestino = surgicalPackageModel.UFUCODIGO.Trim();

            var admissionResult = _dashBoardPharmacyAdminService.GetAdmissionInformation(0, 0, movementData.Atendimento, movementData.CentroCustoDestino, audit);

            AdmissionInformation admissionInformation = admissionResult.ObjectEmbbeded;

            PharmaceuticalDispensingDetailModel[] dispensingDetails = PharmaceuticalDispensingDetail(movementData, surgicalPackageModel, admissionInformation, serverDate, audit);

            pharmaceuticalDispensing[0] = new PharmaceuticalDispensingModel()
            {
                Code = null,
                OperatingUnitId = 1,
                AdmissionNumber = movementData.Atendimento,
                DocumentDate = movementData.DataMovimentacao,
                AffectInventory = false,
                Status = 1,
                PharmaceuticalDispensingDetail = dispensingDetails,
                CodePatient = surgicalPackageModel.CodigoPaciente,
                PantientName = surgicalPackageModel.NombrePaciente,
                CareCenterCode = "11011",
                FunctionUnitCode = movementData.CentroCustoDestino,
                FunctionUnitName = surgicalPackageModel.UnidadFuncional,
                ConsecutivePescription = 0,
                ConsecutiveInputs = 0,
                ConsecutivePharmacy = movementData.CodigoDocumento,
                ConsecutiveCrystal = movementData.CodigoDocumento,
                HistoryType = null,
                CodeNameWarehouse = "",
                OfficeType = 0,
                LogisticOperator = 0,
                Validado = false
            };
            return pharmaceuticalDispensing;
        }

        /// <summary>
        /// Confirma la solicitud de paquete qx
        /// </summary>
        /// <param name="programationId"></param>
        /// <param name="codeUser"></param>
        /// <param name="products"></param>
        /// <param name="txSettings"></param>
        /// <returns></returns>
        private ActionResult<SP_AGE_ConfirmarSolicitarPaquete_Result> ConfirmSurgicalPackage(int programationId, string codeUser, string products, TransactionOptions txSettings)
        {
                try
                {
                    var resultProcess = _surgicalExpenseSheetRepository.SP_AGE_ConfirmarSolicitarPaquete(programationId, codeUser, products);
                    if (resultProcess.CodeMessage == "999")
                    {
                        return new ActionResult<SP_AGE_ConfirmarSolicitarPaquete_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = resultProcess.Message };
                    }
                    return new ActionResult<SP_AGE_ConfirmarSolicitarPaquete_Result> { ObjectEmbbeded = resultProcess, StatusCode = eStatusResult.SUCCESS, StateResult = true, Message = resultProcess.Message };
                }
                catch (Exception ex)
                {
                    throw new Exception("Ocurrio un Error al Consultar : " + Environment.NewLine + ex.Message);
                }
        }

        /// <summary>
        /// Método que arma el detalle de la dispensación
        /// </summary>
        /// <param name="movementData"></param>
        /// <param name="surgicalPackageModel"></param>
        /// <param name="admissionInformation"></param>
        /// <param name="serverDate"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        private PharmaceuticalDispensingDetailModel[] PharmaceuticalDispensingDetail(DataUnilogMovementModel movementData, ViewDashBoardPharmacy_SurgicalPackage surgicalPackageModel, AdmissionInformation admissionInformation, DateTime serverDate, AuditMessage audit)
        {
            PharmaceuticalDispensingDetailModel[] pharmaceuticalDispensingDetail = new PharmaceuticalDispensingDetailModel[1];


            Domain.Entities.InventoryProduct product = _inventoryProductRepository.GetInventoryProduct(movementData.Produto);
            GeneralLedgerIVA generalLedgerIVA = product.IVAId.HasValue ? _generalLedgerIVARepository.GetGeneralLedgerIVAById(product.IVAId.Value) : null;

            var surgicalPackageDetails = _dashboardPharmacyDetailSurgicalPackageRepository.DashboardPharmacyDetailSurgicalPackage(Convert.ToDecimal(movementData.CodigoDocumento), surgicalPackageModel.CodigoPaciente, movementData.Produto.Trim());

            if (movementData.Quantidade > surgicalPackageDetails.CantidadPendiente)
            {
                throw new Exception(String.Format("La cantidad a entregar para el producto {0} - {1} supera la cantidad pendiente.", product.Code, product.Name));
            }
            ThirdParty thirdPartyProfessional = _thirdPartyRepository.GetThirdPartyByNit(surgicalPackageDetails.NitMedico.Trim());
            CompanySettings companySettings = _companySettingsRepository.GetCompanySettings();
            Domain.Entities.ProductRateDetail productRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(admissionInformation.careGroup.Id, product.Id, serverDate);
            decimal salesPrice = Math.Round(productRateDetail.SalesValue, 2, MidpointRounding.AwayFromZero);
            ProductSalePrice productSalePrice = GetProductSalePrice(companySettings.SalePriceIncludeTax, salesPrice, product.PercentageIVA, movementData.Quantidade);
            Domain.Entities.PhysicalInventory physicalInventoryByProduct = GetPhysicalInventory(movementData, product.Id, Convert.ToInt32(surgicalPackageDetails.Tipo), audit.IdUser ,admissionInformation.careGroup.Id);
            if (movementData.Quantidade > physicalInventoryByProduct.Quantity)
            {
                throw new Exception(String.Format("La cantidad en el inventario del producto {0} es menor a la cantidad a entregar.", movementData.Produto));
            }

            pharmaceuticalDispensingDetail[0] = new PharmaceuticalDispensingDetailModel()
            {
                CareGroupId = admissionInformation.careGroup.Id,
                HealthAdministratorId = admissionInformation.healthAdministratorId,
                ThirdPartyId = admissionInformation.thirdPartyId,
                ProductId = product.Id,
                WarehouseId = physicalInventoryByProduct.WarehouseId,
                Quantity = movementData.Quantidade,
                ReturnedQuantity = 0,
                ServiceDate = movementData.DataMovimentacao,
                FunctionalUnitId = admissionInformation.functionalUnit.Id,
                OrderedHealthProfessionalCode = surgicalPackageDetails.NitMedico.Trim(),
                OrderedProfessionalSpecialty = surgicalPackageDetails.Especialidad.Split('-')[0].Trim(),
                OrderedHealthProfessionalThirdPartyId = thirdPartyProfessional.Id,
                AuthorizationNumber = null,
                LiquidationType = 1,
                CupsEntityId = null,
                SurchargeApply = false,
                SalePrice = productSalePrice.SalePrice,
                AverageCost = product.ProductCost,
                DiscountPercentage = 0.0M,
                DiscountValue = 0.0M,
                TotalSalesPrice = productSalePrice.TotalSalesPrice,
                GrandTotalSalesPrice = productSalePrice.GrandTotalSalesPrice,
                QuotationPharmaceuticalDispensingDetailId = null,
                EntityId = null,
                EntityName = null,
                FinalProductCost = product.FinalProductCost,
                GrossValue = productSalePrice.GrossValue,
                TaxValue = productSalePrice.TaxValue,
                IvaId = product.IVAId,
                FunctionalUnit = null,
                PharmaceuticalDispensingDetailBatchSerial = PharmaceuticalDispensingDetailBatchSerial(movementData, product.Id, physicalInventoryByProduct.Id, physicalInventoryByProduct.WarehouseId, audit.CodeUser),
                CodeProduct = movementData.Produto,
                MedicamentCode = movementData.Produto,
                NameProduct = product.Name.Trim(),
                CantidadPendiente = surgicalPackageDetails.CantidadPendiente,
                CantidadSolicitada = surgicalPackageDetails.CantidadSolicitada,
                ProductType = surgicalPackageDetails.Tipo,
                idProductoHeon = 0,
                recetarioOMedica = "",
                GuardaGastoQX = true,
                IdProgramacionQXPrincipal = surgicalPackageDetails.IDAGEPROGQX,
                Extramural = false,
                Custody = false,
                QuotationId = 0,
                AuthorizationOutsourcedServicesId = 0,
                TypeProduct = 0,
                CantidadMezcla = 0,
                Note = null
            };

            return pharmaceuticalDispensingDetail;
        }

        /// <summary>
        /// Retorna el modelo de datos para el lote del producto
        /// </summary>
        /// <param name="movementData"></param>
        /// <param name="ProductId"></param>
        /// <param name="physicalInventoryId"></param>
        /// <param name="IdWarehouse"></param>
        /// <param name="CodeUser"></param>
        /// <returns></returns>
        private PharmaceuticalDispensingDetailBatchSerialModel[] PharmaceuticalDispensingDetailBatchSerial(DataUnilogMovementModel movementData, int ProductId, int physicalInventoryId, int IdWarehouse, string CodeUser)
        {
            PharmaceuticalDispensingDetailBatchSerialModel[] pharmaceuticalDispensingDetailBatchSerial = new PharmaceuticalDispensingDetailBatchSerialModel[1];
            pharmaceuticalDispensingDetailBatchSerial[0] = new PharmaceuticalDispensingDetailBatchSerialModel()
            {
                PhysicalInventoryId = physicalInventoryId,
                Quantity = movementData.Quantidade,
                OutstandingQuantity = movementData.Quantidade,
                ProductId = ProductId,
                IdWarehouse = IdWarehouse,
                BatchSerialId = null,
                UserIndigo = CodeUser
            };

            return pharmaceuticalDispensingDetailBatchSerial;
        }

        /// <summary>
        /// Método que calcula el valor de venta del producto
        /// </summary>
        /// <param name="SalePriceIncludeTax"></param>
        /// <param name="SalesPrice"></param>
        /// <param name="IvaIdProduct"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        private ProductSalePrice GetProductSalePrice(bool SalePriceIncludeTax, decimal SalesPrice, decimal IvaIdProduct, int Quantity)
        {
            decimal _grossValue;
            decimal _taxValue;
            decimal _salePrice;
            decimal _totalSalesPrice;

            if (SalePriceIncludeTax)
            {
                _grossValue = (IvaIdProduct != 0) ? SalesPrice / (1 + (IvaIdProduct / 100)) : SalesPrice;
                _taxValue = (IvaIdProduct != 0) ? _grossValue * ((IvaIdProduct / 100)) : 0;
                _salePrice = SalesPrice;
                _totalSalesPrice = SalesPrice;
            }
            else
            {
                _grossValue = SalesPrice;
                _taxValue = (IvaIdProduct != 0) ? SalesPrice * (IvaIdProduct / 100) : 0;
                _salePrice = _grossValue + _taxValue;
                _totalSalesPrice = _salePrice;
            }
            ProductSalePrice productSalePrice = new ProductSalePrice()
            {
                GrossValue = _grossValue,
                TaxValue = _taxValue,
                SalePrice = _salePrice,
                TotalSalesPrice = _totalSalesPrice,
                GrandTotalSalesPrice = _totalSalesPrice * Quantity
            };
            return productSalePrice;
        }


        /// <summary>
        /// Método que obtiene el inventario físico
        /// </summary>
        /// <param name="movementData"></param>
        /// <param name="productId"></param>
        /// <param name="typeProduct"></param>
        /// <param name="userId"></param>
        /// <param name="careGroupId"></param>
        /// <returns></returns>
        private Domain.Entities.PhysicalInventory GetPhysicalInventory(DataUnilogMovementModel movementData, int productId, int typeProduct, int userId ,int careGroupId)
        {
            List<Domain.Entities.PhysicalInventory> listPhysicalInventory = _physicalInventoryAdminService.ListPhysicalInventoryByATCNumberWithAdditionalInformation(movementData.Produto, typeProduct, userId, careGroupId);
            Domain.Entities.PhysicalInventory physicalInventoryByProduct;

            if (movementData.Proveedor != null && movementData.Programa == "CONS")
            {
                Supplier supplier = _supplierRepository.GetSupplier(movementData.Proveedor);
                Domain.Entities.Warehouse warehouseOrigin = _wareHouseRepository.GetWarehouseSupplierByType(supplier.Id, 2);//Bodega de consignacion
                if (warehouseOrigin.Id == 0)
                {
                    throw new Exception(String.Format("No existe una bodega de consignación para el proveedor {0}.", movementData.Proveedor));
                }
                physicalInventoryByProduct = listPhysicalInventory.Find(x => x.ProductId == productId && x.WarehouseId == warehouseOrigin.Id);
                if (physicalInventoryByProduct == null)
                {
                    throw new Exception(String.Format("El producto {0} en la bodega {1}, no contiene un inventario disponible para realizar la dispensación.", movementData.Produto, movementData.CentroCustoOrigem));
                }
            }
            else
            {
                Domain.Entities.Warehouse warehouseOrigin = _wareHouseRepository.GetWarehouse(movementData.CentroCustoOrigem.Substring(1));
                physicalInventoryByProduct = listPhysicalInventory.Find(x => x.ProductId == productId && x.WarehouseId == warehouseOrigin.Id);

                if (physicalInventoryByProduct == null)
                {
                    throw new Exception(String.Format("El producto {0} en la bodega {1}, no contiene un inventario disponible para realizar la dispensación.", movementData.Produto, movementData.CentroCustoOrigem));
                }
            }
            return physicalInventoryByProduct;
        }
        #endregion Methods

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
                    //_physicalInventoryAdminService.Dispose();
                }
                _pharmaceuticalDispensingRepository = null;
                _dashBoardPharmacyAdminService = null;
                _generalLedgerIVARepository = null;
                _dashboardPharmacyDetailSurgicalPackageRepository = null;
                _companySettingsRepository = null;
                _productRateDetailRepository = null;
                _supplierRepository = null;
                _wareHouseRepository = null;
                _physicalInventoryAdminService = null;
                _thirdPartyRepository = null;
                _inventoryProductRepository = null;
                _pharmacyRepository = null;

                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion
    }
}
