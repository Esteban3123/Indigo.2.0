Imports Domain.Entities
Public Class AddFixedAssetEntryItem
    Inherits EventArgs

    ''' <summary>
    ''' Detalle equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FixedAssetEntryItem As FixedAssetEntryItem

    ''' <summary>
    ''' Si esta en modo edición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EditMode As Boolean

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Property ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)

    ''' <summary>
    ''' Listado que se devuelve al form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListFixedAssetEntryItem As List(Of FixedAssetEntryItem)

End Class
