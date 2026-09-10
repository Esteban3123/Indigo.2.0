'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IServiceOrderDetailDistributionAdminService
    Inherits IDisposable
    ''' <summary>
    ''' metodo para obtener un detalle de la distribucion
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution)
End Interface
