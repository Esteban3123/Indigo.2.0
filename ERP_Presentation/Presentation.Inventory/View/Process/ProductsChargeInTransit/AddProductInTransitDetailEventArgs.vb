Imports Domain.Entities

Public Class AddProductInTransitDetailEventArgs
    Inherits EventArgs
    Property ListProductInTransitDetail As List(Of ProductInTransitDetail)

    Property ProductInTransitDetail As ProductInTransitDetail

    Property EditMode As Boolean

    Property ImportDataMode As Boolean
End Class
