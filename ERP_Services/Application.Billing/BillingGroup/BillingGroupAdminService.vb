'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
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

Public Class BillingGroupAdminService
    Implements IBillingGroupAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    Private _billingGroupRepository As IBillingGroupRepository

    Public Sub New(secuenceDRepository As IBillingSequenceDetailRepository, billingGroupRepository As IBillingGroupRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If billingGroupRepository Is Nothing Then
            Throw New ArgumentNullException("billingGroupRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _billingGroupRepository = billingGroupRepository
    End Sub

    Public Function ChangeStateBillingGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingGroup) Implements IBillingGroupAdminService.ChangeStateBillingGroup
        Dim BillingGroup As BillingGroup = _billingGroupRepository.GetBillingGroupByCode(code)
        BillingGroup.Status = state
        Return SaveBillingGroup(BillingGroup, audit)
    End Function

    Public Function DeleteBillingGroup(BillingGroup As BillingGroup, audit As AuditMessage) As ActionResult Implements IBillingGroupAdminService.DeleteBillingGroup
        If BillingGroup Is Nothing Then
            Throw New ArgumentNullException("BillingGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._billingGroupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of BillingGroup)
            auditProcess = New IndigoAuditSimpleEntity(Of BillingGroup)(BillingGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._billingGroupRepository.DeleteEntity(BillingGroup)
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

    Public Function GetBillingGroup(code As String, audit As AuditMessage) As BillingGroup Implements IBillingGroupAdminService.GetBillingGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BillingGroup As BillingGroup = Me._billingGroupRepository.GetBillingGroupByCode(code.Trim())
            If BillingGroup IsNot Nothing AndAlso BillingGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BillingGroup)(BillingGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return BillingGroup
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BillingGroup
        End Try
    End Function

    Public Function GetBillingGroupById(id As Integer) As BillingGroup Implements IBillingGroupAdminService.GetBillingGroupById
        Try
            Return _billingGroupRepository.GetBillingGroupById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New BillingGroup
        End Try
    End Function

    Public Function SaveBillingGroup(BillingGroup As BillingGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BillingGroup) Implements IBillingGroupAdminService.SaveBillingGroup
        If BillingGroup Is Nothing Then
            Throw New ArgumentNullException("BillingGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._billingGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As BillingSequenceDetail = Nothing
                If BillingGroup.Code Is Nothing OrElse BillingGroup.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            BillingGroup.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BillingGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BillingGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxBillingGroup As BillingGroup = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BillingGroup)
                Dim status As Integer

                If BillingGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    BillingGroup.CreationUser = audit.CodeUser
                    BillingGroup.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxBillingGroup = BillingGroup.OriginalValue
                    BillingGroup.ModificationUser = audit.CodeUser
                    BillingGroup.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._billingGroupRepository.SaveEntity(BillingGroup)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BillingGroup)(BillingGroup, audit, status, auxBillingGroup)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                BillingGroup.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingGroup) With {.StateResult = True, .ObjectEmbbeded = BillingGroup}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _secuenseDRepository = Nothing
            _billingGroupRepository = Nothing
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
