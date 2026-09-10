'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
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
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    Public Function GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As Domain.Entities.SurgeriesPercentageManual Implements IContractServiceSurgeriesPercentageManual.GetSurgeriesPercentageManualByRateManualIdInterventionType
        Using service As ISurgeriesPercentageManualAdminService = Container.Current.Resolve(Of ISurgeriesPercentageManualAdminService)()
            Return service.GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId, InterventionType)
        End Using
        'Return _surgeriesPercentageManualAdminService.GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId, InterventionType)
    End Function
End Class
