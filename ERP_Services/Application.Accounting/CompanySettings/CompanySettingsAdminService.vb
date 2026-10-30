'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Globalization
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
#End Region
Public Class CompanySettingsAdminService
    Implements ICompanySettingsAdminService

    Private _Repository As ICompanySettingsRepository

#Region "Builder"
    Public Sub New(ByVal repository As Domain.Entities.ICompanySettingsRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _Repository = repository
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Funcion para obtenetr los parametros de la empresa
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompanySettings() As CompanySettings Implements ICompanySettingsAdminService.GetCompanySettings
        Return _Repository.GetCompanySettings(True)
    End Function

    ''' <summary>
    ''' Saves the company settings.
    ''' </summary>
    ''' <param name="settings">The settings.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveCompanySettings(settings As CompanySettings, audit As AuditMessage) As ActionResult(Of CompanySettings) Implements ICompanySettingsAdminService.SaveCompanySettings
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CompanySettings)
            Dim status As Integer
            Dim auxCompanySettings As CompanySettings = Nothing

            If settings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settings.CreationDate = DateTime.Now
                settings.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                settings.ModificationDate = DateTime.Now
                settings.ModificationUser = audit.CodeUser
                auxCompanySettings = _Repository.GetCompanySettings(True)
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            DetachRelatedEntities(settings)
            Me._Repository.SaveEntity(settings)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CompanySettings)(settings, audit, status, auxCompanySettings)
            auditProcess.Execute()
            settings.MarkAsUnchanged()
            Return New ActionResult(Of CompanySettings) With {.StateResult = True, .ObjectEmbbeded = settings}
        Catch ex As DbUpdateConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of CompanySettings) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of CompanySettings) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CompanySettings) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' retorna la moneda oficial, establecida en companySettings
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrency() As Currency Implements ICompanySettingsAdminService.GetOfficialCurrency
        Try
            Dim Currency = _Repository.FirstOrDefault(Function(x) True, False, {"Currency.ISO4217"})?.Currency
            If Currency Is Nothing Then
                Return New Currency
            End If
            Return Currency
        Catch ex As Exception
            Return New Currency
        End Try
    End Function

    ''' <summary>
    ''' Quita las navegaciones del grafo STE para que ApplyChanges persista solo CompanySettings y sus FKs.
    ''' Conserva los identificadores porque el Fixup de MainAccounts/CostCenter anula el FK al asignar Nothing.
    ''' </summary>
    Private Sub DetachRelatedEntities(settings As CompanySettings)
        If settings Is Nothing Then
            Return
        End If

        Dim officialCurrencyId = settings.OfficialCurrencyId
        Dim profitAccountId = settings.ProfitLostByExchangeCurrencyAccountId
        Dim lostAccountId = settings.LostByExchangeCurrencyAccountId
        Dim journalVoucherTypeId = settings.ProfitLostJournalVoucherTypeId
        Dim costCenterId = settings.CostCenterId
        Dim trackingEnabled = settings.ChangeTracker.ChangeTrackingEnabled

        settings.StopTracking()
        settings.Currency = Nothing
        settings.MainAccounts = Nothing
        settings.MainAccounts1 = Nothing
        settings.JournalVoucherTypes = Nothing
        settings.CostCenter = Nothing
        settings.OfficialCurrencyId = officialCurrencyId
        settings.ProfitLostByExchangeCurrencyAccountId = profitAccountId
        settings.LostByExchangeCurrencyAccountId = lostAccountId
        settings.ProfitLostJournalVoucherTypeId = journalVoucherTypeId
        settings.CostCenterId = costCenterId
        RemoveOriginalNavigation(settings, "Currency")
        RemoveOriginalNavigation(settings, "MainAccounts")
        RemoveOriginalNavigation(settings, "MainAccounts1")
        RemoveOriginalNavigation(settings, "JournalVoucherTypes")
        RemoveOriginalNavigation(settings, "CostCenter")

        If trackingEnabled Then
            settings.StartTracking()
        End If
    End Sub

    Private Sub RemoveOriginalNavigation(settings As CompanySettings, navigationName As String)
        If settings.ChangeTracker.OriginalValues.ContainsKey(navigationName) Then
            settings.ChangeTracker.OriginalValues.Remove(navigationName)
        End If
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _Repository = Nothing
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
