
#Region "Imports"

Imports System.ServiceModel.Description
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher

#End Region

<AttributeUsage(AttributeTargets.Class)>
Public Class UnityInstanceProviderServiceBehavior
    Inherits Attribute
    Implements IServiceBehavior

#Region "IServiceBehavior Members"

    Public Sub AddBindingParameters(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase, endpoints As ObjectModel.Collection(Of ServiceEndpoint), bindingParameters As BindingParameterCollection) Implements IServiceBehavior.AddBindingParameters

    End Sub

    Public Sub ApplyDispatchBehavior(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase) Implements IServiceBehavior.ApplyDispatchBehavior
        For Each item In serviceHostBase.ChannelDispatchers
            Dim dispatcher = TryCast(item, ChannelDispatcher)
            If dispatcher IsNot Nothing Then
                ' add new instance provider for each end point dispatcher
                dispatcher.Endpoints.ToList().ForEach(Sub(endpoint)
                                                          endpoint.DispatchRuntime.InstanceProvider = New UnityInstanceProvider(serviceDescription.ServiceType)
                                                      End Sub)
            End If
        Next
    End Sub

    Public Sub Validate(serviceDescription As ServiceDescription, serviceHostBase As ServiceHostBase) Implements IServiceBehavior.Validate

    End Sub

#End Region

End Class