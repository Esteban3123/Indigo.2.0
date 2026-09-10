Imports Microsoft.IdentityModel.Tokens
Imports System.Runtime.Remoting
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher

Public Class JwtMessageInspector
    Implements IDispatchMessageInspector

#Region "IDispatchMessageInspector"

    Public Function AfterReceiveRequest(ByRef request As Channels.Message, channel As IClientChannel, instanceContext As InstanceContext) As Object Implements IDispatchMessageInspector.AfterReceiveRequest
        Try
            Dim messageProperty As Object = Nothing
            If Not request.Properties.TryGetValue(HttpRequestMessageProperty.Name, messageProperty) Then
                Throw New SecurityTokenException("No se encontró la propiedad HTTP.")
            End If

            Dim httpRequest As HttpRequestMessageProperty = CType(messageProperty, HttpRequestMessageProperty)
            Dim authorizationHeader As String = httpRequest.Headers("Authorization")

            If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Bearer ") Then
                Throw New SecurityTokenException("Token JWT no encontrado en la cabecera Authorization.")
            End If

            Dim token As String = authorizationHeader.Substring("Bearer ".Length).Trim()

            Task.WaitAll(JwtFactory.ValidateToken(token))

            Return Nothing

        Catch ex As SecurityTokenException
            Dim ticketId As String = GenerateTicketId()
            Throw New FaultException(String.Format("Acceso no autorizado. Código de incidencia: {0}", ticketId))

        Catch ex As AggregateException
            Dim ticketId As String = GenerateTicketId()
            Throw New FaultException(String.Format("Error al validar credenciales. Código de incidencia: {0}", ticketId))

        Catch ex As Exception
            Dim ticketId As String = GenerateTicketId()
            Throw New FaultException(String.Format("Error interno del servicio. Código de incidencia: {0}", ticketId))
        End Try
    End Function

    Public Sub BeforeSendReply(ByRef reply As Channels.Message, correlationState As Object) Implements IDispatchMessageInspector.BeforeSendReply

    End Sub

    Private Function GenerateTicketId() As String
        Return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
    End Function

#End Region

End Class