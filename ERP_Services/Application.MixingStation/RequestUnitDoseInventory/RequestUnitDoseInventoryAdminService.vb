'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class RequestUnitDoseInventoryAdminService
    Implements IRequestUnitDoseInventoryAdminService, Inject

    Private _RequestUnitDoseInventoryRepository As IRequestUnitDoseInventoryRepository

    Private _IADCENATENRepository As IADCENATENRepository

    Public Sub New(RequestUnitDoseInventoryRepository As IRequestUnitDoseInventoryRepository, IADCENATENRepository As IADCENATENRepository)
        If RequestUnitDoseInventoryRepository Is Nothing Then
            Throw New ArgumentNullException("RequestUnitDoseInventoryRepository vacío")
        End If
        If IADCENATENRepository Is Nothing Then
            Throw New ArgumentNullException("IADCENATENRepository vacío")
        End If
        _RequestUnitDoseInventoryRepository = RequestUnitDoseInventoryRepository
        _IADCENATENRepository = IADCENATENRepository
    End Sub

    Public Function SaveRequestUnitDoseInventory(RequestUnitDoseInventory As RequestUnitDoseInventory, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of RequestUnitDoseInventory) Implements IRequestUnitDoseInventoryAdminService.SaveRequestUnitDoseInventory
        If RequestUnitDoseInventory Is Nothing Then
            Throw New ArgumentNullException("RequestUnitDoseInventory")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(RequestUnitDoseInventory, operatingUnitId)

                Dim result = _RequestUnitDoseInventoryRepository.SP_SaveRequestUnitDoseInventory(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = False, .Message = result.Message}
                End If

                RequestUnitDoseInventory.Id = result.Id
                RequestUnitDoseInventory.Code = result.Code

                scope.Complete()
                Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseInventory, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(RequestUnitDoseInventory As RequestUnitDoseInventory, operatingUnitId As Integer) As Object
        Dim builder As New StringBuilder

        builder.Append("<RequestUnitDoseInventory>")

        With RequestUnitDoseInventory
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<CMConfigurationId>" & .CMConfigurationId & "</CMConfigurationId>")
            builder.Append("<WarehouseId>" & .WarehouseId & "</WarehouseId>")
            builder.Append("<DocumentDate>" & .DocumentDate.ToString("dd/MM/yyyy HH:mm") & "</DocumentDate>")
            builder.Append("<Status>" & .Status & "</Status>")
            builder.Append("<OperatingUnitId>" & operatingUnitId & "</OperatingUnitId>")
            builder.Append("<CareCenterCode>" & .CareCenterCode & "</CareCenterCode>")

            If .RequestUnitDoseInventoryDetail IsNot Nothing AndAlso .RequestUnitDoseInventoryDetail.Count > 0 Then
                For Each item In .RequestUnitDoseInventoryDetail
                    builder.Append("<RequestUnitDoseInventoryDetail>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<RequestUnitDoseInventoryId>" & item.RequestUnitDoseInventoryId & "</RequestUnitDoseInventoryId>")
                    builder.Append("<UnitDoseTypeId>" & item.UnitDoseTypeId & "</UnitDoseTypeId>")
                    builder.Append("<ATCId>" & item.ATCId & "</ATCId>")
                    builder.Append("<PackageId>" & item.PackageId & "</PackageId>")
                    builder.Append("<Quantity>" & item.Quantity & "</Quantity>")
                    builder.Append("<AdministrationRouteId>" & item.AdministrationRouteId & "</AdministrationRouteId>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</RequestUnitDoseInventoryDetail>")
                Next
            End If
        End With

        builder.Append("</RequestUnitDoseInventory>")

        Return builder.ToString()
    End Function

    Public Function GetRequestUnitDoseInventory(code As String, audit As AuditMessage) As ActionResult(Of RequestUnitDoseInventory) Implements IRequestUnitDoseInventoryAdminService.GetRequestUnitDoseInventory
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RequestUnitDoseInventory As RequestUnitDoseInventory = Me._RequestUnitDoseInventoryRepository.GetRequestUnitDoseInventory(code.Trim())
            If RequestUnitDoseInventory IsNot Nothing AndAlso RequestUnitDoseInventory.Id > 0 Then

                Dim careCenter = _IADCENATENRepository.GetADCENATENByCode(RequestUnitDoseInventory.CareCenterCode, False)
                If careCenter IsNot Nothing AndAlso Not String.IsNullOrEmpty(careCenter.CODCENATE) Then
                    RequestUnitDoseInventory.CareCenterCodeName = careCenter.CODCENATE.Trim + " - " + careCenter.NOMCENATE.Trim
                End If

                Dim auditObject As New IndigoAuditSimpleEntity(Of RequestUnitDoseInventory)(RequestUnitDoseInventory, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseInventory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetRequestUnitDoseInventoryById(id As Integer) As ActionResult(Of RequestUnitDoseInventory) Implements IRequestUnitDoseInventoryAdminService.GetRequestUnitDoseInventoryById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim RequestUnitDoseInventory As RequestUnitDoseInventory = Me._RequestUnitDoseInventoryRepository.GetRequestUnitDoseInventoryById(id)
            Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = True, .ObjectEmbbeded = RequestUnitDoseInventory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestUnitDoseInventory) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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
            _RequestUnitDoseInventoryRepository = Nothing
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
