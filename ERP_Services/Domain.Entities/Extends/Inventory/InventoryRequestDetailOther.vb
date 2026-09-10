Imports System.Runtime.Serialization

Partial Public Class InventoryRequestDetailOther

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del componente: medicamento, insumo o producto
    ''' </summary>
    <DataMember()>
    Public Property SourceCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de componente
    ''' </summary>
    <DataMember()>
    Public Property ComponentTypeName As String

    ''' <summary>
    ''' propiedad que contiene la unidad de consumo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property consumptionUnit As String

    ''' <summary>
    ''' Cantidades aprovadas desde el dashboard de solicitudes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property ApproveQuantity As Integer

End Class
