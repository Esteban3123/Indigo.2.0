'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Sergio Fernandez
' Created          : 2014-10-07
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
#End Region
Public Class CompanySettingsRepository
    Inherits GenericRepository(Of CompanySettings)
    Implements ICompanySettingsRepository, Inject
    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region


#Region "functions"
    ''' <summary>
    ''' Funcion para obtener los parametros de la empresa
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompanySettings(Optional ByVal AsNoTracking As Boolean = False) As CompanySettings Implements ICompanySettingsRepository.GetCompanySettings
        Dim query As IQueryable(Of CompanySettings) = If(AsNoTracking,
            _context.CompanySettings.AsNoTracking(),
            _context.CompanySettings)

        Dim first = query.FirstOrDefault()
        If first Is Nothing Then
            Return New CompanySettings
        End If

        Dim currency = (From c In _context.Currency.AsNoTracking()
                        Where c.Id = first.OfficialCurrencyId
                        Select c).FirstOrDefault()
        first.CurrencyCodName = $"{currency?.Code} - {currency?.Name}"

        If first.ProfitLostByExchangeCurrencyAccountId IsNot Nothing Then
            Dim profitAccount = (From a In _context.MainAccounts.AsNoTracking()
                                 Where a.Id = first.ProfitLostByExchangeCurrencyAccountId
                                 Select a).FirstOrDefault()
            first.ProfitAccountName = $"{profitAccount?.Number} - {profitAccount?.Name}"
        End If

        If first.LostByExchangeCurrencyAccountId IsNot Nothing Then
            Dim lostAccount = (From a In _context.MainAccounts.AsNoTracking()
                               Where a.Id = first.LostByExchangeCurrencyAccountId
                               Select a).FirstOrDefault()
            first.LostAccountName = $"{lostAccount?.Number} - {lostAccount?.Name}"
        End If

        If first.ProfitLostJournalVoucherTypeId IsNot Nothing Then
            Dim journalName = (From x In _context.JournalVoucherTypes.AsNoTracking()
                               Where x.Id = first.ProfitLostJournalVoucherTypeId
                               Select x).FirstOrDefault()
            first.ProfitJournalVoucherName = String.Format("{0} - {1}", journalName?.Code, journalName?.Name)
        End If

        Return first
    End Function

    ''' <summary>
    ''' Indica si ya existen cuentas por pagar radicadas
    ''' </summary>
    Public Function HasAccountPayable() As Boolean Implements ICompanySettingsRepository.HasAccountPayable
        Return _context.AccountPayable.AsNoTracking().Any()
    End Function
#End Region

End Class
