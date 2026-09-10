'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Validation

#End Region

Public Class BillingControlAdminService
    Implements IBillingControlAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de control de documentos de tesoreria
    ''' </summary>
    Private _billingControlRepository As IBillingControlRepository

#End Region

#Region "Methods"

    Public Sub New(billingControlRepository As IBillingControlRepository)
        If billingControlRepository Is Nothing Then
            Throw New ArgumentNullException("treasuryControlRepository")
        End If
        _billingControlRepository = billingControlRepository
    End Sub

#End Region

    ''' <summary>
    ''' Elimina un registro de control de los documentos de facturacion
    ''' </summary>
    ''' <param name="billingControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="witCommit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function DeleteBillingControl(billingControl As BillingControl, audit As AuditMessage, Optional witCommit As Boolean = True) As ActionResult Implements IBillingControlAdminService.DeleteBillingControl
        If billingControl Is Nothing Then
            Throw New ArgumentNullException("treasuryControl")
        End If
        Dim unitOfWork As IUnitWork = Me._billingControlRepository.UnitWork
        Try
            If billingControl.ChangeTracker.State = ObjectState.Deleted Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingControl)(billingControl, audit, status)
                Me._billingControlRepository.DeleteEntity(billingControl)
                If witCommit Then
                    unitOfWork.Commit()
                End If
                auditProcess.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de facturacion por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    Public Function GetBillingControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As BillingControl Implements IBillingControlAdminService.GetBillingControlByDocumentNumber
        Try
            Return _billingControlRepository.GetBillingControlByDocumentNumber(DocumentNumber, DocumentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de facturacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetBillingControlById(Id As Integer) As BillingControl Implements IBillingControlAdminService.GetBillingControlById
        Try
            Return _billingControlRepository.GetBillingControlById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro de control de los documentos de facturacion
    ''' </summary>
    ''' <param name="billingControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="witCommit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function SaveBillingControl(billingControl As BillingControl, audit As AuditMessage, Optional witCommit As Boolean = True) As ActionResult(Of BillingControl) Implements IBillingControlAdminService.SaveBillingControl
        If billingControl Is Nothing Then
            Throw New ArgumentNullException("treasuryControl")
        End If
        Dim unitOfWork As IUnitWork = Me._billingControlRepository.UnitWork
        Try
            Dim auxCard As BillingControl = Nothing
            Dim status As Integer
            If billingControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxCard = billingControl.OriginalValue
            End If

            Me._billingControlRepository.SaveEntity(billingControl)
            If witCommit Then
                unitOfWork.Commit()
            End If
            Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingControl)(billingControl, audit, status, auxCard)
            auditProcess.Execute()
            Return New ActionResult(Of BillingControl) With {.StateResult = True, .ObjectEmbbeded = billingControl}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingControl) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As DbEntityValidationException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingControl) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingControl) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _billingControlRepository = Nothing
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