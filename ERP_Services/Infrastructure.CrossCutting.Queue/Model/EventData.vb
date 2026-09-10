Imports Newtonsoft.Json

Public Class EventData

    Public data As Object
    Public source As String
    Public action As String
    Public db As String
    Public user_code As String
    Public timestamp As Integer
    <JsonIgnore>
    Public OutboxId As Integer?

    Public Sub New(_data As Object,
             _source As String,
             _action As String,
             _db As String,
             _userCode As String,
            _timestamp As Integer)
        data = _data
        source = _source
        action = _action
        db = _db
        timestamp = _timestamp
        user_code = _userCode
    End Sub

End Class
