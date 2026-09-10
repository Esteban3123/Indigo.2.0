'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.MixingStation
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class TransportationAssistantAdminService
    Implements ITransportationAssistantAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _transportationAssistantRepository As ITransportationAssistantRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(transportationAssistantRepository As ITransportationAssistantRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If transportationAssistantRepository Is Nothing Then
            Throw New ArgumentNullException("transportationAssistantRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        _transportationAssistantRepository = transportationAssistantRepository
        _secuenseDetailRepository = secuenseDetailRepository
    End Sub

    Public Function SaveTransportationAssistant(TransportationAssistant As TransportationAssistant, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of TransportationAssistant) Implements ITransportationAssistantAdminService.SaveTransportationAssistant
        If TransportationAssistant Is Nothing Then
            Throw New ArgumentNullException("TransportationAssistant")
        End If
        Dim unitOfWork As IUnitWork = Me._transportationAssistantRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If TransportationAssistant.Code Is Nothing OrElse TransportationAssistant.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDetailRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            TransportationAssistant.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of TransportationAssistant) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of TransportationAssistant) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxTransportationAssistant As TransportationAssistant = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of TransportationAssistant)
                Dim status As Integer

                If TransportationAssistant.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    TransportationAssistant.CreationUser = audit.CodeUser
                    TransportationAssistant.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxTransportationAssistant = TransportationAssistant.OriginalValue
                    TransportationAssistant.ModificationUser = audit.CodeUser
                    TransportationAssistant.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._transportationAssistantRepository.SaveEntity(TransportationAssistant)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of TransportationAssistant)(TransportationAssistant, audit, status, auxTransportationAssistant)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                TransportationAssistant.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of TransportationAssistant) With {.StateResult = True, .ObjectEmbbeded = TransportationAssistant}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteTransportationAssistant(TransportationAssistant As TransportationAssistant, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements ITransportationAssistantAdminService.DeleteTransportationAssistant
        If TransportationAssistant Is Nothing Then
            Throw New ArgumentNullException("TransportationAssistant")
        End If
        Dim unitOfWork As IUnitWork = Me._transportationAssistantRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of TransportationAssistant)
            auditProcess = New IndigoAuditSimpleEntity(Of TransportationAssistant)(TransportationAssistant, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            TransportationAssistant.MarkAsDeleted()

            Me._transportationAssistantRepository.SaveEntity(TransportationAssistant)
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

    Public Function GetTransportationAssistant(code As String, audit As AuditMessage) As ActionResult(Of TransportationAssistant) Implements ITransportationAssistantAdminService.GetTransportationAssistant
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim TransportationAssistant As TransportationAssistant = Me._transportationAssistantRepository.GetTransportationAssistant(code)
            If TransportationAssistant IsNot Nothing AndAlso TransportationAssistant.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TransportationAssistant)(TransportationAssistant, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = True, .ObjectEmbbeded = TransportationAssistant}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetTransportationAssistantById(id As Integer) As ActionResult(Of TransportationAssistant) Implements ITransportationAssistantAdminService.GetTransportationAssistantById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim TransportationAssistant As TransportationAssistant = Me._transportationAssistantRepository.GetTransportationAssistantById(id)
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = True, .ObjectEmbbeded = TransportationAssistant}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TransportationAssistant) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateTransportationAssistant(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of TransportationAssistant) Implements ITransportationAssistantAdminService.ChangeStateTransportationAssistant
        Dim TransportationAssistant As TransportationAssistant = _transportationAssistantRepository.GetTransportationAssistant(code)
        TransportationAssistant.Status = state
        Return SaveTransportationAssistant(TransportationAssistant, audit, operatingUnitId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _transportationAssistantRepository = Nothing
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
