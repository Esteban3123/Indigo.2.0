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

                Me._Repository.SaveEntity(settings)
                UnitOfWork.Commit()
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxCompanySettings = _Repository.GetCompanySettings(True)

                'Concurrencia de negocio: otro usuario guardó la configuración después de que este la cargó.
                'No se usa el TimeStamp porque los SP de radicación de cuentas por pagar actualizan ConsecutiveFiling en esta misma fila.
                If Not Nullable.Equals(auxCompanySettings.ModificationDate, settings.ModificationDate) Then
                    Return New ActionResult(Of CompanySettings) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
                End If

                'Se actualiza la fila vigente (TimeStamp y ConsecutiveFiling actuales) solo con los campos editables de la pantalla
                Dim current As CompanySettings = _Repository.GetCompanySettings(False)
                With current
                    .LastClosingDate = settings.LastClosingDate
                    .Consolidate = settings.Consolidate
                    .ConsolidateDate = settings.ConsolidateDate
                    .SMLV = settings.SMLV
                    .UVT = settings.UVT
                    .BusinessLine = settings.BusinessLine
                    .WorkIncomeControl = settings.WorkIncomeControl
                    .SalePriceIncludeTax = settings.SalePriceIncludeTax
                    .OfficialCurrencyId = settings.OfficialCurrencyId
                    .ProfitLostByExchangeCurrencyAccountId = settings.ProfitLostByExchangeCurrencyAccountId
                    .ProfitLostJournalVoucherTypeId = settings.ProfitLostJournalVoucherTypeId
                    .ThirdPartyCheckDigit = settings.ThirdPartyCheckDigit
                    .LostByExchangeCurrencyAccountId = settings.LostByExchangeCurrencyAccountId
                    .CostCenterId = settings.CostCenterId
                    .TaxRegistration = settings.TaxRegistration
                    .TransactionEconomicActivity = settings.TransactionEconomicActivity
                    .AppliesCatalogPropertyandServices = settings.AppliesCatalogPropertyandServices

                    'El consecutivo de radicación solo es editable mientras no existan cuentas por pagar
                    If Not _Repository.HasAccountPayable() Then
                        .ConsecutiveFiling = settings.ConsecutiveFiling
                    End If

                    'Truncado a segundos: SQL datetime redondea los milisegundos y el cliente compara esta fecha en el siguiente guardado
                    Dim now As DateTime = DateTime.Now
                    .ModificationDate = New DateTime(now.Ticks - (now.Ticks Mod TimeSpan.TicksPerSecond), now.Kind)
                    .ModificationUser = audit.CodeUser
                End With

                UnitOfWork.Commit()

                'Se devuelven al cliente los valores vigentes para que un segundo guardado en la misma pantalla no choque
                settings.ConsecutiveFiling = current.ConsecutiveFiling
                settings.ModificationDate = current.ModificationDate
                settings.ModificationUser = current.ModificationUser
                settings.TimeStamp = current.TimeStamp
            End If

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
