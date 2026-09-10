Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface ICurrencyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los paises.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllCurrency() As List(Of Currency)

    ''' <summary>
    ''' Elimina un pais
    ''' </summary>
    ''' <param name="Currency">el pais</param>
    ''' <returns></returns>
    Function DeleteCurrency(ByVal Currency As Currency, ByVal audit As AuditMessage) As ActionMessageResult(Of Currency)
    ''' <summary>
    ''' graba un Pais
    ''' </summary>
    ''' <param name="Currency">el Pais</param>
    ''' <returns></returns>
    Function SaveCurrency(ByVal Currency As Currency, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Currency)
    ''' <summary>
    ''' consulta un Pais
    ''' </summary>
    ''' <param name="code">el codigo del pais</param>
    ''' <returns></returns>
    Function GetCurrency(ByVal code As String) As Currency

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Function GetCurrencyById(ByVal idCurrency As Integer, ByVal audit As AuditMessage) As Currency

    ''' <summary>
    ''' Actualiza el Estado de un IVA
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateCurrency(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Currency)

    ''' <summary>
    ''' consulta la tasa de cambio con respecto a la moneda oficial
    ''' </summary>
    ''' <param name="ToCurrencyId"></param>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, session As SessionValues, Optional DateTrm As Date? = Nothing, Optional entityName As String = Nothing) As ActionResult(Of TRM)


End Interface
