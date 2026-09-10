#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class TransferOrderDetail

    ''' <summary>
    ''' propiedad que contiene el codigo de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Code As String

    ''' <summary>
    ''' propiedad que contiene el codigo y nombre del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionProduct As String

    ''' <summary>
    ''' propiedad que contiene la cantidad a importar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property QuantityImport As Integer

    ''' <summary>
    ''' propiedad que contiene la unidad de consumo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property ConsumptionUnit As String

    ''' <summary>
    ''' propiedad que contiene el costo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property CostProduct As Decimal

    ''' <summary>
    ''' propiedad que contiene el tipo de item (1 - Medicamento. 2- Insumo. 3- Producto)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property ComponentType As Byte

    ''' <summary>
    ''' propiedad que contiene el Id del item dependiendo del ComponentType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemId As Integer

End Class