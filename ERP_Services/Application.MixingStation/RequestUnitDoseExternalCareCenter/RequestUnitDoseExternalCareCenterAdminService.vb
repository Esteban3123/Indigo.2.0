'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
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
Imports Application.MixingStation
Imports System.Text

Public Class RequestUnitDoseExternalCareCenterAdminService
    Implements IRequestUnitDoseExternalCareCenterAdminService, Inject

    Private _RequestUnitDoseExternalCareCenterRepository As IRequestUnitDoseExternalCareCenterRepository

    Public Sub New(RequestUnitDoseExternalCareCenterRepository As IRequestUnitDoseExternalCareCenterRepository)
        If RequestUnitDoseExternalCareCenterRepository Is Nothing Then
            Throw New ArgumentNullException("RequestUnitDoseExternalCareCenterRepository vacío")
        End If
        _RequestUnitDoseExternalCareCenterRepository = RequestUnitDoseExternalCareCenterRepository
    End Sub

    Public Function SaveRequestUnitDoseExternalCareCenter(RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IRequestUnitDoseExternalCareCenterAdminService.SaveRequestUnitDoseExternalCareCenter
        If RequestUnitDoseExternalCareCenter Is Nothing Then
            Throw New ArgumentNullException("RequestUnitDoseExternalCareCenter")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(RequestUnitDoseExternalCareCenter, operatingUnitId)

                Dim result = _RequestUnitDoseExternalCareCenterRepository.SP_SaveRequestUnitDoseExternalCareCenter(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = False, .Message = result.Message}
                End If

                RequestUnitDoseExternalCareCenter.Id = result.Id
                RequestUnitDoseExternalCareCenter.Code = result.Code

                scope.Complete()
                Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseExternalCareCenter, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter, operatingUnitId As Integer) As Object
        Dim builder As New StringBuilder
        Dim RequestUnitDoseExternalCareCenterPatientRowId As Integer = 1
        Dim ExternalPatientPreparationRowId As Integer = 1

        builder.Append("<RequestUnitDoseExternalCareCenter>")

        With RequestUnitDoseExternalCareCenter
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<CMConfigurationId>" & .CMConfigurationId & "</CMConfigurationId>")
            builder.Append("<ExternalCareCenterId>" & .ExternalCareCenterId & "</ExternalCareCenterId>")
            builder.Append("<RequestType>" & .RequestType & "</RequestType>")
            builder.Append("<DocumentDate>" & .DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<OperatingUnitId>" & operatingUnitId & "</OperatingUnitId>")
            builder.Append("<ContractExternalClientsId>" & .ContractExternalClientsId & "</ContractExternalClientsId>")

            If .RequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso .RequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                For Each item In .RequestUnitDoseExternalCareCenterMaquila
                    builder.Append("<RequestUnitDoseExternalCareCenterMaquila>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<RequestUnitDoseExternalCareCenterId>" & item.RequestUnitDoseExternalCareCenterId & "</RequestUnitDoseExternalCareCenterId>")
                    builder.Append("<Type>" & item.Type & "</Type>")
                    builder.Append("<ATCId>" & item.ATCId & "</ATCId>")
                    builder.Append("<PackageId>" & item.PackageId & "</PackageId>")
                    builder.Append("<UnitDoseTypeId>" & item.UnitDoseTypeId & "</UnitDoseTypeId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</RequestUnitDoseExternalCareCenterMaquila>")
                Next
            End If

            If .RequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso .RequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                For Each item In .RequestUnitDoseExternalCareCenterPatient
                    builder.Append("<RequestUnitDoseExternalCareCenterPatient>")
                    builder.Append("<RequestUnitDoseExternalCareCenterPatientRowId>" & RequestUnitDoseExternalCareCenterPatientRowId & "</RequestUnitDoseExternalCareCenterPatientRowId>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<RequestUnitDoseExternalCareCenterId>" & item.RequestUnitDoseExternalCareCenterId & "</RequestUnitDoseExternalCareCenterId>")
                    builder.Append("<PatientExternalCareCenterId>" & item.PatientExternalCareCenterId & "</PatientExternalCareCenterId>")
                    builder.Append("<UnitDoseTypeId>" & item.UnitDoseTypeId & "</UnitDoseTypeId>")
                    builder.Append("<ExternalFunctionalUnitCode>" & item.ExternalFunctionalUnitCode & "</ExternalFunctionalUnitCode>")
                    builder.Append("<Bed>" & item.Bed & "</Bed>")
                    If item.NptId IsNot Nothing Then
                        builder.Append("<NptId>" & item.NptId & "</NptId>")
                    End If
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                    If item.PatientExternalCareCenter IsNot Nothing Then
                        builder.Append("<PatientExternalCareCenter>")
                        builder.Append("<RequestUnitDoseExternalCareCenterPatientRowId>" & RequestUnitDoseExternalCareCenterPatientRowId & "</RequestUnitDoseExternalCareCenterPatientRowId>")
                        builder.Append("<Id>" & item.PatientExternalCareCenter.Id & "</Id>")
                        builder.Append("<IdentificationNumber>" & item.PatientExternalCareCenter.IdentificationNumber & "</IdentificationNumber>")
                        builder.Append("<IdentificationTypeId>" & item.PatientExternalCareCenter.IdentificationTypeId & "</IdentificationTypeId>")
                        builder.Append("<Name>" & item.PatientExternalCareCenter.Name & "</Name>")
                        builder.Append("<LastName>" & item.PatientExternalCareCenter.LastName & "</LastName>")
                        builder.Append("<GenderTypeId>" & item.PatientExternalCareCenter.GenderTypeId & "</GenderTypeId>")
                        builder.Append("<Status>" & item.PatientExternalCareCenter.Status & "</Status>")
                        builder.Append("<PatientMobileNumber>" & item.PatientExternalCareCenter.PatientMobileNumber & "</PatientMobileNumber>")
                        builder.Append("<PatientEmail>" & item.PatientExternalCareCenter.PatientEmail & "</PatientEmail>")
                        builder.Append("<ExternalFunctionalUnit>" & item.PatientExternalCareCenter.ExternalFunctionalUnit & "</ExternalFunctionalUnit>")
                        builder.Append("<PatientBed>" & item.PatientExternalCareCenter.PatientBed & "</PatientBed>")
                        builder.Append("</PatientExternalCareCenter>")
                    End If

                    If item.ExternalPatientPreparation IsNot Nothing AndAlso item.ExternalPatientPreparation.Count > 0 Then
                        For Each itemDetail In item.ExternalPatientPreparation
                            builder.Append("<ExternalPatientPreparation>")
                            builder.Append("<RequestUnitDoseExternalCareCenterPatientRowId>" & RequestUnitDoseExternalCareCenterPatientRowId & "</RequestUnitDoseExternalCareCenterPatientRowId>")
                            builder.Append("<ExternalPatientPreparationRowId>" & ExternalPatientPreparationRowId & "</ExternalPatientPreparationRowId>")
                            builder.Append("<Id>" & itemDetail.Id & "</Id>")
                            builder.Append("<RequestUnitDoseExternalCareCenterPatientId>" & itemDetail.RequestUnitDoseExternalCareCenterPatientId & "</RequestUnitDoseExternalCareCenterPatientId>")
                            builder.Append("<PreparationsRequested>" & itemDetail.PreparationsRequested & "</PreparationsRequested>")
                            If itemDetail.PreparationTypeId IsNot Nothing Then
                                builder.Append("<PreparationTypeId>" & itemDetail.PreparationTypeId & "</PreparationTypeId>")
                            End If
                            builder.Append("<AdministrationRouteId>" & itemDetail.AdministrationRouteId & "</AdministrationRouteId>")
                            If itemDetail.AssociatedPackageId IsNot Nothing Then
                                builder.Append("<AssociatedPackageId>" & itemDetail.AssociatedPackageId & "</AssociatedPackageId>")
                            End If
                            builder.Append("<VolumeTotalOrder>" & itemDetail.VolumeTotalOrder.ToString().Replace(",", ".") & "</VolumeTotalOrder>")
                            builder.Append("<TotalPreparedUnitMeasurementId>" & itemDetail.TotalPreparedUnitMeasurementId & "</TotalPreparedUnitMeasurementId>")
                            builder.Append("<Concentration>" & itemDetail.Concentration & "</Concentration>")
                            builder.Append("<Description>" & itemDetail.Description & "</Description>")
                            builder.Append("<IsDelete>" & If(itemDetail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

                            If itemDetail.ExternalPatientPreparationDetail IsNot Nothing AndAlso itemDetail.ExternalPatientPreparationDetail.Count > 0 Then
                                For Each itemPreparationDetail In itemDetail.ExternalPatientPreparationDetail
                                    builder.Append("<ExternalPatientPreparationDetail>")
                                    builder.Append("<ExternalPatientPreparationRowId>" & ExternalPatientPreparationRowId & "</ExternalPatientPreparationRowId>")
                                    builder.Append("<Id>" & itemPreparationDetail.Id & "</Id>")
                                    builder.Append("<ExternalPatientPreparationId>" & itemPreparationDetail.ExternalPatientPreparationId & "</ExternalPatientPreparationId>")
                                    builder.Append("<itemType>" & itemPreparationDetail.itemType & "</itemType>")
                                    If itemPreparationDetail.AtcId IsNot Nothing Then
                                        builder.Append("<AtcId>" & itemPreparationDetail.AtcId & "</AtcId>")
                                    End If
                                    If itemPreparationDetail.SupplieId IsNot Nothing Then
                                        builder.Append("<SupplieId>" & itemPreparationDetail.SupplieId & "</SupplieId>")
                                    End If
                                    If itemPreparationDetail.ProductId IsNot Nothing Then
                                        builder.Append("<ProductId>" & itemPreparationDetail.ProductId & "</ProductId>")
                                    End If
                                    builder.Append("<ComponentType>" & itemPreparationDetail.ComponentType & "</ComponentType>")
                                    If itemPreparationDetail.Quantity IsNot Nothing Then
                                        builder.Append("<Quantity>" & itemPreparationDetail.Quantity.ToString().Replace(",", ".") & "</Quantity>")
                                    End If
                                    If itemPreparationDetail.MeasurementUnitId IsNot Nothing Then
                                        builder.Append("<MeasurementUnitId>" & itemPreparationDetail.MeasurementUnitId & "</MeasurementUnitId>")
                                    End If
                                    If itemPreparationDetail.Volume IsNot Nothing Then
                                        builder.Append("<Volume>" & itemPreparationDetail.Volume.ToString().Replace(",", ".") & "</Volume>")
                                    End If
                                    If itemPreparationDetail.VolumeMeasureUnitId IsNot Nothing Then
                                        builder.Append("<VolumeMeasureUnitId>" & itemPreparationDetail.VolumeMeasureUnitId & "</VolumeMeasureUnitId>")
                                    End If
                                    builder.Append("<IsDelete>" & If(itemPreparationDetail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                                    builder.Append("</ExternalPatientPreparationDetail>")
                                Next
                            End If

                            If itemDetail.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ExternalPatientPreparationDetail") Then
                                For Each itemPreparationDetail In itemDetail.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("ExternalPatientPreparationDetail")
                                    builder.Append("<ExternalPatientPreparationDetail>")
                                    builder.Append("<ExternalPatientPreparationRowId>" & ExternalPatientPreparationRowId & "</ExternalPatientPreparationRowId>")
                                    builder.Append("<Id>" & itemPreparationDetail.Id & "</Id>")
                                    builder.Append("<ExternalPatientPreparationId>" & itemPreparationDetail.ExternalPatientPreparationId & "</ExternalPatientPreparationId>")
                                    builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                                    builder.Append("</ExternalPatientPreparationDetail>")
                                Next
                            End If

                            builder.Append("</ExternalPatientPreparation>")

                            ExternalPatientPreparationRowId += 1
                        Next
                    End If

                    If item.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ExternalPatientPreparation") Then
                        For Each itemDetail As ExternalPatientPreparation In item.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("ExternalPatientPreparation")
                            builder.Append("<ExternalPatientPreparation>")
                            builder.Append("<RequestUnitDoseExternalCareCenterPatientRowId>" & RequestUnitDoseExternalCareCenterPatientRowId & "</RequestUnitDoseExternalCareCenterPatientRowId>")
                            builder.Append("<Id>" & itemDetail.Id & "</Id>")
                            builder.Append("<RequestUnitDoseExternalCareCenterPatientId>" & itemDetail.RequestUnitDoseExternalCareCenterPatientId & "</RequestUnitDoseExternalCareCenterPatientId>")
                            builder.Append("<IsDelete>" & 1 & "</IsDelete>")
                            builder.Append("</ExternalPatientPreparation>")
                        Next
                    End If

                    builder.Append("</RequestUnitDoseExternalCareCenterPatient>")

                    RequestUnitDoseExternalCareCenterPatientRowId += 1
                Next
            End If
        End With

        builder.Append("</RequestUnitDoseExternalCareCenter>")

        Return builder.ToString()
    End Function

    Public Function GetRequestUnitDoseExternalCareCenter(code As String, audit As AuditMessage) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IRequestUnitDoseExternalCareCenterAdminService.GetRequestUnitDoseExternalCareCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter = Me._RequestUnitDoseExternalCareCenterRepository.GetRequestUnitDoseExternalCareCenter(code.Trim())
            If RequestUnitDoseExternalCareCenter IsNot Nothing AndAlso RequestUnitDoseExternalCareCenter.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RequestUnitDoseExternalCareCenter)(RequestUnitDoseExternalCareCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseExternalCareCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetRequestUnitDoseExternalCareCenterById(id As Integer) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IRequestUnitDoseExternalCareCenterAdminService.GetRequestUnitDoseExternalCareCenterById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter = Me._RequestUnitDoseExternalCareCenterRepository.GetRequestUnitDoseExternalCareCenterById(id)
            Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseExternalCareCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestUnitDoseExternalCareCenter) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _RequestUnitDoseExternalCareCenterRepository = Nothing
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
