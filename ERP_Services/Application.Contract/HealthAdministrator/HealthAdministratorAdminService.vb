'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
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
Imports Application.Events.Models
Imports Infrastructure.CrossCutting.Queue

Public Class HealthAdministratorAdminService
    Implements IHealthAdministratorAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal healthAdministratorRepository As IHealthAdministratorRepository, ByVal secuenseDRepository As ISequenseContractDRepository, FactoryQueue As IFactoryQueue)
        If healthAdministratorRepository Is Nothing Then
            Throw New ArgumentNullException("healthAdministratorRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _healthAdministratorRepository = healthAdministratorRepository
        _secuenseDRepository = secuenseDRepository
        _factoryQueue = FactoryQueue
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateHealthAdministrator(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of HealthAdministrator) Implements IHealthAdministratorAdminService.ChangeStateHealthAdministrator
        Dim HealthAdministrator As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministrator(code)
        HealthAdministrator.Status = state
        Return SaveHealthAdministrator(HealthAdministrator, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="HealthAdministrator"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteHealthAdministrator(HealthAdministrator As HealthAdministrator, audit As AuditMessage) As ActionResult Implements IHealthAdministratorAdminService.DeleteHealthAdministrator
        If HealthAdministrator Is Nothing Then
            Throw New ArgumentNullException("HealthAdministrator")
        End If
        Dim unitOfWork As IUnitWork = Me._healthAdministratorRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of HealthAdministrator)
            auditProcess = New IndigoAuditSimpleEntity(Of HealthAdministrator)(HealthAdministrator, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._healthAdministratorRepository.DeleteEntity(HealthAdministrator)
            unitOfWork.Commit()
            auditProcess.Execute()

            HealthAdministrator.MarkAsDeleted()
            TriggerEvent(HealthAdministrator, audit)
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

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthAdministrator(code As String, audit As AuditMessage) As ActionResult(Of HealthAdministrator) Implements IHealthAdministratorAdminService.GetHealthAdministrator
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim HealthAdministrator As HealthAdministrator = Me._healthAdministratorRepository.GetHealthAdministrator(code.Trim())
            If HealthAdministrator IsNot Nothing AndAlso HealthAdministrator.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of HealthAdministrator)(HealthAdministrator, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = True, .ObjectEmbbeded = HealthAdministrator}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthAdministratorById(id As Integer, audit As AuditMessage) As ActionResult(Of HealthAdministrator) Implements IHealthAdministratorAdminService.GetHealthAdministratorById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim HealthAdministrator As HealthAdministrator = Me._healthAdministratorRepository.GetHealthAdministratorById(id)
            If HealthAdministrator IsNot Nothing AndAlso HealthAdministrator.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of HealthAdministrator)(HealthAdministrator, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = True, .ObjectEmbbeded = HealthAdministrator}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="HealthAdministrator"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHealthAdministrator(HealthAdministrator As HealthAdministrator, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of HealthAdministrator) Implements IHealthAdministratorAdminService.SaveHealthAdministrator
        If HealthAdministrator Is Nothing Then
            Throw New ArgumentNullException("HealthAdministrator")
        End If
        Dim unitOfWork As IUnitWork = Me._healthAdministratorRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If HealthAdministrator.Code Is Nothing OrElse HealthAdministrator.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        HealthAdministrator.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxHealthAdministrator As HealthAdministrator = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of HealthAdministrator)
            Dim status As Integer

            If HealthAdministrator.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                HealthAdministrator.CreationUser = audit.CodeUser
                HealthAdministrator.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxHealthAdministrator = HealthAdministrator.OriginalValue
                HealthAdministrator.ModificationUser = audit.CodeUser
                HealthAdministrator.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If
            TriggerEvent(HealthAdministrator, audit)
            Me._healthAdministratorRepository.SaveEntity(HealthAdministrator)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of HealthAdministrator)(HealthAdministrator, audit, status, auxHealthAdministrator)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            HealthAdministrator.MarkAsUnchanged()

            Return New ActionResult(Of HealthAdministrator) With {.StateResult = True, .ObjectEmbbeded = HealthAdministrator}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {"-555"}.ToList(), .Message = ex.InnerException.InnerException.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthAdministrator) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


#Region "Events"

    Public Sub TriggerEvent(HealthAdministrator As HealthAdministrator, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = HealthAdministrator.ChangeTracker.State.ToString().ToLower()

        If {ObjectState.Unchanged, ObjectState.Modified}.Contains(HealthAdministrator.ChangeTracker.State) Then
            ChangeTracker = "modified"

        ElseIf HealthAdministrator.ChangeTracker.State = ObjectState.Added Then
            ChangeTracker = "added"
        End If

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(HealthAdministrator, audit.CodeUser, ChangeTracker, DittoSourceType.healthAdministrator)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _healthAdministratorRepository = Nothing
            _secuenseDRepository = Nothing
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
