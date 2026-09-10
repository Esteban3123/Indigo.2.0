'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
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
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Data.Entity.Validation
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class AccountReceivableShareAdminService
    Implements IAccountReceivableShareAdminService


#Region "Fields"
    Dim _iAccountReceivableShareRepository As IAccountReceivableShareRepository


#End Region

#Region "Builder"
    Public Sub New(ByVal iAccountReceivableShareRepository As IAccountReceivableShareRepository)
        If iAccountReceivableShareRepository Is Nothing Then
            Throw New ArgumentNullException("_IAccountReceivableShareRepository")
        End If
        _iAccountReceivableShareRepository = iAccountReceivableShareRepository
    End Sub
#End Region

    Public Function GetAccountReceivableShareById(idAccountReceivableShare As Integer) As AccountReceivableShare Implements IAccountReceivableShareAdminService.GetAccountReceivableShareById
        If idAccountReceivableShare = 0 Then
            Throw New InvalidOperationException("idAccountReceivableShare")
        End If
        Try
            Return _iAccountReceivableShareRepository.GetAccountReceivableShareById(idAccountReceivableShare)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveAccountReceivableShare(AccountReceivableShare As AccountReceivableShare, audit As AuditMessage) As ActionResult(Of AccountReceivableShare) Implements IAccountReceivableShareAdminService.SaveAccountReceivableShare
        If AccountReceivableShare Is Nothing Then
            Throw New ArgumentNullException("AccountReceivableShare")
        End If
        Dim AccountReceivableShareUnitOfWork As IUnitWork = _iAccountReceivableShareRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AccountReceivableShare)
            Dim auxAccountReceivableShare As AccountReceivableShare = Nothing
            Dim status As Integer

            If AccountReceivableShare.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxAccountReceivableShare = _iAccountReceivableShareRepository.GetAccountReceivableShareById(AccountReceivableShare.Id)
            End If

            Me._iAccountReceivableShareRepository.SaveEntity(AccountReceivableShare)
            AccountReceivableShareUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of AccountReceivableShare)(AccountReceivableShare, audit, status, auxAccountReceivableShare)
            auditProcess.Execute()

            Return New ActionResult(Of AccountReceivableShare) With {.StateResult = True, .ObjectEmbbeded = AccountReceivableShare}
        Catch ex As OptimisticConcurrencyException
            AccountReceivableShareUnitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountReceivableShare) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            AccountReceivableShareUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountReceivableShare) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _iAccountReceivableShareRepository = Nothing
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

