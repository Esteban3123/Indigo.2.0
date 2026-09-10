Imports Domain.Entities

Public Class AddRemissionEntranceDetailEventArgs
    Inherits EventArgs
    Property ListRemissionEntranceDetail As List(Of RemissionEntranceDetail)

    Property RemissionEntranceDetail As RemissionEntranceDetail

    Property EditMode As Boolean

    Property ImportDataMode As Boolean
End Class
