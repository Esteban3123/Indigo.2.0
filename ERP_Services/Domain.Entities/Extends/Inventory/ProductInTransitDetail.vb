Imports System.Runtime.Serialization

Partial Public Class ProductInTransitDetail

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
    ''' Código del documento
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
    ''' Cantidad a importar
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property QuantityImport As Integer?

    ''' <summary>
    ''' Valor unitario registrado en el kardex
    ''' </summary>
    <DataMember()>
    Public Property ValueInKardex As Decimal

End Class
