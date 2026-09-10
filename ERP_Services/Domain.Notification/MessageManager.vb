Imports System.Net
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Root

Public Class MessageManager
    Implements IDisposable

    Private Property Proxy As EmailNotificationProxy

    Public Sub New(GetUrlNotification As String, FxGetEmailNotification As String)
        ' Setting TLS 1.2 protocol '
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Me.Proxy = New EmailNotificationProxy(GetUrlNotification, FxGetEmailNotification)
    End Sub

    Public Async Function Send(message As MessageNotification) As Task(Of ActionResult(Of String))
        Try
            Return Await Me.Proxy.SendMessageNotification(message)
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            Me.Proxy = Nothing
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
