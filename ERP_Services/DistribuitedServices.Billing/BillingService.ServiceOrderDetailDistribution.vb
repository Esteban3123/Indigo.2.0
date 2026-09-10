'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService
    ''' <summary>
    ''' metodo para obtener un detalle de la distribucion
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of Domain.Entities.ServiceOrderDetailDistribution) Implements IBillingServiceServiceOrderDetailDistribution.GetServiceOrderDetailDistributionByServideOrderDetailId
        Using service As IServiceOrderDetailDistributionAdminService = Container.Current.Resolve(Of IServiceOrderDetailDistributionAdminService)()
            Return service.GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId)
        End Using
        'Return _serviceOrderDetailDistributionAdminService.GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId)
    End Function
End Class
