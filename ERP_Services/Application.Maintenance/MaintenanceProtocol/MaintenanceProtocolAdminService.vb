'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Diego Roldan
' Created          : 2018-08-31
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

Public Class MaintenanceProtocolAdminService
    Implements IMaintenanceProtocolAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    Private _maintenanceProtocolRepository As IMaintenanceProtocolRepository

    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository, maintenanceProtocolRepository As IMaintenanceProtocolRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If maintenanceProtocolRepository Is Nothing Then
            Throw New ArgumentNullException("maintenanceProtocolRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _maintenanceProtocolRepository = maintenanceProtocolRepository
    End Sub

    Public Function SaveMaintenanceProtocol(maintenanceProtocol As MaintenanceProtocol, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceProtocol) Implements IMaintenanceProtocolAdminService.SaveMaintenanceProtocol
        If maintenanceProtocol Is Nothing Then
            Throw New ArgumentNullException("maintenanceProtocol")
        End If
        Dim unitOfWork As IUnitWork = Me._maintenanceProtocolRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(maintenanceProtocol.Code) Then
                    Dim seq As MaintenanceSequenceDetail = _secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            maintenanceProtocol.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), maintenanceProtocol.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxmaintenanceProtocol As MaintenanceProtocol = Nothing
                Dim status As Integer
                If maintenanceProtocol.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), maintenanceProtocol.Code)
                    End If
                    maintenanceProtocol.CreationDate = Date.Now
                    maintenanceProtocol.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    maintenanceProtocol.ModificationDate = Date.Now
                    maintenanceProtocol.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxmaintenanceProtocol = maintenanceProtocol.OriginalValue
                End If
                Me._maintenanceProtocolRepository.SaveEntity(maintenanceProtocol)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaintenanceProtocol)(maintenanceProtocol, audit, status, auxmaintenanceProtocol)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = maintenanceProtocol, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeleteMaintenanceProtocol(maintenanceProtocol As MaintenanceProtocol, audit As AuditMessage) As ActionResult Implements IMaintenanceProtocolAdminService.DeleteMaintenanceProtocol
        If maintenanceProtocol Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediate")
        End If
        Dim unitOfWork As IUnitWork = Me._maintenanceProtocolRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While maintenanceProtocol.ProtocolActivities.Any()
                    maintenanceProtocol.ProtocolActivities.FirstOrDefault().MarkAsDeleted()
                End While
                While maintenanceProtocol.ProtocolConsumables.Any()
                    maintenanceProtocol.ProtocolConsumables.FirstOrDefault().MarkAsDeleted()
                End While
                While maintenanceProtocol.ProtocolSupplier.Any()
                    maintenanceProtocol.ProtocolSupplier.FirstOrDefault().MarkAsDeleted()
                End While
                While maintenanceProtocol.ProtocolTools.Any()
                    maintenanceProtocol.ProtocolTools.FirstOrDefault().MarkAsDeleted()
                End While
                maintenanceProtocol.MarkAsDeleted()
                maintenanceProtocol.ModificationUser = audit.CodeUser
                maintenanceProtocol.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaintenanceProtocol)(maintenanceProtocol, audit, status)
                Me._maintenanceProtocolRepository.SaveEntity(maintenanceProtocol)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetMaintenanceProtocol(code As String, audit As AuditMessage) As MaintenanceProtocol Implements IMaintenanceProtocolAdminService.GetMaintenanceProtocol
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim maintenanceprotocol As MaintenanceProtocol = Me._maintenanceProtocolRepository.GetMaintenanceProtocolByCode(code.Trim())
            If maintenanceprotocol IsNot Nothing AndAlso maintenanceprotocol.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MaintenanceProtocol)(maintenanceprotocol, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return maintenanceprotocol
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetMaintenanceProtocolById(id As Integer) As MaintenanceProtocol Implements IMaintenanceProtocolAdminService.GetMaintenanceProtocolById
        Try
            Return _maintenanceProtocolRepository.GetMaintenanceProtocolById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ChangeStateMaintenanceProtocol(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceProtocol) Implements IMaintenanceProtocolAdminService.ChangeStateMaintenanceProtocol
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim maintenanceProtocol As MaintenanceProtocol = Me._maintenanceProtocolRepository.GetMaintenanceProtocolByCode(code.Trim())
            If maintenanceProtocol IsNot Nothing AndAlso maintenanceProtocol.Id > 0 Then
                maintenanceProtocol.State = state
            End If
            Return Me.SaveMaintenanceProtocol(maintenanceProtocol, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceProtocol) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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

            ' TODO: libere los recursos no administrados (objetos no administrados) y reemplace Finalize() a continuación.
            ' TODO: configure los campos grandes en nulos.
            _maintenanceProtocolRepository = Nothing
            _secuenseDRepository = Nothing
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
