Imports NewRelic.Api.Agent

Public Class NewRelicAPM
    Implements ApmHandler

    Dim transaction As ITransaction
    Public Sub New()
        Dim agent = NewRelic.Api.Agent.NewRelic.GetAgent()
        If agent IsNot Nothing Then
            Me.transaction = agent.CurrentTransaction
        End If
    End Sub

    Public Sub AddCustomAttribute(key As String, value As String) Implements ApmHandler.AddCustomAttribute
        If transaction IsNot Nothing Then
            transaction.AddCustomAttribute(key, value)
        End If
    End Sub

    Public Sub NoticeError(message As String) Implements ApmHandler.NoticeError
        Dim data = New Dictionary(Of String, String)
        NewRelic.Api.Agent.NewRelic.NoticeError(message, data)
    End Sub

    Public Sub NoticeError(ex As Exception) Implements ApmHandler.NoticeError
        NewRelic.Api.Agent.NewRelic.NoticeError(ex)
    End Sub
End Class
