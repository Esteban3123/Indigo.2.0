
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Text

Public Class PIntegrationErrorLogs
    Private ReadOnly _apiBaseUrl As String

    Public Sub New(apiBaseUrl As String)
        _apiBaseUrl = apiBaseUrl
    End Sub

    ''' <summary>
    ''' Obtiene los mensajes paginados desde el endpoint de Azure Function
    ''' </summary>
    ''' <param name="pageNumber">Número de página</param>
    ''' <param name="pageSize">Tamaño de la página</param>
    ''' <param name="invalidLogs">Espera 1 o 0, 1 si se necesitan los logs erroneos, y 0 si se necesitan los logs validos</param>
    ''' <returns>JSON con la cantidad de logs solicitados</returns>
    Public Async Function GetIntegrationLogsPagedAsync(pageNumber As Integer, pageSize As Integer, invalidLogs As Integer) As Task(Of String)
        Using httpClient As New HttpClient()
            httpClient.BaseAddress = New Uri(_apiBaseUrl)

            ' Construir la URL con los parámetros de paginación
            Dim queryParams = $"integrationlogs?pageNumber={pageNumber}&pageSize={pageSize}&invalidLogs={invalidLogs}"
            Dim response = Await httpClient.GetAsync(queryParams)

            response.EnsureSuccessStatusCode() ' Lanza excepción si el estado no es 2xx

            ' Retornar el contenido de la respuesta como JSON
            Return Await response.Content.ReadAsStringAsync()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los detalles de un IntegrationLog desde el endpoint de Azure Function.
    ''' </summary>
    ''' <param name="logId">ID del IntegrationLog para el cual se requieren los detalles.</param>
    ''' <returns>JSON con los detalles del IntegrationLog.</returns>
    Public Async Function GetIntegrationLogDetailsAsync(logId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of String))
        Using httpClient As New HttpClient()
            httpClient.BaseAddress = New Uri(_apiBaseUrl)

            ' Construir la URL con el parámetro logId
            Dim queryParams = $"integrationlogs/{logId}/details"
            Dim response = Await httpClient.GetAsync(queryParams)

            If response.StatusCode.Equals(Net.HttpStatusCode.NotFound) Then
                Throw New HttpRequestException($"Recurso No encontrado ({response.StatusCode})")
            End If

            If response.StatusCode.Equals(Net.HttpStatusCode.InternalServerError) Then
                Throw New HttpRequestException($"Hubo un problema al obtener los detalles del mensaje seleccionado ({response.StatusCode})")
            End If

            If response.StatusCode.Equals(Net.HttpStatusCode.NoContent) Then
                Return New Domain.Base.Entities.ActionResult(Of String) With {.StateResult = False, .Message = $"El mensaje seleccionado no posee detalles asociados"}
            End If

            Dim content = Await response.Content.ReadAsStringAsync()
            Return New Domain.Base.Entities.ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = content}
        End Using
    End Function

    ''' <summary>
    ''' Reenvía un mensaje llamando al endpoint de Azure Function.
    ''' </summary>
    ''' <param name="integrationLogId">ID del mensaje que se desea reenviar.</param>
    ''' <returns>Un Task que devuelve un mensaje de resultado.</returns>
    ''' <exception cref="HttpRequestException">Lanza excepción si el estado de la respuesta no es exitoso.</exception>
    Public Async Function ResendMessageAsync(integrationLogId As String) As Task(Of String)
        If String.IsNullOrWhiteSpace(integrationLogId) Then
            Throw New ArgumentException("MessageId cannot be null or empty.", NameOf(integrationLogId))
        End If

        Using httpClient As New HttpClient()
            httpClient.BaseAddress = New Uri(_apiBaseUrl)

            Dim requestBody = New With {integrationLogId}
            Dim content = New StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")

            Dim response = Await httpClient.PostAsync("integrationlogs/resend", content)

            If response.IsSuccessStatusCode Then
                Return Await response.Content.ReadAsStringAsync()
            Else
                Dim errorMessage = Await response.Content.ReadAsStringAsync()
                Throw New HttpRequestException($"No se pudo reenviar el mensaje seleccionado. Status Code: {response.StatusCode}, Error: {errorMessage}")
            End If
        End Using
    End Function
End Class
