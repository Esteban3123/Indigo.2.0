Imports System.Runtime.Serialization

Partial Public Class EntranceVoucherDetailBatchSerial

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Serial o lote del detalle
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CodeBatchSerial As String

    ''' <summary>
    ''' Causa de la devolución
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property DevolutionCauseId As Integer?

    ''' <summary>
    ''' Cantidad a devolver
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DevolutionQuantity As Decimal

    ''' <summary>
    ''' Valor unitario definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property UnitValueProduct As Decimal

    ''' <summary>
    ''' Subtotal es el subtotal del producto definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property SubTotalValueProduct As Decimal

    ''' <summary>
    ''' Porcentaje de descuento definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DiscountPercentageProduct As Decimal

    ''' <summary>
    ''' Valor Total descuento definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DiscountValueProduct As Decimal

    ''' <summary>
    ''' Porcentaje de Iva definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IvaPercentageProduct As Decimal

    ''' <summary>
    ''' Valor Total Iva definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IvaValueProduct As Decimal

    ''' <summary>
    ''' Porcentaje de retención en la fuente definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RtfPercentageProduct As Decimal

    ''' <summary>
    ''' Valor Total Retefuente definido en el detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RtfValueProduct As Decimal

End Class
