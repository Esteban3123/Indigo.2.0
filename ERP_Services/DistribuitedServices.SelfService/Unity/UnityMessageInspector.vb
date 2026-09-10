
#Region "Imports"

Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class UnityMessageInspector
    Implements IDispatchMessageInspector

#Region "IDispatchMessageInspector"

    Public Function AfterReceiveRequest(ByRef request As Channels.Message, channel As IClientChannel, instanceContext As InstanceContext) As Object Implements IDispatchMessageInspector.AfterReceiveRequest
        Dim res As String = request.Headers.GetHeader(Of String)(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        If res IsNot Nothing AndAlso Not res.Equals(String.Empty) Then
            ServerSessionValues.Current.CurrentContainer = res
        End If
        Return Nothing
    End Function

    Public Sub BeforeSendReply(ByRef reply As Channels.Message, correlationState As Object) Implements IDispatchMessageInspector.BeforeSendReply

    End Sub

#End Region

End Class