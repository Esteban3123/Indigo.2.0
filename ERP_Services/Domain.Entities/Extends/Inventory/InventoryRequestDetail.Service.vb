#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class InventoryRequestDetail

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
    ''' propiedad que contiene el tipo de item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComponentType As Byte

    ''' <summary>
    ''' propiedad que contiene la descripción del tipo de item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ComponentTypeName As String

    ''' <summary>
    ''' propiedad que contiene el Id del item dependiendo del ComponentType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemId As Integer

End Class
