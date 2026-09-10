'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Class MonthlyAmortizationRepository
    Inherits GenericRepository(Of DeferredCausation)
    Implements IMonthlyAmortizationRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork, companySettingsRepository As ICompanySettingsRepository)
        MyBase.New(context)
        _context = context
        Me._companySettingsRepository = companySettingsRepository
    End Sub

    ''' <summary>
    ''' Obtiene el listado de causaciones diferidas por la fecha
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByDate(year As Integer, month As Integer, Optional tracking As Boolean = True) As List(Of DeferredCausationShare) Implements IMonthlyAmortizationRepository.GetDeferredCausationByDate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim officialCurrency = _companySettingsRepository.FirstOrDefault(Function(x) True, False, {"Currency"})?.Currency
        Dim res = (From dcs In _context.DeferredCausationShare.AsNoTracking
                   Join dc In _context.DeferredCausation.AsNoTracking On dc.Id Equals dcs.DeferredCausationId
                   Where dc.Status = 2 And dcs.PaymentMonth = month And dcs.PaymentYear = year And dcs.Amortized = False And dcs.Value > 0
                   Select dcs).ToList

        If res.Count > 0 Then

            For Each item As DeferredCausationShare In res
                Dim deferredCausation = (From d In _context.DeferredCausation.AsNoTracking Where d.Id = item.DeferredCausationId Select d).FirstOrDefault
                Dim AccountPayable = (From d In _context.AccountPayable.AsNoTracking.Include("Currency").AsNoTracking Where d.Id = deferredCausation.IdAccountPayable Select d).FirstOrDefault
                item.IdThirdParty = deferredCausation.IdThirdParty
                Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = item.IdThirdParty Select t).FirstOrDefault
                item.ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name

                item.IdMainAccount = deferredCausation.IdMainAccount
                Dim account = (From a In _context.MainAccounts.AsNoTracking Where a.Id = item.IdMainAccount Select a).FirstOrDefault
                item.MainAccountDescription = account.Number + " - " + account.Name

                If deferredCausation.IdCostCenter IsNot Nothing Then
                    item.IdCostCenter = deferredCausation.IdCostCenter
                Else
                    item.IdCostCenter = Nothing
                End If

                item.Periods = deferredCausation.PeriodsNumber
                item.BillNumber = deferredCausation.BillNumber
                item.DatePeriod = DateTime.Now
                If String.IsNullOrEmpty(AccountPayable?.Currency?.Abbreviation) Then
                    item.CurrencyAbbreviation = officialCurrency?.Abbreviation
                Else
                    item.CurrencyAbbreviation = AccountPayable?.Currency?.Abbreviation
                End If
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationById(id As String, Optional tracking As Boolean = True) As DeferredCausation Implements IMonthlyAmortizationRepository.GetDeferredCausationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In _context.DeferredCausation.AsNoTracking.Include("AccountPayable").AsNoTracking.Include("DeferredCausationDetails").AsNoTracking.Include("DeferredCausationShare").AsNoTracking
                   Where d.Id = id
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New DeferredCausation
        End If
    End Function

    ''' <summary>
    ''' Genera el ajuste diferencial por amortizacion mensual de diferidos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function GenerateDifferentialAdjustment(data As DataRevaluation, userCode As String) As List(Of SPResultModelDiffAdjustment) Implements IMonthlyAmortizationRepository.GenerateDifferentialAdjustment

        Dim xmlCxPDeferred = Utils.SerializeToXmlString(data)

        Return Me.ExecuteStoredProcedure(Of SPResultModelDiffAdjustment)("[Payments].[SP_DeferredCausationRevaluation]", {("@ListDeferredCausationXml", xmlCxPDeferred.ToString()),
                                                                                                                          ("@UserCode", userCode)}).ToList()
    End Function
End Class
