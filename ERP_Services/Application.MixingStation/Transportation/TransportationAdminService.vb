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
Public Class TransportationAdminService
    Implements ITransportationAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _transportationRepository As ITransportationRepository

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
    Public Sub New(transportationRepository As ITransportationRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If transportationRepository Is Nothing Then
            Throw New ArgumentNullException("transportationRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        _transportationRepository = transportationRepository
        _secuenseDetailRepository = secuenseDetailRepository
    End Sub

    Public Function SaveTransportation(Transportation As Transportation, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of Transportation) Implements ITransportationAdminService.SaveTransportation
        If Transportation Is Nothing Then
            Throw New ArgumentNullException("Transportation")
        End If
        Dim unitOfWork As IUnitWork = Me._transportationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If Transportation.Code Is Nothing OrElse Transportation.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDetailRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Transportation.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Transportation) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Transportation) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxTransportation As Transportation = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Transportation)
                Dim status As Integer

                If Transportation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Transportation.CreationUser = audit.CodeUser
                    Transportation.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxTransportation = Transportation.OriginalValue
                    Transportation.ModificationUser = audit.CodeUser
                    Transportation.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._transportationRepository.SaveEntity(Transportation)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Transportation)(Transportation, audit, status, auxTransportation)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Transportation.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Transportation) With {.StateResult = True, .ObjectEmbbeded = Transportation}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Transportation) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Transportation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteTransportation(Transportation As Transportation, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements ITransportationAdminService.DeleteTransportation
        If Transportation Is Nothing Then
            Throw New ArgumentNullException("Transportation")
        End If
        Dim unitOfWork As IUnitWork = Me._transportationRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Transportation)
            auditProcess = New IndigoAuditSimpleEntity(Of Transportation)(Transportation, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            Transportation.MarkAsDeleted()

            Me._transportationRepository.SaveEntity(Transportation)
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

    Public Function GetTransportation(code As String, audit As AuditMessage) As ActionResult(Of Transportation) Implements ITransportationAdminService.GetTransportation
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Transportation As Transportation = Me._transportationRepository.GetTransportation(code)
            If Transportation IsNot Nothing AndAlso Transportation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Transportation)(Transportation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Transportation) With {.StateResult = True, .ObjectEmbbeded = Transportation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Transportation) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetTransportationById(id As Integer) As ActionResult(Of Transportation) Implements ITransportationAdminService.GetTransportationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim Transportation As Transportation = Me._transportationRepository.GetTransportationById(id)
            Return New ActionResult(Of Transportation) With {.StateResult = True, .ObjectEmbbeded = Transportation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Transportation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateTransportation(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of Transportation) Implements ITransportationAdminService.ChangeStateTransportation
        Dim Transportation As Transportation = _transportationRepository.GetTransportation(code)
        Transportation.Status = state
        Return SaveTransportation(Transportation, audit, operatingUnitId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _transportationRepository = Nothing
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
