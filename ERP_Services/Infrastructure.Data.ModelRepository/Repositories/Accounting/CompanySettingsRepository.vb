'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Sergio Fernandez
' Created          : 2014-10-07
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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
        Dim query As System.Linq.IQueryable(Of Domain.Entities.CompanySettings)

        If AsNoTracking = True Then
            query = From e In _context.CompanySettings.AsNoTracking().Include("Currency").Include("MainAccounts").AsNoTracking().Include("MainAccounts1").AsNoTracking()
                    Select e
        Else
            query = From e In _context.CompanySettings.Include("Currency").Include("MainAccounts")
                    Select e
        End If

        If query.Any() Then
            Dim first = query.FirstOrDefault()
            With first
                .CurrencyCodName = $"{first?.Currency?.Code} - {first?.Currency?.Name}"
                .ProfitAccountName = $"{first?.MainAccounts?.Number} - {first?.MainAccounts?.Name}"
                .LostAccountName = $"{first?.MainAccounts1?.Number} - {first?.MainAccounts1?.Name}"
                If first.ProfitLostJournalVoucherTypeId IsNot Nothing Then
                    Dim JournaName = (From x In _context.JournalVoucherTypes Where x.Id = first.ProfitLostJournalVoucherTypeId Select x).First
                    .ProfitJournalVoucherName = String.Format("{0} - {1}", JournaName?.Code, JournaName?.Name)
                End If
            End With
            Return first
                Else
            Return New CompanySettings
        End If
    End Function
#End Region

End Class
