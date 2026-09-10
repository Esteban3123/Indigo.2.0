Imports Domain.Entities

Public Class GetListEntranceVoucherDetailEventArgs
    Inherits EventArgs

    Property ListEntranceVoucherDetail As List(Of EntranceVoucherDetail)

    Property EntranceSource As Integer

End Class
