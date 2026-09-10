Imports System.Runtime.Serialization

Partial Public Class InventoryContractDetail

    <DataMember()>
    Public Property GroupCodeName As String

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCode As String

    ''' <summary>
    ''' Descripcion del tipo de producto
    ''' </summary>
    <DataMember()>
    Public Property ProductName As String

    <DataMember>
    Property Code As String

    <DataMember>
    Property CodeNameProduct As String

    Property Activated As Boolean
    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en remision de entrada)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInvalid As Boolean

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' abreviacion de la moneda que esta en la cabecera
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Id de la moneda de la cabecera
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CurrencyId As Integer
End Class
