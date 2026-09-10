Imports Domain.Base

Public Interface IFixedAssetEntryItemRepository
    Inherits IRepository(Of FixedAssetEntryItem)

    ''' <summary>
    ''' Obtiene un ingreso de artículo por el ID del artículo
    ''' </summary>
    ''' <returns></returns>
    Function GetFixedAssetEntryItemByItemId(itemId As Integer) As List(Of FixedAssetEntryItem)


End Interface
