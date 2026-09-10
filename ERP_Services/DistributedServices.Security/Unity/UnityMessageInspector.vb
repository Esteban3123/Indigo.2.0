
#Region "Imports"

Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class UnityMessageInspector
    Implements IDispatchMessageInspector

#Region "IDispatchMessageInspector"

    Public Function AfterReceiveRequest(ByRef request As Channels.Message, channel As IClientChannel, instanceContext As InstanceContext) As Object Implements IDispatchMessageInspector.AfterReceiveRequest
        Dim res As String = Nothing
        Dim res1 As String = Nothing
        Dim res2 As String = Nothing

        If request.Headers.FirstOrDefault(Function(h) h.Name.Equals(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER)) IsNot Nothing Then
            res = request.Headers.GetHeader(Of String)(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        End If
        If request.Headers.FirstOrDefault(Function(h) h.Name.Equals(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_HIS)) IsNot Nothing Then
            res1 = request.Headers.GetHeader(Of String)(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_HIS, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        End If
        If request.Headers.FirstOrDefault(Function(h) h.Name.Equals(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_INTEROPCOST)) IsNot Nothing Then
            res2 = request.Headers.GetHeader(Of String)(Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_CONTAINER_INTEROPCOST, Infrastructure.CrossCutting.Base.ConfigurationFile.SESS_NAME_SPACE)
        End If

        If res IsNot Nothing AndAlso Not res.Equals(String.Empty) Then
            ServerSessionValues.Current.CurrentContainer = res
        End If
        If res1 IsNot Nothing AndAlso Not res1.Equals(String.Empty) Then
            ServerSessionValues.Current.CurrentHISContainer = res1
        End If
        If res2 IsNot Nothing AndAlso Not res2.Equals(String.Empty) Then
            ServerSessionValues.Current.CurrentInteropCostContainer = res2
        End If
        Return Nothing
    End Function

    Public Sub BeforeSendReply(ByRef reply As Channels.Message, correlationState As Object) Implements IDispatchMessageInspector.BeforeSendReply

    End Sub

#End Region

End Class