
#Region "Imports"

Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel
Imports System.Web.Services.Description
Imports Microsoft.Practices.Unity
Imports System.ComponentModel

#End Region

Public Class UnityInstanceProvider
    Implements IInstanceProvider

#Region "Members"

    Private _serviceType As Type
    Private _container As IUnityContainer

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Create a new instance of unity instance provider
    ''' </summary>
    ''' <param name="serviceType">The service where we apply the instance provider</param>
    Public Sub New(serviceType As Type)
        If serviceType Is Nothing Then
            Throw New ArgumentNullException("serviceType")
        End If

        Me._serviceType = serviceType
        Me._container = Container.Current
    End Sub

#End Region

#Region "IInstance Provider Members"

    ''' <summary>
    ''' <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
    ''' </summary>
    ''' <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
    ''' <param name="message"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
    ''' <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
    Public Function GetInstance(instanceContext As InstanceContext, message As System.ServiceModel.Channels.Message) As Object Implements IInstanceProvider.GetInstance
        'This is the only call to UNITY container in the whole solution
        Return Me._container.Resolve(Me._serviceType)
    End Function
    ''' <summary>
    ''' <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
    ''' </summary>
    ''' <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
    ''' <returns><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></returns>
    Public Function GetInstance(instanceContext As InstanceContext) As Object Implements IInstanceProvider.GetInstance
        Return GetInstance(instanceContext, Nothing)
    End Function

    ''' <summary>
    ''' <see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/>
    ''' </summary>
    ''' <param name="instanceContext"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
    ''' <param name="instance"><see cref="System.ServiceModel.Dispatcher.IInstanceProvider"/></param>
    Public Sub ReleaseInstance(instanceContext As InstanceContext, instance As Object) Implements IInstanceProvider.ReleaseInstance
        If TypeOf instance Is IDisposable Then
            DirectCast(instance, IDisposable).Dispose()
        End If
    End Sub

#End Region

End Class
