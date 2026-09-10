#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetItem

    ''' <summary>
    ''' Obtiene o establece el código y nombre del catalogo de equipos
    ''' </summary>
    <DataMember()>
    Public Property CodeNameEquipmentCatalog As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del catalogo de bienes y servicios
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCatalogOfPropertyandServices As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del IVA
    ''' </summary>
    <DataMember()>
    Public Property CodeNameIVA As String

End Class
