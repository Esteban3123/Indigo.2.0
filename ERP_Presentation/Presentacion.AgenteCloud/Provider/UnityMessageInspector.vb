#Region "Imports"

Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher
Imports Infrastructure.Base
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class UnityMessageInspector
    Implements IClientMessageInspector

    Private Const AUTHORIZATION_TOKEN As String = "Authorization"
    Public Sub AfterReceiveReply(ByRef reply As ServiceModel.Channels.Message, correlationState As Object) Implements IClientMessageInspector.AfterReceiveReply
    End Sub

    Public Function BeforeSendRequest(ByRef request As ServiceModel.Channels.Message, channel As ServiceModel.IClientChannel) As Object Implements IClientMessageInspector.BeforeSendRequest
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer

        If TokenManager.Instance.IsTokenExpired() Then
            ' Si hay un manejador de logout registrado, llamarlo
            LogoutManager.TriggerLogoutIfNeeded()
            Throw New IndigoTokenException("Token expirado. Se ha cerrado la sesión automáticamente.")
        End If

        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If

        Dim mess As New MessageHeader(Of String)(TransactionalContainer)
        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        Dim mess1 As New MessageHeader(Of String)(Infrastructure.CrossCutting.Base.SessionValues.Instance.HisContainer)
        Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_HIS, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        Dim mess2 As New MessageHeader(Of String)(Infrastructure.CrossCutting.Base.SessionValues.Instance.InteropCostContainer)
        Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_INTEROPCOST, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        Dim mess3 As New MessageHeader(Of String)(Infrastructure.CrossCutting.Base.SessionValues.Instance.IndigoVersion)
        Dim header3 As System.ServiceModel.Channels.MessageHeader = mess3.GetUntypedHeader(Infrastructure.CrossCutting.Base.ConfigurationFile.INDIGO_VERSION, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        Dim mess4 As New MessageHeader(Of Infrastructure.CrossCutting.Base.AuditMessage)(Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf)
        Dim header4 As System.ServiceModel.Channels.MessageHeader = mess4.GetUntypedHeader(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_AUDITMESSAGE, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        ' Asegurarse de tener la propiedad HTTP
        Dim httpRequest As HttpRequestMessageProperty = Nothing

        If request.Properties.ContainsKey(HttpRequestMessageProperty.Name) Then
            httpRequest = CType(request.Properties(HttpRequestMessageProperty.Name), HttpRequestMessageProperty)
        Else
            httpRequest = New HttpRequestMessageProperty()
            request.Properties.Add(HttpRequestMessageProperty.Name, httpRequest)
        End If

        ' Agregar Authorization Header
        Dim token = TokenManager.Instance.GetToken()
        If Not String.IsNullOrEmpty(token) Then
            httpRequest.Headers(AUTHORIZATION_TOKEN) = "Bearer " & token
        End If

        request.Headers.Add(header)
        request.Headers.Add(header1)
        request.Headers.Add(header2)
        request.Headers.Add(header3)
        request.Headers.Add(header4)
        Return Nothing
    End Function

End Class
