#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

#End Region

Public Class FixedAssetDepreciationAdminService
    Implements IFixedAssetDepreciationAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _fixedAssetDepreciationRepository As IFixedAssetDepreciationRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fixedAssetDepreciationRepository As IFixedAssetDepreciationRepository)
        If fixedAssetDepreciationRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetDepreciationRepository")
        End If
        _fixedAssetDepreciationRepository = fixedAssetDepreciationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el registro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation) Implements IFixedAssetDepreciationAdminService.ConfirmDepreciation

    End Function

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetDepreciation(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation) Implements IFixedAssetDepreciationAdminService.GetFixedAssetDepreciation
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetDepreciation As FixedAssetDepreciation = Me._fixedAssetDepreciationRepository.GetFixedAssetDepreciation(code.Trim())
            If FixedAssetDepreciation IsNot Nothing AndAlso FixedAssetDepreciation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetDepreciation)(FixedAssetDepreciation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = True, .ObjectEmbbeded = FixedAssetDepreciation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetDepreciationById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation) Implements IFixedAssetDepreciationAdminService.GetFixedAssetDepreciationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetDepreciation As FixedAssetDepreciation = Me._fixedAssetDepreciationRepository.GetFixedAssetDepreciationById(Id)
            If FixedAssetDepreciation IsNot Nothing AndAlso FixedAssetDepreciation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetDepreciation)(FixedAssetDepreciation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = True, .ObjectEmbbeded = FixedAssetDepreciation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta el proceso de depreciación
    ''' </summary>
    ''' <param name="DepreciationMonth"></param>
    ''' <param name="DepreciationYear"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <param name="ModeConfirm"></param>
    ''' <returns></returns>
    Public Function SaveDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage, ModeConfirm As Boolean) As ActionResult(Of FixedAssetDepreciation) Implements IFixedAssetDepreciationAdminService.SaveDepreciation
        If DepreciationMonth = 0 OrElse DepreciationYear = 0 Then
            Throw New ArgumentNullException("DepreciationDate")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = _fixedAssetDepreciationRepository.SP_SaveDepreciation(DepreciationMonth, DepreciationYear, audit.CodeUser, OperatingUnitId, ModeConfirm)
                If resultStore.CodeMessage <> 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = False, .Message = resultStore.Message, .StatusCode = eStatusResult.WARNING}
                End If

                Dim FixedAssetDepreciation As New FixedAssetDepreciation
                FixedAssetDepreciation.Id = resultStore.Id
                FixedAssetDepreciation.Code = resultStore.Code

                'Mensaje que se devuelve
                Dim message As New StringBuilder
                message.AppendLine(resultStore.Message)
                If ModeConfirm Then
                    message.AppendLine("Se generaron comprobantes contables tipo " + resultStore.JournalVoucherType + ": " + resultStore.Consecutives)
                End If

                transaction.Complete()
                Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = True, .ObjectEmbbeded = FixedAssetDepreciation, .StatusCode = eStatusResult.SUCCESS, .Message = message.ToString}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Dim error_message As String = ex.Message
                If ex.InnerException IsNot Nothing AndAlso Not (String.IsNullOrEmpty(ex.InnerException.Message)) Then
                    error_message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not (String.IsNullOrEmpty(ex.InnerException.InnerException.Message)) Then
                        error_message = ex.InnerException.InnerException.Message
                    End If
                End If

                Return New ActionResult(Of FixedAssetDepreciation) With {.StateResult = False, .Message = error_message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _fixedAssetDepreciationRepository = Nothing
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
