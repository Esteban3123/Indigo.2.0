Public Interface ApmHandler

    Sub AddCustomAttribute(key As String, value As String)

    Sub NoticeError(message As String)

    Sub NoticeError(ex As Exception)

End Interface
