Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class EntranceVoucherDevolutionDetail

    ''' <summary>
    ''' Nombre del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Id del detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property EntranceVoucherDetailId As Integer

    ''' <summary>
    ''' Lote del producto
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property BatchSerialId As Integer?

    ''' <summary>
    ''' Cantidad inicial del detalle del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property InitialQuantity As Integer

    ''' <summary>
    ''' Cantidad disponiple a devolver
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property OutstandingQuantity As Integer

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
    ''' Subtotal es el valor unitario por la cantidad a devolver
    ''' UnitValue * Quantity
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property SubTotalValue As Decimal

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
    ''' Es el decuento unitario por la cantidad a devolver
    ''' UnitDiscountValue * Quantity
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property DiscountValue As Decimal

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
    ''' Es el valor unitario del iva por la cantidad a devolver
    ''' UnitIvaValue * Quantity
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IvaValue As Decimal

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

    ''' <summary>
    ''' Es el valor unitario de retención por la cantidad a devolver
    ''' UnitRtfValue * Quantity
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property RtfValue As Decimal

End Class
