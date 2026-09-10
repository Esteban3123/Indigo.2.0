Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Globalization
Imports Domain.Common.Entities
Imports Domain.Base

Public Class CurrencyRepository
    Inherits GenericRepository(Of Currency)
    Implements ICurrencyRepository, Inject


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetCurrency(code As String) As Currency Implements ICurrencyRepository.GetCurrency
        Dim _Currency = (From e In _context.Currency.Include("TRM").Include("TRM1").Include("ISO4217") Where e.Code = code Select e)
        If (_Currency.Count > 0) Then
            _Currency.Single().OriginalValue = (From e In _context.Currency.AsNoTracking()
                                                Where e.Code = code
                                                Select e).FirstOrDefault()
            Return _Currency.Single()
        Else
            Return New Currency()
        End If
    End Function

    ''' <summary>
    ''' Devuelve una Moneda por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del Moneda</param>
    ''' <returns>El Moneda</returns>
    ''' <remarks></remarks>
    Public Function GetCurrencyById(ByVal idCurrency As Integer, Optional tracking As Boolean = True) As Currency Implements ICurrencyRepository.GetCurrencyById
        If tracking Then
            Dim _Currency = From e In _context.Currency.Include("ISO4217")
                            Where e.Id = idCurrency
                            Select e

            If _Currency.Count > 0 Then
                Return _Currency.Single
            Else
                Return New Currency
            End If
        Else
            Dim _Currency = From e In _context.Currency.AsNoTracking
                            Where e.Id = idCurrency
                            Select e

            If _Currency.Count > 0 Then
                Return _Currency.Single
            Else
                Return New Currency
            End If
        End If

    End Function

    Public Function ListAllCurrency() As List(Of Currency) Implements ICurrencyRepository.ListAllCurrency
        Dim Busqueda = From e In _context.Currency
                       Select e
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el Trm de la moneda con respecto a la oficial del la compañia
    ''' </summary>
    ''' <param name="CurrencyId"></param>
    ''' <returns></returns>
    Public Function GetTRMbyCurrencyId(CurrencyId As Integer, Optional DateTrm As Date? = Nothing) As TRM Implements ICurrencyRepository.GetTRMbyCurrencyId
        If DateTrm Is Nothing Then
            DateTrm = DateTime.Now
        End If
        DateTrm = DateTrm?.ToString("d", CultureInfo.CurrentCulture)
        Dim _tRM = (From e In _context.TRM
                    Join c In _context.CompanySettings On e.OfficialCurrencyId Equals (c.OfficialCurrencyId)
                    Where e.CurrencyId = CurrencyId And e.MeasurementDate = DateTrm
                    Order By e.MeasurementDate Descending
                    Select e)
        Return _tRM.FirstOrDefault()
    End Function

    Public Function GetCurrencyByAbbreviation(code As String) As Currency Implements ICurrencyRepository.GetCurrencyByAbbreviation
        Dim _Currency = (From e In _context.Currency.Include("TRM").Include("TRM1") Where e.Abbreviation = code Select e)
        If (_Currency.Count > 0) Then
            _Currency.Single().OriginalValue = (From e In _context.Currency.AsNoTracking()
                                                Where e.Code = code
                                                Select e).FirstOrDefault()
            Return _Currency.Single()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CurrencyId"></param>
    ''' <param name="DateTrm"></param>
    ''' <returns></returns>
    Public Function GetEspecificModuleTMR(CurrencyId As Integer, Optional DateTrm As Date? = Nothing) As TRM Implements ICurrencyRepository.GetEspecificModuleTMR
        If DateTrm Is Nothing Then
            DateTrm = DateTime.Now
        End If

        DateTrm = DateTrm?.ToString("d", CultureInfo.CurrentCulture)
        Dim customTRM = (From e In _context.CustomTRM
                         Join c In _context.CompanySettings On e.OfficialCurrencyId Equals (c.OfficialCurrencyId)
                         Where e.CurrencyId = CurrencyId AndAlso DateTrm >= e.InitialMeasurementDate AndAlso DateTrm <= e.FinalMeasurementDate
                         Order By e.Id Descending
                         Select e)?.FirstOrDefault()

        If customTRM Is Nothing Then
            Return Nothing
        End If

        Return New TRM With {.MeasurementDate = DateTrm,
                                .CurrencyId = customTRM.CurrencyId,
                                .OfficialCurrencyId = customTRM.OfficialCurrencyId,
                                .Value = customTRM.Value,
                                .ValueOfficialToCurrency = customTRM.ValueOfficialToCurrency}
    End Function

    ''' <summary>
    ''' this function get the TRM value in database to do always a division operation
    ''' </summary>
    ''' <param name="fromCurrencyId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="entityName"></param>
    ''' <param name="dateTrm"></param>
    ''' <returns></returns>
    Public Function GetJustTRMToMakeDivision(fromCurrencyId As Integer, toCurrencyId As Integer, Optional entityName As String = Nothing, Optional dateTrm As Date? = Nothing) As CurrencyExchangeRate Implements ICurrencyRepository.GetJustTRMToMakeDivision
        If dateTrm Is Nothing Then
            dateTrm = DateTime.Now
        End If

        Dim OfficialCurrencyId = _context.CompanySettings.AsNoTracking().FirstOrDefault.OfficialCurrencyId
        Dim flagDirectConversion = (OfficialCurrencyId = fromCurrencyId OrElse OfficialCurrencyId = toCurrencyId)

        Dim tRMCurrencyInit As TRM = Nothing
        Dim tRMValue As Decimal = 0

        tRMCurrencyInit = If(NameOf(Invoice) = entityName,
                            GetEspecificModuleTMR(If(OfficialCurrencyId = fromCurrencyId, toCurrencyId, fromCurrencyId), dateTrm),
                            GetTRMbyCurrencyId(If(OfficialCurrencyId = fromCurrencyId, toCurrencyId, fromCurrencyId), dateTrm))

        If tRMCurrencyInit Is Nothing Then
            Return New CurrencyExchangeRate With {.FromCurrencyId = fromCurrencyId,
                                                .ToCurrencyId = toCurrencyId,
                                                .TRMValue = tRMValue,
                                                .Value = tRMValue}
        End If

        tRMValue = SelectedTRM(OfficialCurrencyId, fromCurrencyId, tRMCurrencyInit.Value, tRMCurrencyInit.ValueOfficialToCurrency)

        'Conversion Indirecta
        If Not flagDirectConversion Then
            Dim tRMCurrencyEnd = If(NameOf(Invoice) = entityName, GetEspecificModuleTMR(toCurrencyId, dateTrm), GetTRMbyCurrencyId(toCurrencyId, dateTrm))
            Dim tRMValueEnd = SelectedTRM(OfficialCurrencyId, toCurrencyId, tRMCurrencyInit.Value, tRMCurrencyInit.ValueOfficialToCurrency)
            tRMValue /= tRMValueEnd
        End If

        Return New CurrencyExchangeRate With {.FromCurrencyId = fromCurrencyId,
                                                .ToCurrencyId = toCurrencyId,
                                                .TRMValue = tRMValue,
                                                .Value = tRMValue}
    End Function

    ''' <summary>
    ''' this private function obtain the TRM value to make a division operation
    ''' </summary>
    ''' <param name="officialCurrencyId"></param>
    ''' <param name="fromCurrencyId"></param>
    ''' <param name="value"></param>
    ''' <param name="valueReverse"></param>
    ''' <returns></returns>
    Private Function SelectedTRM(officialCurrencyId As Integer, fromCurrencyId As Integer, value As Decimal, valueReverse As Decimal) As Decimal
        If valueReverse > value Then
            If fromCurrencyId = officialCurrencyId Then
                Return (1 / valueReverse)
            Else
                Return valueReverse
            End If
        Else
            If fromCurrencyId = officialCurrencyId Then
                Return value
            Else
                Return (1 / value)
            End If
        End If
    End Function

End Class
