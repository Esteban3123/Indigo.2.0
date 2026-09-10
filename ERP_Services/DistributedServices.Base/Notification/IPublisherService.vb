'***********************************************************************
' Assembly         : DistributedService.Base
' Author           : Juan F. Tamayo
' Created          : 2013-08-01
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-08-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel

#End Region

''' <summary>
''' Contrato que define las caracteristicas básicas de un servicio publicador
''' </summary>
<ServiceContract()>
Public Interface IPublisherService

#Region "Operations"

    ''' <summary>
    ''' Subscribe un cliente al servicio de notificación correspondiente
    ''' </summary>
    ''' <param name="subscriber">Cliente a subscribir</param>
    <OperationContract(IsOneWay:=True)>
    Sub Subscribe(ByVal subscriber As Subscriber)

    ''' <summary>
    ''' Desuscribe un cliente del servicio de notificación correspondiente
    ''' </summary>
    ''' <param name="subscriber">Cliente a desuscribir</param>
    <OperationContract(IsOneWay:=True)>
    Sub Unsubscribe(ByVal subscriber As Subscriber)

#End Region

End Interface
