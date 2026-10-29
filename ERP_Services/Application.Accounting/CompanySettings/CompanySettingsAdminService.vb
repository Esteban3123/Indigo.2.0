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
            Dim savedSettings As CompanySettings = settings

            If settings.Id = 0 Then
                settings.StartTracking()
                settings.CreationDate = DateTime.Now
                settings.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Me._Repository.SaveEntity(settings)
                savedSettings = settings
            Else
                auxCompanySettings = _Repository.GetCompanySettings(True)
                Dim currentSettings = _Repository.FirstOrDefault(Function(x) x.Id = settings.Id, True, Nothing)
                If currentSettings Is Nothing OrElse currentSettings.Id = 0 Then
                    settings.StartTracking()
                    settings.ModificationDate = DateTime.Now
                    settings.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    Me._Repository.SaveEntity(settings)
                    savedSettings = settings
                Else
                    ApplyIncomingCompanySettings(settings, currentSettings)
                    currentSettings.ModificationDate = DateTime.Now
                    currentSettings.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    savedSettings = currentSettings
                End If
            End If

            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CompanySettings)(savedSettings, audit, status, auxCompanySettings)
            auditProcess.Execute()
            savedSettings.MarkAsUnchanged()
            Return New ActionResult(Of CompanySettings) With {.StateResult = True, .ObjectEmbbeded = savedSettings}
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
    ''' Copia solo propiedades escalares sobre la fila rastreada por EF.
    ''' No copia Id ni TimeStamp: TimeStamp es el token de concurrencia (ROWVERSION)
    ''' y el grafo STE que llega por WCF (Currency/Cuentas incluidas en el GET)
    ''' suele traer un original inválido o desactualizado, lo que provoca
    ''' DbUpdateConcurrencyException (0 filas) al hacer ApplyChanges + SaveChanges.
    ''' </summary>
    Private Shared Sub ApplyIncomingCompanySettings(source As CompanySettings, target As CompanySettings)
        target.LastClosingDate = source.LastClosingDate
        target.Consolidate = source.Consolidate
        target.ConsolidateDate = source.ConsolidateDate
        target.SMLV = source.SMLV
        target.UVT = source.UVT
        target.ConsecutiveFiling = source.ConsecutiveFiling
        target.BusinessLine = source.BusinessLine
        target.WorkIncomeControl = source.WorkIncomeControl
        target.OfficialCurrencyId = source.OfficialCurrencyId
        target.SalePriceIncludeTax = source.SalePriceIncludeTax
        target.ThirdPartyCheckDigit = source.ThirdPartyCheckDigit
        target.ProfitLostByExchangeCurrencyAccountId = source.ProfitLostByExchangeCurrencyAccountId
        target.ProfitLostJournalVoucherTypeId = source.ProfitLostJournalVoucherTypeId
        target.LostByExchangeCurrencyAccountId = source.LostByExchangeCurrencyAccountId
        target.CostCenterId = source.CostCenterId
        target.TaxRegistration = source.TaxRegistration
        target.TransactionEconomicActivity = source.TransactionEconomicActivity
        target.AppliesCatalogPropertyandServices = source.AppliesCatalogPropertyandServices
    End Sub

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
