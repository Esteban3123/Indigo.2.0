
#Region "Imports"

Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel
Imports System.ServiceModel.Description

#End Region

<AttributeUsage(AttributeTargets.Class)>
Public Class UnityMessageInspectorServiceBehavior
    Inherits Attribute
    Implements IServiceBehavior

#Region "IServiceBehavior"

    Public Sub AddBindingParameters(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase, endpoints As ObjectModel.Collection(Of ServiceEndpoint), bindingParameters As Channels.BindingParameterCollection) Implements IServiceBehavior.AddBindingParameters

    End Sub

    Public Sub ApplyDispatchBehavior(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase) Implements IServiceBehavior.ApplyDispatchBehavior
        For i As Integer = 0 To serviceHostBase.ChannelDispatchers.Count - 1
            Dim channelDispatcher As ChannelDispatcher = TryCast(serviceHostBase.ChannelDispatchers(i), ChannelDispatcher)
            If channelDispatcher IsNot Nothing Then
                For Each endpointDispatcher As EndpointDispatcher In channelDispatcher.Endpoints
                    Dim inspector As New UnityMessageInspector()
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector)
                Next
            End If
        Next
    End Sub

    Public Sub Validate(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase) Implements IServiceBehavior.Validate

    End Sub

#End Region

End Class
