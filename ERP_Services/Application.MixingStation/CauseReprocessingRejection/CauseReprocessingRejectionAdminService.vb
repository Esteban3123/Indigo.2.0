'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
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
Imports Application.Security
Imports Application.MixingStation

Public Class CauseReprocessingRejectionAdminService
    Implements ICauseReprocessingRejectionAdminService, Inject

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMixingStationSequenceDetailRepository
    Private _causeReprocessingRejectionRepository As ICauseReprocessingRejectionRepository

    Public Sub New(secuenceDRepository As IMixingStationSequenceDetailRepository, causeReprocessingRejectionRepository As ICauseReprocessingRejectionRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If causeReprocessingRejectionRepository Is Nothing Then
            Throw New ArgumentNullException("causeReprocessingRejectionRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _causeReprocessingRejectionRepository = causeReprocessingRejectionRepository
    End Sub

    Public Function SaveCauseReprocessingRejection(CauseReprocessingRejection As CauseReprocessingRejection, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CauseReprocessingRejection) Implements ICauseReprocessingRejectionAdminService.SaveCauseReprocessingRejection
        If CauseReprocessingRejection Is Nothing Then
            Throw New ArgumentNullException("CauseReprocessingRejection")
        End If
        Dim unitOfWork As IUnitWork = Me._causeReprocessingRejectionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If CauseReprocessingRejection.Code Is Nothing OrElse CauseReprocessingRejection.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CauseReprocessingRejection.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CauseReprocessingRejection) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CauseReprocessingRejection) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxCauseReprocessingRejection As CauseReprocessingRejection = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CauseReprocessingRejection)
                Dim status As Integer

                If CauseReprocessingRejection.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CauseReprocessingRejection.CreationUser = audit.CodeUser
                    CauseReprocessingRejection.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCauseReprocessingRejection = CauseReprocessingRejection.OriginalValue
                    CauseReprocessingRejection.ModificationUser = audit.CodeUser
                    CauseReprocessingRejection.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._causeReprocessingRejectionRepository.SaveEntity(CauseReprocessingRejection)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CauseReprocessingRejection)(CauseReprocessingRejection, audit, status, auxCauseReprocessingRejection)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CauseReprocessingRejection.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CauseReprocessingRejection) With {.StateResult = True, .ObjectEmbbeded = CauseReprocessingRejection}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CauseReprocessingRejection) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CauseReprocessingRejection) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteCauseReprocessingRejection(CauseReprocessingRejection As CauseReprocessingRejection, audit As AuditMessage) As ActionResult Implements ICauseReprocessingRejectionAdminService.DeleteCauseReprocessingRejection
        If CauseReprocessingRejection Is Nothing Then
            Throw New ArgumentNullException("CauseReprocessingRejection")
        End If
        Dim unitOfWork As IUnitWork = Me._causeReprocessingRejectionRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CauseReprocessingRejection)
            auditProcess = New IndigoAuditSimpleEntity(Of CauseReprocessingRejection)(CauseReprocessingRejection, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            CauseReprocessingRejection.MarkAsDeleted()

            Me._causeReprocessingRejectionRepository.SaveEntity(CauseReprocessingRejection)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetCauseReprocessingRejection(code As String, audit As AuditMessage) As CauseReprocessingRejection Implements ICauseReprocessingRejectionAdminService.GetCauseReprocessingRejection
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CauseReprocessingRejection As CauseReprocessingRejection = Me._causeReprocessingRejectionRepository.GetCauseReprocessingRejectionByCode(code.Trim())
            If CauseReprocessingRejection IsNot Nothing AndAlso CauseReprocessingRejection.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CauseReprocessingRejection)(CauseReprocessingRejection, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return CauseReprocessingRejection
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CauseReprocessingRejection
        End Try
    End Function

    Public Function GetCauseReprocessingRejectionById(id As Integer) As CauseReprocessingRejection Implements ICauseReprocessingRejectionAdminService.GetCauseReprocessingRejectionById
        Try
            Return _causeReprocessingRejectionRepository.GetCauseReprocessingRejectionById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CauseReprocessingRejection
        End Try
    End Function

    Public Function ChangeStateCauseReprocessingRejection(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CauseReprocessingRejection) Implements ICauseReprocessingRejectionAdminService.ChangeStateCauseReprocessingRejection
        Dim CauseReprocessingRejection As CauseReprocessingRejection = _causeReprocessingRejectionRepository.GetCauseReprocessingRejectionByCode(code)
        CauseReprocessingRejection.Status = state
        Return SaveCauseReprocessingRejection(CauseReprocessingRejection, audit)
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
            _causeReprocessingRejectionRepository = Nothing
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
