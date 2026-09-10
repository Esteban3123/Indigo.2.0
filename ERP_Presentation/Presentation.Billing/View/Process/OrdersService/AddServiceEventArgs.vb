Imports Domain.Entities
Imports Domain.Base.Entities

Public Class AddServiceEventArgs
    Inherits EventArgs

    Property ListServiceOrderDetail As List(Of ServiceOrderDetail)
    Property ListServiceOrderDetailDelete As ObjectList
    Property ServiceOrderDetail As ServiceOrderDetail
    Property EditMode As Boolean
    Property ViewToPackageItems As DevExpress.XtraGrid.Views.Grid.GridView
    Property ContractPackageId As Integer?
End Class
