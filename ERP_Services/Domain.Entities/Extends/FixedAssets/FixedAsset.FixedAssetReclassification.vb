#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetReclassification

    ''' <summary>
    ''' Catalogo previo Codigo y Descripción
    ''' </summary>
    <DataMember()>
    Public Property ItemCatalogPreviousCodeDescription As String

    ''' <summary>
    ''' Item previo Codigo y Descripción
    ''' </summary>
    <DataMember()>
    Public Property ItemPreviousCodeDescription As String

    ''' <summary>
    ''' Catalogo Codigo y Descripción
    ''' </summary>
    <DataMember()>
    Public Property ItemCatalogCodeDescription As String

    ''' <summary>
    ''' Item Codigo y Descripción
    ''' </summary>
    <DataMember()>
    Public Property ItemCodeDescription As String

End Class
