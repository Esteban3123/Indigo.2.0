#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class PurchaseRequestDetail

    ''' <summary>
    ''' Bandera para saber que el item que voy a importar (se usa en orden de compra)
    ''' </summary>
    ''' <returns></returns>
    Property Activated As Boolean

    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en orden de compra)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInvalid As Boolean

    ''' <summary>
    ''' propiedad que contiene el codigo de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Code As String

    ''' <summary>
    ''' fecha de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' propiedad que contiene la observacion de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Observation As String

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
    ''' propiedad que contiene el codigo y nombre del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionProduct As String

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
    ''' propiedad que contiene la cantidad a importar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property QuantityImport As Integer?

    ''' <summary>
    ''' propiedad que contiene la unidad de consumo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property consumptionUnit As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property StatusName As String



End Class
