Imports Domain.Entities
Public Class AddValorizationDetailEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Detalle equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FixedAssetTransactionDetail As FixedAssetTransactionDetail

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
    Property ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook)

    Property ListFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook)

    Property ListFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail)



End Class
