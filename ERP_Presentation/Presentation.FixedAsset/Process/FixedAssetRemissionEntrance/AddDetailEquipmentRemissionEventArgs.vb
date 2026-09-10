Imports Domain.Entities

Public Class AddDetailEquipmentRemissionEventArgs

    Inherits EventArgs

    Property ListInputRemissionEquipmentDetail As List(Of FixedAssetRemissionEntranceItemDetail)

    Property InputRemissionEquipmentDetail As FixedAssetRemissionEntranceItemDetail

    Property EditMode As Boolean

End Class
