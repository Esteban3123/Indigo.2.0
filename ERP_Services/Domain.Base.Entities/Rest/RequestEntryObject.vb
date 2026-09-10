Public Class RequestEntryObject
    Public Property Data As EntryData
    Public Property Source As String
    Public Property Action As String
    Public Property Db As String
    Public Property User_code As String
    Public Property Timestamp As String
    Public Property Message As String
    Public Property Status As Boolean

    Public Sub New(
                  data As Object,
                  source As String,
                  action As String,
                  db As String,
                  user_code As String,
                  timestamp As String,
                  message As String,
                  status As Boolean)
        Me.Data = data
        Me.Source = source
        Me.Action = action
        Me.Db = db
        Me.User_code = user_code
        Me.Timestamp = timestamp
        Me.Message = message
        Me.Status = status
    End Sub

    Public Sub New()
    End Sub
End Class

Public Class EntryData
    Public Property Code As String
End Class
