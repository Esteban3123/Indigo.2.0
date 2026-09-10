Imports System.IO
Imports System.Net
Imports Domain.Base.Entities

Public Class ConsumeServiceSingleton

    Private Shared singleton As ConsumeServiceSingleton

    Shared ReadOnly Property instance As ConsumeServiceSingleton
        Get
            If singleton Is Nothing Then
                singleton = New ConsumeServiceSingleton()
            End If
            Return singleton
        End Get
    End Property

    Public Function GetObjectByUrl(url As String) As ActionResult(Of String)
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Credentials = CredentialCache.DefaultCredentials
        Dim response As HttpWebResponse = CType(request.GetResponse, HttpWebResponse)

        If response.StatusCode <> HttpStatusCode.OK Then
            response.Close()
            Return New ActionResult(Of String) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = response.StatusDescription}
        End If

        Dim dataStream As Stream = response.GetResponseStream
        Dim reader As StreamReader = New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd
        reader.Close()
        response.Close()

        Return New ActionResult(Of String) With {.ObjectEmbbeded = responseFromServer, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
    End Function

    Public Function PostObjectByUrlAndJson(url As String, json As String) As ActionResult(Of String)
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.Credentials = CredentialCache.DefaultCredentials

        Using streamWritter As New StreamWriter(request.GetRequestStream())
            streamWritter.Write(json)
            streamWritter.Flush()
            streamWritter.Close()
        End Using

        Dim response As HttpWebResponse = CType(request.GetResponse, HttpWebResponse)

        If response.StatusCode <> HttpStatusCode.OK Then
            response.Close()
            Return New ActionResult(Of String) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = response.StatusDescription}
        End If

        Dim dataStream As Stream = response.GetResponseStream
        Dim reader As StreamReader = New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd
        reader.Close()
        response.Close()

        Return New ActionResult(Of String) With {.ObjectEmbbeded = responseFromServer, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
    End Function

End Class
