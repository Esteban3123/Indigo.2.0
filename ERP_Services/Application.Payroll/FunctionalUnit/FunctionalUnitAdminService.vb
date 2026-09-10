'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 19-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Queue

Public Class FunctionalUnitAdminService
    Implements IFunctionalUnitAdminService

    ''' <summary>
    ''' Repositorio de la unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Private _FunctionalRepository As IFunctionalUnitRepository

    Private _Fu

    Private Const FORM_NAME As String = "Unidades Funcionales"
    ''' <summary>
    ''' Repositorio de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private _UserRepository As IUserRepository
    Private _secuenseDRepository As Domain.Entities.ISequenseAccountingDRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

    ''' <summary>
    ''' incia el repositorio de educationLevels
    ''' </summary>
    ''' <param name="repository">Repositorio de educations levels</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IFunctionalUnitRepository, UserRepository As IUserRepository,
                   secuenseDRepository As Domain.Entities.ISequenseAccountingDRepository, FactoryQueue As IFactoryQueue)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("Repositorio de unidad funcional Vacio")
        End If
        _FunctionalRepository = repository
        _UserRepository = UserRepository
        _secuenseDRepository = secuenseDRepository
        _factoryQueue = FactoryQueue
    End Sub

    ''' <summary>
    ''' Elimina una unidad funcional
    ''' </summary>
    ''' <param name="FunctionalUnit">Unidad Funcional</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteFunctionalUnit(functionalUnit As Domain.Payroll.Entities.FunctionalUnit, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Domain.Payroll.Entities.FunctionalUnit) Implements IFunctionalUnitAdminService.DeleteFunctionalUnit
        If functionalUnit Is Nothing Then
            Throw New ArgumentNullException("Unidad Funcional Vacia")
        End If
        Dim unitOfWork As IUnitWork = Me._FunctionalRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                functionalUnit.ModificationUser = audit.CodeUser
                functionalUnit.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FunctionalUnit)(functionalUnit, audit, status)

                While functionalUnit.FunctionalUnitResponsible.Count > 0
                    functionalUnit.FunctionalUnitResponsible.Item(functionalUnit.FunctionalUnitResponsible.Count() - 1).MarkAsDeleted()
                End While
                While functionalUnit.FunctionalUnitUser.Count > 0
                    functionalUnit.FunctionalUnitUser.Item(functionalUnit.FunctionalUnitUser.Count() - 1).MarkAsDeleted()
                End While
                While functionalUnit.FunctionalUnitUserAuthorizationRequest.Count > 0
                    functionalUnit.FunctionalUnitUserAuthorizationRequest.Item(functionalUnit.FunctionalUnitUserAuthorizationRequest.Count() - 1).MarkAsDeleted()
                End While

                functionalUnit.MarkAsDeleted()
                Me._FunctionalRepository.SaveEntity(functionalUnit)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of FunctionalUnit) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit(code As String) As Domain.Payroll.Entities.FunctionalUnit Implements IFunctionalUnitAdminService.GetFunctionalUnit
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Dim functionalUnit = _FunctionalRepository.GetFunctionalUnit(code)

            If functionalUnit Is Nothing OrElse functionalUnit.Id = 0 Then
                Return functionalUnit
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim listUsers As New List(Of User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim listUserIds As New List(Of Integer)

            If functionalUnit.FunctionalUnitResponsible.Any() Then
                listUserIds.AddRange(functionalUnit.FunctionalUnitResponsible.Select(Function(m) m.UserId).ToList())
            End If

            If functionalUnit.FunctionalUnitUser.Any() Then
                listUserIds.AddRange(functionalUnit.FunctionalUnitUser.Select(Function(m) m.UserId).ToList())
            End If

            If functionalUnit.FunctionalUnitUserAuthorizationRequest.Any() Then
                listUserIds.AddRange(functionalUnit.FunctionalUnitUserAuthorizationRequest.Select(Function(m) m.UserId).ToList())
            End If

            If listUserIds.Any() Then
                'Se obtiene el listado de usuarios
                listUsers = _UserRepository.ListUsersByIds(listUserIds.Distinct().ToList())
            End If

            If functionalUnit.FunctionalUnitResponsible.Any() AndAlso listUsers.Any() Then
                'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                For Each bu In functionalUnit.FunctionalUnitResponsible
                    Dim user = (From e In listUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                    If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                        bu.User = user.UserCode
                        bu.Fullname = user.Person.Fullname
                    End If
                Next
            End If

            If functionalUnit.FunctionalUnitUser.Any() AndAlso listUsers.Any() Then
                'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                For Each bu In functionalUnit.FunctionalUnitUser
                    Dim user = (From e In listUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                    If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                        bu.User = user.UserCode
                        bu.Fullname = user.Person.Fullname
                    End If
                Next
            End If

            If functionalUnit.FunctionalUnitUserAuthorizationRequest.Any() AndAlso listUsers.Any() Then
                'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                For Each bu In functionalUnit.FunctionalUnitUserAuthorizationRequest
                    Dim user = (From e In listUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                    If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                        bu.Fullname = user.Person.Fullname
                    End If
                Next
            End If

            Return functionalUnit
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="id">id de la unidad funcional</param>
    ''' <returns>la unidad funcional</returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitById(id As String) As FunctionalUnit Implements IFunctionalUnitAdminService.GetFunctionalUnitById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("Id vacio")
        End If
        Try
            Return _FunctionalRepository.GetFunctionalUnitById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las unidades funcionales
    ''' </summary>
    ''' <returns>Lista las unidades funcionales</returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit() As List(Of Domain.Payroll.Entities.FunctionalUnit) Implements IFunctionalUnitAdminService.ListAllFunctionalUnit
        Try

            Return _FunctionalRepository.ListAllFunctionalUnit()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnit">unidad funcional a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveFunctionalUnit(functionalUnit As Domain.Payroll.Entities.FunctionalUnit, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FunctionalUnit) Implements IFunctionalUnitAdminService.SaveFunctionalUnit
        If functionalUnit Is Nothing Then
            Throw New ArgumentNullException("functionalUnit")

        End If
        Dim unitOfWork As IUnitWork = Me._FunctionalRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If functionalUnit.Code Is Nothing OrElse functionalUnit.Code.Trim().Equals(String.Empty) Then
                    Dim seq As Domain.Entities.GeneralLedgerSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            functionalUnit.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FunctionalUnit) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), functionalUnit.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FunctionalUnit) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxfunctionalUnit As FunctionalUnit = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FunctionalUnit)
                Dim status As Integer

                If functionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    functionalUnit.CreationUser = audit.CodeUser
                    functionalUnit.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxfunctionalUnit = _FunctionalRepository.GetFunctionalUnitById(functionalUnit.Id, False) 'functionalUnit.OriginalValue
                    functionalUnit.ModificationUser = audit.CodeUser
                    functionalUnit.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._FunctionalRepository.SaveEntity(functionalUnit)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FunctionalUnit)(functionalUnit, audit, status, auxfunctionalUnit)
                auditProcess.Execute()

                'se asegura que se hace commit y se se dispara el evento
                TriggerEvent(functionalUnit, audit)
                'Se marca la entidad como sin cambios
                functionalUnit.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FunctionalUnit) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = functionalUnit, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    Public Function UpdateStateFunctionalUnit(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FunctionalUnit) Implements IFunctionalUnitAdminService.UpdateStateFunctionalUnit
        Try
            Dim functionalUnit As FunctionalUnit = Me._FunctionalRepository.GetFunctionalUnit(code.Trim())
            If functionalUnit IsNot Nothing AndAlso functionalUnit.Id > 0 Then
                functionalUnit.State = state
            End If
            Return Me.SaveFunctionalUnit(functionalUnit, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FunctionalUnit) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Sub TriggerEvent(functionalUnit As FunctionalUnit, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = ""
        If functionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            ChangeTracker = "added"
        ElseIf functionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or functionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
            ChangeTracker = "modified"
        End If
        Dim eventData = wrapperEvent.GenerateWrapperEventData(functionalUnit, audit.CodeUser, ChangeTracker, DittoSourceType.functionalUnit)
        Dim queue = _factoryQueue.CreateQueue()
        queue.Publish(eventData)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _FunctionalRepository = Nothing
            _UserRepository = Nothing
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
