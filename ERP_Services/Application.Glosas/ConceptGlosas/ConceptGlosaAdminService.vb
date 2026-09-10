'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class ConceptGlosaAdminService
    Implements IConceptGlosaAdminService

    Private ReadOnly _conceptGlosasRepository As IConceptGlosaRepository
    Private ReadOnly _secuenceDRepository As IGlosaSequenceDetailRepository
    Private ReadOnly _userRepository As IUserRepository

    Public Sub New(conceptGlosasRepository As IConceptGlosaRepository,
                   secuenceDRepository As IGlosaSequenceDetailRepository,
                   userRepository As IUserRepository)
        _secuenceDRepository = secuenceDRepository
        _conceptGlosasRepository = conceptGlosasRepository
        _userRepository = userRepository
    End Sub

    Public Function GetConceptGlosasById(id As Integer) As ConceptGlosas Implements IConceptGlosaAdminService.GetConceptGlosasById
        Try
            Return _conceptGlosasRepository.FindById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetConceptGlosasByCode(code As String) As ConceptGlosas Implements IConceptGlosaAdminService.GetConceptGlosasByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If

        Try
            Dim concept = _conceptGlosasRepository.FirstOrDefault(Function(m) m.Code = code.Trim(), includes:={"ConceptGlosasUser"})

            If concept IsNot Nothing AndAlso concept.ConceptGlosasUser.Any() Then
                Dim userIds = concept.ConceptGlosasUser.Select(Function(m) m.UserId).ToList()
                Dim users = _userRepository.Query(Function(m) userIds.Contains(m.Id), False, {"Person"}) _
                    .Select(Function(m) New With {.UserId = m.Id, .UserFullName = m.Person.Fullname}) _
                    .ToList()

                For Each u In concept.ConceptGlosasUser
                    u.UserFullName = users.FirstOrDefault(Function(m) m.UserId = u.UserId)?.UserFullName
                Next
            End If

            Return concept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveConceptGlosas(conceptGlosa As ConceptGlosas, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ConceptGlosas) Implements IConceptGlosaAdminService.SaveConceptGlosas
        If conceptGlosa Is Nothing Then
            Throw New ArgumentNullException("BillingGroup")
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As GlosaSequenceDetail = Nothing
                If conceptGlosa.Code Is Nothing OrElse conceptGlosa.Code.Trim().Equals(String.Empty) Then
                    seq = _secuenceDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GlosaSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            conceptGlosa.Code = res
                            seq.Next += 1
                            _secuenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ConceptGlosas) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ConceptGlosas) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxConceptGlosa As ConceptGlosas = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ConceptGlosas)
                Dim status As Integer

                If conceptGlosa.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'conceptGlosa.CreationUser = audit.CodeUser
                    'conceptGlosa.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxConceptGlosa = conceptGlosa.OriginalValue
                    'conceptGlosa.ModificationUser = audit.CodeUser
                    'conceptGlosa.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                _conceptGlosasRepository.SaveEntity(conceptGlosa)
                _conceptGlosasRepository.UnitWork.Commit()

                auditProcess = New IndigoAuditSimpleEntity(Of ConceptGlosas)(conceptGlosa, audit, status, auxConceptGlosa)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                conceptGlosa.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ConceptGlosas) With {.StateResult = True, .ObjectEmbbeded = conceptGlosa}
            End Using
        Catch ex As Exception
            _conceptGlosasRepository.UnitWork.RollbackChanges()
            Return New ActionResult(Of ConceptGlosas) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un concepto de glosas
    ''' </summary>
    ''' <param name="conceptGlosa"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteConceptGlosas(conceptGlosa As ConceptGlosas, audit As AuditMessage) As ActionResult Implements IConceptGlosaAdminService.DeleteConceptGlosas
        If conceptGlosa Is Nothing Then
            Throw New ArgumentNullException("BillingGroup")
        End If
        Dim unitOfWork As IUnitWork = _conceptGlosasRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While conceptGlosa.ConceptGlosasUser.Any()
                    conceptGlosa.ConceptGlosasUser(0).MarkAsDeleted()
                End While

                conceptGlosa.MarkAsDeleted()

                Dim auditProcess As IndigoAuditSimpleEntity(Of ConceptGlosas)
                auditProcess = New IndigoAuditSimpleEntity(Of ConceptGlosas)(conceptGlosa, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                _conceptGlosasRepository.SaveEntity(conceptGlosa)
                unitOfWork.Commit()
                scope.Complete()
                auditProcess.Execute()
            End Using
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
    ''' Cambia el estado de los conceptos de glosas
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeConceptGlosas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConceptGlosas) Implements IConceptGlosaAdminService.ChangeConceptGlosas
        Dim conceptGlosas = _conceptGlosasRepository.FirstOrDefault(Function(m) m.Code = code)
        conceptGlosas.State = state
        Return SaveConceptGlosas(conceptGlosas, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
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
