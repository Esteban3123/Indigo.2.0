Imports Domain.Entities

Public Class AddEquipmentCatalogEventArgs

    Inherits EventArgs

    Property ListFixedAssetEquipmentCatalogDetail As List(Of FixedAssetItemCatalogDetail)

    Property FixedAssetItemCatalogDetail As FixedAssetItemCatalogDetail

    Property EditMode As Boolean

    Property ImportDataMode As Boolean

End Class
