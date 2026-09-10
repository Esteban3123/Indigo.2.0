#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetDepreciationDetail

    ''' <summary>
    ''' Libro
    ''' </summary>
    <DataMember()>
    Public Property LegalBookDescription As String

    ''' <summary>
    ''' Activo
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetDescription As String

    ''' <summary>
    ''' Serie
    ''' </summary>
    <DataMember()>
    Public Property SeriePhysical As String

    ''' <summary>
    ''' Placa
    ''' </summary>
    <DataMember()>
    Public Property PlatePhysical As String

    ''' <summary>
    ''' Modelo
    ''' </summary>
    <DataMember()>
    Public Property ModelPhysical As String

    ''' <summary>
    ''' Valor histórico
    ''' </summary>
    <DataMember()>
    Public Property HistoricalValuePhysical As Decimal

    ''' <summary>
    ''' Marca
    ''' </summary>
    <DataMember()>
    Public Property TrademarkPhysical As String

End Class
