Imports Domain.Entities
Public Class AddFixedAssetInitialBalanceItemParts
    Inherits EventArgs

    ''' <summary>
    ''' Detalle d ela parte del equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FixedAssetInitialBalanceItemParts As FixedAssetInitialBalanceItemParts

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
    Property ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook)

End Class
