Imports Domain.Base
Imports Domain.Common.Entities

Public Interface ICurrencyRepository
    Inherits IRepository(Of Currency)

    ''' <summary>
    ''' funcion la cual retorna todos los Monedas
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllCurrency() As List(Of Currency)

    ''' <summary>
    '''  funcion el cual trae un Moneda en especifico
    ''' </summary>
    ''' <param name="code">_Codigo del Moneda</param>
    ''' <returns>Moneda</returns>
    ''' <remarks></remarks>
    Function GetCurrency(ByVal code As String) As Currency

    ''' <summary>
    ''' Devuelve una Moneda por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del Moneda</param>
    ''' <returns>El Moneda</returns>
    ''' <remarks></remarks>
    Function GetCurrencyById(ByVal idCurrency As Integer, Optional tracking As Boolean = True) As Currency

    ''' <summary>
    ''' Obtiene el Trm de la moneda con respecto a la oficial del la compañia
    ''' </summary>
    ''' <param name="CurrencyId"></param>
    ''' <returns></returns>
    Function GetTRMbyCurrencyId(CurrencyId As Integer, Optional DateTrm As Date? = Nothing) As TRM

    ''' <summary>
    '''  funcion el cual trae un Moneda en especifico filtrando por abreviacion
    ''' </summary>
    ''' <param name="code">_Codigo del Moneda</param>
    ''' <returns>Moneda</returns>
    ''' <remarks></remarks>
    Function GetCurrencyByAbbreviation(ByVal code As String) As Currency

    ''' <summary>
    ''' funcion que retorna el TRM especifico de INVOICE
    ''' </summary>
    ''' <param name="CurrencyId"></param>
    ''' <param name="DateTrm"></param>
    ''' <returns></returns>
    Function GetEspecificModuleTMR(CurrencyId As Integer, Optional DateTrm As Date? = Nothing) As TRM

    ''' <summary>
    ''' this function Obtain the TRM value to do always a division operation
    ''' </summary>
    ''' <param name="fromCurrencyId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="entityName"></param>
    ''' <param name="dateTrm"></param>
    ''' <returns></returns>
    Function GetJustTRMToMakeDivision(fromCurrencyId As Integer, toCurrencyId As Integer,
                                      Optional entityName As String = Nothing,
                                      Optional dateTrm As Date? = Nothing) As Domain.Common.Entities.CurrencyExchangeRate
End Interface
