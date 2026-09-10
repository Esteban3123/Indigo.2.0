'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService
    ''' <summary>
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    ''' <param name="rateManualId"></param>
    ''' <param name="ipsService"></param>
    ''' <param name="surgicalGrouopId"></param>
    ''' <param name="UVRNumber"></param>
    ''' <param name="serviceManual"></param>
    ''' <returns></returns>
    Public Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As Domain.Entities.RateManualDetailSurgical Implements IContractRateManualDetailSurgical.GetSurgicalDetailServiceOrder
        Using service As IRateManualDetailSurgicalAdminService = Container.Current.Resolve(Of IRateManualDetailSurgicalAdminService)()
            Return service.GetSurgicalDetailServiceOrder(rateManualId, ipsService, surgicalGrouopId, UVRNumber, serviceManual)
        End Using
        'Return _rateManualDetailSurgicalAdminService.GetSurgicalDetailServiceOrder(rateManualId, ipsService, surgicalGrouopId, UVRNumber, serviceManual)
    End Function
End Class
