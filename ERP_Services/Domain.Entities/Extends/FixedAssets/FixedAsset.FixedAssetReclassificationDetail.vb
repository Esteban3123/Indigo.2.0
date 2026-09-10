#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetReclassificationDetail

    ''' <summary>
    ''' Catalogo Id
    ''' </summary>
    <DataMember()>
    Public Property ItemCatalogId As Integer

    ''' <summary>
    ''' Item Id
    ''' </summary>
    <DataMember()>
    Public Property ItemId As Integer

    ''' <summary>
    ''' Item Codigo y Descripción
    ''' </summary>
    <DataMember()>
    Public Property ItemCodeDescription As String

    ''' <summary>
    ''' Activo Serie
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetSerie As String

    ''' <summary>
    ''' Activo Placa
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetPlate As String

    ''' <summary>
    ''' Activo Estado
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetStatus As Byte

    ''' <summary>
    ''' Activo Tipo de Adquisición Real
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetAdquisitionTypeReal As Byte

    ''' <summary>
    ''' Activo ha sido reclasificado
    ''' </summary>
    <DataMember()>
    Public Property PhysicalAssetHasReclassified As Boolean

End Class
