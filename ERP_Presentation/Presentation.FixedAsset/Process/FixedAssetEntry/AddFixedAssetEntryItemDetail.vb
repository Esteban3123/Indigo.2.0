Imports Domain.Entities
Public Class AddFixedAssetEntryItemDetail
    Inherits EventArgs

    ''' <summary>
    ''' Detalle equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FixedAssetEntryItemDetail As FixedAssetEntryItemDetail

    ''' <summary>
    ''' Listado de detalle de equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)

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

End Class
