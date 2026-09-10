'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Sergio Abraham Fernande Cruz
' Created          : 15-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Domain.Security.Entities
Imports Infrastructure.Data.ModelRepository

#End Region
Public Class SettingAccountAdminService
    Implements ISettingAccountAdminService
    Private _Repository As ISettingsAccountRepository

#Region "Builder"
    Public Sub New(ByVal Repository As ISettingsAccountRepository)

        _Repository = Repository
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Funmcion para obtener el parametro contable
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingAccount(ByVal idOperationUnit As Integer) As GeneralLedgerSettings Implements ISettingAccountAdminService.GetSettingAccount
        Return _Repository.GetSettingAccount(idOperationUnit)
    End Function

    ''' <summary>
    ''' Funcion para guardar el parametro contable
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">folio vacio Vacia</exception>
    Public Function SaveSettingAccount(ByVal settingAccount As GeneralLedgerSettings, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of GeneralLedgerSettings) Implements ISettingAccountAdminService.SaveSettingAccount
        If settingAccount Is Nothing Then
            Throw New ArgumentNullException("parametro contable Vacio")
        End If
        Dim settingUnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of GeneralLedgerSettings)
            Dim status As Integer
            Dim AuxSetting As GeneralLedgerSettings = Nothing
            If settingAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
                settingAccount.CreationDate = DateTime.Now
                settingAccount.CreationUser = audit.CodeUser
            Else
                settingAccount.ModificationDate = DateTime.Now
                settingAccount.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxSetting = settingAccount.OriginalValue
            End If

            Me._Repository.SaveEntity(settingAccount)
            settingUnitOfWork.Commit()

            auditProcess = New IndigoAuditSimpleEntity(Of GeneralLedgerSettings)(settingAccount, audit, status, AuxSetting)
            auditProcess.Execute()
            Return New ActionResult(Of GeneralLedgerSettings) With {.StateResult = True, .ObjectEmbbeded = settingAccount}
        Catch ex As OptimisticConcurrencyException
            settingUnitOfWork.RollbackChanges()
            Return New ActionResult(Of GeneralLedgerSettings) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            settingUnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralLedgerSettings) With {.StateResult = False}
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
            Me._Repository = Nothing
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
