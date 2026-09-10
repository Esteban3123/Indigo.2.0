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
Imports Application.Billing
Imports System.Text

Public Class DashboardQuotedAdminService
    Implements IDashboardQuotedAdminService

    Private _dashboardQuotedRepository As IDashboardQuotedRepository
    Private _quotationAdminService As IQuotationAdminService

    Public Sub New(dashboardQuotedRepository As IDashboardQuotedRepository, quotationAdminService As IQuotationAdminService)
        If dashboardQuotedRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardQuotedRepository")
        End If
        _dashboardQuotedRepository = dashboardQuotedRepository
        _quotationAdminService = quotationAdminService
    End Sub

    Public Function SP_SaveDashboardQuoted(listDashboardQuoted As List(Of DashboardQuoted), audit As AuditMessage) As ActionResult(Of SP_SaveDashboardQuoted_Result) Implements IDashboardQuotedAdminService.SP_SaveDashboardQuoted
        If listDashboardQuoted Is Nothing OrElse listDashboardQuoted.Count = 0 Then
            Throw New ArgumentNullException("listDashboardQuoted")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(listDashboardQuoted)

                Dim result = _dashboardQuotedRepository.SP_SaveDashboardQuoted(xml, audit.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_SaveDashboardQuoted_Result) With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of SP_SaveDashboardQuoted_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_SaveDashboardQuoted_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(listDashboardQuoted As List(Of DashboardQuoted)) As String
        Dim builder As New StringBuilder

        For Each item In listDashboardQuoted
            builder.Append("<DashboardQuoted>")

            With item
                builder.Append("<Id>" & .Id & "</Id>")
                builder.Append("<AdmissionNumber>" & .AdmissionNumber & "</AdmissionNumber>")
                builder.Append("<ServiceCode>" & .ServiceCode & "</ServiceCode>")
                builder.Append("<Type>" & .Type & "</Type>")
                builder.Append("<PatientCode>" & .PatientCode & "</PatientCode>")
                builder.Append("<CareCenterCode>" & .CareCenterCode & "</CareCenterCode>")
                builder.Append("<RequestDate>" & .RequestDate.ToString("dd/MM/yyyy HH:mm") & "</RequestDate>")
                builder.Append("<RequestQuantity>" & .RequestQuantity & "</RequestQuantity>")
                builder.Append("<FunctionalUnitCode>" & .FunctionalUnitCode & "</FunctionalUnitCode>")
                builder.Append("<EntityId>" & .EntityId & "</EntityId>")
                builder.Append("<EntityName>" & .EntityName & "</EntityName>")
                builder.Append("<CareGroupId>" & .CareGroupId & "</CareGroupId>")
                builder.Append("<HealthAdministratorId>" & .HealthAdministratorId & "</HealthAdministratorId>")
                builder.Append("<ProfessionalCode>" & .ProfessionalCode & "</ProfessionalCode>")
                builder.Append("<QuotationId>" & item.QuotationId & "</QuotationId>")
                builder.Append("<Status>" & .Status & "</Status>")
            End With

            builder.Append("</DashboardQuoted>")
        Next

        Return builder.ToString()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _dashboardQuotedRepository = Nothing
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
