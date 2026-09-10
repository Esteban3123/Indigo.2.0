'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
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

Public Class TreasuryControlAdminService
    Implements ITreasuryControlAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de control de documentos de tesoreria
    ''' </summary>
    Private _treasuryControlRepository As ITreasuryControlRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal treasuryControlRepository As ITreasuryControlRepository)
        If treasuryControlRepository Is Nothing Then
            Throw New ArgumentNullException("treasuryControlRepository")
        End If
        _treasuryControlRepository = treasuryControlRepository
    End Sub

    ''' <summary>
    ''' Elimina un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function DeleteTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult Implements ITreasuryControlAdminService.DeleteTreasuryControl
        If treasuryControl Is Nothing Then
            Throw New ArgumentNullException("treasuryControl")
        End If
        Dim unitOfWork As IUnitWork = Me._treasuryControlRepository.UnitWork
        Try
            If treasuryControl.ChangeTracker.State = ObjectState.Deleted Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryControl)(treasuryControl, audit, status)
                Me._treasuryControlRepository.DeleteEntity(treasuryControl)
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
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetTreasuryControlById(Id As Integer) As TreasuryControl Implements ITreasuryControlAdminService.GetTreasuryControlById
        Try
            Return _treasuryControlRepository.GetTreasuryControlById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function SaveTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult(Of TreasuryControl) Implements ITreasuryControlAdminService.SaveTreasuryControl
        If treasuryControl Is Nothing Then
            Throw New ArgumentNullException("treasuryControl")
        End If
        Dim unitOfWork As IUnitWork = Me._treasuryControlRepository.UnitWork
        Try
            Dim auxCard As TreasuryControl = Nothing
            Dim status As Integer
            If treasuryControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxCard = treasuryControl.OriginalValue
            End If

            Me._treasuryControlRepository.SaveEntity(treasuryControl)
            If witCommit Then
                unitOfWork.Commit()
            End If
            Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryControl)(treasuryControl, audit, status, auxCard)
            auditProcess.Execute()
            Return New ActionResult(Of TreasuryControl) With {.StateResult = True, .ObjectEmbbeded = treasuryControl}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TreasuryControl) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As DbEntityValidationException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryControl) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryControl) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Public Function GetTreasuryControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As TreasuryControl Implements ITreasuryControlAdminService.GetTreasuryControlByDocumentNumber
        Try
            Return _treasuryControlRepository.GetTreasuryControlByDocumentNumber(DocumentNumber, DocumentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _treasuryControlRepository = Nothing
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