#Region "Imports"

Imports System.ServiceModel.Description

#End Region

Public Class UnityMessageBehavior
    Implements IEndpointBehavior

#Region "IEndpointBehavior"

    Public Sub AddBindingParameters(endpoint As ServiceEndpoint, bindingParameters As ServiceModel.Channels.BindingParameterCollection) Implements IEndpointBehavior.AddBindingParameters

    End Sub

    Public Sub ApplyClientBehavior(endpoint As ServiceEndpoint, clientRuntime As ServiceModel.Dispatcher.ClientRuntime) Implements IEndpointBehavior.ApplyClientBehavior
        clientRuntime.MessageInspectors.Add(New UnityMessageInspector())
    End Sub

    Public Sub ApplyDispatchBehavior(endpoint As ServiceEndpoint, endpointDispatcher As ServiceModel.Dispatcher.EndpointDispatcher) Implements IEndpointBehavior.ApplyDispatchBehavior

    End Sub

    Public Sub Validate(endpoint As ServiceEndpoint) Implements IEndpointBehavior.Validate

    End Sub

#End Region

End Class
