Imports System.Net
Imports System.Net.Http
Imports Domain.Base.Entities
Imports Newtonsoft.Json

Public Class EmailNotificationProxy

    Public Property GetUrlNotification As String

    Public Property FxGetEmailNotification As String

    Public Sub New(GetUrlNotification As String, FxGetEmailNotification As String)
        Me.GetUrlNotification = GetUrlNotification
        Me.FxGetEmailNotification = FxGetEmailNotification
    End Sub

    Public Async Function SendMessageNotification(message As MessageNotification) As Task(Of ActionResult(Of String))
        Dim client As New HttpClient
        client.BaseAddress = New Uri(Me.GetUrlNotification)
        client.DefaultRequestHeaders.Accept.Clear()
        client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))

        Dim json = JsonConvert.SerializeObject(message)

        Dim response As HttpResponseMessage = Await client.PostAsync(Me.FxGetEmailNotification, New StringContent(json))

        If response.StatusCode <> HttpStatusCode.OK Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = "Se presento un error al enviar la notificacion, codigo error: " + response.StatusCode}
        End If

        Return New ActionResult(Of String) With {.StateResult = True, .Message = response.Content.ReadAsStringAsync().Result}
    End Function

End Class
