'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/01/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Billing
Imports System.Text

Public Class QuotationAdminService
    Implements IQuotationAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _quotationRepository As IQuotationRepository

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, quotationRepository As IQuotationRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If quotationRepository Is Nothing Then
            Throw New ArgumentNullException("quotationRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _quotationRepository = quotationRepository
    End Sub

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="Quotation"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveQuotation(Quotation As Quotation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Quotation) Implements IQuotationAdminService.SaveQuotation
        If Quotation Is Nothing Then
            Throw New ArgumentNullException("Quotation")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(Quotation, audit)

                Dim result = _quotationRepository.SP_SaveQuotation(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of Quotation) With {.StateResult = False, .Message = result.Message}
                End If

                Quotation.Id = result.QuotationId
                Quotation.Code = result.QuotationCode

                scope.Complete()
                Return New ActionResult(Of Quotation) With {.StateResult = True, .ObjectEmbbeded = Quotation, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Quotation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convierte la entidad en xml
    ''' </summary>
    ''' <param name="quotation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function ConvertEntityToXml(quotation As Quotation, audit As AuditMessage) As Object
        Dim builder As New StringBuilder
        Dim rowIdSOD As Integer = 1
        Dim rowIdPDD As Integer = 1

        builder.Append("<Quotation>")

        With quotation
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<DocumentDate>" & .DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
            builder.Append("<QuotationType>" & .QuotationType & "</QuotationType>")
            builder.Append("<AdmissionNumber>" & .AdmissionNumber & "</AdmissionNumber>")
            builder.Append("<ThirdPartyId>" & .ThirdPartyId & "</ThirdPartyId>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<Description>" & .Description & "</Description>")

            If .ListServiceOrderDetail IsNot Nothing AndAlso .ListServiceOrderDetail.Count > 0 Then
                For Each itemServiceOrderDetail In .ListServiceOrderDetail
                    builder.Append("<QuotationServiceOrderDetail>")
                    builder.Append("<RowId>" & rowIdSOD & "</RowId>")
                    builder.Append("<Id>" & itemServiceOrderDetail.Id & "</Id>")
                    builder.Append("<QuotationId>" & itemServiceOrderDetail.QuotationId & "</QuotationId>")
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

                    If itemServiceOrderDetail.ServiceOrderDetailDistribution IsNot Nothing AndAlso itemServiceOrderDetail.ServiceOrderDetailDistribution.Count > 0 Then
                        For Each itemServiceOrderDetailDistribution In itemServiceOrderDetail.ServiceOrderDetailDistribution
                            builder.Append("<QuotationServiceOrderDetailDistribution>")
                            builder.Append("<RowId>" & rowIdSOD & "</RowId>")
                            builder.Append("<Id>" & itemServiceOrderDetailDistribution.Id & "</Id>")
                            builder.Append("<QuotationServiceOrderDetailId>" & itemServiceOrderDetailDistribution.QuotationServiceOrderDetailId & "</QuotationServiceOrderDetailId>")
                            builder.Append("<Quantity>" & itemServiceOrderDetailDistribution.Quantity & "</Quantity>")
                            builder.Append("<GrandTotalSalesPrice>" & itemServiceOrderDetailDistribution.GrandTotalSalesPrice & "</GrandTotalSalesPrice>")
                            builder.Append("<GrandTotalDiscount>" & itemServiceOrderDetailDistribution.GrandTotalDiscount & "</GrandTotalDiscount>")
                            builder.Append("<DistributionType>" & itemServiceOrderDetailDistribution.DistributionType & "</DistributionType>")
                            builder.Append("<ThirdPartySalesPrice>" & itemServiceOrderDetailDistribution.ThirdPartySalesPrice & "</ThirdPartySalesPrice>")
                            builder.Append("<ThirdPartyPercentage>" & itemServiceOrderDetailDistribution.ThirdPartyPercentage & "</ThirdPartyPercentage>")
                            builder.Append("<ApplyRecoveryFee>" & itemServiceOrderDetailDistribution.ApplyRecoveryFee & "</ApplyRecoveryFee>")
                            builder.Append("<RecoveryFeeType>" & itemServiceOrderDetailDistribution.RecoveryFeeType & "</RecoveryFeeType>")
                            builder.Append("<SubTotalPatientSalesPrice>" & itemServiceOrderDetailDistribution.SubTotalPatientSalesPrice & "</SubTotalPatientSalesPrice>")
                            builder.Append("<PatientPercentage>" & itemServiceOrderDetailDistribution.PatientPercentage & "</PatientPercentage>")
                            builder.Append("<LastCaregroupId>" & itemServiceOrderDetailDistribution.LastCaregroupId & "</LastCaregroupId>")
                            If itemServiceOrderDetail.ChangeTracker.State = ObjectState.Deleted OrElse itemServiceOrderDetailDistribution.ChangeTracker.State = ObjectState.Deleted Then
                                builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                            Else
                                builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                            End If
                            builder.Append("</QuotationServiceOrderDetailDistribution>")
                        Next
                    End If

                    If itemServiceOrderDetail.ServiceOrderDetailSurgical IsNot Nothing AndAlso itemServiceOrderDetail.ServiceOrderDetailSurgical.Count > 0 Then
                        For Each itemServiceOrderDetailSurgical In itemServiceOrderDetail.ServiceOrderDetailSurgical
                            builder.Append("<QuotationServiceOrderDetailSurgical>")
                            builder.Append("<RowId>" & rowIdSOD & "</RowId>")
                            builder.Append("<Id>" & itemServiceOrderDetailSurgical.Id & "</Id>")
                            builder.Append("<QuotationServiceOrderDetailId>" & itemServiceOrderDetailSurgical.QuotationServiceOrderDetailId & "</QuotationServiceOrderDetailId>")
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
                            builder.Append("</QuotationServiceOrderDetailSurgical>")
                        Next
                    End If

                    builder.Append("</QuotationServiceOrderDetail>")

                    rowIdSOD += 1
                Next
            End If

            If .ListPharmaceuticalDispensingDetail IsNot Nothing AndAlso .ListPharmaceuticalDispensingDetail.Count > 0 Then
                For Each itemPharmaceuticalDispensingDetail In .ListPharmaceuticalDispensingDetail
                    builder.Append("<QuotationPharmaceuticalDispensingDetail>")
                    builder.Append("<RowId>" & rowIdPDD & "</RowId>")
                    builder.Append("<Id>" & itemPharmaceuticalDispensingDetail.Id & "</Id>")
                    builder.Append("<QuotationId>" & itemPharmaceuticalDispensingDetail.QuotationId & "</QuotationId>")
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

                    If itemPharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial IsNot Nothing AndAlso itemPharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial.Count > 0 Then
                        For Each itemPharmaceuticalDispensingDetailBatchSerial In itemPharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailBatchSerial
                            builder.Append("<QuotationPharmaceuticalDispensingDetailBatchSerial>")
                            builder.Append("<RowId>" & rowIdPDD & "</RowId>")
                            builder.Append("<Id>" & itemPharmaceuticalDispensingDetailBatchSerial.Id & "</Id>")
                            builder.Append("<QuotationPharmaceuticalDispensingDetailId>" & itemPharmaceuticalDispensingDetailBatchSerial.QuotationPharmaceuticalDispensingDetailId & "</QuotationPharmaceuticalDispensingDetailId>")
                            builder.Append("<PhysicalInventoryId>" & itemPharmaceuticalDispensingDetailBatchSerial.PhysicalInventoryId & "</PhysicalInventoryId>")
                            builder.Append("<Quantity>" & itemPharmaceuticalDispensingDetailBatchSerial.Quantity & "</Quantity>")
                            builder.Append("<OutstandingQuantity>" & itemPharmaceuticalDispensingDetailBatchSerial.OutstandingQuantity & "</OutstandingQuantity>")
                            builder.Append("<PhysicalInventoryCustodyId>" & itemPharmaceuticalDispensingDetailBatchSerial.PhysicalInventoryCustodyId & "</PhysicalInventoryCustodyId>")
                            If itemPharmaceuticalDispensingDetail.ChangeTracker.State = ObjectState.Deleted OrElse itemPharmaceuticalDispensingDetailBatchSerial.ChangeTracker.State = ObjectState.Deleted Then
                                builder.Append("<ItemDelete>" & 1 & "</ItemDelete>")
                            Else
                                builder.Append("<ItemDelete>" & 0 & "</ItemDelete>")
                            End If
                            builder.Append("</QuotationPharmaceuticalDispensingDetailBatchSerial>")
                        Next
                    End If

                    builder.Append("</QuotationPharmaceuticalDispensingDetail>")

                    rowIdPDD += 1
                Next
            End If
        End With

        builder.Append("</Quotation>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Obtiene por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetQuotation(code As String) As ActionResult(Of Quotation) Implements IQuotationAdminService.GetQuotation
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim Quotation As Quotation = Me._quotationRepository.GetQuotation(code.Trim())
            Return New ActionResult(Of Quotation) With {.StateResult = True, .ObjectEmbbeded = Quotation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Quotation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetQuotationById(id As Integer) As ActionResult(Of Quotation) Implements IQuotationAdminService.GetQuotationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim Quotation As Quotation = Me._quotationRepository.GetQuotationById(id)
            Return New ActionResult(Of Quotation) With {.StateResult = True, .ObjectEmbbeded = Quotation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Quotation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _quotationRepository = Nothing
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
