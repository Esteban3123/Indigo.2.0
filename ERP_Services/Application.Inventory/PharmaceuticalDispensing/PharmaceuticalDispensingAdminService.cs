using Application.Accounting;
using Application.Base;
using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using Application.Inventory.PhysicalInventory;
using Application.Inventory.Sequense;
using Application.Inventory.TransferOrder;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.PharmaceuticalDispensing
{
    public class PharmaceuticalDispensingAdminService : IPharmaceuticalDispensingAdminService
    {
        #region Variables

        private const string TAG_SERVICEORDER = "755";
        private IPharmaceuticalDispensingRepository _pharmaceuticalDispensingRepository;
        private IPharmaceuticalDispensingDetailRepository _pharmaceuticalDispensingDetailRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IAdmissionRepository _admissionRepository;
        private IHealthProfessionalRepository _healthProfessionalRepository;
        private IFunctionalUnitRepository _functionalUnitRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IInventoryService _inventoryServices;
        private IAccountingDocumentAdminService _accountingAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IProductGroupsRepository _productGroupRepository;
        private IInventoryProductRepository _inventoryProductRepository;
        private IWarehouseRepository _wareHouseRepository;
        private IKardexCrystalRepository _kardexCrystalRepository;
        private Domain.Crystal.IConsecutiveRepository _consecutiveRepository;
        private IPharmacyDetailRepository _pharmacyDetailRepository;
        private IPharmacyRepository _pharmacyRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IHospitalInventoryRepository _hospitalInventoryRepository;
        private IMedicalPrescriptionRepository _medicalPrescriptionRepository;
        private IPhysicalInventoryCrystalRepository _physicalInventoryCrystalRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IStayRepository _stayRepository;
        private IServiceOrderRepository _serviceOrderRepository;
        private IAccountingDocumentRepository _accountingDocumentRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialAdminService _consignmentInventoryRemissionDetailBatchSerialAdminService;
        private IInventoryControlServiceRepository _inventoryControlServiceRepository;
        private ITransferOrderAdminService _transferOrderAdminService;
        private IInventorySequenceRepository _sequenceRepository;
        private IInventorySequenceAdminService _sequenceAdminService;
        private IOperatingUnitRepository _operatingUnitRepository;
        private IThirdPartyRepository _thirdPartyRepository;
        private ISettingsAccountRepository _settingsAccountRepository;
        private IBillingAuthorizationRepository _billingAuthorizationRepository;
        private IInvoiceRepository _invoiceRepository;
        private IElectronicDocumentRepository _electronicDocumentRepository;
        private IProductServiceDetailRepository _productServiceDetailRepository;
        private IDetailPhysicalCUM _detailPhysicalCUMRepository;
        private IHCFISIPRORepository _hCFISIPRORepository;
        private IRequestPackageDetailStatusRepository _requestPackageDetailStatusRepository;
        private IPharmaDoseRepository _pharmaDoseRepository;
        private IINUNIFUNCRepository _iNUNIFUNCRepository;

        #endregion Variables

        #region Builder

        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        public PharmaceuticalDispensingAdminService(IPharmaceuticalDispensingRepository pharmaceuticalDispensingRepository, IInventorySequenceDetailRepository sequenseRepository, IAdmissionRepository admissionRepository,
            IHealthProfessionalRepository healthProfessionalRepository, IFunctionalUnitRepository functionalUnitRepository,
            IPhysicalInventoryAdminService physicalInventoryAdminService, IInventoryService inventoryServices, IAccountingDocumentAdminService accountingAdminService, ISettingInventoryRepository settingInventoryRepository,
            IProductGroupsRepository productGroupRepository, IInventoryProductRepository inventoryProductRepository, IWarehouseRepository wareHouseRepository, IKardexCrystalRepository kardexCrystalRepository,
            Domain.Crystal.IConsecutiveRepository consecutiveRepository, IPharmacyDetailRepository pharmacyDetailRepository, IPharmacyRepository pharmacyRepository, IPhysicalInventoryRepository physicalInventoryRepository,
            IHospitalInventoryRepository hospitalInventoryRepository, IMedicalPrescriptionRepository medicalPrescriptionRepository, IPhysicalInventoryCrystalRepository physicalInventoryCrystalRepository,
            IInventoryControlDocumentRepository InventoryControlDocumentRepository, IStayRepository stayRepository, IServiceOrderRepository serviceOrderRepository, IAccountingDocumentRepository accountingDocumentRepository, IPharmaceuticalDispensingDetailRepository pharmaceuticalDispensingDetailRepository,
            IConsignmentInventoryRemissionDetailBatchSerialAdminService consignmentInventoryRemissionDetailBatchSerialAdminService, IInventoryControlServiceRepository inventoryControlServiceRepository,
            ITransferOrderAdminService transferOrderAdminService, IInventorySequenceAdminService sequenceAdminService, IInventorySequenceRepository sequenceRepository,
            IOperatingUnitRepository operatingUnitRepository, IThirdPartyRepository thirdPartyRepository, ISettingsAccountRepository settingsAccountRepository, IBillingAuthorizationRepository billingAuthorizationRepository,
            IInvoiceRepository invoiceRepository, IElectronicDocumentRepository electronicDocumentRepository, IProductServiceDetailRepository ProductServiceDetailRepository, IDetailPhysicalCUM DetailPhysicalCUMRepository,
            IHCFISIPRORepository HCFISIPRORepository, IRequestPackageDetailStatusRepository RequestPackageDetailStatusRepository, IPharmaDoseRepository PharmaDoseRepository, IINUNIFUNCRepository INUNIFUNCRepository)
        {
            if ((physicalInventoryCrystalRepository == null))
            {
                throw new ArgumentNullException("physicalInventoryCrystalRepository vacio");
            }
            if ((hospitalInventoryRepository == null))
            {
                throw new ArgumentNullException("hospitalInventoryRepository vacio");
            }
            if ((medicalPrescriptionRepository == null))
            {
                throw new ArgumentNullException("medicalPrescriptionRepository vacio");
            }
            if ((pharmaceuticalDispensingRepository == null))
            {
                throw new ArgumentNullException("Repositorio de dispensación farmacéutica vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if ((admissionRepository == null))
            {
                throw new ArgumentNullException("Repositorio de admissionRepository vacio");
            }
            if ((healthProfessionalRepository == null))
            {
                throw new ArgumentNullException("Repositorio de healthProfessionalRepository vacio");
            }
            if ((functionalUnitRepository == null))
            {
                throw new ArgumentNullException("Repositorio de functionalUnitRepository vacio");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            if (pharmaceuticalDispensingDetailRepository == null)
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDetailRepository");
            }
            _sequenceAdminService = sequenceAdminService;
            _healthProfessionalRepository = healthProfessionalRepository;
            _pharmaceuticalDispensingRepository = pharmaceuticalDispensingRepository;
            _sequenseRepository = sequenseRepository;
            _admissionRepository = admissionRepository;
            _functionalUnitRepository = functionalUnitRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _inventoryServices = inventoryServices;
            _accountingAdminService = accountingAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _productGroupRepository = productGroupRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _wareHouseRepository = wareHouseRepository;
            _kardexCrystalRepository = kardexCrystalRepository;
            _consecutiveRepository = consecutiveRepository;
            _pharmacyDetailRepository = pharmacyDetailRepository;
            _pharmacyRepository = pharmacyRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _hospitalInventoryRepository = hospitalInventoryRepository;
            _medicalPrescriptionRepository = medicalPrescriptionRepository;
            _physicalInventoryCrystalRepository = physicalInventoryCrystalRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _stayRepository = stayRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _accountingDocumentRepository = accountingDocumentRepository;
            _pharmaceuticalDispensingDetailRepository = pharmaceuticalDispensingDetailRepository;
            _consignmentInventoryRemissionDetailBatchSerialAdminService = consignmentInventoryRemissionDetailBatchSerialAdminService;
            _inventoryControlServiceRepository = inventoryControlServiceRepository;
            _transferOrderAdminService = transferOrderAdminService;
            _sequenceRepository = sequenceRepository;

            _operatingUnitRepository = operatingUnitRepository;
            _thirdPartyRepository = thirdPartyRepository;
            _settingsAccountRepository = settingsAccountRepository;
            _billingAuthorizationRepository = billingAuthorizationRepository;
            _invoiceRepository = invoiceRepository;
            _electronicDocumentRepository = electronicDocumentRepository;
            _productServiceDetailRepository = ProductServiceDetailRepository;
            _detailPhysicalCUMRepository = DetailPhysicalCUMRepository;
            _hCFISIPRORepository = HCFISIPRORepository;
            _requestPackageDetailStatusRepository = RequestPackageDetailStatusRepository;
            _pharmaDoseRepository = PharmaDoseRepository;
            _iNUNIFUNCRepository = INUNIFUNCRepository;
        }

        #endregion Builder

        #region Public Methods

        #region FunctionalUnit

        /// <summary>
        /// metodo para actualizar la unidad funcional desde el dashboard
        /// </summary>
        /// <param name="ConsecutivePharmacy"></param>
        /// <param name="funcionalUnitCode"></param>
        /// <returns></returns>
        public ActionResult UpdateFunctionalUnitCrystal(decimal ConsecutivePharmacy, string funcionalUnitCode)
        {
            try
            {
                var pharmacy = _pharmacyRepository.GetPharmacyByConsecutiveWithDetail(ConsecutivePharmacy);
                pharmacy.UFUCODIGO = funcionalUnitCode;

                foreach (var pharmacyDetail in pharmacy.HCFARMEPD)
                {
                    pharmacyDetail.UFUCODIGO = funcionalUnitCode;
                }

                _pharmacyRepository.SaveEntity(pharmacy);
                _pharmacyRepository.UnitWork.Commit();
                return new ActionResult { StateResult = true };
            }
            catch (Exception ex)
            {
                return new ActionResult { StateResult = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// metodo para validar la unidad funcional en el dashboard de solicitudes
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="patientCode"></param>
        /// <param name="functionalUnitCode"></param>
        /// <returns></returns>
        public ActionResult<string> ValidateFunctionalUnitDashBoard(string admissionNumber, string patientCode, string functionalUnitCode, int requestNumber)
        {
            //valdar que el paciente no tenga egreso
            var exitsEgress = _pharmacyRepository.GetPatientEgress(admissionNumber);
            if (exitsEgress)
            {
                return new ActionResult<string> { StateResult = false, Message = "El paciente " + patientCode + " ya fue egresado de la institución, favor actualizar los datos" };
            }

            //Se valida si la Unidad Funcional de la solicitud es de tipo cirugia
            bool FunctionalUnitTypeUrgency = false;
            FunctionalUnitTypeUrgency = _iNUNIFUNCRepository.GetByFilter(x => x.UFUCODIGO == functionalUnitCode && new List<int> { 19 }.Contains(x.UFUTIPUNI)).Any();
            if (FunctionalUnitTypeUrgency) 
            {
                //validamos que la solicitud provenga desde Paquetes Quirúrgicos u Hojas de Gasto Quirúrgico
                var result = _stayRepository.GetHCFARMEPDByRequest(admissionNumber, patientCode, requestNumber);
                if (result != null)
                {
                    if (result.IDAGEPROGQX.HasValue || result.IDAGPAQUETES.HasValue) { return new ActionResult<string> { StateResult = true, ObjectEmbbeded = functionalUnitCode }; }
                }
            }

            //validamos si el paciente esta en otra unidad funcional para actualizar la orden
            var stayAdmission = _stayRepository.GetStayByAdmissionNumber(admissionNumber);
            if (stayAdmission.Count == 0)
            {
                return new ActionResult<string> { StateResult = true, ObjectEmbbeded = functionalUnitCode };
            }
            else if (stayAdmission.Count == 1)
            {
                return new ActionResult<string> { StateResult = true, ObjectEmbbeded = stayAdmission[0].CHCAMASHO.UFUCODIGO };
            }
            else if (stayAdmission.Count > 1)//si el paciente tiene orden de traslado de cama
            {
                var stay = stayAdmission.Find(x => x.CHCAMASHO.CODCONCEC != null);
                if (stay != null)
                {
                    return new ActionResult<string> { StateResult = false, Message = "El paciente " + patientCode + " tiene pendiente una aceptación de medicamentos por motivo de traslado de hospitalización en la unidad funcional " + stay.CHCAMASHO.UFUCODIGO };
                }
                else
                {
                    stay = stayAdmission.OrderByDescending(s => s.FECINIEST).FirstOrDefault();
                    if (stay.REGESTADO != 1)
                    {
                        return new ActionResult<string> { StateResult = false, Message = "El paciente " + patientCode + " se encuentra en una estancia " + (stay.REGESTADO == 2 ? "Liquidada Parcial" : "Liquidada Total") + " en la unidad funcional " + stay.CHCAMASHO.UFUCODIGO };
                    }
                    return new ActionResult<string> { StateResult = true, ObjectEmbbeded = stay.CHCAMASHO.UFUCODIGO };
                }
            }
            return new ActionResult<string> { StateResult = false, Message = "El paciente " + patientCode + " tiene un error en el modulo de hospitalización, está asignado en dos o mas camas en las unidades funcionales" + string.Join(" - ", (from a in stayAdmission select a.CHCAMASHO.UFUCODIGO).ToList()) };
        }

        /// <summary>
        /// Obtiene el listado de medicamentos mediante una formula medica
        /// </summary>
        /// <param name="CODCONCEC"></param>
        /// <returns></returns>
        public ActionResult<List<SP_ListHCPRESCRDByCODCONCEC_Result>> SP_ListHCPRESCRDByCODCONCEC(string CODCONCEC)
        {
            try
            {
                List<SP_ListHCPRESCRDByCODCONCEC_Result> list = _pharmaceuticalDispensingRepository.SP_ListHCPRESCRDByCODCONCEC(CODCONCEC);
                return new ActionResult<List<SP_ListHCPRESCRDByCODCONCEC_Result>> { StateResult = true, ObjectEmbbeded = list, Message = "" };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<SP_ListHCPRESCRDByCODCONCEC_Result>> { StateResult = false, ObjectEmbbeded = null, Message = ex.Message };
            }
        }

        #endregion

        #region PharmaceuticalDispensing

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Id</exception>
        public Domain.Entities.PharmaceuticalDispensing GetPharmaceuticalDispensingById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensingById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene una dispensación farmacéutica
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// Code
        /// or
        /// audit
        /// </exception>
        public ActionResult<Domain.Entities.PharmaceuticalDispensing> GetPharmaceuticalDispensing(string code, AuditMessage audit)
        {
            if (code == string.Empty)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                ((IObjectContextAdapter)_pharmaceuticalDispensingRepository.UnitWork).ObjectContext.CommandTimeout = 3600;
                ((IObjectContextAdapter)_healthProfessionalRepository.UnitWork).ObjectContext.CommandTimeout = 3600;
                Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing = _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensing(code);
                if (pharmaceuticalDispensing != null && pharmaceuticalDispensing.Id > 0)
                {
                    dynamic admission = _admissionRepository.GetAdmissionByServiceOrder(pharmaceuticalDispensing.AdmissionNumber);
                    if (admission != null)
                        pharmaceuticalDispensing.FullNameAdmission = String.Format(ResourceManager.get_GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim());

                    foreach (PharmaceuticalDispensingDetail item in pharmaceuticalDispensing.PharmaceuticalDispensingDetail)
                    {
                        INPROFSAL professional = _healthProfessionalRepository.GetHealthProfessionalByCode(item.OrderedHealthProfessionalCode);
                        if (professional != null && !string.IsNullOrEmpty(professional.CODPROSAL) && !string.IsNullOrEmpty(professional.NOMMEDICO))
                            item.CodeNameHealthProfessional = string.Concat(professional.CODPROSAL.Trim(), " - ", professional.NOMMEDICO.Trim());

                        List<Tuple<string, string>> ListSpecialty = new List<Tuple<string, string>>();
                        if (professional.INESPECIA != null)
                        {
                            ListSpecialty.Add(new Tuple<string, string>(professional.INESPECIA.CODESPECI, string.Concat(professional.INESPECIA.CODESPECI.Trim(), " - ", professional.INESPECIA.DESESPECI.Trim())));
                        }
                        if (professional.INESPECIA1 != null)
                        {
                            ListSpecialty.Add(new Tuple<string, string>(professional.INESPECIA1.CODESPECI, string.Concat(professional.INESPECIA1.CODESPECI.Trim(), " - ", professional.INESPECIA1.DESESPECI.Trim())));
                        }
                        if (professional.INESPECIA2 != null)
                        {
                            ListSpecialty.Add(new Tuple<string, string>(professional.INESPECIA2.CODESPECI, string.Concat(professional.INESPECIA2.CODESPECI.Trim(), " - ", professional.INESPECIA2.DESESPECI.Trim())));
                        }
                        if (ListSpecialty.Count > 0)
                        {
                            var Speciality = ListSpecialty.Where(x => x.Item1 == item.OrderedProfessionalSpecialty).FirstOrDefault();
                            if (Speciality != null)
                            {
                                item.CodeNameHealthProfessionalSpeciality = Speciality.Item2;
                            }
                        }
                        //else
                        //{
                        //    return new ActionResult<Domain.Entities.PharmaceuticalDispensing> { StateResultAux = false, ObjectEmbbeded = pharmaceuticalDispensing, Message = "El médico no tiene ninguna especialidad asignada." };
                        //}
                    }

                    IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensing> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensing>(pharmaceuticalDispensing, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.PharmaceuticalDispensing> { StateResult = true, ObjectEmbbeded = pharmaceuticalDispensing };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalDispensing> { StateResult = false, ObjectEmbbeded = null, Message = ex.Message };
            }
        }

        /// <summary>
        /// Guarda una dispensación farmacéutica
        /// </summary>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">PharmaceuticalDispensing</exception>
        public ActionResult<Domain.Entities.PharmaceuticalDispensing> SavePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit, bool confirm, long idSecuence = 0, bool affectInventory = true)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    if (confirm)
                    {
                        pharmaceuticalDispensing.Status = 2;
                        List<Int32> idxs = new List<int>();
                        pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Where((d) => d.Id > 0).ToList().ForEach((idd) => idxs.Add(idd.Id));
                        List<PharmaceuticalDispensingDetail> list = _pharmaceuticalDispensingDetailRepository.ListPharmaceuticalDispensingDetailsByIds(pharmaceuticalDispensing.Id, idxs, false);
                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                foreach (PharmaceuticalDispensingDetail item in list)
                                {
                                    item.MarkAsUnchanged();
                                    pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(item);
                                }
                            }
                        }
                    }
                    foreach (var d in pharmaceuticalDispensing.PharmaceuticalDispensingDetail)
                    {
                        foreach (var bs in d.PharmaceuticalDispensingDetailBatchSerial.Where(bs => bs.PhysicalInventoryCustodyId == 0))
                        {
                            bs.PhysicalInventoryCustodyId = null;
                        }
                        foreach (var bs in d.PharmaceuticalDispensingDetailBatchSerial.Where(bs => bs.PhysicalInventoryId == 0))
                        {
                            bs.PhysicalInventoryId = null;
                        }
                    }

                    var xml = pharmaceuticalDispensing.ToXML(true);
                    var result = _pharmaceuticalDispensingRepository.GeneratePharmaceuticalDispensingSP(xml, "", audit.CodeUser);
                    var ObjResult = result.ToList()[0];
                    ActionResult<Domain.Entities.PharmaceuticalDispensing> actionResultReturn = new ActionResult<Domain.Entities.PharmaceuticalDispensing>();

                    actionResultReturn.Message = ObjResult.Message;
                    if (ObjResult.Status == 1)
                    {
                        string[] sparator = { " *USERINDIGO* " };

                        var splitMessage = ObjResult.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);

                        actionResultReturn.Message = splitMessage.ElementAt(0);

                        actionResultReturn.StateResult = true;
                        actionResultReturn.StatusCode = eStatusResult.SUCCESS;

                        actionResultReturn.ObjectEmbbeded = _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensingById(ObjResult.DispensingId);
                        foreach (var x in pharmaceuticalDispensing.PharmaceuticalDispensingDetail)
                        {
                            if (x.ProductServiceDetail is null || x.ProductServiceDetail.Count == 0)
                            {
                                continue;
                            }

                            SaveProductServiceDetail(x.ProductServiceDetail.ToList(), actionResultReturn.ObjectEmbbeded, x.ProductId);

                        }

                        actionResultReturn.MessageResult = new List<string>();
                        actionResultReturn.MessageResult.Add(ObjResult.Message);
                    }
                    else
                    {
                        actionResultReturn.StateResult = false;
                        actionResultReturn.StatusCode = eStatusResult.WARNING;
                        actionResultReturn.Message = ObjResult.Message;
                        return actionResultReturn;
                    }

                    transaction.Complete();
                    return actionResultReturn;
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensing> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

        /// <summary>
        /// Funcion para guardar o actualizar registro de la tabla Product Service Detail
        /// </summary>
        /// <param name="ListProductServiceDetail"></param>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public void SaveProductServiceDetail(List<ProductServiceDetail> ListProductServiceDetail, Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, int ProductId)
        {

            if (ListProductServiceDetail.Where(x => x.ChangeTracker.State == Domain.Base.Entities.ObjectState.Unchanged).Count() == ListProductServiceDetail.Count()
                && pharmaceuticalDispensing.Status != 2) { return; }

            foreach (var k in ListProductServiceDetail)
            {
                var PSD = new ProductServiceDetail
                {
                    Id = k.Id,
                    PharmaceuticalDispensingDetailId = k.PharmaceuticalDispensingDetailId,
                    ServiceOrderDetailId = k.ServiceOrderDetailId,
                    CUPSEntityId = k.CUPSEntityId,
                    ContractDescriptionsId = k.ContractDescriptionsId,
                    ProductId = k.ProductId,
                    Price = k.Price,
                    LiquidationType = k.LiquidationType,
                    RateType = k.RateType,
                    DiscountPercentage = k.DiscountPercentage
                };
                switch (k.ChangeTracker.State)
                {
                    case Domain.Base.Entities.ObjectState.Modified:
                        PSD.MarkAsModified();
                        break;
                    case Domain.Base.Entities.ObjectState.Deleted:
                        PSD.MarkAsDeleted();
                        break;
                }

                if (pharmaceuticalDispensing.Status == 2)
                {
                    if (k.ChangeTracker.State == Domain.Base.Entities.ObjectState.Unchanged) { PSD.MarkAsModified(); }
                    var ServiceOrder = _serviceOrderRepository.FirstOrDefault(u => u.EntityId == pharmaceuticalDispensing.Id && u.EntityName == "PharmaceuticalDispensing", false, new List<string>() { "ServiceOrderDetail" });
                    PSD.ServiceOrderDetailId = (from o in ServiceOrder.ServiceOrderDetail where o.ProductId == ProductId select o.Id).FirstOrDefault();
                }

                if (PSD.PharmaceuticalDispensingDetailId == 0)
                {
                    PSD.PharmaceuticalDispensingDetailId = (from l in pharmaceuticalDispensing.PharmaceuticalDispensingDetail where l.ProductId == ProductId select l.Id).FirstOrDefault();
                }
                _productServiceDetailRepository.SaveEntity(PSD);
            }

            _productServiceDetailRepository.UnitWork.Commit();
            return;
        }

        /// <summary>
        /// Función para guardar y actualizar las tablas detailPhysicalCUM y PharmaDose
        /// </summary>
        /// <param name="pharmaceuticalDispensingDetail"></param>
        /// <param name="pharmaceuticalDispensingId"></param>
        /// <param name="PatientCode"></param>
        /// <returns></returns>
        public ActionResult SaveDetailPhysicalCUMAndUpdatePharmaDose(PharmaceuticalDispensingDetail pharmaceuticalDispensingDetail, int pharmaceuticalDispensingId, string PatientCode)
        {
            IUnitWork unitOfWork = _detailPhysicalCUMRepository.UnitWork;

            try
            {
                if (pharmaceuticalDispensingId == 0)
                {
                    throw new Exception("Error en el detalle de la dispensación");
                }

                var _detailPhysicalCUM = new DetailPhysicalCUM();
                var _pharmaceuticalDispensing = _pharmaceuticalDispensingRepository
                    .GetByFilter(d => d.Id == pharmaceuticalDispensingId, false, new List<string> { 
                        "PharmaceuticalDispensingDetail.InventoryProduct.ATC", 
                        "PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory.BatchSerial" 
                    }).ToList().FirstOrDefault();
                var _pharmaceuticalDispensingDetail = _pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Where(j => j.ProductId == pharmaceuticalDispensingDetail.ProductId).ToList().FirstOrDefault();
                var fisiPro = _hCFISIPRORepository.Query(x => x.CODPRODUC == _pharmaceuticalDispensingDetail.InventoryProduct.ATC.Code 
                    && x.NUMINGRES == _pharmaceuticalDispensingDetail.PharmaceuticalDispensing.AdmissionNumber && x.IPCODPACI == PatientCode
                ).OrderByDescending(m => m.ID).FirstOrDefault();

                if (fisiPro == null) throw new Exception("Error en el detalle de la dispensación");

                var _pharmaceuticalDispensingDetailBatchSerial = _pharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.ToList();
                var ListBatchCode = _pharmaceuticalDispensingDetailBatchSerial.Select(r => r.PhysicalInventory.BatchSerial.BatchCode).ToList();
                var ListRequestDetailStatus = _requestPackageDetailStatusRepository.GetByFilter(f => ListBatchCode.Contains(f.BatchCode)).ToList();
                if (ListRequestDetailStatus is null || ListRequestDetailStatus.Count == 0 || ListRequestDetailStatus.All(o => o.GroupingCodeDose is null)) { throw new Exception("Error en el detalle de la dispensación"); };

                foreach (var Item in _pharmaceuticalDispensingDetailBatchSerial)
                {
                    var RequestDetailStatus = new RequestPackageDetailStatus();
                    var BatchCode = Item.PhysicalInventory.BatchSerial.BatchCode.ToString();
                    RequestDetailStatus = ListRequestDetailStatus.Where(j => j.BatchCode == BatchCode).ToList().FirstOrDefault();
                    if (RequestDetailStatus.GroupingCodeDose is null)
                    {
                        continue;
                    }
                    var IDHCFISIPRO = fisiPro.ID;
                    _detailPhysicalCUM = _detailPhysicalCUMRepository.GetByFilter(k => k.IDHCFISIPRO == IDHCFISIPRO && k.ProductId == _pharmaceuticalDispensingDetail.ProductId &&
                                                                                    k.GroupingCodeDose == RequestDetailStatus.GroupingCodeDose, true).FirstOrDefault();
                    if (_detailPhysicalCUM != null && _detailPhysicalCUM.Id > 0)
                    {
                        _detailPhysicalCUM.DispensedQuantity += Item.Quantity;
                    }
                    else
                    {
                        _detailPhysicalCUM = new DetailPhysicalCUM();
                        _detailPhysicalCUM.IDHCFISIPRO = fisiPro.ID;
                        _detailPhysicalCUM.BatchCode = RequestDetailStatus.BatchCode;
                        _detailPhysicalCUM.ProductId = _pharmaceuticalDispensingDetail.ProductId;
                        _detailPhysicalCUM.DateExpiration = _pharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.ToList().FirstOrDefault().PhysicalInventory.BatchSerial.ExpirationDate.Value;
                        _detailPhysicalCUM.Hour = DateTime.Now;
                        _detailPhysicalCUM.DispensedQuantity = Item.Quantity;
                        _detailPhysicalCUM.GroupingCodeDose = RequestDetailStatus.GroupingCodeDose;
                        _detailPhysicalCUM.MarkAsAdded();
                    }

                    _detailPhysicalCUMRepository.SaveEntity(_detailPhysicalCUM);
                }

                List<Guid> _listGroupingCodeDose = ListRequestDetailStatus.Select(u => u.GroupingCodeDose.Value).ToList();
                var _pharmaDose = _pharmaDoseRepository.GetByFilter(k => _listGroupingCodeDose.Contains(k.GroupingCodeDose)).ToList();

                if (_pharmaDose.Count > 0)
                {

                    foreach (var item in _pharmaDose)
                    {
                        item.DeliveryStatus = 1;
                        _pharmaDoseRepository.SaveEntity(item);
                    }

                }

                unitOfWork.Commit();

                return new ActionResult { StateResult = true };

            }
            catch (Exception ex)
            {
                return new ActionResult { StateResult = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Elimina una dispensación farmacéutica
        /// </summary>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">PharmaceuticalDispensing</exception>
        public ActionResult DeletePharmaceuticalDispensing(Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing, AuditMessage audit)
        {
            if (pharmaceuticalDispensing == null)
            {
                throw new ArgumentNullException("PharmaceuticalDispensing");
            }

            IUnitWork unitOfWork = _pharmaceuticalDispensingRepository.UnitWork;
            try
            {
                while (pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count > 0)
                {
                    PharmaceuticalDispensingDetail pharmaceuticDispensing = pharmaceuticalDispensing.PharmaceuticalDispensingDetail[pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count() - 1];
                    if (pharmaceuticDispensing.PharmaceuticalDispensingDetailBatchSerial != null && pharmaceuticDispensing.PharmaceuticalDispensingDetailBatchSerial.Count > 0)
                    {
                        while (pharmaceuticDispensing.PharmaceuticalDispensingDetailBatchSerial.Count > 0)
                        {
                            pharmaceuticDispensing.PharmaceuticalDispensingDetailBatchSerial[pharmaceuticDispensing.PharmaceuticalDispensingDetailBatchSerial.Count - 1].MarkAsDeleted();
                        }
                    }
                    pharmaceuticalDispensing.PharmaceuticalDispensingDetail.ElementAt(pharmaceuticalDispensing.PharmaceuticalDispensingDetail.Count - 1).MarkAsDeleted();
                }

                pharmaceuticalDispensing.MarkAsDeleted();
                _pharmaceuticalDispensingRepository.SaveEntity(pharmaceuticalDispensing);
                unitOfWork.Commit();
                IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensing> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensing>(pharmaceuticalDispensing, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                auditProcess.Execute();

                return new ActionResult { StateResult = true };
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (DbUpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, MessageResult = { IndigoManagementExceptions.GetExceptionDetails(ex) } };
            }
        }

        #endregion

        #region DashboardPharmacy

        public ActionResult<string> SaveDashboardPharmacy(List<Domain.Entities.PharmaceuticalDispensing> listPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> listDetailAnnular, AuditMessage audit, long idSecuence = 0)
        {
            try
            {
                var xmlDevolution = "";

                if (listPharmaceuticalDispensing != null)
                {
                    listPharmaceuticalDispensing.ForEach(m => m.Status = 2);
                    xmlDevolution = GenerateXmlDashboard(false, listPharmaceuticalDispensing, audit.DispensingIntegration, "SaveDashboardPharmacy");
                }

                var xmlAnnulation = GenerateXmlAnnulationDashboard(listDetailAnnular);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                }))
                {
                    var result = _pharmaceuticalDispensingRepository.GeneratePharmaceuticalDispensingSP(xmlDevolution, xmlAnnulation, audit.CodeUser).FirstOrDefault();

                    if (result != null)
                    {
                        var actionResultReturn = new ActionResult<string>() { Message = result.Message };

                        if (result.Status == 1)
                        {
                            string[] sparator = { " *USERINDIGO* " };
                            string[] splitMessage = null;

                            if (result.Message != null)
                            {
                                splitMessage = result.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);
                                actionResultReturn.Message = splitMessage.ElementAt(0);
                            }

                            actionResultReturn.StateResult = true;
                            actionResultReturn.StatusCode = eStatusResult.SUCCESS;
                            if (result.DispensingId > 0)
                            {
                                actionResultReturn.ObjectEmbbeded = result.DispensingCode;
                                actionResultReturn.MessageResult = new List<string> { splitMessage.ElementAt(1) }.ToList();
                            }
                            if (listPharmaceuticalDispensing == null)
                            {
                                actionResultReturn.Message = "Se anulo correctamente";
                            }
                            else
                            {
                                foreach (var g in listPharmaceuticalDispensing)
                                {
                                    foreach (var x in g.PharmaceuticalDispensingDetail.Where(m => m.ProductId > 0).ToList())
                                    {
                                        var _inventoryProduct = _inventoryProductRepository.GetByFilter(y => y.Id == x.ProductId, false, new List<string> { "ProductType" }).ToList().FirstOrDefault();

                                        if (x.ProductServiceDetail is null || x.ProductServiceDetail.Count == 0) continue;

                                        var PharmaceuticalDispensing = _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensingById(result.DispensingId);

                                        SaveProductServiceDetail(x.ProductServiceDetail.ToList(), PharmaceuticalDispensing, x.ProductId);
                                    }
                                }
                            }

                        }
                        else
                        {
                            scope.Dispose();
                            actionResultReturn.StateResult = false;
                            actionResultReturn.StatusCode = eStatusResult.WARNING;
                            return actionResultReturn;
                        }

                        scope.Complete();
                        return actionResultReturn;
                    }
                    else
                    {
                        return new ActionResult<string>() { StateResult = false, StatusCode = eStatusResult.WARNING };
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<string> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Genera la dispensacion para los paquetes Quirurgicos
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing">Listado de item dispensar</param>
        /// <param name="ListDetailAnnular">listado items a Anular</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<string> SaveDashboardPharmacySurgicalPackage(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashBoardPharmacy_SurgicalPackageDeatils> ListDetailAnnular, AuditMessage audit, long idSecuence = 0)
        {
            var xmlDispensingSurgicalPackage = "";
            if (ListPharmaceuticalDispensing != null)
            {
                foreach (Domain.Entities.PharmaceuticalDispensing item in ListPharmaceuticalDispensing)
                {
                    item.Status = 2;
                }
                xmlDispensingSurgicalPackage = GenerateXmlDashboard(false, ListPharmaceuticalDispensing, audit.DispensingIntegration, "SaveDashboardPharmacySurgicalPackage");
            }
            var xmlAnnulation = GenerateXmlAnnulationDashboardSurgicalPackage(ListDetailAnnular);
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var result = _pharmaceuticalDispensingRepository.GeneratePharmaceuticalDispensingSP(xmlDispensingSurgicalPackage, xmlAnnulation, audit.CodeUser).FirstOrDefault();
                    if (result != null)
                    {
                        ActionResult<string> actionResultReturn = new ActionResult<string>();
                        actionResultReturn.Message = result.Message;
                        if (result.Status == 1)
                        {
                            string[] sparator = { " *USERINDIGO* " };
                            string[] splitMessage = null;
                            if (result.Message != null)
                            {
                                splitMessage = result.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);
                                actionResultReturn.Message = splitMessage.ElementAt(0);
                            }
                            actionResultReturn.StateResult = true;
                            actionResultReturn.StatusCode = eStatusResult.SUCCESS;
                            if (result.DispensingId > 0)
                            {
                                //actionResultReturn.ObjectEmbbeded = _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensingById(ObjResult.DispensingId).Code;
                                actionResultReturn.ObjectEmbbeded = result.DispensingCode;
                                actionResultReturn.MessageResult = new List<string> { splitMessage.ElementAt(0) }.ToList();
                            }
                            if (ListPharmaceuticalDispensing == null)
                            {
                                actionResultReturn.Message = "Se anulo correctamente";
                            }
                        }
                        else
                        {
                            actionResultReturn.StateResult = false;
                            actionResultReturn.StatusCode = eStatusResult.WARNING;
                            return actionResultReturn;
                        }
                        scope.Complete();
                        return actionResultReturn;
                    }
                    else
                    {
                        return new ActionResult<string>() { StateResult = false, StatusCode = eStatusResult.WARNING };
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<string> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        public ActionResult<string> SaveDashboardCentralMix(List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, AuditMessage audit)
        {
            return null;
        }

        /// <summary>
        /// Metodo que guarda la dispensación por paciente Medilaser
        /// </summary>
        /// <returns></returns>
        public ActionResult<SP_SaveDispensingByPatientMedilaser_Result> SaveDispensingByPatientMedilaser(List<SP_ListHCPRESCRDByCODCONCEC_Result> ListCrystal, List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, List<ViewDashboardPharmacyDetail> ListDetailAnnular, string CareCenterCode, int WareHouseId, int CareGroupId, int BillingAuthorizationId, DateTime Date, string Number, AuditMessage audit, List<ViewDashboardPharmacyDetailDeferred> ListDeferred, bool IsManual, SessionValues session)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    int OperatingUnitId = 0;
                    var xmlPharmaceutical = "";
                    GeneralLedgerSettings settingsAccount = null;
                    BillingAuthorization billingAuthorization = null;
                    Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing = null;

                    if (ListPharmaceuticalDispensing != null && ListPharmaceuticalDispensing.Count > 0)
                    {
                        OperatingUnitId = ListPharmaceuticalDispensing[0].OperatingUnitId;

                        //Consulto los parametros de Contabilidad definidos para la unidad operativa
                        settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(OperatingUnitId);
                        if (settingsAccount == null || settingsAccount.Id == 0)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = "No se encontro parametros de contabilidad para la unidad operativa seleccionada" };
                        }
                        //Consulto la Autorizacion de Facturacion Asociada a la Factura de Producto
                        billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(BillingAuthorizationId);
                        if (billingAuthorization == null || billingAuthorization.Id == 0 || billingAuthorization.Status == false)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = "No se encontró la autorización de facturación o esta se encuentra inactiva" };
                        }
                        //Valido existencia de consecutivos
                        if (billingAuthorization.Consecutive == billingAuthorization.FinalInvoice)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = "La autorización de facturación " + billingAuthorization.Name + " llego al número maximo de consecutivos" };
                        }
                        //Valido que la Autorizacion de Facturacion se encuentre vigente
                        if (billingAuthorization.InitialDate != null && billingAuthorization.FinalDate != null && (DateTime.Now < billingAuthorization.InitialDate || DateTime.Now > billingAuthorization.FinalDate))
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = "La autorización de facturación " + billingAuthorization.Name + " no se encuentra vigente" };
                        }
                        //Valido que, si la facturacion electronica se encuentra habilitada, y la autorizacion es de tipo electronica, esta tenga asignado un codigo
                        if (settingsAccount.HandlesElectronicBilling == true && billingAuthorization.InvoiceType == 3 && String.IsNullOrEmpty(billingAuthorization.TechnicalKey))
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = "No se ha parametrizado la clave tecnica en la autorización de facturación " + billingAuthorization.Name };
                        }

                        foreach (Domain.Entities.PharmaceuticalDispensing item in ListPharmaceuticalDispensing)
                        {
                            item.Status = 2;
                        }
                        xmlPharmaceutical = GenerateXmlDashboard(false, ListPharmaceuticalDispensing, 3, "SaveDispensingByPatientMedilaser");
                        pharmaceuticalDispensing = ListPharmaceuticalDispensing[0];
                    }

                    var xmlAnnulation = GenerateXmlAnnulationDashboard(ListDetailAnnular);
                    var xmlPrescription = GenerateXmlPrescription(pharmaceuticalDispensing, ListCrystal, CareCenterCode, WareHouseId, CareGroupId, BillingAuthorizationId, Date, Number, ListDeferred, IsManual, session);

                    var resultProcess = _pharmaceuticalDispensingRepository.SP_SaveDispensingByPatientMedilaser(xmlPharmaceutical, xmlAnnulation, xmlPrescription, audit.CodeUser, OperatingUnitId);
                    if (resultProcess.Status > 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = resultProcess.Message };
                    }

                    //Consultamos las facturas y, si estas se encuentran habilitadas para la facturación electronica realizamos la actualización de los datos correspondientes
                    //Si se maneja facturación electrónica y la autorización de facturación es de tipo electrónica, se agregan los datos que debe incluir la factura
                    if (settingsAccount != null && billingAuthorization != null && settingsAccount.HandlesElectronicBilling == true && billingAuthorization.InvoiceType == 3)
                    {
                        //Consultamos si ya esta creado el documento electrónico de la factura
                        var document = _electronicDocumentRepository.GetElectronicDocumentByInvoiceId(resultProcess.InvoiceId);
                        if (document == null)
                        {
                            var invoice = _invoiceRepository.GetInvoiceById(resultProcess.InvoiceId);
                            //Verificamos que se haya asignado un numero de autorización, puesto que este solo se asigna si es pago por servicios (Liquidacion Tipo = 1)
                            if (invoice.BillingAuthorizationId != null)
                            {
                                var supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, false);
                                var customerThirdParty = _thirdPartyRepository.GetThirdPartyById(invoice.ThirdPartyId, false);

                                invoice.DianVersion = settingsAccount.DianVersion;
                                invoice.NitFE = supplierThirdParty.Person.IdentificationNumber;
                                invoice.TipAdq = customerThirdParty.Person.getAcquirerType();
                                invoice.NumAdq = customerThirdParty.Person.IdentificationNumber;
                                invoice.ClTec = billingAuthorization.TechnicalKey;
                                invoice.Environment = settingsAccount.Environment;

                                invoice.CUFE = invoice.getCUFE();
                                invoice.QR = invoice.GetQRCode();

                                _invoiceRepository.SaveEntity(invoice);

                                string documentNumber = invoice.InvoiceNumber;
                                if (!String.IsNullOrEmpty(billingAuthorization.InvoicePrefix))
                                {
                                    documentNumber = invoice.InvoiceNumber.Replace(billingAuthorization.InvoicePrefix, "");
                                }

                                var electronicDocument = new ElectronicDocument()
                                {
                                    DianVersion = settingsAccount.DianVersion,
                                    OperatingUnitId = invoice.OperatingUnitId,
                                    CustomerPartyId = customerThirdParty.Id,
                                    EntityId = invoice.Id,
                                    EntityName = invoice.GetType().Name,
                                    DocumentDate = invoice.InvoiceDate,
                                    DocumentType = invoice.DocumentType,
                                    Status = 1,
                                    CreationDate = DateTime.Now,
                                    Container = session.TransactionalContainer,
                                    Prefix = billingAuthorization.InvoicePrefix,
                                    DocumentNumber = documentNumber,
                                    CUFE = invoice.CUFE,
                                    Year = DateTime.Now.Year
                                };

                                var operatingUnit = _operatingUnitRepository.GetOperatingUnitById(invoice.OperatingUnitId);
                                electronicDocument.FilePath = System.IO.Path.Combine(
                                    Utils.GetPathElectronicDocuments(),
                                    electronicDocument.Container,
                                    operatingUnit.UnitCode,
                                    electronicDocument.DocumentDate.Year.ToString(),
                                    electronicDocument.DocumentDate.Month.ToString(),
                                    electronicDocument.getDocumentTypeName(),
                                    String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                                );

                                IUnitWork unitOfWorkElectronicDocument = _electronicDocumentRepository.UnitWork;
                                _electronicDocumentRepository.SaveEntity(electronicDocument);
                                unitOfWorkElectronicDocument.Commit();
                            }
                        }
                    }

                    string[] sparator = { " *USERINDIGO* " };
                    string[] splitMessage = null;
                    splitMessage = resultProcess.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);

                    scope.Complete();
                    return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { ObjectEmbbeded = resultProcess, StatusCode = eStatusResult.SUCCESS, StateResult = true, Message = splitMessage.ElementAt(0) };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                    return new ActionResult<Domain.Entities.SP_SaveDispensingByPatientMedilaser_Result> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _pharmaceuticalDispensingRepository.CascadeRollback(value);
            return await Task.FromResult(res);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// metodo para convertir los items del dasboard en xml
        /// </summary>
        /// <param name="ListPharmaceuticalDispensing"></param>
        /// /// <param name="isPharmaceuticalDispensing">Indica si se esta ejecutando desde dispensacion farmaceutica o no</param>
        /// <returns></returns>
        private string GenerateXmlDashboard(bool isPharmaceuticalDispensing, List<Domain.Entities.PharmaceuticalDispensing> ListPharmaceuticalDispensing, Byte DispensingIntegration = 1, String EntityName = null)
        {
            StringBuilder result = new StringBuilder();
            if (ListPharmaceuticalDispensing == null)
            {
                return "";
            }
            foreach (var item in ListPharmaceuticalDispensing)
            {
                result.AppendLine(item.ToXML(isPharmaceuticalDispensing, DispensingIntegration, EntityName));
            }
            return result.ToString();
        }

        /// <summary>
        /// metodo para convertir los items de la anulacion en xml
        /// </summary>
        /// <param name="ListDetailAnnular"></param>
        /// <returns></returns>
        private string GenerateXmlAnnulationDashboard(List<Domain.Crystal.Entities.ViewDashboardPharmacyDetail> ListDetailAnnular)
        {
            if (ListDetailAnnular == null || ListDetailAnnular.Count == 0)
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            result.Append("<Main>");
            foreach (var item in ListDetailAnnular)
            {
                result.AppendLine(item.ToXML());
            }
            result.Append("</Main>");
            return result.ToString();
        }

        /// <summary>
        /// metodo para convertir los items de la anulacion en xml
        /// </summary>
        /// <param name="ListDetailAnnular"></param>
        /// <returns></returns>
        private string GenerateXmlAnnulationDashboardSurgicalPackage(List<Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils> ListDetailAnnular)
        {
            if (ListDetailAnnular == null || ListDetailAnnular.Count == 0)
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            result.Append("<Main>");
            foreach (var item in ListDetailAnnular)
            {
                result.AppendLine(item.ToXML());
            }
            result.Append("</Main>");
            return result.ToString();
        }

        /// <summary>
        /// Metodo para convertir los items en un xml
        /// </summary>
        /// <param name="ListCrystal"></param>
        /// <returns></returns>
        private string GenerateXmlPrescription(Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing, List<SP_ListHCPRESCRDByCODCONCEC_Result> ListCrystal, string CareCenterCode, int WareHouseId, int CareGroupId, int BillingAuthorizationId, DateTime Date, string Number, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred> ListDeferred, bool IsManual, SessionValues session)
        {
            if (ListCrystal == null || ListCrystal.Count() == 0)
            {
                return "";
            }

            PharmaceuticalDispensing = UnifedMedicine(PharmaceuticalDispensing);

            StringBuilder result = new StringBuilder();

            result.Append("<PrescriptionHeader>");

            result.Append("<UFUCODIGO>" + ListCrystal[0].UFUCODIGO + "</UFUCODIGO>");
            result.Append("<UFUDESCRI>" + ListCrystal[0].UFUDESCRI + "</UFUDESCRI>");
            result.Append("<CODCENATE>" + ListCrystal[0].CODCENATE + "</CODCENATE>");
            result.Append("<NUMINGRES>" + ListCrystal[0].NUMINGRES + "</NUMINGRES>");
            result.Append("<IPCODPACI>" + ListCrystal[0].IPCODPACI + "</IPCODPACI>");
            result.Append("<IPTIPODOC>" + ListCrystal[0].IPTIPODOC.ToString() + "</IPTIPODOC>");
            result.Append("<CODIGONIT>" + ListCrystal[0].CODIGONIT + "</CODIGONIT>");
            result.Append("<IPEXPEDIC>" + ListCrystal[0].IPEXPEDIC + "</IPEXPEDIC>");
            result.Append("<IPPRIAPEL>" + ListCrystal[0].IPPRIAPEL + "</IPPRIAPEL>");
            result.Append("<IPSEGAPEL>" + ListCrystal[0].IPSEGAPEL + "</IPSEGAPEL>");
            result.Append("<IPPRINOMB>" + ListCrystal[0].IPPRINOMB + "</IPPRINOMB>");
            result.Append("<IPSEGNOMB>" + ListCrystal[0].IPSEGNOMB + "</IPSEGNOMB>");
            result.Append("<IPNOMCOMP>" + ListCrystal[0].IPNOMCOMP + "</IPNOMCOMP>");
            result.Append("<CODEMPRES>" + ListCrystal[0].CODEMPRES + "</CODEMPRES>");
            result.Append("<IPTIPOPAC>" + ListCrystal[0].IPTIPOPAC.ToString() + "</IPTIPOPAC>");
            result.Append("<IPTIPOAFI>" + ListCrystal[0].IPTIPOAFI.ToString() + "</IPTIPOAFI>");
            result.Append("<CAPACIPAG>" + ListCrystal[0].CAPACIPAG.ToString() + "</CAPACIPAG>");
            result.Append("<AUUBICACI>" + ListCrystal[0].AUUBICACI + "</AUUBICACI>");
            result.Append("<NIVCODIGO>" + ListCrystal[0].NIVCODIGO + "</NIVCODIGO>");
            result.Append("<IPDIRECCI>" + ListCrystal[0].IPDIRECCI + "</IPDIRECCI>");
            result.Append("<IPTELEFON>" + ListCrystal[0].IPTELEFON + "</IPTELEFON>");
            result.Append("<IPTELMOVI>" + ListCrystal[0].IPTELMOVI + "</IPTELMOVI>");
            result.Append("<IPFECNACI>" + ListCrystal[0].IPFECNACI.Year.ToString() + "-" + ListCrystal[0].IPFECNACI.Day.ToString() + "-" + ListCrystal[0].IPFECNACI.Month.ToString() + "</IPFECNACI>");
            result.Append("<CODACTIVI>" + ListCrystal[0].CODACTIVI + "</CODACTIVI>");
            result.Append("<IPSEXOPAC>" + ListCrystal[0].IPSEXOPAC.ToString() + "</IPSEXOPAC>");
            result.Append("<IPESTADOC>" + ListCrystal[0].IPESTADOC.ToString() + "</IPESTADOC>");
            result.Append("<TIPCOBSAL>" + ListCrystal[0].TIPCOBSAL + "</TIPCOBSAL>");
            result.Append("<ESTADOPAC>" + ListCrystal[0].ESTADOPAC + "</ESTADOPAC>");
            result.Append("<INDAUDFOR>" + ListCrystal[0].INDAUDFOR + "</INDAUDFOR>");
            result.Append("<NUMCARPET>" + ListCrystal[0].NUMCARPET + "</NUMCARPET>");
            result.Append("<CODUSUCRE>" + ListCrystal[0].CODUSUCRE + "</CODUSUCRE>");
            result.Append("<IPSCode>" + ListCrystal[0].IPSCode + "</IPSCode>");
            result.Append("<PerformsHealthProfessionalThirdPartyId>" + ListCrystal[0].PerformsHealthProfessionalThirdPartyId + "</PerformsHealthProfessionalThirdPartyId>");

            if (ListCrystal[0].DateFormulation != null)
            {
                result.Append("<DateFormulation>" + ListCrystal[0].DateFormulation.Value.ToString("yyyy-MM-dd") + "</DateFormulation>");
            }

            if (ListCrystal[0].FECREGCRE != null)
            {
                result.Append("<FECREGCRE>" + ListCrystal[0].FECREGCRE.Value.Year.ToString() + "-" + ListCrystal[0].FECREGCRE.Value.Day.ToString() + "-" + ListCrystal[0].FECREGCRE.Value.Month.ToString() + "</FECREGCRE>");
            }
            else
            {
                result.Append("<FECREGCRE>" + "" + "</FECREGCRE>");
            }

            if (ListCrystal[0].GENCAREGROUP != null)
            {
                result.Append("<GENCAREGROUP>" + ListCrystal[0].GENCAREGROUP.ToString() + "</GENCAREGROUP>");
            }
            else
            {
                result.Append("<GENCAREGROUP>" + 0 + "</GENCAREGROUP>");
            }

            if (ListCrystal[0].GENCONENTITY != null)
            {
                result.Append("<GENCONENTITY>" + ListCrystal[0].GENCONENTITY.ToString() + "</GENCONENTITY>");
            }
            else
            {
                result.Append("<GENCONENTITY>" + 0 + "</GENCONENTITY>");
            }

            result.Append("<CareCenterCode>" + CareCenterCode + "</CareCenterCode>");
            result.Append("<WareHouseId>" + WareHouseId.ToString() + "</WareHouseId>");
            result.Append("<CareGroupId>" + CareGroupId.ToString() + "</CareGroupId>");
            result.Append("<BillingAuthorizationId>" + BillingAuthorizationId.ToString() + "</BillingAuthorizationId>");
            result.Append("<Date>" + Date.ToString("yyyy-MM-dd") +"</Date>");
            result.Append("<Number>" + Number + "</Number>");
            result.Append("<IsManual>" + IsManual + "</IsManual>");
            result.Append($"<FilePath>{System.IO.Path.Combine(Utils.GetPathElectronicDocuments(), session.TransactionalContainer)}</FilePath>");

            var Dictionary = new Dictionary<string, bool>();
            foreach (var detail in ListCrystal)
            {
                if (Dictionary.ContainsKey(detail.CODPRODUC))
                {
                    continue;
                }
                Dictionary.Add(detail.CODPRODUC, true);
                result.Append("<PrescriptionDetail>");
                result.Append("<AdmissionNumber>" + detail.NUMINGRES + "</AdmissionNumber>");

                if (detail.GENCONENTITY != null)
                {
                    result.Append("<EntityId>" + detail.GENCONENTITY.ToString() + "</EntityId>");
                }
                else
                {
                    result.Append("<EntityId>" + 0 + "</EntityId>");
                }

                if (detail.GENCONENTITY != null)
                {
                    result.Append("<EntityCode>" + detail.GENCONENTITY.ToString() + "</EntityCode>");
                }
                else
                {
                    result.Append("<EntityCode>" + "" + "</EntityCode>");
                }

                Domain.Entities.PharmaceuticalDispensingDetail pharmaceuticalDispensingDetail = null;
                if (PharmaceuticalDispensing != null)
                {
                    pharmaceuticalDispensingDetail = (from x in PharmaceuticalDispensing.PharmaceuticalDispensingDetail where x.MedicamentCode == detail.CODPRODUC.Trim() select x).FirstOrDefault();
                }

                result.Append("<ContractCode>" + detail.CODCONTRA + "</ContractCode>");
                result.Append("<PlanCode>" + detail.CODPANATE + "</PlanCode>");
                result.Append("<ProductCode>" + detail.CODPRODUC + "</ProductCode>");
                result.Append("<ProductName>" + detail.DESPRODUC + "</ProductName>");
                result.Append("<ProductType>" + detail.TIPPRODUC + "</ProductType>");
                result.Append("<TreatmentDays>" + detail.TreatmentDays + "</TreatmentDays>");
                result.Append("<DiagnosticCode>" + detail.DiagnosticCode + "</DiagnosticCode>");
                result.Append("<AuthorizationNumber>" + detail.AuthorizationNumber + "</AuthorizationNumber>");
                result.Append("<IDMipres>" + detail.IDMipres + "</IDMipres>");

                if (pharmaceuticalDispensingDetail != null)
                {
                    result.Append("<RequestQuantity>" + pharmaceuticalDispensingDetail.CantidadSolicitada + "</RequestQuantity>");
                    result.Append("<DeliveryQuantity>" + pharmaceuticalDispensingDetail.Quantity + "</DeliveryQuantity>");
                    result.Append("<PendingQuantity>" + (pharmaceuticalDispensingDetail.CantidadSolicitada - pharmaceuticalDispensingDetail.Quantity) + "</PendingQuantity>");
                    

                    foreach (var item in pharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial)
                    {
                        result.Append("<PrescriptionDetailBatchSerial>");
                        result.Append("<ProductCode>" + pharmaceuticalDispensingDetail.MedicamentCode + "</ProductCode>");
                        result.Append("<ProductId>" + item.ProductId + "</ProductId>");
                        result.Append("<BatchSerialId>" + item.BatchSerialId + "</BatchSerialId>");
                        result.Append("<WarehouseId>" + item.IdWarehouse + "</WarehouseId>");
                        result.Append("<Quantity>" + item.Quantity + "</Quantity>");
                        result.Append("<CreationUser>" + session.UserIndigo + "</CreationUser>");
                        result.Append("</PrescriptionDetailBatchSerial>");
                    }
                }
                else
                {
                    result.Append("<RequestQuantity>" + detail.CANPEDPRO.ToString() + "</RequestQuantity>");
                    result.Append("<DeliveryQuantity>" + 0 + "</DeliveryQuantity>");
                    result.Append("<PendingQuantity>" + detail.CANPEDPRO.ToString() + "</PendingQuantity>");
                }

                result.Append("<NoPos>" + detail.NOPOSPROD + "</NoPos>");
                result.Append("<MeasurementUnitCode>" + detail.CODUNIMED + "</MeasurementUnitCode>");
                result.Append("<HealthProfessionalCode>" + detail.CODPROSAL + "</HealthProfessionalCode>");
                result.Append("<HealthProfessionalName>" + detail.NOMMEDICO + "</HealthProfessionalName>");
                result.Append("<HealthProfessionalNit>" + detail.CODIGONITPROFSAL + "</HealthProfessionalNit>");
                result.Append("<IdeTipHis>" + detail.IDETIPHIS + "</IdeTipHis>");
                result.Append("<NumFolio>" + detail.NUMEFOLIO + "</NumFolio>");
                result.Append("<PatientCode>" + detail.IPCODPACI + "</PatientCode>");
                result.Append("<SpecialtyCode>" + detail.CODESPEC1 + "</SpecialtyCode>");
                result.Append("<SpecialtyName>" + detail.DESESPECI + "</SpecialtyName>");

                if (ListDeferred != null && ListDeferred.Count > 0 && (from x in ListDeferred where x.ProductCode.Trim() == detail.CODPRODUC.Trim() select x).Count() > 0)
                {
                    result.Append("<IsDeferred>1</IsDeferred>");

                    int tempQuantity = 0;
                    if (pharmaceuticalDispensingDetail != null)
                    {
                        tempQuantity = pharmaceuticalDispensingDetail.Quantity;
                    }

                    var listTemp = (from y in ListDeferred where y.ProductCode.Trim() == detail.CODPRODUC.Trim() select y).ToList();
                    foreach (var itemDeferred in listTemp)
                    {
                        result.Append("<PrescriptionDetailDeferred>");
                        result.Append("<Id>" + itemDeferred.Id + "</Id>");
                        result.Append("<FirstDeliveryDate>" + itemDeferred.FirstDeliveryDate.Year.ToString() + "-" + itemDeferred.FirstDeliveryDate.Day.ToString() + "-" + itemDeferred.FirstDeliveryDate.Month.ToString() + "</FirstDeliveryDate>");
                        result.Append("<DeliveryQuantityDeferred>" + itemDeferred.DeliveryQuantityDeferred + "</DeliveryQuantityDeferred>");
                        result.Append("<Periodicity>" + itemDeferred.Periodicity + "</Periodicity>");
                        result.Append("<ProductCode>" + itemDeferred.ProductCode + "</ProductCode>");
                        result.Append("<Number>" + itemDeferred.Number + "</Number>");
                        result.Append("<DeliveryDate>" + itemDeferred.DeliveryDate.Year.ToString() + "-" + itemDeferred.DeliveryDate.Day.ToString() + "-" + itemDeferred.DeliveryDate.Month.ToString() + "</DeliveryDate>");
                        result.Append("<DeliveryQuantity>" + itemDeferred.DeliveryQuantity + "</DeliveryQuantity>");

                        if (itemDeferred.DeliveryDate <= DateTime.Now)
                        {
                            if (tempQuantity > itemDeferred.PendingQuantity)
                            {
                                tempQuantity = tempQuantity - itemDeferred.PendingQuantity;
                                itemDeferred.PendingQuantity = 0;
                            }
                            else
                            {
                                itemDeferred.PendingQuantity = itemDeferred.PendingQuantity - tempQuantity;
                                tempQuantity = 0;
                            }
                        }

                        result.Append("<PendingQuantity>" + itemDeferred.PendingQuantity + "</PendingQuantity>");

                        result.Append("</PrescriptionDetailDeferred>");
                    }

                }
                else
                {
                    result.Append("<IsDeferred>0</IsDeferred>");
                }

                result.Append("<ProductId>" + (pharmaceuticalDispensingDetail?.ProductId is null ? detail.ProductId : pharmaceuticalDispensingDetail?.ProductId) + "</ProductId>");
                result.Append("</PrescriptionDetail>");
            }

            result.Append("</PrescriptionHeader>");

            return result.ToString();
        }

        #endregion

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
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                    _accountingAdminService.Dispose();
                    _consignmentInventoryRemissionDetailBatchSerialAdminService.Dispose();
                }
                _healthProfessionalRepository = null;
                _pharmaceuticalDispensingRepository = null;
                _sequenseRepository = null;
                _admissionRepository = null;
                _functionalUnitRepository = null;
                _physicalInventoryAdminService = null;
                _inventoryServices = null;
                _accountingAdminService = null;
                _settingInventoryRepository = null;
                _productGroupRepository = null;
                _inventoryProductRepository = null;
                _wareHouseRepository = null;
                _kardexCrystalRepository = null;
                _consecutiveRepository = null;
                _pharmacyDetailRepository = null;
                _pharmacyRepository = null;
                _physicalInventoryRepository = null;
                _hospitalInventoryRepository = null;
                _medicalPrescriptionRepository = null;
                _physicalInventoryCrystalRepository = null;
                _InventoryControlDocumentRepository = null;
                _stayRepository = null;
                _serviceOrderRepository = null;
                _accountingDocumentRepository = null;
                _pharmaceuticalDispensingDetailRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialAdminService = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }



        class GroupPharmaceuticalDetail
        {
            public int CantidadSolicitada;
            public int Quantity;
            public int CantidadPendiente;
            public string MedicamentCode;
            public string ProductType;
            public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> BatchSerial;
        }

        /// <summary>
        /// Unificar medicamentos
        /// </summary>
        /// <param name="PharmaceuticalDispensing"></param>
        /// <returns></returns>
        private Domain.Entities.PharmaceuticalDispensing UnifedMedicine(Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing)
        {
            if (PharmaceuticalDispensing == null) { return null; }
            var ListMedicine = PharmaceuticalDispensing.PharmaceuticalDispensingDetail.GroupBy(x => x.MedicamentCode).Where(g => g.Count() > 1).Select(x => x.Key).ToList();
            List<PharmaceuticalDispensingDetail> deleteDetails = (from PharmaceuticalDispensingDetail e in PharmaceuticalDispensing.PharmaceuticalDispensingDetail where ListMedicine.Contains(e.MedicamentCode) select e).ToList();
            //Remove Duplicate
            foreach (var item in deleteDetails)
            {
                PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Remove(item);
            }

            foreach (var code in ListMedicine)
            {
                var newDetail = new PharmaceuticalDispensingDetail();
                var groupData = (from e
                           in deleteDetails
                                 where e.MedicamentCode == code
                                 group e by e.MedicamentCode into x
                                 select new GroupPharmaceuticalDetail
                                 {
                                     CantidadSolicitada = x.Sum(xy => xy.CantidadSolicitada),
                                     Quantity = x.Sum(xy => xy.Quantity),
                                     CantidadPendiente = x.Sum(xy => xy.CantidadPendiente),
                                     MedicamentCode = x.FirstOrDefault().MedicamentCode,
                                     ProductType = x.FirstOrDefault().ProductType,
                                     BatchSerial = x.SelectMany(detail => detail.PharmaceuticalDispensingDetailBatchSerial).ToList()
                                 }
                           ).SingleOrDefault();

                newDetail.CantidadSolicitada = groupData.CantidadSolicitada;
                newDetail.Quantity = groupData.Quantity;
                newDetail.CantidadPendiente = groupData.CantidadPendiente;
                newDetail.MedicamentCode = groupData.MedicamentCode;
                newDetail.ProductType = groupData.ProductType;

                foreach (var item in groupData.BatchSerial)
                {
                    newDetail.PharmaceuticalDispensingDetailBatchSerial.Add(item);
                }
                PharmaceuticalDispensing.PharmaceuticalDispensingDetail.Add(newDetail);
            }
            return PharmaceuticalDispensing;
        }
        #endregion
    }
}