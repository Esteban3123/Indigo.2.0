'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ISurgeriesPercentageManualRepository
    Inherits IRepository(Of SurgeriesPercentageManual)

    ''' <summary>
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As SurgeriesPercentageManual
End Interface
