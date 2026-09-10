Imports System.IdentityModel.Tokens.Jwt
Imports System.Security.Claims
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports Application.Autentication.JwtService
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.Practices.Unity


Public Class JwtFactory

    Public Shared Async Function GenerateToken(username As String, containerx As String) As Task(Of String)

        Using service As IJwtTokenService = Container.Current.Resolve(Of IJwtTokenService)()
            Return Await service.GenerateToken(username, containerx)
        End Using


    End Function

    Public Shared Async Function ValidateToken(token As String) As Task

        Using service As IJwtTokenService = Container.Current.Resolve(Of IJwtTokenService)()
            Await service.ValidateToken(token)
        End Using

    End Function

    Public Shared Function GetContainerFromToken() As (container As String, hisContainer As String, costContainer As String, securityContainer As String)?
        Dim httpRequestMessage = GetHttpRequestMessageProperty()
        If httpRequestMessage Is Nothing Then
            Return Nothing
        End If

        Dim httpRequest As HttpRequestMessageProperty = CType(httpRequestMessage, HttpRequestMessageProperty)
        Dim authorizationHeader As String = httpRequest.Headers("Authorization")

        If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Bearer ") Then
            Throw New SecurityTokenException("Token JWT no encontrado en la cabecera Authorization.")
        End If

        Dim token As String = authorizationHeader.Substring("Bearer ".Length).Trim()

        Dim container As String = ""
        Dim hisContainer As String = ""
        Dim costContainer As String = ""
        Dim securityContainer As String = ""
        Dim indigoVersion As String = ""

        If OperationContext.Current IsNot Nothing Then
            If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SESS_CONTAINER)) IsNot Nothing Then
                container = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SESS_CONTAINER, ConfigurationFile.SESS_NAME_SPACE)
            End If

            If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SESS_CONTAINER_HIS)) IsNot Nothing Then
                hisContainer = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SESS_CONTAINER_HIS, ConfigurationFile.SESS_NAME_SPACE)
            End If

            If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SESS_CONTAINER_INTEROPCOST)) IsNot Nothing Then
                costContainer = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SESS_CONTAINER_INTEROPCOST, ConfigurationFile.SESS_NAME_SPACE)
            End If

            If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME)) IsNot Nothing Then
                securityContainer = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME, ConfigurationFile.SESS_NAME_SPACE)
            End If

            If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.INDIGO_VERSION)) IsNot Nothing Then
                indigoVersion = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.INDIGO_VERSION, ConfigurationFile.SESS_NAME_SPACE)
            End If
        End If

        ServerSessionValues.Current.CurrentContainer = container
        ServerSessionValues.Current.CurrentHISContainer = hisContainer
        ServerSessionValues.Current.CurrentInteropCostContainer = costContainer
        ServerSessionValues.Current.BlobContainerName = System.Configuration.ConfigurationManager.AppSettings("BlobContainerName")
        ServerSessionValues.Current.CurrentBlobConnectionString = System.Configuration.ConfigurationManager.AppSettings("AzureBlobConnectionString")
        ServerSessionValues.Current.IndigoVersion = indigoVersion
        Return (container, hisContainer, costContainer, securityContainer)
    End Function

    Private Shared Function GetHttpRequestMessageProperty() As HttpRequestMessageProperty
        ' Acceder a IncomingMessageProperties dentro de OperationContext
        If OperationContext.Current IsNot Nothing AndAlso OperationContext.Current.IncomingMessageProperties.ContainsKey(HttpRequestMessageProperty.Name) Then
            ' Devolver el HttpRequestMessageProperty desde IncomingMessageProperties
            Return CType(OperationContext.Current.IncomingMessageProperties(HttpRequestMessageProperty.Name), HttpRequestMessageProperty)
        End If

        Return Nothing
    End Function
End Class
