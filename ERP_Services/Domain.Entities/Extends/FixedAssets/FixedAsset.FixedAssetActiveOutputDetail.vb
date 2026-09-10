#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetActiveOutputDetail

    ''' <summary>
    ''' Activo
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetDescription As String

    ''' <summary>
    ''' Serie del articulo
    ''' </summary>
    <DataMember()>
    Public Property MainAccountNumberName As String

    ''' <summary>
    ''' Placa del articulo
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyNitName As String

    ''' <summary>
    ''' Parte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PhysicalAssetPartDescription As String

    <DataMember()>
    Public Property LowTypeName As String

    <DataMember()>
    Public Property HistoricalValue As Decimal

    <DataMember()>
    Public Property ClassificationFixedAsset As Integer

End Class
