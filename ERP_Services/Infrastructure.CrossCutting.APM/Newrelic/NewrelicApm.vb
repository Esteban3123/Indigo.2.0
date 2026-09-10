Imports Infrastructure.APM
Imports NewRelic.Api.Agent

Public Class NewrelicApm
    Implements ApmHandler

    Private transaction As ITransaction

    Public Sub New()
        Dim agent As IAgent = NewRelic.Api.Agent.NewRelic.GetAgent()

        If agent IsNot Nothing Then
            transaction = agent.CurrentTransaction
        End If
    End Sub

    Public Sub AddCustomAttribute(ByVal key As String, ByVal value As Object) Implements ApmHandler.AddCustomAttribute
        If transaction IsNot Nothing Then transaction.AddCustomAttribute(key, value)
    End Sub

    Public Sub NoticeError(ByVal message As String) Implements ApmHandler.NoticeError
        Dim data As IDictionary(Of String, String) = New Dictionary(Of String, String)()
        NewRelic.Api.Agent.NewRelic.NoticeError(message, data)
    End Sub

    Public Sub NoticeError(ByVal ex As Exception) Implements ApmHandler.NoticeError
        NewRelic.Api.Agent.NewRelic.NoticeError(ex)
    End Sub

End Class
