'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
' Modified         : Diego A. Roldán
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration
Imports System.Data.Entity
Imports System.Data.Entity.Core
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Text
Imports System.Transactions
Imports Application.Accounting
Imports Application.Contract
Imports Application.EventHandlers
Imports Application.EventHandlers.Enums.Enums
Imports Application.EventHandlers.Model
Imports Application.EventHandlers.Security
Imports Application.EventHandlers.Security.Entities
Imports Application.Portfolio
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Crystal.Service
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
#End Region

Public Class LiquidationAdminService
    Implements ILiquidationAdminService

#Region "Fileds"

    Private Const TAG_ACCOUNTINGRECEIVABLE As String = "682"
    Private Const TAG_PORTFOLIO_TRANSFER As String = "687"
    Private Const TAG_PAGARE As String = "1510"
    Private Const TAG_SERVICE_ORDER As String = "755"
    Private ReadOnly _eventProxy As IEventProxy
    Private _admissionRepository As IAdmissionRepository
    Private _surgeriesPercentageManualAdminService As ISurgeriesPercentageManualAdminService
    Private _revenueControlRepository As IRevenueControlRepository
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository
    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository
    Private _serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository
    Private _serviceOrderRepository As IServiceOrderRepository
    Private _companySettingsRepository As ICompanySettingsRepository
    Private _specialityRepository As ISpecialityRepository
    Private _caregroupRepository As ICareGroupRepository
    Private _LiquidationDataRepository As ILiquidationDataRepository
    Private _rateManualRepository As IRateManualRepository
    Private _cupsHomologation As ICupsHomologationRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _cupsRepository As ICupsEntityRepository
    Private _ipsServiceRepository As IIPSServicesRepository
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository
    Private _rateManualDetailRepository As IRateManualDetailRepository
    Private _productRateDetailRepository As IProductRateDetailRepository
    Private _invoiceAdminService As IInvoiceAdminService
    Private _billingServ As IBillingServices
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _billingAuthorizationAdminService As IBillingAuthorizationAdminService
    Private _accountingDocumentAdminService As IAccountingDocumentAdminService
    Private _documentTypeRepository As IDocumentTypeRepository
    Private _accountingDocumentRepository As IAccountingDocumentRepository
    Private _accountReceivableAdminService As IAccountReceivableAdminService
    Private _portFolioSequenceAdminService As IPortfolioSequenseAdminService
    Private _invoiceRepository As IInvoiceRepository
    Private _medicalFeesCausationRepository As IMedicalFeesCausationRepository
    Private _settingBillingRepository As ISettingsBillingRepository
    Private _settingBillingAdminService As ISettingBillingAdminService
    Private _portfolioTransferAdminService As IPortfolioTransfersAdminService
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository
    Private _serviceOrderAdminService As IServiceOrderAdminService
    Private _serviceOrderDetailAdminService As IServiceOrderDetailAdminService
    Private _billingSequenceRepository As IBillingSequenseAdminService
    Private _surgicalProcedureDetail As ISurgicalProcedureServiceRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _portfolioTransferRepository As IPortfolioTransferRepository
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    Private _contractServices As IContractServices
    Private _invoicePortfolioAdvanceRepository As IInvoicePortfolioAdvanceRepository
    Private _recognitionDetailRepository As IRevenueRecognitionDetailRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private _operatingUnitRepository As IOperatingUnitRepository
    Private _electronicDocumentRepository As IElectronicDocumentRepository
    Private _electronicDocumentNotificationRepository As IElectronicDocumentNotificationRepository
    Private _billingNoteRepository As IBillingNoteRepository
    Private _billingReversalReasonRepository As IBillingReversalReasonRepository
    Private _sequenseRepository As IBillingSequenceRepository
    Private _basicBillingAdminService As IBasicBillingAdminService

    Private _hCREGEGRERepository As IHCREGEGRERepository
    Private _aDCONCOEXrepository As IADCONCOEXrepository
    Private _aMBORDLABRepository As IAMBORDLABRepository
    Private _aMBORDIMARepository As IAMBORDIMARepository
    Private _aMBORDPATRepository As IAMBORDPATRepository
    Private _iNPACIENTTOPANURepository As IINPACIENTTOPANURepository
    Private _patientRepository As IPatientRepository
    Private _iHCJUNOPMHRepository As IHCJUNOPMHRepository

    Private _stayRepository As IStayRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _pharmaceuticalDispensingRepository As IPharmaceuticalDispensingRepository
    Private _pharmaceuticalDispensingDevolutionRepository As IPharmaceuticalDispensingDevolutionRepository
    Private _thirdpartyRepository As IThirdPartyRepository
    Private ReadOnly _contractPackageServiceRepository As IContractPackageServiceRepository
    Private ReadOnly _contractPackageProductRepository As IContractPackageProductRepository
    Private ReadOnly _mipresCodeRepository As IMipresCodeRepository
    Private ReadOnly _pharmaceuticalDispensingDetailBathSerialRepository As IPharmaceuticalDispensingDetailBatchSerialRepository
    Private ReadOnly _pharmaceuticalDispensingDetailRepository As IPharmaceuticalDispensingDetailRepository
    Private ReadOnly _requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private ReadOnly _productServiceDetailRepository As IProductServiceDetailRepository
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _customerRepository As ICustomerRepository
    Private ReadOnly _retentionConceptRepository As IRetentionConceptRepository
    Private ReadOnly _invoiceCopayService As IInvoiceCopayAdminService
    Private ReadOnly _invoiceCopayRepository As IInvoiceCopayRepository
    Private ReadOnly _basicBillingRepository As IBasicBillingRepository
    ''' <summary>
    ''' repositorio vista que lista los detalles no qx
    ''' </summary>
    Private _viewListNoSurgicalRepository As IViewListNoSurgicalRepository
    ''' <summary>
    ''' Repositorio Tabla HCORDIMAG
    ''' </summary>
    Private _hCORDIMAGRepository As IHCORDIMAGRepository

    Private _folioAdminService As IFolioAdminService
    Private ReadOnly _stayService As IStayService

    ''' <summary>
    ''' Variable que contiene el listado de datos para el reporte de estadistico de facturacion
    ''' </summary>
    Private ReportBillingStadisticsData As List(Of SP_ReportBillingStadistics_Result)

    ''' <summary>
    ''' repositorio de la entidad sin recaudo de cuota
    ''' </summary>
    Private _feeNotCollectedRepository As IFeeNotCollectedRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="admissionRepository">Repositorio de ingresos</param>
    ''' <param name="revenueControlRepository">Repositorio de control</param>
    ''' <param name="revenueControlDetailRepository">Repositorio de detalle de control folio</param>
    ''' <param name="serviceOrderDetailDistributionRepository">Repositorio de detalle de ordenes de servicio</param>
    ''' <param name="companySettingsRepository">Repositorio de configuración de empresa</param>
    ''' <param name="serviceOrderRepository">Repositorio de la orden de servicio</param>
    Public Sub New(ByVal admissionRepository As IAdmissionRepository, revenueControlRepository As IRevenueControlRepository, revenueControlDetailRepository As IRevenueControlDetailRepository,
                   serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository, companySettingsRepository As ICompanySettingsRepository, serviceOrderRepository As IServiceOrderRepository,
                   rateManualRepository As IRateManualRepository, cupsHomologation As ICupsHomologationRepository,
                   functionalUnitRepository As IFunctionalUnitRepository, costCenterRepository As ICostCenterRepository, cupsRepository As ICupsEntityRepository,
                   ipsServiceRepository As IIPSServicesRepository, surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository,
                   rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository, rateManualDetailRepository As IRateManualDetailRepository,
                   specialityRepository As ISpecialityRepository, serviceOrderDetailRepository As IServiceOrderDetailRepository,
                   serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository, caregroupRepository As ICareGroupRepository, liquidationDataRepository As ILiquidationDataRepository,
                   productRateDetailRepository As IProductRateDetailRepository, billingServ As IBillingServices, invoiceAdminService As IInvoiceAdminService, billingAuthorizationRepository As IBillingAuthorizationRepository,
                   billingAuthorizationAdminService As IBillingAuthorizationAdminService, accountingDocumentAdminService As IAccountingDocumentAdminService, documentTypeRepository As IDocumentTypeRepository,
                   accountingDocumentRepository As IAccountingDocumentRepository, accountReceivableAdminService As IAccountReceivableAdminService, portFolioSequenceAdminService As IPortfolioSequenseAdminService,
                   invoiceRepository As IInvoiceRepository, medicalFeesCausationRepository As IMedicalFeesCausationRepository, settingBillingRepository As ISettingsBillingRepository,
                   settingBillingAdminService As ISettingBillingAdminService, portfolioTransferAdminService As IPortfolioTransfersAdminService, portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   serviceOrderAdminService As IServiceOrderAdminService, billingSequenceRepository As IBillingSequenseAdminService, surgicalProcedureDetail As ISurgicalProcedureServiceRepository,
                   accountReceivableRepository As IAccountReceivableRepository, portfolioTransferRepository As IPortfolioTransferRepository, healthAdministratorRepository As IHealthAdministratorRepository,
                   contractServices As IContractServices, hCREGEGRERepository As IHCREGEGRERepository, aDCONCOEXrepository As IADCONCOEXrepository, aMBORDLABRepository As IAMBORDLABRepository,
                   aMBORDIMARepository As IAMBORDIMARepository, aMBORDPATRepository As IAMBORDPATRepository, iNPACIENTTOPANURepository As IINPACIENTTOPANURepository, patientRepository As IPatientRepository,
                   invoicePortfolioAdvanceRepository As IInvoicePortfolioAdvanceRepository, stayRepository As IStayRepository, iHCJUNOPMHRepository As IHCJUNOPMHRepository,
                   inventoryProductRepository As IInventoryProductRepository, pharmaceuticalDispensingRepository As IPharmaceuticalDispensingRepository, pharmaceuticalDispensingDevolutionRepository As IPharmaceuticalDispensingDevolutionRepository,
                   thirdpartyRepository As IThirdPartyRepository, serviceOrderDetailAdminService As IServiceOrderDetailAdminService, surgeriesPercentageManualAdminService As ISurgeriesPercentageManualAdminService,
                   recognitionDetailRepository As IRevenueRecognitionDetailRepository, settingsAccountRepository As ISettingsAccountRepository,
                   operatingUnitRepository As IOperatingUnitRepository, electronicDocumentRepository As IElectronicDocumentRepository, billingNoteRepository As IBillingNoteRepository,
                   billingReversalReasonRepository As IBillingReversalReasonRepository, sequenseRepository As IBillingSequenceRepository,
                   contractPackageServiceRepository As IContractPackageServiceRepository,
                   contractPackageProductRepository As IContractPackageProductRepository,
                   electronicDocumentNotificationRepository As IElectronicDocumentNotificationRepository,
                   mipresCodeRepository As IMipresCodeRepository,
                   pharmaceuticalDispensingDetailBathSerialRepository As IPharmaceuticalDispensingDetailBatchSerialRepository,
                   requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   productServiceDetailRepository As IProductServiceDetailRepository,
                   pharmaceuticalDispensingDetailRepository As IPharmaceuticalDispensingDetailRepository, ViewListNoSurgicalRepository As IViewListNoSurgicalRepository, HCORDIMAGRepository As IHCORDIMAGRepository,
                   userRepository As IUserRepository, eventProxy As IEventProxy, folioAdminService As IFolioAdminService, stayService As IStayService,
                   basicBillingAdminService As IBasicBillingAdminService,
                   customerRepository As ICustomerRepository,
                   retentionConceptRepository As IRetentionConceptRepository,
                   invoiceCopayService As IInvoiceCopayAdminService,
                   basicBillingRepository As IBasicBillingRepository,
                   invoiceCopayRepository As IInvoiceCopayRepository,
                   feeNotCollectedRepository As IFeeNotCollectedRepository)
        _userRepository = userRepository
        _basicBillingRepository = basicBillingRepository
        _invoiceCopayService = invoiceCopayService
        _retentionConceptRepository = retentionConceptRepository
        _invoiceCopayRepository = invoiceCopayRepository
        _customerRepository = customerRepository
        _basicBillingAdminService = basicBillingAdminService
        _mipresCodeRepository = mipresCodeRepository
        _contractPackageServiceRepository = contractPackageServiceRepository
        _contractPackageProductRepository = contractPackageProductRepository
        _aDCONCOEXrepository = aDCONCOEXrepository
        _aMBORDLABRepository = aMBORDLABRepository
        _aMBORDIMARepository = aMBORDIMARepository
        _aMBORDPATRepository = aMBORDPATRepository
        _serviceOrderDetailAdminService = serviceOrderDetailAdminService
        _surgeriesPercentageManualAdminService = surgeriesPercentageManualAdminService
        Me._admissionRepository = admissionRepository
        Me._revenueControlRepository = revenueControlRepository
        Me._revenueControlDetailRepository = revenueControlDetailRepository
        Me._serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
        Me._companySettingsRepository = companySettingsRepository
        Me._serviceOrderRepository = serviceOrderRepository
        Me._rateManualRepository = rateManualRepository
        Me._cupsHomologation = cupsHomologation
        Me._functionalUnitRepository = functionalUnitRepository
        Me._costCenterRepository = costCenterRepository
        Me._cupsRepository = cupsRepository
        Me._ipsServiceRepository = ipsServiceRepository
        Me._surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        Me._rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        Me._rateManualDetailRepository = rateManualDetailRepository
        Me._specialityRepository = specialityRepository
        Me._serviceOrderDetailRepository = serviceOrderDetailRepository
        Me._serviceOrderDetailSurgicalRepository = serviceOrderDetailSurgicalRepository
        Me._caregroupRepository = caregroupRepository
        Me._LiquidationDataRepository = liquidationDataRepository
        Me._productRateDetailRepository = productRateDetailRepository
        Me._billingServ = billingServ
        Me._invoiceAdminService = invoiceAdminService
        Me._billingAuthorizationRepository = billingAuthorizationRepository
        Me._billingAuthorizationAdminService = billingAuthorizationAdminService
        Me._accountingDocumentAdminService = accountingDocumentAdminService
        Me._documentTypeRepository = documentTypeRepository
        Me._accountingDocumentRepository = accountingDocumentRepository
        Me._accountReceivableAdminService = accountReceivableAdminService
        Me._portFolioSequenceAdminService = portFolioSequenceAdminService
        Me._invoiceRepository = invoiceRepository
        Me._medicalFeesCausationRepository = medicalFeesCausationRepository
        Me._settingBillingRepository = settingBillingRepository
        Me._settingBillingAdminService = settingBillingAdminService
        Me._portfolioTransferAdminService = portfolioTransferAdminService
        Me._portfolioAdvanceRepository = portfolioAdvanceRepository
        Me._serviceOrderAdminService = serviceOrderAdminService
        Me._billingSequenceRepository = billingSequenceRepository
        Me._surgicalProcedureDetail = surgicalProcedureDetail
        Me._accountReceivableRepository = accountReceivableRepository
        Me._portfolioTransferRepository = portfolioTransferRepository
        Me._healthAdministratorRepository = healthAdministratorRepository
        Me._contractServices = contractServices
        Me._settingsAccountRepository = settingsAccountRepository
        Me._operatingUnitRepository = operatingUnitRepository
        Me._electronicDocumentRepository = electronicDocumentRepository
        Me._billingNoteRepository = billingNoteRepository
        Me._billingReversalReasonRepository = billingReversalReasonRepository
        Me._sequenseRepository = sequenseRepository
        _hCREGEGRERepository = hCREGEGRERepository
        _iNPACIENTTOPANURepository = iNPACIENTTOPANURepository
        _patientRepository = patientRepository
        _invoicePortfolioAdvanceRepository = invoicePortfolioAdvanceRepository
        _stayRepository = stayRepository
        _iHCJUNOPMHRepository = iHCJUNOPMHRepository
        _inventoryProductRepository = inventoryProductRepository
        _pharmaceuticalDispensingRepository = pharmaceuticalDispensingRepository
        _pharmaceuticalDispensingDevolutionRepository = pharmaceuticalDispensingDevolutionRepository
        _thirdpartyRepository = thirdpartyRepository
        _recognitionDetailRepository = recognitionDetailRepository
        _electronicDocumentNotificationRepository = electronicDocumentNotificationRepository
        _pharmaceuticalDispensingDetailBathSerialRepository = pharmaceuticalDispensingDetailBathSerialRepository
        _requestPackageDetailStatusRepository = requestPackageDetailStatusRepository
        _productServiceDetailRepository = productServiceDetailRepository
        _pharmaceuticalDispensingDetailRepository = pharmaceuticalDispensingDetailRepository
        _viewListNoSurgicalRepository = ViewListNoSurgicalRepository
        _hCORDIMAGRepository = HCORDIMAGRepository
        _eventProxy = eventProxy
        Me._folioAdminService = folioAdminService
        Me._stayService = stayService
        Me._feeNotCollectedRepository = feeNotCollectedRepository
    End Sub

#End Region

#Region "Methods"
    Public Function LiquidateItemProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult Implements ILiquidationAdminService.LiquidateItemProduction
        Try
            Dim sod = _serviceOrderDetailRepository.FirstOrDefault(Function(m) m.Id = serviceOrderDetailId, includes:={"InventoryProduct", "ServiceOrder", "FunctionalUnit.BranchOffice", "ServiceOrderDetailDistribution"})

            ValidateItemToItemProduction(sod)

            Dim pharmaceuticalDetail = _pharmaceuticalDispensingDetailRepository _
                .Query(Function(m) m.PharmaceuticalDispensingId = sod.ServiceOrder.EntityId _
                    AndAlso m.ProductId = sod.ProductId.Value) _
                .FirstOrDefault()

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                }
            )
                ' Ahora Eliminamos los datos de ProductServiceDetail
                Dim productServiceDetail = _productServiceDetailRepository.GetByFilter(Function(m) m.ServiceOrderDetailId = sod.Id)

                If productServiceDetail.Any() Then
                    For Each psd In productServiceDetail
                        _productServiceDetailRepository.DeleteEntity(psd)
                    Next

                    _productServiceDetailRepository.UnitWork.Commit()
                End If

                Dim res = GetValueProductValue(
                    caregroupId:=sod.ServiceOrderDetailDistribution(0).LastCaregroupId,
                    productId:=sod.ProductId,
                    functionalUnitId:=sod.PerformsFunctionalUnitId,
                    specialty:=sod.PerformsProfessionalSpecialty,
                    admissionNumber:=sod.ServiceOrder.AdmissionNumber,
                    patientCode:=sod.ServiceOrder.PatientCode,
                    centerAttentionCode:=sod.FunctionalUnit.BranchOffice.Code,
                    professionalHealthCode:=sod.PerformsHealthProfessionalCode,
                    professionalHealthThirdPartyId:=sod.PerformsHealthProfessionalThirdPartyId,
                    quantity:=sod.InvoicedQuantity,
                    pharmaceuticalDispensingDetailId:=pharmaceuticalDetail.Id,
                    serviceOrderDetailId:=sod.Id
                )

                If Not res.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = res.Message}
                End If

                sod.TotalSalesPrice = res.ObjectEmbbeded.price
                sod.SubTotalSalesPrice = res.ObjectEmbbeded.price
                sod.GrandTotalSalesPrice = res.ObjectEmbbeded.price * sod.InvoicedQuantity
                sod.RateManualSalePrice = res.ObjectEmbbeded.price
                sod.ProductLiquidationType = 1

                sod.ServiceOrderDetailDistribution(0).GrandTotalSalesPrice = res.ObjectEmbbeded.price * sod.ServiceOrderDetailDistribution(0).Quantity
                sod.ServiceOrderDetailDistribution(0).ThirdPartySalesPrice = res.ObjectEmbbeded.price * sod.ServiceOrderDetailDistribution(0).Quantity

                _serviceOrderDetailRepository.SaveEntity(sod)
                _serviceOrderDetailRepository.UnitWork.Commit()

                Dim resRecalculate = _billingServ.UpdateRevenueControlDetailValues(sod.ServiceOrderDetailDistribution(0).RevenueControlDetailId)

                If Not resRecalculate.StateResult Then Throw New IndigoValidationException(resRecalculate.Message)

                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Validaciones de item de produccion
    ''' </summary>
    ''' <param name="sod"></param>
    Private Sub ValidateItemToItemProduction(sod As ServiceOrderDetail)
        If sod Is Nothing Then Throw New IndigoValidationException("Detalle de Órden de servicio no encontrada")

        If sod.ProductId Is Nothing Then Throw New IndigoValidationException("Producto no encontrado")

        If sod.ServiceOrderDetailDistribution.Count > 1 Then Throw New IndigoValidationException("El item se encuentra distribuído")

        If Not sod.ServiceOrder.EntityName.Equals(GetType(PharmaceuticalDispensing).Name) Then Throw New IndigoValidationException("El ítem no fué generado a partir de una dispensación")
    End Sub

    ''' <summary>
    ''' Liquida los detalles de producción
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function LiquidateDetailProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult Implements ILiquidationAdminService.LiquidateDetailProduction
        Try
            Dim sod = _serviceOrderDetailRepository.FirstOrDefault(Function(m) m.Id = serviceOrderDetailId, includes:={"InventoryProduct", "ServiceOrder", "FunctionalUnit.BranchOffice", "ServiceOrderDetailDistribution"})

            ValidateItemToDetailProduction(sod)

            Dim pharmaceuticalBatchSerial = _pharmaceuticalDispensingDetailBathSerialRepository _
                .Query(Function(m) m.PharmaceuticalDispensingDetail.PharmaceuticalDispensingId = sod.ServiceOrder.EntityId _
                    AndAlso m.PharmaceuticalDispensingDetail.ProductId = sod.ProductId.Value, includes:={"PhysicalInventory.BatchSerial"}) _
                .FirstOrDefault()

            If pharmaceuticalBatchSerial Is Nothing Then Throw New IndigoValidationException("Producto no encontrado en la dispensación")

            If pharmaceuticalBatchSerial?.PhysicalInventory?.BatchSerial Is Nothing Then
                Throw New IndigoValidationException("PhysicalInventory no encontrado")
            End If

            Dim status = _requestPackageDetailStatusRepository.FirstOrDefault(Function(m) m.BatchCode = pharmaceuticalBatchSerial.PhysicalInventory.BatchSerial.BatchCode, includes:={"CampaignRawMaterial.InventoryProduct"})

            If status Is Nothing Then
                Throw New IndigoValidationException("No se encontró el producto en central de mezclas")
            End If

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                }
            )
                Dim sb As New StringBuilder()
                Dim totalProduct As Decimal = 0

                ' Ahora Eliminamos los datos de ProductServiceDetail
                Dim productServiceDetail = _productServiceDetailRepository.GetByFilter(Function(m) m.ServiceOrderDetailId = sod.Id)

                If productServiceDetail.Any() Then
                    For Each psd In productServiceDetail
                        _productServiceDetailRepository.DeleteEntity(psd)
                    Next

                    _productServiceDetailRepository.UnitWork.Commit()
                End If

                For Each item In status.CampaignRawMaterial
                    Dim res = GetValueProductValue(
                        caregroupId:=sod.ServiceOrderDetailDistribution(0).LastCaregroupId,
                        productId:=item.ProductValidationId,
                        functionalUnitId:=sod.PerformsFunctionalUnitId,
                        specialty:=sod.PerformsProfessionalSpecialty,
                        admissionNumber:=sod.ServiceOrder.AdmissionNumber,
                        patientCode:=sod.ServiceOrder.PatientCode,
                        centerAttentionCode:=sod.FunctionalUnit.BranchOffice.Code,
                        professionalHealthCode:=sod.PerformsHealthProfessionalCode,
                        professionalHealthThirdPartyId:=sod.PerformsHealthProfessionalThirdPartyId,
                        quantity:=sod.InvoicedQuantity,
                        pharmaceuticalDispensingDetailId:=pharmaceuticalBatchSerial.PharmaceuticalDispensingDetailId,
                        serviceOrderDetailId:=sod.Id,
                        FlagDetaiProduction:=True
                    )

                    If Not res.StateResult Then
                        sb.AppendLine(res.Message)
                        Continue For
                    End If

                    totalProduct += res.ObjectEmbbeded.price
                Next

                If sb.Length > 0 Then Throw New IndigoValidationException(sb.ToString())

                ' Actualizamos el detalle de la orden de servicio
                sod.TotalSalesPrice = totalProduct
                sod.SubTotalSalesPrice = totalProduct
                sod.GrandTotalSalesPrice = totalProduct * sod.InvoicedQuantity
                sod.RateManualSalePrice = totalProduct
                sod.ProductLiquidationType = 2

                sod.ServiceOrderDetailDistribution(0).GrandTotalSalesPrice = totalProduct * sod.ServiceOrderDetailDistribution(0).Quantity
                sod.ServiceOrderDetailDistribution(0).ThirdPartySalesPrice = totalProduct * sod.ServiceOrderDetailDistribution(0).Quantity

                _serviceOrderDetailRepository.SaveEntity(sod)
                _serviceOrderDetailRepository.UnitWork.Commit()

                Dim resRecalculate = _billingServ.UpdateRevenueControlDetailValues(sod.ServiceOrderDetailDistribution(0).RevenueControlDetailId)

                If Not resRecalculate.StateResult Then Throw New IndigoValidationException(resRecalculate.Message)

                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el valor de un producto
    ''' </summary>
    ''' <param name="caregroupId"></param>
    ''' <param name="productId"></param>
    ''' <param name="functionalUnitId"></param>
    ''' <param name="specialty"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="centerAttentionCode"></param>
    ''' <param name="professionalHealthCode"></param>
    ''' <param name="professionalHealthThirdPartyId"></param>
    ''' <returns></returns>
    Private Function GetValueProductValue(
        caregroupId As Integer,
        productId As Integer,
        functionalUnitId As Integer,
        specialty As String,
        admissionNumber As String,
        patientCode As String,
        centerAttentionCode As String,
        professionalHealthCode As String,
        professionalHealthThirdPartyId As Integer,
        quantity As Integer,
        pharmaceuticalDispensingDetailId As Integer?,
        serviceOrderDetailId As Integer?,
        Optional FlagDetaiProduction As Boolean = False
    ) As ActionResult(Of (price As Decimal, productRateDetail As ProductRateDetail))

        Dim salePriceRateType As Decimal = 0
        Dim salesPriceCUPS As Decimal = 0
        Dim salesPrice As Decimal = 0

        Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = productId)
        Dim caregroup = _caregroupRepository.FirstOrDefault(Function(m) m.Id = caregroupId)
        Dim productRateDetailList = _productRateDetailRepository.GetListProductRateDetailByCareGroupIdProductIdServiceDate(
            CareGroupId:=caregroupId,
            ProductId:={productId}.ToList(),
            ServiceDate:=Date.Now
        )

        If productRateDetailList Is Nothing OrElse Not productRateDetailList.Any() Then
            Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = False, .Message = $"No se encontro tarifa para el producto ({product.Code} - {product.Name})"}
        End If

        Dim productRateDetail = productRateDetailList(0)

        Select Case productRateDetail.RateType
            Case 1
                salePriceRateType = productRateDetail.SalesValue
            Case 2
                'porcentaje basado en = 1.Costo promedio ponderado; 2 - Ultimo costo
                If productRateDetail.PercentageBasedOn = 1 Then
                    salePriceRateType = (productRateDetail.InventoryProduct.ProductCost * (productRateDetail.Percentage / 100)) + (productRateDetail.InventoryProduct.ProductCost)

                ElseIf productRateDetail.PercentageBasedOn = 2 Then
                    salePriceRateType = (productRateDetail.InventoryProduct.FinalProductCost * (productRateDetail.Percentage / 100)) + (productRateDetail.InventoryProduct.FinalProductCost)
                Else
                    salePriceRateType = 0
                End If
            Case 0
                salesPrice = 0
        End Select

        If {2, 3}.Contains(productRateDetail.LiquidationType) Then
            Dim homologationsResult = _contractServices.GetHomologationCups(
                CareGroupId:=caregroupId,
                CupsId:=productRateDetail.CupsId,
                FunctionalUnitId:=functionalUnitId,
                Specialty:=specialty,
                ServiceDate:=Date.Now,'sod.ServiceDate,
                IPSServiceId:=0,
                ManualType:=0,
                RiasId:=0,
                ContractDescriptionId:=productRateDetail.ContractDescriptionId
            )

            If Not homologationsResult.StateResult OrElse homologationsResult.ObjectEmbbeded.Count > 1 Then
                If homologationsResult.Message.Length Then
                    Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = False, .Message = homologationsResult.Message}
                Else
                    Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = False, .Message = "El CUPS asociado al Producto tiene más de una Homologación"}
                End If
            Else

                Dim thirdPatient = _thirdpartyRepository.FirstOrDefault(Function(m) m.Nit = patientCode.Trim(), False, {"Person"})

                If thirdPatient Is Nothing Then
                    Throw New IndigoValidationException("El paciente no estpa creado como tercero en Indigo VIE")
                End If

                Dim ThirdPartyPatienDate = thirdPatient.Person.BirthDate
                Dim genderThirdParty = thirdPatient.Person.Gender

                Dim listHomologation = homologationsResult.ObjectEmbbeded
                Dim resServiceOrderDetail = _serviceOrderDetailAdminService.GetServiceValue(
                                AdmissionNumber:=admissionNumber,
                                CenterAttentionCode:=centerAttentionCode,
                                listCupsHomologation:=listHomologation,
                                CareGroupId:=caregroupId,
                                FunctionalUnitId:=functionalUnitId,
                                Specialty:=specialty,
                                ServiceDate:=Date.Now,
                                PatientGenus:=genderThirdParty,
                                PatientDateBirth:=ThirdPartyPatienDate,
                                InvoicedQuantity:=quantity,
                                ProfessionalHealthCode:=professionalHealthCode,
                                ProfessionalHealthThirdPartyId:=professionalHealthThirdPartyId,
                                RiasId:=0,
                                ContractDescriptionId:=productRateDetail.ContractDescriptionId
                            )

                Dim listServiceOrderDetail = resServiceOrderDetail.ObjectEmbbeded

                If Not resServiceOrderDetail.StateResult Then
                    Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = False, .Message = resServiceOrderDetail.Message}
                End If

                If listServiceOrderDetail Is Nothing OrElse Not listServiceOrderDetail.Any() Then
                    Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = False, .Message = $"No se ha encontrado Tarifa para el CUPS asociado al producto ({product.Code} - {product.Name})"}
                End If

                salesPriceCUPS = listServiceOrderDetail(0).SubTotalSalesPrice
            End If

            If pharmaceuticalDispensingDetailId.HasValue Then
                Dim productServiceDetail As New ProductServiceDetail With {
                    .PharmaceuticalDispensingDetailId = pharmaceuticalDispensingDetailId,
                    .ServiceOrderDetailId = serviceOrderDetailId,
                    .CUPSEntityId = productRateDetail.CupsId,
                    .ContractDescriptionsId = productRateDetail.ContractDescriptionId,
                    .ProductId = Nothing,
                    .Price = salesPriceCUPS,
                    .LiquidationType = productRateDetail.LiquidationType,
                    .RateType = productRateDetail.RateType,
                    .DiscountPercentage = 0
                }

                _productServiceDetailRepository.SaveEntity(productServiceDetail)

                If productRateDetail.RateType <> 0 Then
                    productServiceDetail = New ProductServiceDetail With {
                        .PharmaceuticalDispensingDetailId = pharmaceuticalDispensingDetailId,
                        .ServiceOrderDetailId = serviceOrderDetailId,
                        .CUPSEntityId = Nothing,
                        .ContractDescriptionsId = Nothing,
                        .ProductId = productRateDetail.ProductId,
                        .Price = salePriceRateType,
                        .LiquidationType = productRateDetail.LiquidationType,
                        .RateType = productRateDetail.RateType,
                        .DiscountPercentage = 0
                    }

                    _productServiceDetailRepository.SaveEntity(productServiceDetail)
                End If

                _productServiceDetailRepository.UnitWork.Commit()
            End If
        Else
            If FlagDetaiProduction Then
                Dim ProductServiceDetail = New ProductServiceDetail With {
                            .PharmaceuticalDispensingDetailId = pharmaceuticalDispensingDetailId,
                            .ServiceOrderDetailId = serviceOrderDetailId,
                            .CUPSEntityId = Nothing,
                            .ContractDescriptionsId = Nothing,
                            .ProductId = productRateDetail.ProductId,
                            .Price = salePriceRateType,
                            .LiquidationType = productRateDetail.LiquidationType,
                            .RateType = productRateDetail.RateType,
                            .DiscountPercentage = 0
                        }

                _productServiceDetailRepository.SaveEntity(ProductServiceDetail)
            End If
        End If

        salesPrice = salePriceRateType + salesPriceCUPS

        Return New ActionResult(Of (Decimal, ProductRateDetail)) With {.StateResult = True, .ObjectEmbbeded = (salesPrice, productRateDetail)}
    End Function

    ''' <summary>
    ''' validaciones previas al detalle de produccion
    ''' </summary>
    ''' <param name="sod"></param>
    Private Sub ValidateItemToDetailProduction(sod As ServiceOrderDetail)
        If sod Is Nothing Then Throw New IndigoValidationException("Detalle de Órden de servicio no encontrada")

        If sod.ProductId Is Nothing Then Throw New IndigoValidationException("Producto no encontrado")

        If sod.ServiceOrderDetailDistribution.Count > 1 Then Throw New IndigoValidationException("El item se encuentra distribuído")

        If Not sod.ServiceOrder.EntityName.Equals(GetType(PharmaceuticalDispensing).Name) Then Throw New IndigoValidationException("El ítem no fué generado a partir de una dispensación")
    End Sub

    ''' <summary>
    ''' Metodo para re enviar la notificación de la factura electrónica desde el formulario de trazabilidad electrónica
    ''' </summary>
    ''' <param name="listElectronicDocumentNotification">Lista de los registros que se van a persistir</param>
    ''' <returns></returns>
    Private Function SendNotification(listElectronicDocumentNotification As List(Of ElectronicDocumentNotification)) As ActionResult(Of String) Implements ILiquidationAdminService.SendNotification

        Try
            For Each item In listElectronicDocumentNotification
                _electronicDocumentNotificationRepository.SaveEntity(New ElectronicDocumentNotification With
{
                                   .ElectronicDocumentId = item.ElectronicDocumentId,
                                   .Email = item.Email,
                                   .Status = 0,
                                   .CreationDate = DateTime.Now
                               })
                _electronicDocumentNotificationRepository.UnitWork.Commit()
            Next
            Return New ActionResult(Of String) With {.StateResult = True, .Message = "OK"}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para liquidar manualmente las estancias
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StayManualLiquidation(admissionNumber As String, ByVal endDate As DateTime, ByVal audit As AuditMessage) As ActionResult Implements ILiquidationAdminService.StayManualLiquidation
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim listStay = _stayRepository.GetStayByAdmissionNumberManualLiquidation(admissionNumber)
                For Each stay In listStay
                    stay.GENESTLIQ = 3
                    stay.MarkAsModified()
                    _stayRepository.SaveEntity(stay)
                    _stayRepository.UnitWork.Commit()
                Next
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As Exception
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using

    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la cuota de recuperacion
    ''' </summary>
    ''' <param name="ServiceOrderDetailDistributionId"></param>
    ''' <param name="ApplyRecoveryFee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId As Integer, ApplyRecoveryFee As Integer) As ActionResult Implements ILiquidationAdminService.SaveApplyRecoveryFeeServiceOrderDetailDistribution
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionById(ServiceOrderDetailDistributionId)
                ServiceOrderDetailDistribution.ApplyRecoveryFee = ApplyRecoveryFee
                ServiceOrderDetailDistribution.MarkAsModified()
                _serviceOrderDetailDistributionRepository.SaveEntity(ServiceOrderDetailDistribution)
                _serviceOrderDetailDistributionRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As Exception
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Gets the hcregegre by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Public Function GetHCREGEGREByAdmissionCode(admissionCode As String) As Domain.Crystal.Entities.HCREGEGRE Implements ILiquidationAdminService.GetHCREGEGREByAdmissionCode
        Try
            Return _hCREGEGRERepository.GetHCREGEGREByAdmissionCode(admissionCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the admission poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code;LiquidationAdminService.GetAdmissionPOCOByCode</exception>
    Public Function GetAdmissionPOCOByAdmissionCode(admissionCode As String) As ActionResult(Of String) Implements ILiquidationAdminService.GetAdmissionPOCOByAdmissionCode
        If String.IsNullOrEmpty(admissionCode) Then
            Throw New ArgumentNullException("code", "LiquidationAdminService.GetAdmissionPOCOByCode")
        End If
        Dim errorList As New StringBuilder()
        Try
            Dim admission As Object = Me._admissionRepository.GetAdmissionPOCOByCode(admissionCode.Trim())
            If CType(admission, IDictionary(Of String, Object))("AdmissionCaregroupId") Is Nothing Then
                errorList.AppendLine("La admisión no tiene asociada un grupo de atención para Indigo VIE")
            End If
            'If CType(admission, IDictionary(Of String, Object))("PatientCareGroupId") Is Nothing Then
            '    errorList.AppendLine("El paciente no tiene asociado un grupo de atención para Indigo VIE")
            'End If
            'If CType(admission, IDictionary(Of String, Object))("HealthAdministratorId") Is Nothing Then
            '    errorList.AppendLine("La admisión no tiene asociado una entidad administradora para Indigo VIE")
            'End If
            If errorList.Length > 0 Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
            End If
            If admission.PatientEntityId IsNot Nothing Then
                Dim healthAdministrator As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(CInt(admission.PatientEntityId))
                If healthAdministrator IsNot Nothing AndAlso healthAdministrator.Id > 0 Then
                    admission.PatientEntityCode = healthAdministrator.Code
                    admission.PatientEntityName = healthAdministrator.Name
                End If
            End If
            If admission.HealthAdministratorId IsNot Nothing Then
                Dim healthAdministratorAdmission As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(CInt(admission.HealthAdministratorId))
                If healthAdministratorAdmission IsNot Nothing AndAlso healthAdministratorAdmission.Id > 0 Then
                    admission.EntityCode = healthAdministratorAdmission.Code
                    admission.EntityName = healthAdministratorAdmission.Name
                End If
            End If
            If admission IsNot Nothing Then
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(admission)}
            Else
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
                'Return String.Empty
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
            'Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Gets the revenue control poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <param name="careGroupAdmissionId"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code;LiquidationAdminService.GetAdmissionPOCOByCode</exception>
    Public Function GetRevenueControlPOCOByAdmissionCodeWithCareGroup(admissionCode As String,
                                                                      careGroupAdmissionId As Integer,
                                                                      patientCaregroupId As Integer, patientNit As String) As ActionResult(Of String) Implements ILiquidationAdminService.GetRevenueControlPOCOByAdmissionCodeWithCareGroup
        If String.IsNullOrEmpty(admissionCode) Then
            Throw New ArgumentNullException("code", "LiquidationAdminService.GetAdmissionPOCOByCode")
        End If
        Dim errorList As New StringBuilder()
        Try
            Dim admission As Object = New ExpandoObject()
            Dim res As FolioDataHeader = Me._revenueControlRepository.GetControlPOCOByCode(admissionCode)
            If res IsNot Nothing AndAlso res.ListRevenueControlDetails.Any() Then

                admission.CareGroupTypePatient = -1
                admission.PatientEntity = ""
                admission.CareGroupPatientCodeName = ""
                admission.AdmissionCaregroupCodeName = ""

                Dim caregroupAdmission = Me._caregroupRepository.GetCareGroupByIdSimple(careGroupAdmissionId)
                If caregroupAdmission IsNot Nothing AndAlso caregroupAdmission.Id > 0 Then
                    admission.AdmissionCaregroupCodeName = caregroupAdmission.Code & " - " & caregroupAdmission.Name
                    admission.AdmissionTypeLiquidationEmergencyStays = caregroupAdmission.TypeLiquidationEmergencyStays
                End If

                Dim careGroupPatient = Me._caregroupRepository.GetCareGroupByIdSimple(patientCaregroupId)
                If careGroupPatient IsNot Nothing AndAlso careGroupPatient.Id > 0 Then
                    admission.CareGroupTypePatient = careGroupPatient.CareGroupType
                    If careGroupPatient.CareGroupType = 3 Then
                        'tercero
                        Dim third As ThirdParty = _thirdpartyRepository.GetThirdPartyByNit(patientNit)
                        admission.PatientEntity = String.Concat(third.Nit, " - ", third.Name)
                    End If
                    admission.CareGroupPatientCodeName = String.Concat(careGroupPatient.Code, " - ", careGroupPatient.Name)
                End If

                admission.Id = res.Id
                admission.FolioQuantity = res.FolioQuantity
                admission.LiquidationType = res.LiquidationType
                admission.LiquidationTypeName = res.LiquidationTypeName
                admission.ContractCodeName = res.ContractCodeName
                admission.ListRevenueControlDetails = New List(Of Object)()
                admission.ListRevenueControlDetails = res.ListRevenueControlDetails
                Dim setting As CompanySettings = Me._companySettingsRepository.GetCompanySettings()
                admission.SMLV = setting.SMLV
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(admission)}
            Else
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
                'Return String.Empty
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
            'Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Gets the control poco by code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Public Function GetControlPOCOByCode(admissionCode As String) As ActionResult(Of FolioDataHeader) Implements ILiquidationAdminService.GetControlPOCOByCode
        Try
            Dim obj As FolioDataHeader = Me._revenueControlRepository.GetControlPOCOByCode(admissionCode)

            'Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(obj)}
            Return New ActionResult(Of FolioDataHeader) With {.StateResult = True, .ObjectEmbbeded = obj}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            'Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Return New ActionResult(Of FolioDataHeader) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    Public Function GetAdmissionPOCOByCode(code As String) As ActionResult(Of String) Implements ILiquidationAdminService.GetAdmissionPOCOByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code", "LiquidationAdminService.GetAdmissionPOCOByCode")
        End If
        Dim errorList As New StringBuilder()
        Try
            Dim admission As Object = Me._admissionRepository.GetAdmissionPOCOByCode(code.Trim())

            If admission IsNot Nothing Then
                Dim res As FolioDataHeader = Me._revenueControlRepository.GetControlPOCOByCode(code)
                If res IsNot Nothing AndAlso res.ListRevenueControlDetails.Count > 0 Then

                    If CType(admission, IDictionary(Of String, Object))("AdmissionCaregroupId") Is Nothing Then
                        errorList.AppendLine("La admisión no tiene asociada un grupo de atención para Indigo VIE")
                    End If
                    If CType(admission, IDictionary(Of String, Object))("PatientCareGroupId") Is Nothing Then
                        errorList.AppendLine("El paciente no tiene asociado un grupo de atención para Indigo VIE")
                    End If
                    'If CType(admission, IDictionary(Of String, Object))("HealthAdministratorId") Is Nothing Then
                    '    errorList.AppendLine("La admisión no tiene asociado una entidad administradora para Indigo VIE")
                    'End If
                    If errorList.Length > 0 Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
                    End If

                    Dim res1 = Me._caregroupRepository.GetCareGroupById(admission.AdmissionCaregroupId)
                    If res1 IsNot Nothing AndAlso res1.Id > 0 Then
                        admission.AdmissionCaregroupCodeName = res1.Code & " - " & res1.Name
                    End If
                    admission.Id = res.Id
                    admission.FolioQuantity = res.FolioQuantity
                    'admission.LiquidationType = res.LiquidationType
                    admission.LiquidationTypeName = res.LiquidationTypeName
                    admission.ContractCodeName = res.ContractCodeName
                    admission.ListRevenueControlDetails = New List(Of Object)()
                    admission.ListRevenueControlDetails = res.ListRevenueControlDetails

                    Dim setting As CompanySettings = Me._companySettingsRepository.GetCompanySettings()

                    admission.SMLV = setting.SMLV

                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(admission)}
                Else
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
                    'Return String.Empty
                End If
            Else
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
                'Return String.Empty
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = String.Empty}
            'Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta el método de acción seleccionado
    ''' </summary>
    ''' <param name="actionMethod">Método de acción seleccionado</param>
    ''' <param name="arguments">Objeto dinámico con los argumentos del método de acción
    ''' seleccionado, serializado en formato JSON</param>
    ''' <returns>Resultado de la ejecución del método de acción</returns>
    Public Function ExecuteActionMethod(actionMethod As LiquidationActionMethod, arguments As String) As ActionResult(Of String) Implements ILiquidationAdminService.ExecuteActionMethod
        Try
            'Obtenemos el objeto dinámico con los argumentos
            Dim args As Object = Utils.DeserializeJsonToObject(arguments)
            Select Case actionMethod
                Case LiquidationActionMethod.UpdateNoPos
                    Dim res As ActionResult = UpdateNoPos(args.FolioId, CBool(args.IsNoPos))
                    Return New ActionResult(Of String)() With {.StatusCode = res.StatusCode, .Message = res.Message}

                Case LiquidationActionMethod.UpdateDescriptionFolio
                    Dim res As ActionResult = UpdateDescriptionFolio(args.FolioId, args.Description)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.UpdateCategoryFolio
                    Dim res As ActionResult = UpdateCategory(args.FolioId, args.CategoryId)
                    Return New ActionResult(Of String)() With {.StatusCode = res.StatusCode, .Message = res.Message}

                Case LiquidationActionMethod.UpdateObservationFolio
                    Dim res As ActionResult = UpdateObservation(args.FolioId, args.Observation)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.UpdateStatusFolio
                    Dim res As ActionResult = UpdateStatusFolio(args.FolioId, args.StatusFolio)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.LiquidateRecoveryFee 'Liquidar cuota de recuperación
                    'Consultamos el objeto dinamico con los datos del ingreso
                    Dim admission As Object = Nothing
                    Dim liquidateRecovery As Boolean = False 'true - liquida cuota paciente, false - remueve la cuota al paciente
                    If CType(args, IDictionary(Of String, Object)).ContainsKey("liquidateRecovery") AndAlso CType(args, IDictionary(Of String, Object))("liquidateRecovery") = True Then
                        liquidateRecovery = True
                    End If

                    Dim liqType As eLiquidateRecoveryType
                    Select Case CType(args.LiquidationType, Integer)
                        Case 1
                            liqType = eLiquidateRecoveryType.AllItems
                        Case 2
                            liqType = eLiquidateRecoveryType.OnlyOne
                        Case 3
                            liqType = eLiquidateRecoveryType.MultiSelectItems
                    End Select

                    Dim res As ActionResult = Nothing
                    If CType(args, IDictionary(Of String, Object)).ContainsKey("ServiceDistributionList") AndAlso CType(args, IDictionary(Of String, Object))("ServiceDistributionList") IsNot Nothing Then
                        res = Me.LiquidateRecoveryFeeSP(args.TransactionContainer.ToString(), admission, args.IdFolio, args.idDetail, liqType, args.ServiceDistributionList, liquidateRecovery, args)
                    Else
                        res = Me.LiquidateRecoveryFeeSP(args.TransactionContainer.ToString(), admission, args.IdFolio, args.idDetail, liqType, Nothing, liquidateRecovery, args)
                    End If
                    If res IsNot Nothing Then
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                    Else
                        'No se pudo liquidar la cuota
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .ObjectEmbbeded = Nothing, .Message = res.Message}
                    End If

                Case LiquidationActionMethod.BlockFolio
                    Dim res = Me.BlockFolio(args.IdFolio)
                    If res IsNot Nothing Then
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                    Else
                        'No se cambiar el valor
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .ObjectEmbbeded = Nothing, .Message = res.Message}
                    End If

                Case LiquidationActionMethod.UnblockFolio
                    Dim res = Me.BlockFolio(args.IdFolio, False)
                    If res IsNot Nothing Then
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                    Else
                        'No se cambiar el valor
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .ObjectEmbbeded = Nothing, .Message = res.Message}
                    End If

                Case LiquidationActionMethod.ChangeServiceValue
                    Dim res = Me.ChangeServiceValue(args.ServiceOrderId, args.ServiceOrderDetailId, args.NewValue)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.ValidateItemDistribution
                    Dim res = Me.ValidateItemDistribution(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.IncludeInOtherService
                    Dim res = Me.IncludeInOtherService(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.ExcludeOutService
                    Dim res = Me.ExcludeOutService(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.RemoveNoPOSLiquidation
                    Dim res = Me.RemoveNoPOSLiquidation(args)
                    Return New ActionResult(Of String)() With {.StatusCode = res.StatusCode, .StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.GetIdsFolios
                    Dim res = Me.GetIdsFolios(args.IdAdmission)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message, .ObjectEmbbeded = Utils.SerializeObjectToJson(res.ObjectEmbbeded)}

                Case LiquidationActionMethod.DeleteEmptyFolio
                    Dim res = Me.DeleteEmptyFolio(args.IdFolio)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.AddNewFolio
                    Dim res = Me.AddNewFolio(args.IdAdmission, args.CreationUser)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.UpdateUnitValue
                    Dim res = Me.UpdateTotalSalesPrice(args.ServiceOrderDetailDistributionId, args.TotalSalesPrice)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.UpdateAuthorizationNumber
                    Dim res = Me.UpdateAuthorizationNumber(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.SavePatientBonus
                    Dim res = Me.SavePatientBonus(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message, .StatusCode = res.StatusCode}

                Case LiquidationActionMethod.RemovePatientBonus
                    Dim res = Me.RemovePatientBonus(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.AnulateFolio
                    Dim objAudit As Object = DirectCast(args.audit, Object)
                    Dim audit As New AuditMessage()
                    With audit
                        .CodeUser = objAudit.CodeUser
                        .Company = objAudit.Company
                        .CompanyType = objAudit.CompanyType
                        .ComputerName = objAudit.ComputerName
                        .ContainerSecurity = objAudit.ContainerSecurity
                        .Functional = objAudit.Functional
                        .IdUser = objAudit.IdUser
                        .NameUser = objAudit.NameUser
                        .WindowsUser = objAudit.WindowsUser
                    End With
                    Dim res = Me.AnulateFolio(args.RevenueControlDetailIdsToLiquidate, args.OperativeUnitId, args.ReversalReasonId, args.ReversalDescription, args.ContainerHis, args.PatientCode, audit, args.TransactionContainer)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message, .ObjectEmbbeded = res.ObjectEmbbeded, .MessageResult = res.MessageResult}

                Case LiquidationActionMethod.UnPackageItems
                    Dim res = Me.UnPackageItems(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.RecalculateFolio
                    Dim txSettings = New TransactionOptions()
                    txSettings.Timeout = TransactionManager.MaximumTimeout
                    txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                        Dim res = _billingServ.UpdateRevenueControlDetailValues(args.FolioId)

                        If res Is Nothing OrElse Not res.StateResult Then
                            transaction.Dispose()
                            Return New ActionResult(Of String)() With {.StateResult = False, .Message = res?.Message}
                        End If

                        transaction.Complete()
                        Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res?.Message}
                    End Using
                Case LiquidationActionMethod.UpdateThirdPartyFolio
                    Dim res = Me.UpdateThirdPartyFolio(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.UpdateHealthAdministratorFolio
                    Dim res = Me.UpdateHealthAdministratorFolio(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}

                Case LiquidationActionMethod.SavePatientQuotaResponsible
                    Dim res = Me.SavePatientQuotaResponsible(args)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                Case LiquidationActionMethod.CreateFeeNotCollected
                    Dim feeNotCollected As FeeNotCollected = New FeeNotCollected
                    With feeNotCollected
                        .Id = args.FeeNotCollected.Id
                        .RevenueControlDetailId = args.FeeNotCollected.RevenueControlDetailId
                        .ReportType = args.FeeNotCollected.ReportType
                        .Value = args.FeeNotCollected.Value
                        .Observations = args.FeeNotCollected.Observations
                        .CreationUser = args.FeeNotCollected.CreationUser
                    End With
                    Dim res = Me.CreateFeeNotCollected(feeNotCollected)
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                Case LiquidationActionMethod.DeleteFeeNotCollected
                    Dim res = Me.DeleteFeeNotCollected(CInt(args.FeeNotCollectedId), CInt(args.RevenueControlDetailId))
                    Return New ActionResult(Of String)() With {.StateResult = res.StateResult, .Message = res.Message}
                Case Else 'Esto nunca ocurre, se pone para evitar la advertencia del compilador
                    Return Nothing
            End Select
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String)() With {.ObjectEmbbeded = Nothing, .StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda responsable de cuota a paciente
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Private Function SavePatientQuotaResponsible(args As Object) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            Dim patientQuotaResponsibleId As Integer? = If(args.PatientQuotaResponsibleId Is Nothing, CType(Nothing, Integer?), Convert.ToInt32(args.PatientQuotaResponsibleId))

            Dim folioId = CInt(args.FolioId)
            Dim folio = _revenueControlDetailRepository.FirstOrDefault(Function(m) m.Id = folioId)

            If folio.Status <> 1 Then
                Return New ActionResult With {.StateResult = False, .Message = "El folio tiene un estado erróneo para esta operación."}
            End If

            folio.PatientQuotaResponsibleThirdPartyId = patientQuotaResponsibleId
            _revenueControlDetailRepository.SaveEntity(folio)
            unitWork.Commit()

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Includes the in other service.
    ''' </summary>
    ''' <param name="args">The arguments.</param>
    ''' <returns></returns>
    Private Function IncludeInOtherService(args As Object) As ActionResult
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() _
                                                With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim serviceOrderDetailToInclude As List(Of Object) = DirectCast(args.ServiceOrderDetailToInclude, List(Of Object))
                Dim xml As New StringBuilder()
                xml.AppendLine("<Main>")
                xml.AppendLine($"<RevenueControlDetailId>{CInt(args.RevenueControlDetailId)}</RevenueControlDetailId>")
                xml.AppendLine($"<serviceOrderDetailSelected>{CInt(args.serviceOrderDetailMaster)}</serviceOrderDetailSelected>")
                serviceOrderDetailToInclude.ForEach(Sub(o As Integer)
                                                        xml.AppendLine("<Detail>")
                                                        xml.AppendLine($"<serviceOrderDetailId>{o}</serviceOrderDetailId>")
                                                        xml.AppendLine("</Detail>")
                                                    End Sub)
                xml.AppendLine("</Main>")

                Dim response = _revenueControlDetailRepository.IncludeInOtherService(xml.ToString())

                If response Is Nothing OrElse response.CodeResult <> "0" Then
                    scope.Dispose()
                    Return New ActionResult(False, response.MessageResult)
                End If

                Dim flagLiquidateMasterAccount As Boolean? = _settingBillingRepository?.Any(Function(x) x.LiquidateMasterAccount)

                If flagLiquidateMasterAccount Is Nothing OrElse Not flagLiquidateMasterAccount Then
                    scope.Complete()
                    Return New ActionResult(True, "")
                End If

                If Not CType(args, IDictionary(Of String, Object)).ContainsKey("AdmissionNumber") Then
                    scope.Dispose()
                    Return New ActionResult(False, "No se obtuvo el número de ingreso")
                End If

                'se manda a recalcular el folio segun datos de liquidacion.
                Dim result = _folioAdminService.RecalculateFolio(args.AdmissionNumber, args.RevenueControlDetailId, SessionValues.Instance.AuditMessageWcf)

                If result Is Nothing OrElse Not result?.StateResult Then
                    scope.Dispose()
                    _revenueControlDetailRepository.UnitWork.RollbackChanges()
                    Return New ActionResult With {.StateResult = False, .Message = If(String.IsNullOrEmpty(result?.Message), "Error en el servicio de recalcular Nuevo", result?.Message)}
                End If

                scope.Complete()
                Return New ActionResult(True, "")
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Excludes the out service.
    ''' </summary>
    ''' <param name="args">The arguments.</param>
    ''' <returns></returns>
    Private Function ExcludeOutService(args As Object) As ActionResult
        Try
            Dim ServiceOrderDetailToExcludeXml As String = Nothing
            If args.ServiceOrderDetailToInclude IsNot Nothing Then
                ServiceOrderDetailToExcludeXml = String.Join(vbCrLf, CType(args.ServiceOrderDetailToInclude, List(Of Object)) _
                                                                       .Select(Function(o)
                                                                                   Return $"<ServiceOrderDetailToExclude><ServiceOrderDetailId>{CInt(o)}</ServiceOrderDetailId></ServiceOrderDetailToExclude>"
                                                                               End Function))
            End If

            Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "" _
                                     , ServerSessionValues.Current.CurrentContainer)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() _
                                                With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Using connection As New SqlConnection(conx)
                    connection.Open()
                    Dim command As New SqlCommand("[Billing].[SP_ExcludeOutService]")
                    Dim transaction As SqlTransaction
                    transaction = connection.BeginTransaction()
                    Try
                        command.Connection = connection
                        command.Transaction = transaction
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.Add(New SqlParameter("@ServiceOrderDetailToExcludeXml", If(ServiceOrderDetailToExcludeXml Is Nothing, DBNull.Value, ServiceOrderDetailToExcludeXml)))
                        command.Parameters.Add(New SqlParameter("@FolioId", CInt(args.FolioId)))
                        command.Parameters.Add(New SqlParameter("@CareGroupId", CInt(args.CareGroupId)))
                        command.Parameters.Add(New SqlParameter("@PatientGenus", CInt(args.PatientGenus)))
                        command.Parameters.Add(New SqlParameter("@PatientBirth", CDate(args.PatientBirth)))

                        Dim dt As New DataTable()
                        Using adapter As New SqlDataAdapter(command)
                            adapter.SelectCommand.CommandTimeout = 90
                            adapter.Fill(dt)
                        End Using

                        If Not CBool(dt.Rows(0).Item(0)) Then
                            transaction.Rollback()
                            Return New ActionResult(False, dt.Rows(0).Item(1).ToString())
                        End If

                        Dim flagLiquidateMasterAccount As Boolean? = _settingBillingRepository?.Any(Function(x) x.LiquidateMasterAccount)

                        If flagLiquidateMasterAccount Is Nothing OrElse Not flagLiquidateMasterAccount Then
                            transaction.Commit()
                            scope.Complete()
                            Return New ActionResult(True, dt.Rows(0).Item(1).ToString())
                        End If

                        If Not CType(args, IDictionary(Of String, Object)).ContainsKey("AdmissionNumber") Then
                            transaction.Rollback()
                            scope.Dispose()
                            Return New ActionResult(False, "No se obtuvo el número de ingreso")
                        End If

                        'se manda a recalcular el folio segun datos de liquidacion.
                        Dim result = _folioAdminService.RecalculateFolio(args.AdmissionNumber, args.FolioId, SessionValues.Instance.AuditMessageWcf)

                        If result Is Nothing OrElse Not result?.StateResult Then
                            transaction.Rollback()
                            scope.Dispose()
                            Return New ActionResult With {.StateResult = False, .Message = If(String.IsNullOrEmpty(result?.Message), "Error en el servicio de recalcular Nuevo", result?.Message)}
                        End If

                        transaction.Commit()
                        scope.Complete()
                        Return New ActionResult(True, dt.Rows(0).Item(1).ToString())
                    Catch ex As Exception
                        transaction.Rollback()
                        scope.Dispose()
                        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                        Return New ActionResult(False, IndigoManagementExceptions.GetExceptionDetails(ex))
                    Finally
                        connection.Close()
                    End Try
                End Using
            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(False, ResourceManager.GetString("ErrorConcurrence"))
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(False, IndigoManagementExceptions.GetExceptionDetails(ex))
        End Try
    End Function

    ''' <summary>
    ''' Valida 
    ''' </summary>
    ''' <param name="objArgs">The object arguments.</param>
    ''' <returns></returns>
    Private Function ValidateItemDistribution(objArgs As Object) As ActionResult
        Try
            Dim serviceOrderDetail As ServiceOrderDetail
            For Each d In CType(objArgs.ProductsAndServices, List(Of Object)).ToList()
                serviceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(d.ServiceOrderDetailId)
                If serviceOrderDetail.RecordType = eRecordType.Services Then
                    'Esta validación se puede hacer con los items en el proceso de retarificar
                    Dim result As ActionResult(Of List(Of CupsHomologation)) = _contractServices.GetHomologationCups(objArgs.CaregroupId, serviceOrderDetail.CUPSEntityId, serviceOrderDetail.PerformsFunctionalUnitId, serviceOrderDetail.PerformsProfessionalSpecialty, serviceOrderDetail.ServiceDate, serviceOrderDetail.IPSServiceId)
                    If Not result.StateResult Then
                        Return New ActionResult With {.StateResult = False, .Message = result.Message}
                    End If
                End If
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception

            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Anulates the folio.
    ''' </summary>
    ''' <returns></returns>
    Private Function AnulateFolio(RevenueControlDetailIdsToLiquidate As List(Of Object), OperativeUnitId As Integer, ReversalReasonId As Integer, ReversalDescription As String, containerHist As String, patientCode As String, audit As AuditMessage, TransactionContainer As String) As ActionResult(Of String)
        Dim unitWork As IUnitWork = _invoiceRepository.UnitWork
        Dim errorList As New StringBuilder()
        Dim resultList As New List(Of String)()
        Try
            Dim messageNotificationContract As String = ""

            Dim eventValidate = ValidateExistEvent(EventType.Invoice.ToString(), audit.Company)

            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                For Each revenueControlDetailId As Integer In RevenueControlDetailIdsToLiquidate
                    'Primero Actualizamos el folio agregando el número de autorización
                    'Anulación de la factura
                    Dim invoiceId = _invoiceRepository.Query(Function(m) m.RevenueControlDetailId = revenueControlDetailId).Select(Function(m) m.Id).FirstOrDefault()
                    Dim resultAnulateInvoice As ActionResult(Of SP_AnulateInvoice_Result) = Me.AnulateInvoice(revenueControlDetailId, OperativeUnitId, ReversalReasonId, ReversalDescription, containerHist, patientCode, audit, TransactionContainer, eventValidate)
                    If resultAnulateInvoice.StateResult = False Then
                        errorList.AppendLine(resultAnulateInvoice.Message)
                    Else
                        resultList.Add(resultAnulateInvoice.Message)
                        If resultAnulateInvoice.ObjectEmbbeded.NotificationContract IsNot Nothing AndAlso Not resultAnulateInvoice.ObjectEmbbeded.NotificationContract.Trim().Equals(String.Empty) Then
                            messageNotificationContract += resultAnulateInvoice.ObjectEmbbeded.NotificationContract
                        End If

                        Dim res = ReverseBasicBilling(invoiceId, ReversalReasonId, ReversalDescription, operativeUnitId:=OperativeUnitId, session:=New SessionValues With {.AuditMessageWcf = audit})
                        If res.StateResult AndAlso Not String.IsNullOrEmpty(res.Message) Then
                            resultList.Add(res.Message)
                        End If
                    End If
                Next

                If errorList.Length > 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
                End If

                transaction.Complete()
            End Using
            Return New ActionResult(Of String) With {.StateResult = True, .MessageResult = resultList, .Message = messageNotificationContract}
        Catch ex As IndigoValidationException
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try

    End Function

    Private Function ReverseBasicBilling(invoiceId As Integer, reversalReasonId As Integer, reversalDescription As String, operativeUnitId As Integer, session As SessionValues) As ActionResult
        Dim basicBilling = _basicBillingRepository.Query(Function(m) m.InvoiceCopay.Any(Function(o) o.InvoiceId = invoiceId), includes:={"BasicBillingDetail"}).Select(Function(m) m).FirstOrDefault()

        If basicBilling IsNot Nothing Then
            ' Reversion de la cuenta por cobrar para revivir el saldo
            Dim res = _revenueControlRepository.ExecuteStoredProcedure(Of GeneratePortfolioTransferResponse)("[Billing].[SP_ReversePortfolioByBasicBillingIdAsList]", {
                    ("@OperatingUnitId", operativeUnitId),
                    ("@BasicBillingId", basicBilling.Id),
                    ("@AnnulmentDate", Date.Now),
                    ("@ReversalReasonDescription", reversalDescription),
                    ("@CodeUser", session.AuditMessageWcf.CodeUser),
                    ("@CompanyType", session.AuditMessageWcf.CompanyType)
                })

            If res IsNot Nothing AndAlso res(0).Code <> "0" Then
                Throw New IndigoValidationException(res(0).Message)
            End If

            Dim message As String = res(0).Message
            Dim result = _basicBillingAdminService.ReverseBasicBilling(basicBilling, reversalReasonId:=reversalReasonId, reversalReasonDescription:=reversalDescription, session:=session)

            If Not result.StateResult Then
                Throw New IndigoValidationException(result.Message)
            End If

            message &= vbCrLf & result.Message

            Return New ActionResult With {.StateResult = True, .Message = message}
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Anulates the invoice.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Private Function AnulateInvoice(folioId As Integer, OperativeUnitId As Integer, ReversalReasonId As Integer, ReversalDescription As String, containerHis As String, patientCode As String, audit As AuditMessage, TransactionContainer As String, eventValidate As Boolean) As ActionResult(Of SP_AnulateInvoice_Result)
        Dim errorList As New StringBuilder()
        Dim resultMessage As New StringBuilder()
        Try
            Dim settingBilling As SettingsBilling = _settingBillingRepository.GetSettingsBillingByIdUnitOperative(OperativeUnitId, False)
            Dim accountReceivableEntity As AccountReceivable = Nothing
            Dim accountReceivablePatient As AccountReceivable = Nothing
            Dim lstAccountReceivable As List(Of AccountReceivable) = Nothing
            Dim messageNotificationContract As New StringBuilder()

            Dim invoice = _invoiceRepository.GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking(folioId)

            Dim codeNote As String = String.Empty
            Dim companyType As Byte = CByte(audit.CompanyType)

            'Aqui validamos, si es una factura electronica para realizar el registro de la nota Credito por la anulacion
            If String.IsNullOrEmpty(invoice.CUFE) = False Then
                Dim reservation = _sequenseRepository.ReserveNextFormattedCodeByFormId("2037")
                If Not reservation.Success Then
                    Return New ActionResult(Of SP_AnulateInvoice_Result) With {.StateResult = False, .Message = reservation.Message}
                End If
                codeNote = reservation.Code
            End If

            Dim resultAnullateInvoice = _revenueControlDetailRepository.SP_AnulateInvoice(OperativeUnitId, folioId, ReversalReasonId, ReversalDescription, audit.CodeUser, containerHis, patientCode, companyType)
            If resultAnullateInvoice.CodeResult <> 0 Then
                Throw New ArgumentException(resultAnullateInvoice.MessageResult)
            End If

            If invoice.RevenueControlDetail.LiquidationType = 1 Then 'Pago por servicios
                '*************************************************************************
                If String.IsNullOrEmpty(invoice.CUFE) = False Then
                    Dim reversalReason = _billingReversalReasonRepository.GetReversalReasonById(ReversalReasonId, False)
                    Dim observations As String = reversalReason.Description + ": " + ReversalDescription

                    Dim billingNote As BillingNote = New BillingNote() With
                    {
                        .Code = codeNote,
                        .NoteDate = DateTime.Now,
                        .CustomerPartyId = invoice.ThirdPartyId,
                        .Observations = observations,
                        .Nature = 2,
                        .OperatingUnitId = invoice.OperatingUnitId,
                        .EntityId = invoice.Id,
                        .EntityName = invoice.GetType().Name
                    }

                    ' Preparamos dos StringBuilder con capacidad estimada
                    Dim prefixSb As New Text.StringBuilder(billingNote.Code.Length)
                    Dim numberSb As New Text.StringBuilder(billingNote.Code.Length)

                    For Each c As Char In billingNote.Code
                        If Char.IsDigit(c) Then
                            numberSb.Append(c)
                        Else
                            prefixSb.Append(c)
                        End If
                    Next

                    Dim billingNoteDetail As BillingNoteDetail = New BillingNoteDetail() With
                    {
                        .InvoiceId = invoice.Id,
                        .InvoiceNumber = invoice.InvoiceNumber,
                        .CUFE = invoice.CUFE,
                        .DocumentDate = invoice.InvoiceDate,
                        .AdjusmentValue = invoice.InvoiceValue,
                        .BillingValue = invoice.InvoiceValue,
                        .DiscountValue = invoice.ThirdPartyDiscountValue,
                        .ConceptId = 2
                    }

                    billingNote.BillingNoteDetail.Add(billingNoteDetail)

                    Dim unitOfWorkBillingNote As IUnitWork = _billingNoteRepository.UnitWork
                    billingNote.CUDE = billingNote.getCUDE()
                    _billingNoteRepository.SaveEntity(billingNote)
                    unitOfWorkBillingNote.Commit()

                    Dim settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(OperativeUnitId)
                    Dim electronicDocument As ElectronicDocument = New ElectronicDocument() With
                    {
                        .DianVersion = settingsAccount.DianVersion,
                        .OperatingUnitId = billingNote.OperatingUnitId,
                        .CustomerPartyId = billingNote.CustomerPartyId,
                        .EntityId = billingNote.Id,
                        .EntityName = billingNote.GetType().Name,
                        .DocumentDate = billingNote.NoteDate,
                        .DocumentType = billingNote.GetDocumentType(),
                        .Status = 1,
                        .CreationDate = DateTime.Now,
                        .Container = TransactionContainer,
                        .Prefix = prefixSb.ToString(),
                        .DocumentNumber = numberSb.ToString(),
                        .CUFE = billingNote.CUDE,
                        .Year = DateTime.Now.Year
                    }

                    Dim operatingUnit = _operatingUnitRepository.GetOperatingUnitById(OperativeUnitId)
                    electronicDocument.FilePath = System.IO.Path.Combine(
                        Utils.GetPathElectronicDocuments(),
                        electronicDocument.Container,
                        operatingUnit.UnitCode,
                        electronicDocument.DocumentDate.Year.ToString(),
                        electronicDocument.DocumentDate.Month.ToString(),
                        electronicDocument.getDocumentTypeName(),
                        String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                    )

                    Dim unitOfWorkElectronicDocument As IUnitWork = _electronicDocumentRepository.UnitWork
                    _electronicDocumentRepository.SaveEntity(electronicDocument)
                    unitOfWorkElectronicDocument.Commit()
                End If
            End If

            resultMessage.AppendLine(resultAnullateInvoice.MessageResult)

            ''validacion si se debe publicar un evento o no
            If eventValidate Then
                Dim data As Object
                Dim source As String
                Dim jsonSend = ToInvoiceEvent({invoice.Id}.ToList())

                'si la creacion del jsom sale bien hace el proceso de publicacion normal
                If jsonSend.StateResult Then
                    data = jsonSend.ObjectEmbbeded
                    source = EventType.Invoice.ToString()
                Else
                    'si se se genera alguna Exception dentro de la creacion del jsom y enviamos un jsom de error para que quede registrado en la DeadLetterMessageAsync
                    'enviar json de error 
                    Dim errorHandler As New ErrorHandler()

                    errorHandler.InvoiceNumber = invoice.InvoiceNumber
                    errorHandler.DateError = DateTime.Now()
                    errorHandler.Error = jsonSend.Message
                    errorHandler.CodeError = "99"

                    data = errorHandler
                    source = EventType.ErrorHandler.ToString()

                End If
                _eventProxy.Publish(New EventData(data, source, EventAction.annulate.ToString(), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))
            End If

            Return New ActionResult(Of SP_AnulateInvoice_Result) With {.StateResult = True, .ObjectEmbbeded = resultAnullateInvoice, .Message = resultMessage.ToString()}
        Catch ex As ArgumentException
            Return New ActionResult(Of SP_AnulateInvoice_Result) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            Return New ActionResult(Of SP_AnulateInvoice_Result) With {.StateResult = False, .Message = ex.InnerException.Message}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los valores de la órden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailDistributionId">The service order detail distribution identifier.</param>
    ''' <param name="TotalSalesPrice">The total sales price.</param>
    ''' <returns></returns>
    Private Function UpdateTotalSalesPrice(ServiceOrderDetailDistributionId As Integer, TotalSalesPrice As Decimal) As ActionResult(Of String)
        Dim unitWork As IUnitWork = _serviceOrderDetailDistributionRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim _serviceOrderDetailDistribution As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionById(ServiceOrderDetailDistributionId)
                Dim _serviceOrderDetail As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(_serviceOrderDetailDistribution.ServiceOrderDetailId)
                Dim SalePriceIncludeTax = _companySettingsRepository.FirstOrDefault(Function(x) x.Id > 0).SalePriceIncludeTax
                If _serviceOrderDetail.SettlementType = eSettlementType.IncluidoOtroServicio OrElse _serviceOrderDetail.SettlementType = eSettlementType.PorcentajeOtroServicio Then
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = "El valor de este item no se puede modificar debido a que está incluido dentro de otro servicio"}
                End If

                With _serviceOrderDetail
                    Dim percent As Decimal = (1 - (.ThirdPartyDiscountPercentage / 100))
                    Dim _dictionaryValues = Utils.SetValueSalesPrice(SalePriceIncludeTax,
                                                          If(percent > 0, .TotalSalesPrice - (.TotalSalesPrice * percent), .TotalSalesPrice), .TaxPercent)
                    If _dictionaryValues?.Any() Then
                        .SubTotalSalesPrice = Math.Round(_dictionaryValues.FirstOrDefault(Function(g) g.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value, 2, MidpointRounding.AwayFromZero)
                        .GrossValue = Math.Round(_dictionaryValues.FirstOrDefault(Function(g) g.Key = Utils.eTypeTaxControl.GrossValue).Value, 2, MidpointRounding.AwayFromZero)
                        .TaxValue = Math.Round(_dictionaryValues.FirstOrDefault(Function(g) g.Key = Utils.eTypeTaxControl.TaxValue).Value, 2, MidpointRounding.AwayFromZero)
                    End If

                    .TotalSalesPrice = .SubTotalSalesPrice + (.SubTotalSalesPrice * percent)
                    .ThirdPartyDiscount = .SubTotalSalesPrice * (.ThirdPartyDiscountPercentage / 100)
                    .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
                End With

                With _serviceOrderDetailDistribution
                    .GrandTotalSalesPrice = _serviceOrderDetail.GrandTotalSalesPrice
                    .ThirdPartySalesPrice = .GrandTotalSalesPrice
                    .ThirdPartyPercentage = 100
                    .SubTotalPatientSalesPrice = 0
                    .PatientPercentage = 0
                    .RecoveryFeeType = 1
                    .ApplyRecoveryFee = 1
                    .ServiceOrderDetail = _serviceOrderDetail
                End With
                _serviceOrderDetailDistributionRepository.SaveEntity(_serviceOrderDetailDistribution)
                unitWork.Commit()
                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Private Function UpdateAuthorizationNumber(args As Object) As ActionResult(Of String)
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                For Each ServiceOrderDetailId As Integer In CType(args.serviceOrderDetailList, List(Of Object))
                    Dim _serviceOrderDetail As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(ServiceOrderDetailId)
                    With _serviceOrderDetail
                        .AuthorizationNumber = args.AuthorizationNumber.ToString()
                    End With
                    _serviceOrderDetailRepository.SaveEntity(_serviceOrderDetail)
                    unitWork.Commit()
                Next

                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function SavePatientBonus(args As Object) As ActionResult
        Try
            Dim unitWork As IUnitWork = _revenueControlDetailRepository.UnitWork
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                'Obtener el parametro de facturacion
                Dim billinSettings As SettingsBilling = _settingBillingRepository.GetSettingsBillingByIdUnitOperative(CInt(args.OperativeUnitId), False)
                Dim folio As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailById(CInt(args.FolioId))
                Dim serviceorderDetailDistributionListId = _serviceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailId(folio.Id)


                Dim BonusValue = Utils.RoundValue(args.BonusValue, billinSettings.RoundingTypeRecoveryFeeType)

                folio.TotalPatientWithDiscount = BonusValue
                Select Case args.ShareType
                    Case 2 'cuota moderadora
                        folio.RevenueControl.TopEventFeeModerator += BonusValue
                        folio.ValueFeeModerator = BonusValue
                    Case 3 'copago
                        folio.RevenueControl.TopEventCopay += BonusValue
                        folio.ValueCopay = BonusValue
                    Case 4 'bono
                        folio.ValueVoucher = BonusValue

                End Select
                folio.TotalPatientSalesPrice = BonusValue

                Dim percentage As Double = (BonusValue * 100) / args.TotalEntity

                Dim serv As ServiceOrderDetailDistribution = Nothing
                For Each soddId As Integer In serviceorderDetailDistributionListId

                    Dim invalidId = CType(args.ListItemsApplyRecoveryFee, List(Of Object)).Find(Function(x) x = soddId)
                    If invalidId = 0 Then
                        Continue For
                    End If

                    If BonusValue = 0 Then
                        Exit For
                    End If
                    serv = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)

                    Dim value = Utils.RoundValue(Convert.ToDecimal((serv.GrandTotalSalesPrice * percentage) / 100), billinSettings.RoundingTypeRecoveryFeeType)
                    If BonusValue > value Then
                        serv.SubTotalPatientSalesPrice = value
                        BonusValue -= value
                    Else
                        serv.SubTotalPatientSalesPrice = BonusValue
                        BonusValue = 0
                    End If

                    serv.RecoveryFeeType = args.ShareType
                    serv.ApplyRecoveryFee = 2 'Se cobro
                    serv.PatientPercentage = IIf(serv.SubTotalPatientSalesPrice = 0, 0, percentage)  'Porcentaje aplicado
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                    serv.ThirdPartyPercentage = 100 - serv.PatientPercentage
                Next
                If BonusValue > 0 Then
                    serv.SubTotalPatientSalesPrice += BonusValue
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                End If

                _revenueControlDetailRepository.SaveEntity(folio)
                unitWork.Commit()

                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function RemovePatientBonus(args As Object) As ActionResult
        Try
            Dim unitWork As IUnitWork = _revenueControlDetailRepository.UnitWork
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim folio As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailById(CInt(args.FolioId))
                folio.ValueVoucher = 0

                Dim serviceorderDetailDistributionListId = _serviceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailIdToRemove(folio.Id)

                folio.TotalPatientWithDiscount = 0
                folio.ValueVoucher = 0

                For Each soddId As Integer In serviceorderDetailDistributionListId
                    Dim serv As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)


                    Select Case serv.RecoveryFeeType
                        Case 2 'cuota moderadora
                            folio.RevenueControl.TopEventFeeModerator -= serv.SubTotalPatientSalesPrice
                            folio.ValueFeeModerator -= serv.SubTotalPatientSalesPrice
                        Case 3 'copago
                            folio.RevenueControl.TopEventCopay -= serv.SubTotalPatientSalesPrice
                            folio.ValueCopay -= serv.SubTotalPatientSalesPrice
                    End Select

                    folio.TotalPatientSalesPrice = 0

                    If serv.RecoveryFeeType = 2 Then 'cuota moderadora
                        folio.RevenueControl.TopEventFeeModerator -= serv.SubTotalPatientSalesPrice
                    Else 'copago
                        folio.RevenueControl.TopEventCopay -= serv.SubTotalPatientSalesPrice
                    End If

                    serv.RecoveryFeeType = 1 'ninguna
                    serv.ApplyRecoveryFee = 1 'Se cobro
                    serv.PatientPercentage = 0 'Porcentaje aplicado
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice 'Se calcula el valor de la entidad
                    serv.ThirdPartyPercentage = 100
                    serv.SubTotalPatientSalesPrice = 0
                Next
                _revenueControlDetailRepository.SaveEntity(folio)
                unitWork.Commit()

                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC As List(Of ProductATC), AdmissionCode As String, caregroupId As Integer) As ActionResult(Of List(Of ProductATC)) Implements ILiquidationAdminService.GetATCPOSByATCNoPOSAndAdmissionCode
        Try
            Dim res As List(Of ProductATC) = _iHCJUNOPMHRepository.GetHCJUNOPMH(listProductATC, AdmissionCode)
            If res IsNot Nothing AndAlso res.Count > 0 Then
                'Ahora como ya consultamos el atc del producto POS ahora consultamos el producto por defecto para ese ATC, si no hay ninguno entonces elimino el atc POS de la lista
                res.ForEach(Sub(o)
                                If Not String.IsNullOrEmpty(o.ATCCodePOS) Then
                                    Dim productNoPOS = _inventoryProductRepository.GetInventoryProductById(o.ProductIdNoPOS, False)
                                    Dim sod As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(o.ServiceOrderDetailId, False)
                                    Dim productRateDetailNoPOS As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(caregroupId, productNoPOS.Id, sod.ServiceDate)

                                    If productRateDetailNoPOS IsNot Nothing AndAlso productRateDetailNoPOS.Id > 0 Then
                                        o.UnitValueProductNoPOS = productRateDetailNoPOS.SalesValue
                                        Dim product = _inventoryProductRepository.GetFirstProductByATCCode(o.ATCCodePOS)
                                        If product IsNot Nothing AndAlso product.Id > 0 Then
                                            o.DefaultProductCodePOS = product.Code
                                            o.DefaultProductIdPOS = product.Id
                                            o.DefaultProductNamePOS = product.Name

                                            Dim productRateDetail As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(caregroupId, product.Id, sod.ServiceDate)
                                            If productRateDetail IsNot Nothing AndAlso productRateDetail.Id > 0 Then
                                                o.DefaultProductUnitValue = productRateDetail.SalesValue
                                                o.DefaultProductTotalValue = productRateDetail.SalesValue * o.Quantity
                                                If productRateDetailNoPOS.SalesValue > productRateDetail.SalesValue Then
                                                    o.UnitValueDifference = productRateDetailNoPOS.SalesValue - productRateDetail.SalesValue
                                                    o.TotalValueDifference = (productRateDetailNoPOS.SalesValue - productRateDetail.SalesValue) * o.Quantity
                                                Else
                                                    o.UnitValueDifference = 0
                                                    o.TotalValueDifference = 0
                                                End If
                                            Else
                                                o.UnitValueDifference = 0
                                                o.TotalValueDifference = 0
                                            End If
                                        Else
                                            o.UnitValueDifference = 0
                                            o.TotalValueDifference = 0
                                            o.ATCCodePOS = String.Empty
                                        End If
                                    Else
                                        o.UnitValueDifference = 0
                                        o.TotalValueDifference = 0
                                        o.ATCCodePOS = String.Empty
                                    End If
                                End If
                            End Sub)
                '''res.RemoveAll(Function(x) String.IsNullOrEmpty(x.ATCCodePOS))
                Return New ActionResult(Of List(Of ProductATC)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = res}
            Else
                Return New ActionResult(Of List(Of ProductATC)) With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontraton productos homólogos POS"}
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ProductATC)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As ActionResult _
        Implements ILiquidationAdminService.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm
        Try
            Dim results = _pharmaceuticalDispensingRepository.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber)
            If results IsNot Nothing AndAlso results.Any(Function(o) Not String.IsNullOrEmpty(o.Codes)) Then
                Return New ActionResult With {.StatusCode = eStatusResult.WARNING,
                    .MessageResult = New List(Of String)({results.Find(Function(o) o.TypeDispensing = 1).Codes,
                        results.Find(Function(o) o.TypeDispensing = 2).Codes})}
            Else
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Liquida un folio
    ''' </summary>
    ''' <param name="BillingAuthorizationId">The billing authorization identifier.</param>
    ''' <returns></returns>
    Private Async Function LiquidateFolio(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), patientCode As String, admissionNumber As String, containerCrystal As String, BillingAuthorizationId As Decimal, OperativeUnitId As Integer, ThirdPartyPatientId As Integer, skipAccountControlValidations As Boolean, audit As AuditMessage, session As SessionValues) As Threading.Tasks.Task(Of ActionResult(Of List(Of InvoiceResult))) Implements ILiquidationAdminService.LiquidateFolio
        Try
            Dim resValidationStay = ValidatePendingStays(revenueControlDetailCrossingList, admissionNumber, OperativeUnitId, skipAccountControlValidations, audit)
            If Not resValidationStay.StateResult AndAlso resValidationStay.StatusCode = eStatusResult.EXCEPTION Then
                Return New ActionResult(Of List(Of InvoiceResult)) With {.StateResult = False, .Message = resValidationStay.Message}
            End If

            Dim revenueControlDetailCrossingListXml As String = Me.GenerateRevenueControlDetailCrossingListXml(revenueControlDetailCrossingList, session)
            Dim eventValidate = ValidateExistEvent(EventType.Invoice.ToString(), audit.Company)

            Dim linvoices As New List(Of InvoiceResult)()
            Dim messageResult As String = Nothing

            'Liquidar Factura
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.IsolationLevel = IsolationLevel.ReadCommitted, .Timeout = TransactionManager.MaximumTimeout}, TransactionScopeAsyncFlowOption.Enabled)
                Try
                    Dim result = _revenueControlRepository.LiquidateFolio(patientCode, admissionNumber, containerCrystal, BillingAuthorizationId, OperativeUnitId, ThirdPartyPatientId, audit.CodeUser, audit.CompanyType, If(String.IsNullOrEmpty(revenueControlDetailCrossingListXml), Nothing, revenueControlDetailCrossingListXml), skipAccountControlValidations)
                    If result Is Nothing OrElse result.Any(Function(r) Not r.StatusResult) Then
                        Dim message = "No se encontró un resultado"
                        If result IsNot Nothing Then
                            message = result.FirstOrDefault(Function(r) Not r.StatusResult).Message
                        End If
                        scope.Dispose()
                        Return New ActionResult(Of List(Of InvoiceResult))(False, Nothing, message, Nothing)
                    End If

                    For Each r As LiquidateFolio_Result In result
                        linvoices.Add(New InvoiceResult() With {.InvoiceId = r.InvoiceId, .InvoiceNumber = r.InvoiceNumber})
                    Next

                    messageResult = result(0).Message
                    If Not resValidationStay.StateResult Then
                        Dim messages = messageResult.Split(vbCrLf).Where(Function(m) Not m.Equals(vbCrLf) AndAlso Not m.Equals(vbLf) AndAlso Not m.Equals(String.Empty)).ToList()
                        messages.Add("Existen ítems por generar en control de cuentas hospitalario")
                        messageResult = String.Join(vbCrLf, messages)
                    Else
                        Dim resBasicBilling = Await GenerateBasicBillingCopay(
                        invoiceIds:=result.Select(Function(m) m.InvoiceId).ToList(),
                        folioIds:=revenueControlDetailCrossingList.Select(Function(m) m.RevenueControlDetailId).ToList(),
                        revenueControlDetailCrossingList:=revenueControlDetailCrossingList,
                        operativeUnitId:=OperativeUnitId,
                        thirdPartyId:=ThirdPartyPatientId,
                        currencyId:=session.OfficialCurrencyId,
                        session:=session,
                        companyType:=audit.CompanyType
                    )

                        If resBasicBilling.Length > 0 Then
                            result(0).MessageResult &= resBasicBilling
                        End If
                    End If

                    If eventValidate Then

                        Dim data As Object
                        Dim source As String

                        Dim invoiceIds = linvoices.Select(Function(m) m.InvoiceId).ToList()
                        Dim jsonSend = ToInvoiceEvent(invoiceIds)

                        'si la creacion del jsom sale bien hace el proceso de publicacion normal
                        If jsonSend.StateResult Then
                            data = jsonSend.ObjectEmbbeded
                            source = EventType.Invoice.ToString()
                        Else
                            'si se se genera alguna Exception dentro de la creacion del jsom y enviamos un jsom de error para que quede registrado en la DeadLetterMessageAsync
                            'enviar json de error
                            Dim errorHandler As New ErrorHandler()

                            errorHandler.InvoiceNumber = String.Join(", ", linvoices.Select(Function(inv) inv.InvoiceNumber))
                            errorHandler.DateError = DateTime.Now()
                            errorHandler.Error = jsonSend.Message
                            errorHandler.CodeError = "99"

                            data = errorHandler
                            source = EventType.ErrorHandler.ToString()
                        End If

                        _eventProxy.Publish(New EventData(data, source, EventAction.added.ToString(), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))

                    End If

                    Dim invoiceMessageResult = result(0).MessageResult.ToString()

                    scope.Complete()
                    Return New ActionResult(Of List(Of InvoiceResult))(True, invoiceMessageResult.Split("@").ToList().FindAll(Function(x) Not x.Equals("")), messageResult, linvoices)
                Catch ex As Exception
                    scope.Dispose()
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                    Return New ActionResult(Of List(Of InvoiceResult))(False, Nothing, Utils.GetInnerExceptionMessageToString(ex), Nothing)
                End Try

            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of InvoiceResult))(False, Nothing, Utils.GetInnerExceptionMessageToString(ex), Nothing)
        End Try
    End Function

    ''' <summary>
    ''' Generación de la factura copago y cuotas moderadoras
    ''' </summary>
    ''' <param name="invoiceIds"></param>
    ''' <param name="folioIds"></param>
    ''' <param name="revenueControlDetailCrossingList"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="currencyId"></param>
    ''' <param name="session"></param>
    ''' <param name="companyType"></param>
    ''' <returns></returns>
    Private Async Function GenerateBasicBillingCopay(invoiceIds As List(Of Integer), folioIds As List(Of Integer),
                                               revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing),
                                               operativeUnitId As Integer,
                                               thirdPartyId As Integer,
                                               currencyId As Integer,
                                               session As SessionValues,
                                               companyType As Integer) As Threading.Tasks.Task(Of String)
        Dim message As String = ""
        Dim invoices = _invoiceRepository.Query(Function(m) invoiceIds.Contains(m.Id)).Select(Function(m) New With {
                                                                                                  Key .InvoiceId = m.Id,
                                                                                                  Key .ThirdPartyId = m.ThirdPartyId,
                                                                                                  Key .RevenueControlDetailId = m.RevenueControlDetailId,
                                                                                                  Key .AdmissionNumber = m.AdmissionNumber}).ToList()

        ' condicional solo si existe valor a paciente
        Dim revenueControlDetails = _revenueControlDetailRepository.GetByFilter(Function(m) folioIds.Contains(m.Id)).ToList()

        'lista de Ids de los grupos de atencion
        Dim listIdsCareGroup = revenueControlDetails.Select(Function(x) x.CareGroupId).ToList()
        'obtencion del listado de grupos de atencion
        Dim listCareGroup = _caregroupRepository?.Query(Function(m) listIdsCareGroup.Contains(m.Id), False, {"BillingConcept"})?.ToList()

        'validacion si no viene un grupo de atencion
        If listCareGroup Is Nothing OrElse Not listCareGroup.Any() Then
            Throw New IndigoValidationException("Grupo de atención no encontrado.")
        End If

        If revenueControlDetails.Any(Function(m) m.TotalPatientWithDiscount > 0 AndAlso m.IsMasterAccount <> 4 AndAlso listCareGroup.Any(Function(e) e.Id = m.CareGroupId AndAlso e.CareGroupType <> 3)) Then
            Dim billingSetting = _settingBillingRepository.FirstOrDefault(Function(m) m.IdOperatingUnit = operativeUnitId)
            If billingSetting Is Nothing OrElse Not billingSetting.BillingAuthorizationCopayId.HasValue Then
                Throw New IndigoValidationException("No se ha configurado la Resolución de factura electrónica copago en los parámetros de facturación")
            End If

            Dim third = _thirdpartyRepository.FirstOrDefault(Function(m) m.Id = thirdPartyId, includes:={"Person.Address"})
            If third Is Nothing Then
                Throw New IndigoValidationException("Tercero no encontrado.")
            End If

            If Not third.Person.Address.Any() Then
                Throw New IndigoValidationException("No se encontró una dirección relacionada al tercero.")
            End If

            Dim customerId = _customerRepository.Query(Function(m) m.ThirdPartyId = thirdPartyId).Select(Function(m) m.Id)?.FirstOrDefault()

            For Each item In revenueControlDetailCrossingList '.FindAll(Function(m) m.) filtrar solo los que tengan valor a paciente
                Dim revenueControlDetail = revenueControlDetails.FirstOrDefault(Function(m) m.Id = item.RevenueControlDetailId)

                If revenueControlDetail.TotalPatientWithDiscount > 0 Then
                    Dim invoice = invoices.FirstOrDefault(Function(m) m.RevenueControlDetailId = item.RevenueControlDetailId)
                    Dim functionalUnitId As Integer = 0

                    Dim egre = _hCREGEGRERepository.FirstOrDefault(Function(m) m.NUMINGRES = invoice.AdmissionNumber)
                    If egre Is Nothing Then
                        Dim ufuCodigo = _admissionRepository.Query(Function(m) m.NUMINGRES = invoice.AdmissionNumber).Select(Function(m) m.UFUCODIGO).FirstOrDefault()
                        functionalUnitId = _functionalUnitRepository.Query(Function(m) m.Code = ufuCodigo).Select(Function(m) m.Id).FirstOrDefault()
                    Else
                        functionalUnitId = _functionalUnitRepository.Query(Function(m) m.Code = egre.UFUCODIGO).Select(Function(m) m.Id).FirstOrDefault()
                    End If

                    If functionalUnitId = 0 Then
                        Throw New IndigoValidationException("Fact.Copago: El código de unidad funcional asociada no existe.")
                    End If

                    'Se establece el tercero responsable del copago  = tercero de la factura Salud Inicialmente. --> transmision xml como fact mandato -11
                    Dim thirdPartyEntityCopayId As Integer? = invoice.ThirdPartyId

                    Dim careGroup = listCareGroup?.FirstOrDefault(Function(m) m.Id = revenueControlDetail.CareGroupId)
                    If careGroup Is Nothing Then
                        Throw New IndigoValidationException("No se encontró el grupo de atención")
                    End If

                    'valida si el tipo de liquidacion es PGP O CAPITA y que el parametro "Tercero factura copago" este en "Tercero Responsable de Pago"
                    If New List(Of Byte) From {2, 5}.Contains(careGroup?.LiquidationType) AndAlso careGroup.ThirdPartyInvoiceCopayFixedAmount = 1 Then
                        'Se establece el tercero responsable del copago  = Tercero cliente --> transmision xml como fact comercial -10
                        thirdPartyEntityCopayId = thirdPartyId
                    End If

                    Dim basicBilling = New BasicBilling With {
                        .Code = "",
                        .DocumentDate = Date.Now,
                        .Description = $"Ingreso paciente ({third.Nit} - {third.Name}) Factura copagos/cuotas moderadoras",
                        .SaleModality = 1, ' 1 - Contado, 2 - Crédito
                        .Status = 1,
                        .OperatingUnitId = operativeUnitId,
                        .BillingAuthorizationId = billingSetting.BillingAuthorizationCopayId.Value,
                        .CustomerId = If(customerId, 0),
                        .ThirdPartyCustomerId = thirdPartyId,
                        .AddressId = third.Person.Address.FirstOrDefault().Id,
                        .WarehouseId = Nothing,
                        .InvoiceId = invoice.InvoiceId, ' este es una factura que se genera a partir de la factura basica
                        .Value = revenueControlDetail.TotalPatientWithDiscount,
                        .ValueDiscount = revenueControlDetail.PatientDiscount,
                        .ValueIVA = 0,
                        .WithholdingTax = 0,
                        .RetentionIdIVA = billingSetting.ReteIVAConceptId,
                        .RetentionPercentageIVA = 0,
                        .WithholdingIVA = 0,
                        .WithholdingICA = 0,
                        .TotalValue = revenueControlDetail.TotalPatientWithDiscount,
                        .RoundLevel = 2,
                        .BudgetId = If(careGroup?.AffectBudget, careGroup.PromissoryNoteBudgetId, Nothing),
                        .CurrencyId = currencyId,
                        .FunctionalUnitId = 0,
                        .ThirdPartyEntityCopayId = thirdPartyEntityCopayId
                    }

                    Dim billingConceptCopay = careGroup?.BillingConcept
                    If billingConceptCopay Is Nothing Then
                        Throw New IndigoValidationException("No se encontró concepto de facturación asociado al grupo de atención")
                    End If

                    Dim retentionIdTax As Integer? = Nothing
                    If billingConceptCopay.WithholdingICAConceptId IsNot Nothing Then
                        retentionIdTax = _retentionConceptRepository.Query(Function(m) m.Id = billingConceptCopay.WithholdingICAConceptId).Select(Function(m) m.Id).FirstOrDefault()
                    End If

                    Dim basicBillingDetail As New BasicBillingDetail With {
                        .DetailType = 2,
                        .BillingConceptId = billingConceptCopay.Id, 'analizar lógica del requerimiento
                        .SupplierId = 0,
                        .FunctionalUnitId = functionalUnitId,
                        .RetentionIdTax = retentionIdTax,
                        .Quantity = 1,
                        .Price = revenueControlDetail.TotalPatientWithDiscount,
                        .PercentageDiscount = 0,
                        .PercentageIVA = 0,
                        .Value = revenueControlDetail.TotalPatientWithDiscount
                    }

                    basicBilling.BasicBillingDetail.Add(basicBillingDetail)

                    Dim resBasicBilling = Await _basicBillingAdminService.SaveAndConfirmBasicBillingAsync(basicBilling, Nothing, session)
                    ' crear registro que relacione la factura con la factura basica de copagos
                    If Not resBasicBilling.StateResult OrElse Not resBasicBilling.StateResultAux Then
                        Throw New IndigoValidationException(resBasicBilling.Message)
                    End If

                    Dim invoiceId = _basicBillingRepository.Query(Function(m) m.Id = basicBilling.Id).AsNoTracking().Select(Function(m) m.InvoiceId).FirstOrDefault()
                    ' Cambiamos el tipo a 6: Copagos
                    Dim accountReceivable = _accountReceivableRepository.FirstOrDefault(Function(m) m.InvoiceId = invoiceId)
                    accountReceivable.AccountReceivableType = 6
                    _accountReceivableRepository.SaveEntity(accountReceivable)
                    _accountReceivableRepository.UnitWork.Commit()

                    Dim invoiceCopay As New InvoiceCopay With {
                    .InvoiceId = invoice.InvoiceId,
                    .BasicBillingId = resBasicBilling.ObjectEmbbeded.Id
                    }

                    _invoiceCopayService.Create(invoiceCopay, audit:=session.AuditMessageWcf)

                    message &= resBasicBilling.Message.Replace($"Se actualizo y se confirmo correctamente{vbCrLf}", "")

                    ' Cruce
                    If item.ListPortfolioAdvance?.Any() Then
                        Dim listPortfolioAdvanceCrossingXml = String.Join(vbCrLf, item.ListPortfolioAdvance _
                                                                          .Where(Function(x) x.PortfolioAdvanceType = 2).Select(Function(o)
                                                                                                                                    Return $"
                                            <ListPortfolioAdvanceCrossing>
                                                <Id>{o.Id}</Id>
                                                <Code>{o.Code}</Code>
                                                <CrossingValue>{o.CrossingValue.ToString().Replace(",", ".")}</CrossingValue>
                                                <CashReceiptDetailIdTmp>{o.CashReceiptDetailIdTmp}</CashReceiptDetailIdTmp>
                                            </ListPortfolioAdvanceCrossing>"
                                                                                                                                End Function))

                        Dim res = _revenueControlRepository.ExecuteStoredProcedure(Of GeneratePortfolioTransferResponse)("[Portfolio].[SP_GeneratePortfolioTransferAsList]", {
                        ("@ListPortfolioAdvanceCrossingXml", listPortfolioAdvanceCrossingXml),
                        ("@OperativeUnitId", operativeUnitId),
                        ("@UserCode", session.AuditMessageWcf.CodeUser),
                        ("@AccountReceivableId", accountReceivable.Id),
                        ("@CompanyType", companyType)
                        })

                        If res IsNot Nothing AndAlso res(0).Code <> "0" Then
                            Throw New IndigoValidationException(res(0).Message)
                        End If
                        message &= vbCrLf & res(0).Message
                    End If
                End If
            Next
        End If

        Return message
    End Function

    ''' <summary>
    ''' se valida si el cliente tiene un evento para ser publicado dependiendo del tipo de evento que se envie, en este caso mayormente facturacion que solo mente esta funcional para costa rica
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="company"></param>
    ''' <returns></returns>
    Public Function ValidateExistEvent(type As String, company As String) As Boolean
        Dim securityContainer = ConfigurationManager.AppSettings.Get("containerSecurity")
        Dim validateEventConfiguration As EventConfiguration = Nothing

        Try
            Using transaction As New TransactionScope(TransactionScopeOption.Suppress)
                ''se realiza la consulta a la tabla eventconfiguration
                Using context As New SecurityContext(
                        Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, securityContainer, True)
                    )
                    validateEventConfiguration = (From ec In context.EventsConfiguration.AsNoTracking()
                                                  Where ec.Container.Code = company And ec.Code = type).SingleOrDefault()
                End Using

                transaction.Complete()
            End Using

            ''si existe un evento a publicar se retorna true
            Return validateEventConfiguration IsNot Nothing
        Catch ex As Exception
            Dim message = $"Error validando evento '{type}' compañía '{company}' (containerSecurity='{securityContainer}'): {ex}"
            Throw New Exception(message)
        End Try


    End Function

    ''' <summary>
    ''' Validaciones de estancias
    ''' </summary>
    ''' <param name="revenueControlDetailCrossingList"></param>
    ''' <param name="audit"></param>
    Private Function ValidatePendingStays(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing),
                                     admissionNumber As String,
                                     operativeUnitId As Integer,
                                     skipAccountControlValidations As Boolean,
                                     audit As AuditMessage) As ActionResult
        'Generamos las estancias pendientes por liquidar
        Dim revenueControlId = revenueControlDetailCrossingList(0).RevenueControlId
        Dim pendingQuantity = _revenueControlDetailRepository.Query(Function(m) m.RevenueControlId = revenueControlId AndAlso m.Status = 1).Count()
        Dim accountControlValidation = _settingBillingRepository.Query(Function(m) m.IdOperatingUnit = operativeUnitId).Select(Function(m) m.AccountControlValidation).FirstOrDefault()

        If accountControlValidation AndAlso Not skipAccountControlValidations Then
            Try
                _stayService.ListOfStaysWithConfigurationByAdmissionToModel(admissionNumber, eLiquidateStayOption.DefectoManualGrupoAtencion, Nothing, audit, Nothing)
            Catch ex As IndigoValidationException
                Return New ActionResult With {.StateResult = False, .StatusCode = If(ex.Source = "999", eStatusResult.WARNING, eStatusResult.EXCEPTION), .Message = ex.Message}
            Catch ex As Exception
                Dim statusCode = IIf(pendingQuantity = revenueControlDetailCrossingList.Count, eStatusResult.EXCEPTION, eStatusResult.WARNING)
                Return New ActionResult With {.StateResult = False, .StatusCode = statusCode, .Message = "-001"}
            End Try
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    Private Function ToInvoiceEvent(invoiceIds As List(Of Integer)) As ActionResult(Of List(Of InvoiceEvent))
        Try
            Dim data = _invoiceRepository.ExecuteQueryDR(Of VReportInvoice)($"SELECT * FROM Billing.VReportInvoice (NOLOCK) WHERE Id In ({String.Join(",", invoiceIds)})", {})

            Dim result = data.Select(Function(m) New InvoiceEvent With {
                .InvoiceNumber = m.InvoiceNumber,
                .InvoiceDate = m.InvoiceDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                .CutType = m.CutType,
                .Observation = m.Observation,
                .DocumentType = m.DocumentType,
                .SubTotalService = m.SubTotalService,
                .ThirdPartyDiscountValue = m.ThirdPartyDiscountValue,
                .TotalPatientSalesPrice = m.TotalPatientSalesPrice,
                .PatientDiscount = m.PatientDiscount,
                .TotalPatientAccountReceivable = m.TotalPatientAccountReceivable,
                .ThirdPartySalesValue = m.ThirdPartySalesValue,
                .Status = m.Status,
                .DocumentTypename = m.DocumentTypeName,
                .InvoiceCategory = If(String.IsNullOrEmpty(m.InvoiceCategory), Nothing, New InvoiceCategoryEvent With {
                    .Code = m.InvoiceCategory.Split("-")(0).Trim(),
                    .Name = m.InvoiceCategory.Split("-")(1).Trim()
                }),
                .User = New UserEvent With {.Code = m.UserCode, .Name = m.FullNameUser},
                .Client = If(String.IsNullOrEmpty(m.Nit), Nothing, New ClientEvent With {
                    .IdentificationType = 1,
                    .Identification = m.Nit.Split("-")(0).Trim(),
                    .DigitVerification = m.Nit.Split("-")(1).Trim(),
                    .FullName = m.Name,
                    .Address = m.ThirdPartyAddress,
                    .Phone = m.ThirdPartyPhone
                }),
                .CareGroup = If(String.IsNullOrEmpty(m.CareGroup), Nothing, New CareGroupEvent With {
                    .Code = m.CareGroup.Split("-")(0).Trim(),
                    .Name = m.CareGroup.Split("-")(1).Trim(),
                    .Type = m.CareGroupType
                }),
                .HealthAdministrator = If(String.IsNullOrEmpty(m.DescriptionHealthAdministrator), Nothing, New HealthAdministratorEvent With {
                    .Code = m.DescriptionHealthAdministrator.Split("-")(0).Trim(),
                    .Name = m.DescriptionHealthAdministrator.Split("-")(1).Trim(),
                    .HealthEntityCode = m.HealthEntityCode
                }),
                .Contract = If(String.IsNullOrEmpty(m.Contract), Nothing, New ContractEvent With {
                    .ContractNumber = m.Contract?.Split("-")(0).Trim(),
                    .Name = m.Contract?.Split("-")(1).Trim()
                }),
                .Patient = New PatientEvent With {
                    .IdentificationType = MapIdentificationType(m.IdentificationTypeCode),
                    .Identification = m.PatientCode,
                    .FullName = m.PatientName,
                    .PatientType = m.PatientType,
                    .PatientLevel = If(String.IsNullOrEmpty(m.PatientLevel), Nothing, New PatientLevelEvent With {
                        .Code = m.PatientLevel.Split("-")(0).Trim(),
                        .Name = m.PatientLevel.Split("-")(1).Trim()
                    }),
                    .Address = m.PatientAddress,
                    .Phone = m.PatientTelephoneNumber,
                    .PhoneMovil = m.PatientPhoneMovil
                },
                .Admission = New AdmissionEvent With {
                    .TypeAdmission = m.TypeAdmission,
                    .Number = m.AdmissionNumber,
                    .InputDate = m.AdmissionDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    .EgressDate = m.EgressDate?.ToString("yyyy-MM-ddTHH:mm:ss"),
                    .AuthorizationNumber = m.AuthorizationNumber,
                    .CenterAttention = New CenterAttentionEvent With {
                        .Code = m.CenterAttentionCode,
                        .Name = m.CenterAttentionName,
                        .Address = m.CenterAttentionAddress,
                        .Phone = m.CenterAttentionPhone
                    },
                    .AdmissionFunctionalUnit = If(String.IsNullOrEmpty(m.UFUIGRMED), Nothing, New FunctionalUnitEvent With {
                        .Code = m.UFUIGRMED.Split("-")(0).Trim(),
                        .Name = m.UFUIGRMED.Split("-")(1).Trim()
                    }),
                    .EgressFunctionalUnit = If(String.IsNullOrEmpty(m.UFUEGRMED), Nothing, New FunctionalUnitEvent With {
                        .Code = m.UFUEGRMED.Split("-")(0).Trim(),
                        .Name = m.UFUEGRMED.Split("-")(1).Trim()
                    })
                },
                .ElectronicDocument = New ElectronicDocumentEvent With {
                    .PaymentMethod = m.PaymentMethod,
                    .CUFE = m.CUFE,
                    .QR = m.QR,
                    .ValidationDate = m.ValidationDate?.ToString("yyyy-MM-ddTHH:mm:ss")
                }
            }).ToList()

            Return New ActionResult(Of List(Of InvoiceEvent)) With {.StateResult = True, .ObjectEmbbeded = result, .Message = "Creado Correctamente"}

        Catch ex As Exception
            Return New ActionResult(Of List(Of InvoiceEvent)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function MapInvoiceDetailEvent(m As VReportInvoiceDetail) As InvoiceDetailEvent
        Return New InvoiceDetailEvent With {
            .BillingGroup = New BillingGroupEvent With {
                .Code = m.BillingGroup.Split("-")(0).Trim(),
                .Name = m.BillingGroup.Split("-")(1).Trim()
            },
            .CUPsEntity = New CUPsEntityEvent With {
                .Code = m.CUPSCode,
                .Name = m.CUPSName,
                .RIPSCode = m.RIPSCode,
                .RIPSDescription = m.RIPSName
            },
            .IpsService = IIf(m.RecordType = 1, New IpsServiceEvent With {
                .Code = m.Code,
                .Name = m.Name
            }, Nothing),
            .Product = IIf(m.RecordType = 2, New ProductEvent With {
                .Code = m.Code,
                .Name = m.Name,
                .CodeAlternative = m.CodeAlternative,
                .CodeAlternativeTwo = m.CodeAlternativeTwo,
                .CodeCUM = m.CodeCUM
            }, Nothing),
            .ContractDescriptionName = m.ContractDescriptionName,
            .AuthorizationNumber = m.AuthorizationNumber,
            .ServiceDate = m.ServiceDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            .RecordType = m.RecordType,
            .InvoicedQuantity = m.InvoicedQuantity,
            .TotalSalesPrice = m.TotalSalesPrice,
            .SubTotalPatientSalesPrice = m.SubTotalPatientSalesPrice,
            .ThirdPartySalesPrice = m.ThirdPartySalesPrice,
            .Surgical = New SurgicalEvent With {
                .Code = m.CodeSurgical,
                .Name = m.NameSurgical,
                .Quantity = m.QuantitySurgical,
                .TotalSalesPrice = m.TotalSalesPriceSurgical
            }
        }
    End Function

    Private Function MapIdentificationType(code As String) As Byte
        Select Case code
            Case "CC"
                Return 0
            Case "CE"
                Return 1
            Case "TI"
                Return 2
            Case "RC"
                Return 3
            Case "PA"
                Return 4
            Case "AS"
                Return 5
            Case "MS"
                Return 6
            Case "NIT"
                Return 7
            Case "NU"
                Return 8
            Case "CN"
                Return 9
            Case "CD"
                Return 10
            Case "SC"
                Return 11
            Case "PE"
                Return 12
            Case Else
                Return 0
        End Select
    End Function

    Private Function GenerateRevenueControlDetailCrossingListXml(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), session As SessionValues) As String
        Dim revenueControlDetailCrossingListXml As String = String.Empty
        If revenueControlDetailCrossingList IsNot Nothing AndAlso revenueControlDetailCrossingList.Any() Then
            revenueControlDetailCrossingListXml =
                    String.Join(vbCrLf, revenueControlDetailCrossingList _
                        .Select(Function(x)
                                    Dim rcdl = $"
                                    <RevenueControlDetailCrossingList>
                                        <FolioOrder>{x.FolioOrder}</FolioOrder>
                                        <FolioType>{x.FolioType}</FolioType>
                                        <CareGroupId>{x.CareGroupId}</CareGroupId>
                                        <RevenueControlDetailId>{x.RevenueControlDetailId}</RevenueControlDetailId>
                                        <TotalPatientDiscount>{x.TotalPatientDiscount.ToString().Replace(",", ".")}</TotalPatientDiscount>
                                        <RevenueControlId>{x.RevenueControlId}</RevenueControlId>
                                        <OutputDate>{x.OutputDate.ToString("dd/MM/yyyy HH:mm:ss")}</OutputDate>
                                        <IsCutAccount>{x.IsCutAccount}</IsCutAccount>
                                        <OutputDiagnosis>{x.OutputDiagnosis}</OutputDiagnosis>
                                        <InitialDate>{x.InitialDate.ToString("dd/MM/yyyy HH:mm:ss")}</InitialDate>
                                        <CutType>{x.CutType}</CutType>
                                        <FilePath>{System.IO.Path.Combine(Utils.GetPathElectronicDocuments(), session.TransactionalContainer)}</FilePath>
                                        {IIf(x.ListPortfolioAdvance Is Nothing, "", String.Join(vbCrLf, x.ListPortfolioAdvance.Select(Function(o)
                                                                                                                                          Return $"
                                            <ListPortfolioAdvanceCrossing>
                                                <Id>{o.Id}</Id>
                                                <Code>{o.Code}</Code>
                                                <CrossingValue>{o.CrossingValue.ToString().Replace(",", ".")}</CrossingValue>
                                                <CashReceiptDetailIdTmp>{o.CashReceiptDetailIdTmp}</CashReceiptDetailIdTmp>
                                                <PortfolioAdvanceType>{o.PortfolioAdvanceType}</PortfolioAdvanceType>
                                            </ListPortfolioAdvanceCrossing>"
                                                                                                                                      End Function)))}
                                        <CurrencyId>{x.CurrencyId}</CurrencyId>
                                        <TRMValue>{x.TRMValue.ToString().Replace(",", ".")}</TRMValue>
                                        <TaxDevolutionValue>{x.TaxDevolutionValue.ToString().Replace(",", ".")}</TaxDevolutionValue>
                                        <ConditionSalesId>{x.ConditionSalesId}</ConditionSalesId>
                                        <EconomicActivityId>{x.EconomicActivityId}</EconomicActivityId>
                                    </RevenueControlDetailCrossingList>"
                                    Return rcdl
                                End Function))
        End If
        Return revenueControlDetailCrossingListXml
    End Function

    ''' <summary>
    ''' Gets the portfolio sequence by tag form.
    ''' </summary>
    ''' <param name="tag">The tag.</param>
    ''' <param name="idOperativeUnitId">The identifier operative unit identifier.</param>
    ''' <returns></returns>
    Private Function GetPortfolioSequenceByTagForm(tag As String, Optional idOperativeUnitId As Integer = 0) As ActionResult(Of String)
        Try
            'Consultar el id de la secuencia
            Dim _idCurrentSequence As Integer = 0
            Dim _sequence As PortfolioSequence = _portFolioSequenceAdminService.GetSequenseByIdForm(tag)
            If _sequence IsNot Nothing AndAlso _sequence.Id > 0 Then
                If _sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequence = _sequence.PortfolioSequenceDetail(0).Id
                ElseIf _sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    Dim res = (From ou As PortfolioSequenceDetail In _sequence.PortfolioSequenceDetail Where ou.OperatingUnit.Id = idOperativeUnitId Select ou).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        _idCurrentSequence = res(0).Id
                    End If
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
            Else
                Throw New Exception()
            End If
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar", tag)}
        End Try
    End Function

    Private Function ValidateContractLiquidate(contract As Domain.Entities.Contract, thirdpartySalesValue As Decimal, folioOrder As Byte)
        Dim message As String = ""
        Dim messageNotification As New List(Of String)()
        Dim validationValue As Boolean = (contract.ExecuteValue + thirdpartySalesValue) > contract.ContractValue
        Dim validationDate As Boolean = Date.Now > contract.EndDate
        Dim contractCodeName As String = String.Concat(contract.Code, " - ", contract.ContractName)
        If contract.TerminationControl = 3 Then
            'Terminación del contrato por valor del contrato
            If validationValue Then
                message = String.Format("El folio {0} no se pueden liquidar debido a que se superaría el valor del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationValueType = 2 AndAlso (contract.ExecuteValue + thirdpartySalesValue) > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue + thirdpartySalesValue)).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso (contract.ExecuteValue + thirdpartySalesValue) > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue + thirdpartySalesValue)).ToString("C0"), contractCodeName))
            End If
        ElseIf contract.TerminationControl = 2 Then
            'Terminación del contrato por Fecha del contrato
            If validationDate Then
                message = String.Format("El folio {0} no se pueden liquidar debido a ya se venció la fecha del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        ElseIf contract.TerminationControl = 4 Then
            'Terminación del contrato por Fecha o Valor del contrato
            If validationValue Then
                message = String.Format("El folio {0} no se pueden liquidar debido a que se superaría el valor del contrato {1}", folioOrder, contractCodeName)
            End If
            If validationDate Then
                message = String.Format("El folio {0} no se pueden liquidar debido a ya se venció la fecha del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationValueType = 2 AndAlso (contract.ExecuteValue + thirdpartySalesValue) > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue + thirdpartySalesValue)).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso (contract.ExecuteValue + thirdpartySalesValue) > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue + thirdpartySalesValue)).ToString("C0"), contractCodeName))
            End If
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        End If
        If message.Equals(String.Empty) Then
            Return New ActionResult With {.StateResult = True, .MessageResult = messageNotification}
        Else
            Return New ActionResult With {.StateResult = False, .Message = message}
        End If
    End Function

    Private Function ValidateContractAnulate(contract As Domain.Entities.Contract, thirdpartySalesValue As Decimal, folioOrder As Byte)
        Dim message As String = ""
        Dim messageNotification As New List(Of String)()
        Dim validationValue As Boolean = (contract.ExecuteValue - thirdpartySalesValue) > contract.ContractValue
        Dim validationDate As Boolean = Date.Now > contract.EndDate
        Dim contractCodeName As String = String.Concat(contract.Code, " - ", contract.ContractName)
        If contract.TerminationControl = 3 Then
            'Terminación del contrato por valor del contrato
            If validationValue Then
                message = String.Format("El folio {0} no se pueden liquidar debido a que se superaría el valor del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationValueType = 2 AndAlso (contract.ExecuteValue - thirdpartySalesValue) > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue - thirdpartySalesValue)).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso (contract.ExecuteValue - thirdpartySalesValue) > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue - thirdpartySalesValue)).ToString("C0"), contractCodeName))
            End If
        ElseIf contract.TerminationControl = 2 Then
            'Terminación del contrato por Fecha del contrato
            If validationDate Then
                message = String.Format("El folio {0} no se pueden liquidar debido a ya se venció la fecha del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        ElseIf contract.TerminationControl = 4 Then
            'Terminación del contrato por Fecha o Valor del contrato
            If validationValue Then
                message = String.Format("El folio {0} no se pueden liquidar debido a que se superaría el valor del contrato {1}", folioOrder, contractCodeName)
            End If
            If validationDate Then
                message = String.Format("El folio {0} no se pueden liquidar debido a ya se venció la fecha del contrato {1}", folioOrder, contractCodeName)
            End If
            If contract.NotificationValueType = 2 AndAlso (contract.ExecuteValue - thirdpartySalesValue) > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue - thirdpartySalesValue)).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso (contract.ExecuteValue - thirdpartySalesValue) > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - (contract.ExecuteValue - thirdpartySalesValue)).ToString("C0"), contractCodeName))
            End If
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        End If
        If message.Equals(String.Empty) Then
            Return New ActionResult With {.StateResult = True, .MessageResult = messageNotification}
        Else
            Return New ActionResult With {.StateResult = False, .Message = message}
        End If
    End Function

    Function ConvertFolioToXml(folio As RevenueControlDetail) As String

    End Function

    Public Function ValidateHomologation(folio As RevenueControlDetail, careGroupId As Integer) As ActionResult(Of List(Of Homologation))
        Dim myResult As New ActionResult(Of List(Of Homologation))() With {.StateResult = False, .ObjectEmbbeded = New List(Of Homologation), .MessageResult = New List(Of String)()}
        'bandera que indica si existe al menos una homologación múltiple
        Dim multipleHomologations As Boolean = False
        'bandera que indica que existe al menos un Quirurgico (en este caso se devuelven todas las homologacionesm "listHomologations" para los que contengan quirurgicos escojan los datos)
        'Dim isQx As Boolean = False
        'Validamos la cantidad de homologaciones que tiene cada servicio en el nuevo grupo de atención




        'Converimos el objeto RevenueControlDetail con agregados Distribution y ServiceOrderDetail a Xml
        'Dim folioXml As String = folio.ConvertToXml({"ServiceOrderDetailDistribution", "ServiceOrderDetail"}.ToArray)

        'convertimos folio.ServiceOrderDetailDistribution.Where(Function(dis) dis.LastCaregroupId <> careGroupId AndAlso dis.DistributionType <> 2).ToList() a Xml
        'tener en cuenta que hasta el momento este filtro no se esta haciendo, se esta enviando todo



        For Each s In folio.ServiceOrderDetailDistribution.Where(Function(dis) dis.LastCaregroupId <> careGroupId AndAlso dis.DistributionType <> 2).ToList()
            'Se filtra solo los detalles que no se han retarificado para éste grupo de atención
            If s.ServiceOrderDetail.RecordType = eRecordType.Services Then
                If s.ServiceOrderDetail.CUPSAssociateService = False Then
                    'If s.ServiceOrderDetail.Presentation = 2 Then
                    '    'Presentacion Quirurgico
                    '    isQx = True
                    'End If
                    Dim result As ActionResult(Of List(Of CupsHomologation)) = _contractServices.GetHomologationCups(careGroupId, s.ServiceOrderDetail.CUPSEntityId,
                        s.ServiceOrderDetail.PerformsFunctionalUnitId, s.ServiceOrderDetail.PerformsProfessionalSpecialty, s.ServiceOrderDetail.ServiceDate, s.ServiceOrderDetail.IPSServiceId)
                    If result.StateResult Then
                        If result.ObjectEmbbeded.Count = 0 Then
                            'Error ya que debe tener al menos un homologo
                            myResult.MessageResult.Add(result.Message)
                        ElseIf result.ObjectEmbbeded.Count = 1 Then
                            'Solo contiene una homologación
                            'todas las homologaciones uno a uno se marcan con la bandera activated = true para tomarlos al retarificar
                            result.ObjectEmbbeded(0).Activated = True
                        ElseIf result.ObjectEmbbeded.Count > 1 Then
                            'Consulto la especialidad por el codigo para poderla enviar en el servicio
                            multipleHomologations = True
                        End If
                        Dim obj As Object = New Homologation()
                        If s.ServiceOrderDetail.PerformsProfessionalSpecialty IsNot Nothing Then
                            Dim spe = Me._specialityRepository.GetSpecialityByCode(s.ServiceOrderDetail.PerformsProfessionalSpecialty)
                            'If spe Is Nothing OrElse String.IsNullOrEmpty(spe.CODESPECI) Then
                            '    myResult.StateResult = False
                            '    myResult.Message = "No encontró especialidad con código " & s.ServiceOrderDetail.PerformsProfessionalSpecialty
                            '    'myResult.Message = multipleHomologations.ToString()
                            '    Return myResult
                            'End If
                            s.ServiceOrderDetail.CodeNameSpeciality = (spe.CODESPECI.Trim() & " - " & spe.DESESPECI.Trim())
                        End If
                        's.ServiceOrderDetail.CodeNameCups = s.ServiceOrderDetail.CodeNameCups '(s.ServiceOrderDetail.CUPSEntity.Code & " - " & s.ServiceOrderDetail.CUPSEntity.Description)
                        's.ServiceOrderDetail.CodeNameIpsService = s.ServiceOrderDetail.CodeNameIpsService '(s.ServiceOrderDetail.IPSService.Code & " - " & s.ServiceOrderDetail.IPSService.Name)
                        s.ServiceOrderDetail.CodeNameFunctionalUnit = s.ServiceOrderDetail.FunctionalUnit.Code & " - " & s.ServiceOrderDetail.FunctionalUnit.Name
                        s.ServiceOrderDetail.CodeNameHealthAdministrator = s.ServiceOrderDetail.ThirdParty.Nit & " - " & s.ServiceOrderDetail.ThirdParty.Name
                        obj.Service = s.ServiceOrderDetail
                        obj.Homologations = result.ObjectEmbbeded
                        myResult.ObjectEmbbeded.Add(obj)
                    Else
                        myResult.MessageResult.Add(result.Message) 'Listado de errores de homologación
                    End If
                End If
            ElseIf s.ServiceOrderDetail.RecordType = eRecordType.Medicamentos Then
                'Dim productRateDetail As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(s.ServiceOrderDetail.CareGroupId, s.ServiceOrderDetail.ProductId, s.ServiceOrderDetail.ServiceDate)
            End If
        Next
        myResult.Message = multipleHomologations.ToString()
        Return myResult
    End Function

    ''' <summary>
    ''' Realiza la retarificación de servicios en un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio a retarificar</param>
    ''' <param name="careGroupId">Id del nuevo grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente. 1-Masculino, 2-Femenino</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function ChangeRateServices(ByVal idFolio As Integer, ByVal careGroupId As Integer, ByVal patientGenus As Integer, ByVal patientBirth As Date,
                                       ByVal listHomologations As List(Of Homologation), onlyRateChange As Boolean, ThirdPartyPatientId As String,
                                       HealthAdministratorId As Integer, listServiceOrderDetailWithQx As List(Of ServiceOrderDetail), OperativeUnitId As Integer?) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) Implements ILiquidationAdminService.ChangeRateServices
        Try
            Dim ListHomologationsXml As String = Nothing
            Dim ListServiceOrderDetailWithQxXml As String = Nothing
            If listHomologations IsNot Nothing AndAlso listHomologations.Any() Then
                Dim ListHomologationsStr As List(Of String) =
                    listHomologations.Select(Function(o)
                                                 Return o.ToXml()
                                             End Function).ToList()
                ListHomologationsXml = String.Join(vbCrLf, ListHomologationsStr)
                If ListHomologationsXml IsNot Nothing Then
                    ListHomologationsXml = ListHomologationsXml.Trim()
                End If
            End If
            If listServiceOrderDetailWithQx IsNot Nothing AndAlso listServiceOrderDetailWithQx.Any() Then
                Dim counter As Integer = 1
                ListServiceOrderDetailWithQxXml = "<ListServiceOrderDetailWithQx>" &
                    String.Join("", listServiceOrderDetailWithQx.Select(Function(o)
                                                                            o.RowXml = counter
                                                                            counter += 1
                                                                            Return o.ToXml()
                                                                        End Function)).Trim() & "</ListServiceOrderDetailWithQx>"
                If ListServiceOrderDetailWithQxXml IsNot Nothing Then
                    ListServiceOrderDetailWithQxXml = ListServiceOrderDetailWithQxXml.Trim()
                End If
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                    .IsolationLevel = IsolationLevel.ReadCommitted,
                    .Timeout = TransactionManager.MaximumTimeout
                })
                Dim result As SP_ChangeRateServices_Result = _revenueControlRepository.SP_ChangeRateServices(idFolio,
                                                                             careGroupId,
                                                                             patientGenus,
                                                                             patientBirth,
                                                                             ListHomologationsXml,
                                                                             onlyRateChange,
                                                                             ThirdPartyPatientId,
                                                                             HealthAdministratorId,
                                                                             ListServiceOrderDetailWithQxXml)
                If result Is Nothing Then
                    scope.Dispose()
                    Return New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) With {
                        .StateResult = False,
                        .Message = "No se encontró un resultado"
                    }
                End If
                Dim res As New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))()
                res.StateResult = result.StatusResult.Value
                If Not String.IsNullOrEmpty(result.MessageResult) Then
                    res.MessageResult = {result.MessageResult}.ToList()
                End If
                If String.IsNullOrEmpty(result.Message) AndAlso res.MessageResult IsNot Nothing Then
                    res.Message = String.Join(vbCrLf, res.MessageResult)
                Else
                    res.Message = result.Message
                End If
                If Not String.IsNullOrEmpty(result.ObjectEmbbeded) Then
                    Dim resultHomologation As New List(Of Homologation)()
                    Dim xmlObjectEmbbeded As New Xml.XmlDocument()
                    xmlObjectEmbbeded.LoadXml($"<doc>{result.ObjectEmbbeded}</doc>")
                    For Each i As Xml.XmlNode In xmlObjectEmbbeded.SelectNodes("doc/Homologacion")
                        Dim homologacion As New Homologation()
                        With homologacion
                            .Service = New ServiceOrderDetail()
                            .Service.Id = i.SelectSingleNode("Service/ServiceOrderDetailId").AsInt()
                            .Service.CUPSAssociateService = i.SelectSingleNode("Service/CUPSAssociateService").AsInt()
                            .Service.IsDelete = i.SelectSingleNode("Service/IsDelete").AsBoolean()
                            .Service.IsFirstEvent = i.SelectSingleNode("Service/IsFirstEvent").AsBoolean()
                            .Service.ServiceOrderDetailDistribution = New Domain.Entities.TrackableCollection(Of ServiceOrderDetailDistribution)()
                            .Service.ServiceOrderDetailDistribution.Add(New ServiceOrderDetailDistribution())
                            .Service.ServiceOrderDetailDistribution(0).Id = i.SelectSingleNode("Service/Id").AsInt()
                            .Service.ServiceOrderDetailDistribution(0).DistributionType = i.SelectSingleNode("Service/DistributionType").AsByte()
                            .Service.ServiceOrderDetailDistribution(0).LastCaregroupId = i.SelectSingleNode("Service/LastCaregroupId").AsInt()
                            .Service.RecordType = i.SelectSingleNode("Service/RecordType").AsByte()
                            .Service.CUPSEntityId = i.SelectSingleNode("Service/CUPSEntityId").AsInt()
                            .Service.PerformsFunctionalUnitId = i.SelectSingleNode("Service/PerformsFunctionalUnitId").AsInt()
                            .Service.PerformsProfessionalSpecialty = i.SelectSingleNode("Service/PerformsProfessionalSpecialty").AsString()
                            .Service.ServiceDate = i.SelectSingleNode("Service/ServiceDate").AsDateTime()
                            .Service.IPSServiceId = i.SelectSingleNode("Service/IPSServiceId").AsInt()
                            .Service.CodeNameSpeciality = i.SelectSingleNode("Service/CodeNameSpeciality").AsString()
                            .Service.CodeNameFunctionalUnit = i.SelectSingleNode("Service/CodeNameFunctionalUnit").AsString()
                            .Service.CodeNameHealthAdministrator = i.SelectSingleNode("Service/CodeNameHealthAdministrator").AsString()
                            .Service.ThirdPartyId = i.SelectSingleNode("Service/ThirdPartyId").AsInt()
                            .Service.SettlementType = i.SelectSingleNode("Service/SettlementType").AsByte()
                        End With
                        homologacion.Homologations = New List(Of CupsHomologation)()
                        For Each h As Xml.XmlNode In i.SelectNodes("Homologations/CupsHomologation")
                            Dim cHomolocation As New CupsHomologation()
                            With cHomolocation
                                .Id = h.SelectSingleNode("CupsHomologationId").AsInt()
                                .IPSServiceId = h.SelectSingleNode("IPSServiceId").AsInt()
                                .CupsEntityId = h.SelectSingleNode("CupsEntityId").AsInt()
                                .CodeNameCupsEntity = h.SelectSingleNode("CodeNameCupsEntity").AsString()
                                .CodeNameIpsService = h.SelectSingleNode("CodeNameIpsService").AsString()
                                .Activated = h.SelectSingleNode("Activated").AsBoolean()
                            End With
                            homologacion.Homologations.Add(cHomolocation)
                        Next
                        resultHomologation.Add(homologacion)
                    Next
                    res.ObjectEmbbeded = resultHomologation
                End If
                If Not String.IsNullOrEmpty(result.ObjectEmbbededAux) Then
                    Dim listNewServiceOrderD As New List(Of ServiceOrderDetail)()
                    Dim xmlObjectEmbbeded As New Xml.XmlDocument()
                    xmlObjectEmbbeded.LoadXml($"<doc>{result.ObjectEmbbededAux}</doc>")
                    For Each i As Xml.XmlNode In xmlObjectEmbbeded.SelectNodes("doc/ListNewServiceOrderDetail")
                        listNewServiceOrderD.Add(New ServiceOrderDetail(i))
                    Next
                    res.ObjectEmbbededAux = listNewServiceOrderD
                End If
                If String.IsNullOrEmpty(res.Message) AndAlso res.ObjectEmbbeded Is Nothing AndAlso res.ObjectEmbbededAux Is Nothing Then
                    If String.IsNullOrEmpty(res.Message) AndAlso res.MessageResult IsNot Nothing AndAlso res.MessageResult.Any() Then
                        res.Message = String.Join(vbCrLf, res.MessageResult)
                    End If

                    Dim execResult = ChangedCareGroupMasterAccount(idFolio, OperativeUnitId)
                    If execResult Is Nothing OrElse Not execResult?.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = execResult?.Message}
                    ElseIf Not String.IsNullOrEmpty(execResult?.Message) Then
                        res.Message = $"{res.Message} - {execResult.Message}"
                    End If

                    scope.Complete()
                    Return res
                End If
                scope.Dispose()
                Return res
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion el cambio de grupo de atencion para los folios de cuenta madre, si no esta marcado con alguna distribucion de la cuenta madre No se ejecuta nada y sigue el flujo antiguo
    ''' si no se manda a recalcular el folio segun parametros de liquidación.
    ''' </summary>
    ''' <param name="revenueControlDetailId"> id del folio</param>
    ''' <param name="operativeUnitId">id de la unidad operativa para verificar los parametros de facturación</param>
    ''' <returns></returns>
    Private Function ChangedCareGroupMasterAccount(revenueControlDetailId As Integer?, operativeUnitId As Integer?) As ActionResult
        If revenueControlDetailId Is Nothing OrElse operativeUnitId Is Nothing Then
            Return New ActionResult With {.Message = "", .StateResult = False}
        End If

        Dim settingsBilling As SettingsBilling = _settingBillingRepository.GetSettingsBillingByIdUnitOperative(operativeUnitId, False)

        'se valida que existan parametros de facturación, y si existen, se valida que liquida cuenta madre este activa de lo contrario No se ejecuta nada y sigue el flujo antiguo
        If settingsBilling Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = "No existen párametros para la unidad operativa"}
        End If
        If Not settingsBilling.LiquidateMasterAccount Then
            Return New ActionResult With {.StateResult = True}
        End If

        Dim RevenueControlDetail = _revenueControlDetailRepository.Query(Function(x) x.Id = revenueControlDetailId, True, {"RevenueControl.RevenueControlDetail"}).FirstOrDefault

        'se valida que exista el folio, y se valida si tiene alguna definicion de cuenta madre, si es ismasterAccount = 0 es porque No va por flujo nuevo.
        If RevenueControlDetail Is Nothing Then
            Return New ActionResult With {.StateResult = False, .Message = "No se encuentra informacion del folio"}
        End If

        'si el folio es estandart y existe mas de un folio en el ingreso, se va por el flujo antiguo
        If RevenueControlDetail.IsMasterAccount = 0 AndAlso RevenueControlDetail?.RevenueControl?.RevenueControlDetail?.Count > 1 Then
            Return New ActionResult With {.StateResult = True}
        End If

        'se valida si existe algun folio madre ya distribuido/ solo se toman en cuenta los folio No marcados con la numeracion de cuenta madre
        If RevenueControlDetail?.RevenueControl?.RevenueControlDetail.Any(Function(x) x.Id <> revenueControlDetailId AndAlso {2, 4}.Contains(x.IsMasterAccount)) OrElse RevenueControlDetail.IsMasterAccount = 2 Then
            Return New ActionResult With {.StateResult = False, .Message = "Este folio se originó a partir de una cuenta madre. Para continuar, debe unificar las cuentas."}
        End If

        'se actualiza la cuenta madre dependiendo del tipo de folio, si es particular(3) asigna un folio paciente(4) sino cuenta madre(1)
        RevenueControlDetail.IsMasterAccount = If(RevenueControlDetail.FolioType = 3, 4, 1)

        Dim admissionNumber As String = RevenueControlDetail?.RevenueControl?.AdmissionNumber
        Dim liquidationData = _LiquidationDataRepository.FirstOrDefault(Function(x) x.AdmissionNumber = admissionNumber, True)

        If liquidationData Is Nothing Then
            _revenueControlDetailRepository.SaveEntity(RevenueControlDetail)
            _revenueControlDetailRepository.UnitWork.Commit()
            Return New ActionResult With {.StateResult = True, .Message = "No hay datos de liquidacion, por ende debe recalcular manualmente"}
        End If

        If liquidationData?.ApplyDiscount = 3 Then
            Dim discountCareGroupNew = _caregroupRepository.GetCareGroupById(RevenueControlDetail.CareGroupId)
            If discountCareGroupNew IsNot Nothing AndAlso discountCareGroupNew?.DiscountContractedCustomer <> liquidationData?.DiscountValueorPercentage Then
                liquidationData.DiscountValueorPercentage = discountCareGroupNew.DiscountContractedCustomer
                _revenueControlDetailRepository.SaveEntity(RevenueControlDetail)
            End If
        End If

        _revenueControlDetailRepository.SaveEntity(RevenueControlDetail)
        'se manda a recalcular el folio segun datos de liquidacion.
        Dim result = _folioAdminService.RecalculateFolio(RevenueControlDetail?.RevenueControl?.AdmissionNumber, revenueControlDetailId, SessionValues.Instance.AuditMessageWcf)

        If result Is Nothing OrElse Not result?.StateResult Then
            _revenueControlDetailRepository.UnitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .Message = If(String.IsNullOrEmpty(result?.Message), "Error en el servicio de recalcular Nuevo", result?.Message)}
        End If

        _revenueControlDetailRepository.UnitWork.Commit()
        Return New ActionResult With {.StateResult = result.StateResult, .Message = result?.Message}

    End Function

    ''' <summary>
    ''' funcion para guardar el reporte Sin recaudo de cuota
    ''' </summary>
    ''' <param name="feeNotCollected"></param>
    ''' <returns></returns>
    Private Function CreateFeeNotCollected(feeNotCollected As FeeNotCollected) As ActionResult
        Dim unitWork As IUnitWork = _feeNotCollectedRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                If feeNotCollected Is Nothing Then
                    Throw New ArgumentNullException(NameOf(feeNotCollected))
                End If

                If feeNotCollected.Id > 0 Then
                    Throw New Exception("Ya existe un recaudo sin cuota")
                End If

                feeNotCollected.CreationDate = DateTime.Now()

                _feeNotCollectedRepository.SaveEntity(feeNotCollected)
                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "Guardado con éxito"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' funcion encargada de eliminar el reporte de Sin recaudo de cuota
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="revenueControlDetailId"></param>
    ''' <returns></returns>
    Private Function DeleteFeeNotCollected(Id As Integer?, revenueControlDetailId As Integer?) As ActionResult
        Dim unitWork As IUnitWork = _feeNotCollectedRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                If Id Is Nothing OrElse Id = 0 Then
                    Throw New ArgumentNullException(NameOf(Id))
                End If

                If revenueControlDetailId Is Nothing OrElse revenueControlDetailId = 0 Then
                    Throw New ArgumentNullException(NameOf(revenueControlDetailId))
                End If

                Dim query = _feeNotCollectedRepository.FirstOrDefault(Function(x) x.Id = Id AndAlso x.RevenueControlDetailId = revenueControlDetailId, True)
                query.MarkAsDeleted()

                _feeNotCollectedRepository.DeleteEntity(query)
                unitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "Eliminado el reporte (Sin recaudo de cuota)"}
            Catch ex As Exception
                unitWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function
#End Region

#Region "Action Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function SP_ReportBillingStatistics(InitialDate As Date, EndDate As Date, ReportType As Integer, AgrupedBy As Integer, DocumentType As String, StatusInvoice As Integer, CareCenterCodes As String, ThirdPartyIds As String, HealthAdministratorIds As String, CareGroupIds As String, UserCodes As String, session As SessionValues) As DataSet Implements ILiquidationAdminService.SP_ReportBillingStatistics
        Try
            Dim ds As New DataSet
            Dim query1 As String = "EXEC [Billing].[SP_ReportBillingStatistics] '" & InitialDate.ToString("yyyy-MM-dd") & "','" & EndDate.ToString("yyyy-MM-dd") & "','" & ReportType & "','" & AgrupedBy & "','" & DocumentType & "','" & StatusInvoice & "','" & CareCenterCodes & "','" & ThirdPartyIds & "','" & HealthAdministratorIds & "','" & CareGroupIds & "','" & UserCodes & "'"
            Dim dt1 = Me.GetDatatable(query1, session, "ReportBillingStatistics")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que retorna el conteo de registros para validar antes de ejecutar el reporte estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials">Criterios de busqueda</param>
    ''' <param name="XmlFilters">Filtros de busqueda</param>
    ''' <returns>Resultado con Success, TotalRegistros y Message</returns>
    Public Function ReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String, session As SessionValues) As SP_ReportBillingStadistics_Count_Result Implements ILiquidationAdminService.ReportBillingStadisticsCount
        Try
            Return _settingBillingRepository.ReportBillingStadisticsCount(XmlCriterials, XmlFilters)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que retorna los datos para el reporte de estadistico de facturacion
    ''' </summary>
    ''' <returns></returns>
    Public Function ReportBillingStadistics(XmlCriterials As String, XmlFilters As String, session As SessionValues) As List(Of SP_ReportBillingStadistics_Result) Implements ILiquidationAdminService.ReportBillingStadistics
        Try
            ReportBillingStadisticsData = _settingBillingRepository.GetReportBillingStadistics(XmlCriterials, XmlFilters)
            Return ReportBillingStadisticsData
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Método que obtiene la información del sp de estadísitco de ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Function SPCH_ReportAdmissionStatistics(ParametrosString As String(), ParametrosDate As DateTime(), session As SessionValues) As DataTable Implements ILiquidationAdminService.SPCH_ReportAdmissionStatistics
        Try
            Dim ParametrosSQL As String

            'Parametros fijos
            ParametrosSQL = "'" & ParametrosString(0).Trim() & "','" & Format(ParametrosDate(0), "dd/MM/yyyy HH:mm") & "','" & Format(ParametrosDate(1), "dd/MM/yyyy HH:mm") & "','" & ParametrosString(1) & "','" & ParametrosString(2) & "'"

            'Parametros variables.
            ParametrosSQL += ",'" & ParametrosString(3) & "'"
            ParametrosSQL += ",'" & ParametrosString(4) & "'"
            ParametrosSQL += ",'" & ParametrosString(5) & "'"
            ParametrosSQL += ",'" & ParametrosString(6) & "'"
            ParametrosSQL += ",'" & ParametrosString(7) & "'"
            ParametrosSQL += ",'" & ParametrosString(8) & "'"

            Dim SQL As String = "EXEC [Billing].[SPCH_ReportAdmissionStatistics] " & ParametrosSQL


            Dim INDdtAdmissionReport = Me.GetDatatable(SQL, session, "SPCH_ReportAdmissionStatistics")
            Return INDdtAdmissionReport
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

    Private Function UnPackageItems(objArgs As Object) As ActionResult(Of String)
        Dim errorList As New StringBuilder()
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Dim unitWorkSodd As IUnitWork = _serviceOrderDetailDistributionRepository.UnitWork

        Dim objAudit As Object = DirectCast(objArgs.audit, Object)
        Dim audit As New AuditMessage()
        With audit
            .CodeUser = objAudit.CodeUser
            .Company = objAudit.Company
            .ComputerName = objAudit.ComputerName
            .ContainerSecurity = objAudit.ContainerSecurity
            .Functional = objAudit.Functional
            .IdUser = objAudit.IdUser
            .NameUser = objAudit.NameUser
            .WindowsUser = objAudit.WindowsUser
        End With

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim folio As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailById(CType(objArgs.FolioId, Integer))
                Dim lstSOD As New List(Of ServiceOrderDetail)()
                Dim lstSODtoUnPackage As List(Of Object) = DirectCast(objArgs.lstServiceOrderDetailId, List(Of Object))
                Dim lstSOrderId As List(Of Object) = DirectCast(objArgs.lstServiceOrderId, List(Of Object))
                Dim servOrder As ServiceOrder
                Dim serviceOrderDetail As ServiceOrderDetail

                For Each sodId In lstSODtoUnPackage
                    serviceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(CType(sodId, Integer))
                    If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.ServiceOrderDetail1 IsNot Nothing AndAlso serviceOrderDetail.ServiceOrderDetail1.Count > 0 Then
                        For Each sod As ServiceOrderDetail In serviceOrderDetail.ServiceOrderDetail1
                            lstSOD.Add(sod)
                        Next
                    End If
                Next

                For Each sod As ServiceOrderDetail In lstSOD
                    sod.StartTracking()
                    sod.Packaging = False
                    sod.PackageServiceOrderDetailId = Nothing
                Next

                For Each sorderId In lstSOrderId.Distinct().ToList()
                    servOrder = _serviceOrderRepository.GetServiceOrderByIdWithDetails(CType(sorderId, Integer))
                    servOrder.StartTracking()
                    servOrder.Status = 3 'Anulado
                    Dim res = _serviceOrderAdminService.SaveServiceOrder(servOrder, audit, 0, 0, EActionServiceOrder.Unpack)
                    If Not res.StateResult Then
                        errorList.AppendLine(res.Message)
                    End If
                Next

                _billingServ.UpdateRevenueControlDetailValues(CInt(objArgs.FolioId), Nothing)

                If errorList.Length > 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = errorList.ToString()}
                End If

                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Generates the service order detail distribution.
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <returns></returns>
    Private Function GenerateServiceOrderDetailDistribution(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetailDistribution
        Dim serviceOrderDetailDistribution As New ServiceOrderDetailDistribution
        serviceOrderDetailDistribution.ApplyRecoveryFee = 1
        serviceOrderDetailDistribution.RecoveryFeeType = 1
        serviceOrderDetailDistribution.ServiceOrderDetailId = serviceOrderDetail.Id
        serviceOrderDetailDistribution.GrandTotalSalesPrice = serviceOrderDetail.GrandTotalSalesPrice
        serviceOrderDetailDistribution.DistributionType = 1
        serviceOrderDetailDistribution.ThirdPartySalesPrice = serviceOrderDetail.GrandTotalSalesPrice
        serviceOrderDetailDistribution.ThirdPartyPercentage = 100
        serviceOrderDetailDistribution.GrandTotalDiscount = serviceOrderDetail.ThirdPartyDiscount * serviceOrderDetail.InvoicedQuantity
        serviceOrderDetailDistribution.Quantity = serviceOrderDetail.InvoicedQuantity
        serviceOrderDetailDistribution.LastCaregroupId = serviceOrderDetail.CareGroupId
        'RevenueControlDetail.ServiceOrderDetailDistribution.Add(serviceOrderDetailDistribution)
        Return serviceOrderDetailDistribution
    End Function

    ''' <summary>
    ''' Packages the items.
    ''' </summary>
    ''' <returns></returns>
    Private Function PackageItems(serviceOrderDetail As ServiceOrderDetail, arguments As String, audit As AuditMessage) As ActionResult(Of String) Implements ILiquidationAdminService.PackageItems
        Dim objArgs As Object = Utils.DeserializeJsonToObject(arguments)
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Dim unitWorkSodd As IUnitWork = _serviceOrderDetailDistributionRepository.UnitWork
        Try
            Dim operativeUnitId As Integer = CType(objArgs.OperativeUnitId, Integer)
            If operativeUnitId = 0 Then
                Throw New ArgumentException("Seleccione una Unidad Operativa")
            End If

            Dim errorList As New StringBuilder()
            Dim lstSODtoPackage As List(Of Object) = DirectCast(objArgs.lstServiceOrderDetailId, List(Of Object))

            Dim serviceOrder As New ServiceOrder()
            With serviceOrder
                .Code = ""
                .AdmissionNumber = objArgs.AdmissionNumber
                .PatientCode = objArgs.PatientCode
                .OrderDate = Date.Now
                .OperatingUnitId = operativeUnitId
                .Status = 1
                .CreationUser = audit.CodeUser
                .CreationDate = Date.Now
            End With

            If CType(objArgs, IDictionary(Of String, Object)).ContainsKey("ContractPackageId") AndAlso objArgs?.ContractPackageId IsNot Nothing Then
                serviceOrderDetail.ContractPackageId = CInt(objArgs?.ContractPackageId)
            End If

            serviceOrder.ServiceOrderDetail.Add(serviceOrderDetail)

            ' Estar pendiente porque se está iniciando el tracking, verificar con un error
            If lstSODtoPackage IsNot Nothing Then
                For Each sodId As Object In lstSODtoPackage
                    Dim serviceOrderDetailToEmpackage As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(CType(sodId, Integer))
                    If serviceOrderDetailToEmpackage IsNot Nothing AndAlso serviceOrderDetailToEmpackage.Id > 0 Then
                        serviceOrderDetailToEmpackage.StartTracking()
                        serviceOrderDetailToEmpackage.Packaging = True
                        serviceOrder.ServiceOrderDetail(0).ServiceOrderDetail1.Add(serviceOrderDetailToEmpackage)
                    End If
                Next
            End If

            ValidatePackageItems(
                objArgs:=objArgs,
                packageQuantity:=serviceOrder.ServiceOrderDetail(0).InvoicedQuantity,
                operativeUnitId:=operativeUnitId,
                serviceOrderDetailToPackage:=serviceOrder.ServiceOrderDetail(0).ServiceOrderDetail1.ToList()
            )

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim res = _serviceOrderAdminService.SaveServiceOrder(serviceOrder, audit, 0, CType(objArgs.FolioId, Integer), EActionServiceOrder.Package)
                If Not res.StateResult Then
                    scope.Dispose()
                    Throw New ArgumentException(res.Message)
                End If

                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True}
            End Using

        Catch ex As ArgumentException
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Valida la configuración de los paquetes si el parámetro de facturación se encuentra habilitado
    ''' </summary>
    Private Sub ValidatePackageItems(objArgs As Object, packageQuantity As Integer, operativeUnitId As Integer, serviceOrderDetailToPackage As List(Of ServiceOrderDetail))
        Dim billingParam = _settingBillingRepository.GetSettingsBillingByIdUnitOperative(operativeUnitId, False)
        If Not billingParam.ValidatePackaging Then Exit Sub

        If Not CType(objArgs, IDictionary(Of String, Object)).ContainsKey("ContractPackageId") Then
            Throw New ArgumentException("No se ha enviado el paquete a generar")
        End If

        If serviceOrderDetailToPackage Is Nothing OrElse Not serviceOrderDetailToPackage.Any() Then
            Throw New ArgumentException("No se enviaron los items a empaquetar")
        End If

        If packageQuantity <= 0 Then
            Throw New ArgumentException("La cantidad de paquetes debe ser mayor a cero")
        End If

        Dim contractPackageId As Integer = objArgs.ContractPackageId

        ' Se necesita que se envíe el contract package seleccionado o mirar como se puede hallar sin enviarlo
        Dim services = _contractPackageServiceRepository.GetAllContractPackageServiceByContractPackageId(contractPackageId, False)
        Dim products = _contractPackageProductRepository.GetAllContractPackageProductByContractPackageId(contractPackageId, False)

        If Not services.Any() AndAlso Not products.Any() Then
            Throw New ArgumentException("No se encontró configuración de servicios y/o productos para el paquete seleccionado")
        End If

        'Se crea diccionario para los servicios configurados dentro del paquete
        Dim svcConfigByCupsId As New Dictionary(Of Integer, ContractPackageService)()
        For Each s In services
            If s.CUPSEntityId > 0 AndAlso Not svcConfigByCupsId.ContainsKey(s.CUPSEntityId) Then
                svcConfigByCupsId(s.CUPSEntityId) = s
            End If
        Next

        'Se crea diccionario para los productos configurados dentro del paquete
        Dim productsInPackage As New Dictionary(Of Integer, ContractPackageProduct)()
        For Each p In products
            If p.ProductId > 0 AndAlso Not productsInPackage.ContainsKey(p.ProductId) Then
                productsInPackage(p.ProductId) = p
            End If
        Next

        Dim errors As New StringBuilder()

        Dim productGroups = serviceOrderDetailToPackage.Where(Function(d) d.ProductId.HasValue).GroupBy(Function(d) d.ProductId.Value).ToList()

        'Se realizan las validaciones para las cantidades de los productos
        For Each pg In productGroups
            Dim productId = pg.Key
            Dim totalQty = pg.Sum(Function(x) x.InvoicedQuantity)

            Dim cpp As ContractPackageProduct = Nothing
            Dim productExists = productsInPackage.TryGetValue(productId, cpp)

            If Not productExists OrElse cpp Is Nothing Then
                ' No está configurado en el paquete
                Dim product = _inventoryProductRepository.GetCleanInventoryProductById(productId, False)
                errors.AppendLine($"El ítem ({product.Code} - {product.Name}) no se puede empaquetar")
            Else
                Dim allowed = cpp.Quantity * packageQuantity
                If totalQty > allowed Then
                    Dim product = _inventoryProductRepository.GetCleanInventoryProductById(productId, False)
                    errors.AppendLine($"El ítem ({product.Code} - {product.Name}) excede la cantidad permitida. Permitido: {allowed}, Enviado: {totalQty}")
                End If
            End If
        Next

        Dim cupsGroups = serviceOrderDetailToPackage.Where(Function(d) d.CUPSEntityId.HasValue).GroupBy(Function(d) d.CUPSEntityId.Value).ToList()

        'Se realizan las validaciones para las cantidades de los CUPS
        For Each g In cupsGroups
            Dim cupsId = g.Key
            Dim totalQty = g.Sum(Function(x) x.InvoicedQuantity)

            Dim cps As ContractPackageService = Nothing
            Dim cupsExists = svcConfigByCupsId.TryGetValue(cupsId, cps)

            If Not cupsExists OrElse cps Is Nothing Then
                Dim cups = _cupsRepository.GetCupsEntityById(cupsId, False)
                errors.AppendLine($"El ítem ({cups.Code} - {cups.Description}) no se puede empaquetar")
            Else
                Dim allowed = cps.Quantity * packageQuantity
                If totalQty > allowed Then
                    Dim cups = _cupsRepository.GetCupsEntityById(cupsId, False)
                    errors.AppendLine($"El ítem ({cups.Code} - {cups.Description}) excede la cantidad permitida. Permitido: {allowed}, Enviado: {totalQty}")
                End If
            End If
        Next

        'Se realizan las validaciones para las cantidades de los productos y CUPS con restricción de tecnología
        Dim productApplyIds = products.Where(Function(p) p.ApplyCondition AndAlso p.ProductId).Select(Function(p) p.ProductId).ToHashSet()

        Dim productIdsInPackage = productGroups.Select(Function(g) g.Key).ToHashSet()
        Dim productApplyPresentCount = productApplyIds.Intersect(productIdsInPackage).Count()

        If productApplyPresentCount > 1 Then
            Dim firstId = productApplyIds.Intersect(productIdsInPackage).First()
            Dim product = _inventoryProductRepository.GetCleanInventoryProductById(firstId, False)
            errors.AppendLine($"Los productos con restricción de tecnología no pueden combinarse. Solo puede incluir una tecnología.")
        End If

        Dim cupsApplyIds = services.Where(Function(s) s.ApplyCondition AndAlso s.CUPSEntityId).Select(Function(s) s.CUPSEntityId).ToHashSet()

        Dim cupsIdsInPackage = cupsGroups.Select(Function(g) g.Key).ToHashSet()
        Dim cupsApplyPresentCount = cupsApplyIds.Intersect(cupsIdsInPackage).Count()

        If cupsApplyPresentCount > 1 Then
            Dim firstId = cupsApplyIds.Intersect(cupsIdsInPackage).First()
            Dim cups = _cupsRepository.GetCupsEntityById(firstId, False)
            errors.AppendLine($"Los servicios con restricción de tecnología no pueden combinarse. Solo puede incluir una tecnología.")
        End If

        If errors.Length > 0 Then
            Throw New ArgumentException(errors.ToString())
        End If

    End Sub

    ''' <summary>
    ''' Obtiene la lista de Ids de folios de un ingreso
    ''' </summary>
    ''' <param name="IdAdmission">Id del ingreso</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function GetIdsFolios(ByVal IdAdmission As Integer) As ActionResult(Of List(Of Object))
        Try
            Dim res = Me._revenueControlRepository.GetRevenueControlWithAggregatesById(IdAdmission)
            If res.Id > 0 Then
                Dim list As New List(Of Object)()
                For Each d In res.RevenueControlDetail
                    Dim obj As Object = New ExpandoObject()
                    obj.Id = d.Id
                    obj.Status = d.Status
                    list.Add(obj)
                Next
                Return New ActionResult(Of List(Of Object)) With {.StateResult = True, .ObjectEmbbeded = list}
            Else
                Return New ActionResult(Of List(Of Object)) With {.StateResult = False}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Object)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un folio cuando se encuentra sin detalles
    ''' </summary>
    ''' <param name="idFolio">Id del folio</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteEmptyFolio(ByVal idFolio As Integer) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim res = Me._revenueControlDetailRepository.GetRevenueControlDetailWithAggregatesById(idFolio)
                If res IsNot Nothing AndAlso res.Id > 0 AndAlso (res.ServiceOrderDetailDistribution Is Nothing OrElse res.ServiceOrderDetailDistribution.Count = 0) Then
                    Dim revenueRecognitionDetails = Me._recognitionDetailRepository.GetRevenueRecognitionDetailByRevenueControlDetailId(res.Id)
                    If revenueRecognitionDetails IsNot Nothing AndAlso revenueRecognitionDetails.Count > 0 _
                        AndAlso revenueRecognitionDetails.Where(Function(rrd) rrd.RevenueRecognition.State <> 2).Count > 0 Then
                        unitWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult() With {.StateResult = False, .Message = "El folio se encuentra en un reconocimiento de ingreso no reversado"}
                    End If

                    'If revenueRecognitionDetails IsNot Nothing AndAlso revenueRecognitionDetails.Count > 0 Then
                    '    For Each rrd In revenueRecognitionDetails
                    '        Me._recognitionDetailRepository.ExecuteNonQuery(String.Format("DELETE FROM Billing.RevenueRecognitionDetail WHERE Id = {0}", rrd.Id))
                    '    Next

                    '    _recognitionDetailRepository.UnitWork.Commit()
                    'End If

                    _revenueControlDetailRepository.DeleteEntity(res)
                    unitWork.Commit()

                    Dim revenueControlDetailsByFolio = _revenueControlDetailRepository.GetByFilter(Function(m) m.RevenueControlId = res.RevenueControlId)

                    For i As Integer = 1 To revenueControlDetailsByFolio.Count()
                        revenueControlDetailsByFolio(i - 1).FolioOrder = i
                    Next

                    res.RevenueControl.FolioQuantity = revenueControlDetailsByFolio.Count
                    _revenueControlRepository.SaveEntity(res.RevenueControl)

                    unitWork.Commit()
                    scope.Complete()
                    Return New ActionResult() With {.StateResult = True}
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Return New ActionResult() With {.StateResult = False}
                End If
            End Using
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Agrega un nuevo folio al ingreso
    ''' </summary>
    ''' <param name="revenueControlId">Id del ingreso</param>
    ''' <param name="creationUser">Usuario quien crea el folio</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function AddNewFolio(ByVal revenueControlId As Integer, ByVal creationUser As String) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlRepository.UnitWork
        Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim admission = Me._revenueControlRepository.GetRevenueControlWithAggregatesById(revenueControlId)
                Dim newFolio As New RevenueControlDetail()
                Dim lastFolio As RevenueControlDetail = admission.RevenueControlDetail.Last()
                Dim folioOrder As Integer = 0
                For Each f In admission.RevenueControlDetail
                    If folioOrder < f.FolioOrder Then
                        folioOrder = f.FolioOrder
                    End If
                Next
                newFolio.FolioOrder = (folioOrder + 1)
                newFolio.FolioType = lastFolio.FolioType
                newFolio.ContractEntityId = lastFolio.ContractEntityId
                newFolio.HealthAdministratorId = lastFolio.HealthAdministratorId
                newFolio.ThirdPartyId = lastFolio.ThirdPartyId
                newFolio.CareGroupId = lastFolio.CareGroupId
                newFolio.LiquidationType = lastFolio.LiquidationType
                newFolio.ResponsibleRecoveryFee = 1
                newFolio.Status = 1
                newFolio.CreationUser = creationUser
                newFolio.CreationDate = DateTime.Now
                'newFolio.TotalFolio = 1

                admission.FolioQuantity += 1
                admission.RevenueControlDetail.Add(newFolio)

                Me._revenueControlRepository.SaveEntity(admission)

                unitWork.Commit()
                transaction.Complete()
                Return New ActionResult() With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                Return New ActionResult() With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult() With {.StateResult = False}
            End Try
        End Using
    End Function

    Function CreateNewTargetFolio(objParams As Object, folioOrder As Integer) As RevenueControlDetail
        Dim targetFolio = New RevenueControlDetail()
        targetFolio.RevenueControlId = objParams.RevenueControlId
        targetFolio.FolioOrder = folioOrder 'objParams.FolioOrder
        targetFolio.FolioType = objParams.FolioType
        targetFolio.ContractEntityId = IIf(objParams.ContractEntityId IsNot Nothing, CInt(objParams.ContractEntityId), Nothing)
        targetFolio.HealthAdministratorId = If(objParams.HealthAdministratorId IsNot Nothing, IIf(objParams.HealthAdministratorId Is Nothing, Nothing, CInt(objParams.HealthAdministratorId)), Nothing)
        targetFolio.ThirdPartyId = objParams.ThirdPartyId
        targetFolio.CareGroupId = objParams.CareGroupId
        targetFolio.LiquidationType = 1
        targetFolio.TotalFolio = 0
        targetFolio.ResponsibleRecoveryFee = 1
        targetFolio.TotalPatientSalesPrice = 0
        targetFolio.PatientDiscount = 0
        targetFolio.PatientDiscountPercentage = 0
        targetFolio.TotalPatientWithDiscount = 0
        targetFolio.ValueCopay = 0
        targetFolio.ValueFeeModerator = 0
        targetFolio.Status = 1
        targetFolio.TotalFolio = CType(objParams.ProductsAndServices, List(Of Object)).Sum(Function(x) x.TargetFolioValue)
        targetFolio.CreationDate = DateTime.Now
        targetFolio.CreationUser = objParams.User
        targetFolio.ServiceOrderDetailDistribution = New Domain.Entities.TrackableCollection(Of ServiceOrderDetailDistribution)()
        Return targetFolio
    End Function

    Private Function RemoveNoPOSLiquidation(args As Object) As ActionResult
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim listSourceDeleteDistribution As New List(Of ServiceOrderDetailDistribution)()
                Dim listSourceDelete As New List(Of ServiceOrderDetail)()
                Dim SourceFolioId, TargetFolioId As Integer

                For Each detail In CType(args.Details, List(Of Object))
                    Dim sourceDist As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionById(CInt(detail.ServiceOrderDetailDistributionId))
                    Dim targetDist As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionById(CInt(detail.ServiceOrderDetailDistributionIdFromDelete))
                    SourceFolioId = sourceDist.RevenueControlDetailId
                    TargetFolioId = targetDist.RevenueControlDetailId


                    'OJO CUIDADO **************---------- No se esta contemplando la opcion de descuento ya que en la distribucion siempre se deja en cero la columna de descuento, por lo tanto si en algun
                    'momento se reporta error por temas de descuento, verificar todo el procedimiento de liquidacion Res 1479 para implementarlo en la logica y de igual forma en las 
                    ' otras formas de distribucion

                    If targetDist.ServiceOrderDetail.CUPSAssociateService Then
                        sourceDist.DistributionType = 1
                        listSourceDeleteDistribution.Add(targetDist)
                        Dim invoiceDetail As InvoiceDetail = _invoiceRepository.GetInvoiceDetailByServiceOrderDetailId(targetDist.ServiceOrderDetailId)
                        If invoiceDetail IsNot Nothing AndAlso invoiceDetail.Id > 0 Then
                            targetDist.ServiceOrderDetail.IsDelete = True
                            _serviceOrderDetailRepository.SaveEntity(targetDist.ServiceOrderDetail)
                        Else
                            listSourceDelete.Add(targetDist.ServiceOrderDetail)
                        End If
                        sourceDist.ServiceOrderDetail.SettlementType = 1
                        sourceDist.ServiceOrderDetail.SubTotalSalesPrice = (targetDist.GrandTotalSalesPrice + sourceDist.GrandTotalSalesPrice) / sourceDist.Quantity
                        sourceDist.ServiceOrderDetail.TotalSalesPrice = sourceDist.ServiceOrderDetail.SubTotalSalesPrice
                        sourceDist.ServiceOrderDetail.GrandTotalSalesPrice = targetDist.GrandTotalSalesPrice + sourceDist.GrandTotalSalesPrice
                        sourceDist.ServiceOrderDetail.CodeAssociateService = Nothing
                        sourceDist.RevenueControlDetailId = targetDist.RevenueControlDetailId
                        sourceDist.GrandTotalSalesPrice = sourceDist.GrandTotalSalesPrice + targetDist.GrandTotalSalesPrice
                        sourceDist.ThirdPartySalesPrice = sourceDist.GrandTotalSalesPrice
                        _serviceOrderDetailRepository.SaveEntity(sourceDist.ServiceOrderDetail)
                    Else
                        targetDist.DistributionType = 1
                        listSourceDeleteDistribution.Add(sourceDist)
                        Dim invoiceDetail As InvoiceDetail = _invoiceRepository.GetInvoiceDetailByServiceOrderDetailId(sourceDist.ServiceOrderDetailId)
                        If invoiceDetail IsNot Nothing AndAlso invoiceDetail.Id > 0 Then
                            sourceDist.ServiceOrderDetail.IsDelete = True
                            _serviceOrderDetailRepository.SaveEntity(targetDist.ServiceOrderDetail)
                        Else
                            listSourceDelete.Add(sourceDist.ServiceOrderDetail)
                        End If
                        targetDist.ServiceOrderDetail.SettlementType = 1
                        targetDist.ServiceOrderDetail.SubTotalSalesPrice = (targetDist.GrandTotalSalesPrice + sourceDist.GrandTotalSalesPrice) / sourceDist.Quantity
                        targetDist.ServiceOrderDetail.TotalSalesPrice = targetDist.ServiceOrderDetail.SubTotalSalesPrice
                        targetDist.ServiceOrderDetail.GrandTotalSalesPrice = targetDist.GrandTotalSalesPrice + sourceDist.GrandTotalSalesPrice
                        targetDist.ServiceOrderDetail.CodeAssociateService = Nothing
                        targetDist.RevenueControlDetailId = sourceDist.RevenueControlDetailId
                        targetDist.GrandTotalSalesPrice = sourceDist.GrandTotalSalesPrice + targetDist.GrandTotalSalesPrice
                        targetDist.ThirdPartySalesPrice = targetDist.GrandTotalSalesPrice
                        _serviceOrderDetailRepository.SaveEntity(targetDist.ServiceOrderDetail)
                    End If
                Next
                _serviceOrderDetailRepository.RemoveRange(listSourceDelete)
                _serviceOrderDetailDistributionRepository.RemoveRange(listSourceDeleteDistribution)
                _serviceOrderDetailDistributionRepository.UnitWork.Commit()

                _revenueControlDetailRepository.UpdateRevenueControlDetailValues(SourceFolioId)
                _revenueControlDetailRepository.UpdateRevenueControlDetailValues(TargetFolioId)
                scope.Complete()
            End Using
            Return New ActionResult() With {.StatusCode = eStatusResult.SUCCESS}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Function CreateServiceOrderDetaiPOS(serviceOrderDetail As ServiceOrderDetail, folio As RevenueControlDetail, quantity As Integer, codeassociate As String, productId As Integer) As ActionResult(Of ServiceOrderDetail)
        Dim productPOS As InventoryProduct = _inventoryProductRepository.GetInventoryProductById(productId, False)
        Dim productRateDetailPOS As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(folio.CareGroupId, productId, serviceOrderDetail.ServiceDate)
        If productPOS.ProductGroupId = 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {.Message = "El producto " + productPOS.Code + " - " + productPOS.Name + " no tiene un grupo asociado", .StateResult = False}
        End If
        Dim sod As New ServiceOrderDetail()
        With sod
            .CareGroupId = folio.CareGroupId
            .HealthAdministratorId = folio.HealthAdministratorId
            .ThirdPartyId = folio.ThirdPartyId
            .RecordType = 2
            .CodeAssociateService = codeassociate
            .LiquidationType = 5
            .ProductId = productId
            .InvoicedQuantity = quantity
            .SupplyQuantity = quantity
            .DevolutionQuantity = 0
            .RateManualSalePrice = productRateDetailPOS.SalesValue
            .CostValue = productPOS.ProductCost
            .ServiceDate = serviceOrderDetail.ServiceDate
            .AuthorizationNumber = serviceOrderDetail.AuthorizationNumber
            .PerformsFunctionalUnitId = serviceOrderDetail.PerformsFunctionalUnitId
            .PerformsHealthProfessionalCode = serviceOrderDetail.PerformsHealthProfessionalCode
            .PerformsProfessionalSpecialty = serviceOrderDetail.PerformsProfessionalSpecialty
            .PerformsHealthProfessionalThirdPartyId = serviceOrderDetail.PerformsHealthProfessionalThirdPartyId
            .SettlementType = serviceOrderDetail.SettlementType
            .RecoveryRatio = Nothing
            .SubTotalSalesPrice = productRateDetailPOS.SalesValue
            .ThirdPartyDiscount = 0
            .IsFirstEvent = True
            .ThirdPartyDiscountPercentage = 0
            .TotalSalesPrice = productRateDetailPOS.SalesValue
            .GrandTotalSalesPrice = productRateDetailPOS.SalesValue * .InvoicedQuantity
            .SurchargeApply = False
            .IncomeMainAccountId = productPOS.ProductGroup.IncomeAccountId
            .CostCenterId = serviceOrderDetail.CostCenterId
        End With
        Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = sod}
    End Function

    ''' <summary>
    ''' Distribuye los detalles de un folio en otro
    ''' </summary>
    ''' <param name="listServiceOrderDetail">Listado de Detallles de ordenes de servicio cuando en la retarificación había algún item quirurgico</param>
    ''' <returns></returns>
    Public Function DistributeFolio(ByVal arguments As Object, homologations As List(Of Homologation), listServiceOrderDetail As List(Of ServiceOrderDetail)) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) Implements ILiquidationAdminService.DistributeFolio
        Dim objParams As Object = Utils.DeserializeJsonToObject(arguments)
        Try
            Dim ListHomologationsXml As String = Nothing
            Dim ListServiceOrderDetailWithQxXml As String = Nothing
            If homologations IsNot Nothing AndAlso homologations.Any() Then
                Dim ListHomologationsStr As List(Of String) =
                    homologations.Select(Function(o)
                                             Return o.ToXml()
                                         End Function).ToList()
                ListHomologationsXml = String.Join(vbCrLf, ListHomologationsStr)
                If ListHomologationsXml IsNot Nothing Then
                    ListHomologationsXml = ListHomologationsXml.Trim()
                End If
            End If
            If listServiceOrderDetail IsNot Nothing AndAlso listServiceOrderDetail.Any() Then
                Dim counter As Integer = 1
                ListServiceOrderDetailWithQxXml = "<ListServiceOrderDetailWithQx>" &
                    String.Join("", listServiceOrderDetail.Select(Function(o)
                                                                      o.RowXml = counter
                                                                      counter += 1
                                                                      Return o.ToXml()
                                                                  End Function)).Trim() & "</ListServiceOrderDetailWithQx>"
                If ListServiceOrderDetailWithQxXml IsNot Nothing Then
                    ListServiceOrderDetailWithQxXml = ListServiceOrderDetailWithQxXml.Trim()
                End If
            End If
            Dim ProductAndServicesXml As String = String.Empty
            CType(objParams.ProductsAndServices, List(Of Object)) _
                .ForEach(Sub(x)
                             ProductAndServicesXml &= $"<ProductsAndServicesXml>
                                <Id>{x.Id}</Id>
                                <ServiceOrderDetailId>{x.ServiceOrderDetailId}</ServiceOrderDetailId>
                                <TargetFolioValue>{x.TargetFolioValue.ToString().Replace(",", ".")}</TargetFolioValue>
                                <TargetFolioGrandTotalDiscountValue>{x.TargetFolioGrandTotalDiscountValue.ToString().Replace(",", ".")}</TargetFolioGrandTotalDiscountValue>
                                <GuidHomologation>{x.GuidHomologation}</GuidHomologation>
                                <SourceDistribType>{x.SourceDistribType}</SourceDistribType>
                                <InvoiceQuantity>{x.InvoiceQuantity}</InvoiceQuantity>
                                <TargetDistribType>{x.TargetDistribType}</TargetDistribType>                                
                                <SourceFolioValue>{x.SourceFolioValue.ToString().Replace(",", ".")}</SourceFolioValue>
                                <SourceFolioGrandTotalDiscountValue>{x.SourceFolioGrandTotalDiscountValue.ToString().Replace(",", ".")}</SourceFolioGrandTotalDiscountValue>"
                             If CType(x, IDictionary(Of String, Object)).ContainsKey("ProductPOSTotalValue") Then
                                 ProductAndServicesXml &= $"<ProductPOSTotalValue>{x.ProductPOSTotalValue.ToString().Replace(",", ".")}</ProductPOSTotalValue>"
                             End If
                             If CType(x, IDictionary(Of String, Object)).ContainsKey("ProductDefaultPOSId") Then
                                 ProductAndServicesXml &= $"<ProductDefaultPOSId>{x.ProductDefaultPOSId}</ProductDefaultPOSId>"
                             End If
                             If CType(x, IDictionary(Of String, Object)).ContainsKey("ApplyRIAS") Then
                                 ProductAndServicesXml &= $"<ApplyRIAS>{x.ApplyRIAS}</ApplyRIAS>"
                             End If
                             If CType(x, IDictionary(Of String, Object)).ContainsKey("RIASCupsId") Then
                                 ProductAndServicesXml &= $"<RIASCupsId>{x.RIASCupsId}</RIASCupsId>"
                             End If
                             ProductAndServicesXml &= "</ProductsAndServicesXml>"
                         End Sub)
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                .IsolationLevel = IsolationLevel.ReadCommitted,
                .Timeout = TransactionManager.MaximumTimeout
            })
                Dim result As SP_DistributeFolio_Result = _revenueControlRepository _
                    .SP_DistributeFolio(CInt(objParams.RevenueControlId),
                                        CInt(objParams.SourceFolioId),
                                        CInt(objParams.TargetFolioId),
                                        CByte(objParams.DistribType),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("DistributeQuantity"), CInt(objParams.DistributeQuantity), -1),
                                        CInt(objParams.CaregroupId),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("CareGroupIdTarget"), CInt(objParams.CareGroupIdTarget), -1),
                                        CBool(objParams.ChangeRateServicesNeccesary),
                                        CStr(objParams.User),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("PatientGenus"), CInt(objParams.PatientGenus), -1),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("PatientBirth"), CDate(objParams.PatientBirth), Date.Now),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("onlyRateChange"), CBool(objParams.onlyRateChange), False),
                                        CInt(objParams.ThirdPartyPatientId),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("HealthAdministratorId"), If(objParams.HealthAdministratorId Is Nothing, CType(Nothing, Integer?), CInt(objParams.HealthAdministratorId)), Nothing),
                                        CByte(objParams.FolioType),
                                        If(CType(objParams, IDictionary(Of String, Object)).ContainsKey("ContractEntityId"), If(objParams.ContractEntityId Is Nothing OrElse CInt(objParams.ContractEntityId) = 0, CType(Nothing, Integer?), CInt(objParams.ContractEntityId)), -1),
                                        CInt(objParams.ThirdPartyId),
                                        ProductAndServicesXml,
                                        ListHomologationsXml,
                                        ListServiceOrderDetailWithQxXml)
                'objParams.ContractEntityId,
                If result Is Nothing Then
                    scope.Dispose()
                    Return New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) With {
                        .StateResult = False,
                        .Message = "No se encontró un resultado"
                    }
                End If
                Dim res As New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))()
                res.StateResult = result.StatusResult
                If Not String.IsNullOrEmpty(result.MessageResult) Then
                    res.MessageResult = {result.MessageResult}.ToList()
                End If
                res.Message = result.Message
                If Not String.IsNullOrEmpty(result.ObjectEmbbeded) Then
                    Dim resultHomologation As New List(Of Homologation)()
                    Dim xmlObjectEmbbeded As New Xml.XmlDocument()
                    xmlObjectEmbbeded.LoadXml($"<doc>{result.ObjectEmbbeded}</doc>")
                    For Each i As Xml.XmlNode In xmlObjectEmbbeded.SelectNodes("doc/Homologacion")
                        Dim homologacion As New Homologation()
                        With homologacion
                            .Service = New ServiceOrderDetail()
                            .Service.Id = i.SelectSingleNode("Service/ServiceOrderDetailId").AsInt()
                            .Service.CUPSAssociateService = i.SelectSingleNode("Service/CUPSAssociateService").AsInt()
                            .Service.IsDelete = i.SelectSingleNode("Service/IsDelete").AsBoolean()
                            .Service.IsFirstEvent = i.SelectSingleNode("Service/IsFirstEvent").AsBoolean()
                            .Service.ServiceOrderDetailDistribution = New Domain.Entities.TrackableCollection(Of ServiceOrderDetailDistribution)()
                            .Service.ServiceOrderDetailDistribution.Add(New ServiceOrderDetailDistribution())
                            .Service.ServiceOrderDetailDistribution(0).Id = i.SelectSingleNode("Service/Id").AsInt()
                            .Service.ServiceOrderDetailDistribution(0).DistributionType = i.SelectSingleNode("Service/DistributionType").AsByte()
                            .Service.ServiceOrderDetailDistribution(0).LastCaregroupId = i.SelectSingleNode("Service/LastCaregroupId").AsInt()
                            .Service.RecordType = i.SelectSingleNode("Service/RecordType").AsByte()
                            .Service.CUPSEntityId = i.SelectSingleNode("Service/CUPSEntityId").AsInt()
                            .Service.PerformsFunctionalUnitId = i.SelectSingleNode("Service/PerformsFunctionalUnitId").AsInt()
                            .Service.PerformsProfessionalSpecialty = i.SelectSingleNode("Service/PerformsProfessionalSpecialty").AsString()
                            .Service.ServiceDate = i.SelectSingleNode("Service/ServiceDate").AsDateTime()
                            .Service.IPSServiceId = i.SelectSingleNode("Service/IPSServiceId").AsInt()
                            .Service.CodeNameSpeciality = i.SelectSingleNode("Service/CodeNameSpeciality").AsString()
                            .Service.CodeNameFunctionalUnit = i.SelectSingleNode("Service/CodeNameFunctionalUnit").AsString()
                            .Service.CodeNameHealthAdministrator = i.SelectSingleNode("Service/CodeNameHealthAdministrator").AsString()
                            .Service.ThirdPartyId = i.SelectSingleNode("Service/ThirdPartyId").AsInt()
                            .Service.SettlementType = i.SelectSingleNode("Service/SettlementType").AsByte()
                        End With
                        homologacion.Homologations = New List(Of CupsHomologation)()
                        For Each h As Xml.XmlNode In i.SelectNodes("Homologations/CupsHomologation")
                            Dim cHomolocation As New CupsHomologation()
                            With cHomolocation
                                .Id = h.SelectSingleNode("CupsHomologationId").AsInt()
                                .IPSServiceId = h.SelectSingleNode("IPSServiceId").AsInt()
                                .CupsEntityId = h.SelectSingleNode("CupsEntityId").AsInt()
                                .CodeNameCupsEntity = h.SelectSingleNode("CodeNameCupsEntity").AsString()
                                .CodeNameIpsService = h.SelectSingleNode("CodeNameIpsService").AsString()
                                .Activated = h.SelectSingleNode("Activated").AsBoolean()
                            End With
                            homologacion.Homologations.Add(cHomolocation)
                        Next
                        resultHomologation.Add(homologacion)
                    Next
                    res.ObjectEmbbeded = resultHomologation
                End If
                If Not String.IsNullOrEmpty(result.ObjectEmbbededAux) Then
                    Dim listNewServiceOrderD As New List(Of ServiceOrderDetail)()
                    Dim xmlObjectEmbbeded As New Xml.XmlDocument()
                    xmlObjectEmbbeded.LoadXml($"<doc>{result.ObjectEmbbededAux}</doc>")
                    For Each i As Xml.XmlNode In xmlObjectEmbbeded.SelectNodes("doc/ListNewServiceOrderDetail")
                        listNewServiceOrderD.Add(New ServiceOrderDetail(i))
                    Next
                    res.ObjectEmbbededAux = listNewServiceOrderD
                End If
                If String.IsNullOrEmpty(res.Message) AndAlso res.ObjectEmbbeded Is Nothing AndAlso res.ObjectEmbbededAux Is Nothing Then
                    If String.IsNullOrEmpty(res.Message) AndAlso res.MessageResult IsNot Nothing AndAlso res.MessageResult.Any() Then
                        res.Message = String.Join(vbCrLf, res.MessageResult)
                    End If
                End If
                If res.StateResult Then
                    scope.Complete()
                    Return res
                Else
                    scope.Dispose()
                    Return res
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el precio de un servicio
    ''' </summary>
    ''' <param name="idServiceOrder">Id de la orden de servicio a cambiar</param>
    ''' <param name="idServiceOrderDetail">Id del detalle a modificar</param>
    ''' <param name="newValue">Nuevo valor a aplicar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function ChangeServiceValue(ByVal idServiceOrder As Integer, ByVal idServiceOrderDetail As Integer, ByVal newValue As Decimal) As ActionResult
        Dim unitWork As IUnitWork = Me._serviceOrderRepository.UnitWork
        Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim order = Me._serviceOrderRepository.GetServiceOrderByIdWithDetails(idServiceOrder)
                If order.Id > 0 Then
                    If order.ServiceOrderDetail.Any(Function(d) d.Id = idServiceOrderDetail) Then
                        order.ServiceOrderDetail.Where(Function(d) d.Id = idServiceOrderDetail).First().TotalSalesPrice = newValue
                        unitWork.Commit()
                        transaction.Complete()
                        Return New ActionResult() With {.StateResult = True} 'El detalle no existe
                    Else
                        unitWork.RollbackChangesUnitOfWork()
                        transaction.Dispose()
                        Return New ActionResult() With {.StateResult = False, .Message = "{ERR2}"} 'El detalle no existe
                    End If
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    transaction.Dispose()
                    Return New ActionResult() With {.StateResult = False, .Message = "{ERR1}"} 'La orden no existe
                End If
            Catch ex As OptimisticConcurrencyException
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                Return New ActionResult() With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult() With {.StateResult = False}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Bloque o desbloquea un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio</param>
    ''' <param name="isBlockFolio">Valor que indica si se bloquea el folio</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function BlockFolio(ByVal idFolio As Integer, Optional ByVal isBlockFolio As Boolean = True) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim folio = Me._revenueControlDetailRepository.GetRevenueControlDetailById(idFolio)
                Dim admission As Object = Me._admissionRepository.GetAdmissionPOCOByCode(folio?.RevenueControl?.AdmissionNumber)

                If Not isBlockFolio AndAlso admission IsNot Nothing AndAlso {"F", "C"}.Contains(admission.Status) Then
                    Return New ActionResult() With {.StateResult = False, .Message = "No fue posbile la apertura del folio, el ingreso esta facturado o cerrado"}
                End If

                If folio IsNot Nothing AndAlso folio.Id > 0 AndAlso folio.Status <> 2 Then 'Valido que el estado no sea facturado
                    If isBlockFolio Then
                        folio.Status = 3
                    Else
                        folio.Status = 1
                    End If
                    Me._revenueControlDetailRepository.SaveEntity(folio)
                    unitWork.Commit()
                    transaction.Complete()
                    Return New ActionResult() With {.StateResult = True}
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    transaction.Dispose()
                    Return New ActionResult() With {.StateResult = False}
                End If
            Catch ex As OptimisticConcurrencyException
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                Return New ActionResult() With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitWork.RollbackChangesUnitOfWork()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult() With {.StateResult = False}
            End Try
        End Using
    End Function

    Private Function UpdateCategory(FolioId As Integer, CategoryId As Integer) As ActionResult
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim revenueControlDetail As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(FolioId)

                If revenueControlDetail Is Nothing Then
                    scope.Dispose()
                    Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = $"No se encontró el folio con Id {FolioId}"}
                End If

                revenueControlDetail.InvoiceCategoryId = CategoryId
                If revenueControlDetail.Status = 2 Then
                    Dim invoice As Invoice = _invoiceRepository.GetInvoiceByRevenueControlDetailId(revenueControlDetail.Id)
                    If invoice IsNot Nothing AndAlso invoice.Id > 0 Then
                        invoice.InvoiceCategoryId = CategoryId
                        _invoiceRepository.SaveEntity(invoice)

                        Dim accountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumber(invoice.InvoiceNumber)
                        If accountReceivable IsNot Nothing AndAlso accountReceivable.Id <> 0 Then
                            accountReceivable.InvoiceCategoryId = CategoryId
                            _accountReceivableRepository.SaveEntity(accountReceivable)
                        End If
                    End If
                End If
                _revenueControlDetailRepository.SaveEntity(revenueControlDetail)
                _revenueControlDetailRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = "La acción se ejecutó correctamente"}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function UpdateObservation(FolioId As Integer, Observation As String) As ActionResult
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim revenueControlDetail As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(FolioId)
                revenueControlDetail.Observation = Observation
                _revenueControlDetailRepository.SaveEntity(revenueControlDetail)
                _revenueControlDetailRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Private Function UpdateStatusFolio(FolioId As Integer, StatusFolio As Integer) As ActionResult
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim revenueControlDetail As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(FolioId)
                revenueControlDetail.StatusFolioId = StatusFolio
                _revenueControlDetailRepository.SaveEntity(revenueControlDetail)
                _revenueControlDetailRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Private Function UpdateNoPos(FolioId As Integer, IsNoPos As Boolean) As ActionResult
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim revenueControlDetail As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(FolioId)
                ' revenueControlDetail.FolioNoPos = IsNoPos
                _revenueControlDetailRepository.SaveEntity(revenueControlDetail)
                _revenueControlDetailRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function UpdateDescriptionFolio(FolioId As Object, Description As Object) As ActionResult
        Dim unitWork As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim revenueControlDetail As RevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(FolioId)
                ' revenueControlDetail.Description = Description
                _revenueControlDetailRepository.SaveEntity(revenueControlDetail)
                _revenueControlDetailRepository.UnitWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Liquida la cuota de recuperación en un folio
    ''' </summary>
    ''' <param name="admission">Objeto dinamico con los datos del ingreso</param>
    ''' <param name="idFolio">Id del fólio a liquidar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function LiquidateRecoveryFee(transactionContainer As String, admission As Object, idFolio As Integer, serviceOrderDetailDistributionDetail As Integer,
                                         LiquidationType As eLiquidateRecoveryType, Optional ServiceDistributionList As List(Of Object) = Nothing,
                                         Optional liquidateRecovery As Boolean = False, Optional args As Object = Nothing) As ActionResult Implements ILiquidationAdminService.LiquidateRecoveryFee

        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            If idFolio = 0 Then
                Throw New ArgumentNullException("No se ha enviado el parámetro Id de Folio.")
            End If
            If serviceOrderDetailDistributionDetail = 0 Then
                Throw New ArgumentNullException("No se ha enviado el parámetro Id servicio de distribucion.")
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() _
                                                With {.Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim ressult As New ActionResult() With {.StateResult = False}
                Dim res = Me._billingServ.LiquidateRecoveryFee(admission,
                                                               idFolio,
                                                               serviceOrderDetailDistributionDetail,
                                                               ServiceDistributionList,
                                                               LiquidationType,
                                                               liquidateRecovery,
                                                               args)
                If res IsNot Nothing AndAlso res.StateResult = True Then
                    If res.ObjectEmbbeded IsNot Nothing Then
                        Dim resFolio As ActionResult = _billingServ.
                            UpdateRevenueControlDetailValues(res.ObjectEmbbeded.Id, res.ObjectEmbbeded)
                        If res.StateResult AndAlso resFolio IsNot Nothing Then
                            ressult.StateResult = res.StateResult And resFolio.StateResult
                        Else
                            ressult.StateResult = res.StateResult
                        End If
                        scope.Complete()
                        Return New ActionResult(ressult.StateResult, res.Message)
                    Else
                        Return New ActionResult(res.StateResult, res.Message)
                    End If
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Return New ActionResult(False, res.Message)
                End If
            End Using

        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult(False, ResourceManager.GetString("ErrorConcurrence"))
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(False, IndigoManagementExceptions.GetExceptionDetails(ex))
        End Try
    End Function

    Public Function LiquidateRecoveryFeeSP(transactionContainer As String, admission As Object, idFolio As Integer, serviceOrderDetailDistributionDetail As Integer,
                                         LiquidationType As eLiquidateRecoveryType, Optional ServiceDistributionList As List(Of Object) = Nothing,
                                         Optional liquidateRecovery As Boolean = False, Optional args As Object = Nothing) As ActionResult Implements ILiquidationAdminService.LiquidateRecoveryFeeSP
        Try
            If idFolio = 0 Then
                Throw New ArgumentNullException("No se ha enviado el parámetro Id de Folio.")
            End If
            If serviceOrderDetailDistributionDetail = 0 Then
                Throw New ArgumentNullException("No se ha enviado el parámetro Id servicio de distribucion.")
            End If

            Dim ServiceDistributionListXml As String = Nothing
            If ServiceDistributionList IsNot Nothing AndAlso ServiceDistributionList.Any() Then
                ServiceDistributionListXml = String.Join(vbCrLf, ServiceDistributionList.Select(Function(o) $"<ServiceDistributionList><Id>{o.ToString()}</Id></ServiceDistributionList>").ToArray())
            End If
            Dim ListItemsApplyRecoveryFeeXml As String = Nothing
            If CType(args, IDictionary(Of String, Object)).ContainsKey("ListItemsApplyRecoveryFee") Then
                Dim listItemsApplyRecoveryFee As List(Of Object) = CType(args.ListItemsApplyRecoveryFee, List(Of Object))
                If listItemsApplyRecoveryFee IsNot Nothing AndAlso listItemsApplyRecoveryFee.Any() Then
                    ListItemsApplyRecoveryFeeXml = String.Join(vbCrLf, listItemsApplyRecoveryFee.Select(Function(o) $"<ListItemsApplyRecoveryFee><Id>{o.ToString()}</Id></ListItemsApplyRecoveryFee>").ToArray())
                End If
            End If
            Dim TotalsItemsApplyRecoveryFee As Decimal = 0
            If CType(args, IDictionary(Of String, Object)).ContainsKey("TotalsItemsApplyRecoveryFee") Then
                TotalsItemsApplyRecoveryFee = CDec(args.TotalsItemsApplyRecoveryFee)
            End If

            Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", transactionContainer)
            Using connection As New SqlConnection(conx)
                connection.Open()
                Dim command As New SqlCommand("[Billing].[SP_LiquidateRecoveryFee]")
                Dim transaction As SqlTransaction
                transaction = connection.BeginTransaction()
                Try

                    command.Connection = connection
                    command.Transaction = transaction
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.Add(New SqlParameter("@AdmissionCode", args.AdmissionCode.ToString()))
                    command.Parameters.Add(New SqlParameter("@RevenueControlDetailId", idFolio))
                    command.Parameters.Add(New SqlParameter("@TotalsItemsApplyRecoveryFee", TotalsItemsApplyRecoveryFee))
                    command.Parameters.Add(New SqlParameter("@LiquidationType", CInt(LiquidationType)))
                    command.Parameters.Add(New SqlParameter("@serviceOrderDetailDistributionDetailId", serviceOrderDetailDistributionDetail))
                    command.Parameters.Add(New SqlParameter("@ServiceDistributionListXml", If(ServiceDistributionListXml Is Nothing, DBNull.Value, ServiceDistributionListXml)))
                    command.Parameters.Add(New SqlParameter("@ListItemsApplyRecoveryFeeXml", If(ListItemsApplyRecoveryFeeXml Is Nothing, DBNull.Value, ListItemsApplyRecoveryFeeXml)))
                    command.Parameters.Add(New SqlParameter("@Liquidate", liquidateRecovery))
                    Dim dt As New DataTable()
                    Using adapter As New SqlDataAdapter(command)
                        adapter.SelectCommand.CommandTimeout = 90
                        adapter.Fill(dt)
                    End Using

                    If CBool(dt.Rows(0).Item(0)) = False Then
                        transaction.Rollback()
                        Return New ActionResult(False, dt.Rows(0).Item(1).ToString())
                    End If

                    transaction.Commit()
                    Return New ActionResult(True, dt.Rows(0).Item(1).ToString())
                Catch ex As Exception
                    transaction.Rollback()
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(False, IndigoManagementExceptions.GetExceptionDetails(ex))
                Finally
                    connection.Close()
                End Try
            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(False, ResourceManager.GetString("ErrorConcurrence"))
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(False, IndigoManagementExceptions.GetExceptionDetails(ex))
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el tercero del folio
    ''' </summary>
    ''' <param name="objArgs">The object arguments.</param>
    ''' <returns></returns>
    Private Function UpdateThirdPartyFolio(objArgs As Object) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim folio = _revenueControlDetailRepository.GetRevenueControlDetailById(objArgs.FolioId)
                folio.ThirdPartyId = objArgs.ThirdPartyId

                _revenueControlDetailRepository.SaveEntity(folio)
                unitWork.Commit()
                scope.Complete()

            End Using
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Updates the health administrator folio.
    ''' </summary>
    ''' <param name="objArgs">The object arguments.</param>
    ''' <returns></returns>
    Private Function UpdateHealthAdministratorFolio(objArgs As Object) As ActionResult
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim folio = _revenueControlDetailRepository.GetRevenueControlDetailById(objArgs.FolioId)
                folio.HealthAdministratorId = Convert.ToInt32(objArgs.HealthAdministratorId)
                Dim ha As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(Convert.ToInt32(objArgs.HealthAdministratorId))
                folio.ThirdPartyId = ha.ThirdPartyId
                _revenueControlDetailRepository.SaveEntity(folio)
                unitWork.Commit()
                scope.Complete()

            End Using
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de cambiar el estado del ingreso a 'Cerrado'
    ''' </summary>
    Public Function CloseAdmission(admissionNumber As String, containerCrystal As String, ByVal audit As AuditMessage) As ActionResult(Of SP_CloseAdmission_Result) Implements ILiquidationAdminService.CloseAdmission
        Try
            Dim res As SP_CloseAdmission_Result = _revenueControlRepository.CloseAdmission(admissionNumber, containerCrystal, audit.CodeUser)
            If res.StatusResult = True Then
                Return New ActionResult(Of SP_CloseAdmission_Result) With {.StateResult = True, .ObjectEmbbeded = res, .Message = "El Ingreso se facturó Correctamente."}
            Else
                Return New ActionResult(Of SP_CloseAdmission_Result) With {.StateResult = False, .Message = res.MessageResult}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_CloseAdmission_Result) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal audit As AuditMessage) As ActionResult Implements ILiquidationAdminService.AssociateInvoice
        Dim unitWork As IUnitWork = Me._revenueControlDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim res = _revenueControlRepository.SP_AssociateInvoice(RevenueControlDetailId, InvoiceId, audit.CodeUser)
                If res.StatusResult Then
                    unitWork.Commit()
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .Message = res.MessageResult}
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = res.MessageResult}
                End If
            End Using
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Consulta los mipres por sod
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetMipresByServiceOrderDetailId(serviceOrderDetailId As Integer) As List(Of MipresCode) Implements ILiquidationAdminService.GetMipresByServiceOrderDetailId
        Try
            Dim mipres = _mipresCodeRepository.GetByFilter(Function(m) m.ServiceOrderDetailId = serviceOrderDetailId, False)
            Return mipres.ToList()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina un mipre
    ''' </summary>
    ''' <param name="mipresCodeId"></param>
    ''' <returns></returns>
    Public Function DeleteMipresCode(mipresCodeId As Integer) As ActionResult Implements ILiquidationAdminService.DeleteMipresCode
        Try
            Dim mipres = _mipresCodeRepository.FirstOrDefault(Function(m) m.Id = mipresCodeId)
            _mipresCodeRepository.DeleteEntity(mipres)
            _mipresCodeRepository.UnitWork.Commit()

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Guarda los códigos mipres
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <param name="mipresCodes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMipresCodes(serviceOrderDetailIds As List(Of Integer), mipresCodes As List(Of MipresCode), audit As AuditMessage) As ActionResult Implements ILiquidationAdminService.SaveMipresCodes
        Dim uow As IUnitWork = _serviceOrderDetailRepository.UnitWork
        Try
            If serviceOrderDetailIds Is Nothing OrElse Not serviceOrderDetailIds.Any() Then
                Throw New IndigoValidationException("No se ha seleccionado una órden de servicio")
            End If

            If mipresCodes Is Nothing OrElse Not mipresCodes.Any() Then
                Throw New IndigoValidationException("No se ha ingresado ningún código Mipres")
            End If

            Dim sodId = serviceOrderDetailIds(0)
            Dim revenueControlDetailStatus = _serviceOrderDetailRepository.Query(Function(m) m.Id = sodId, False, includes:={"ServiceOrderDetailDistribution.RevenueControlDetail"}) _
                .Select(Function(m) m.ServiceOrderDetailDistribution.FirstOrDefault().RevenueControlDetail.Status).FirstOrDefault()

            If revenueControlDetailStatus <> 1 Then
                Throw New IndigoValidationException("El folio no se encuentra abierto")
            End If

            Dim toDelete = mipresCodes.FindAll(Function(m) m.ChangeTracker.State = ObjectState.Deleted)
            mipresCodes = mipresCodes.FindAll(Function(m) m.Id = 0 AndAlso m.ChangeTracker.State = ObjectState.Added)

            mipresCodes.ForEach(Sub(o)
                                    o.CreationUser = audit.CodeUser
                                    o.CreationDate = Date.Now
                                End Sub)

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                }
            )
                toDelete.ForEach(Sub(o) _mipresCodeRepository.DeleteEntity(o))

                For Each sodId In serviceOrderDetailIds
                    Dim sod = _serviceOrderDetailRepository.FirstOrDefault(Function(m) m.Id = sodId, includes:={"MipresCode"})

                    If sod Is Nothing Then
                        Throw New IndigoValidationException("Órden de servicio no encontrada")
                    End If

                    If serviceOrderDetailIds.Count > 1 AndAlso sod.MipresCode.Any() Then
                        For Each mp In sod.MipresCode
                            _mipresCodeRepository.DeleteEntity(mp)
                        Next
                    End If


                    mipresCodes.ForEach(Sub(o) sod.MipresCode.Add(o.Clone()))
                    _serviceOrderDetailRepository.SaveEntity(sod)
                Next

                uow.Commit()
                scope.Complete()
            End Using
            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            uow.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' obtiene datos de la vista y se le da tratamiento a los cups de imagenologia para leer los
    ''' medicos que la realizo leyendo las tablas de cristal HCORDIMAG Y AMBORDIMA
    ''' </summary>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    Public Function GetViewListNoSurgical(InvoiceId As Integer, invoiceDetailId As Integer?) As List(Of ViewListNoSurgical) Implements ILiquidationAdminService.GetViewListNoSurgical
        If InvoiceId = 0 Then
            Throw New ArgumentNullException("InvoiceId")
        End If
        Dim unitOfWork As IUnitWork = Me._viewListNoSurgicalRepository.UnitWork
        Try
            Dim ViewListNoSurgical = New List(Of ViewListNoSurgical)
            ViewListNoSurgical = Me._viewListNoSurgicalRepository.GetByFilter(Function(x) x.InvoiceId = InvoiceId AndAlso (invoiceDetailId Is Nothing OrElse x.InvoiceDetailId = invoiceDetailId), False)
            Dim _count = 0
            For Each item In ViewListNoSurgical.Where(Function(f) f.ServiceType = 3) _
                .GroupBy(Function(m) New With {Key m.AdmissionNumber, Key m.Code, Key m.ServiceOrderId})
                Dim ImagingH = Me._hCORDIMAGRepository.GetHCORDIMAGByNumberandCupsCode(item.Key.AdmissionNumber, item.Key.Code, item.Key.ServiceOrderId)
                Dim ImagingA = Me._aMBORDIMARepository.GetAMBORDIMAByNumberandCupsCode(item.Key.AdmissionNumber, item.Key.Code, item.Key.ServiceOrderId)
                Dim Union = ImagingH.Union(ImagingA)
                If Union.Count = 0 Then
                    Continue For
                End If
                For i As Integer = 0 To item.Count() - 1
                    If Union(i) IsNot Nothing Then
                        item(i).PerformsHealthProfessionalCode = Union(i).MEDREALEC
                        Dim Nit = Union(i).CODIGONIT.Trim()
                        Dim ThirdPartyId = Me._thirdpartyRepository.FirstOrDefault(Function(h) h.Nit = Nit, False)?.Id
                        item(i).ThirdPartyId = ThirdPartyId
                    End If
                Next
            Next

            Return ViewListNoSurgical
        Catch ex As Exception
            Return New List(Of ViewListNoSurgical)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de ordenes de servicios de una admisión para incluir a otro servicio
    ''' </summary>
    ''' <param name="listIds">Lista de IDs a excluir</param>
    ''' <param name="admissionNumber">Número de admisión</param>
    ''' <returns>Lista de detalles de orden de servicio</returns>
    Public Function GetListServiceOrderDetail(listIds As List(Of Integer), admissionNumber As String) As List(Of SP_GetListServiceOrderDetail) Implements ILiquidationAdminService.GetListServiceOrderDetail
        Try
            Dim xmlids = Utils.SerializeToXmlString(listIds)

            Return _revenueControlRepository.ExecuteStoredProcedure(Of SP_GetListServiceOrderDetail)("[Billing].[SP_GetListServiceOrderDetail]", {("@AdmissionNumber", admissionNumber),
                                                                                                                              ("@ids", xmlids.ToString())}).ToList()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtiene una lista de ingresos unificados por la lista de ids de folios y el numero de ingreso cabecera
    ''' </summary>
    ''' <param name="listFoliosIds">Lista de ids de folios</param>
    ''' <param name="admissionNumber">Numero de ingreso cabecera</param>
    ''' <returns>Lista de ingresos unificados</returns>
    Public Function GetListUnifiedAdmissions(listFoliosIds As List(Of Integer), admissionNumber As String) As ActionResult(Of List(Of ADINGRESO)) Implements ILiquidationAdminService.GetListUnifiedAdmissions
        Try
            Dim unifiedAdmissions = _revenueControlRepository.UnifiedAdmissionListByFolioIds(listFoliosIds, admissionNumber)

            If unifiedAdmissions.Count = 0 Then
                Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = False, .Message = "No se encontraron ingresos unificados"}
            End If

            Dim admissionsInfo As New List(Of ADINGRESO)
            For Each ua In unifiedAdmissions
                Dim admissionInfo = _admissionRepository.GetAdmissionByCode(ua)
                admissionsInfo.Add(admissionInfo)
            Next

            Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = True, .ObjectEmbbeded = admissionsInfo, .Message = "El folio contiene servicios asociados a otro ingreso. Verifique la fecha de egreso."}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ADINGRESO)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "Enums"
    ''' <summary>
    ''' Enumeración para tipo de servicio
    ''' </summary>
    Public Enum eRecordType
        Services = 1
        Medicamentos = 2
    End Enum

    ''' <summary>
    ''' Enumeracion para saber en que caso se genera la cuenta por cobrar
    ''' </summary>
    Public Enum eAccountReceivableGenerate
        ThirdParty = 1 'Entidad
        Patient = 2 'Paciente
        Pagare = 3 'Pagare
    End Enum
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'fileStream.Dispose()
                _serviceOrderDetailAdminService.Dispose()
                _surgeriesPercentageManualAdminService.Dispose()
                _billingServ.Dispose()
                _invoiceAdminService.Dispose()
                _billingAuthorizationAdminService.Dispose()
                _accountingDocumentAdminService.Dispose()
                _accountReceivableAdminService.Dispose()
                _portFolioSequenceAdminService.Dispose()
                _settingBillingAdminService.Dispose()
                _portfolioTransferAdminService.Dispose()
                _serviceOrderAdminService.Dispose()
                _contractServices.Dispose()
                'For Each i In memoryStreamArray
                '    i.Dispose()
                'Next
            End If
            'fileStream = Nothing
            'memoryStreamArray = Nothing
            _aDCONCOEXrepository = Nothing
            _aMBORDLABRepository = Nothing
            _aMBORDIMARepository = Nothing
            _aMBORDPATRepository = Nothing
            _serviceOrderDetailAdminService = Nothing
            _surgeriesPercentageManualAdminService = Nothing
            _admissionRepository = Nothing
            _revenueControlRepository = Nothing
            _revenueControlDetailRepository = Nothing
            _serviceOrderDetailDistributionRepository = Nothing
            _companySettingsRepository = Nothing
            _serviceOrderRepository = Nothing
            _rateManualRepository = Nothing
            _cupsHomologation = Nothing
            _functionalUnitRepository = Nothing
            _costCenterRepository = Nothing
            _cupsRepository = Nothing
            _ipsServiceRepository = Nothing
            _surgicalProcedureServiceRepository = Nothing
            _rateManualDetailSurgicalRepository = Nothing
            _rateManualDetailRepository = Nothing
            _specialityRepository = Nothing
            _serviceOrderDetailRepository = Nothing
            _serviceOrderDetailSurgicalRepository = Nothing
            _caregroupRepository = Nothing
            _LiquidationDataRepository = Nothing
            _productRateDetailRepository = Nothing
            _billingServ = Nothing
            _invoiceAdminService = Nothing
            _billingAuthorizationRepository = Nothing
            _billingAuthorizationAdminService = Nothing
            _accountingDocumentAdminService = Nothing
            _documentTypeRepository = Nothing
            _accountingDocumentRepository = Nothing
            _accountReceivableAdminService = Nothing
            _portFolioSequenceAdminService = Nothing
            _invoiceRepository = Nothing
            _medicalFeesCausationRepository = Nothing
            _settingBillingRepository = Nothing
            _settingBillingAdminService = Nothing
            _portfolioTransferAdminService = Nothing
            _portfolioAdvanceRepository = Nothing
            _serviceOrderAdminService = Nothing
            _billingSequenceRepository = Nothing
            _surgicalProcedureDetail = Nothing
            _accountReceivableRepository = Nothing
            _portfolioTransferRepository = Nothing
            _healthAdministratorRepository = Nothing
            _contractServices = Nothing
            _hCREGEGRERepository = Nothing
            _iNPACIENTTOPANURepository = Nothing
            _patientRepository = Nothing
            _invoicePortfolioAdvanceRepository = Nothing
            _stayRepository = Nothing
            _iHCJUNOPMHRepository = Nothing
            _inventoryProductRepository = Nothing
            _pharmaceuticalDispensingRepository = Nothing
            _pharmaceuticalDispensingDevolutionRepository = Nothing
            _thirdpartyRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
