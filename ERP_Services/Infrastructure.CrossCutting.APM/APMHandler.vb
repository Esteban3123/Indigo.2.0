Public Interface ApmHandler

    Sub AddCustomAttribute(ByVal key As String, ByVal value As Object)

    Sub NoticeError(ByVal message As String)

    Sub NoticeError(ByVal ex As Exception)

End Interface
