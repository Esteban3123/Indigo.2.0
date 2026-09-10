'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Text
Imports Application.Authorization

Public Class AuthorizationOutsourcedServicesAdminService
    Implements IAuthorizationOutsourcedServicesAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationOutsourcedServicesRepository As IAuthorizationOutsourcedServicesRepository

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationOutsourcedServicesRepository As IAuthorizationOutsourcedServicesRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationOutsourcedServicesRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationOutsourcedServicesRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationOutsourcedServicesRepository = authorizationOutsourcedServicesRepository
    End Sub

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="AuthorizationOutsourcedServices"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveAuthorizationOutsourcedServices(AuthorizationOutsourcedServices As AuthorizationOutsourcedServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationOutsourcedServicesAdminService.SaveAuthorizationOutsourcedServices
        If AuthorizationOutsourcedServices Is Nothing Then
            Throw New ArgumentNullException("AuthorizationOutsourcedServices")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(AuthorizationOutsourcedServices, audit)

                Dim result = _authorizationOutsourcedServicesRepository.SP_SaveAuthorizationOutsourcedServices(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = False, .Message = result.Message}
                End If

                AuthorizationOutsourcedServices.Id = result.AuthorizationOutsourcedServicesId
                AuthorizationOutsourcedServices.Code = result.AuthorizationOutsourcedServicesCode

                scope.Complete()
                Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = True, .ObjectEmbbeded = AuthorizationOutsourcedServices, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad en xml
    ''' </summary>
    ''' <param name="authorizationOutsourcedServices"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function ConvertEntityToXml(authorizationOutsourcedServices As AuthorizationOutsourcedServices, audit As AuditMessage) As Object
        Dim builder As New StringBuilder
        Dim rowIdSOD As Integer = 1
        Dim rowIdPDD As Integer = 1

        builder.Append("<AuthorizationOutsourcedServices>")

        With authorizationOutsourcedServices
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<DocumentDate>" & .DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
            builder.Append("<Type>" & .Type & "</Type>")
            builder.Append("<AdmissionNumber>" & .AdmissionNumber & "</AdmissionNumber>")
            builder.Append("<ThirdPartyId>" & .ThirdPartyId & "</ThirdPartyId>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<Description>" & .Description & "</Description>")

            If .ListServiceOrderDetail IsNot Nothing AndAlso .ListServiceOrderDetail.Count > 0 Then
                For Each itemServiceOrderDetail In .ListServiceOrderDetail
                    builder.Append("<AuthorizationOutsourcedServicesServiceOrderDetail>")
                    builder.Append("<RowId>" & rowIdSOD & "</RowId>")
                    builder.Append("<Id>" & itemServiceOrderDetail.Id & "</Id>")
                    builder.Append("<AuthorizationOutsourcedServicesId>" & itemServiceOrderDetail.AuthorizationOutsourcedServicesId & "</AuthorizationOutsourcedServicesId>")
                    builder.Append("<CareGroupId>" & itemServiceOrderDetail.CareGroupId & "</CareGroupId>")
                    builder.Append("<HealthAdministratorId>" & itemServiceOrderDetail.HealthAdministratorId & "</HealthAdministratorId>")
                    builder.Append("<ThirdPartyId>" & itemServiceOrderDetail.ThirdPartyId & "</ThirdPartyId>")
                    builder.Append("<ServiceType>" & itemServiceOrderDetail.ServiceType & "</ServiceType>")
                    builder.Append("<RecordType>" & itemServiceOrderDetail.RecordType & "</RecordType>")
                    builder.Append("<CUPSEntityId>" & itemServiceOrderDetail.CUPSEntityId & "</CUPSEntityId>")
                    builder.Append("<IPSServiceId>" & itemServiceOrderDetail.IPSServiceId & "</IPSServiceId>")
                    builder.Append("<HospitalStayId>" & itemServiceOrderDetail.HospitalStayId & "</HospitalStayId>")
                    builder.Append("<HospitalStayDetailId>" & itemServiceOrderDetail.HospitalStayDetailId & "</HospitalStayDetailId>")
                    builder.Append("<ControlExternalConsultation>" & itemServiceOrderDetail.ControlExternalConsultation & "</ControlExternalConsultation>")
                    builder.Append("<ControlExternalConsultationCode>" & itemServiceOrderDetail.ControlExternalConsultationCode & "</ControlExternalConsultationCode>")
                    builder.Append("<CUPSAssociateService>" & itemServiceOrderDetail.CUPSAssociateService & "</CUPSAssociateService>")
                    builder.Append("<CodeAssociateService>" & itemServiceOrderDetail.CodeAssociateService & "</CodeAssociateService>")
                    builder.Append("<IsPackage>" & itemServiceOrderDetail.IsPackage & "</IsPackage>")
                    builder.Append("<Packaging>" & itemServiceOrderDetail.Packaging & "</Packaging>")
                    builder.Append("<PackageServiceOrderDetailId>" & itemServiceOrderDetail.PackageServiceOrderDetailId & "</PackageServiceOrderDetailId>")
                    builder.Append("<LiquidationType>" & itemServiceOrderDetail.LiquidationType & "</LiquidationType>")
                    builder.Append("<Presentation>" & itemServiceOrderDetail.Presentation & "</Presentation>")
                    builder.Append("<ProductId>" & itemServiceOrderDetail.ProductId & "</ProductId>")
                    builder.Append("<InvoicedQuantity>" & itemServiceOrderDetail.InvoicedQuantity & "</InvoicedQuantity>")
                    builder.Append("<SupplyQuantity>" & itemServiceOrderDetail.SupplyQuantity & "</SupplyQuantity>")
                    builder.Append("<DevolutionQuantity>" & itemServiceOrderDetail.DevolutionQuantity & "</DevolutionQuantity>")
                    builder.Append("<RateManualSalePrice>" & itemServiceOrderDetail.RateManualSalePrice & "</RateManualSalePrice>")
                    builder.Append("<CostValue>" & itemServiceOrderDetail.CostValue & "</CostValue>")
                    builder.Append("<ServiceDate>" & itemServiceOrderDetail.ServiceDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ServiceDate>")
                    builder.Append("<AuthorizationNumber>" & itemServiceOrderDetail.AuthorizationNumber & "</AuthorizationNumber>")
                    builder.Append("<PerformsFunctionalUnitId>" & itemServiceOrderDetail.PerformsFunctionalUnitId & "</PerformsFunctionalUnitId>")
                    builder.Append("<PerformsHealthProfessionalCode>" & itemServiceOrderDetail.PerformsHealthProfessionalCode & "</PerformsHealthProfessionalCode>")
                    builder.Append("<PerformsProfessionalSpecialty>" & itemServiceOrderDetail.PerformsProfessionalSpecialty & "</PerformsProfessionalSpecialty>")
                    builder.Append("<PerformsHealthProfessionalThirdPartyId>" & itemServiceOrderDetail.PerformsHealthProfessionalThirdPartyId & "</PerformsHealthProfessionalThirdPartyId>")
                    builder.Append("<BillingConceptId>" & itemServiceOrderDetail.BillingConceptId & "</BillingConceptId>")
                    builder.Append("<CostCenterId>" & itemServiceOrderDetail.CostCenterId & "</CostCenterId>")
                    builder.Append("<SettlementType>" & itemServiceOrderDetail.SettlementType & "</SettlementType>")
                    builder.Append("<IncludeServiceOrderDetailId>" & itemServiceOrderDetail.IncludeServiceOrderDetailId & "</IncludeServiceOrderDetailId>")
                    builder.Append("<RecoveryRatio>" & itemServiceOrderDetail.RecoveryRatio & "</RecoveryRatio>")
                    builder.Append("<RateManualId>" & itemServiceOrderDetail.RateManualId & "</RateManualId>")
                    builder.Append("<RateManualType>" & itemServiceOrderDetail.RateManualType & "</RateManualType>")
                    builder.Append("<RateManualDetailId>" & itemServiceOrderDetail.RateManualDetailId & "</RateManualDetailId>")
                    builder.Append("<DefinitionRateDetailId>" & itemServiceOrderDetail.DefinitionRateDetailId & "</DefinitionRateDetailId>")
                    builder.Append("<DefinitionRateDetailConditionId>" & itemServiceOrderDetail.DefinitionRateDetailConditionId & "</DefinitionRateDetailConditionId>")
                    builder.Append("<SubTotalSalesPrice>" & itemServiceOrderDetail.SubTotalSalesPrice & "</SubTotalSalesPrice>")
                    builder.Append("<ThirdPartyDiscount>" & itemServiceOrderDetail.ThirdPartyDiscount & "</ThirdPartyDiscount>")
                    builder.Append("<ThirdPartyDiscountPercentage>" & itemServiceOrderDetail.ThirdPartyDiscountPercentage & "</ThirdPartyDiscountPercentage>")
                    builder.Append("<TotalSalesPrice>" & itemServiceOrderDetail.TotalSalesPrice & "</TotalSalesPrice>")
                    builder.Append("<GrandTotalSalesPrice>" & itemServiceOrderDetail.GrandTotalSalesPrice & "</GrandTotalSalesPrice>")
                    builder.Append("<SurchargeApply>" & itemServiceOrderDetail.SurchargeApply & "</SurchargeApply>")
                    builder.Append("<SurgicalInterventionType>" & itemServiceOrderDetail.SurgicalInterventionType & "</SurgicalInterventionType>")
                    builder.Append("<SurgeryNumber>" & itemServiceOrderDetail.SurgeryNumber & "</SurgeryNumber>")
                    builder.Append("<IsFirstEvent>" & itemServiceOrderDetail.IsFirstEvent & "</IsFirstEvent>")
                    builder.Append("<IsAnnulled>" & itemServiceOrderDetail.IsAnnulled & "</IsAnnulled>")
                    builder.Append("<IsDelete>" & itemServiceOrderDetail.IsDelete & "</IsDelete>")
                    builder.Append("<IncomeMainAccountId>" & itemServiceOrderDetail.IncomeMainAccountId & "</IncomeMainAccountId>")
                    builder.Append("<ApplyRIAS>" & itemServiceOrderDetail.ApplyRIAS & "</ApplyRIAS>")
                    builder.Append("<RIASCupsId>" & itemServiceOrderDetail.RIASCupsId & "</RIASCupsId>")
                    builder.Append("<CUPSEntityContractDescriptionId>" & itemServiceOrderDetail.CUPSEntityContractDescriptionId & "</CUPSEntityContractDescriptionId>")
                    If itemServiceOrderDetail.ChangeTracker.State = ObjectState.Deleted Then
                        builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                    Else
                        builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                    End If

                    If itemServiceOrderDetail.ServiceOrderDetailSurgical IsNot Nothing AndAlso itemServiceOrderDetail.ServiceOrderDetailSurgical.Count > 0 Then
                        For Each itemServiceOrderDetailSurgical In itemServiceOrderDetail.ServiceOrderDetailSurgical
                            builder.Append("<AuthorizationOutsourcedServicesServiceOrderDetailSurgical>")
                            builder.Append("<RowId>" & rowIdSOD & "</RowId>")
                            builder.Append("<Id>" & itemServiceOrderDetailSurgical.Id & "</Id>")
                            builder.Append("<AuthorizationOutsourcedServicesServiceOrderDetailId>" & itemServiceOrderDetailSurgical.AuthorizationOutsourcedServicesServiceOrderDetailId & "</AuthorizationOutsourcedServicesServiceOrderDetailId>")
                            builder.Append("<IPSServiceId>" & itemServiceOrderDetailSurgical.IPSServiceId & "</IPSServiceId>")
                            builder.Append("<InvoicedQuantity>" & itemServiceOrderDetailSurgical.InvoicedQuantity & "</InvoicedQuantity>")
                            builder.Append("<LiquidationPercentage>" & itemServiceOrderDetailSurgical.LiquidationPercentage & "</LiquidationPercentage>")
                            builder.Append("<RateManualSalePrice>" & itemServiceOrderDetailSurgical.RateManualSalePrice & "</RateManualSalePrice>")
                            builder.Append("<TotalSalesPrice>" & itemServiceOrderDetailSurgical.TotalSalesPrice & "</TotalSalesPrice>")
                            builder.Append("<PerformsHealthProfessionalCode>" & itemServiceOrderDetailSurgical.PerformsHealthProfessionalCode & "</PerformsHealthProfessionalCode>")
                            builder.Append("<PerformsHealthProfessionalThirdPartyId>" & itemServiceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId & "</PerformsHealthProfessionalThirdPartyId>")
                            builder.Append("<CostValue>" & itemServiceOrderDetailSurgical.CostValue & "</CostValue>")
                            builder.Append("<BillingConceptId>" & itemServiceOrderDetailSurgical.BillingConceptId & "</BillingConceptId>")
                            builder.Append("<CostCenterId>" & itemServiceOrderDetailSurgical.CostCenterId & "</CostCenterId>")
                            builder.Append("<RateManualDetailSurgicalId>" & itemServiceOrderDetailSurgical.RateManualDetailSurgicalId & "</RateManualDetailSurgicalId>")
                            builder.Append("<SurchargeApply>" & itemServiceOrderDetailSurgical.SurchargeApply & "</SurchargeApply>")
                            builder.Append("<OnlyMedicalFees>" & itemServiceOrderDetailSurgical.OnlyMedicalFees & "</OnlyMedicalFees>")
                            builder.Append("<IncomeMainAccountId>" & itemServiceOrderDetailSurgical.IncomeMainAccountId & "</IncomeMainAccountId>")
                            If itemServiceOrderDetail.ChangeTracker.State = ObjectState.Deleted OrElse itemServiceOrderDetailSurgical.ChangeTracker.State = ObjectState.Deleted Then
                                builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                            Else
                                builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                            End If
                            builder.Append("</AuthorizationOutsourcedServicesServiceOrderDetailSurgical>")
                        Next
                    End If

                    builder.Append("</AuthorizationOutsourcedServicesServiceOrderDetail>")

                    rowIdSOD += 1
                Next
            End If

            If .ListPharmaceuticalDispensingDetail IsNot Nothing AndAlso .ListPharmaceuticalDispensingDetail.Count > 0 Then
                For Each itemPharmaceuticalDispensingDetail In .ListPharmaceuticalDispensingDetail
                    builder.Append("<AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail>")
                    builder.Append("<RowId>" & rowIdPDD & "</RowId>")
                    builder.Append("<Id>" & itemPharmaceuticalDispensingDetail.Id & "</Id>")
                    builder.Append("<AuthorizationOutsourcedServicesId>" & itemPharmaceuticalDispensingDetail.AuthorizationOutsourcedServicesId & "</AuthorizationOutsourcedServicesId>")
                    builder.Append("<CareGroupId>" & itemPharmaceuticalDispensingDetail.CareGroupId & "</CareGroupId>")
                    builder.Append("<HealthAdministratorId>" & itemPharmaceuticalDispensingDetail.HealthAdministratorId & "</HealthAdministratorId>")
                    builder.Append("<ThirdPartyId>" & itemPharmaceuticalDispensingDetail.ThirdPartyId & "</ThirdPartyId>")
                    builder.Append("<ProductId>" & itemPharmaceuticalDispensingDetail.ProductId & "</ProductId>")
                    builder.Append("<WarehouseId>" & itemPharmaceuticalDispensingDetail.WarehouseId & "</WarehouseId>")
                    builder.Append("<Quantity>" & itemPharmaceuticalDispensingDetail.Quantity & "</Quantity>")
                    builder.Append("<ReturnedQuantity>" & itemPharmaceuticalDispensingDetail.ReturnedQuantity & "</ReturnedQuantity>")
                    builder.Append("<ServiceDate>" & itemPharmaceuticalDispensingDetail.ServiceDate.ToString("dd/MM/yyyy HH:mm:ss") & "</ServiceDate>")
                    builder.Append("<FunctionalUnitId>" & itemPharmaceuticalDispensingDetail.FunctionalUnitId & "</FunctionalUnitId>")
                    builder.Append("<OrderedHealthProfessionalCode>" & itemPharmaceuticalDispensingDetail.OrderedHealthProfessionalCode & "</OrderedHealthProfessionalCode>")
                    builder.Append("<OrderedProfessionalSpecialty>" & itemPharmaceuticalDispensingDetail.OrderedProfessionalSpecialty & "</OrderedProfessionalSpecialty>")
                    builder.Append("<OrderedHealthProfessionalThirdPartyId>" & itemPharmaceuticalDispensingDetail.OrderedHealthProfessionalThirdPartyId & "</OrderedHealthProfessionalThirdPartyId>")
                    builder.Append("<AuthorizationNumber>" & itemPharmaceuticalDispensingDetail.AuthorizationNumber & "</AuthorizationNumber>")
                    builder.Append("<LiquidationType>" & itemPharmaceuticalDispensingDetail.LiquidationType & "</LiquidationType>")
                    builder.Append("<CupsEntityId>" & itemPharmaceuticalDispensingDetail.CupsEntityId & "</CupsEntityId>")
                    builder.Append("<SurchargeApply>" & itemPharmaceuticalDispensingDetail.SurchargeApply & "</SurchargeApply>")
                    builder.Append("<SalePrice>" & itemPharmaceuticalDispensingDetail.SalePrice & "</SalePrice>")
                    builder.Append("<AverageCost>" & itemPharmaceuticalDispensingDetail.AverageCost & "</AverageCost>")
                    builder.Append("<DiscountPercentage>" & itemPharmaceuticalDispensingDetail.DiscountPercentage & "</DiscountPercentage>")
                    builder.Append("<DiscountValue>" & itemPharmaceuticalDispensingDetail.DiscountValue & "</DiscountValue>")
                    builder.Append("<TotalSalesPrice>" & itemPharmaceuticalDispensingDetail.TotalSalesPrice & "</TotalSalesPrice>")
                    builder.Append("<GrandTotalSalesPrice>" & itemPharmaceuticalDispensingDetail.GrandTotalSalesPrice & "</GrandTotalSalesPrice>")
                    If itemPharmaceuticalDispensingDetail.ChangeTracker.State = ObjectState.Deleted Then
                        builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                    Else
                        builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                    End If

                    builder.Append("</AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail>")

                    rowIdPDD += 1
                Next
            End If

            'Si hay ids de tramites
            If .ListTraceabilityPaperworkIds IsNot Nothing AndAlso .ListTraceabilityPaperworkIds.Count > 0 Then
                For Each item In .ListTraceabilityPaperworkIds
                    builder.Append("<TraceabilityPaperwork>")
                    builder.Append("<TraceabilityPaperworkId>" & item & "</TraceabilityPaperworkId>")
                    builder.Append("</TraceabilityPaperwork>")
                Next
            End If
        End With

        builder.Append("</AuthorizationOutsourcedServices>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Obtiene por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationOutsourcedServices(code As String) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationOutsourcedServicesAdminService.GetAuthorizationOutsourcedServices
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim AuthorizationOutsourcedServices As AuthorizationOutsourcedServices = Me._authorizationOutsourcedServicesRepository.GetAuthorizationOutsourcedServices(code.Trim())
            Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = True, .ObjectEmbbeded = AuthorizationOutsourcedServices}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationOutsourcedServicesById(id As Integer) As ActionResult(Of AuthorizationOutsourcedServices) Implements IAuthorizationOutsourcedServicesAdminService.GetAuthorizationOutsourcedServicesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim AuthorizationOutsourcedServices As AuthorizationOutsourcedServices = Me._authorizationOutsourcedServicesRepository.GetAuthorizationOutsourcedServicesById(id)
            Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = True, .ObjectEmbbeded = AuthorizationOutsourcedServices}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationOutsourcedServices) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _authorizationOutsourcedServicesRepository = Nothing
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
