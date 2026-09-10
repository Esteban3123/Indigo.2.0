Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class EntranceVoucherDevolution

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property DescriptionWarehouse As String

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property CodeEntranceVoucher As String

    ''' <summary>
    ''' Prefijo para la secuencia si se opta por secuencias por almacén
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Prefix As String

    ''' <summary>
    ''' Porcentaje de Retencion de Iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property WithholdingTaxPercentaje As Decimal

    ''' <summary>
    ''' Porcentaje de retencion de Ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property WithholdingIcaPercentage As Decimal

    ''' <summary>
    ''' Bandera que define si la devolcion es parcial o total
    ''' </summary>
    ''' <value>Total = true, Parcial = false</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DevolutionType As Boolean

    ''' <summary>
    ''' Valor de redondeo
    ''' </summary>
    ''' <value>Total = true, Parcial = false</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RoundingValue As Integer

End Class
