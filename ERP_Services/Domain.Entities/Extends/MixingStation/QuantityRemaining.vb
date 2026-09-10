Imports System.Runtime.Serialization

Partial Public Class QuantityRemaining

    ''' <summary>
    ''' Obtiene o establece la entregada a la campaña
    ''' </summary>
    <DataMember()>
    Public Property DeliveredQuantity As Integer

    ''' <summary>
    ''' Obtiene O establece el codigo del lote
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property BatchSerialCode As String

    ''' <summary>
    ''' Obtienen o establece la fecha de expiracion de un producto 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ExpirationDate As String

    ''' <summary>
    ''' Obtiene o Establece el nombre del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ProductFullName As String

    ''' <summary>
    ''' Obtiene o Establece el Id de la unidad de medida del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property UnitMeasurementId As String

    <DataMember()>
    Public Property CampaignDetailNumber As String

    ''' <summary>
    ''' Cantidad para aprovechar
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property QuantityHarnessed As Decimal

    ''' <summary>
    ''' propiedad que guarda el nombre y codigo del almacen de remanente
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RemnantFullName As String

    ''' <summary>
    ''' propiedad que guarda la estabilidad del producto con la unidad de tiempo correspondiente
    ''' </summary>
    ''' <returns></returns>
    Public Property StabilityWithTimeUnit As String

    ''' <summary>
    ''' propiedad que guarda la concentración del producto con la unidad de medida correspondiente
    ''' </summary>
    ''' <returns></returns>
    Public Property ConcentrationWithMeasurementUnit As String

    ''' <summary>
    ''' propiedad que guarda el volumen máximo que puede tener un remanente
    ''' </summary>
    ''' <returns></returns>
    Public Property MaximumVolume As Decimal

    ''' <summary>
    ''' propiedad que guarda la fecha de vencimiento de la estabilidad
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property VctoStability As Date
End Class
