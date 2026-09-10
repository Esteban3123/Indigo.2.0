'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISurgeriesPercentageManualAdminService
    Inherits IDisposable
    ''' <summary>
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As SurgeriesPercentageManual
End Interface
