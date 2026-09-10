Imports Microsoft.IdentityModel.Tokens
Imports System.Runtime.Remoting
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher

Public Class JwtMessageInspector
    Implements IDispatchMessageInspector

#Region "IDispatchMessageInspector"

    Public Function AfterReceiveRequest(ByRef request As Channels.Message, channel As IClientChannel, instanceContext As InstanceContext) As Object Implements IDispatchMessageInspector.AfterReceiveRequest
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
    End Function

    Public Sub BeforeSendReply(ByRef reply As Channels.Message, correlationState As Object) Implements IDispatchMessageInspector.BeforeSendReply

    End Sub

#End Region

End Class