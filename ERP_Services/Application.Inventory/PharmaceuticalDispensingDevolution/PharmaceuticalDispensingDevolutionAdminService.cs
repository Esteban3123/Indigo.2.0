///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Carlos Ernesto Cordoba
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Resources;
using System.Data;
using Application.Base;
using Application.Inventory.PhysicalInventory;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Text;
using Domain.Entities.Service;
using System.Linq;
using Application.Accounting;
using Domain.Crystal.Entities;
using Domain.Crystal;
using Infrastructure.CrossCutting.Interface;
using System.Configuration;
using System.Xml.Serialization;
using System.Web;
using System.Data.Entity.Infrastructure;
using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using System.Threading.Tasks;

namespace Application.Inventory.PharmaceuticalDispensingDevolution
{
    public class PharmaceuticalDispensingDevolutionAdminService : IPharmaceuticalDispensingDevolutionAdminService
    {
        #region Fields
        private IPharmaceuticalDispensingDevolutionRepository _pharmaceuticalDispensingDevolutionRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IRevenueControlDetailRepository _revenueControlDetailRepository;
        private IServiceOrderRepository _serviceOrderRepository;
        private IServiceOrderDetailRepository _serviceOrderDetailRepository;
        private IInventoryService _inventoryService;
        private IServiceOrderDetailDistributionRepository _serviceOrderDetailDistributionRepository;
        private IAccountingDocumentAdminService _accountingAdminService;
        private IBillingServices _billingService;
        private IPharmaceuticalDispensingDetailRepository _pharmaceuticalDispensingDetailRepository;
        private IPharmaceuticalDispensingDetailBatchSerialRepository _pharmaceuticalDispensingDetailBatchSerialRepository;
        private Domain.Crystal.IConsecutiveRepository _consecutiveRepository;
        private IKardexCrystalRepository _kardexCrystalRepository;
        private IPhysicalInventoryCrystalRepository _physicalInventoryCrystalRepository;
        private IStayRepository _stayRepository;
        private IDevolutionMedicationRepository _devolutionMedicationRepository;
        private IDevolutionMedicationDetailRepository _devolutionMedicationDetailRepository;
        private IBedRepository _bedRepository;
        private IAdmissionRepository _admissionRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IWarehouseRepository _warehouseRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialAdminService _consignmentInventoryRemissionDetailBatchSerialAdminService;
        #endregion

        #region Builder
        public PharmaceuticalDispensingDevolutionAdminService(IPharmaceuticalDispensingDevolutionRepository pharmaceuticalDispensingDevolutionRepository, IInventorySequenceDetailRepository sequenseRepository,
            ISettingInventoryRepository settingInventoryRepository, IRevenueControlDetailRepository revenueControlDetailRepository, IServiceOrderRepository serviceOrderRepository,
            IServiceOrderDetailRepository serviceOrderDetailRepository, IInventoryService inventoryService, IServiceOrderDetailDistributionRepository serviceOrderDetailDistributionRepository,
            IAccountingDocumentAdminService accountingAdminService, IBillingServices billingService, IPharmaceuticalDispensingDetailRepository pharmaceuticalDispensingDetailRepository,
            IPharmaceuticalDispensingDetailBatchSerialRepository pharmaceuticalDispensingDetailBatchSerialRepository, Domain.Crystal.IConsecutiveRepository consecutiveRepository,
            IKardexCrystalRepository kardexCrystalRepository, IPhysicalInventoryCrystalRepository physicalInventoryCrystalRepository, IStayRepository stayRepository,
            IDevolutionMedicationRepository devolutionMedicationRepository, IDevolutionMedicationDetailRepository devolutionMedicationDetailRepository, IBedRepository bedRepository,
            IAdmissionRepository admissionRepository, IInventoryControlDocumentRepository InventoryControlDocumentRepository, IPhysicalInventoryAdminService physicalInventoryAdminService,
            IPhysicalInventoryRepository physicalInventoryRepository, IWarehouseRepository warehouseRepository,
            IConsignmentInventoryRemissionDetailBatchSerialAdminService consignmentInventoryRemissionDetailBatchSerialAdminService)
        {
            if (admissionRepository == null)
            {
                throw new ArgumentNullException("admissionRepository");
            }
            if (bedRepository == null)
            {
                throw new ArgumentNullException("bedRepository");
            }
            if (devolutionMedicationRepository == null)
            {
                throw new ArgumentNullException("devolutionMedicationRepository");
            }
            if (devolutionMedicationDetailRepository == null)
            {
                throw new ArgumentNullException("devolutionMedicationDetailRepository");
            }
            if (stayRepository == null)
            {
                throw new ArgumentNullException("stayRepository");
            }
            if (physicalInventoryCrystalRepository == null)
            {
                throw new ArgumentNullException("physicalInventoryCrystalRepository");
            }
            if (kardexCrystalRepository == null)
            {
                throw new ArgumentNullException("kardexCrystalRepository");
            }
            if (consecutiveRepository == null)
            {
                throw new ArgumentNullException("consecutiveRepository");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("sequenseRepository");
            }
            if (pharmaceuticalDispensingDevolutionRepository == null)
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDevolutionRepository");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("settingInventoryRepository");
            }
            if (revenueControlDetailRepository == null)
            {
                throw new ArgumentNullException("revenueControlDetailRepository");
            }
            if (serviceOrderRepository == null)
            {
                throw new ArgumentNullException("serviceOrderRepository");
            }
            if (serviceOrderDetailRepository == null)
            {
                throw new ArgumentNullException("serviceOrderDetailRepository");
            }
            if (serviceOrderDetailDistributionRepository == null)
            {
                throw new ArgumentNullException("serviceOrderDetailDistributionRepository");
            }
            if (accountingAdminService == null)
            {
                throw new ArgumentNullException("accountingAdminService");
            }
            if (billingService == null)
            {
                throw new ArgumentNullException("billingService");
            }
            if (pharmaceuticalDispensingDetailRepository == null)
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDetailRepository");
            }
            if (pharmaceuticalDispensingDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDetailBatchSerialRepository");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            _pharmaceuticalDispensingDetailRepository = pharmaceuticalDispensingDetailRepository;
            _pharmaceuticalDispensingDetailBatchSerialRepository = pharmaceuticalDispensingDetailBatchSerialRepository;
            _billingService = billingService;
            _accountingAdminService = accountingAdminService;
            _pharmaceuticalDispensingDevolutionRepository = pharmaceuticalDispensingDevolutionRepository;
            _sequenseRepository = sequenseRepository;
            _settingInventoryRepository = settingInventoryRepository;
            _revenueControlDetailRepository = revenueControlDetailRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _serviceOrderDetailRepository = serviceOrderDetailRepository;
            _inventoryService = inventoryService;
            _serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository;
            _consecutiveRepository = consecutiveRepository;
            _kardexCrystalRepository = kardexCrystalRepository;
            _physicalInventoryCrystalRepository = physicalInventoryCrystalRepository;
            _stayRepository = stayRepository;
            _devolutionMedicationRepository = devolutionMedicationRepository;
            _devolutionMedicationDetailRepository = devolutionMedicationDetailRepository;
            _bedRepository = bedRepository;
            _admissionRepository = admissionRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _physicalInventoryRepository = physicalInventoryRepository;
            _warehouseRepository = warehouseRepository;
            _consignmentInventoryRemissionDetailBatchSerialAdminService = consignmentInventoryRemissionDetailBatchSerialAdminService;
        }
        #endregion

        /// <summary>
        /// obtiene una devolucion por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionById(int id)
        {
            try
            {
                return _pharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PharmaceuticalDispensingDevolution();
            }
        }

        /// <summary>
        /// obtiene una devolucion por codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="audit">The audit.</param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingDevolution GetPharmaceuticalDispensingDevolutionByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution = _pharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionByCode(code.Trim());
                if (pharmaceuticalDispensingDevolution != null && pharmaceuticalDispensingDevolution.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensingDevolution> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensingDevolution>(pharmaceuticalDispensingDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return pharmaceuticalDispensingDevolution;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PharmaceuticalDispensingDevolution();
            }
        }

        private string GenerateXmlDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, bool isDashBoard = false)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<PharmaceuticalDispensingDevolution>");
            result.Append("<IsDashBoard>" + isDashBoard.ToString() + "</IsDashBoard>");
            result.Append("<Id>" + pharmaceuticalDispensingDevolution.Id.ToString() + "</Id>");
            if (pharmaceuticalDispensingDevolution.Code != null && pharmaceuticalDispensingDevolution.Code != string.Empty)
            {
                result.Append("<Code>" + pharmaceuticalDispensingDevolution.Code + "</Code>");
            }
            result.Append("<OperatingUnitId>" + pharmaceuticalDispensingDevolution.OperatingUnitId.ToString() + "</OperatingUnitId>");
            result.Append("<DocumentDate>" + pharmaceuticalDispensingDevolution.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss") + "</DocumentDate>");
            result.Append("<WarehouseId>" + pharmaceuticalDispensingDevolution.WarehouseId.ToString() + "</WarehouseId>");
            if (pharmaceuticalDispensingDevolution.CodeNameWarehouse != null)
            {
                result.Append("<CodeNameWarehouse>" + pharmaceuticalDispensingDevolution.CodeNameWarehouse + "</CodeNameWarehouse>");
            }
            if (pharmaceuticalDispensingDevolution.CodePatient != null)
            {
                result.Append("<CodePatient>" + pharmaceuticalDispensingDevolution.CodePatient + "</CodePatient>");
            }
            if (pharmaceuticalDispensingDevolution.CareCenterCode != null)
            {
                result.Append("<CareCenterCode>" + pharmaceuticalDispensingDevolution.CareCenterCode + "</CareCenterCode>");
            }
            if (pharmaceuticalDispensingDevolution.FunctionUnitCode != null)
            {
                result.Append("<FunctionUnitCode>" + pharmaceuticalDispensingDevolution.FunctionUnitCode + "</FunctionUnitCode>");
            }
            if (pharmaceuticalDispensingDevolution.ConsecutiveCrystal > 0)
            {
                result.Append("<ConsecutiveCrystal>" + pharmaceuticalDispensingDevolution.ConsecutiveCrystal.ToString() + "</ConsecutiveCrystal>");
            }
            if (pharmaceuticalDispensingDevolution.DevolutionOrigin != null)
            {
                result.Append("<DevolutionOrigin>" + pharmaceuticalDispensingDevolution.DevolutionOrigin + "</DevolutionOrigin>");
            }
            if (pharmaceuticalDispensingDevolution.Prefix != null)
            {
                result.Append("<Prefix>" + pharmaceuticalDispensingDevolution.Prefix + "</Prefix>");
            }
            result.Append("<AdmissionNumber>" + pharmaceuticalDispensingDevolution.AdmissionNumber + "</AdmissionNumber>");
            if (pharmaceuticalDispensingDevolution.Observation != null)
            {
                result.Append("<Observation>" + pharmaceuticalDispensingDevolution.Observation + "</Observation>");
            }
            result.Append("<Status>" + pharmaceuticalDispensingDevolution.Status.ToString() + "</Status>");


            foreach (var detail in pharmaceuticalDispensingDevolution.PharmaceuticalDispensingDevolutionDetail)
            {
                result.Append("<PharmaceuticalDispensingDevolutionDetail>");
                result.Append("<Id>" + detail.Id.ToString() + "</Id>");
                if (detail.CodeProduct != null)
                {
                    result.Append("<CodeProduct>" + detail.CodeProduct + "</CodeProduct>");
                }
                if (detail.CodePharmaceuticalDispensing != null)
                {
                    result.Append("<CodePharmaceuticalDispensing>" + detail.CodePharmaceuticalDispensing + "</CodePharmaceuticalDispensing>");
                }
                if (detail.PharmaceuticalDispensingDetailId > 0)
                {
                    result.Append("<PharmaceuticalDispensingDetailId>" + detail.PharmaceuticalDispensingDetailId.ToString() + "</PharmaceuticalDispensingDetailId>");
                }
                if (detail.ProductId > 0)
                {
                    result.Append("<ProductId>" + detail.ProductId.ToString() + "</ProductId>");
                }
                if (detail.CodeNameProduct != null)
                {
                    result.Append("<CodeNameProduct>" + detail.CodeNameProduct.CleanSpecialChars() + "</CodeNameProduct>");
                }
                if (detail.OrderedHealthProfessionalCode != null)
                {
                    result.Append("<OrderedHealthProfessionalCode>" + detail.OrderedHealthProfessionalCode + "</OrderedHealthProfessionalCode>");
                }
                result.Append("<PharmaceuticalDispensingDetailBatchSerialId>" + detail.PharmaceuticalDispensingDetailBatchSerialId.ToString() + "</PharmaceuticalDispensingDetailBatchSerialId>");
                result.Append("<Quantity>" + detail.Quantity.ToString() + "</Quantity>");
                result.Append("<EntityState>" + detail.ChangeTracker.State.ToString() + "</EntityState>");
                result.Append("<EntityId>" + detail.EntityId + "</EntityId>");
                result.Append("<EntityName>" + detail.EntityName + "</EntityName>");
                result.Append("<HCDEVMEDDId>" + detail.HCDEVMEDDId + "</HCDEVMEDDId>");
                result.Append("</PharmaceuticalDispensingDevolutionDetail>");

            }

            result.Append("</PharmaceuticalDispensingDevolution>");

            return result.ToString();
        }

        /// <summary>
        /// metodo para convertir los items de la anulacion en xml
        /// </summary>
        /// <param name="ListDetailAnnular"></param>
        /// <returns></returns>
        private string GenerateXmlDevolutionAnnulation(List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution> ListDetailAnnular)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in ListDetailAnnular)
            {
                result.Append("<ViewDashboardPharmacyDetailDevolution>");
                result.Append("<Consecutivo>" + item.Consecutivo.ToString() + "</Consecutivo>");
                if (item.CodigoPacienteDevolucion != null)
                {
                    result.Append("<CodigoPacienteDevolucion>" + item.CodigoPacienteDevolucion + "</CodigoPacienteDevolucion>");
                }

                result.Append("<Ingreso>" + item.Ingreso + "</Ingreso>");
                result.Append("<CODCENATE>" + item.CODCENATE + "</CODCENATE>");
                result.Append("<UFUCODIGO>" + item.UFUCODIGO + "</UFUCODIGO>");
                result.Append("<CODPROSAL>" + item.CODPROSAL + "</CODPROSAL>");
                result.Append("<CODPRODUC>" + item.CODPRODUC + "</CODPRODUC>");
                result.Append("<CantidadDevuelta>" + item.CantidadDevuelta.ToString() + "</CantidadDevuelta>");
                result.Append("<PROESTADO>" + item.PROESTADO + "</PROESTADO>");
                result.Append("<FECRESGIS>" + item.FECRESGIS.ToString("dd/MM/yyyy hh:mm:ss") + "</FECRESGIS>");
                result.Append("<CODUSUARI>" + item.CODUSUARI + "</CODUSUARI>");
                result.Append("<NOPOS>" + item.NOPOS.ToString() + "</NOPOS>");
                if (item.Entidad != null)
                {
                    result.Append("<Entidad>" + item.Entidad.CleanSpecialChars() + "</Entidad>");
                }
                result.Append("<Producto>" + item.Producto.CleanSpecialChars() + "</Producto>");
                if (item.ContratoPlan != null)
                {
                    result.Append("<ContratoPlan>" + item.ContratoPlan.CleanSpecialChars() + "</ContratoPlan>");
                }
                result.Append("<Tipo>" + item.Tipo + "</Tipo>");
                result.Append("<CantidadPendiente>" + item.CantidadPendiente + "</CantidadPendiente>");
                result.Append("<Medico>" + item.Medico + "</Medico>");
                result.Append("<Especialidad>" + item.Especialidad + "</Especialidad>");
                result.Append($"<HCMOANULBId>{item.HCMOANULBId}</HCMOANULBId>");
                result.Append($"<Description>{item.Description}</Description>");
                result.Append("<EntityId>" + item.EntityId + "</EntityId>");
                result.Append("<EntityName>" + item.EntityName + "</EntityName>");
                result.Append("<HCDEVMEDDId>" + item.HCDEVMEDDId + "</HCDEVMEDDId>");

                result.Append("</ViewDashboardPharmacyDetailDevolution>");
            }
            return result.ToString();
        }

        /// <summary>
        /// guarda una devolucion
        /// </summary>
        /// <param name="pharmaceuticalDispensingDevolution">The pharmaceutical dispensing devolution.</param>
        /// <param name="audit">The audit.</param>
        /// <param name="idSequence">The identifier sequence.</param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SavePharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            StringBuilder errors = new StringBuilder();
            try
            {
                //Obtengo el almacén de la devolución para determinar si es un almacén en consignación
                var warehouseDevolution = _warehouseRepository.GetWarehouseById(pharmaceuticalDispensingDevolution.WarehouseId);
                //Valido que, si el item a devolver proviene de un almacén en consignación, sea retornado al mismo almacén del cual proviene
                foreach (var item in pharmaceuticalDispensingDevolution.PharmaceuticalDispensingDevolutionDetail)
                {
                    //Obtengo la dispensación originaria de la devolución
                    var detailBacth = _pharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialById(item.PharmaceuticalDispensingDetailBatchSerialId);

                    //Obtengo el almacén de la dispensación para determinar si es un almacén en consignación
                    var warehouse = _warehouseRepository.GetWarehouseById(detailBacth.IdWarehouse);

                    //Si es un almacén en consignación el almacén debe ser el mismo almacén al cual se dispensó
                    if (warehouse.WarehouseConsignment == true && detailBacth.IdWarehouse != pharmaceuticalDispensingDevolution.WarehouseId)
                    {
                        errors.AppendLine(String.Format("El producto '{0}' de la dispensación '{1}' debe ser retornada al almacén: {2} - {3}.", item.CodeNameProduct.Trim(), item.CodePharmaceuticalDispensing.Trim(), warehouse.Code, warehouse.Name));
                    }
                    //Si el almacén de la devolución es de consignación todos los detalles deben proceder del mismo almacén
                    else if (warehouseDevolution.WarehouseConsignment == true && detailBacth.IdWarehouse != pharmaceuticalDispensingDevolution.WarehouseId)
                    {
                        errors.AppendLine(String.Format("El producto '{0}' de la dispensación '{1}' no puede ser devuelto al almacén en consignación: {2} - {3}.", item.CodeNameProduct.Trim(), item.CodePharmaceuticalDispensing.Trim(), warehouseDevolution.Code, warehouseDevolution.Name));
                    }
                }
                if (errors.Length > 0)
                {
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { StatusCode = eStatusResult.WARNING, StateResult = false, StateResultAux = false, Message = errors.ToString() };
                }

                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var xml = GenerateXmlDevolution(pharmaceuticalDispensingDevolution);
                    ((IObjectContextAdapter)_pharmaceuticalDispensingDevolutionRepository.UnitWork).ObjectContext.CommandTimeout = 3600;

                    var result = _pharmaceuticalDispensingDevolutionRepository.GeneratePharmaceuticalDevolutionSP(xml, "", audit.CodeUser);
                    var ObjResult = result.ToList()[0];
                    ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> actionResultReturn = new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>();
                    actionResultReturn.Message = ObjResult.Message;
                    if (ObjResult.Status == 1)
                    {
                        string[] sparator = { " *USERINDIGO* " };

                        var splitMessage = ObjResult.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);

                        actionResultReturn.Message = splitMessage.ElementAt(0);

                        actionResultReturn.StateResult = true;
                        actionResultReturn.StatusCode = eStatusResult.SUCCESS;
                        actionResultReturn.ObjectEmbbeded = _pharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionById(ObjResult.DevolutionId);
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
            }
            catch (Exception ex)
            {
                return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
            }
        }

        public ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> ConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            var listRevenueControlDetail = new List<int>();
            var serviceOrderCode = "";
            StringBuilder errors = new StringBuilder();
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork pharmaceuticalDispensingDevolutionUnitWork = _pharmaceuticalDispensingDevolutionRepository.UnitWork;
                IUnitWork serviceOrderDetailUnitWork = _serviceOrderDetailRepository.UnitWork;
                IUnitWork serviceOrderDetailDistributionUnitWork = _serviceOrderDetailDistributionRepository.UnitWork;
                IUnitWork pharmaceuticalDispensingDetailBatchSerialUnitWork = _pharmaceuticalDispensingDetailBatchSerialRepository.UnitWork;
                IUnitWork pharmaceuticalDispensingDetailUnitWork = _pharmaceuticalDispensingDetailRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                try
                {
                    //Obtengo el almacén para determinar si es un almacén en consignación
                    var warehouse = _warehouseRepository.GetWarehouseById(pharmaceuticalDispensingDevolution.WarehouseId);

                    foreach (var item in pharmaceuticalDispensingDevolution.PharmaceuticalDispensingDevolutionDetail)
                    {
                        var resulValidate = _inventoryService.ValidatePharmaceuticalDispensingDevolutionDetail(item);
                        if (resulValidate.StateResult == false)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { StateResult = false, StateResultAux = true, Message = resulValidate.Message };
                        }

                        var serviceOrder = _serviceOrderRepository.GetServiceOrderByEntityCode(item.CodePharmaceuticalDispensing);
                        serviceOrderCode = serviceOrder.Code;
                        var quantity = item.Quantity;
                        decimal grandTotalSalesPrice = 0;
                        foreach (var itemServiceOrderDetail in serviceOrder.ServiceOrderDetail.Where(x => x.ProductId == item.ProductId).ToList())
                        {
                            var resultList = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId(itemServiceOrderDetail.Id);
                            if (resultList == null || resultList.Count == 0)
                            {
                                continue;
                            }
                            var serviceOrderDetailDistribution = resultList.ElementAt(0);
                            if (itemServiceOrderDetail.InvoicedQuantity > quantity)
                            {
                                itemServiceOrderDetail.InvoicedQuantity -= quantity;
                                serviceOrderDetailDistribution.Quantity -= quantity;
                                itemServiceOrderDetail.DevolutionQuantity += quantity;
                                itemServiceOrderDetail.GrandTotalSalesPrice = itemServiceOrderDetail.TotalSalesPrice * itemServiceOrderDetail.InvoicedQuantity;
                                //quantity = 0;
                            }
                            else
                            {
                                itemServiceOrderDetail.DevolutionQuantity += itemServiceOrderDetail.InvoicedQuantity;
                                quantity -= itemServiceOrderDetail.InvoicedQuantity;
                                itemServiceOrderDetail.InvoicedQuantity = 0;
                                serviceOrderDetailDistribution.Quantity = 0;
                                itemServiceOrderDetail.GrandTotalSalesPrice = itemServiceOrderDetail.TotalSalesPrice * itemServiceOrderDetail.InvoicedQuantity;
                            }

                            serviceOrderDetailDistribution.GrandTotalSalesPrice = itemServiceOrderDetail.GrandTotalSalesPrice;
                            serviceOrderDetailDistribution.ThirdPartySalesPrice = itemServiceOrderDetail.GrandTotalSalesPrice;
                            grandTotalSalesPrice += itemServiceOrderDetail.GrandTotalSalesPrice;
                            serviceOrderDetailDistribution.ThirdPartyPercentage = 100;
                            serviceOrderDetailDistribution.ApplyRecoveryFee = 1;
                            serviceOrderDetailDistribution.RecoveryFeeType = 2;
                            serviceOrderDetailDistribution.SubTotalPatientSalesPrice = 0;
                            serviceOrderDetailDistribution.PatientPercentage = 0;

                            _serviceOrderDetailDistributionRepository.SaveEntity(serviceOrderDetailDistribution);
                            serviceOrderDetailDistributionUnitWork.Commit();
                            listRevenueControlDetail.Add(serviceOrderDetailDistribution.RevenueControlDetailId);
                            _serviceOrderDetailRepository.SaveEntity(itemServiceOrderDetail);
                            serviceOrderDetailUnitWork.Commit();


                        }
                        var detailBacth = _pharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialById(item.PharmaceuticalDispensingDetailBatchSerialId);
                        detailBacth.OutstandingQuantity -= item.Quantity;
                        _pharmaceuticalDispensingDetailBatchSerialRepository.SaveEntity(detailBacth);
                        pharmaceuticalDispensingDetailBatchSerialUnitWork.Commit();

                        //obtengo el detalle de la dispensación de la cual se realizará la devolución
                        var pharmaceuticalDispensing = _pharmaceuticalDispensingDetailRepository.GetPharmaceuticalDispensingDetailById(item.PharmaceuticalDispensingDetailId);

                        //aumento el inventario fisico
                        Domain.Entities.PhysicalInventory physical = _physicalInventoryRepository.GetPhysicalInventoryById(detailBacth.PhysicalInventoryId.GetValueOrDefault());
                        List<Kardex> listKardex = new List<Kardex>()
                        {
                            new Kardex()
                            {
                                ProductId = item.ProductId,
                                WarehouseId = pharmaceuticalDispensingDevolution.WarehouseId,
                                BatchSerialId = physical.BatchSerialId,
                                MovementType = 1,
                                Quantity = item.Quantity,
                                Value = pharmaceuticalDispensing.TotalSalesPrice,
                                AffectInventory = true
                            }
                        };
                        var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, pharmaceuticalDispensingDevolution.Id, pharmaceuticalDispensingDevolution.Code, pharmaceuticalDispensingDevolution.GetType().Name, pharmaceuticalDispensingDevolution.CreationUser);
                        if (result.StateResult == false)
                        {
                            errors.AppendLine(result.Message);
                            continue;
                        }

                        //Si es un almacén en consignación se debe marcar el producto como usado
                        if (warehouse.WarehouseConsignment == true)
                        {
                            //Actualizar el detalle de la remisión
                            result = _consignmentInventoryRemissionDetailBatchSerialAdminService.UpdateTheQuantityProductUsedInConsignmentInventoryRemission(item.Id, pharmaceuticalDispensingDevolution.OperatingUnitId, pharmaceuticalDispensing.FunctionalUnitId, pharmaceuticalDispensingDevolution.WarehouseId, item.ProductId, physical.BatchSerialId, MovementTypeRemissionUsed.OutPut, item.Quantity, pharmaceuticalDispensing.TotalSalesPrice, pharmaceuticalDispensingDevolution.Id, pharmaceuticalDispensingDevolution.Code, pharmaceuticalDispensingDevolution.GetType().Name, pharmaceuticalDispensingDevolution.CreationUser, pharmaceuticalDispensing.PharmaceuticalDispensingId);
                            if (result.StateResult == false)
                            {
                                errors.AppendLine(result.Message);
                                continue;
                            }
                        }

                        var pharmaceuticalDetail = _pharmaceuticalDispensingDetailRepository.GetPharmaceuticalDispensingDetailById(detailBacth.PharmaceuticalDispensingDetailId);
                        pharmaceuticalDetail.ReturnedQuantity += item.Quantity;
                        _pharmaceuticalDispensingDetailRepository.SaveEntity(pharmaceuticalDetail);
                        pharmaceuticalDispensingDetailUnitWork.Commit();
                    }


                    foreach (var item in listRevenueControlDetail.Distinct())
                    {
                        var resultRevenueControlDetail = _billingService.UpdateRevenueControlDetailValues(item, null, pharmaceuticalDispensingDevolution.OperatingUnitId);
                        if (resultRevenueControlDetail.StateResult == false)
                        {
                            errors.AppendLine(resultRevenueControlDetail.Message);
                        }
                    }

                    if (errors.Length > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { StateResult = false, StateResultAux = true, Message = errors.ToString() };
                    }

                    var resultGenerateJornalVourcher = _inventoryService.LoadJournalVoucherDevolution(pharmaceuticalDispensingDevolution);
                    if (resultGenerateJornalVourcher.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { Message = resultGenerateJornalVourcher.Message, StateResult = false, StateResultAux = true };
                    }
                    long consecutive = 0;
                    ActionMessageResult<JournalVouchers> resultSaveJournalVoucher = _accountingAdminService.SaveAccountingDocument(resultGenerateJornalVourcher.ObjectEmbbeded, audit, true);
                    if (resultSaveJournalVoucher.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { StateResult = false, StateResultAux = true, Message = resultSaveJournalVoucher.Message };
                    }
                    else
                    {
                        consecutive = resultSaveJournalVoucher.ObjectEmbbeded.Consecutive;
                    }
                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(pharmaceuticalDispensingDevolution.Code, (int)eTypeDocumentsControlInventory.ReturnPharmaceuticaldispensing);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }
                    pharmaceuticalDispensingDevolution.Status = 2;
                    pharmaceuticalDispensingDevolution.ConfirmationDate = DateTime.Now;
                    pharmaceuticalDispensingDevolution.ConfirmationUser = audit.CodeUser;
                    pharmaceuticalDispensingDevolution.ModificationDate = DateTime.Now;
                    pharmaceuticalDispensingDevolution.ModificationUser = audit.CodeUser;
                    pharmaceuticalDispensingDevolution.MarkAsModified();

                    _pharmaceuticalDispensingDevolutionRepository.SaveEntity(pharmaceuticalDispensingDevolution);
                    pharmaceuticalDispensingDevolutionUnitWork.Commit();

                    transaction.Complete();
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>() { MessageResult = new List<string> { serviceOrderCode }, Message = consecutive.ToString(), StateResult = true, ObjectEmbbeded = pharmaceuticalDispensingDevolution };



                }
                catch (OptimisticConcurrencyException ex)
                {
                    pharmaceuticalDispensingDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    pharmaceuticalDispensingDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    pharmaceuticalDispensingDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string> { ex.Message }
                    };
                }

            }
        }

        public ActionResult<Domain.Entities.PharmaceuticalDispensingDevolution> SaveAndConfirmPharmaceuticalDispensingDevolution(Domain.Entities.PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            pharmaceuticalDispensingDevolution.Status = 2;
            var result = SavePharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, audit, idSequence, sequenceC);
            return result;
        }

        /// <summary>
        /// metodo para hacer la devolucion del sumistro desde el dashboard
        /// </summary>
        /// <param name="ListPharmaceuticalDispensingDevolution"></param>
        /// <param name="ListDetailAnnular"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<string> SaveDashboardPharmacyDevolution(List<Domain.Entities.PharmaceuticalDispensingDevolution> ListPharmaceuticalDispensingDevolution, List<Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution> ListDetailAnnular, AuditMessage audit, long idSecuence = 0)
        {

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    var xmlDevolution = "";
                    if (ListPharmaceuticalDispensingDevolution != null)
                    {
                        xmlDevolution = GenerateXmlDevolution(ListPharmaceuticalDispensingDevolution[0], true);
                    }

                    var xmlAnnulation = GenerateXmlDevolutionAnnulation(ListDetailAnnular);
                    var result = _pharmaceuticalDispensingDevolutionRepository.GeneratePharmaceuticalDevolutionSP(xmlDevolution, xmlAnnulation, audit.CodeUser);
                    var ObjResult = result.ToList()[0];
                    ActionResult<string> actionResultReturn = new ActionResult<string>();
                    actionResultReturn.Message = ObjResult.Message;
                    if (ObjResult.Status == 1)
                    {
                        string[] sparator = { " *USERINDIGO* " };
                        string[] splitMessage = null;

                        if (ObjResult.Message != null)
                        {
                            splitMessage = ObjResult.Message.Split(sparator, StringSplitOptions.RemoveEmptyEntries);
                            actionResultReturn.Message = splitMessage.ElementAt(0);
                        }


                        actionResultReturn.StateResult = true;
                        actionResultReturn.StatusCode = eStatusResult.SUCCESS;
                        if (ObjResult.DevolutionId > 0)
                        {

                            actionResultReturn.ObjectEmbbeded = _pharmaceuticalDispensingDevolutionRepository.GetPharmaceuticalDispensingDevolutionById(ObjResult.DevolutionId).Code;
                            actionResultReturn.MessageResult = new List<string> { splitMessage.ElementAt(1) }.ToList();
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
                catch (Exception ex)
                {
                    scope.Dispose();
                    return new ActionResult<string> { StatusCode = eStatusResult.EXCEPTION, StateResult = false, StateResultAux = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
            }
        }

        public async Task<int> ExecuteRollbackAsync(string rollbackObjectCode)
        {
            var res = _pharmaceuticalDispensingDevolutionRepository.CascadeRollback(rollbackObjectCode);
            return await Task.FromResult(res);
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
                    _billingService.Dispose();
                    _accountingAdminService.Dispose();
                    _inventoryService.Dispose();
                    _physicalInventoryAdminService.Dispose();
                    _consignmentInventoryRemissionDetailBatchSerialAdminService.Dispose();
                }
                _pharmaceuticalDispensingDetailRepository = null;
                _pharmaceuticalDispensingDetailBatchSerialRepository = null;
                _billingService = null;
                _accountingAdminService = null;
                _pharmaceuticalDispensingDevolutionRepository = null;
                _sequenseRepository = null;
                _settingInventoryRepository = null;
                _revenueControlDetailRepository = null;
                _serviceOrderRepository = null;
                _serviceOrderDetailRepository = null;
                _inventoryService = null;
                _serviceOrderDetailDistributionRepository = null;
                _consecutiveRepository = null;
                _kardexCrystalRepository = null;
                _physicalInventoryCrystalRepository = null;
                _stayRepository = null;
                _devolutionMedicationRepository = null;
                _devolutionMedicationDetailRepository = null;
                _bedRepository = null;
                _admissionRepository = null;
                _InventoryControlDocumentRepository = null;
                _physicalInventoryAdminService = null;
                _physicalInventoryRepository = null;
                _warehouseRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialAdminService = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion

    }
}
