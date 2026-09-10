'***********************************************************************
' Assembly         : Application.Budget
' Author           : Duván Albeiro Mejia Cortes 
' Created          : 2022-01-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class PublicPolicyAdminService
    Implements IPublicPolicyAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmPublicPolicy"
    ''' <summary>
    ''' Repositorio de la política públicas
    ''' </summary>
    Private _publicPolicyRepository As IPublicPolicyRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="publicPolicyRepository"></param>
    ''' <param name="sequenseRepository"></param>
    Public Sub New(ByVal publicPolicyRepository As IPublicPolicyRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If publicPolicyRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _publicPolicyRepository = publicPolicyRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una política pública
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Public Function GetPublicPolicy(code As String, audit As AuditMessage) As ActionResult(Of PublicPolicy) Implements IPublicPolicyAdminService.GetPublicPolicy
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim publicPolicy = Me._publicPolicyRepository.GetByFilter(Function(m) m.Code = code.Trim()).FirstOrDefault()
            If publicPolicy IsNot Nothing AndAlso publicPolicy.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PublicPolicy)(publicPolicy, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PublicPolicy) With {.StateResult = True, .ObjectEmbbeded = publicPolicy}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PublicPolicy) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una política pública
    ''' </summary>
    ''' <param name="publicPolicy"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SavePublicPolicy(publicPolicy As PublicPolicy, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PublicPolicy) Implements IPublicPolicyAdminService.SavePublicPolicy

        If publicPolicy Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._publicPolicyRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(publicPolicy.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            publicPolicy.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PublicPolicy) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), publicPolicy.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PublicPolicy) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PublicPolicy = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PublicPolicy)
                Dim status As Integer

                If publicPolicy.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    publicPolicy.CreationUser = audit.CodeUser
                    publicPolicy.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = publicPolicy.OriginalValue
                    publicPolicy.ModificationUser = audit.CodeUser
                    publicPolicy.ModificationDate = DateTime.Now
                    publicPolicy.MarkAsModified
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If
                Me._publicPolicyRepository.SaveEntity(publicPolicy)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PublicPolicy)(publicPolicy, audit, status, auxObjEntity)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of PublicPolicy) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = publicPolicy, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PublicPolicy) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PublicPolicy) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado a le entidad
    ''' </summary>
    ''' <param name="PublicPolicyId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePublicPolicy(PublicPolicyId As Integer, audit As AuditMessage) As ActionResult Implements IPublicPolicyAdminService.ChangeStatePublicPolicy

        If String.IsNullOrEmpty(PublicPolicyId) Then
            Throw New ArgumentNullException("EntityId")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim uow As IUnitWork = Me._publicPolicyRepository.UnitWork
                Dim publicPolicy As PublicPolicy = Me._publicPolicyRepository.FirstOrDefault(Function(m) m.Id = PublicPolicyId, False)
                If publicPolicy IsNot Nothing AndAlso publicPolicy.Id > 0 Then
                    publicPolicy.Status = 0
                    publicPolicy.MarkAsModified
                End If
                Me._publicPolicyRepository.SaveEntity(publicPolicy)
                uow.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("UpdateState")}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _publicPolicyRepository = Nothing
            _sequenseBudgetDRepository = Nothing
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
