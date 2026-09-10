Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPCurrency

#Region "Currency"

    ''' <summary>
    ''' Obtiene todos los Monedas
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    <OperationContract()>
    Function ListAllCurrency(session As SessionValues) As List(Of Currency)

    ''' <summary>
    ''' Obtiene un Moneda especifico
    ''' </summary>
    ''' <param name="code">Codigo del Moneda</param>
    ''' <returns>Moneda</returns>
    <OperationContract()>
    Function GetCurrency(ByVal code As String, session As SessionValues) As Currency

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del Moneda</param>
    ''' <returns>El Moneda</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCurrencyById(ByVal idCurrency As Integer, session As SessionValues) As Currency

    ''' <summary>
    ''' Graba un Moneda
    ''' </summary>
    ''' <param name="Currency">Moneda a grabar</param>
    ''' <param name="session"></param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso, 0. si no lo fue</returns>
    <OperationContract()>
    Function SaveCurrency(ByVal Currency As Currency, session As SessionValues, idSequence As Long) As ActionResult(Of Currency)

    ''' <summary>
    ''' Elimina un Moneda
    ''' </summary>
    ''' <param name="Currency">Moneda que se desa eliminar</param>
    ''' <param name="session"></param>
    ''' <returns>Retorna un boleano: 1. Si es exitoso el borrado, 0. si no lo fue</returns>
    <OperationContract()>
    Function DeleteCurrency(ByVal Currency As Currency, session As SessionValues) As ActionMessageResult(Of Currency)



    <OperationContract()>
    Function UpdateCurrency(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of Currency)

    ''' <summary>
    ''' consulta la tasa de cambio con respecto a la moneda oficial
    ''' </summary>
    ''' <param name="ToCurrencyId"></param>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, session As SessionValues, Optional DateTrm As Date? = Nothing, Optional entityName As String = Nothing) As ActionResult(Of TRM)

#End Region

End Interface
