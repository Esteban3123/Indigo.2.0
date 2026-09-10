'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceServiceOrderDetailDistribution
    ''' <summary>
    ''' metodo para obtener un detalle de la distribucion
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution)
End Interface
