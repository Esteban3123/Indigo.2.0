Imports System.Runtime.Serialization

Partial Public Class PurchaseOrderDetail

    ''' <summary>
    ''' Bandera para saber si el item esta activo
    ''' </summary>
    ''' <returns></returns>
    Property Activated As Boolean

    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en remision de entrada)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInvalid As Boolean

    ''' <summary>
    ''' Código del documentos
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property Code As String

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Grupo del Producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property GroupCodeName As String

    ''' <summary>
    ''' Código del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ProductCode As String

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ProductName As String

    ''' <summary>
    ''' Código y Nombre del Producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeNameProduct As String

    ''' <summary>
    ''' Fabricante
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ManufacturerName As String

    ''' <summary>
    ''' Registro Sanitario
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property HealthRegistration As String

    ''' <summary>
    ''' Presentación
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Presentation As String

    ''' <summary>
    ''' Fecha de entrega de la orden de compra
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property DeliveredDate As DateTime

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


    ''' <summary>
    ''' valor del iva total del detalle
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property TotalIva As Decimal


End Class
