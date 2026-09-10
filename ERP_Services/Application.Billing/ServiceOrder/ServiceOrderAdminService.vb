'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal
Imports System.Text
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure

#End Region

Public Class ServiceOrderAdminService
    Implements IServiceOrderAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _serviceOrderRepository As IServiceOrderRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenceDRepository As IBillingSequenceDetailRepository
    ''' <summary>
    ''' repositorio de la tabla de control del folio
    ''' </summary>
    ''' <remarks></remarks>
    Private _revenueControlRepository As IRevenueControlRepository
    ''' <summary>
    ''' repositorio de la tabla de control del folio
    ''' </summary>
    ''' <remarks></remarks>
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository

    Private _revenueControlDetailRepositoryFind As IRevenueControlDetailRepository
    ''' <summary>
    ''' repositorio de la distribucion de los folio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository
    ''' <summary>
    ''' repositorio de grupo de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepository As ICareGroupRepository

    ''' <summary>
    ''' Repositorio de ingresos
    ''' </summary>
    Private _admissionRepository As IAdmissionRepository

    Private _billingService As IBillingServices

    Private _stayRepository As IStayRepository
    Private _stayDetailRepository As IStayDetailRepository

    Private _healthAdministratorRepository As IHealthAdministratorRepository

    Private _cupsEntityRepository As ICupsEntityRepository
    Private _productGroupRepository As IProductGroupsRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _billingConceptRepository As IBillingConceptRepository

#End Region

#Region "Builder"
    Public Sub New(serviceOrderRepository As IServiceOrderRepository, secuenceDRepository As IBillingSequenceDetailRepository, revenueControlRepository As IRevenueControlRepository,
                   careGroupRepository As ICareGroupRepository, revenueControlDetailRepository As IRevenueControlDetailRepository, admissionRepository As IAdmissionRepository,
                   serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository, billingService As IBillingServices, stayRepository As IStayRepository,
                   stayDetailRepository As IStayDetailRepository, revenueControlDetailRepositoryFind As IRevenueControlDetailRepository, healthAdministratorRepository As IHealthAdministratorRepository,
                   cupsEntityRepository As ICupsEntityRepository, productGroupRepository As IProductGroupsRepository, functionalUnitRepository As IFunctionalUnitRepository, billingConceptRepository As IBillingConceptRepository)
        If serviceOrderRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderRepository")
        End If
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If revenueControlRepository Is Nothing Then
            Throw New ArgumentNullException("revenueControlRepository")
        End If
        If careGroupRepository Is Nothing Then
            Throw New ArgumentNullException("careGroupRepository")
        End If
        If revenueControlDetailRepository Is Nothing Then
            Throw New ArgumentNullException("revenueControlDetailRepository")
        End If
        If admissionRepository Is Nothing Then
            Throw New ArgumentNullException("admissionRepository")
        End If
        If serviceOrderDetailDistributionRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailDistributionRepository")
        End If
        If billingService Is Nothing Then
            Throw New ArgumentNullException("billingService")
        End If
        If stayRepository Is Nothing Then
            Throw New ArgumentNullException("stayRepository")
        End If
        If healthAdministratorRepository Is Nothing Then
            Throw New ArgumentNullException("healthAdministrator")
        End If
        _stayRepository = stayRepository
        _billingService = billingService
        _serviceOrderRepository = serviceOrderRepository
        _secuenceDRepository = secuenceDRepository
        _revenueControlRepository = revenueControlRepository
        _careGroupRepository = careGroupRepository
        _revenueControlDetailRepository = revenueControlDetailRepository
        _admissionRepository = admissionRepository
        _serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
        _stayDetailRepository = stayDetailRepository
        _revenueControlDetailRepositoryFind = revenueControlDetailRepositoryFind
        _healthAdministratorRepository = healthAdministratorRepository
        _cupsEntityRepository = cupsEntityRepository
        _productGroupRepository = productGroupRepository
        _functionalUnitRepository = functionalUnitRepository
        _billingConceptRepository = billingConceptRepository
    End Sub
#End Region

#Region "Methods"
    Public Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As InvoiceDetail Implements IServiceOrderAdminService.GetInvoiceDetailByServiceOrderDetailId
        Try
            Return Me._serviceOrderRepository.GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New InvoiceDetail
        End Try
    End Function
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    Public Function GetServiceOrder(code As String, ByVal audit As AuditMessage) As ServiceOrder Implements IServiceOrderAdminService.GetServiceOrder
        If code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Dim ServiceOrder = _serviceOrderRepository.GetServiceOrder(code)
            If ServiceOrder IsNot Nothing AndAlso ServiceOrder.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ServiceOrder)(ServiceOrder, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return ServiceOrder
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ServiceOrder()
        End Try
    End Function

    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderById(id As Integer) As ServiceOrder Implements IServiceOrderAdminService.GetServiceOrderById
        Try
            Return _serviceOrderRepository.GetServiceOrderById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ServiceOrder()
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of ServiceOrderDetail) Implements IServiceOrderAdminService.GetServiceOrderDetailByServiceOrderId
        Try
            Return _serviceOrderRepository.GetServiceOrderDetailByServiceOrderId(ServiceOrderId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ServiceOrderDetail)
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el objeto de ordenes de servicios 
    ''' </summary>
    ''' <param name="serviceOrder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetXmlServiceOrder(serviceOrder As ServiceOrder) As String

        For i = 0 To serviceOrder.ServiceOrderDetail.Count - 1
            serviceOrder.ServiceOrderDetail(i).RowXml = i + 1
        Next

        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<ServiceOrder>")
        builder.Append("<Id>" & serviceOrder.Id & "</Id>")
        builder.Append("<Code>" & serviceOrder.Code & "</Code>")
        builder.Append("<AdmissionNumber>" & serviceOrder.AdmissionNumber & "</AdmissionNumber>")
        builder.Append("<PatientCode>" & serviceOrder.PatientCode & "</PatientCode>")
        builder.Append("<OrderDate>" & serviceOrder.OrderDate.ToString("dd/MM/yyyy HH:mm:ss") & "</OrderDate>")
        builder.Append("<AffectInventory>" & serviceOrder.AffectInventory & "</AffectInventory>")
        builder.Append("<Status>" & serviceOrder.Status & "</Status>")
        If serviceOrder.EntityCode IsNot Nothing AndAlso serviceOrder.EntityCode IsNot String.Empty Then
            builder.Append("<EntityCode>" & serviceOrder.EntityCode & "</EntityCode>")
        End If
        If serviceOrder.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & serviceOrder.EntityId & "</EntityId>")
        End If
        If serviceOrder.EntityName IsNot Nothing AndAlso serviceOrder.EntityName IsNot String.Empty Then
            builder.Append("<EntityName>" & serviceOrder.EntityName & "</EntityName>")
        End If
        builder.Append("<OperatingUnitId>" & serviceOrder.OperatingUnitId & "</OperatingUnitId>")
        For Each detail In serviceOrder.ServiceOrderDetail

            builder.Append("<ServiceOrderDetail>")
            builder.Append("<RowXml>" & detail.RowXml & "</RowXml>")
            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<CareGroupId>" & detail.CareGroupId & "</CareGroupId>")
            builder.Append("<ExcludeIds>" & detail.ExcludeIds & "</ExcludeIds>")
            If detail.HealthAdministratorId IsNot Nothing Then
                builder.Append("<HealthAdministratorId>" & detail.HealthAdministratorId & "</HealthAdministratorId>")
            End If
            If detail.ThirdPartyId IsNot Nothing Then
                builder.Append("<ThirdPartyId>" & detail.ThirdPartyId & "</ThirdPartyId>")
            End If
            builder.Append("<ServiceType>" & detail.ServiceType & "</ServiceType>")
            builder.Append("<RecordType>" & detail.RecordType & "</RecordType>")

            If detail.CUPSEntityId IsNot Nothing Then
                builder.Append("<CUPSEntityId>" & detail.CUPSEntityId & "</CUPSEntityId>")
            End If

            If detail.IPSServiceId IsNot Nothing Then
                builder.Append("<IPSServiceId>" & detail.IPSServiceId & "</IPSServiceId>")
            End If

            If detail.HospitalStayId IsNot Nothing Then
                builder.Append("<HospitalStayId>" & detail.HospitalStayId & "</HospitalStayId>")
            End If
            If detail.HospitalStayDetailId IsNot Nothing Then
                builder.Append("<HospitalStayDetailId>" & detail.HospitalStayDetailId & "</HospitalStayDetailId>")
            End If

            If detail.ControlExternalConsultation IsNot Nothing Then
                builder.Append("<ControlExternalConsultation>" & detail.ControlExternalConsultation & "</ControlExternalConsultation>")
            End If
            If detail.ControlExternalConsultationCode IsNot Nothing Then
                builder.Append("<ControlExternalConsultationCode>" & detail.ControlExternalConsultationCode.ToString().Replace(",", ".") & "</ControlExternalConsultationCode>")
            End If

            builder.Append("<CUPSAssociateService>" & detail.CUPSAssociateService & "</CUPSAssociateService>")

            If detail.CodeAssociateService IsNot Nothing AndAlso detail.CodeAssociateService IsNot String.Empty > 0 Then
                builder.Append("<CodeAssociateService>" & detail.CodeAssociateService & "</CodeAssociateService>")
            End If

            builder.Append("<IsPackage>" & detail.IsPackage & "</IsPackage>")

            If detail.IsPackage Then
                For Each package In detail.ServiceOrderDetail1
                    builder.Append("<ServiceOrderDetailPackage>")
                    builder.Append("<ItemPackageId>" & package.Id & "</ItemPackageId>")
                    builder.Append("</ServiceOrderDetailPackage>")
                Next
            End If

            builder.Append("<Packaging>" & detail.Packaging & "</Packaging>")

            If detail.PackageServiceOrderDetailId IsNot Nothing Then
                builder.Append("<PackageServiceOrderDetailId>" & detail.PackageServiceOrderDetailId & "</PackageServiceOrderDetailId>")
            End If

            builder.Append("<LiquidationType>" & detail.LiquidationType & "</LiquidationType>")

            If detail.Presentation IsNot Nothing Then
                builder.Append("<Presentation>" & detail.Presentation & "</Presentation>")
            End If

            If detail.ProductId IsNot Nothing Then
                builder.Append("<ProductId>" & detail.ProductId & "</ProductId>")
            End If

            builder.Append("<InvoicedQuantity>" & detail.InvoicedQuantity & "</InvoicedQuantity>")
            builder.Append("<SupplyQuantity>" & detail.SupplyQuantity & "</SupplyQuantity>")
            builder.Append("<DevolutionQuantity>" & detail.DevolutionQuantity & "</DevolutionQuantity>")
            builder.Append("<RateManualSalePrice>" & detail.RateManualSalePrice.ToString().Replace(",", ".") & "</RateManualSalePrice>")
            builder.Append("<CostValue>" & detail.CostValue.ToString().Replace(",", ".") & "</CostValue>")


            builder.Append("<ServiceDate>" & detail.ServiceDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ServiceDate>")
            If detail.AuthorizationNumber IsNot Nothing AndAlso detail.AuthorizationNumber IsNot String.Empty Then
                builder.Append("<AuthorizationNumber>" & detail.AuthorizationNumber & "</AuthorizationNumber>")
            End If

            builder.Append("<PerformsFunctionalUnitId>" & detail.PerformsFunctionalUnitId & "</PerformsFunctionalUnitId>")
            If detail.PerformsHealthProfessionalCode IsNot Nothing AndAlso detail.PerformsHealthProfessionalCode IsNot String.Empty Then
                builder.Append("<PerformsHealthProfessionalCode>" & detail.PerformsHealthProfessionalCode.Trim() & "</PerformsHealthProfessionalCode>")
            End If
            If detail.PerformsProfessionalSpecialty IsNot Nothing AndAlso detail.PerformsProfessionalSpecialty IsNot String.Empty Then
                builder.Append("<PerformsProfessionalSpecialty>" & detail.PerformsProfessionalSpecialty.Trim() & "</PerformsProfessionalSpecialty>")
            End If


            If detail.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                builder.Append("<PerformsHealthProfessionalThirdPartyId>" & detail.PerformsHealthProfessionalThirdPartyId & "</PerformsHealthProfessionalThirdPartyId>")
            End If

            If detail.BillingConceptId IsNot Nothing Then
                builder.Append("<BillingConceptId>" & detail.BillingConceptId & "</BillingConceptId>")
            End If
            builder.Append("<CostCenterId>" & detail.CostCenterId & "</CostCenterId>")


            builder.Append("<SettlementType>" & detail.SettlementType & "</SettlementType>")

            If detail.SettlementType = 2 Or detail.SettlementType = 3 Then
                If detail.IncludeServiceOrderDetailId IsNot Nothing AndAlso detail.IncludeServiceOrderDetailId > 0 Then
                    builder.Append("<IncludeServiceOrderDetailId>" & detail.IncludeServiceOrderDetailId & "</IncludeServiceOrderDetailId>")
                ElseIf detail.ServiceOrderDetail3 IsNot Nothing Then
                    builder.Append("<IncludeServiceOrderDetailIdRow>" & detail.ServiceOrderDetail3.RowXml & "</IncludeServiceOrderDetailIdRow>")
                End If
            End If

            If detail.RecoveryRatio IsNot Nothing Then
                builder.Append("<RecoveryRatio>" & detail.RecoveryRatio.ToString().Replace(",", ".") & "</RecoveryRatio>")
            End If
            If detail.RateManualId IsNot Nothing Then
                builder.Append("<RateManualId>" & detail.RateManualId & "</RateManualId>")
            End If
            If detail.RateManualType IsNot Nothing Then
                builder.Append("<RateManualType>" & detail.RateManualType & "</RateManualType>")
            End If
            If detail.RateManualDetailId IsNot Nothing Then
                builder.Append("<RateManualDetailId>" & detail.RateManualDetailId & "</RateManualDetailId>")
            End If

            If detail.DefinitionRateDetailId IsNot Nothing Then
                builder.Append("<DefinitionRateDetailId>" & detail.DefinitionRateDetailId & "</DefinitionRateDetailId>")
            End If
            If detail.DefinitionRateDetailConditionId IsNot Nothing Then
                builder.Append("<DefinitionRateDetailConditionId>" & detail.DefinitionRateDetailConditionId & "</DefinitionRateDetailConditionId>")
            End If
            builder.Append("<GrossValue>" & detail.GrossValue.ToString().Replace(",", ".") & "</GrossValue>")
            builder.Append("<TaxValue>" & detail.TaxValue.ToString().Replace(",", ".") & "</TaxValue>")
            builder.Append("<SubTotalSalesPrice>" & detail.SubTotalSalesPrice.ToString().Replace(",", ".") & "</SubTotalSalesPrice>")
            builder.Append("<ThirdPartyDiscount>" & detail.ThirdPartyDiscount.ToString().Replace(",", ".") & "</ThirdPartyDiscount>")
            builder.Append("<ThirdPartyDiscountPercentage>" & detail.ThirdPartyDiscountPercentage.ToString().Replace(",", ".") & "</ThirdPartyDiscountPercentage>")

            builder.Append("<TotalSalesPrice>" & detail.TotalSalesPrice.ToString().Replace(",", ".") & "</TotalSalesPrice>")
            builder.Append("<GrandTotalSalesPrice>" & detail.GrandTotalSalesPrice.ToString().Replace(",", ".") & "</GrandTotalSalesPrice>")
            builder.Append("<SurchargeApply>" & detail.SurchargeApply & "</SurchargeApply>")
            If detail.SurgicalInterventionType IsNot Nothing Then
                builder.Append("<SurgicalInterventionType>" & detail.SurgicalInterventionType & "</SurgicalInterventionType>")
            End If
            builder.Append("<SurgeryNumber>" & detail.SurgeryNumber & "</SurgeryNumber>")
            builder.Append("<IsFirstEvent>" & detail.IsFirstEvent & "</IsFirstEvent>")
            builder.Append("<IsAnnulled>" & detail.IsAnnulled & "</IsAnnulled>")
            builder.Append("<IsDelete>" & detail.IsDelete & "</IsDelete>")
            builder.Append("<IncomeMainAccountId>" & detail.IncomeMainAccountId & "</IncomeMainAccountId>")


            builder.Append("<EntityState>" & detail.ChangeTracker.State.ToString() & "</EntityState>")

            If detail.ApplyRIAS IsNot Nothing Then
                builder.Append("<ApplyRIAS>" & detail.ApplyRIAS.ToString() & "</ApplyRIAS>")
            Else
                builder.Append("<ApplyRIAS>-</ApplyRIAS>")
            End If
            builder.Append("<RIASCupsId>" & detail.RIASCupsId & "</RIASCupsId>")
            builder.Append("<RealizedQuantity>" & detail.RealizedQuantity & "</RealizedQuantity>")
            builder.Append("<CUPSEntityContractDescriptionId>" & detail.CUPSEntityContractDescriptionId & "</CUPSEntityContractDescriptionId>")
            builder.Append("<QuotationServiceOrderDetailId>" & detail.QuotationServiceOrderDetailId & "</QuotationServiceOrderDetailId>")
            builder.Append("<TraceabilityPaperworkEventsId>" & detail.TraceabilityPaperworkEventsId & "</TraceabilityPaperworkEventsId>")
            builder.Append(String.Format("<IsServiceOrderDetailControlJustify>{0}</IsServiceOrderDetailControlJustify>", detail.IsServiceOrderDetailControlJustify))
            builder.Append(String.Format("<ServiceOrderDetailControlJustification>{0}</ServiceOrderDetailControlJustification>", detail.ServiceOrderDetailControlJustification))

            If detail.ContractPackageId.HasValue Then
                builder.Append($"<ContractPackageId>{detail.ContractPackageId.Value}</ContractPackageId>")
            End If

            'agrego los detalles quirurgicos que vengan como eliminados
            If detail.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ServiceOrderDetailSurgical") Then
                For Each detailSurgical In detail.ChangeTracker.ObjectsRemovedFromCollectionProperties("ServiceOrderDetailSurgical")
                    builder.Append("<ServiceOrderDetailSurgical>")
                    builder.Append("<Id>" & detailSurgical.Id & "</Id>")
                    builder.Append("<ServiceOrderDetailId>" & detailSurgical.ServiceOrderDetailId & "</ServiceOrderDetailId>")
                    builder.Append("<ServiceOrderDetailIdRow>" & detail.RowXml & "</ServiceOrderDetailIdRow>")
                    builder.Append("<IPSServiceId>" & detailSurgical.IPSServiceId & "</IPSServiceId>")
                    builder.Append("<InvoicedQuantity>" & detailSurgical.InvoicedQuantity & "</InvoicedQuantity>")
                    builder.Append("<LiquidationPercentage>" & detailSurgical.LiquidationPercentage & "</LiquidationPercentage>")
                    builder.Append("<RateManualSalePrice>" & detailSurgical.RateManualSalePrice & "</RateManualSalePrice>")
                    builder.Append("<TotalSalesPrice>" & detailSurgical.TotalSalesPrice & "</TotalSalesPrice>")
                    If detailSurgical.PerformsHealthProfessionalCode IsNot Nothing AndAlso detailSurgical.PerformsHealthProfessionalCode IsNot String.Empty Then
                        builder.Append("<PerformsHealthProfessionalCode>" & detailSurgical.PerformsHealthProfessionalCode & "</PerformsHealthProfessionalCode>")
                    End If
                    If detailSurgical.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                        builder.Append("<PerformsHealthProfessionalThirdPartyId>" & detailSurgical.PerformsHealthProfessionalThirdPartyId & "</PerformsHealthProfessionalThirdPartyId>")
                    End If
                    builder.Append("<CostValue>" & detailSurgical.CostValue & "</CostValue>")
                    builder.Append("<BillingConceptId>" & detailSurgical.BillingConceptId & "</BillingConceptId>")
                    builder.Append("<CostCenterId>" & detailSurgical.CostCenterId & "</CostCenterId>")

                    If detailSurgical.RateManualDetailSurgicalId IsNot Nothing Then
                        builder.Append("<RateManualDetailSurgicalId>" & detailSurgical.RateManualDetailSurgicalId & "</RateManualDetailSurgicalId>")
                    End If


                    builder.Append("<SurchargeApply>" & detailSurgical.SurchargeApply & "</SurchargeApply>")
                    builder.Append("<OnlyMedicalFees>" & detailSurgical.OnlyMedicalFees & "</OnlyMedicalFees>")
                    builder.Append("<IncomeMainAccountId>" & detailSurgical.IncomeMainAccountId & "</IncomeMainAccountId>")
                    builder.Append("<EntityState>" & detailSurgical.ChangeTracker.State.ToString() & "</EntityState>")
                    builder.Append("</ServiceOrderDetailSurgical>")
                Next
            End If

            For Each detailSurgical In detail.ServiceOrderDetailSurgical
                builder.Append("<ServiceOrderDetailSurgical>")
                builder.Append("<Id>" & detailSurgical.Id & "</Id>")
                builder.Append("<ServiceOrderDetailId>" & detailSurgical.ServiceOrderDetailId & "</ServiceOrderDetailId>")
                builder.Append("<ServiceOrderDetailIdRow>" & detail.RowXml & "</ServiceOrderDetailIdRow>")
                builder.Append("<IPSServiceId>" & detailSurgical.IPSServiceId & "</IPSServiceId>")
                builder.Append("<InvoicedQuantity>" & detailSurgical.InvoicedQuantity & "</InvoicedQuantity>")
                builder.Append("<LiquidationPercentage>" & detailSurgical.LiquidationPercentage.ToString().Replace(",", ".") & "</LiquidationPercentage>")
                builder.Append("<RateManualSalePrice>" & detailSurgical.RateManualSalePrice.ToString().Replace(",", ".") & "</RateManualSalePrice>")
                builder.Append("<TotalSalesPrice>" & detailSurgical.TotalSalesPrice.ToString().Replace(",", ".") & "</TotalSalesPrice>")
                If detailSurgical.PerformsHealthProfessionalCode IsNot Nothing AndAlso detailSurgical.PerformsHealthProfessionalCode IsNot String.Empty Then
                    builder.Append("<PerformsHealthProfessionalCode>" & detailSurgical.PerformsHealthProfessionalCode & "</PerformsHealthProfessionalCode>")
                End If
                If detailSurgical.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                    builder.Append("<PerformsHealthProfessionalThirdPartyId>" & detailSurgical.PerformsHealthProfessionalThirdPartyId & "</PerformsHealthProfessionalThirdPartyId>")
                End If
                builder.Append("<CostValue>" & detailSurgical.CostValue.ToString().Replace(",", ".") & "</CostValue>")
                builder.Append("<BillingConceptId>" & detailSurgical.BillingConceptId & "</BillingConceptId>")
                builder.Append("<CostCenterId>" & detailSurgical.CostCenterId & "</CostCenterId>")

                If detailSurgical.RateManualDetailSurgicalId IsNot Nothing Then
                    builder.Append("<RateManualDetailSurgicalId>" & detailSurgical.RateManualDetailSurgicalId & "</RateManualDetailSurgicalId>")
                End If


                builder.Append("<SurchargeApply>" & detailSurgical.SurchargeApply & "</SurchargeApply>")
                builder.Append("<OnlyMedicalFees>" & detailSurgical.OnlyMedicalFees & "</OnlyMedicalFees>")
                builder.Append("<IncomeMainAccountId>" & detailSurgical.IncomeMainAccountId & "</IncomeMainAccountId>")
                builder.Append("<EntityState>" & detailSurgical.ChangeTracker.State.ToString() & "</EntityState>")
                builder.Append("</ServiceOrderDetailSurgical>")
            Next
            builder.Append("</ServiceOrderDetail>")
        Next
        builder.Append("</ServiceOrder>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Guardar una orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrder"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ServiceOrder Vacio</exception>
    Public Function SaveServiceOrder(ServiceOrder As ServiceOrder, audit As AuditMessage, Optional idSequence As Long = 0, Optional idFolio As Integer = 0, Optional action As EActionServiceOrder = EActionServiceOrder.NoAction) As ActionResult(Of ServiceOrder) Implements IServiceOrderAdminService.SaveServiceOrder
        If ServiceOrder Is Nothing Then
            Throw New ArgumentNullException("ServiceOrder Vacio")
        End If
        Dim serviceOrderXml = GetXmlServiceOrder(ServiceOrder)
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Dim result = _serviceOrderRepository.GenerateServiceOrderSP(serviceOrderXml, audit.CodeUser)
            Dim resultList = result.ToList()
            Dim actionResult As New ActionResult(Of ServiceOrder)()
            If (From e In resultList Where e.Status = 1 Select e).Count > 0 Then
                actionResult.StateResult = True
                actionResult.Message = String.Join(vbCrLf, (From e In resultList Where e.Status = 2 Select e.Message).ToList())
                actionResult.ObjectEmbbeded = _serviceOrderRepository.GetServiceOrderById((From e In resultList Where e.Status = 1 Select e).ToList()(0).ServiceOrderId)
                scope.Complete()
                Return actionResult
            Else
                actionResult.StateResult = False
                actionResult.StateResultAux = False
                actionResult.Message = String.Join(vbCrLf, (From e In resultList Where e.Status = 3 Select e.Message).ToList())
                actionResult.ObjectEmbbeded = ServiceOrder
                scope.Dispose()
                Return actionResult
            End If
            
            '    Try
            '        Dim messageNotificationContract As New StringBuilder()
            '        Dim seq As BillingSequenceDetail = Nothing
            '        Dim auditProcess As IndigoAuditSimpleEntity(Of ServiceOrder)
            '        Dim status As Integer
            '        Dim auxServiceOrder As ServiceOrder = Nothing

            '        Dim admission As Object = _admissionRepository.GetAdmissionByServiceOrder(ServiceOrder.AdmissionNumber)
            '        If ServiceOrder.OrderDate < admission.AdmissionDate Then
            '            scope.Dispose()
            '            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("InvalidServiceOrderDate", "Billing")}
            '        End If
            '        'valido que el ingreso no este facturado
            '        If admission.Status = "F" Then
            '            scope.Dispose()
            '            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = "No se puede crear la orden de servicio porque el ingreso " + ServiceOrder.AdmissionNumber + " esta facturado"}
            '        End If
            '        If ServiceOrder.Code Is Nothing OrElse ServiceOrder.Code.Trim().Equals(String.Empty) Then
            '            seq = _secuenceDRepository.GetSequenseDById(idSequence)
            '            If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
            '                Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
            '                If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
            '                    ServiceOrder.Code = res
            '                    seq.Next += 1
            '                    Me._secuenceDRepository.SaveEntity(seq)
            '                Else
            '                    scope.Dispose()
            '                    Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
            '                End If
            '            Else
            '                scope.Dispose()
            '                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
            '            End If
            '        End If

            '        If ServiceOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '            If ServiceOrder.EntityName Is Nothing OrElse ServiceOrder.EntityName.Trim().Equals(String.Empty) Then
            '                ServiceOrder.EntityName = ServiceOrder.GetType.Name
            '            End If
            '            ServiceOrder.CreationUser = audit.CodeUser
            '            ServiceOrder.CreationDate = DateTime.Now
            '            status = Infrastructure.CrossCutting.Audit.Actions.Insert
            '        Else
            '            If ServiceOrder.Status = 3 Then
            '                'si la orden de servicio se creo para una estancia
            '                If ServiceOrder.ServiceOrderDetail.Where(Function(x) x.HospitalStayId IsNot Nothing).ToList().Count > 0 Then
            '                    For Each itemStay In ServiceOrder.ServiceOrderDetail.Where(Function(x) x.HospitalStayId IsNot Nothing)
            '                        Dim stay = _stayRepository.GetStayById(itemStay.HospitalStayId)
            '                        stay.StartTracking()
            '                        Dim stayDetail = (From sd In stay.CHREGESTADET Order By sd.GENLIQUIDA Descending).ToList()
            '                        If itemStay.HospitalStayDetailId <> stayDetail(0).ID Then
            '                            scope.Dispose()
            '                            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = "No se puede anular la orden de servicio porque la estancia tiene otras liquidaciones de estancia con fechas superiores a esta"}
            '                        End If

            '                        If stay.CHREGESTADET.Count - 1 = 0 Then
            '                            stay.GENESTLIQ = 1
            '                        Else
            '                            stay.GENESTLIQ = 2
            '                        End If
            '                        Dim stayUnitWork = _stayRepository.UnitWork
            '                        _stayDetailRepository.DeleteEntity(stayDetail(0))
            '                        _stayDetailRepository.UnitWork.Commit()

            '                        _stayRepository.SaveEntity(stay)
            '                        stayUnitWork.Commit()
            '                    Next


            '                End If
            '                ServiceOrder.AnnulmentUser = audit.CodeUser
            '                ServiceOrder.AnnulmentDate = DateTime.Now
            '                status = Infrastructure.CrossCutting.Audit.Actions.Annular
            '                auxServiceOrder = ServiceOrder.OriginalValue
            '            Else
            '                ServiceOrder.ModificationUser = audit.CodeUser
            '                ServiceOrder.ModificationDate = DateTime.Now
            '                status = Infrastructure.CrossCutting.Audit.Actions.Update
            '                auxServiceOrder = ServiceOrder.OriginalValue
            '            End If
            '            'elimino los folios cuando se esta actualizando o anulando la orden de servicio
            '            Dim resultDeleteFolio = DeleteFolio(ServiceOrder, audit, action)
            '            If resultDeleteFolio.StateResult = False Then
            '                scope.Dispose()
            '                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = resultDeleteFolio.Message}
            '            End If
            '        End If


            '        For Each d In ServiceOrder.ServiceOrderDetail.Where(Function(x) x.ChangeTracker.State = ObjectState.Added Or x.ChangeTracker.State = ObjectState.Modified)
            '            If d.IsDelete Then
            '                Continue For
            '            End If
            '            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(d.CareGroupId)
            '            'obtengo la cuenta de ingresos ya sea de la entidad o de particular

            '            Dim resultIncomeAccount = _billingService.GetIncomeMainAccount(d, careGroup.Id, careGroup)
            '            If resultIncomeAccount.StateResult = False Then
            '                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = resultIncomeAccount.Message}
            '            End If

            '            If careGroup IsNot Nothing AndAlso careGroup.Id > 0 AndAlso careGroup.CareGroupType = 1 Then
            '                'EAPB con contrato
            '                If careGroup.Contract.Status = 2 OrElse careGroup.Contract.Status = 3 Then
            '                    Dim messageContract = "Suspendido"
            '                    If careGroup.Contract.Status = 3 Then
            '                        messageContract = "Terminado"
            '                    End If
            '                    Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = String.Format("El contrato asociado al ingreso esta {1}", messageContract)}
            '                End If
            '                Dim resultContractValidation As ActionResult = _billingService.ValidationsContract(careGroup.Contract)
            '                If resultContractValidation.MessageResult IsNot Nothing AndAlso resultContractValidation.MessageResult.Count > 0 Then
            '                    messageNotificationContract.AppendLine(String.Join(vbCrLf, resultContractValidation.MessageResult.ToArray()))
            '                End If
            '            End If
            '        Next

            '        Me._serviceOrderRepository.SaveEntity(ServiceOrder)
            '        ServiceOrderUnitOfWork.Commit()
            '        'genero los folios de la oreden de servicio
            '        If ServiceOrder.Status = 1 Then
            '            Dim resultGenerateFolio = GenerateFolio(ServiceOrder, audit, idFolio)
            '            If resultGenerateFolio.StateResult = False Then
            '                scope.Dispose()
            '                If resultGenerateFolio.Message IsNot Nothing Then
            '                    Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = resultGenerateFolio.Message}
            '                Else
            '                    Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("ErrorCreateFolio", "Billing")}
            '                End If
            '            End If
            '        End If

            '        sequenseUnitOfWork.Commit()
            '        auditProcess = New IndigoAuditSimpleEntity(Of ServiceOrder)(ServiceOrder, audit, status, audit.Company, auxServiceOrder)
            '        auditProcess.Execute()
            '        scope.Complete()
            '        Return New ActionResult(Of ServiceOrder) With {.StateResult = True, .ObjectEmbbeded = ServiceOrder, .Message = messageNotificationContract.ToString()}
            '    Catch ex As OptimisticConcurrencyException
            '        ServiceOrderUnitOfWork.RollbackChanges()
            '        sequenseUnitOfWork.RollbackChanges()
            '        scope.Dispose()
            '        Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            '    Catch ex As DbUpdateException
            '        Dim servicesErrors As New StringBuilder
            '        servicesErrors.AppendLine("No se pueden eliminar los siguientes servicios porque tienen servicios agregados o estan empaquetados")
            '        For Each item In ex.Entries
            '            servicesErrors.AppendLine(DirectCast(item.Entity, ServiceOrderDetail).CodeNameIpsService)
            '        Next

            '        ServiceOrderUnitOfWork.RollbackChanges()
            '        sequenseUnitOfWork.RollbackChanges()
            '        scope.Dispose()
            '        Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = True, .Message = servicesErrors.ToString()}
            '    Catch ex As Exception
            '        ServiceOrderUnitOfWork.RollbackChanges()
            '        sequenseUnitOfWork.RollbackChanges()
            '        scope.Dispose()
            '        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            '        Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .StateResultAux = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            '    End Try
        End Using

    End Function

    ''' <summary>
    ''' metodo para generar el registro en la tabla de control de folio
    ''' </summary>
    ''' <param name="ServiceOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateFolio(ServiceOrder As ServiceOrder, audit As AuditMessage, idFolio As Integer) As ActionResult
        Try
            Dim revenueControl = _revenueControlRepository.GetRevenueControlByAdmissionNumber(ServiceOrder.AdmissionNumber)
            Dim revenueControlUnitOfWork As IUnitWork = _revenueControlRepository.UnitWork
            If revenueControl Is Nothing Then
                'creo la cabecera y los detalles
                revenueControl = New RevenueControl
                With revenueControl
                    .AdmissionNumber = ServiceOrder.AdmissionNumber
                    .PatientCode = ServiceOrder.PatientCode
                    .FolioQuantity = 1
                End With
                _revenueControlRepository.SaveEntity(revenueControl)
                revenueControlUnitOfWork.Commit()
            End If
            Dim listCareGroup = (From s In ServiceOrder.ServiceOrderDetail Select s.CareGroupId).Distinct().ToList()
            For i As Integer = 0 To listCareGroup.Count - 1 Step 1

                'obtengo el listado del detalle de la ordende servicio que se va agregar para insertar los registro en ServiceOrderDetailDistribution
                Dim listDetail = ServiceOrder.ServiceOrderDetail.Where(Function(x) x.CareGroupId = listCareGroup(i)).ToList()
                Dim careGroup As Object = _careGroupRepository.GetCareGroupPOCOById(listCareGroup(i))
                If careGroup.ThirdPartyId Is Nothing Then
                    'asigno el tercero para porder ir a buscar el folio
                    careGroup.ThirdPartyId = listDetail.ElementAt(0).ThirdPartyId
                End If

                Dim revenueControlDetail As RevenueControlDetail = Nothing
                Dim dictionaryRevenueControl As New Dictionary(Of Integer, RevenueControlDetail)
                'If idFolio > 0 Then
                '    revenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByIdNoAdded(idFolio)
                'Else
                '    revenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByCareGropIdThirdPartyId(revenueControl.Id, careGroup.CareGroupId, careGroup.ThirdPartyId)
                'End If
                'si el revenueControlDetail no es nothing se actualiza el folio 
                'If revenueControlDetail IsNot Nothing Then
                For Each itemDetail In listDetail
                    If itemDetail.ChangeTracker.State = ObjectState.Unchanged Then
                        Continue For
                    End If
                    If itemDetail.IsPackage AndAlso itemDetail.ChangeTracker.State <> ObjectState.Added Then
                        Return New ActionResult With {.StateResult = False, .Message = "No se puede modificar el folio porque el servicio " + itemDetail.CodeNameIpsService + "  esta empaquetado"}
                    End If
                    Dim serviceOrderDetailDistribution As ServiceOrderDetailDistribution
                    Dim listServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId(itemDetail.Id)
                    If listServiceOrderDetailDistribution IsNot Nothing AndAlso listServiceOrderDetailDistribution.Count > 1 Then
                        Return New ActionResult With {.StateResult = False, .Message = "No se puede modificar el folio porque tiene items distribuidos"}
                    End If
                    If listServiceOrderDetailDistribution.Count = 1 Then
                        If dictionaryRevenueControl.ContainsKey(listServiceOrderDetailDistribution.ElementAt(0).RevenueControlDetailId) Then
                            revenueControlDetail = dictionaryRevenueControl(listServiceOrderDetailDistribution.ElementAt(0).RevenueControlDetailId)
                        Else
                            revenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailById(listServiceOrderDetailDistribution.ElementAt(0).RevenueControlDetailId)
                            dictionaryRevenueControl.Add(revenueControlDetail.Id, revenueControlDetail)
                        End If
                        If revenueControlDetail.Status > 1 Then
                            If revenueControlDetail.Status = 2 Then
                                Return New ActionResult With {.StateResult = False, .Message = "No se puede modificar el folio porque esta facturado"}
                            Else
                                Return New ActionResult With {.StateResult = False, .Message = "No se puede modificar el folio porque esta bloqueado"}
                            End If
                        End If
                        serviceOrderDetailDistribution = listServiceOrderDetailDistribution.ElementAt(0)
                        serviceOrderDetailDistribution.GrandTotalSalesPrice = itemDetail.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.ThirdPartySalesPrice = itemDetail.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.SubTotalPatientSalesPrice = 0
                        serviceOrderDetailDistribution.PatientPercentage = 0
                        serviceOrderDetailDistribution.Quantity = itemDetail.InvoicedQuantity
                        If serviceOrderDetailDistribution.GrandTotalSalesPrice = 0 Then
                            serviceOrderDetailDistribution.ThirdPartyPercentage = 0
                        Else
                            serviceOrderDetailDistribution.ThirdPartyPercentage = 100
                        End If
                        serviceOrderDetailDistribution.LastCaregroupId = itemDetail.CareGroupId
                        serviceOrderDetailDistribution.MarkAsModified()
                    Else
                        Dim idRevenueControlDetail As Integer = _revenueControlDetailRepositoryFind.GetIdByCareGroupIdThirdPartyId(revenueControl.Id, careGroup.CareGroupId, careGroup.ThirdPartyId)
                        If idRevenueControlDetail = 0 Then
                            If dictionaryRevenueControl.ContainsKey(0) Then
                                revenueControlDetail = dictionaryRevenueControl(0)
                            Else
                                revenueControlDetail = GenerateFolioDetail(itemDetail, audit, revenueControl.Id, careGroup.ThirdPartyId)
                                dictionaryRevenueControl.Add(revenueControlDetail.Id, revenueControlDetail)
                            End If
                        Else 'Si el id es mayor a 0 por lo que el folio debe existir
                            If dictionaryRevenueControl.ContainsKey(idRevenueControlDetail) Then
                                revenueControlDetail = dictionaryRevenueControl(idRevenueControlDetail)
                            Else
                                revenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByCareGroupIdThirdPartyId(revenueControl.Id, careGroup.CareGroupId, careGroup.ThirdPartyId)
                                dictionaryRevenueControl.Add(revenueControlDetail.Id, revenueControlDetail)
                            End If
                        End If
                        serviceOrderDetailDistribution = New ServiceOrderDetailDistribution
                        serviceOrderDetailDistribution.ApplyRecoveryFee = 1
                        serviceOrderDetailDistribution.RecoveryFeeType = 1
                        serviceOrderDetailDistribution.ServiceOrderDetailId = itemDetail.Id
                        serviceOrderDetailDistribution.GrandTotalSalesPrice = itemDetail.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.DistributionType = 1
                        serviceOrderDetailDistribution.ThirdPartySalesPrice = itemDetail.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.ThirdPartyPercentage = 100
                        serviceOrderDetailDistribution.GrandTotalDiscount = itemDetail.ThirdPartyDiscount * itemDetail.InvoicedQuantity
                        serviceOrderDetailDistribution.LastCaregroupId = itemDetail.CareGroupId
                        serviceOrderDetailDistribution.Quantity = itemDetail.InvoicedQuantity
                    End If
                    revenueControlDetail.ServiceOrderDetailDistribution.Add(serviceOrderDetailDistribution)
                Next
                'revenueControl.RevenueControlDetail.Add(revenueControlDetail.MarkAsModified())
                '_revenueControlRepository.SaveEntity(revenueControl)
                'revenueControlUnitOfWork.Commit()
                For Each itemRevenueDetail In dictionaryRevenueControl
                    If itemRevenueDetail.Value.Id = 0 Then
                        revenueControl.FolioQuantity += 1
                    End If
                    _revenueControlDetailRepository.SaveEntity(itemRevenueDetail.Value)
                    _revenueControlDetailRepository.UnitWork.Commit()
                    _billingService.UpdateRevenueControlDetailValues(itemRevenueDetail.Value.Id, Nothing, ServiceOrder.OperatingUnitId)
                Next

                '*********************************************************************************************************
                'Else
                'aumemto el numero de folios y creo el folio y la distribucion
                'revenueControl.FolioQuantity += 1
                'Dim listFolioDetail = GenerateFolioDetail(ServiceOrder, audit, revenueControl.Id)
                'For Each item In listFolioDetail
                '    revenueControl.RevenueControlDetail.Add(item)
                'Next
                '*********************************************************************************************************
                _revenueControlRepository.SaveEntity(revenueControl)
                revenueControlUnitOfWork.Commit()
                'End If
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar el detalle del folio
    ''' </summary>
    ''' <param name="ServiceOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateFolioDetail(ServiceOrderDetail As ServiceOrderDetail, audit As AuditMessage, revenueControlId As Integer, thirdPartyId As Integer) As RevenueControlDetail
        Dim revenueControlDetail As RevenueControlDetail
        Dim _folioOrder = 0
        If revenueControlId > 0 Then
            _folioOrder = _revenueControlDetailRepository.GetMaxFolioOrder(revenueControlId)
        End If
        Dim careGroup As Object = _careGroupRepository.GetCareGroupPOCOById(ServiceOrderDetail.CareGroupId)
        revenueControlDetail = New RevenueControlDetail
        With revenueControlDetail
            'obtengo el listado del detalle de la ordende servicio que se va agregar para insertar los registro en ServiceOrderDetailDistribution
            _folioOrder += 1
            .FolioOrder = _folioOrder
            .RevenueControlId = revenueControlId
            .FolioType = careGroup.FolioType
            .LiquidationType = careGroup.LiquidationType
            .ContractEntityId = careGroup.ContractEntityId
            .ThirdPartyId = thirdPartyId
            .HealthAdministratorId = careGroup.HealthAdministratorId
            .CareGroupId = careGroup.CareGroupId
            .TotalFolio = ServiceOrderDetail.GrandTotalSalesPrice
            .Status = 1
            .CreationUser = audit.CodeUser
            .CreationDate = DateTime.Now
        End With
        Return revenueControlDetail
    End Function

    Private Function DeleteFolio(ServiceOrder As ServiceOrder, audit As AuditMessage, Optional action As EActionServiceOrder = EActionServiceOrder.NoAction) As ActionResult
        Dim errors As New StringBuilder
        Try
            Dim listDetailDelete As List(Of ServiceOrderDetail)
            If ServiceOrder.Status = 3 Then
                listDetailDelete = ServiceOrder.ServiceOrderDetail.ToList()
            Else
                listDetailDelete = ServiceOrder.ServiceOrderDetail.Where(Function(x) x.ChangeTracker.State = ObjectState.Deleted).ToList()
            End If
            If listDetailDelete.Count > 0 Then
                Dim serviceOrderDetailDistributionUnitOfWork As IUnitWork = _serviceOrderDetailDistributionRepository.UnitWork
                Dim revenueControlDetailUnitOfWork As IUnitWork = _revenueControlDetailRepository.UnitWork
                Dim revenueControlDetailId As Integer = 0
                Dim listRevenueControlDetailTmpControl As New List(Of RevenueControlDetail)
                Dim countServiceOrderDetailToAdd As Integer = ServiceOrder.ServiceOrderDetail.Where(Function(o) o.ChangeTracker.State = ObjectState.Added).Count()
                For Each item In listDetailDelete
                    If action = EActionServiceOrder.NoAction Then
                        If item.IsPackage Then
                            errors.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede modificar porque esta empaquetado")
                            Continue For
                        End If
                    End If
                    Dim serviceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId(item.Id)
                    If serviceOrderDetailDistribution IsNot Nothing AndAlso serviceOrderDetailDistribution.Count > 1 Then
                        errors.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede modificar porque esta distribuido")
                        Continue For
                    End If
                    revenueControlDetailId = serviceOrderDetailDistribution(0).RevenueControlDetailId
                    'busco si existe un item para no tener que consultar
                    Dim revenueControlDetailUpdate = listRevenueControlDetailTmpControl.Find(Function(x) x.Id = revenueControlDetailId)

                    If revenueControlDetailUpdate Is Nothing Then
                        'si no esta el revenue control detail en el listado lo consulto y lo agrego para evitar errores de accept changes
                        revenueControlDetailUpdate = _revenueControlDetailRepositoryFind.GetRevenueControlDetailByIdNoAdded(revenueControlDetailId)
                        listRevenueControlDetailTmpControl.Add(revenueControlDetailUpdate)
                    End If

                    If revenueControlDetailUpdate.Status = 1 Then
                        revenueControlDetailUpdate.TotalFolio -= serviceOrderDetailDistribution(0).GrandTotalSalesPrice
                        revenueControlDetailUpdate.TotalPatientSalesPrice -= serviceOrderDetailDistribution(0).SubTotalPatientSalesPrice
                        revenueControlDetailUpdate.TotalPatientWithDiscount -= serviceOrderDetailDistribution(0).SubTotalPatientSalesPrice '* (1 - item. / 100)
                        revenueControlDetailUpdate.ModificationUser = audit.CodeUser
                        revenueControlDetailUpdate.ModificationDate = DateTime.Now
                        revenueControlDetailUpdate.MarkAsModified()
                        _serviceOrderDetailDistributionRepository.DeleteEntity(serviceOrderDetailDistribution(0))
                        serviceOrderDetailDistributionUnitOfWork.Commit()
                        _revenueControlDetailRepository.SaveEntity(revenueControlDetailUpdate)
                        revenueControlDetailUnitOfWork.Commit()

                        If revenueControlDetailUpdate.TotalFolio = 0 AndAlso countServiceOrderDetailToAdd = 0 Then
                            'eliminamos el revenueControlDetail
                            'revenueControlDetailUpdate.MarkAsDeleted()
                            _revenueControlDetailRepository.DeleteEntity(revenueControlDetailUpdate)
                            revenueControlDetailUnitOfWork.Commit()
                            Dim revenueControl = _revenueControlRepository.GetRevenueControlById(revenueControlDetailUpdate.RevenueControlId)
                            Dim revenueControlUnitOfWork As IUnitWork = _revenueControlRepository.UnitWork
                            Dim listRevenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailByRevenueControlId(revenueControl.Id)
                            If listRevenueControlDetail.Count = 0 Then
                                revenueControl.MarkAsDeleted()
                                _revenueControlRepository.SaveEntity(revenueControl)
                                revenueControlUnitOfWork.Commit()
                            Else
                                revenueControl.FolioQuantity -= 1
                                revenueControl.MarkAsModified()
                                _revenueControlRepository.SaveEntity(revenueControl)
                                revenueControlUnitOfWork.Commit()
                            End If
                        Else
                            _billingService.UpdateRevenueControlDetailValues(revenueControlDetailUpdate.Id, Nothing, ServiceOrder.OperatingUnitId)
                        End If
                    Else
                        If revenueControlDetailUpdate.Status = 2 Then
                            errors.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede modificar porque el folio #" + (revenueControlDetailUpdate.FolioOrder).ToString() + " esta facturado")
                        Else
                            errors.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede modificar porque el folio #" + (revenueControlDetailUpdate.FolioOrder).ToString() + " esta bloqueado")
                        End If
                    End If
                Next
                If errors.Length > 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = errors.ToString()}
                End If

            End If
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function
#End Region

    ''' <summary>
    ''' Obtiene un detalle de orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailById(Id As Integer) As ServiceOrderDetail Implements IServiceOrderAdminService.GetServiceOrderDetailById
        Try
            Return _serviceOrderRepository.GetServiceOrderDetailById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetAdmissionByServiceOrder(admissionNumber As String) As String Implements IServiceOrderAdminService.GetAdmissionByServiceOrder
        Try
            Dim admission As Object = _admissionRepository.GetAdmissionByServiceOrder(admissionNumber)
            If admission IsNot Nothing Then
                If CType(admission, IDictionary(Of String, Object))("HealthAdministratorId") IsNot Nothing Then
                    Dim healthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(admission.HealthAdministratorId)
                    If healthAdministrator IsNot Nothing AndAlso healthAdministrator.Id > 0 Then
                        admission.EntityNit = healthAdministrator.ThirdPartyDescription.Split("-").ElementAt(0).Trim()
                        admission.EntityCode = healthAdministrator.Code
                        admission.EntityName = healthAdministrator.Name
                    End If
                End If
                Return Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(admission)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Método para listar medicamentos procesados por la central de mezclas para generación de la orden de servicios.
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns></returns>
    Public Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result) Implements IServiceOrderAdminService.GetProcessedMedicationItemsForBillingList
        Return _serviceOrderRepository.GetProcessedMedicationItemsForBillingList(admissionNumber, isChild)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _billingService.Dispose()
            End If
            _stayRepository = Nothing
            _billingService = Nothing
            _serviceOrderRepository = Nothing
            _secuenceDRepository = Nothing
            _revenueControlRepository = Nothing
            _careGroupRepository = Nothing
            _revenueControlDetailRepository = Nothing
            _admissionRepository = Nothing
            _serviceOrderDetailDistributionRepository = Nothing
            _stayDetailRepository = Nothing
            _revenueControlDetailRepositoryFind = Nothing
            _healthAdministratorRepository = Nothing
            _cupsEntityRepository = Nothing
            _productGroupRepository = Nothing
            _functionalUnitRepository = Nothing
            _billingConceptRepository = Nothing
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

Public Enum EActionServiceOrder
    'empaquetar
    Unpack = 1
    'desempaquetar
    Package = 2
    'ninguna
    NoAction = 3
End Enum