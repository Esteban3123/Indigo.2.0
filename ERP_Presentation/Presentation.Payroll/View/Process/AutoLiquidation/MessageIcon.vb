Public Class MessageIcon

    Enum Icon
        WARNING
        [ERROR]
    End Enum


    Sub New(_icon As Icon, _message As String)
        IconMessage = _icon
        Message = _message
    End Sub

    Public Property IconMessage As Icon
    Public Property Message

End Class
