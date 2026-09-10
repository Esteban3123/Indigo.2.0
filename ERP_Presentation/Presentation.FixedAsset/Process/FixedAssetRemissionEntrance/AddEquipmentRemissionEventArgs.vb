Imports Domain.Entities

Public Class AddEquipmentRemissionEventArgs

    Inherits EventArgs

    Property ListFixedAssetRemissionEntranceItem As List(Of FixedAssetRemissionEntranceItem)

    Property InputRemissionEquipment As FixedAssetRemissionEntranceItem

    Property EditMode As Boolean

End Class
