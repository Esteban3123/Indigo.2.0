Imports Domain.Entities

Public Class AddConsignmentInventoryRemissionDetailEventArgs
    Inherits EventArgs

    Property ConsignmentInventoryRemissionDetail As ConsignmentInventoryRemissionDetail

    Property ListConsignmentInventoryRemissionDetail As List(Of ConsignmentInventoryRemissionDetail)

    Property EditMode As Boolean

    Property ImportDataMode As Boolean
End Class