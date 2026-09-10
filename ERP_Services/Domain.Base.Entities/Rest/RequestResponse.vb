Public Class RequestResponse(Of T)
    Public Property Status As Boolean

    Public Property Code As String

    Public Property Data As T

    Public Property Message As String

    Public Sub New(status As Boolean, code As String, data As T, message As String)
        Me.Status = status
        Me.Code = code
        Me.Data = data
        Me.Message = message
    End Sub

    Public Sub New()
    End Sub

End Class