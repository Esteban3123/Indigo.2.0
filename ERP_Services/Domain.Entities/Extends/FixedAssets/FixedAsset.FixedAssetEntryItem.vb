#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetEntryItem

    ''' <summary>
    ''' Código nombre articulo
    ''' </summary>
    <DataMember()>
    Public Property ItemCodeName As String

    ''' <summary>
    ''' Código nombre marca
    ''' </summary>
    <DataMember()>
    Public Property TrademarkCodeName As String

    ''' <summary>
    ''' Código nombre poliza
    ''' </summary>
    <DataMember()>
    Public Property PolicyCodeName As String

    ''' <summary>
    ''' Código nombre IVA
    ''' </summary>
    <DataMember()>
    Public Property IVACodeName As String

    <DataMember()>
    Public Property MinBase As Decimal

    <DataMember()>
    Public Property Rate As Decimal

    <DataMember()>
    Public Property TypeRounding As Byte

End Class
